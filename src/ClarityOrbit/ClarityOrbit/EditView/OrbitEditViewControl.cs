using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        public Clarity.GUI.MouseInfo Minfo = new Clarity.GUI.MouseInfo();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            this.panelDx.MouseWheel += PanelDx_MouseWheel;

            this.Logic.Init();
        }

        

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
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseMove(object sender, MouseEventArgs e)
        {
            this.Minfo.MoveMouse(e);
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
        }
    }
}
