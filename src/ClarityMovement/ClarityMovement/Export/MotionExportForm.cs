using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Export
{
    /// <summary>
    /// 出力設定画面
    /// </summary>
    internal partial class MotionExportForm : Form
    {
        public MotionExportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 書き込み管理
        /// </summary>
        private MotionDataWriter? MWriter = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面の初期化
        /// </summary>
        /// <param name="project"></param>
        public void Init(CmProject project)
        {
            this.MWriter = new MotionDataWriter(project);

            //デバッグ
            this.textBoxExportFilePath.Text = @"F:\作業領域\Game\Clarity\src\ClarityMovement\data.zip";
            this.textBoxMotionCode.Text = @"testmotion";

        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        private void CheckError()
        {
            if (this.textBoxExportFilePath.Text.Length <= 0)
            {
                throw new InvaildInputException("filepath", this.textBoxExportFilePath);
            }
            if (this.textBoxMotionCode.Text.Length <= 0)
            {
                throw new InvaildInputException("motion code", this.textBoxMotionCode);
            }
        }

        /// <summary>
        /// 出力処理本体
        /// </summary>
        private void Export()
        {

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MotionExportForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 出力パス選択ボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectExportFile_Click(object sender, EventArgs e)
        {
            //保存先の選択
            SaveFileDialog diag = new SaveFileDialog();
            diag.DefaultExt = ".zip";
            diag.AddExtension = true;
            diag.Filter = "zip|*.zip|all|*.*";
            var dret = diag.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }
            this.textBoxExportFilePath.Text = diag.FileName;
        }

        /// <summary>
        /// okボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                //入力チェック
                this.CheckError();

                this.DialogResult = DialogResult.OK;
            }
            catch (InvaildInputException iex)
            {
                using (ErrorControlState st = new ErrorControlState(iex))
                {
                    MessageBox.Show("入力に問題があります");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"失敗:{ex.Message}");
            }

        }

        /// <summary>
        /// cancelボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
