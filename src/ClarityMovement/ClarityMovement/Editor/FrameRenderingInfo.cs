using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Editor
{
    /// <summary>
    /// フレーム描画情報
    /// </summary>
    internal class FrameRenderingInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">フレーム番号</param>
        /// <param name="fo">描画フォント</param>
        public FrameRenderingInfo(int index)
        {
            this.FrameIndex = index;            
        }

        /// <summary>
        /// フレーム番号
        /// </summary>
        public int FrameIndex { get; private set; }


        /// <summary>
        /// 基準幅
        /// </summary>
        public float BaseWidth { get; set; } = 100.0f;

        /// <summary>
        /// タグ一つの大きさ
        /// </summary>
        public float TagHeight { get; set; } = 30.0f;


        /// <summary>
        /// このフレームの描画領域のサイズと位置
        /// </summary>
        public RectangleF Area { get; private set; } = new RectangleF();
        /// <summary>
        /// ヘッダーの描画領域
        /// </summary>
        public RectangleF HeaderArea { get; private set; } = new RectangleF();
        /// <summary>
        /// 基準画像の描画領域
        /// </summary>
        public RectangleF BaseImageArea { get; private set; } = new RectangleF();
        /// <summary>
        /// タグの描画位置
        /// </summary>
        public List<PointF> TagPointList { get; init; } = new List<PointF>();

        /// <summary>
        /// 現在描画中か否か true=描画中、このフレームに関連するものは描画対象
        /// </summary>
        public bool RenderEnabled { get; private set; } = true;


        /// <summary>
        /// 中心位置
        /// </summary>
        public PointF Center
        {
            get
            {
                float cx = this.Area.Left + (this.Area.Width * 0.5f);
                float cy = this.Area.Top + (this.Area.Height * 0.5f);
                return new PointF(cx, cy);
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 描画領域を計算する
        /// </summary>
        /// <param name="con">描画領域</param>
        /// <param name="offset">描画オフセット</param>
        public void CalclateArea(Control con, PointF offset, MvProject proj)
        {
            //全体のサイズを計算する
            {
                //基準サイズを取得
                float x = this.BaseWidth * this.FrameIndex + offset.X;
                float y = offset.Y;

                //縦はタグの個数から計算した値ととコントロールサイズHの大きい方になる
                float tagsize = proj.TagList.Count * this.TagHeight;

                float ch = Math.Max(tagsize, con.Height);

                this.Area = new RectangleF(x, y, this.BaseWidth, ch);
            }
            //ヘッダー
            {
                //必要なら描画フォントに応じたさいずにすること
                this.HeaderArea = new RectangleF(this.Area.X, this.Area.Y, this.Area.Width, 20);
            }

            //基準画像描画を計算する
            {

                float starty = this.HeaderArea.Bottom;
                this.BaseImageArea = new RectangleF(this.Area.X, starty, this.Area.Width, 100);
            }

            //このフレームにおけるタグの描画位置を計算する
            {
                float startpos = this.BaseImageArea.Bottom;

                float taghelf = this.TagHeight * 0.5f;

                this.TagPointList.Clear();
                int tagindex = 0;
                proj.TagList.ForEach(x =>
                {
                    float y = (tagindex * this.TagHeight) + taghelf + startpos;

                    this.TagPointList.Add(new PointF(this.Center.X, y));
                    tagindex++;
                });
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="renderarea">描画エリア</param>
        /// <param name="gra">描画場所</param>
        public void Render(RectangleF renderarea, Graphics gra, Font fo)
        {
            //範囲外なら描画しない            
            this.RenderEnabled = this.CheckClipping(renderarea);
            if (this.RenderEnabled == false)
            {
                return;
            }

            
            //枠を描画
            using (Pen pe = new Pen(MvGlobal.Setting.Editor.GridColor, 1))
            {                   
                //枠
                gra.DrawRectangle(pe, this.Area.X, this.Area.Y, this.Area.Width, this.Area.Height);
                gra.DrawRectangle(pe, this.BaseImageArea.X, this.BaseImageArea.Y, this.BaseImageArea.Width, this.BaseImageArea.Height);

                //中心線を描く
                gra.DrawLine(pe, new PointF(this.Center.X, this.BaseImageArea.Bottom), new PointF(this.Center.X, this.Area.Bottom));
            }

            //フレーム番号の描画
            using (SolidBrush bru = new SolidBrush(MvGlobal.Setting.Editor.FontColor))
            {
                SizeF tsize = gra.MeasureString($"{this.FrameIndex + 1}", fo);
                float hw = tsize.Width * 0.5f;                
                gra.DrawString($"{this.FrameIndex + 1}", fo, bru, new PointF(this.Center.X - hw,  this.HeaderArea.Y));
            }
        }


        /// <summary>
        /// 当たった領域を検定する
        /// </summary>
        /// <param name="cpos"></param>
        /// <returns></returns>
        public BaseUiElement? ColPointAreaRect(PointF cpos)
        {
            BaseUiElement? ans = null;
            BaseUiElement[] ckvec =
            {
                new FrameImageUiElement(this),
            };

            foreach(var rc in ckvec)
            {
                bool f = rc.Area.Contains(cpos);
                if(f == true)
                {
                    ans = rc;
                }
            }

            return ans;

        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// データが描画範囲化否かを調べる
        /// </summary>
        /// <param name="rect">描画領域矩形</param>
        /// <returns>true=領域内 false=領域外</returns>
        private bool CheckClipping(RectangleF rect)
        {
            //4点すべてが領域内か否かを調べる
            PointF[] ckpos =
            {
                new PointF(rect.Left, rect.Top),
                new PointF(rect.Left, rect.Bottom),
                new PointF(rect.Right, rect.Top),
                new PointF(rect.Right, rect.Bottom),
            };

            int c = 0;
            foreach (var po in ckpos)
            {
                bool f = rect.Contains(po);
                if (f == true)
                {
                    c++;
                }
            }
            if (c == 0)
            {
                return false;
            }
            return true;
        }

    }



    
}
