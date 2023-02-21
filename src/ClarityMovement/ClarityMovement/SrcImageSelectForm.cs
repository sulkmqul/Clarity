using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.GUI;




namespace ClarityMovement
{
    /// <summary>
    /// 元画像の読み込みと選択
    /// </summary>
    public partial class SrcImageSelectForm : Form
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f">true:読み込み画像選択画面、false:読み込み済み画像選択画面</param>
        public SrcImageSelectForm(bool f)
        {
            InitializeComponent();

            //
            this.WorkFlag = f;
        }
        


        class ImageSet
        {
            /// <summary>
            /// タグ名
            /// </summary>
            public string Name;

            /// <summary>
            /// 画像
            /// </summary>
            public Bitmap DataImage;

            /// <summary>
            /// 元ネタファイルパス
            /// </summary>
            public string FullPath;

            /// <summary>
            /// 元データ
            /// </summary>
            public CmImageData? SrcObject = null;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 選択モードの時の選択データ
        /// </summary>
        public CmImageData? SelectedData
        {
            get
            {
                //importの時は無し
                if (this.WorkFlag == true)
                {
                    return null;
                }

                //選択なし
                if (this.listViewSrcImage.SelectedItems.Count <= 0)
                {
                    return null;
                }

                //現在の選択を取得
                ImageSet data = (ImageSet)this.listViewSrcImage.SelectedItems[0].Tag;
                return data.SrcObject;
                
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 動作モード true=import false=select
        /// </summary>
        private bool WorkFlag;

        /// <summary>
        /// 全体データ
        /// </summary>
        private List<ImageSet> DataList { get; set; } = new List<ImageSet>();
        /// <summary>
        /// 削除データ
        /// </summary>
        private List<ImageSet> RemoveList { get; set; } = new List<ImageSet>();


        /// <summary>
        /// 画像追加sub
        /// </summary>
        private Subject<ImageSet> ImageAddSub = new Subject<ImageSet>();

        /// <summary>
        /// 画像削除Sub
        /// </summary>
        private Subject<ListViewItem> ImageRemoveSub = new Subject<ListViewItem>();

        /// <summary>
        /// rx解放
        /// </summary>
        private CompositeDisposable SubjectRemover = new CompositeDisposable();


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        private void InitForm()
        {
            //画像追加の表示            
            var di = this.ImageAddSub.Subscribe(iset =>
            {
                //同じ名前のデータがないことを確認する
                int ct = this.DataList.Where(x => x.Name == iset.Name).Count();
                if (ct > 0)
                {
                    //MessageBox.Show(this, $"[{iset.Name}] already added.");
                    Console.WriteLine($"[{iset.Name}] already added.");
                    return;
                }


                //データ本体に追加
                this.DataList.Add(iset);

                //追加表示
                this.imageList1.Images.Add(iset.DataImage);
                ListViewItem item = new ListViewItem(iset.Name, this.imageList1.Images.Count - 1);
                item.Tag = iset;
                this.listViewSrcImage.Items.Add(item);                
                
            });

            //画像の削除
            var deldi = this.ImageRemoveSub.Subscribe(ritem =>
            {
                //削除
                ImageSet idata = (ImageSet)ritem.Tag;
                this.listViewSrcImage.Items.Remove(ritem);

                //削除
                this.DataList.Remove(idata);

                //元ネタがある場合は削除リストに追加して置く
                if (idata.SrcObject != null)
                {
                    this.RemoveList.Add(idata);
                }
            });

            this.SubjectRemover.Add(di);
            this.SubjectRemover.Add(deldi);


            //コントロール制御
            this.buttonAddImage.Visible = this.WorkFlag;
            

            //既存データの読み込み
            this.LoadExistingImage();
            
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 既存データの読み込み
        /// </summary>
        private void LoadExistingImage()
        {
            if (CmGlobal.Project.Value == null)
            {
                return;
            }

            //既存データの取得
            var ilist = CmGlobal.Project.Value.ImageDataMana.GetImageList();

            //全データの変換読み込み
            ilist.ForEach(x =>
            {
                ImageSet data = new ImageSet();
                data.Name = x.ImageDataName;
                data.DataImage = x.Image;
                data.FullPath = x.FilePath;
                data.SrcObject = x;

                this.ImageAddSub.OnNext(data);
            });

            
        }



        /// <summary>
        /// データの追加
        /// </summary>
        /// <param name="filepathvec">読み込みファイルパス一式</param>
        /// <returns></returns>
        private async Task AddImageList(string[] filepathvec)
        {
            foreach (string fpath in filepathvec)
            {
                //画像で読み込み
                ImageSet? data = await Task.Run(() => this.LoadImage(fpath));
                if (data != null)
                {
                    this.ImageAddSub.OnNext(data);
                    continue;
                }

                //失敗したらzip読み込み
                var ivlist = await this.LoadImageZip(fpath);                
                ivlist.ForEach(x => this.ImageAddSub.OnNext(x));
            }
        }

        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private ImageSet? LoadImage(string filepath)
        {
            try
            {
                Bitmap bit = new Bitmap(filepath);
                //Path.GetFileNameWithoutExtension(filepath), bit, filepath
                return new ImageSet() { Name = Path.GetFileNameWithoutExtension(filepath), DataImage = bit, FullPath = filepath };
                }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Zipファイルの展開読み込み
        /// </summary>
        /// <param name="filepath">zipファイルパス</param>
        /// <returns></returns>
        private async Task<List<ImageSet>> LoadImageZip(string filepath)
        {
            List<ImageSet> anslist = new List<ImageSet>();

            string rname = Path.GetFileNameWithoutExtension(filepath);

            using (ZipArchive zf = ZipFile.Open(filepath, ZipArchiveMode.Read))
            {
                foreach (var zent in zf.Entries)
                {
                    ImageSet adata = await Task.Run(()=>
                    {
                        //値の取得
                        Bitmap bit;
                        string fname = zent.FullName;
                        using (Stream data = zent.Open())
                        {
                            bit = new Bitmap(data);                            
                        }

                        //$"{rname}_{fname}", bit, filepath
                        return new ImageSet() { Name = fname, DataImage = bit, FullPath = filepath };
                    });

                    anslist.Add(adata);
                }
            }


            return anslist;

        }


        /// <summary>
        /// 選択画像の削除
        /// </summary>
        private void RemoveSelectImage()
        {
            //selecteditemだと削除したときに選択が解除されてしまうので全体で回してチェックする

            foreach (ListViewItem ritem in this.listViewSrcImage.Items)
            {
                if (ritem.Selected == false)
                {
                    continue;
                }
                                

                //削除
                this.ImageRemoveSub.OnNext(ritem);
            }


        }


        /// <summary>
        /// データ変更を適応する
        /// </summary>
        //private void ApplayFrameImages()
        //{
        //    if (CmGlobal.Project.Value == null)
        //    {
        //        return;
        //    }
        //    var mana = CmGlobal.Project.Value.ImageDataMana;

        //    foreach (ImageSet data in this.DataList)
        //    {
        //        //既存データはaddしない
        //        if (data.SrcObject != null)
        //        {
        //            return;
        //        }

        //        //追加
        //        mana.AddImage(data.Name, data.FullPath, data.DataImage);

        //    }

        //    //データの削除
        //    this.RemoveList.ForEach(x =>
        //    {
        //        if (x.SrcObject != null)
        //        {
        //            mana.RemoveImage(x.SrcObject.CmImageID);
        //        }
        //    });
        //}
        private async Task ApplayFrameImages()
        {
            if (CmGlobal.Project.Value == null)
            {
                return;
            }
            var mana = CmGlobal.Project.Value.ImageDataMana;

            foreach (ImageSet data in this.DataList)
            {
                await Task.Run(() =>
                {
                    //既存データはaddしない
                    if (data.SrcObject != null)
                    {
                        return;
                    }

                    //追加
                    mana.AddImage(data.Name, data.FullPath, data.DataImage);
                });

            }

            //データの削除
            foreach (ImageSet rdata in this.RemoveList)
            {
                await Task.Run(() =>
                {
                    if (rdata.SrcObject != null)
                    {
                        mana.RemoveImage(rdata.SrcObject.CmImageID);
                    }
                });
            };
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SrcImageSelectForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 画像追加ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAddImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Multiselect = true;
            diag.Filter = "zipファイル|*.zip|Image File|*.png;*.jpg;*,bmp|全てのファイル|*.*";
            var dret = diag.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            try
            {

                //画像の読み込み
                using (AsyncControlState sw = new AsyncControlState(this))
                {
                    await this.AddImageList(diag.FileNames);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"失敗 {ex}");
            }
        }

        /// <summary>
        /// okボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                //importの時
                if (this.WorkFlag == true)
                {
                    //作業を確定させる
                    using (AsyncControlState st = new AsyncControlState(this))
                    {
                        //画像の反映、追加されていないものをテクスチャ管理へ追加
                        await this.ApplayFrameImages();

                    }
                }
                else
                {
                    //選択があるかを確認
                    if (this.SelectedData == null)
                    {
                        MessageBox.Show(this, "選択されていません");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"失敗 {ex}");
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// cancelボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }

        /// <summary>
        /// 表示された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SrcImageSelectForm_Shown(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// キーが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewSrcImage_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //importの時だけ削除を有効化
                if (e.KeyCode == Keys.Delete && this.WorkFlag == true)
                {

                    this.RemoveSelectImage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"失敗 {ex}");
            }
        }

        /// <summary>
        /// 閉じられた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SrcImageSelectForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.SubjectRemover.Dispose();
        }

        /// <summary>
        /// ダブルクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewSrcImage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //選択モードでちゃんと選択されていなるなら確定closeする
            if (this.SelectedData != null && this.WorkFlag == false)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
