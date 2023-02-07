using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clarity.GUI
{

    /*
     * Image View Displayの関係
     * Image=元画像のサイズ
     * View=元画像に拡大率を考慮したサイズ(仮想なのでオフセットはなし)
     * Display=ViewをControlサイズに合わせて一部だけ表示しているもの=オフセットはあるが、拡縮率はViewと同等
     * 
     * つまり
     * Image←→View変換はサイズが変わる
     * View←→Display変換は位置が変わる
     * 
     */

    /// <summary>
    /// 画像変換者
    /// </summary>
    public class ImageViewerTranslator
    {
        public ImageViewerTranslator()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">元サイズ</param>
        /// <param name="view">描画サイス</param>
        /// <param name="disp">表示サイズ</param>
        public ImageViewerTranslator(SizeF src, SizeF view, SizeF disp)
        {
            this.SrcSize = src;
            this.ViewSize = view;
            this.DispSize = disp;
        }

        #region 領域取得設定        
        /// <summary>
        /// 表示エリア領域
        /// </summary>
        internal RectangleF DispRect = new RectangleF(0, 0, 0, 0);

        /// <summary>
        /// 描画エリア
        /// </summary>
        internal RectangleF ViewRect = new RectangleF(0, 0, 0, 0);


        /// <summary>
        /// 表示元サイズ
        /// </summary>
        internal RectangleF SrcRect = new RectangleF(0, 0, 0, 0);

        /// <summary>
        /// 表示サイズの設定
        /// </summary>
        public SizeF DispSize
        {
            get
            {
                return this.DispRect.Size;
            }
            internal set
            {
                this.DispRect = new RectangleF(this.DispRect.Left, this.DispRect.Top, value.Width, value.Height);
            }
        }

        /// <summary>
        /// 画像サイズの設定
        /// </summary>
        public SizeF ViewSize
        {
            get
            {
                return this.ViewRect.Size;
            }
            internal set
            {
                this.ViewRect = new RectangleF(this.ViewRect.Left, this.ViewRect.Top, value.Width, value.Height);
            }
        }

        public float ViewX
        {
            get
            {
                return this.ViewRect.X;
            }
            set
            {
                this.ViewRect.X= value;
            }
        }
        public float ViewY
        {
            get
            {
                return this.ViewRect.Y;
            }
            set
            {
                this.ViewRect.Y = value;
            }
        }

        /// <summary>
        /// 元サイズの設定
        /// </summary>
        public SizeF SrcSize
        {
            get
            {
                return this.SrcRect.Size;
            }
            internal set
            {
                this.SrcRect = new RectangleF(this.SrcRect.Left, this.SrcRect.Top, value.Width, value.Height);
            }
        }

        #endregion



        #region 座標変換

        #region Src→View変換        
        /// <summary>
        /// 元Xから描画座標Xを計算する
        /// </summary>
        /// <returns>image x</returns>
        /// <param name="sx">Ix.</param>
        public float SrcXToViewX(float sx)
        {
            float dw = this.ViewRect.Width;
            float sw = this.SrcRect.Width;

            float ans = sx * dw / sw;
            return ans;
        }

        /// <summary>
        /// 元Yから描画座標Yを計算する
        /// </summary>
        /// <returns>The YT o disp y.</returns>
        /// <param name="sy">sy.</param>
        public float SrcYToViewY(float sy)
        {
            float dh = this.ViewRect.Height;
            float sh = this.SrcRect.Height;

            float ans = sy * dh / sh;            
            return ans;
        }

        /// <summary>
        /// 元座標から描画座標を計算する
        /// </summary>
        /// <returns>The to display.</returns>
        /// <param name="sp">Ip.</param>
        public PointF SrcPointToViewPoint(PointF sp)
        {
            PointF ans = new PointF();
            ans.X = this.SrcXToViewX(sp.X);
            ans.Y = this.SrcYToViewY(sp.Y);
            return ans;
        }
        #endregion

        #region View→Src変換                
        /// <summary>
        /// 描画座標Xから元座標Xを算出する
        /// </summary>
        /// <returns>The XT o image x.</returns>
        /// <param name="dx">Dx.</param>
        public float ViewXToSrcX(float vx)
        {
            float dw = this.ViewRect.Width;
            float sw = this.SrcRect.Width;

            float ans = vx * sw / dw;
            return ans;
        }

        /// <summary>
        /// 画面座標Yから元座標Yを算出する
        /// </summary>
        /// <returns>The YT o image y.</returns>
        /// <param name="dy">Dy.</param>
        public float ViewYToSrcY(float vy)
        {
            float dh = this.ViewRect.Height;
            float sh = this.SrcRect.Height;

            float ans = vy * sh / dh;            
            return ans;
        }

        /// <summary>
        /// 画面座標から画像座標を計算する
        /// </summary>
        /// <returns>The point to image point.</returns>
        /// <param name="dpos">Dpos.</param>
        public PointF ViewPointToSrcPoint(PointF dpos)
        {
            PointF ans = new PointF();
            ans.X = this.ViewXToSrcX(dpos.X);
            ans.Y = this.ViewYToSrcY(dpos.Y);
            return ans;
        }
        #endregion

        #region View→Display変換        
        /// <summary>
        /// 描画座標Xから表示座標Xを計算する
        /// </summary>
        /// <param name="vx"></param>
        /// <returns></returns>
        public float ViewXToDispX(float vx)
        {
            float ans = vx + this.ViewRect.X;
            ans = ans + this.DispRect.X;
            return ans;
        }
        /// <summary>
        /// 描画座標Yから表示座標Yを計算する
        /// </summary>
        /// <param name="vy"></param>
        /// <returns></returns>
        public float ViewYToDispY(float vy)
        {
            float ans = vy + this.ViewRect.Y;
            ans = ans + this.DispRect.Y;
            return ans;
        }

        /// <summary>
        /// 描画座標から表示座標へ変換する
        /// </summary>
        /// <param name="vp"></param>
        /// <returns></returns>
        public PointF ViewPointToDispPoint(PointF vp)
        {
            PointF ans = new PointF();
            ans.X = this.ViewXToDispX(vp.X);
            ans.Y = this.ViewYToDispY(vp.Y);
            return ans;
        }
        #endregion

        #region Display→View変換
        /// <summary>
        /// 表示座標Xから描画座標Xを計算する
        /// </summary>
        /// <param name="dx"></param>
        /// <returns></returns>
        public float DispXToViewX(float dx)
        {
            float ans = dx + this.DispRect.X;
            ans = ans - this.ViewRect.X;
            return ans;
        }
        /// <summary>
        /// 表示座標Yから描画座標Yを計算する
        /// </summary>
        /// <param name="vy"></param>
        /// <returns></returns>
        public float DispYToViewY(float dy)
        {
            float ans = dy + this.DispRect.Y;
            ans = ans - this.ViewRect.Y;
            return ans;
        }

        /// <summary>
        /// 表示位置から描画位置を計算する
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public PointF DispPointToViewPoint(PointF dp)
        {
            PointF ans = new PointF();
            ans.X = this.DispXToViewX(dp.X);
            ans.Y = this.DispYToViewY(dp.Y);
            return ans;
        }

        #endregion

        #region Src→Display変換
        /// <summary>
        /// 元座標Xから表示座標Xへ変換する
        /// </summary>
        /// <param name="sx"></param>
        /// <returns></returns>
        public float SrcXToDispX(float sx)
        {
            float vx = this.SrcXToViewX(sx);
            return this.ViewXToDispX(vx);
        }
        /// <summary>
        /// 元座標Yから表示座標Yへ変換する
        /// </summary>
        /// <param name="sy"></param>
        /// <returns></returns>
        public float SrcYToDispY(float sy)
        {
            float vy = this.SrcYToViewY(sy);
            return this.ViewYToDispY(vy);
        }

        /// <summary>
        /// 元座標から表示座標へ変換する
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public PointF SrcPointToDispPoint(PointF sp)
        {
            PointF ans = new PointF();
            ans.X = this.SrcXToDispX(sp.X);
            ans.Y = this.SrcYToDispY(sp.Y);
            return ans;
        }
        #endregion

        #region Display→Src変換
        /// <summary>
        /// 表示座標Xから元座標Xを計算する
        /// </summary>
        /// <param name="dx"></param>
        /// <returns></returns>
        public float DispXToSrcX(float dx)
        {
            float vx = this.DispXToViewX(dx);
            return this.ViewXToSrcX(vx);
        }
        /// <summary>
        /// 表示座標Yから元座標Yを計算する
        /// </summary>
        /// <param name="dy"></param>
        /// <returns></returns>
        public float DispYToSrcY(float dy)
        {
            float vy = this.DispYToViewY(dy);
            return this.ViewYToSrcY(vy);
        }

        /// <summary>
        /// 表示位置から元位置を算出する
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public PointF DispPointToSrcPoint(PointF dp)
        {
            PointF ans = new PointF();
            ans.X = this.DispXToSrcX(dp.X);
            ans.Y = this.DispYToSrcY(dp.Y);
            return ans;
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// 描画管理基底クラス 
    /// </summary>
    public abstract class BaseDisplayer : ImageViewerTranslator
    {
        /// <summary>
        /// 管理リンク(minidipalyerのリンク)
        /// </summary>
        internal BaseDisplayer? ManageLink { get; set; } = null;


        /// <summary>
        /// マウスが押されたとき
        /// </summary>
        /// <param name="minfo"></param>        
        public virtual void MouseDown(MouseInfo minfo)
        {
            
        }

        /// <summary>
        /// マウスが押されたとき
        /// </summary>
        /// <param name="minfo"></param>
        /// <returns></returns>
        public virtual void MouseMove(MouseInfo minfo)
        {
            
        }

        /// <summary>
        /// マウスが押されたとき
        /// </summary>
        /// <param name="minfo"></param>        
        public virtual void MouseUp(MouseInfo minfo)
        {
            
        }


        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra"></param>
        public virtual void Render(Graphics gra)
        {
            
        }


        /// <summary>
        /// エリアの作成
        /// </summary>
        /// <param name="c">中心位置</param>
        /// <param name="width">半径</param>
        /// <returns></returns>
        public Rectangle CreateArea(Point c, int whlf)
        {
            Rectangle ans = new Rectangle();
            ans.X = c.X - whlf;
            ans.Y = c.Y - whlf;
            ans.Width = whlf * 2;
            ans.Height = whlf * 2;
            
            return ans;
        }


        
    }
}
