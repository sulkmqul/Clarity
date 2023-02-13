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
                this.clarityDxViewer1.Init();
                this.frameEditControlEditor.Init();
            });
            this.RxClean.Add(dp);

            //�ڍ׏�����
            this.Logic.Init();
        }




        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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

        /// <summary>
        /// �V�K�쐬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �V�K�쐬NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Logic.CreateProject();
        }

        /// <summary>
        /// �v���W�F�N�g�ǂݍ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �J��OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �v���W�F�N�g�ۑ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �ۑ�WToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �m��f�[�^�̏o��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �o��EToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        
    }
}