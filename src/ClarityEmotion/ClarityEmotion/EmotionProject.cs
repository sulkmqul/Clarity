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

        /// <summary>
        /// 画像サイズ
        /// </summary>
        public int ImageWidth = 1;
        /// <summary>
        /// 画像サイズ
        /// </summary>
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
    /// 情報(保存しない、一時情報が含まれる)
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


        /// <summary>
        /// FPS
        /// </summary>
        public int FPS = 60;

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
    public class EmotionProject
    {
        public EmotionProject()
        {

        }


        class LayerSeq
        {
            private static int LayerNo = 0;
            public int Next()
            {
                return LayerSeq.LayerNo++;
            }

        }


        /// <summary>
        /// 生データ
        /// </summary>
        private EmotionProjectData PData = new EmotionProjectData();

        /// <summary>
        /// レイヤー番号
        /// </summary>
        private LayerSeq Seq = new LayerSeq();

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
        public AnimeElement? SelectLayerData
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
        /// 新しいレイヤーの追加
        /// </summary>
        /// <returns>追加したレイヤー</returns>
        public AnimeElement AddNewLayer()
        {
            AnimeElement ans = new AnimeElement(this.Seq.Next());
            this.Anime.LayerList.Add(ans);
            return ans;
        }

        /// <summary>
        /// 指定レイヤーの削除
        /// </summary>
        /// <param name="layno"></param>
        public void RemoveSelectLayer(int layno)
        {
            var da = this.Anime.LayerList.Where(x => x.LayerNo == layno).First();
            this.Anime.LayerList.Remove(da);

        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    
    
    }
}
