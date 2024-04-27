using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainSrcArranger.ArrangeEditor
{
    public partial class ArrangeEditor : UserControl
    {
        public ArrangeEditor()
        {
            InitializeComponent();
        }

        public double ZoomRate
        {
            get
            {
                return this.clarityImageViewer1.ZoomRate;
            }
        }

        /// <summary>
        /// 切り出しサイズ
        /// </summary>
        private Size CutSize = new Size(1, 1);

        /// <summary>
        /// 描画者
        /// </summary>
        private AreaDisplayer Adisp = new AreaDisplayer(new Size(1, 1));

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="cutsize"></param>
        public void Init(Size cutsize)
        {

            this.CutSize = new Size(cutsize.Width, cutsize.Height);


            this.Adisp = new AreaDisplayer(this.CutSize);
            //this.Adisp.ApplyAreaEvent += Adisp_ApplyAreaEvent;

            //描画者登録
            this.clarityImageViewer1.ClearDisplayer();
            this.clarityImageViewer1.AddDisplayer(this.Adisp);

        }

        /// <summary>
        /// 等倍表示
        /// </summary>
        public void SetFitSize()
        {
            this.clarityImageViewer1.FitImage();
        }
        /// <summary>
        /// 等倍表示
        /// </summary>
        public void SetSameSize()
        {
            this.clarityImageViewer1.SetSameSize();
        }


        /// <summary>
        /// 読込
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadImage(string filepath)
        {
            this.clarityImageViewer1.Init(filepath);

        }

        /// <summary>
        /// 確定処理
        /// </summary>
        /// <param name="area"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ApplyArea(RectangleF srcarea, bool nextimage)
        {
            var image = this.clarityImageViewer1.SrcImage;
            if (image == null)
            {
                return;
            }

            //切り出し処理
            Bitmap bit = this.CutArrangeImage(srcarea);

            //通知
            TsGlobal.NextProcSub.OnNext(new NextProcData(bit, nextimage));
        }


        private Bitmap CutArrangeImage(RectangleF srcarea)
        {
            if (this.clarityImageViewer1.SrcImage == null)
            {
                throw new Exception("NoSrcImage");
            }
            Image srcimage = this.clarityImageViewer1.SrcImage;

            Bitmap bit = new Bitmap(this.CutSize.Width, this.CutSize.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics gra = Graphics.FromImage(bit))
            {
                RectangleF destrect = new RectangleF(0, 0, bit.Width, bit.Height);
                gra.DrawImage(srcimage, destrect, srcarea, GraphicsUnit.Pixel);
            }

            return bit;
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ArrangeEditor_Load(object sender, EventArgs e)
        {

        }

        private void clarityImageViewer1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            {
                var rc = this.Adisp.GetSelectSrcArea();
                this.ApplyArea(rc, true);
            }
            if (e.KeyCode == Keys.A)
            {
                var rc = this.Adisp.GetSelectSrcArea();
                this.ApplyArea(rc, false);
            }
        }

        private void clarityImageViewer1_ClarityViewerZoomChangedEvent(double rate)
        {
            TsGlobal.EventSub.OnNext(EControlEvent.ZoomChanged);
        }
    }
}
