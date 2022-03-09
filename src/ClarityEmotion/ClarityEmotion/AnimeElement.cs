using Clarity;
using Clarity.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClarityEmotion
{
    /// <summary>
    /// 情報まとめ
    /// </summary>
    public class EmotionAnimeData
    {
        /// <summary>
        /// 有効可否
        /// </summary>
        public bool Enabled = false;

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
        public int StartFrame = 0;

        /// <summary>
        /// 終了フレーム
        /// </summary>
        public int EndFrame = 0;

        /// <summary>
        /// 再生速度レート
        /// </summary>
        public double SpeedRate = 1.0;

        /// <summary>
        /// 位置情報
        /// </summary>
        public Point Pos2D = new Point(0, 0);

        /// <summary>
        /// 拡縮率
        /// </summary>
        public double ScaleRate = 0.0;
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
        }

        #region メンバ変数
        /// <summary>
        /// レイヤー管理データ
        /// </summary>
        public EmotionAnimeData EaData = new EmotionAnimeData();

        /// <summary>
        /// 編集表示用一時データ
        /// </summary>
        public EditTemplateData TempData = new EditTemplateData();

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
        /// 終了フレーム
        /// </summary>
        public int EndFrame
        {
            get
            {
                return this.EaData.EndFrame;
            }
            set
            {
                this.EaData.EndFrame = value;
            }
        }

        /// <summary>
        /// これのフレーム数
        /// </summary>
        public int FrameCount
        {
            get
            {
                return this.EaData.EndFrame - this.EaData.StartFrame;
            }
        }

        /// <summary>
        /// 選択アニメデータ取得
        /// </summary>
        public AnimeDefinitionData SelectAnime
        {
            get
            {
                if (this.EaData.AnimeID < 0)
                {
                    return null;
                }

                return EmotionProject.Mana.Anime.AnimeDefinitionDic[this.EaData.AnimeID];
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
        #endregion



        /// <summary>
        /// レイヤー名の作成
        /// </summary>
        /// <returns></returns>
        public string CreateLayerName()
        {
            //return this.
            return $"Layer {this.EaData.LayerNo + 1}";
        }


        /// <summary>
        /// 現在のフレームの適切なアニメを取得する 無効=null
        /// </summary>
        /// <param name="frame">対象フレーム(絶対時間)</param>
        /// <returns></returns>
        public Bitmap GetFrameImage(int frame)
        {
            int sf = frame - this.StartFrame;


            //フレーム範囲外
            if (sf < 0)
            {
                return null;
            }
            if (this.EndFrame <= frame)
            {
                return null;
            }
            //選択アニメ無し
            if (this.SelectAnime == null)
            {
                return null;
            }            

            //フレーム数を取得            
            double framecount = this.SelectAnime.ImageDataList.Count();

            //一回のアニメの速度
            double playframe = this.EaData.SpeedRate * framecount;
            
            //ループせずにアニメカウント以上なら最終アニメを返却で確定
            if (sf >= playframe && this.EaData.LoopFlag == false)
            {
                return this.SelectAnime.ImageDataList.Last().BitImage;
            }


            //sfのフレームを算出する。ただしループの可能性があるため、ループ全体の割り算を行う
            double mm = sf % playframe;


            //変換をする
            double ff = (framecount / playframe) * mm;

            int frameindex = (int)Math.Floor(ff);

            Bitmap ans =  this.SelectAnime.ImageDataList.ElementAt(frameindex).BitImage;
            return ans;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CheckDisplayPoint(Point pos)
        {
            return this.TempData.DispAreaRect.Contains(pos);
        }

    }
}
