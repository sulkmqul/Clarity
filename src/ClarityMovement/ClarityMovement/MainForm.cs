namespace ClarityMovement
{
    /// <summary>
    /// ���C�����
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �����S��
        /// </summary>
        private MainFormLogic Logic { get; init; } = new MainFormLogic();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ��ʏ�����
        /// </summary>
        private void InitForm()
        {
            //View�̏�����
            this.movementEditViewerControl1.Init();

            //editor�̏�����
            this.movementEditor1.Init();
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        ///�ǂݍ��܂ꂽ��
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
            this.InitForm();
        }

        /// <summary>
        /// ����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //�������
            await this.movementEditViewerControl1.RelaseControl();
            this.movementEditor1.ReleaseControl();
        }


        /// <summary>
        /// �V�K�쐬�������ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void �V�K�쐬NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filepath = "";
            using (OpenFileDialog diag = new OpenFileDialog())
            {
                var dret = diag.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }
                filepath = diag.FileName;
            }

            try
            {
                await this.Logic.CreateNewProject(filepath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("���s");
            }

        }

        /// <summary>
        /// �J���������ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �J��OToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// ���邪�����ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ����XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}