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
            this.Grid = new LayerGrid(this.dataGridViewLayerGird, this.imageList1);
        }


        /// <summary>
        /// Gird管理
        /// </summary>
        private LayerGrid Grid;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            this.RefleshDisplay();
        }

        /// <summary>
        /// 削除
        /// </summary>
        public void Release()
        {
        }


        /// <summary>
        /// データ再描画
        /// </summary>
        public void RefleshDisplay()
        {
            if (OrbitGlobal.Project == null)
            {
                return;
            }
            this.Grid.RefleshGrid(OrbitGlobal.Project.Layer.LayerList);
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
