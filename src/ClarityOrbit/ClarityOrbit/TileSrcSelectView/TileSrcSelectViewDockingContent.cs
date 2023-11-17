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
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 新しい元画像を追加する
        /// </summary>
        /// <param name="filepath"></param>
        private void AddTileSrcImage(string filepath)
        {
            //新しいページの作成
            var npage = this.CreateAddTileImageNewPage(filepath);
            //ADD
            this.tabControlTileSrc.Controls.Add(npage);
        }


        /// <summary>
        /// 新しい画像ページの作成
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private TabPage CreateAddTileImageNewPage(string filepath)
        {
            /*
            //ページの追加
            TileImageSrcInfo data = OrbitGlobal.Project.AddNewTileImageSrc(filepath);
            TabPage ans = new TabPage();
            ans.Text = data.Name;
            ans.Tag = data;
            ans.AutoScroll = true;
            Clarity.GUI.ClarityViewer iv = new ClarityViewer();            
            ans.Controls.Add(iv);
            {
                //ImageViewerの初期化
                iv.Dock = DockStyle.Fill;
                //iv.Location = new Point(0, 0);
                //iv.Size = new Size(1000, 1000);
                iv.MinimapVisible = false;
                iv.ZoomMode = EClarityViewerZoomMode.LimitFit;
                iv.PosMode = EClarityViewerPositionMode.LeftTop;

                iv.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                iv.Init(data.TipImage);
                //ここで描画物を入れる
                iv.AddDisplayer(new SrcTileImageGridDisplayer() { SrcInfo = data });
            }

            return ans;*/
            return new TabPage();

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
            if (OrbitGlobal.Mana.Project == null)
            {
                return;
            }

            //ファイルの選択
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

        /// <summary>
        /// チップ画像削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTipRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
