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

            Vector3 posn = Clarity.Engine.ClarityEngine.WindowToWorld(OrbitGlobal.OrbitWorldID, this.Minfo.DownPos.X, this.Minfo.DownPos.Y, 0.0f);
            Vector3 posf = Clarity.Engine.ClarityEngine.WindowToWorld(OrbitGlobal.OrbitWorldID, this.Minfo.DownPos.X, this.Minfo.DownPos.Y, 1.0f);
            Clarity.Engine.ClarityEngine.SetSystemText($"DownPosN({posn.X},{posn.Y},{posn.Z})");
            Clarity.Engine.ClarityEngine.SetSystemText($"DownPosF({posf.X},{posf.Y},{posf.Z})", 1);


        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseMove(object sender, MouseEventArgs e)
        {
            this.Minfo.MoveMouse(e);

            //カメラ移動
            int mp = Math.Abs(this.Minfo.DownLength.X) + Math.Abs(this.Minfo.DownLength.Y);
            if (this.Minfo.DownButton == MouseButtons.Left && mp > 5)
            {
                //動いた距離を取得
                Point pos = this.Minfo.PrevMoveLength;

                Vector3 vec = new Vector3(-pos.X, -pos.Y, 0.0f);
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
                this.FData.Camera.CameraPos += new Vector3(0.0f, 0.0f, -10.0f);
            }
            else
            {
                this.FData.Camera.CameraPos += new Vector3(0.0f, 0.0f, 10.0f);
            }
        }
    }
}
