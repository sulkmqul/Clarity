using Clarity.GUI;
using WeifenLuo.WinFormsUI.Docking;

namespace ClarityOrbit
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : BaseClarityForm
    {
        public MainForm()
        {
            InitializeComponent();
            this.Logic = new MainFormLogic(this);
            
        }

        //ロジック処理
        private MainFormLogic Logic;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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
            //テーマを設定
            this.dockPanelToolWindow.Theme = new VS2015LightTheme();
        }

        /// <summary>
        /// 閉じられた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--
        //メニュー処理
        /// <summary>
        /// プロジェクトの新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProjectSettingForm fo = new ProjectSettingForm();
            fo.Init();
            var dret = fo.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //プロジェクト新規作成
            OrbitGlobal.Mana.CreateNewProject(fo.Result.TileSize, fo.Result.TileCount);
            //作成した初期化
            this.Logic.InitializeEdit();
            OrbitGlobal.SendEvent(EOrbitEventID.CreateProject);

        }
        /// <summary>
        /// 終了選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}