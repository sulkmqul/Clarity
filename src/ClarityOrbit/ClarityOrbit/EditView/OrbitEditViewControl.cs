using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityOrbit.EditView
{
    /// <summary>
    /// メインView画面
    /// </summary>
    public partial class OrbitEditViewControl : UserControl
    {
        public OrbitEditViewControl()
        {
            InitializeComponent();
            this.FData = new OrbitEditViewData();
            this.Logic = new OrbitEditViewControlLogic(this, this.FData);
        }

        /// <summary>
        /// 処理
        /// </summary>
        private OrbitEditViewControlLogic Logic;

        /// <summary>
        /// データ
        /// </summary>
        private OrbitEditViewData FData;

        /// <summary>
        /// マウス管理
        /// </summary>
        public Clarity.GUI.MouseInfo Minfo
        {
            get
            {
                return this.FData.MouseElement.MInfo;
            }
            set
            {
                this.FData.MouseElement.MInfo = value;
            }
        }

        internal static DisplayTemplateInfo TempInfo = new DisplayTemplateInfo();


        /// <summary>
        /// 画面座標から計算された直線において、Zが0となるworld座標を計算する
        /// </summary>
        /// <param name="px">画面座標X</param>
        /// <param name="py">画面座標Y</param>
        /// <returns></returns>
        internal static Vector3 CalcuZeroPos(int px, int py)
        {
            Vector3 st = Clarity.Engine.ClarityEngine.WindowToWorld(0, px, py, 0.0f);
            Vector3 ed = Clarity.Engine.ClarityEngine.WindowToWorld(0, px, py, 1.0f);
            Vector3 dir = ed - st;

            Vector3 stzvec = new Vector3(st.X, st.Y, 0.0f) - st;
            Vector3 stznvec = Vector3.Normalize(stzvec);

            //角度の計算
            Vector3 ndir = Vector3.Normalize(dir);
            float cos = Vector3.Dot(stznvec, ndir);

            float len = stzvec.Z / cos;
            Vector3 zerolen = ndir * len;
            Vector3 apos = st + zerolen;

            return apos;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            this.panelDx.MouseWheel += PanelDx_MouseWheel;

            this.Logic.Init();
        }


        /// <summary>
        /// View情報の初期化
        /// </summary>
        public void InitInfoView()
        {
            this.Logic.InitInfoView();
        }


        /// <summary>
        /// 指定index位置を中心に描写する
        /// </summary>
        /// <param name="ix">tile index x</param>
        /// <param name="iy">tile index y</param>
        public void SetCameraAtTileIndex(int ix, int iy)
        {
            //world座標へ変換
            Vector3 pos = OrbitGlobal.TileIndexToWorld(ix, iy);

            //カメラ設定
            this.FData.Camera.SetCameraXY(pos);
        }

     


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrbitEditViewControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 画面サイズが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrbitEditViewControl_Resize(object sender, EventArgs e)
        {
            
            
        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseDown(object sender, MouseEventArgs e)
        {
            this.Minfo.DownMouse(e);

            //編集処理
            if (this.Minfo.DownButton == MouseButtons.Left)
            {
                TileEditCore.Edit(TempInfo.SelectTileRect);
            }

        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseMove(object sender, MouseEventArgs e)
        {
            this.Minfo.MoveMouse(e);

            //編集処理
            if (this.Minfo.DownButton == MouseButtons.Left)
            {
                bool f = TileEditCore.Edit(TempInfo.SelectTileRect);
                if (f == true)
                {
                    return;
                }
            }

            //カメラ移動
            int mp = Math.Abs(this.Minfo.DownLength.X) + Math.Abs(this.Minfo.DownLength.Y);
            if (this.Minfo.DownButton == MouseButtons.Right && mp > 5)
            {
                //動いた距離を取得
                Point pos = this.Minfo.PrevMoveLength;

                Vector3 vec = new Vector3(pos.X, pos.Y, 0.0f);
                this.FData.Camera.SlideCamera(vec);

                return;
            }
            
        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseUp(object sender, MouseEventArgs e)
        {
            this.Minfo.UpMouse(e);
        }

        /// <summary>
        /// マウスホイール
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void PanelDx_MouseWheel(object sender, MouseEventArgs e)
        {
            this.Minfo.WheelMouse(e);

            if (e.Delta < 0)
            {
                //this.FData.Camera.CameraPos += new Vector3(0.0f, 0.0f, -50.0f);
                this.Logic.ChangeZoomCameraPos(false);
            }
            else
            {
                //this.FData.Camera.CameraPos += new Vector3(0.0f, 0.0f, 50.0f);
                this.Logic.ChangeZoomCameraPos(true);
            }
        }
    }
}
