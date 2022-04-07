using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace Clarity.Engine.Texture
{
    /// <summary>
    /// アニメーション種別
    /// </summary>
    public enum ETextureAnimationKind
    {
        /// <summary>
        /// ループ処理する
        /// </summary>
        Loop = 0,
        /// <summary>
        /// 一回だけで終了
        /// </summary>
        Once,
    }

    /// <summary>
    /// テクスチャアニメーション、1フレームの情報を表す。
    /// </summary>
    public class TextureAnimeFrameInfo
    {
        /// <summary>
        /// このフレームの使用テクスチャ番号
        /// </summary>
        public int TextureID = 0;

        /// <summary>
        /// このアニメーションの切り出しOffset
        /// </summary>
        public Vector2 TextureOffset = new Vector2(0.0f, 0.0f);

        /// <summary>
        /// このテクスチャの表示時間(ms)
        /// </summary>
        public long FrameTime = 100;

    }

    /// <summary>
    /// テクスチャアニメーションデータ
    /// </summary>
    /// <remarks>このクラス一つが一個のアニメーションを表す</remarks>
    public class TextureAnimeData : IDisposable
    {
        /// <summary>
        /// これの一意文字列
        /// </summary>
        public string Code = "";

        /// <summary>
        /// これの種別
        /// </summary>
        public ETextureAnimationKind Kind = ETextureAnimationKind.Loop;


        /// <summary>
        /// アニメのフレームの一覧
        /// </summary>
        public List<TextureAnimeFrameInfo> FrameList = new List<TextureAnimeFrameInfo>();

        //-------------------------------------------------------------------------
        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            
        }
    }


    /// <summary>
    /// オブジェクト対象のテクスチャアニメを管理するクラス
    /// </summary>
    public class TextureAnimeController
    {
        public TextureAnimeController()
        {
        }

        

        /// <summary>
        /// 現在のアニメID
        /// </summary>
        public int CurrentAnimeID { get; private set; } = ClarityEngine.INVALID_ID;


        /// <summary>
        /// 現在のアニメのフレームindex
        /// </summary>
        public int CurrentAnimeFrameIndex { get; private set; } = -1;

        /// <summary>
        /// 現在アニメの開始時間(厳密にはFrame時間ではないことに注意) この時間がマイナスの場合、初期化される
        /// </summary>
        private long CurrentAnimeStartTime = -1;

        /// <summary>
        /// 現在の表示フレーム情報
        /// </summary>
        internal TextureAnimeFrameInfo CurrentFrameInfo = null;


        /// <summary>
        /// アニメ終了時の処理
        /// </summary>
        public event EndTextureAnimeDelegate EndAnimeEvent = null;


        /**************************************************************************************/
        /**************************************************************************************/

        /// <summary>
        /// アニメ処理本体   
        /// </summary>
        /// <param name="frametime">実行時間</param>
        public void Anime(long frametime)
        {

            //開始時間がマイナスなら初期化
            if (this.CurrentAnimeStartTime < 0)
            {
                this.CurrentAnimeStartTime = frametime;
                this.CurrentAnimeFrameIndex = 0;
            }

            int caid = this.CurrentAnimeID;

            //対象を取得
            TextureAnimeData adata = TextureAnimeFactory.GetAnime(caid);

            //今回のフレームを取得
            TextureAnimeFrameInfo finfo = adata.FrameList[this.CurrentAnimeFrameIndex];
            this.CurrentFrameInfo = finfo;

            //表示時間計算
            long ftime = frametime - this.CurrentAnimeStartTime;
            //時間経過していないならそのまま表示
            if (ftime <= finfo.FrameTime)
            {
                return;
            }

            //ここまで来ていたら次のアニメ・・・過剰に表示時間が過ぎていたならindexを1つ以上進める処理が必要かもしれない。
            this.CurrentAnimeFrameIndex += 1;
            
            //表示時間が経過したと判断
            //this.CurrentAnimeStartTime += finfo.FrameTime;
            this.CurrentAnimeStartTime = frametime;

            //最後までいってないなら問題なし
            if (this.CurrentAnimeFrameIndex < adata.FrameList.Count)
            {
                return;
            }

            //ここまで来たらフレームが最後まで行った
            switch (adata.Kind)
            {
                case ETextureAnimationKind.Loop:
                    {
                        //ループ処理
                        this.CurrentAnimeFrameIndex %= adata.FrameList.Count;
                        //this.EndAnimeEvent?.Invoke(caid);
                    }
                    break;
                case ETextureAnimationKind.Once:
                    {
                        //一回だけなら固定
                        this.CurrentAnimeFrameIndex = adata.FrameList.Count - 1;
                        //二度と遷移が起こらないよう極限時間とする
                        this.CurrentAnimeStartTime = long.MaxValue;

                        //Anime終了処理実行
                        this.EndAnimeEvent?.Invoke(caid);

                    }
                    break;
                default:
                    break;
            }

            return;
        }


        /// <summary>
        /// アニメの変更
        /// </summary>
        /// <param name="aid"></param>
        public void ChangeAnime(int aid)
        {
            this.CurrentAnimeID = aid;
            this.CurrentAnimeStartTime = -1;

        }





    }
}
