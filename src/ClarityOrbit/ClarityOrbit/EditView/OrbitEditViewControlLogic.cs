using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;

namespace ClarityOrbit.EditView
{
    /// <summary>
    /// Dx関係のデータなど
    /// </summary>
    internal class OrbitEditViewData
    {
        
    }

    internal class OrbitEditViewControlLogic : ClarityEnginePlugin
    
    {
        public OrbitEditViewControlLogic(OrbitEditViewControl con, OrbitEditViewData fdata)
        {
            this.Con = con;
            this.FData = fdata;
        }

        private OrbitEditViewControl Con { get; init; } 
        /// <summary>
        /// 初期データ}
        /// </summary>
        private OrbitEditViewData FData { get; init; }

        private Task EngineTask = null;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //ClarityEngineの初期化
            ClarityEngine.Init(this.Con.panelDx, OrbitGlobal.ClarityEngineSettingFilePath);
            this.EngineTask = ClarityEngine.RunAsync(this);

            //構造の初期化
            this.InitStructure();
            
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 構造設定
        /// </summary>
        private void InitStructure()
        {
            ClarityEngine.CreateStructure(OrbitGlobal.ClarityEngineSettingFilePath);            
        }
    }
}
