using Clarity.GUI;
using ClarityMovement.Export;
using System.DirectoryServices.ActiveDirectory;
using System.Reactive.Disposables;

namespace ClarityMovement
{
    /// <summary>
    /// ���C�����
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //���W�b�N�̍쐬
            this.Logic = new MainFormLogic(this);
        }

        /// <summary>
        /// ���logic
        /// </summary>
        private MainFormLogic Logic;


        private CompositeDisposable RxClean = new CompositeDisposable();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ��ʏ�����
        /// </summary>
        private void InitForm()
        {
            //clarityengine�̏�����
            this.clarityDxViewer1.Init();

            //�v���W�F�N�g�ǂݍ��ݐ���
            var dp = CmGlobal.Project.Subscribe(x =>
            {
                if (x == null)
                {
                    this.clarityDxViewer1.Clear();
                    this.clarityDxViewer1.Visible = false;
                    this.frameEditControlEditor.Visible = false;
                    return;
                }

                //������
                this.clarityDxViewer1.Visible = true;
                this.frameEditControlEditor.Visible = true;                
                this.frameEditControlEditor.Init();
            });
            this.RxClean.Add(dp);

            //�ڍ׏�����
            this.Logic.Init();
        }




        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// �ǂݍ��܂ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ���߂ɉ�ʂ��\�����ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            //�S�̏�����
            this.InitForm();
        }

        /// <summary>
        /// ��ʂ�������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.RxClean.Dispose();
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// �V�K�쐬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �V�K�쐬NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateProjectForm f = new CreateProjectForm();
                DialogResult dret = f.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }

                //�v���W�F�N�g���̎擾
                var idata = f.GetInputData();
                CmProject proj = new CmProject();

                proj.RenderingSize = idata.Size;
                proj.FrameRate = idata.FPS;
                proj.MaxFrame = idata.MaxFrame;

                this.Logic.CreateProject(proj);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���s:{ex}");
            }
        }

        /// <summary>
        /// �v���W�F�N�g�ǂݍ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �J��OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string filepath = @"F:\��Ɨ̈�\Game\Clarity\src\ClarityMovement\movementproj.xml";
                //�ǂݍ��݃t�@�C���̑I��
                //OpenFileDialog diag = new OpenFileDialog();
                //var dret = diag.ShowDialog(this);
                //if (dret != DialogResult.OK)
                //{
                //    return;
                //}
                //filepath = diag.FileName;

                //�f�[�^�̓ǂݍ���
                this.Logic.LoadProject(filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"���s�F{ex}");
            }
        }

        /// <summary>
        /// �v���W�F�N�g�ۑ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void �ۑ�WToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmGlobal.Project.Value == null)
                {
                    throw new Exception("Project not found");
                }

                
                string filepath = @"F:\��Ɨ̈�\Game\Clarity\src\ClarityMovement\movementproj.xml";

                ////�ۑ��t�@�C���̑I��
                //SaveFileDialog diag = new SaveFileDialog();
                //var dret = diag.ShowDialog(this);
                //if(dret != DialogResult.OK)
                //{
                //    return;
                //}
                //filepath = diag.FileName;

                using (AsyncControlState aw = new AsyncControlState(this))
                {
                    //�v���W�F�N�g�̕ۑ�
                    await this.Logic.SaveProject(CmGlobal.Project.Value, filepath);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"���s�F{ex}");
            }
        }

        /// <summary>
        /// �m��f�[�^�̏o��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �o��EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (CmGlobal.Project.Value == null)
                {
                    throw new Exception("Project not found");
                }
                //CmProject proj = CmGlobal.Project.Value;
                //this.Logic.ExportMotionFile(@"F:\��Ɨ̈�\Game\Clarity\src\ClarityMovement\mdata.xml", proj);

                //�ҏW�̔ɉh
                this.frameEditControlEditor.ApplyTagModifier();

                //�o�͉�ʂ̋N��
                MotionExportForm f = new MotionExportForm();
                f.Init(CmGlobal.Project.Value);
                f.ShowDialog(this);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"���s:{ex}");
            }
        }

        /// <summary>
        /// ����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ����XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// �摜�I��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �t���[���摜�ǉ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SrcImageSelectForm f = new SrcImageSelectForm(true);
            var dret = f.ShowDialog(this);
        }
        /// <summary>
        /// �t���[���摜�̊��蓖��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �t���[���摜���蓖��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var prj = CmGlobal.Project.Value;
                if (prj == null)
                {
                    throw new Exception("�v���W�F�N�g���ǂݍ��܂�Ă��܂���");
                }

                SrcImageAllocateForm f = new SrcImageAllocateForm();
                var dret = f.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }

                //�v���W�F�N�g�̊m��
                f.ApplyState(prj);
                //�\���̍��ς�
                this.frameEditControlEditor.RecreateTagPaint();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"���s:{ex}");
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //EditorTool����
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonEditZoomPlus_Click(object sender, EventArgs e)
        {
            this.frameEditControlEditor.Zoom(true);
        }

        private void toolStripButtonEditZoomMinus_Click(object sender, EventArgs e)
        {
            this.frameEditControlEditor.Zoom(false);
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    }
}