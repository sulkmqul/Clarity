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
        private OrbitEditViewControlLogic Logic = null;

        /// <summary>
        /// データ
        /// </summary>
        private OrbitEditViewData FData = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
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
    }
}
