using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit
{
    internal class WindowManager
    {
        public TipSelectView.TipSelectViewDockingContent TipSelectView = new TipSelectView.TipSelectViewDockingContent();

        public LayerView.LayerViewDockingContent LayerView = new LayerView.LayerViewDockingContent();
    }

    internal class MainFormLogic
    {
        public MainFormLogic(MainForm f)
        {
            this.Form = f;
            this.WindowManager = new WindowManager();
        }

        /// <summary>
        /// 親画面
        /// </summary>
        MainForm Form { get; init; }

        /// <summary>
        /// ウィンドウの管理クラス
        /// </summary>
        public WindowManager WindowManager = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面の全体初期化
        /// </summary>
        public void InitForm()
        {
            //Dockingテーマの設定
            this.Form.dockPanelToolBox.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();

            //画面初期構成
            this.WindowManager.TipSelectView.Show(this.Form.dockPanelToolBox);
            this.WindowManager.TipSelectView.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockTop;

            this.WindowManager.LayerView.Show(this.Form.dockPanelToolBox);
            this.WindowManager.LayerView.Show(this.Form.dockPanelToolBox);


            //各Controlの初期化
            this.Form.orbitEditViewControl1.Init();
        }
    }
}
