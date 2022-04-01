using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion
{
    class MainFormData
    {
    }


    class MainFormLogic
    {
        public MainFormLogic(MainForm f, MainFormData fd)
        {
            this.Form = f;
            this.FData = fd;
        }

        private MainFormData FData = null;
        private MainForm Form = null;


        public void Init()
        {
            //プロジェクト初期化
            EmotionProject.Init();

            //各コントロールの初期化
            this.Form.layerAnimeControl1.InitControl();
            this.Form.animeEditViewControl1.Init();
            this.Form.layerSettingControl1.Init();this.Form.animeEditViewControl1.Init();
        }

        public void Release()
        {
            EmotionProject.Release();
        }
        
        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="np"></param>
        public void CreateNewProject(EmotionProjectDataBasic np)
        {
            //初期データの設定
            EmotionProject.Mana.InitNewProject(np);

            //設定
            this.Form.animeEditViewControl1.ResetView();

        }

        /// <summary>
        /// アニメ定義編集画面の起動
        /// </summary>
        public void SetupAnimeDefinitionEditForm()
        {
            //アニメ定義情報の取得
            List<AnimeDefinitionData> alist = EmotionProject.Mana.Anime.AnimeDefinitionDic.Values.ToList();

            //画面の起動
            AnimeDefinition.AnimeDefinitionEditForm f = new AnimeDefinition.AnimeDefinitionEditForm();
            f.Init(alist);

            DialogResult dret = f.ShowDialog(this.Form);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //入力を取得し、データを作成
            List<AnimeDefinitionData> elist = f.GetInputData();
            EmotionProject.Mana.CreateAnimeDefinitionDic(elist);


            //定義初期化
            this.Form.layerSettingControl1.InitDefinition();
        }


        /// <summary>
        /// プロジェクトの読み込み
        /// </summary>
        /// <param name="pfpath"></param>
        public void LoadProject(string pfpath)
        {
            //プロジェクトの読み込み
            EmotionProject.Mana.LoadProject(pfpath);

            //コントロールの初期化
            this.Form.layerSettingControl1.InitDefinition();
            this.Form.animeEditViewControl1.ResetView();

        }

        /// <summary>
        /// 設定変更時の初期化更新
        /// </summary>
        public void ChangeAnimeSetting()
        {
            this.Form.animeEditViewControl1.ResetView();
            this.Form.layerAnimeControl1.ReInitControl();
        }


        /// <summary>
        /// レイヤー情報を初期化する
        /// </summary>
        public void InitLayer()
        {
            EmotionProject.Mana.CreateDefaultLayer();

        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    }
}
