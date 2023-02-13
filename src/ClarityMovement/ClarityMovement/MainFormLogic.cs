using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    /// <summary>
    /// MainFormロジック管理
    /// </summary>
    internal class MainFormLogic
    {
        public MainFormLogic(MainForm f)
        {
            this.Form = f;
        }

        MainForm Form;


        /// <summary>
        /// 全体初期化
        /// </summary>
        public void Init()
        {
            
        }


        /// <summary>
        /// プロジェクトの作成
        /// </summary>
        public void CreateProject()
        {
            //プロジェクトの作成
            CmGlobal.Project.Value = new CmProject();

        }
    }
}
