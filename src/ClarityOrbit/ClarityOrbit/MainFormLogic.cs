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
        }

        /// <summary>
        /// 親画面
        /// </summary>
        MainForm Form { get; init; }

        /// <summary>
        /// 各画面の管理クラス
        /// </summary>
        public WindowManager WindowManager = new WindowManager();

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


        /// <summary>
        /// 新規作成処理
        /// </summary>
        /// <param name="binfo">作成情報</param>
        public void CreateNewProject(OrbitProjectBase binfo)
        {
            //解放処理

            //作成処理
            this.CreateNew(binfo);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 新規作成処理本体
        /// </summary>
        /// <returns></returns>
        private void CreateNew(OrbitProjectBase binfo)
        {
            //プロジェクト情報初期化
            OrbitProject proj = new OrbitProject();
            proj.Init(binfo);
            OrbitGlobal.Mana.Project = proj;

            //engine構造の作成
        }
    }
}
