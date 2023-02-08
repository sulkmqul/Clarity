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

        /// <summary>
        /// データをまとめたzipの出力
        /// </summary>
        /// <param name="filepath">出力ファイルパス</param>
        /// <returns></returns>
        public async Task ExportArchive(string filepath)
        {

            EmotionWriter ew = new EmotionWriter();
            await ew.ExportArchive(filepath, (int max, int now) =>
            {
                System.Diagnostics.Trace.WriteLine($"{now}/{max}");
            }); 
        }

        /// <summary>
        /// 連番画像の出力
        /// </summary>
        /// <param name="folpath">連番画像の出力フォルダ</param>
        /// <returns></returns>
        public async Task ExportSerialImages(string folpath)
        {
            EmotionWriter ew = new EmotionWriter();
            await ew.ExportImages(folpath, System.Drawing.Imaging.ImageFormat.Png, "", (int max, int now) =>
            {
                System.Diagnostics.Trace.WriteLine($"{now}/{max}");
            });
        }



        /// <summary>
        /// プロジェクトの保存
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveProject(string filepath)
        {
            CeGlobal.Project.SaveProject(filepath);
        }

        /// <summary>
        /// プロジェクト読み込み
        /// </summary>
        /// <param name="filepath"></param>
        public void OpenProject(string filepath)
        {
            CeGlobal.Project.LoadProject(filepath);

            //必要な情報を流す
            CeGlobal.Event.SendValueChangeEvent(EEventID.AnimeDefinitionUpdate, CeGlobal.Project);
            CeGlobal.Event.SendValueChangeEvent(EEventID.OpenProject, CeGlobal.Project);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    }
}
