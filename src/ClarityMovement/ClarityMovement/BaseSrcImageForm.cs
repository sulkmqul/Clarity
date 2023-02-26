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
    /// 元画像の基底画面
    /// </summary>
    public partial class BaseSrcImageForm : Form
    {
        public BaseSrcImageForm()
        {
            InitializeComponent();
        }

        protected class ImageSet
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


        /// <summary>
        /// 全体データ
        /// </summary>
        protected List<ImageSet> DataList { get; set; } = new List<ImageSet>();
        /// <summary>
        /// 削除データ
        /// </summary>
        protected List<ImageSet> RemoveList { get; set; } = new List<ImageSet>();


        /// <summary>
        /// 画像追加sub
        /// </summary>
        protected Subject<ImageSet> ImageAddSub { get; set; } = new Subject<ImageSet>();

        /// <summary>
        /// 画像削除Sub
        /// </summary>
        protected Subject<ListViewItem> ImageRemoveSub { get; set; } = new Subject<ListViewItem>();

        /// <summary>
        /// rx解放
        /// </summary>
        protected CompositeDisposable SubjectRemover = new CompositeDisposable();


        /// <summary>
        /// 自身の表示と削除処理
        /// </summary>
        protected virtual void InitForm()
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
                this.listViewSrcImage.LargeImageList = this.imageList1;
                this.listViewSrcImage.SmallImageList =  this.imageList1;
                //this.listViewSrcImage.StateImageList= this.imageList1;
                

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

            //rx制御
            this.SubjectRemover.Add(di);
            this.SubjectRemover.Add(deldi);


            //既存データの読み込み
            this.LoadExistingImage();
        }



        /// <summary>
        /// 既存データの読み込み
        /// </summary>
        protected void LoadExistingImage()
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
        /// 選択画像の削除
        /// </summary>
        protected void RemoveSelectImage()
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
        /// 選択データを取得する。
        /// </summary>
        /// <returns></returns>
        protected List<ImageSet> GetSelectImageSet()
        {
            List<ImageSet> anslist = new List<ImageSet>();
            foreach (ListViewItem li in this.listViewSrcImage.SelectedItems)
            {
                ImageSet? data = li.Tag as ImageSet;
                if (data == null)
                {
                    continue;
                }
                anslist.Add(data);
            }
            return anslist;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//        
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseSrcImageForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 表示された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseSrcImageForm_Shown(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// 解除されるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseSrcImageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.SubjectRemover.Dispose();
        }
    }
}
