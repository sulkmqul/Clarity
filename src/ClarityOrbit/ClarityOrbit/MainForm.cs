using Clarity.GUI;
using WeifenLuo.WinFormsUI.Docking;

namespace ClarityOrbit
{
    /// <summary>
    /// ���C�����
    /// </summary>
    public partial class MainForm : BaseClarityForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.Logic = new MainFormLogic(this);
            
        }

        //���W�b�N����
        private MainFormLogic Logic;
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
        /// �\�����ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            //�e�[�}��ݒ�
            this.dockPanelToolWindow.Theme = new VS2015LightTheme();
        }

        /// <summary>
        /// ����ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--
        //���j���[����
        /// <summary>
        /// �v���W�F�N�g�̐V�K�쐬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �V�K�쐬ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSettingForm fo = new ProjectSettingForm();
            fo.Init();
            var dret = fo.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //�v���W�F�N�g�V�K�쐬
            OrbitGlobal.Mana.CreateNewProject(fo.Result.TileSize, fo.Result.TileCount);
            //�쐬����������
            this.Logic.InitializeEdit();
            OrbitGlobal.SendEvent(EOrbitEventID.CreateProject);

        }
        /// <summary>
        /// �I���I��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �I��XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}