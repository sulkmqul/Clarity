using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            this.FData = new MainFormData();
            this.Logic = new MainFormLogic(this, this.FData);
        }


        private MainFormData FData = null;
        private MainFormLogic Logic = null;


        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void Init()
        {
            //ログ画面の表示
            //Clarity.ClarityLog.ShowLogForm();

            //初期化
            this.Logic.Init();

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        /// <summary>
        /// 何か押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                EmotionProject.Mana.Info.PlayFlag = !EmotionProject.Mana.Info.PlayFlag;
            }
            if (e.KeyCode == Keys.W)
            {
                EmotionProject.Mana.Info.FramePosition = 0;
            }

            if (e.KeyCode == Keys.Escape)
            {
                //Clarity.ClarityLog.Log.Dispose();
                this.Close();
            }
            
        }

        /// <summary>
        /// 閉じられた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Logic.Release();
        }

        /// <summary>
        /// 新規作成メニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm f = new NewProjectForm();
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //入力を取得し作成
            var fd = f.GetInputData();
            this.Logic.CreateNewProject(fd);
        }

        /// <summary>
        /// 閉じるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 閉じるToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// アニメ定義メニュー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void アニメ定義ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Logic.SetupAnimeDefinitionEditForm();

            
        }

        /// <summary>
        /// プロジェクトファイルを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dret = this.openFileDialog1.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            try
            {
                string pfpath = this.openFileDialog1.FileName;
                this.Logic.LoadProject(pfpath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み失敗");
            }
        }

        /// <summary>
        /// プロジェクトファイルを保存する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dret = this.saveFileDialog1.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            try
            {
                string sfpath = this.saveFileDialog1.FileName;
                EmotionProject.Mana.SaveProject(sfpath);
            }
            catch (Exception ex)
            {                
                MessageBox.Show("保存失敗");
            }
        }

        /// <summary>
        /// 出力ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 出力ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportForm f = new ExportForm();
            f.ShowDialog(this);
        }

        /// <summary>
        /// アニメ設定ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AnimeSettingForm f = new AnimeSettingForm();
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //設定更新
            this.Logic.ChangeAnimeSetting();
        }

        /// <summary>
        /// レイヤーの初期化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void layer初期化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Logic.InitLayer();
        }
    }
}

