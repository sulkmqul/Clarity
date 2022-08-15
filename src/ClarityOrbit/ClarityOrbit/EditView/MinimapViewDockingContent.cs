using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Util;
using Clarity.GUI;

namespace ClarityOrbit.EditView
{
    /// <summary>
    /// マップの簡易View
    /// </summary>
    public partial class MinimapViewDockingContent : WeifenLuo.WinFormsUI.Docking.DockContent 
    {
        public MinimapViewDockingContent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 一定時間ごとにminimap更新を行う
        /// </summary>
        private ClarityCycling? MinimapUpdateCycle;


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面の初期化
        /// </summary>
        public void InitForm()
        {
            //表示者登録
            this.clarityViewerMinimap.AddDisplayer(new MinimapDisplayer());

            //言って時間ごとに、変更を反映するminimapの再構築を行う
            this.MinimapUpdateCycle = new ClarityCycling();

            //表示されている時だけ有効化
            this.MinimapUpdateCycle.EnabledProc = () => {
                return this.Visible; 
            };
            //更新処理の実行
            this.MinimapUpdateCycle.StartCycling(() =>
            {
                //サイズの取得
                Size tcount = OrbitGlobal.Project?.BaseInfo.TileCount ?? new Size(-1, -1);
                if (tcount.Width < 0 || tcount.Height < 0)
                {
                    return;
                }

                //再作成チェック
                bool ckret = this.CheckReCreate(tcount);
                if (ckret == true)
                {
                    this.clarityViewerMinimap.Init(new SizeF(tcount.Width, tcount.Height));
                }

                //再描画指示
                this.Refresh();

            }, 500);

        }




        /// <summary>
        /// 画像を丸ごと作り直すべきかを検討する true=作り直す必要有
        /// </summary>        
        /// <returns></returns>
        private bool CheckReCreate(Size tcount)
        {            

            //サイズが違う
            if (this.clarityViewerMinimap.SrcRect.Width != tcount.Width || this.clarityViewerMinimap.SrcRect.Height != tcount.Height)
            {
                return true;
            }
            return false;

        }

      







        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimapDockingContent_Load(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// 閉じられるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MinimapDockingContent_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.MinimapUpdateCycle == null)
            {
                return;
            }
            await this.MinimapUpdateCycle.StopCycling();
        }

        /// <summary>
        /// Minimap上でマウスが動いたとき
        /// </summary>
        /// <param name="minfo"></param>
        /// <param name="ivt"></param>
        private void clarityViewerMinimap_ClarityViewerMouseMoveEvent(MouseInfo minfo, ImageViewerTranslator ivt)
        {
            if (minfo.DownButton != MouseButtons.Left)
            {
                return;
            }
            if (OrbitGlobal.Project == null)
            {
                return;
            }
            
            //位置をindexに変換
            PointF ipos = ivt.DispPointToSrcPoint(minfo.NowPos);
            int ix = Convert.ToInt32(ipos.X);
            int iy = Convert.ToInt32(ipos.Y);
            OrbitGlobal.Mana.MForm.orbitEditViewControl1.SetCameraAtTileIndex(ix, iy);
        }

        /// <summary>
        /// ミニマップ上でマウスが離された時
        /// </summary>
        /// <param name="minfo"></param>
        /// <param name="ivt"></param>
        private void clarityViewerMinimap_ClarityViewerMouseUpEvent(MouseInfo minfo, ImageViewerTranslator ivt)
        {
            if (OrbitGlobal.Project == null)
            {
                return;
            }

            //位置をindexに変換
            PointF ipos = ivt.DispPointToSrcPoint(minfo.NowPos);
            int ix = Convert.ToInt32(ipos.X);
            int iy = Convert.ToInt32(ipos.Y);

            OrbitGlobal.Mana.MForm.orbitEditViewControl1.SetCameraAtTileIndex(ix, iy);
        }
    }
}
