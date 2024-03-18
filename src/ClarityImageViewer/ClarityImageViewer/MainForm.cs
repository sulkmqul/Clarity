using Clarity.GUI;
using Clarity.Image.PNG;
using ClarityImageViewer.Viewer;
using System.Reactive.Disposables;

namespace ClarityImageViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Subject管理
        /// </summary>
        private CompositeDisposable CompDisp = new CompositeDisposable();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            //情報は閉じておく
            this.ChangeVisibleInfo(false);

            //Viewerの初期化
            this.dxControl1.Init();

            //Eventを仕掛ける
            var t = this.dxControl1.EventSubject.Subscribe(x =>
            {
                //画像読込が終わった時
                if (x.Item1 == Viewer.DxControlEventID.ImageLoadFinished)
                {
                    var info = x.Item2 as ImageInfo;
                    if (info != null)
                    {
                        this.DispImageInfo(info);
                    }
                }

                //サイズが変更された時
                if (x.Item1 == Viewer.DxControlEventID.ScaleChanged && x.Item2 != null)
                {
                    this.DispScaleRate((float)x.Item2);
                }

            });
            this.CompDisp.Add(t);
        }

        /// <summary>
        /// 画面解放
        /// </summary>
        private async Task ReleaseForm()
        {
            //購読停止
            this.CompDisp.Dispose();

            //解放
            await this.dxControl1.RelaseControl();
        }


        /// <summary>
        /// 拡大率の表示
        /// </summary>
        /// <param name="rate"></param>
        private void DispScaleRate(float rate)
        {
            float f = rate * 100.0f;

            this.labelScaleRate.Text = $"{f:f0}%";
        }

        /// <summary>
        /// 画像情報の表示
        /// </summary>
        /// <param name="info"></param>
        private void DispImageInfo(ImageInfo info)
        {
            this.textBoxImageSize.Text = $"{info.ImageSize.Width} x {info.ImageSize.Height}";
            this.textBoxFrameCount.Text = $"{info.FrameCount}";
            this.textBoxInfomation.Text = info.Infomation;
        }

        /// <summary>
        /// infoの表示状態設定
        /// </summary>
        /// <param name="v"></param>
        private void ChangeVisibleInfo(bool v)
        {
            this.panelInfo.Visible = v;
            if (v == true)
            {
                this.buttonInfoHiding.Text = "▶";
            }
            else
            {
                this.buttonInfoHiding.Text = "◀";
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 表示された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.InitForm();

        }

        /// <summary>
        /// 閉じられるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*e.Cancel = true;
            await this.ReleaseForm();
            e.Cancel = false;*/
        }

        /// <summary>
        /// 閉じられた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Task a = this.ReleaseForm();
            GuiUitl.WaitTaskEndSync(a);
        }


        /// <summary>
        /// 実サイズで表示ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScaleOriginal_Click(object sender, EventArgs e)
        {
            this.dxControl1.ChangeActuallySize();
        }

        /// <summary>
        /// 画面fit表示ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonScaleFit_Click(object sender, EventArgs e)
        {
            this.dxControl1.FitImage();
        }

        /// <summary>
        /// 情報ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonInfoHiding_Click(object sender, EventArgs e)
        {
            bool f = !this.panelInfo.Visible;
            this.ChangeVisibleInfo(f);
        }

        /// <summary>
        /// ドラッグアンドドロップ受け入れ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dxControl1_DragEnter(object sender, DragEventArgs e)
        {
            //Fileの場合のみ許可
            bool f = e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false;
            if (f == true)
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// ドラッグアンドドロップ 処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void dxControl1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //FileDropのみ受け付け
                bool f = e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false;
                if (f == false)
                {
                    return;
                }

                string[]? svec = (string[]?)e.Data?.GetData(DataFormats.FileDrop);
                if (svec == null)
                {
                    return;
                }

                await this.dxControl1.LoadImage(svec[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み失敗");
            }
        }

        /// <summary>
        /// 画像の回転処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRotateImage_Click(object sender, EventArgs e)
        {
            this.dxControl1.RotateImage(true);
        }
    }
}