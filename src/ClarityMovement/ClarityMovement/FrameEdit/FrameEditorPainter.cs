using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.FrameEdit
{
    /// <summary>
    /// エディタのパラメータ
    /// </summary>
    public class FrameEditorParam
    {
        /// <summary>
        /// 1フレームの横サイズ
        /// </summary>
        public int FrameSize { get; set; } = 10;

        /// <summary>
        /// 描画開始位置
        /// </summary>
        public Point LeftTop = new Point(50, 50);


        /// <summary>
        /// 計測目盛り縦サイズ(フレーム値の描画)
        /// </summary>
        public int MeasureSize { get; set; } = 20;


        /// <summary>
        /// フレーム画像表示エリア縦サイズ
        /// </summary>
        public int ImageSize { get; set; } = 80;

        /// <summary>
        /// タグの高さ
        /// </summary>
        public int TagSize { get; set; } = 30;

        /// <summary>
        /// 最大タグ数 
        /// </summary>
        public int MaxTagCount { get; } = 10;


        /// <summary>
        /// 線の色
        /// </summary>
        public Color BaseLineColor { get; set; } = Color.Gray;
    }

    /// <summary>
    /// エリア種別
    /// </summary>
    enum EEditorAreaType
    {
        Frame = 0,
        Image,
        Tag,

        None = 999,
    }


    /// <summary>
    /// エディターの描画
    /// </summary>
    internal class FrameEditorPainter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c">管理画面</param>
        public FrameEditorPainter(FrameEditControl c)
        {
            this.Parent= c;
        }


        /// <summary>
        /// 親画面
        /// </summary>
        private FrameEditControl Parent;


        /// <summary>
        /// フレーム領域のの描画範囲
        /// </summary>
        public Rectangle MeasureArea { get; private set; } = new Rectangle();
        /// <summary>
        /// 設定映像の描画範囲
        /// </summary>
        public Rectangle ImageArea { get; private set; } = new Rectangle();
        /// <summary>
        /// タグの描画範囲
        /// </summary>
        public Rectangle TagArea { get; private set; } = new Rectangle();


        

        /// <summary>
        /// 初期化されるとき
        /// </summary>        
        public void Init()
        {
        }



        /// <summary>
        /// エリア領域の再計算
        /// </summary>
        /// <param name="maxframe">最大フレーム数</param>
        /// <param name="fe">各種サイズパラメータ</param>
        /// <returns>これを描画するための必要コントロールサイズ</returns>
        public Size ResizeArea(int maxframe, FrameEditorParam fe)
        {
            //全体横幅の計算
            int width = maxframe * fe.FrameSize;


            //フレーム領域の計算
            int bottom = 0;
            {
                this.MeasureArea = new Rectangle(0, 0, width, fe.MeasureSize);
                bottom = this.MeasureArea.Bottom;
            }
            //フレーム画像領域の計算
            {
                this.ImageArea = new Rectangle(0, bottom, width, fe.ImageSize);
                bottom = this.ImageArea.Bottom;
            }
            //タグサイズ
            {
                //タグ領域の高さ計算
                int th = fe.TagSize * fe.MaxTagCount;
                this.TagArea = new Rectangle(0, bottom, width, th);
            }

            //とりあえず開始位置オフセット
            this.MeasureArea.Offset(fe.LeftTop);
            this.ImageArea.Offset(fe.LeftTop);
            this.TagArea.Offset(fe.LeftTop);


            //一番下の右下がサイズ
            Size ans = new Size(this.TagArea.Right, this.TagArea.Bottom);
            return ans;
        }


        /// <summary>
        /// 描画処理本体
        /// </summary>
        /// <param name="gra">描画物</param>        
        /// <param name="maxframe">最大フレーム数</param>
        public void Paint(Graphics gra, int maxframe)
        {
            gra.Clear(Color.White);

            //フレーム目盛りの描画
            

            //設定フレーム画像の描画

            //設定イベントの描画

            {

                //背景クリア
                using (SolidBrush bru = new SolidBrush(Color.FromArgb(255, 210, 210)))
                {
                    gra.FillRectangle(bru, this.MeasureArea);
                }

                using (SolidBrush bru = new SolidBrush(Color.FromArgb(210, 230, 255)))
                {
                    gra.FillRectangle(bru, this.ImageArea);
                }

                using (SolidBrush bru = new SolidBrush(Color.AntiqueWhite))
                {
                    gra.FillRectangle(bru, this.TagArea);
                }
            }

            this.PaintMeasureArea(gra, maxframe);
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// フレームの目盛りエリアの描画
        /// </summary>
        /// <param name="gra"></param>
        private void PaintMeasureArea(Graphics gra, int maxframe)
        {
            Rectangle area = this.MeasureArea;

           

            //線の描画
            var param = this.Parent.SizeParam;
            using (Pen pe = new Pen(param.BaseLineColor, 1))
            {
                using (SolidBrush bru = new SolidBrush(Color.Black))
                {
                    Font font = new Font("Tohoma", 10.0f);

                    //各フレーム枠の表示
                    for (int i = 0; i < maxframe; i++)
                    {
                        int left = i * param.FrameSize + area.X;

                        Rectangle cell = new Rectangle();
                        #region 目盛り
                        cell = new Rectangle(left, area.Top, param.FrameSize, param.MeasureSize);
                        gra.DrawRectangle(pe, cell);


                        //フレーム番号の表示
                        string text = (i + 1).ToString();
                        SizeF fs = gra.MeasureString(text, font);
                        Rectangle iarea = this.ImageArea;
                        if (fs.Width > param.FrameSize)
                        {
                            text = "..";
                        }
                        gra.DrawString(text, font, bru, cell.Left, cell.Bottom - fs.Height);
                        #endregion


                        #region 目盛り
                        {                            
                            cell = new Rectangle(left, iarea.Top, param.FrameSize, param.ImageSize);
                            gra.DrawRectangle(pe, cell);
                        }
                        #endregion


                        #region tag
                        {
                            Rectangle tarea = this.TagArea;

                            for (int tc = 0; tc < param.MaxTagCount; tc++)
                            {
                                int top = tarea.Top + (param.TagSize * tc);

                                cell = new Rectangle(left, top, param.FrameSize, param.TagSize);
                                gra.DrawRectangle(pe, cell);
                            }
                        }
                        #endregion
                    }
                }
            }

        }
    }
}
