using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.GUI;
using System.Drawing;
namespace ClarityOrbit.TipSelectView
{
    /// <summary>
    /// 元画像グリッド表示
    /// </summary>
    internal class SrcTileImageGridDisplayer : BaseDisplayer
    {
        public SrcTileImageGridDisplayer()
        {
        }
        

        public TileImageSrcInfo SrcInfo;

        /// <summary>
        /// マウスが被っている領域(index)
        /// </summary>
        private Point? MouseOnTileIndex = null;

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra"></param>
        public override void Render(Graphics gra)
        {
            //タイルgridの表示
            this.RenderTileGrid(gra);

            //枠線の描画
            this.RenderBorder(gra);

            //マウス位置の描画
            this.RenderMouseOverRect(gra);
            

            //選択位置の描画
            this.RenderSelectedRect(gra);
            
        }


        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseMove(MouseInfo minfo)
        {
            //マウスオーバー領域の計算
            this.MouseOnTileIndex = this.MousePointToTileIndex(minfo.NowPos);

            if (minfo.DownButton != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }

            //選択領域の更新
            this.UpdateSelectedIndexRect(minfo);
        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="minfo"></param>
        public override void MouseUp(MouseInfo minfo)
        {
            if (minfo.DownButton != System.Windows.Forms.MouseButtons.Left)
            {
                return;            
            }

            //選択領域の更新
            this.UpdateSelectedIndexRect(minfo);
        }

        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// Grid表示
        /// </summary>
        /// <param name="gra"></param>
        private void RenderTileGrid(Graphics gra)
        {
            var tcount = this.SrcInfo.TileCount;
            var tsize = this.SrcInfo.TileSize;


            using (Pen pe = new Pen(Color.White, 1.0f))
            {
                pe.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                float left = this.SrcXToDispX(0);
                float right = this.SrcXToDispX(this.SrcInfo.ImageSize.Width);
                float top = this.SrcYToDispY(0);
                float bottom = this.SrcYToDispY(this.SrcInfo.ImageSize.Height);

                //横
                for (int y = 0; y < tcount.Height + 1; y++)
                {
                    float ypos = this.SrcYToDispY(y * tsize.Height);
                    gra.DrawLine(pe, new PointF(left, ypos), new PointF(right, ypos));
                }
                //縦
                for (int x = 0; x < tcount.Width + 1; x++)
                {
                    float xpos = this.SrcXToDispX(x * tsize.Width);
                    gra.DrawLine(pe, new PointF(xpos, top), new PointF(xpos, bottom));
                }
            }
        }

        /// <summary>
        /// 領域の描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderBorder(Graphics gra)
        {
            Size isize = this.SrcInfo.ImageSize;

            float x = this.SrcXToDispX(0);
            float y = this.SrcYToDispY(0);
            float w = this.SrcXToDispX(isize.Width) - x;
            float h = this.SrcYToDispY(isize.Height) - y;

            using (Pen pe = new Pen(Color.White, 2f))
            {
                gra.DrawRectangle(pe, x, y, w, h);
            }

        }

        /// <summary>
        /// 選択領域の描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderSelectedRect(Graphics gra)
        {
            if (OrbitGlobal.ControlInfo.SrcSelectedInfo == null)
            {
                return;
            }

            var sel = OrbitGlobal.ControlInfo.SrcSelectedInfo;

            //indexを画像位置に
            Point lt = this.SrcInfo.CalcuIndexToImageLT(sel.SelectedIndexRect.Left, sel.SelectedIndexRect.Top); ;
            Point rb = this.SrcInfo.CalcuIndexToImageLT(sel.SelectedIndexRect.Right, sel.SelectedIndexRect.Bottom);

            //画像位置を描画位置に
            PointF dplt =this.SrcPointToDispPoint(new PointF(lt.X, lt.Y));
            PointF dprb = this.SrcPointToDispPoint(new PointF(rb.X, rb.Y));

            //矩形描画
            using (Pen pe = new Pen(Color.Red, 4.0f))
            {
                gra.DrawRectangle(pe, dplt.X, dplt.Y, dprb.X - dplt.X, dprb.Y - dplt.Y);
            }
        }

        /// <summary>
        /// マウスが被っている領域の描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderMouseOverRect(Graphics gra)
        {
            if (this.MouseOnTileIndex == null)
            {
                return;
            }

            var sel = OrbitGlobal.ControlInfo.SrcSelectedInfo;

            //indexを画像位置に
            Point lt = this.SrcInfo.CalcuIndexToImageLT(this.MouseOnTileIndex?.X ?? -10, this.MouseOnTileIndex?.Y ?? -10);
            Point rb = this.SrcInfo.CalcuIndexToImageLT(this.MouseOnTileIndex?.X + 1 ?? -10, this.MouseOnTileIndex?.Y + 1 ?? -10);

            //画像位置を描画位置に
            PointF dplt = this.SrcPointToDispPoint(new PointF(lt.X, lt.Y));
            PointF dprb = this.SrcPointToDispPoint(new PointF(rb.X, rb.Y));

            //矩形描画
            using (Pen pe = new Pen(Color.White, 2.0f))
            {
                gra.DrawRectangle(pe, dplt.X, dplt.Y, dprb.X - dplt.X, dprb.Y - dplt.Y);
            }
        }

        /// <summary>
        /// タイルの矩形サイズ(pixel)を計算する
        /// </summary>
        /// <param name="tposx">tile index pos x</param>
        /// <param name="tposy">tile index pos y</param>
        /// <returns></returns>
        private RectangleF CalcuDisplayRectangle(int tposx, int tposy)
        {            
            var tsize = this.SrcInfo.TileSize;

            //四隅位置を計算
            float left = tposx * tsize.Width;
            float right = (tposx + 1) * tsize.Width;
            float top = tposy * tsize.Height;
            float bottom = (tposy + 1) * tsize.Height;

            //描画座標変換して幅、高さを算出
            left = this.SrcXToDispX(left);
            right = this.SrcXToDispX(right);
            top = this.SrcYToDispY(top);
            bottom = this.SrcYToDispY(bottom);
            float width = right - left;
            float height = bottom - top;

            //
            RectangleF ans = new RectangleF(left, top, width, height);
            return ans;

        }


        /// <summary>
        /// 選択矩形の更新
        /// </summary>
        /// <param name="minfo"></param>
        private void UpdateSelectedIndexRect(MouseInfo minfo)
        {
            //押した位置のindexを取得
            Point ist = this.MousePointToTileIndex(minfo.DownPos);
            //離した位置のindexを取得
            Point ied = this.MousePointToTileIndex(minfo.NowPos);

            //選択情報の作成
            OrbitGlobal.ControlInfo.SrcSelectedInfo = SrcImageSelectedInfo.CreateSrcSelectedInfo(this.SrcInfo, ist, ied);
        }

        /// <summary>
        /// マウス位置(DisplayPoint)のtile indexを計算
        /// </summary>
        /// <param name="mpos">マウス位置</param>
        /// <returns></returns>
        private Point MousePointToTileIndex(PointF mpos)
        {
            PointF dpos = this.DispPointToSrcPoint(mpos);

            //indexを取得
            Point ans = this.SrcInfo.CalcuImagePosIndex((int)dpos.X, (int)dpos.Y);
            return ans;
        }
        
    }
}
