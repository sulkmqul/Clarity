using Clarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClarityEmotion
{
    /// <summary>
    /// Flipタイプ
    /// </summary>
    public enum EFlipType
    {
        None = 0,
        XFlip = 1 << 0,
        YFlip = 1 << 1,
        XYFlip = XFlip | YFlip,
    }

    /// <summary>
    /// 情報まとめ
    /// </summary>
    [Serializable]
    public class EmotionAnimeData
    {
        /// <summary>
        /// これの名前
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 有効可否
        /// </summary>
        public bool Enabled = true;

        /// <summary>
        /// 定義アニメID
        /// </summary>
        public int AnimeID = -1;

        /// <summary>
        /// レイヤー番号
        /// </summary>
        public int LayerNo = -1;

        /// <summary>
        /// これの透明値
        /// </summary>
        public float Alpha = 1.0f;

        /// <summary>
        /// ループ可否 true=ループ再生
        /// </summary>
        public bool LoopFlag = false;

        /// <summary>
        /// 開始フレーム
        /// </summary>
        public int StartFrame = 10;

        /// <summary>
        /// 表示フレーム間隔
        /// </summary>
        public int FrameSpan = 50;

        /// <summary>
        /// 終了フレーム
        /// </summary>
        public int EndFrame
        {
            get
            {
                return this.StartFrame + this.FrameSpan;
            }
        }

        /// <summary>
        /// 再生速度レート
        /// </summary>
        public double SpeedRate = 1.0;

        /// <summary>
        /// フレーム開始オフセット
        /// </summary>
        public int FrameOffset = 0;

        /// <summary>
        /// 位置情報
        /// </summary>
        public Point Pos2D = new Point(0, 0);

        /// <summary>
        /// 表示サイズ
        /// </summary>
        public Size DispSize = new Size(0, 0);

        /// <summary>
        /// フリップ可否
        /// </summary>
        public EFlipType FlipType = EFlipType.None;

    }


    /// <summary>
    /// 表示用一次データ
    /// </summary>
    public class EditTemplateData
    {
        /// <summary>
        /// マウスオーバー可否
        /// </summary>
        public bool MouseOverFlag = false;

        /// <summary>
        /// これの現在の表示上のエリア
        /// </summary>
        public Rectangle DispAreaRect = new Rectangle();
    }

    /// <summary>
    /// レイヤーアニメ管理データ
    /// </summary>
    public class AnimeElement
    {
        public AnimeElement(int layerno)
        {
            this.EaData.LayerNo = layerno;
            this.EaData.Name = $"Layer {layerno}";
        }

        /// <summary>
        /// レイヤー管理データ
        /// </summary>
        public EmotionAnimeData EaData = new EmotionAnimeData();

        /// <summary>
        /// 編集表示用一時データ
        /// </summary>
        public EditTemplateData TempData = new EditTemplateData();

        #region 便利
        /// <summary>
        /// 開始フレーム
        /// </summary>
        public int StartFrame
        {
            get
            {
                return this.EaData.StartFrame;
            }
            set
            {
                this.EaData.StartFrame = value;
            }
        
        }

        /// <summary>
        /// 表示フレーム数
        /// </summary>
        public int FrameSpan
        {
            get
            {
                return this.EaData.FrameSpan;
            }
            set
            {
                this.EaData.FrameSpan = value;
            }
        }


        /// <summary>
        /// 終了フレーム
        /// </summary>
        public int EndFrame
        {
            get
            {
                return this.EaData.EndFrame;
            }
        }

        /// <summary>
        /// レイヤー番号の取得
        /// </summary>
        public int LayerNo
        {
            get
            {
                return this.EaData.LayerNo;
            }
        }

        /// <summary>
        /// 選択アニメデータ取得
        /// </summary>
        public AnimeDefinitionData? SelectAnime
        {
            get
            {
                if (this.EaData.AnimeID < 0)
                {
                    return null;
                }

                return CeGlobal.Project.Anime.AnimeDefinitionDic[this.EaData.AnimeID];
            }
        }
        #endregion


        /// <summary>
        /// 対象の画像取得
        /// </summary>
        /// <param name="frame">絶対フレーム時間</param>
        /// <returns></returns>
        public Bitmap? GetFrameImage(int frame)
        {
            var adata = this.EaData;

            //アニメ選択なし
            if (this.SelectAnime == null)
            {
                return null;
            }

            int stf = frame - adata.StartFrame;
            //自身の範囲外
            if (stf < 0)
            {
                return null;
            }
            if (stf >= adata.FrameSpan)
            {
                return null;
            }

            double framecount = this.SelectAnime.ImageDataList.Count();
            double playframe = adata.SpeedRate* framecount;

            //ループせずにカウント以上なら最終を返却
            if (adata.LoopFlag == false && stf >= playframe)
            {
                return this.SelectAnime.ImageDataList.Last().BitImage;
            }

            //今のフレームを計算
            double mm = stf % playframe;            
            double ff = (framecount / playframe) * mm;

            int frameindex = (int)Math.Floor(ff);

            return this.SelectAnime.ImageDataList.ElementAt(frameindex).BitImage;            
        }
    }
}
