using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Engine;

namespace ClarityOrbit
{
    internal class WindowManager
    {
        public TipSelectView.TileSelectViewDockingContent TipSelectView = new TipSelectView.TileSelectViewDockingContent();

        public LayerView.LayerViewDockingContent LayerView = new LayerView.LayerViewDockingContent();

        public EditView.MinimapViewDockingContent MinimapView = new EditView.MinimapViewDockingContent();
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
            //全体に関係する基本を初期化
            OrbitGlobal.Init();
            OrbitGlobal.Mana.MForm = this.Form;

            //Dockingテーマの設定
            this.Form.dockPanelToolBox.Theme = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();

            //画面初期構成
            {
                this.WindowManager.TipSelectView.Show(this.Form.dockPanelToolBox);
                //this.WindowManager.TipSelectView.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockTop;

                this.WindowManager.LayerView.Show(this.Form.dockPanelToolBox);
                //this.WindowManager.LayerView.Show(this.Form.dockPanelToolBox);
                this.WindowManager.LayerView.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;

                //minimap
                this.WindowManager.MinimapView.Show(this.Form.dockPanelToolBox);
                this.WindowManager.MinimapView.DockState = WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide;

            }


            //各Controlの初期化
            this.Form.orbitEditViewControl1.Init();            
                        
        }


        /// <summary>
        /// 新規作成処理
        /// </summary>
        /// <param name="binfo">作成情報</param>
        public void CreateNew(OrbitProjectBase binfo)
        {
            //解放処理

            //作成処理
            this.CreateNewProject(binfo);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// プロジェクトの新規作成処理
        /// </summary>
        /// <returns></returns>
        private void CreateNewProject(OrbitProjectBase binfo)
        {
            
            //プロジェクト情報初期化
            OrbitProject proj = new OrbitProject();
            proj.Init(binfo);
            OrbitGlobal.Project = proj;

            this.Form.orbitEditViewControl1.InitInfoView();            

            //レイヤの作成
            proj.Layer.AddNewLayer();
            proj.Layer.SelectedLayerIndex = 0;  //初期レイヤー選択
            this.WindowManager.LayerView.Init();

        }
    }
}
