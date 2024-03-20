using Clarity;
using Clarity.Engine;
using Clarity.Engine.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Texture
{

    /// <summary>
    /// アニメ終了Delegate
    /// </summary>
    /// <param name="aid">終了animeID</param>
    public delegate void EndTextureAnimeDelegate();

    /// <summary>
    /// アニメーション基本データ
    /// </summary>
    public class TextureAnimationInfo
    {
        /// <summary>
        /// 横幅
        /// </summary>
        public int ImageWidth { get; set; } = 0;

        /// <summary>
        /// 縦幅
        /// </summary>
        public int ImageHeight { get; set; } = 0;

        /// <summary>
        /// アニメーションループ可否
        /// </summary>
        public bool Loop = true;

        /// <summary>
        /// 描画物一式
        /// </summary>
        public List<AnimeFrameInfo> FrameList { get; init; } = new List<AnimeFrameInfo>();
    }

    /// <summary>
    /// テクスチャアニメーションフレーム情報
    /// </summary>
    public class AnimeFrameInfo
    {
        public AnimeFrameInfo( int tid, float ms)
        {
            this.TextureID = tid;
            this.FrameTime = ms;
        }
        /// <summary>
        /// テクスチャID
        /// </summary>
        public int TextureID = 0;

        /// <summary>
        /// フレームの表示時間(ms)
        /// </summary>
        public float FrameTime = 0.0f;
    }



    /// <summary>
    /// テクスチャアニメの制御
    /// </summary>
    public class TextureAnimeController
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ainfo"></param>
        public TextureAnimeController(TextureAnimationInfo ainfo)
        {
            this.AnimationInfo = ainfo;
        }

        /// <summary>
        /// 現在のアニメのフレームindex
        /// </summary>
        public int CurrentAnimeFrameIndex { get; private set; } = 0;

        /// <summary>
        /// アニメ情報
        /// </summary>
        public TextureAnimationInfo AnimationInfo { get; private set; }

        /// <summary>
        /// 現在の表示フレーム情報
        /// </summary>
        public AnimeFrameInfo CurrentFrameInfo
        {
            get
            {
                return this.AnimationInfo.FrameList[this.CurrentAnimeFrameIndex];
            }
        }


        /// <summary>
        /// アニメ終了時の処理
        /// </summary>
        public event EndTextureAnimeDelegate EndAnimeEvent = delegate () { };

        /// <summary>
        /// 現在アニメの開始時間(厳密にはFrame時間ではないことに注意) この時間がマイナスの場合、初期化される
        /// </summary>
        protected float CurrentAnimeStartTime { get; set; } = -1;
        /**************************************************************************************/
        /**************************************************************************************/        
        /// <summary>
        /// 表示フレームの設定
        /// </summary>
        /// <param name="index"></param>
        public void SetAnimeFrameIndex(int index)
        {
            if(this.AnimationInfo.FrameList.Count <= index || index < 0)
            {
                return;
            }
            this.CurrentAnimeFrameIndex = index;
        }


        /// <summary>
        /// アニメ処理本体   
        /// </summary>
        /// <param name="frametime">実行時間ms</param>
        public virtual void ProcAnimation(float frametime)
        {
            //開始時間がマイナスなら初期化をする
            if (this.CurrentAnimeStartTime < 0)
            {
                this.CurrentAnimeStartTime = frametime;
                this.CurrentAnimeFrameIndex = 0;
            }

            //1フレームしかないならアニメ処理をしない
            if (this.AnimationInfo.FrameList.Count == 1)
            {
                this.CurrentAnimeFrameIndex = 0;
                return;
            }


            //現在フレーム情報取得
            AnimeFrameInfo finfo = this.CurrentFrameInfo;

            //時間経過していないならそのまま表示
            float satime = frametime - this.CurrentAnimeStartTime;
            if (satime <= finfo.FrameTime)
            {
                return;
            }

            float temptime = satime;
            while (true)
            {
                //次のアニメへ
                this.CurrentAnimeFrameIndex += 1;

                //次のアニメの表示時間を計算
                if (this.CurrentAnimeFrameIndex >= this.AnimationInfo.FrameList.Count)
                {
                    break;
                }
                AnimeFrameInfo ft = this.AnimationInfo.FrameList[this.CurrentAnimeFrameIndex];
                temptime -= ft.FrameTime;
                if (temptime < ft.FrameTime)
                {
                    //表示時間以内になれば良い
                    break;
                }
            }

            //時間経過
            this.CurrentAnimeStartTime += satime;

            //最後までいったか？
            if (this.CurrentAnimeFrameIndex < this.AnimationInfo.FrameList.Count)
            {
                return;
            }

            if (this.AnimationInfo.Loop == true)
            {
                //ループ処理
                this.CurrentAnimeFrameIndex %= this.AnimationInfo.FrameList.Count;
                this.EndAnimeEvent?.Invoke();
            }
            else
            {
                //最後のフレームで固定し、無限時間を設定して遷移が起こらないようにする
                this.CurrentAnimeFrameIndex = this.AnimationInfo.FrameList.Count - 1;
                this.CurrentAnimeStartTime = long.MaxValue;

                //Anime終了処理実行
                this.EndAnimeEvent?.Invoke();
            }
        }
    }



    /// <summary>
    /// テクスチャアニメ処理
    /// </summary>
    public class TextureAnimeBehavior : BaseModelBehavior<ClarityObject>
    {
        public TextureAnimeBehavior(TextureAnimationInfo ainfo)
        {
            this.TexCont = new TextureAnimeController(ainfo);               
        }

        /// <summary>
        /// アニメーション管理
        /// </summary>
        private TextureAnimeController TexCont;


        /// <summary>
        /// アニメーションの処理
        /// </summary>
        /// <param name="obj"></param>
        protected override void ExecuteBehavior(ClarityObject obj)
        {
            //アニメーション処理
            this.TexCont.ProcAnimation(obj.ProcTime);
            //texture設定
            obj.TextureID = this.TexCont.CurrentFrameInfo.TextureID;
        }
    }
}
