using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.Engine;

namespace ClarityOrbit.LayerView
{
    /// <summary>
    /// レイヤー表示コントロール
    /// </summary>
    public partial class LayerViewDockingContent : WeifenLuo.WinFormsUI.Docking.DockContent 
    {
        public LayerViewDockingContent()
        {
            InitializeComponent();
            this.Logic = new LayerViewDockingContentLogic(this);
        }

        LayerViewDockingContentLogic Logic = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //初期化
            this.Logic.Init();
        }

        /// <summary>
        /// 削除
        /// </summary>
        public void Release()
        {
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerViewDockingContent_Load(object sender, EventArgs e)
        {

        }
    }
}
