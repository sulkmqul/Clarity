using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.FrameEdit
{
    public class FrameEditorParam
    {
        /// <summary>
        /// 1フレームの横サイズ
        /// </summary>
        public int FrameSize { get; set; } = 10;

        /// <summary>
        /// 描画開始位置
        /// </summary>
        public Point LeftTop = new Point(0, 0);


        /// <summary>
        /// 計測目盛りサイズ(フレーム値の描画)
        /// </summary>
        public int MeasureSize { get; set; } = 20;


        /// <summary>
        /// フレーム画像表示エリアサイズ
        /// </summary>
        public int ImageSize { get; set; } = 50;

        /// <summary>
        /// タグの高さ
        /// </summary>
        public int TagSize { get; set; } = 30;

        /// <summary>
        /// 最大タグ数 
        /// </summary>
        public int MaxTagCount { get; } = 10;
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
            {
                this.MeasureArea = new Rectangle(0, 0, width, fe.MeasureSize);
            }
            //フレーム画像領域の計算
            {
                this.ImageArea = new Rectangle(0, this.MeasureArea.Bottom, width, fe.ImageSize);
            }
            //タグサイズ
            {
                //タグ領域の高さ計算
                int th = fe.TagSize * fe.MaxTagCount;
                this.TagArea = new Rectangle(0, this.ImageArea.Bottom, width, th);
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
        public void Paint(Graphics gra)
        {
            gra.Clear(Color.White);

            //裏のフレームの区切りを描画

            //設定フレーム画像の描画

            //設定イベントの描画

            {
                using (SolidBrush bru = new SolidBrush(Color.Pink))
                {
                    gra.FillRectangle(bru, this.MeasureArea);
                }

                using (SolidBrush bru = new SolidBrush(Color.SkyBlue))
                {
                    gra.FillRectangle(bru, this.ImageArea);
                }

                using (SolidBrush bru = new SolidBrush(Color.Olive))
                {
                    gra.FillRectangle(bru, this.TagArea);
                }
            }
        }
    }
}
