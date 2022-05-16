using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace ClarityEmotion.Core
{
    /// <summary>
    /// データ描画コア
    /// </summary>
    public class EmotionGenerator
    {
        public EmotionGenerator(bool edit)
        {
            this.EditFlag = edit;
        }

        /// <summary>
        /// 編集状態表示可否
        /// </summary>
        public bool EditFlag = false;

        public Size ImageSize = new Size(1, 1);
        public Size DispSize = new Size(1, 1);


        /// <summary>
        /// 描画初期化
        /// </summary>
        /// <param name="isize">描画元サイズ</param>
        /// <param name="dsize">実際の表示サイズ</param>
        public void SetImageInit(Size isize, Size dsize)
        {
            this.ImageSize = isize;
            this.DispSize = dsize;
        }


        /// <summary>
        /// 描画処理本体 対象フレームの描画
        /// </summary>
        /// <param name="gra">描画場所</param>
        /// <param name="alist">描画element</param>
        /// <param name="frame">フレーム</param>
        public List<AnimeElement> Render(Graphics gra, List<AnimeElement> alist, int frame)
        {
            //対象フレームの有効な物体を取得
            List<AnimeElement> ealist = alist.Where(x =>
            {
                if (x.StartFrame <= frame && frame < x.EndFrame && x.EaData.Enabled == true)
                {
                    return true;
                }
                return false;
            }).OrderBy(x => x.EaData.LayerNo).ToList();

            //今回の描画対象がない
            if (ealist.Count <= 0)
            {
                return ealist;
            }

            //フレーム描画
            ealist.ForEach(x => this.RenderElement(gra, x, frame));


            //編集中は余計なものの描画
            if (this.EditFlag == true)
            {
                this.RenderEditInfo(gra, ealist, frame);

               
            }


            return ealist;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        #region 座標変換たち
        /// <summary>
        /// 画像Xを表示Xに変換する
        /// </summary>
        /// <param name="ix"></param>
        /// <returns></returns>
        public int ImageXToDispX(int ix)
        {
            double rate = (double)this.DispSize.Width / (double)this.ImageSize.Width;
            double ax = rate * (double)(ix);
            ax = Math.Round(ax);

            int ans = Convert.ToInt32(ax);
            return ans;
        }
        /// <summary>
        /// 画像Yを表示Yに変換する
        /// </summary>
        /// <param name="iy"></param>
        /// <returns></returns>
        public int ImageYToDispY(int iy)
        {
            double rate = (double)this.DispSize.Height / (double)this.ImageSize.Height;
            double ay = rate * (double)(iy);
            ay = Math.Round(ay);

            int ans = Convert.ToInt32(ay);
            return ans;
        }

        /// <summary>
        /// 画像座標を表示座標に変換する
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Point ImageToDisp(Point pos)
        {
            Point ans = new Point();

            ans.X = this.ImageXToDispX(pos.X);
            ans.Y = this.ImageYToDispY(pos.Y);

            return ans;
        }


        /// <summary>
        /// 表示座標Xを画像座標Xに変換する
        /// </summary>
        /// <param name="dx"></param>
        /// <returns></returns>
        private int DispXToImageX(int dx)
        {
            double rate = (double)this.ImageSize.Width / (double)this.DispSize.Width;
            double ax = rate * (double)(dx);
            ax = Math.Round(ax);

            int ans = Convert.ToInt32(ax);
            return ans;
        }

        /// <summary>
        /// 表示座標Yを画像座標Yに変換する
        /// </summary>
        /// <param name="dy"></param>
        /// <returns></returns>
        public int DispYToImageY(int dy)
        {
            double rate = (double)this.ImageSize.Height / (double)this.DispSize.Height;
            double ay = rate * (double)(dy);
            ay = Math.Round(ay);

            int ans = Convert.ToInt32(ay);
            return ans;
        }

        /// <summary>
        /// 表示座標を画像座標に変換する
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Point DispToImage(Point pos)
        {
            Point ans = new Point();

            ans.X = this.DispXToImageX(pos.X);
            ans.Y = this.DispYToImageY(pos.Y);

            return ans;
        }
        #endregion

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 対象の描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="adata"></param>
        /// <param name="frame"></param>
        private void RenderElement(Graphics gra, AnimeElement adata, int frame)
        {
            //描画情報取得
            AnimeFrameRenderData rdata = adata.GetFrameImage(frame);
            if (rdata == null)
            {
                return;
            }

            Bitmap bit = rdata.Image;
            //Flip処理
            bit = this.FlipBitmap(adata.EaData.FlipType, bit);

            //位置とサイズを変換し、位置を保存
            adata.TempData.DispAreaRect.X = this.ImageXToDispX(rdata.EaData.Pos2D.X);
            adata.TempData.DispAreaRect.Y = this.ImageYToDispY(rdata.EaData.Pos2D.Y);

            double fw = (double)bit.Width * rdata.EaData.ScaleRate;
            double fh = (double)bit.Height * rdata.EaData.ScaleRate;
            adata.TempData.DispAreaRect.Width = this.ImageXToDispX(Convert.ToInt32(fw));
            adata.TempData.DispAreaRect.Height = this.ImageYToDispY(Convert.ToInt32(fh));


            //透明設定
            ColorMatrix cmat = new ColorMatrix();

            cmat.Matrix00 = 1.0f;
            cmat.Matrix11 = 1.0f;
            cmat.Matrix22 = 1.0f;
            cmat.Matrix33 = rdata.EaData.Alpha;
            cmat.Matrix44 = 1.0f;

            ImageAttributes iat = new ImageAttributes();
            iat.SetColorMatrix(cmat);

            //これでマスク可能
            //gra.SetClip

            //加算合成はデフォルトでは無理っぽい

            

            //画像表示
            gra.DrawImage(bit, adata.TempData.DispAreaRect, 
                0, 0, adata.TempData.DispAreaRect.Width, adata.TempData.DispAreaRect.Height,
                GraphicsUnit.Pixel, iat);

        }

        /// <summary>
        /// 画像のFLip処理
        /// </summary>
        /// <param name="ftype"></param>
        /// <param name="bit"></param>
        /// <returns></returns>
        private Bitmap FlipBitmap(EFlipType ftype, Bitmap bit)
        {
            if ((ftype & EFlipType.XFlip) == EFlipType.XFlip)
            {
                bit.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            if ((ftype & EFlipType.YFlip) == EFlipType.YFlip)
            {
                bit.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }
            return bit;
        }

        //----------------------------------------------------------------------------------------
        /// <summary>
        /// 編集中情報の描画可否
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="ealist"></param>
        /// <param name="frame"></param>
        private void RenderEditInfo(Graphics gra, List<AnimeElement> ealist, int frame)
        {
            //とりあえず領域
            ealist.ForEach(x => this.RenderArea(gra, x));

            //マウスオーバー
            AnimeElement oe = ealist.Where(x => x.TempData.MouseOverFlag).FirstOrDefault();
            if (oe != null)
            {
                this.RenderMouseOver(gra, oe);
            }

            //選択領域
            AnimeElement sedata = ealist.Where(x => x.LayerNo == EmotionProject.Mana.SelectLayerData?.LayerNo).FirstOrDefault();
            if (sedata != null)
            {
                this.RenderSelected(gra, sedata);
            }
        }


        /// <summary>
        /// 自身の領域の描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="adata"></param>
        private void RenderArea(Graphics gra, AnimeElement adata)
        {
            using (Pen pe = new Pen(Color.Blue, 1.0f))
            {                
                this.RenderElementArea(gra, pe, adata.TempData.DispAreaRect);
            }
        }




        /// <summary>
        /// 選択の描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="adata"></param>
        private void RenderSelected(Graphics gra, AnimeElement adata)
        {
            using (Pen pe = new Pen(Color.Yellow, 2.0f))
            {
                //gra.DrawRectangle(pe, adata.TempData.DispAreaRect);
                this.RenderElementArea(gra, pe, adata.TempData.DispAreaRect);
            }
        }
        /// <summary>
        /// マウスオーバーの描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="adata"></param>
        private void RenderMouseOver(Graphics gra, AnimeElement adata)
        {
            using (Pen pe = new Pen(Color.Blue, 2.0f))
            {
                //gra.DrawRectangle(pe, adata.TempData.DispAreaRect);
                this.RenderElementArea(gra, pe, adata.TempData.DispAreaRect);
            }
        }




        /// <summary>
        /// 自身の範囲の描画
        /// </summary>
        /// <param name="gra"></param>
        /// <param name="pe"></param>
        /// <param name="rect"></param>
        private void RenderElementArea(Graphics gra, Pen pe, Rectangle rect)
        {
            //矩形の描画
            gra.DrawRectangle(pe, rect);

            //中心線の描画
            int hw = rect.X + (rect.Width / 2);
            int hh = rect.Y + (rect.Height / 2);

            gra.DrawLine(pe, new Point(hw, rect.Top), new Point(hw, rect.Bottom));
            gra.DrawLine(pe, new Point(rect.Left, hh), new Point(rect.Right, hh));

        }
    }
}