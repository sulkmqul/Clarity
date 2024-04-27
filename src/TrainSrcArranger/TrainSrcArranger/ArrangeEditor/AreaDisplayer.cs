using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace TrainSrcArranger.ArrangeEditor
{
    

    internal class AreaDisplayer : BaseDisplayer
    {


        public AreaDisplayer(Size size)
        {
            this.RData = new AreaRenderData(size);
        }

        public delegate void ApplyAreaDelegate(RectangleF srcarea);

        //public event ApplyAreaDelegate ApplyAreaEvent;

        class AreaRenderData
        {
            public AreaRenderData(Size size)
            {
                this.AreaSize = size;
            }

            /// <summary>
            /// 描画サイズ指定
            /// </summary>
            private Size AreaSize;

            /// <summary>
            /// 表示サイズ
            /// </summary>
            public RectangleF DispArea { get; private set; } = new RectangleF();


            /// <summary>
            /// 位置更新
            /// </summary>
            /// <param name="mpos">表示マウス位置</param>
            public void UpdatePos(Point mpos, RectangleF vrect)
            {
                float hw = this.AreaSize.Width * 0.5f;
                float hh = this.AreaSize.Height * 0.5f;


                this.DispArea = new RectangleF(mpos.X - hw, mpos.Y - hh, this.AreaSize.Width, this.AreaSize.Height);

                float cx = this.DispArea.X;
                float cy = this.DispArea.Y;

                //View領域を超えないように補正する
                if (this.DispArea.X < vrect.X)
                {
                    cx = vrect.X;
                }
                if (this.DispArea.Y < vrect.Y)
                {
                    cy = vrect.Y;
                }

                if (this.DispArea.Right > vrect.Right)
                {
                    cx = vrect.Right - this.AreaSize.Width;
                }
                if (this.DispArea.Bottom > vrect.Bottom)
                {
                    cy = vrect.Bottom - this.AreaSize.Height;
                }
                
                //表示サイズが領域より小さくなったら中心とする
                if(vrect.Width < this.AreaSize.Width)
                {
                    cx = vrect.Left + (vrect.Width * 0.5f) - hw;
                }
                if (vrect.Height < this.AreaSize.Height)
                {
                    cy = vrect.Top + (vrect.Height * 0.5f) - hh;
                }

                this.DispArea = new RectangleF(cx, cy, this.AreaSize.Width, this.AreaSize.Height);
            }
        }

        /// <summary>
        /// 描画データ
        /// </summary>
        private AreaRenderData RData;

        

        /// <summary>
        /// 現在の選択エリアを確定させる
        /// </summary>
        /// <returns></returns>
        public RectangleF GetSelectSrcArea()
        {
            //表示エリアを元画像エリアに変換する
            float l = this.DispXToSrcX(this.RData.DispArea.Left);
            float r = this.DispXToSrcX(this.RData.DispArea.Right);

            float t = this.DispYToSrcY(this.RData.DispArea.Top);
            float b = this.DispYToSrcY(this.RData.DispArea.Bottom);

            //画像切り抜き領域の作成
            RectangleF ans = new RectangleF(l, t, r - l, b - t);

            return ans;
        }

        
        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseMove(MouseInfo minfo)
        {
            base.MouseMove(minfo);            

            
            //現在地の更新
            this.RData.UpdatePos(minfo.NowPos, new RectangleF(this.ViewX, this.ViewY, this.ViewSize.Width, this.ViewSize.Height));

        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseUp(MouseInfo minfo)
        {
            base.MouseUp(minfo);

            //領域の確定処理をする・・・不都合ががったのでキーボード確定にする
            //画像切り抜き領域の作成
            //RectangleF srcrect = this.GetSelectSrcArea();
            //コール
            //this.ApplyAreaEvent(srcrect);
            

        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra"></param>
        public override void Render(Graphics gra)
        {
            base.Render(gra);

            using(Pen pe = new Pen(Color.Red, 2.0f))
            {
                gra.DrawRectangle(pe, this.RData.DispArea.X, this.RData.DispArea.Y, this.RData.DispArea.Width, this.RData.DispArea.Height);
            }
           
        }
    }
}
