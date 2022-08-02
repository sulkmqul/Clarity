using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Clarity.GUI
{
    //public delegate void ClarityImageViewerMouseEventDelegate(MouseInfo minfo, ImageViewerTranslator ivt);
    //public delegate void ClarityImageViewerRenderEventDelegate(Graphics gra, ImageViewerTranslator ivt);

    /// <summary>
    /// 画像表示コントロール
    /// </summary>
    public partial class ClarityViewer : UserControl
    {
        public ClarityViewer()
        {
            InitializeComponent();

            this.MouseWheel += this.ClarityViewer_MouseWheel;
            this.DoubleBuffered = true;
        }

        private const string CommonDescriptionCategory = "ClarityViewer";
        private const string MinimapDescriptionCategory = "Minimap";

        #region public property
        #region Viewer        
        [Category(CommonDescriptionCategory)]
        [Description("背景色")]
        public Color ClearColor { get; set; } = Color.Black;

        [Category(CommonDescriptionCategory)]
        [Description("Src範囲色(画像無の時のみ有効)")]
        public Color SrcBackColor { get; set; } = Color.Red;

        [Category(CommonDescriptionCategory)]
        [Description("右ダブルクリックセンタリング可否")]
        public bool DoubleClickCentering { get; set; } = true;


        [Category(CommonDescriptionCategory)]
        [Description("表示画像補間モード")]
        public System.Drawing.Drawing2D.InterpolationMode ImageInterpolationMode { get; set; } = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
        #endregion

        #region Minimap
        [Category(MinimapDescriptionCategory)]
        [Description("ミニマップ表示可否")]
        public bool MinimapVisible { 
            get
            {
                return this.clarityViewerMinimapView.Visible;
                
            }
            set
            {
                this.clarityViewerMinimapView.Visible = value;                
            }
        }

        [Category(MinimapDescriptionCategory)]
        [Description("ミニマップサイズ比")]
        public float MinimapSizeRate
        {
            get
            {
                return this.clarityViewerMinimapView.MinimapSizeRate;
            }
            set
            {
                this.clarityViewerMinimapView.MinimapSizeRate = value;
            }
        }

        [Category(MinimapDescriptionCategory)]
        [Description("描画位置基準比")]
        public float MinimapDisplayMerginRate
        {
            get
            {
                return this.clarityViewerMinimapView.MinimapDisplayMerginRate;
            }
            set
            {
                this.clarityViewerMinimapView.MinimapDisplayMerginRate = value;
            }
        }

        [Category(MinimapDescriptionCategory)]
        [Description("表示エリア描画色")]
        public Color DisplayAreaLineColor
        {
            get
            {
                return this.clarityViewerMinimapView.DisplayAreaLineColor;
            }
            set
            {
                this.clarityViewerMinimapView.DisplayAreaLineColor = value;
            }
        }

        [Category(MinimapDescriptionCategory)]
        [Description("表示エリア線幅")]
        public float DisplayAreaLineWidth
        {
            get
            {
                return this.clarityViewerMinimapView.DisplayAreaLineWidth;
            }
            set
            {
                this.clarityViewerMinimapView.DisplayAreaLineWidth = value;
            }
        }

        [Category(MinimapDescriptionCategory)]
        [Description("表示エリア背景色")]
        public Color MinimapBackColor
        {
            get
            {
                return this.clarityViewerMinimapView.SrcBackColor;
            }
            set
            {
                this.clarityViewerMinimapView.SrcBackColor = value;
            }
        }
        [Category(MinimapDescriptionCategory)]
        [Description("登録Displayer描画可否")]
        public bool DisplayerRendering
        {
            get
            {
                return this.clarityViewerMinimapView.DisplayerRendering;
            }
            set
            {
                this.clarityViewerMinimapView.DisplayerRendering = value;
            }
        }
        #endregion

        #endregion

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 現在の拡大率
        /// </summary>
        [Browsable(false)]
        public double ZoomRate { get; protected set; } = 1.0;


        /// <summary>
        /// 表示範囲
        /// </summary>
        internal RectangleF DispRect
        {
            get
            {
                return this.Ivt.DispRect;
            }
            set
            {
                this.Ivt.DispRect = value;
            }
        }

        /// <summary>
        /// 描画範囲
        /// </summary>
        internal RectangleF ViewRect
        {
            get
            {
                return this.Ivt.ViewRect;
            }
            set
            {
                this.Ivt.ViewRect = value;
            }
        }

        /// <summary>
        /// 元エリア
        /// </summary>
        internal RectangleF SrcRect
        {
            get
            {
                return this.Ivt.SrcRect;
            }
            set
            {
                this.Ivt.SrcRect = value;
            }
        }
        #region メンバ変数
        /// <summary>b
        /// 読み込み画像
        /// </summary>
        private Image? SrcImage = null;

        /// <summary>
        /// 座標管理
        /// </summary>
        private ImageViewerTranslator Ivt = new ImageViewerTranslator();

        /// <summary>
        /// 拡大率テーブル
        /// </summary>
        private double[] ZoomTable = { 0.0625, 0.125, 0.25, 0.5, 1.0, 2.0, 4.0, 8.0, 16.0, 32.0, 64.0, 128.0, 256.0, 512.0 };

        /// <summary>
        /// マウス情報管理
        /// </summary>
        private MouseInfo MInfo = new MouseInfo();

        /// <summary>
        /// 描画管理一式
        /// </summary>
        private List<BaseDisplayer> DisplayerList = new List<BaseDisplayer>();

        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 画像無で初期化する
        /// </summary>
        /// <param name="srcsize">オリジナルのサイズ</param>
        public void Init(SizeF srcsize)
        {
            this.Init(null, srcsize);
        }


        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="filepath">読み込みファイルパス</param>
        public void Init(string filepath)
        {
            using (Bitmap bit = new Bitmap(filepath))
            {
                this.Init(bit);
            }
        }

        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="srcimage"></param>
        public void Init(Image srcimage)
        {
            this.Init(srcimage, srcimage.Size);
        }

        /// <summary>
        /// 一段階拡縮
        /// </summary>
        /// <param name="f">true=拡大 false=縮小</param>
        /// <param name="dcen">拡縮中心disp位置(nullで中心拡大)</param>
        public void Zoom(bool f, PointF? dcen = null)
        {
            int iz = this.SeachZoomTableIndex(this.ZoomRate);
            iz = (f == true) ? iz + 1 : iz - 1;
            //範囲内に収め、次の各縮率を取得
            iz = Math.Clamp(iz, 0, this.ZoomTable.Length - 1);
            this.ZoomRate = this.ZoomTable[iz];

            //拡縮中心位置を表示位置中心とする
            float cx = this.DispRect.Width * 0.5f;
            float cy = this.DispRect.Height * 0.5f;
            cx = dcen?.X ?? cx;
            cy = dcen?.Y ?? cy;

            this.ChangeZoom(this.ZoomRate, new PointF(cx, cy));

        }


        #region Displayerの登録解除
        /// <summary>
        /// 描画者登録
        /// </summary>
        /// <param name="dp"></param>
        public void AddDisplayer(BaseDisplayer dp)
        {
            this.DisplayerList.Add(dp);

            Type a = dp.GetType();
            BaseDisplayer? minidp = (BaseDisplayer?)Activator.CreateInstance(a);
            if (minidp != null)
            {
                dp.ManageLink = minidp;
                this.clarityViewerMinimapView.AddDisplayer(minidp);
            }
            this.Refresh();
        }

        /// <summary>
        /// 描画者登録解除
        /// </summary>
        /// <param name="dp"></param>
        public void RemoveDisplayer(BaseDisplayer dp)
        {
            this.DisplayerList.Remove(dp);
            this.clarityViewerMinimapView.RemoveDisplayer(dp.ManageLink);

            this.Refresh();

        }
        #endregion


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//        
        /// <summary>
        /// 統一初期化処理
        /// </summary>
        /// <param name="srcimage">元画像 nullで画像無初期化</param>
        /// <param name="size">元サイズ、元画像がある場合は元画像のサイズであること</param>
        private void Init(Image? srcimage, SizeF size)
        {
            //画像の読み込み
            this.SrcImage = null;
            if (srcimage != null)
            {
                this.SrcImage = new Bitmap(srcimage);
            }

            //元画像の表示エリアを確定せる
            this.SrcRect = new RectangleF(0, 0, size.Width, size.Height);

            //minimapの初期化
            this.clarityViewerMinimapView.Init(this.SrcImage, this.SrcRect);
            this.clarityViewerMinimapView.Relocate(this.DispRect);

            //描画サイズを設定
            this.ZoomRate = this.CalcuFitRate();
            this.CalcuViewArea();

            //中心描画指示
            this.MoveCenter();

            //初期化
            GC.Collect();

            this.Refresh();
        }
        /// <summary>
        /// Fitする拡大率を計算
        /// </summary>
        /// <returns></returns>
        private double CalcuFitRate()
        {
            float rx = this.DispRect.Width / this.SrcRect.Width;
            float ry = this.DispRect.Height / this.SrcRect.Height;

            double ans = Math.Min(rx, ry);
            return ans;
        }

        /// <summary>
        /// 画像描画範囲の計算
        /// </summary>
        private void CalcuViewArea()
        {
            double w = this.SrcRect.Width * this.ZoomRate;
            double h = this.SrcRect.Height * this.ZoomRate;
            this.ViewRect = new RectangleF(this.ViewRect.X, this.ViewRect.Y, Convert.ToSingle(w), Convert.ToSingle(h));
        }


        /// <summary>
        /// 現在の拡大率に最もふさわしいindexを取得する
        /// </summary>
        /// <returns></returns>
        private int SeachZoomTableIndex(double zrate)
        {
            double len = double.MaxValue;
            int ans = 0;
            for (int i = 0; i < this.ZoomTable.Length; i++)
            {
                //距離が最小な場所を現在の拡大率とする
                double a = Math.Abs(this.ZoomTable[i] - this.ZoomRate);
                if (a < len)
                {
                    len = a;
                    ans = i;
                }
            }

            return ans;
        }


        /// <summary>
        /// 拡縮率の変更
        /// </summary>
        /// <param name="nrate">移行先拡大率</param>
        /// <param name="dcen">disp座標拡縮中心位置</param>
        private void ChangeZoom(double nrate, PointF dcen)
        {
            float nfrate = Convert.ToSingle(nrate);

            //画像中心を取得
            PointF icp = this.Ivt.DispPointToSrcPoint(dcen);
            PointF vcp = this.Ivt.DispPointToViewPoint(dcen);


            //中心位置を抽象化
            float nrx = icp.X / this.SrcRect.Width;
            float nry = icp.Y / this.SrcRect.Height;

            //新しいサイズの算出
            float nw = this.SrcRect.Width * nfrate;
            float nh = this.SrcRect.Height * nfrate;

            RectangleF rc = new RectangleF(-(nrx * nw)+ dcen.X, -(nry * nh) + dcen.Y, nw, nh);
            this.Ivt.ViewRect = rc;
        }

        /// <summary>
        /// 描画位置を画面中心に設定
        /// </summary>
        private void MoveCenter()
        {
            float vx = this.ViewRect.Width * 0.5f;
            float vy = this.ViewRect.Height * 0.5f;

            this.MoveViewPos(new PointF(vx, vy));
        }

        /// <summary>
        /// Srcの指定値を表示する
        /// </summary>
        /// <param name="srcpos"></param>
        private void MoveSrcPos(PointF srcpos)
        {
            PointF vp = this.Ivt.SrcPointToViewPoint(srcpos);

            this.MoveViewPos(vp);
        }

        /// <summary>
        /// Viewの指定位置を表示する
        /// </summary>
        /// <param name="vpos"></param>
        private void MoveViewPos(PointF vpos)
        {
            float vx = vpos.X;
            float vy = vpos.Y;

            vx -= this.DispRect.Width * 0.5f;
            vy -= this.DispRect.Height * 0.5f;

            this.Ivt.ViewRect.X = -vx;
            this.Ivt.ViewRect.Y = -vy;

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        #region 描画処理
        /// <summary>
        /// 描画処理メイン
        /// </summary>
        /// <param name="gra"></param>
        private void Render(Graphics gra)
        {
            //画像の描画
            this.RenderImage(gra);

            //描画者の表示
            this.DisplayerList.ForEach(x =>
            {
                x.SrcRect = this.SrcRect;
                x.ViewRect = this.ViewRect;
                x.DispRect = this.DispRect;

                x.Render(gra);
            });

            {
                float l = this.Ivt.DispXToSrcX(this.DispRect.Left);
                float t = this.Ivt.DispYToSrcY(this.DispRect.Top);
                float r = this.Ivt.DispXToSrcX(this.DispRect.Right);
                float b = this.Ivt.DispYToSrcY(this.DispRect.Bottom);
                this.clarityViewerMinimapView.DispArea = new RectangleF(l, t, r-l, b-t);
                
                this.clarityViewerMinimapView.Refresh();
            }
            
        }
        /// <summary>
        /// 画像の描画
        /// </summary>
        /// <param name="gra"></param>
        private void RenderImage(Graphics gra)
        {
            if (this.SrcImage == null)
            {
                using (SolidBrush bru = new SolidBrush(this.SrcBackColor))
                {
                    gra.FillRectangle(bru, this.ViewRect);
                }

                return;
            }

            //補完モードの設定
            gra.InterpolationMode = this.ImageInterpolationMode;
            gra.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            
            gra.DrawImage(this.SrcImage, this.ViewRect);

        }



        
        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_Load(object sender, EventArgs e)
        {
            
        }


        /// <summary>
        /// コントロールサイズが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_SizeChanged(object sender, EventArgs e)
        {
            this.DispRect = new RectangleF(0, 0, this.Width, this.Height);
            this.clarityViewerMinimapView.Relocate(this.DispRect);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rcClient = this.DisplayRectangle;
            BufferedGraphicsContext bufContext = BufferedGraphicsManager.Current;
            using (BufferedGraphics buf = bufContext.Allocate(e.Graphics, rcClient))
            {
                Graphics gra = buf.Graphics;
                {
                    //全体クリア
                    gra.Clear(this.ClearColor);

                    //描画処理本体
                    this.Render(gra);

                    //{
                    //    //デバッグで座標表示
                    //    using (SolidBrush sb = new SolidBrush(Color.Red))
                    //    {
                    //        gra.DrawString($"ViewX={this.Ivt.DispXToViewX(this.MInfo.NowPos.X)}" +
                    //            $"ViewY={this.Ivt.DispYToViewY(this.MInfo.NowPos.Y)}" +
                    //            $"ImageX={this.Ivt.DispXToImageX(this.MInfo.NowPos.X)}" +
                    //            $"ImageY={this.Ivt.DispYToImageY(this.MInfo.NowPos.Y)}", new Font("Ariel", 16.0f, FontStyle.Regular), sb, new Point(0, 0));
                    //    }
                    //}
                }
                buf.Render();
            }
        }

        /// <summary>
        /// マウスが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_MouseDown(object sender, MouseEventArgs e)
        {
            this.MInfo.DownMouse(e);
            this.DisplayerList.ForEach(x => x.MouseDown(this.MInfo));
            this.Refresh();
        }

        /// <summary>
        /// マウスが動いた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_MouseMove(object sender, MouseEventArgs e)
        {
            this.MInfo.MoveMouse(e);
            this.DisplayerList.ForEach(x => x.MouseMove(this.MInfo));
            if (e.Button == MouseButtons.Right)
            {
                this.Ivt.ViewRect.Offset(this.MInfo.PrevMoveLength.X, this.MInfo.PrevMoveLength.Y);
                
            }
            this.Refresh();
        }

        /// <summary>
        /// マウスが離されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_MouseUp(object sender, MouseEventArgs e)
        {
            this.MInfo.UpMouse(e);
            this.DisplayerList.ForEach(x => x.MouseUp(this.MInfo));
            this.Refresh();
        }

        /// <summary>
        /// マウスがホイールされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ClarityViewer_MouseWheel(object? sender, MouseEventArgs e)
        {
            this.MInfo.WheelMouse(e);

            bool f = false;
            if (e.Delta > 0)
            {
                f = true;
            }
            this.Zoom(f, this.MInfo.NowPos);
            this.Refresh();
        }

        /// <summary>
        /// ダブルクリックされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && this.DoubleClickCentering == true)
            {
                this.MoveCenter();
                this.Refresh();
            }
        }

        /// <summary>
        /// Minimap位置設定
        /// </summary>
        /// <param name="srcpos"></param>
        private void clarityViewerMinimapView_PositonSelectEvent(PointF srcpos)
        {            
            this.MoveSrcPos(srcpos);
            this.Refresh();
        }
    }
}
