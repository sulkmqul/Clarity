using ClarityMovement.Export;
using ClarityMovement.MotionFile;
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
        /// <param name="proj">入力情報</param>
        public void CreateProject(CmProject proj)
        {
            //プロジェクトの作成
            CmGlobal.Project.Value = proj;

        }


        /// <summary>
        /// モーションファイルの出力
        /// </summary>
        /// <param name="filepath">書き出しファイルパス</param>
        /// <param name="proj">書き出しデータ</param>
        public void ExportMotionFile(string filepath, CmProject proj)
        {
            //編集の反映
            this.Form.frameEditControlEditor.ApplyTagModifier();

            //書き出しデータ作成
            MotionConverter cov = new MotionConverter();
            var data = cov.ConvertFileData(proj);

            //保存
            ClarityMotionFile mf = new ClarityMotionFile();
            mf.Write(filepath, data);

        }



        
    }
}
