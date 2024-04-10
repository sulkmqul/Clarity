namespace ClarityMovement
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 処理担当
        /// </summary>
        private MainFormLogic Logic { get; init; } = new MainFormLogic();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //Viewの初期化
            this.movementEditViewerControl1.Init();

            //editorの初期化
            this.movementEditor1.Init();
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        ///読み込まれた時
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
        /// 閉じるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //解放処理
            await this.movementEditViewerControl1.RelaseControl();
            this.movementEditor1.ReleaseControl();
        }


        /// <summary>
        /// 新規作成が押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
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
                MessageBox.Show("失敗");
            }

        }

        /// <summary>
        /// 開くが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 閉じるが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 閉じるXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}