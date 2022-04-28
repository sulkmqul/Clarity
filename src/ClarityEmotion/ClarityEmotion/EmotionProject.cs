using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity;
using System.Xml.Serialization;
using System.IO;

namespace ClarityEmotion
{

    /// <summary>
    /// プロジェクト基本情報
    /// </summary>
    [Serializable]
    public class EmotionProjectDataBasic
    {
        //最大編集フレーム
        public int MaxFrame = 600;

        public int ImageWidth = 1;
        public int ImageHeight = 1;
    }

    /// <summary>
    /// アニメ情報
    /// </summary>    
    public class EmotionProjectDataAnime
    {
        /// <summary>
        /// アニメ定義データ
        /// </summary>
        public Dictionary<int, AnimeDefinitionData> AnimeDefinitionDic = new Dictionary<int, AnimeDefinitionData>();

        /// <summary>
        /// 当該アニメ[Layer番号]
        /// </summary>
        public List<AnimeElement> LayerList = new List<AnimeElement>();


        /// <summary>
        /// アニメ定義dicの作成
        /// </summary>
        /// <param name="alist"></param>
        public void CreateAnimeDefinitionDic(List<AnimeDefinitionData> alist)
        {
            this.AnimeDefinitionDic = new Dictionary<int, AnimeDefinitionData>();
            int id = 0;
            alist.ForEach(x =>
            {
                x.Id = id;                
                this.AnimeDefinitionDic.Add(id, x);
                id++;
            });

        }


        /// <summary>
        /// Layerの更新
        /// </summary>
        /// <param name="ealist"></param>
        public void CreateLayerWithSrc(List<EmotionAnimeData> ealist)
        {
            this.LayerList = new List<AnimeElement>();
            ealist.ForEach(x =>
            {
                AnimeElement a = new AnimeElement(x.LayerNo);
                a.EaData = x;
                this.LayerList.Add(a);
            });

        }

    }


    /// <summary>
    /// 情報(保存しない、一時情報も含まれる)
    /// </summary>    
    public class EmotionProjectDataInfo
    {
        
        /// <summary>
        /// 選択レイヤー番号 マイナス値で選択なし
        /// </summary>
        public int SelectLayerNo = -1;

        /// <summary>
        /// 現在の再生位置
        /// </summary>
        public int FramePosition = 0;

        /// <summary>
        /// 再生中フラグ
        /// </summary>
        public bool PlayFlag = false;
                
    }


    /// <summary>
    /// オプション設定値情報
    /// </summary>
    public class EmotionProjectDataOption
    {
        public System.Drawing.Color EditViewClearColor = System.Drawing.Color.Black;

    }


    /// <summary>
    /// プロジェクト情報
    /// </summary>
    
    public class EmotionProjectData
    {
        /// <summary>
        /// 基本情報
        /// </summary>
        public EmotionProjectDataBasic BasicInfo = new EmotionProjectDataBasic();

        /// <summary>
        /// アニメーション定義
        /// </summary>
        public EmotionProjectDataAnime Anime = new EmotionProjectDataAnime();

        /// <summary>
        /// 表示情報(一時情報なので保存するな)
        /// </summary>        
        public EmotionProjectDataInfo Info = new EmotionProjectDataInfo();

        /// <summary>
        /// オプション設定値
        /// </summary>
        public EmotionProjectDataOption Option = new EmotionProjectDataOption();
    }



    /// <summary>
    /// 管理データ本体
    /// </summary>    
    public class EmotionProject : BaseClaritySingleton<EmotionProject>
    {
        public EmotionProject()
        {

        }

        /// <summary>
        /// 生データ
        /// </summary>
        private EmotionProjectData PData = new EmotionProjectData();


        /// <summary>
        /// 基本情報
        /// </summary>
        public EmotionProjectDataBasic BasicInfo
        {
            get
            {
                return this.PData.BasicInfo;
            }
            protected set
            {
                this.PData.BasicInfo = value;
            }
        }

        /// <summary>
        /// アニメーション定義
        /// </summary>
        public EmotionProjectDataAnime Anime {
            get
            {
                return this.PData.Anime;
            }
            protected set
            {
                this.PData.Anime = value;
            }
        }

        /// <summary>
        /// 表示情報
        /// </summary>        
        public EmotionProjectDataInfo Info
        {
            get
            {
                return this.PData.Info;
            }
            protected set
            {
                this.PData.Info = value;
            }
        }

        /// <summary>
        /// オプション設定値
        /// </summary>
        public EmotionProjectDataOption Option
        {
            get
            {
                return this.PData.Option;
            }
            protected set
            {
                this.PData.Option = value;
            }
        }

        /// <summary>
        /// 選択レイヤ情報の取得
        /// </summary>
        public AnimeElement SelectLayerData
        {
            get
            {
                if (this.Info.SelectLayerNo < 0)
                {
                    return null;
                }

                return this.Anime.LayerList[this.Info.SelectLayerNo];

            }
        }

        /// <summary>
        /// 表示フレーム位置
        /// </summary>
        public int FramePosition
        {
            get
            {
                return this.Info.FramePosition;
            }
            set
            {
                this.Info.FramePosition = value;
            }
        }

                
        /// <summary>
        /// 再生スレッド
        /// </summary>
        private System.Threading.Thread PlayThread = null;
        /// <summary>
        /// 再生スレッド生存フラグ
        /// </summary>
        private bool PlayThreadFlag = false;

        #region 作成解放
        /// <summary>
        /// 新規作成
        /// </summary>
        public static void Init()
        {
            if (Instance == null)
            {
                Instance = new EmotionProject();
            }

            //サイクル開始
            if (Instance.PlayThread == null)
            {
                Instance.StartPlayThread();
            }

            Instance.CreteDefault();
        }

        /// <summary>
        /// 削除処理
        /// </summary>
        public static void Release()
        {
            foreach (var data in Instance.Anime.AnimeDefinitionDic.Values)
            {
                data.Dispose();
            }
            Instance.Anime.AnimeDefinitionDic.Clear();

            //再生スレッド削除
            Instance.PlayThreadFlag = false;
            Instance.PlayThread.Join();
        }
        #endregion

        /// <summary>
        /// アニメ定義dicの作成
        /// </summary>
        /// <param name="alist"></param>
        public void CreateAnimeDefinitionDic(List<AnimeDefinitionData> alist)
        {
            this.Anime.CreateAnimeDefinitionDic(alist);
        }

        /// <summary>
        /// プロジェクト作成時の初期化
        /// </summary>
        /// <param name="bdata"></param>
        public void InitNewProject(EmotionProjectDataBasic bdata)
        {
            //プロジェクト設定
            this.BasicInfo = bdata;

            //他のデータを初期化する
            this.CreteDefault();
        }


        /// <summary>
        /// プロジェクト保存
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveProject(string filepath)
        {
            EmotionProjectFile fp = new EmotionProjectFile();
            fp.WriteProject(filepath, this.PData);
        }

        /// <summary>
        /// プロジェクト読み込み
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadProject(string filepath)
        {
            EmotionProjectFile fp = new EmotionProjectFile();
            var rdata = fp.ReadProject(filepath);

            //必要な場所を取得
            this.PData.BasicInfo = rdata.BasicInfo;
            this.PData.Anime.AnimeDefinitionDic = rdata.Anime.AnimeDefinitionDic;
            this.PData.Anime.LayerList = rdata.Anime.LayerList;

            //画像再読み込み
            foreach (var a in this.PData.Anime.AnimeDefinitionDic.Values)
            {
                a.ImageDataList.ForEach(x => x.ReloadImage());
            }
        }

        /// <summary>
        /// デフォルトレイヤーの作成
        /// </summary>
        public void CreateDefaultLayer()
        {
            //レイヤー情報の初期化
            this.Anime.LayerList = new List<AnimeElement>();
            for (int i = 0; i < 50; i++)
            {
                AnimeElement data = new AnimeElement(i);
                data.EaData.Enabled = true;
                data.StartFrame = 10;
                data.EndFrame = 100;

                this.Anime.LayerList.Add(data);
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// デフォルトデータの作成
        /// </summary>
        private void CreteDefault()
        {
            //レイヤー作成
            this.CreateDefaultLayer();
        }


        /// <summary>
        /// 再生スレッドの作成
        /// </summary>
        private void StartPlayThread()
        {
            this.PlayThreadFlag = true;
            this.PlayThread = new System.Threading.Thread(() =>
            {
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                //60fpsの場合
                double basespan = (1000.0 / 60.0);
                double prevms = 0;

                while (this.PlayThreadFlag)
                {
                    System.Threading.Thread.Sleep(1);

                    //時間が経過したか？
                    double nowms = sw.ElapsedMilliseconds;
                    double sp = nowms - prevms;
                    if (sp < basespan)
                    {
                        continue;
                    }

                    prevms += basespan;

                    if (this.Info.PlayFlag == true)
                    {
                        //時間経過ならフレームを進める
                        this.FramePosition += 1;
                        this.FramePosition = this.FramePosition % this.BasicInfo.MaxFrame;
                    }
                    
                    //System.Diagnostics.Trace.WriteLine($"const={basespan} span={sp}");
                }

            });

            this.PlayThread.Start();
        }
    
    }
}
