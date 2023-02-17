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
    /// <summary>
    /// ClarityViewerのminimap管理
    /// </summary>    
    [ToolboxItem(false)]
    partial class ClarityImageViewerMinimap : UserControl
    {
        public ClarityImageViewerMinimap()
        {
            InitializeComponent();
        }

        public delegate void PositonSelectDelegate(PointF srcpos);

        [Description("位置選択イベント")]
        public event PositonSelectDelegate? PositonSelectEvent = null;

        private const string CategoryName = "Minimap";


        [Category(CategoryName)]
        [Description("表示エリア背景色")]
        public Color SrcBackColor { get; set; } = Color.White;

        [Category(CategoryName)]
        [Description("表示基準比")]
        public float MinimapSizeRate { get; set; } = 0.3f;

        [Category(CategoryName)]
        [Description("描画位置基準比")]
        public float MinimapDisplayMerginRate { get; set; } = 0.05f;

        [Category(CategoryName)]
        [Description("表示エリア描画色")]
        public Color DisplayAreaLineColor { get; set; } = Color.Red;

        [Category(CategoryName)]
        [Description("表示エリア線幅")]
        public float DisplayAreaLineWidth { get; set; } = 1.0f;

        [Category(CategoryName)]
        [Description("登録Displayer描画可否")]
        public bool DisplayerRendering { get; set; } = true;

        [Category(CategoryName)]
        [Description("境界線描画可否")]
        public bool BorderLineRendering { get; set; } = false;

        [Category(CategoryName)]
        [Description("境界線描画色")]
        public Color BorderLineColor { get; set; } = Color.Red;



        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RectangleF SrcRect
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

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RectangleF DispRect
        {
            get
            {
                return this.Ivt.DispRect;
            }
            set
            {
                this.Ivt.DispRect = value;
                this.Ivt.ViewRect = value;
            }
        }

        /// <summary>
        /// 描画矩形(SrcRect基準)
        /// </summary>
        public RectangleF DispArea = new RectangleF();


        /// <summary>
        /// 描画画像
        /// </summary>
        private Image? SrcImage = null;

        /// <summary>
        /// 描画処理
        /// </summary>
        private ImageViewerTranslator Ivt = new ImageViewerTranslator();

        /// <summary>
        /// 実際に描画した範囲(当たり判定用)
        /// </summary>
        private RectangleF RenderRect;

        /// <summary>
        /// マウス管理
        /// </summary>
        private MouseInfo MInfo = new MouseInfo();


        /// <summary>
        /// 描画者
        /// </summary>
        private List<BaseDisplayer> DisplayerList = new List<BaseDisplayer>();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="srcimage">表示画像</param>
        /// <param name="srcrect">表示画像範囲</param>
        public void Init(Image? srcimage, RectangleF srcrect)
        {
            this.SrcImage = null;
            if (srcimage != null)
            {
                this.SrcImage = new Bitmap(srcimage);
                this.pictureBoxMinimap.Image = this.SrcImage;
            }
            this.SrcRect = srcrect;

            //描画設定
            this.pictureBoxMinimap.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        /// <summary>
        /// 画像の変更
        /// </summary>
        /// <param name="image"></param>
        public void ReplaceImage(Image image)
        {
            //非表示の場合は負荷をかけない
            if (this.Visible == false)
            {
                return;
            }

            this.SrcImage = image;
            this.pictureBoxMinimap.Image = this.SrcImage;            
        }

        /// <summary>
        /// 再配置
        /// </summary>
        /// <param name="pdisprect">元表示の基準矩形</param>
        public void Relocate(RectangleF pdisprect)
        {
            if (this.SrcRect.Width <= 0 || this.SrcRect.Height <= 0)
            {
                return;
            }

            //基準サイズを算出(縦横小さい方を基準にする)
            float basesize = Math.Min(pdisprect.Width * this.MinimapSizeRate, pdisprect.Height * this.MinimapSizeRate);

            //基準倍率を算出
            float wzrate = basesize / this.SrcRect.Width;
            float hzrate = basesize / this.SrcRect.Height;
            float zrate = Math.Min(wzrate, hzrate);

            //新しいサイズと位置を算出
            this.Width = Convert.ToInt32(this.SrcRect.Width * zrate);
            this.Height = Convert.ToInt32(this.SrcRect.Height * zrate);
                        
            this.Left = Convert.ToInt32((pdisprect.Right - this.Width) - (pdisprect.Width * this.MinimapDisplayMerginRate));
            this.Top = Convert.ToInt32((pdisprect.Bottom - this.Height) - (pdisprect.Height * this.MinimapDisplayMerginRate));
        }



        #region Displayerの登録解除
        /// <summary>
        /// 描画者登録
        /// </summary>
        /// <param name="dp"></param>
        public void AddDisplayer(BaseDisplayer dp)
        {
            this.DisplayerList.Add(dp);
        }

        /// <summary>
        /// 描画者登録解除
        /// </summary>
        /// <param name="dp"></param>
        public void RemoveDisplayer(BaseDisplayer dp)
        {
            this.DisplayerList.Remove(dp);
        }

        /// <summary>
        /// 描画者登録全解除
        /// </summary>
        public void ClearDisplayer()
        {
            this.DisplayerList.Clear();
        }
        #endregion


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="gra">描画者</param>
        private void RenderMain(Graphics gra)
        {
            //クリア
            if (this.SrcImage == null)
            {
                gra.Clear(this.SrcBackColor);
            }

            //カスタム描画
            if (this.DisplayerRendering == true)
            {
                this.DisplayerList.ForEach(x =>
                {
                    x.SrcRect = this.SrcRect;
                    x.ViewRect = this.DispRect;
                    x.DispRect = this.DispRect;
                    x.Render(gra);
                });
            }

            //矩形の描画
            using (Pen pe = new Pen(this.DisplayAreaLineColor, this.DisplayAreaLineWidth))
            {
                float x = this.Ivt.SrcXToDispX(this.DispArea.Left);
                float y = this.Ivt.SrcYToDispY(this.DispArea.Top);
                float w = this.Ivt.SrcXToDispX(this.DispArea.Width);
                float h = this.Ivt.SrcYToDispY(this.DispArea.Height);

                //描画矩形を保存
                this.RenderRect = new RectangleF(x, y, w, h);

                gra.DrawRectangle(pe, x, y, w, h);
            }

            //境界線の描画
            if (this.BorderLineRendering == true)
            {
                using (Pen pe = new Pen(this.BorderLineColor, 1.0f))
                {
                    Rectangle brc = new Rectangle(0, 0, this.Size.Width-1, this.Size.Height -1);
                    gra.DrawRectangle(pe, brc);
                }
            }



        }
        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewerMinimap_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// サイズが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClarityViewerMinimap_SizeChanged(object sender, EventArgs e)
        {
            this.DispRect = new RectangleF(0, 0, this.Width, this.Height);
        }

        /// <summary>
        /// 描画されるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMinimap_Paint(object sender, PaintEventArgs e)
        {
            this.RenderMain(e.Graphics);
        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMinimap_MouseDown(object sender, MouseEventArgs e)
        {
            this.MInfo.DownMouse(e);

            //当たり判定
            bool f = this.RenderRect.Contains(this.MInfo.DownPos);            
            this.MInfo.SetMemory(f, 0);
            this.MInfo.SetMemory(this.RenderRect, 1);
            if (f == false)
            {
                PointF spos = this.Ivt.DispPointToSrcPoint(this.MInfo.DownPos);
                this.PositonSelectEvent?.Invoke(spos);
                
            }            
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMinimap_MouseMove(object sender, MouseEventArgs e)
        {
            this.MInfo.MoveMouse(e);

            //矩形に当たっていた
            bool f = this.MInfo.GetMemory<bool>(0);
            if (f == true && this.MInfo.DownFlag == true)
            {
                RectangleF dpos = this.MInfo.GetMemory<RectangleF>(1);

                //矩形中心位置を算出                
                float dcx = dpos.Left + this.MInfo.DownLength.X + (dpos.Width * 0.5f);
                float dcy = dpos.Top + this.MInfo.DownLength.Y + (dpos.Height * 0.5f);

                PointF spos = new PointF();
                spos.X = this.Ivt.DispXToSrcX(dcx);
                spos.Y = this.Ivt.DispYToSrcY(dcy);

                this.PositonSelectEvent?.Invoke(spos);
            }
        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxMinimap_MouseUp(object sender, MouseEventArgs e)
        {
            this.MInfo.UpMouse(e);            
        }
    }
}
