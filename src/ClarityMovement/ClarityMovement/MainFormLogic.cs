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
            //既存の物体の解放
            CmGlobal.Project.Value?.Dispose();
            CmGlobal.Project.Value = null;

            //プロジェクトの作成
            CmGlobal.Project.Value = proj;
        }


        /// <summary>
        /// プロジェクトファイルの保存
        /// </summary>
        /// <param name="proj">保存対象</param>
        /// <param name="filepath">保存ファイルパス</param>
        public Task SaveProject(CmProject proj, string filepath)
        {
            //編集の取得
            this.Form.frameEditControlEditor.ApplyTagModifier();

            CmProjectFile fp = new CmProjectFile();
            return fp.SaveProject(proj, filepath);
        }

        /// <summary>
        /// プロジェクトの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadProject(string filepath)
        {
            CmProjectFile fp = new CmProjectFile();
            var proj = fp.LoadProject(filepath);

            //初期化と再読み込み
            this.CreateProject(proj);

        }


        /// <summary>
        /// モーションファイルの出力
        /// </summary>
        /// <param name="filepath">書き出しファイルパス</param>
        /// <param name="proj">書き出しデータ</param>
        public void ExportMotionFile(string filepath, CmProject proj)
        {
            //これはその打ちえkす

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
