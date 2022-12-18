using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityEmotion
{
    internal class MainFormData
    {
    }

    /// <summary>
    /// MainForm処理
    /// </summary>
    internal class MainFormLogic
    {
        public MainFormLogic(MainForm f)
        {
            this.Form = f;
        }

        #region メンバ変数
        /// <summary>
        /// 管理画面
        /// </summary>
        private MainForm Form;

        /// <summary>
        /// データ
        /// </summary>
        private MainFormData FData
        {
            get
            {
                return this.Form.FData;
            }
        }
        #endregion



        /// <summary>
        /// プロジェクト作成
        /// </summary>
        /// <param name="bdata"></param>
        public void CreateNewProject(EmotionProjectDataBasic bdata)
        {
            //プロジェクトデータの作成
            CeGlobal.Project.InitNewProject(bdata);

            //コントロールの初期化
            

            //プロジェクトの作成
            CeGlobal.Event.SendValueChangeEvent(EEventID.CreateProject, CeGlobal.Project);
        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    }
}
