using OpenCvSharp;
using System.Reactive.Linq;
using Clarity.Cv.Video;
using System;
using System.Reactive.Disposables;


namespace ClarityCameraViewer
{
    /// <summary>
    /// ���C�����
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //�\����̏��׍H
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
        /// �J�����f���擾�Ǘ�
        /// </summary>
        private CameraGrabber Grab = new CameraGrabber();

        /// <summary>
        /// �f���ۑ��Ǘ�
        /// </summary>
        private BaseVideoImageWriter? Writer = null;
        /// <summary>
        /// �f���ۑ��I��
        /// </summary>
        private IDisposable? VideoSaveTask = null;

        /// <summary>
        /// sbscripble�Ǘ�
        /// </summary>
        private CompositeDisposable RxDispo = new CompositeDisposable();

        /// <summary>
        /// ���݂̎擾�p�����[�^
        /// </summary>
        private CameraParam CurrentParam = new CameraParam();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// �f�o�C�X�̏�����
        /// </summary>
        private void DeviceInit(CameraParam param)
        {
            //���������
            this.RxDispo.Dispose();
            this.RxDispo = new CompositeDisposable();

            //�V���O���J����������
            this.Grab.InitSingle(new CameraGrabberInitParam() { 
                CameraIndex = param.DeviceID, 
                Resulution = new System.Drawing.Size(param.Width, param.Height),
                Fps = param.Fps
            });

            //���߂̉f���擾���ɉ�ʂ̏��������s���B
            var idd = this.Grab.CameraGrabSub.Take(1).Subscribe(x =>
            {
                Mat mat = x.CaptureImageList.First();
                var bit = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                this.clarityImageViewer1.Init(bit);
            });
            this.RxDispo.Add(idd);
            

            //�f���\��
            var igdd = this.Grab.CameraGrabSub.Subscribe(x =>
            {
                Mat mat = x.CaptureImageList.First();
                var bit = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat);
                this.clarityImageViewer1.ReplaceImage(bit);
            });
            this.RxDispo.Add(igdd);

            //FPS�\��
            var ifps = this.Grab.CameraGrabFPSSub.Subscribe(x =>
            {
                this.Invoke(() => this.labelFPS.Text = x.ToString(".00"));
            });
            this.RxDispo.Add(ifps);
        }


        /// <summary>
        /// �擾�̊J�n
        /// </summary>
        private void StartGrabber()
        {
            //����̃p�����[�^�擾
            CameraParam para = new CameraParam();
            {
                para.DeviceID = Convert.ToInt32(this.numericUpDownDeviceID.Value);
                para.Width = Convert.ToInt32(this.numericUpDownImageWidth.Value);
                para.Height = Convert.ToInt32(this.numericUpDownImageHeight.Value);
                para.Fps = Convert.ToInt32(this.numericUpDownFPS.Value);
            }


            //�����ƈ�v�����珉�������Ȃ�
            bool pinit = this.CurrentParam.Equals(para);

            //����̃p�����[�^�ۑ�
            this.CurrentParam = para;

            if (pinit == false)
            {
                this.DeviceInit(para);
            }
            

            //�擾�J�n
            this.Grab.StartLoop();
        }

        /// <summary>
        /// �ۑ��̊J�n
        /// </summary>
        /// <param name="savefolderpath">�ۑ��t�H���_�p�X</param>
        /// <param name="movieflag">true=����ۑ� false=�A�ԉ摜</param>
        private void StartRecording(string savefolderpath, bool movieflag = true)
        {
            //�O�̂��߁A�������N���A
            this.Writer?.Dispose();
                        
            if (movieflag == true)
            {
                //����ۑ��̎�
                var mfw = new MovieFileWriter();
                mfw.Init(@$"{savefolderpath}\{DateTime.Now.ToString("yyMMddhhmmssfff")}.mp4", this.CurrentParam.Fps);
                this.Writer = mfw;
            }
            else
            {
                //�摜�ۑ��̎�
                var ifw = new ImageFileWriter();                
                ifw.Init(savefolderpath);
                this.Writer = ifw;
            }

            //����Ă����f���̕ۑ��^�X�N���d�|����
            this.VideoSaveTask = this.Grab.CameraGrabSub.Subscribe(x =>
            {
                this.Writer.PushImage(x.CaptureImageList[0]);
            });
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// �ǂݍ��܂ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// �\�����ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �J�n�{�^���������ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonGrabStart_Click(object sender, EventArgs e)
        {
            try
            {
                //�O�̂��ߊ������~
                await this.Grab.StopLoop();


                this.StartGrabber();
                this.buttonGrabStart.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���s���܂���:{ex}");
            }
        }

        /// <summary>
        /// �I���{�^���������ꂽ�Ƃ�
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
                MessageBox.Show($"���s���܂���:{ex}");
            }

        }

        /// <summary>
        /// ��ʂ�������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //���
            this.RxDispo.Dispose();
            await this.Grab.DisposeAsync();
        }
        
        /// <summary>
        /// /�ۑ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxRec_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //�f���擾���Ă��邩�H
                if (this.Grab.IsGrabbing == false)
                {
                    return;
                }

                if (this.checkBoxRec.Checked)
                {
                    //�ۑ��J�n������

                    //�ۑ��t�H���_�̑I��
                    FolderBrowserDialog diag = new FolderBrowserDialog();
                    diag.SelectedPath = Application.ExecutablePath;
                    var dret = diag.ShowDialog(this);
                    if (dret != DialogResult.OK)
                    {
                        this.checkBoxRec.Checked = false;
                        return;
                    }

                    //�ۑ��J�n
                    this.StartRecording(diag.SelectedPath);

                    this.labelRec.Visible = true;


                }
                else
                {
                    //�����ŕۑ��I��
                    this.VideoSaveTask?.Dispose();
                    this.Writer?.Dispose();
                    this.Writer = null;

                    this.labelRec.Visible = false;
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"���s�F{ex}");
            }
        }

        
    }
}