using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Clarity.GUI;

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
        /// <summary>
        /// 処理
        /// </summary>
        private MainFormLogic Logic;


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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
            this.Logic.InitForm();
        }

        /// <summary>
        /// DirectXのサイズ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelWorkingDx_Resize(object sender, EventArgs e)
        {

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 新規作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //設定画面の作成
            ProjectSettingForm f = new ProjectSettingForm();
            f.Init();
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }


            //新規作成処理をここで行う
            this.Logic.CreateNew(f.Result);
        }
    }
}
