using OpenCvSharp;
using System.Reactive.Linq;
using Clarity.Cv.Video;
using System;
using System.Reactive.Disposables;


namespace ClarityCameraViewer
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //表示上の小細工
            this.labelRec.Parent = this.clarityImageViewer1;
        }

        class CameraParam
        {
            public int DeviceID = -1;
            public int Width = -1;
            public int Height = -1;
            public int Fps = -1;

            public override bool Equals(object? obj)
            {
                CameraParam? data = obj as CameraParam;
                if (data == null)
                {
                    return false;
                }

                if (data.DeviceID != this.DeviceID ||
                    data.Width != this.Width ||
                    data.Height != this.Height ||
                    data.Fps != this.Fps)
                {
                    return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// カメラ映像取得管理
        /// </summary>
        private CameraGrabber Grab = new CameraGrabber();

        /// <summary>
        /// 映像保存管理
        /// </summary>
        private BaseVideoImageWriter? Writer = null;
        /// <summary>
        /// 映像保存終了
        /// </summary>
        private IDisposable? VideoSaveTask = null;

        /// <summary>
        /// sbscripble管理
        /// </summary>
        private CompositeDisposable RxDispo = new CompositeDisposable();

        /// <summary>
        /// 現在の取得パラメータ
        /// </summary>
        private CameraParam CurrentParam = new CameraParam();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// デバイスの初期化
        /// </summary>
        private void DeviceInit(CameraParam param)
        {
            //既存を解放
            this.RxDispo.Dispose();
            this.RxDispo = new CompositeDisposable();

            //シングルカメラ初期化
            this.Grab.InitSingle(new CameraGrabberInitParam() { 
                CameraIndex = param.DeviceID, 
                Resulution = new System.Drawing.Size(param.Width, param.Height),
                Fps = param.Fps
            });

            //初めの映像取得時に画面の初期化を行う。
            var idd = this.Grab.CameraGrabSub.Take(1).Subscribe(x =>
            {
                Mat mat = x.CaptureImageList.First();
                var bit = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                this.clarityImageViewer1.Init(bit);
            });
            this.RxDispo.Add(idd);
            

            //映像表示
            var igdd = this.Grab.CameraGrabSub.Subscribe(x =>
            {
                Mat mat = x.CaptureImageList.First();
                var bit = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                this.clarityImageViewer1.ReplaceImage(bit);
            });
            this.RxDispo.Add(igdd);

            //FPS表示
            var ifps = this.Grab.CameraGrabFPSSub.Subscribe(x =>
            {
                this.Invoke(() => this.labelFPS.Text = x.ToString(".00"));
            });
            this.RxDispo.Add(ifps);
        }


        /// <summary>
        /// 取得の開始
        /// </summary>
        private void StartGrabber()
        {
            //今回のパラメータ取得
            CameraParam para = new CameraParam();
            {
                para.DeviceID = Convert.ToInt32(this.numericUpDownDeviceID.Value);
                para.Width = Convert.ToInt32(this.numericUpDownImageWidth.Value);
                para.Height = Convert.ToInt32(this.numericUpDownImageHeight.Value);
                para.Fps = Convert.ToInt32(this.numericUpDownFPS.Value);
            }


            //既存と一致したら初期化しない
            bool pinit = this.CurrentParam.Equals(para);

            //今回のパラメータ保存
            this.CurrentParam = para;

            if (pinit == false)
            {
                this.DeviceInit(para);
            }
            

            //取得開始
            this.Grab.StartLoop();
        }

        /// <summary>
        /// 保存の開始
        /// </summary>
        /// <param name="savefolderpath">保存フォルダパス</param>
        /// <param name="movieflag">true=動画保存 false=連番画像</param>
        private void StartRecording(string savefolderpath, bool movieflag = true)
        {
            //念のため、既存をクリア
            this.Writer?.Dispose();
                        
            if (movieflag == true)
            {
                //動画保存の時
                var mfw = new MovieFileWriter();
                mfw.Init(@$"{savefolderpath}\{DateTime.Now.ToString("yyMMddhhmmssfff")}.mp4", this.CurrentParam.Fps);
                this.Writer = mfw;
            }
            else
            {
                //画像保存の時
                var ifw = new ImageFileWriter();                
                ifw.Init(savefolderpath);
                this.Writer = ifw;
            }

            //流れてきた映像の保存タスクを仕掛ける
            this.VideoSaveTask = this.Grab.CameraGrabSub.Subscribe(x =>
            {
                this.Writer.PushImage(x.CaptureImageList[0]);
            });
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれたとき
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

        }

        /// <summary>
        /// 開始ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonGrabStart_Click(object sender, EventArgs e)
        {
            try
            {
                //念のため既存を停止
                await this.Grab.StopLoop();


                this.StartGrabber();
                this.buttonGrabStart.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"失敗しました:{ex}");
            }
        }

        /// <summary>
        /// 終了ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonGrabStop_Click(object sender, EventArgs e)
        {
            try
            {
                await this.Grab.StopLoop();
                this.buttonGrabStart.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"失敗しました:{ex}");
            }

        }

        /// <summary>
        /// 画面が閉じられるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //解放
            this.RxDispo.Dispose();
            await this.Grab.DisposeAsync();
        }
        
        /// <summary>
        /// /保存処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxRec_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //映像取得しているか？
                if (this.Grab.IsGrabbing == false)
                {
                    return;
                }

                if (this.checkBoxRec.Checked)
                {
                    //保存開始だった

                    //保存フォルダの選択
                    FolderBrowserDialog diag = new FolderBrowserDialog();
                    diag.SelectedPath = Application.ExecutablePath;
                    var dret = diag.ShowDialog(this);
                    if (dret != DialogResult.OK)
                    {
                        this.checkBoxRec.Checked = false;
                        return;
                    }

                    //保存開始
                    this.StartRecording(diag.SelectedPath);

                    this.labelRec.Visible = true;


                }
                else
                {
                    //ここで保存終了
                    this.VideoSaveTask?.Dispose();
                    this.Writer?.Dispose();
                    this.Writer = null;

                    this.labelRec.Visible = false;
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"失敗：{ex}");
            }
        }

        
    }
}