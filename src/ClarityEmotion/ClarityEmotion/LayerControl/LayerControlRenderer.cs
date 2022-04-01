using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ClarityEmotion.LayerControl
{
    /// <summary>
    /// マウス移動タイプ
    /// </summary>
    enum EMouseMoveType
    {
        Start,
        End,
        Move,
        None,

    }

    

    /// <summary>
    /// 描画情報まとめ
    /// </summary>
    class LayerControlDisplayData
    {
        /// <summary>
        /// 親コントロール
        /// </summary>
        public Control Con = null;

        /// <summary>
        /// 親情報
        /// </summary>
        public AnimeElement LayerData;

        /// <summary>
        /// 最大フレーム数
        /// </summary>
        public int MaxFrame;
        /// <summary>
        /// 1pixelの表示レート
        /// </summary>
        public double PixelRate;

        /// <summary>
        /// 描画Range
        /// </summary>
        public int DisplayPixelRange
        {
            get
            {
                double data = this.PixelRate * (double)this.MaxFrame;
                return Convert.ToInt32(data);
            }
        }

        /// <summary>
        /// 現在のマウス情報
        /// </summary>
        public EMouseMoveType MouseType = EMouseMoveType.None; 

        /// <summary>
        /// アニメ開始位置X
        /// </summary>
        public int AnimeStartPos = 0;
        /// <summary>
        /// アニメ開始位置Y
        /// </summary>
        public int AnimeEndPos = 0;

        /// <summary>
        /// アニメ幅
        /// </summary>
        public int AnimePixelWidth
        {
            get
            {
                return this.AnimeEndPos - this.AnimeStartPos;
            }
        }

        #region 変換関数
        public int PixelXToFrame(int pix)
        {
            double fa = (double)pix / this.PixelRate;
            return Convert.ToInt32(fa);
        }

        public int FrameToPixelX(int frame)
        {
            double fa = (double)frame * this.PixelRate;
            return Convert.ToInt32(fa);
        }

        #endregion

        /// <summary>
        /// アニメ一位置の再計算1
        /// </summary>
        /// <param name="startframe"></param>
        /// <param name="endframe"></param>
        public void CalcuAnimeDisplayPos(int startframe, int endframe)
        {
            this.AnimeStartPos = this.FrameToPixelX(startframe);
            this.AnimeEndPos = this.FrameToPixelX(endframe);
        }
    }


    class LayerControlRenderer
    {
        public Color ClearColor = Color.White;

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="pane"></param>
        /// <param name="data"></param>
        public void RenderControl(Graphics gra, Control con, LayerControlDisplayData data, int framepos)
        {
            //クリア
            gra.Clear(this.ClearColor);

            //目盛りの描画
            this.RenderScale(gra, con, data);

            //アニメの描画
            this.RenderAnime(gra, con, data);

            //現在位置フレームの描画
            this.RenderFramePosition(gra, con, data, framepos);
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 表示フレーム位置の描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="con"></param>
        /// <param name="framepos"></param>
        private void RenderFramePosition(Graphics gra, Control con, LayerControlDisplayData data, int framepos)
        {
            using (Pen pe = new Pen(Color.Black, 2.0f))
            {
                //今回の位置を計算
                int pix = data.FrameToPixelX(framepos);
                gra.DrawLine(pe, new Point(pix, 0), new Point(pix, con.Height));
            }
        }

        /// <summary>
        /// 目盛りの描画
        /// </summary>
        private void RenderScale(Graphics gra, Control con, LayerControlDisplayData data)
        {
            using (SolidBrush bru = new SolidBrush(Color.Black))
            {
                using (Pen pe = new Pen(Color.DarkGray))
                {

                    //罫線の描画
                    int prevpos = 0;
                    for (int i = 0; i < data.MaxFrame; i += 5)
                    {
                        //今回の位置を計算
                        int pix = data.FrameToPixelX(i);
                        int sa = pix - prevpos;
                        if (sa < 50)
                        {
                            //あまり詰まるなら描画しない
                            continue;
                        }
                        prevpos = pix;

                        gra.DrawLine(pe, new Point(pix, 0), new Point(pix, con.Height));


                        //文字の表示
                        if (data.LayerData == null)
                        {
                            SizeF fsize = gra.MeasureString(i.ToString(), data.Con.Font);
                            float fpos = pix - (fsize.Width * 0.5f);
                            gra.DrawString(i.ToString(), data.Con.Font, bru, new PointF(fpos, 0));
                        }

                    }
                }
            }
        }


        /// <summary>
        /// アニメの描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="pane"></param>
        /// <param name="data"></param>
        private void RenderAnime(Graphics gra, Control con, LayerControlDisplayData data)
        {
            if (data.LayerData == null)
            {
                return;
            }

            int height = con.Height;

            Rectangle anime_rect = new Rectangle(data.AnimeStartPos, 0, data.AnimePixelWidth, height);

            if (data.LayerData.EaData.Enabled == false)
            {
                return;
            }
            

            //アニメ位置の矩形を描画
            using (SolidBrush bru = new SolidBrush(Color.LightGreen))
            {                
                gra.FillRectangle(bru, anime_rect);
            }

            {
                //マウスが反応する端を演出
                int wid = LayerControl.MouseMoveDetectWidth;
                using (SolidBrush bru = new SolidBrush(Color.DarkSeaGreen))
                {
                    gra.FillRectangle(bru, new Rectangle(data.AnimeStartPos, 0, wid, height));
                    gra.FillRectangle(bru, new Rectangle(data.AnimeEndPos - wid, 0, wid, height));
                }
            }


            //タイトルの描画
            Font fo = con.Font;
            using (SolidBrush bru = new SolidBrush(Color.Black))
            {
                //描く位置の計算                
                string title = data.LayerData.SelectAnime?.Name;
                var size = gra.MeasureString(title, fo);

                int posy = (int)((height - size.Height) / 2.0f);

                gra.DrawString(title, fo, bru, new PointF(data.AnimeStartPos + 5.0f, posy));
            }

            //選択描画
            if (data.MouseType != EMouseMoveType.None)
            {
                using (Pen pe = new Pen(Color.Blue, 2.0f))
                {
                    gra.DrawRectangle(pe, anime_rect);
                }
                
            }
        }
    }
}
