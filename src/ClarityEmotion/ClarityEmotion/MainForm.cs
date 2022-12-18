using ClarityEmotion.AnimeDefinition;
using System.Configuration;

namespace ClarityEmotion
{

    /// <summary>
    /// メイン画面
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
        /// 初期化された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            CeGlobal.Create();

            //コントロールの一発目の初期化
            this.layerControl1.Init();
            this.layerSettingControl1.Init();
            this.ViewMana.Init();

        }

        /// <summary>
        /// プロジェクト作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //新規作成
                NewProjectForm f = new NewProjectForm();
                DialogResult dret = f.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }

                //プロジェクト入力情報の取得
                EmotionProjectDataBasic idata = f.GetInputData();
                this.Logic.CreateNewProject(idata);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 開くとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 保存するとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 出力するとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 閉じるボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// アニメ定義設定ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void アニメーション定義ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アニメ定義編集画面の表示
            AnimeDefinitionEditForm f = new AnimeDefinitionEditForm();
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }
            //入力の取得
            var idata = f.GetInputData();
            //定義の作成
            CeGlobal.Project.Anime.CreateAnimeDefinitionDic(idata);
            CeGlobal.Event.SendValueChangeEvent(EEventID.AnimeDefinitionUpdate, CeGlobal.Project.Anime);
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ログ表示ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}