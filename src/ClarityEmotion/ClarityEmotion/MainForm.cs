using ClarityEmotion.AnimeDefinition;
using System.Configuration;

namespace ClarityEmotion
{

    /// <summary>
    /// ���C�����
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.Logic = new MainFormLogic(this);
            this.ViewMana = new EditViewManager(this.clarityViewer1);
        }

        /// <summary>
        /// 
        /// </summary>
        internal MainFormData FData = new MainFormData();
        internal MainFormLogic Logic;

        internal EditViewManager ViewMana;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ���������ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CeGlobal.Create();

            //�R���g���[���̈ꔭ�ڂ̏�����
            this.layerControl1.Init();
            this.layerSettingControl1.Init();
            this.ViewMana.Init();

        }

        /// <summary>
        /// �v���W�F�N�g�쐬
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �V�K�쐬ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //�V�K�쐬
                NewProjectForm f = new NewProjectForm();
                DialogResult dret = f.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }

                //�v���W�F�N�g���͏��̎擾
                EmotionProjectDataBasic idata = f.GetInputData();
                this.Logic.CreateNewProject(idata);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �J���Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �J��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �ۑ�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �ۑ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// �o�͂���Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �o��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// ����{�^���������ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// �A�j����`�ݒ�{�^���������ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �A�j���[�V������`ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //�A�j����`�ҏW��ʂ̕\��
            AnimeDefinitionEditForm f = new AnimeDefinitionEditForm();
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }
            //���͂̎擾
            var idata = f.GetInputData();
            //��`�̍쐬
            CeGlobal.Project.Anime.CreateAnimeDefinitionDic(idata);
            CeGlobal.Event.SendValueChangeEvent(EEventID.AnimeDefinitionUpdate, CeGlobal.Project.Anime);
        }

        private void �ݒ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ���O�\��ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}