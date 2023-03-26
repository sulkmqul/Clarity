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

namespace ClarityMovement.Viewer
{

    /// <summary>
    /// DirectX描画をGUIで行うためのラッパ
    /// </summary>
    public partial class ClarityEngineViewer : UserControl
    {
        public ClarityEngineViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            if (ClarityEngine.IsEngineInit == false)
            {
                ClarityEngine.Init(this);
            }

            //初期表示
            this.RenderView();
        }


        /// <summary>
        /// 全体の再描画
        /// </summary>
        public void RenderView()
        {
            ClarityEngine.Native.ProcessRendering();
        }

        /// <summary>
        /// 全体クリア
        /// </summary>
        public void Clear()
        {
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    }
}
