using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit
{
    /// <summary>
    /// MainFormロジッククラス
    /// </summary>
    internal class MainFormLogic
    {
        public MainFormLogic(MainForm f)
        {
            this.Form = f;
        }
        /// <summary>
        /// 親画面
        /// </summary>
        private MainForm Form;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// プロジェクト作成時の編集前初期化
        /// </summary>
        /// <returns></returns>
        public bool InitializeEdit()
        {
            //画面の初期化

            //タイル元画像選択画面の初期化
            OrbitWindowManager.ShowTileSrcSelectView(this.Form.dockPanelToolWindow);

            //レイヤー画面の初期化
            OrbitWindowManager.ShowLayerView(this.Form.dockPanelToolWindow);

            //tipエディタの初期化
            this.Form.orbitEditView1.Init();


            return true;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    }
}
