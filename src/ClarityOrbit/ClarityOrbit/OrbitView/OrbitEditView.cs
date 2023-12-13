using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.GUI;

namespace ClarityOrbit.OrbitView
{
    /// <summary>
    /// エディットView(ClarityViewer)
    /// </summary>
    public partial class OrbitEditView : UserControl
    {
        public OrbitEditView()
        {
            InitializeComponent();
        }

        Clarity.Util.ClarityCycling DrawingCycle = new Clarity.Util.ClarityCycling();


        /// <summary>
        /// 初期化されるとき
        /// </summary>
        public void Init()
        {
            //初期化
            Clarity.Engine.ClarityEngine.Init(this.panelDx);

            //バックで規定処理を実行する
            this.DrawingCycle.StartCycling((f) => {
                Clarity.Engine.ClarityEngine.Native.ProcessRendering();
                //System.Diagnostics.Debug.WriteLine($"{f}");
            }, 1000 / 60);
            
            //Clarity.Engine.ClarityEngine.Native.ProcessRendering();
            //Clarity.Engine.ClarityEngine.Native.Rendering();


            
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrbitEditView_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseDown(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseMove(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelDx_MouseUp(object sender, MouseEventArgs e)
        {

        }


        /// <summary>
        /// 大きさが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrbitEditView_Resize(object sender, EventArgs e)
        {

        }
    }
}
