using System.Reactive.Disposables;

namespace ClarityMovement
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //ロジックの作成
            this.Logic = new MainFormLogic(this);
        }

        /// <summary>
        /// 画面logic
        /// </summary>
        private MainFormLogic Logic;


        private CompositeDisposable RxClean = new CompositeDisposable();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //プロジェクト読み込み制御
            var dp = CmGlobal.Project.Subscribe(x =>
            {
                if (x == null)
                {
                    this.clarityDxViewer1.Clear();
                    this.clarityDxViewer1.Visible = false;
                    this.frameEditControlEditor.Visible = false;                    
                    return;
                }

                //初期化
                this.clarityDxViewer1.Visible = true;
                this.frameEditControlEditor.Visible = true;
                this.clarityDxViewer1.Init();
                this.frameEditControlEditor.Init();
            });
            this.RxClean.Add(dp);

            //詳細初期化
            this.Logic.Init();
        }




        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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
        /// 初めに画面が表示された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            //全体初期化
            this.InitForm();
        }

        /// <summary>
        /// 画面が閉じられるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.RxClean.Dispose();
        }

        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新規作成NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Logic.CreateProject();
        }

        /// <summary>
        /// プロジェクト読み込み
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 開くOToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// プロジェクト保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存WToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 確定データの出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 出力EToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 閉じるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 閉じるXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 画像選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void フレーム画像追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SrcImageSelectForm f = new SrcImageSelectForm(true);
            var dret = f.ShowDialog(this);
        }

        
    }
}