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
using System.Reactive.Linq;

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

        IDisposable RefleshUnSubscrible;
        IDisposable OperationUnSubscrible;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面の初期化
        /// </summary>
        public void InitForm()
        {
            //表示者登録
            this.clarityViewerMinimap.AddDisplayer(new MinimapDisplayer());

            //編集登録
            this.RefleshUnSubscrible = OrbitGlobal.Subject.TipEditSubject.Subscribe((x) =>
            {
                this.clarityViewerMinimap.Refresh();
            });

            //制御がはいった
            this.OperationUnSubscrible = OrbitGlobal.Subject.OperationSubject.Subscribe(x =>
            {
                if (OrbitGlobal.Project == null)
                {
                    return;
                }

                //再作成するべき？
                Size tcount = OrbitGlobal.Project.BaseInfo.TileCount;
                bool ckret = this.CheckReCreate(tcount);
                if (ckret == true || x.Operation == EOrbitOperation.NewProject || x.Operation == EOrbitOperation.OpenProject)
                {
                    this.clarityViewerMinimap.Init(new SizeF(tcount.Width, tcount.Height));
                }

            });

        }




        /// <summary>
        /// 画像を丸ごと作り直すべきかを検討する true=作り直す必要有
        /// </summary>        
        /// <returns></returns>
        private bool CheckReCreate(Size tcount)
        {
            if (this.clarityViewerMinimap.InitializedViewFlag == false)
            {
                return true;
            }

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
        private void MinimapDockingContent_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.RefleshUnSubscrible.Dispose();
            this.OperationUnSubscrible.Dispose();
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

            if (minfo.DownButton != MouseButtons.Left)
            {
                return;
            }

            //位置をindexに変換
            PointF ipos = ivt.DispPointToSrcPoint(minfo.NowPos);
            int ix = Convert.ToInt32(ipos.X);
            int iy = Convert.ToInt32(ipos.Y);

            OrbitGlobal.Mana.MForm.orbitEditViewControl1.SetCameraAtTileIndex(ix, iy);
        }

        private void MinimapViewDockingContent_Shown(object sender, EventArgs e)
        {
            
        }

        private void MinimapViewDockingContent_Resize(object sender, EventArgs e)
        {
            
        }
    }
}
