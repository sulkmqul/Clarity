using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Clarity;
using Clarity.GUI;

namespace ClarityOrbit.TileSrcSelectView
{
    /// <summary>
    /// チップ画像選択View
    /// </summary>
    public partial class TileSrcSelectViewDockingContent : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public TileSrcSelectViewDockingContent()
        {
            InitializeComponent();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面の初期化
        /// </summary>
        public void InitWindow()
        {
            //既存のページの削除
            this.tabControlTileSrc.TabPages.Clear();

            //初期表示を行う
            OrbitGlobal.ProjectData.TileSrcImageList.ForEach(x =>
            {
                this.DisplayTileSrcImageInfo(x);
            });
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 新しい元画像を追加する
        /// </summary>
        /// <param name="filepath"></param>
        private void AddTileSrcImage(string filepath)
        {            

            //元データ追加申請
            TileSrcImageInfo tsinfo =  OrbitGlobal.ProjectData.AddTileSrcImage(filepath);
            this.DisplayTileSrcImageInfo(tsinfo);

            OrbitGlobal.SendEvent(EOrbitEventID.TileSrcImageAdd);
        }

        /// <summary>
        /// 元画像情報のページを作成して描画する
        /// </summary>
        /// <param name="tsinfo"></param>
        private void DisplayTileSrcImageInfo(TileSrcImageInfo tsinfo)
        {
            //新しいページの作成
            TabPage npage = new TabPage();

            //管理コントロールの作成
            TileSrcSelectControl ct = new TileSrcSelectControl(tsinfo);
            ct.Initialize();

            ct.Dock = DockStyle.Fill;


            //追加ページへADD
            npage.Controls.Add(ct);
            //タイトル設定
            npage.Text = Path.GetFileName(tsinfo.FilePath);

            //表示へ
            this.tabControlTileSrc.Controls.Add(npage);
            this.tabControlTileSrc.SelectedTab = npage;

            
        }


        /// <summary>
        /// タイル映像の削除
        /// </summary>
        private void RemoveTileSrcImage()
        {
            //現在の選択ページを取得
            TabPage stab = this.tabControlTileSrc.SelectedTab;
            //格納管理の取得
            TileSrcSelectControl scon = stab.Controls.Cast<TileSrcSelectControl>().First();


            //対象を削除する
            OrbitGlobal.ProjectData.RemoveTileSrcImage(scon.SInfo);
            //ページから削除
            this.tabControlTileSrc.Controls.Remove(stab);


            OrbitGlobal.SendEvent(EOrbitEventID.TileSrcImageRemove);

        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// チップ画像ADDボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTipAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (OrbitGlobal.Mana.Project == null)
                {
                    return;
                }

                //画像ファイルの選択
                string filepath = "";
                using (OpenFileDialog diag = new OpenFileDialog())
                {
                    diag.Filter = OrbitGlobal.ImageFileFilter;
                    DialogResult dret = diag.ShowDialog(this);
                    if (dret != DialogResult.OK)
                    {
                        return;
                    }
                    filepath = diag.FileName;
                }

                //Tabの追加
                this.AddTileSrcImage(filepath);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }

        /// <summary>
        /// チップ画像削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTipRemove_Click(object sender, EventArgs e)
        {
            try
            {
                //削除処理
                this.RemoveTileSrcImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }
        }
    }
}
