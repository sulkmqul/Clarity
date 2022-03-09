using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Util;
using Clarity.GUI;
using ClarityEmotion.Core;

namespace ClarityEmotion
{
    /// <summary>
    /// 出力処理
    /// </summary>
    public partial class ExportForm : Form
    {
        public ExportForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 入力チェック
        /// </summary>
        /// <returns></returns>
        private List<Control> CheckInputError()
        {
            List<Control> anslist = new List<Control>();
            {
                string fol = this.textBoxExportPath.Text.Trim();
                if (System.IO.Directory.Exists(fol) == false)
                {
                    anslist.Add(this.textBoxExportPath);
                }
            }

            return anslist;
        }

        /// <summary>
        /// 出力処理の実行
        /// </summary>
        /// <returns></returns>
        private async Task ExportAnime()
        {
            //出力パラメータ作成
            EmotionExporterData edata = new EmotionExporterData();
            edata.ExportFolderPath = this.textBoxExportPath.Text.Trim();

            //出力処理
            EmotionExporter ee = new EmotionExporter();
            await ee.ExportEmotion(edata, this.progressBar1);


        }

        //---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//
        //---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//---//
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 出力フォルダ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectFolder_Click(object sender, EventArgs e)
        {
            DialogResult dret = this.folderBrowserDialog1.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //選択パスを入れる
            this.textBoxExportPath.Text = this.folderBrowserDialog1.SelectedPath;
        }

        /// <summary>
        /// 出力ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonExport_Click(object sender, EventArgs e)
        {
            var clist = this.CheckInputError();
            if (clist.Count > 0)
            {
                using (Clarity.GUI.ErrorControlState se = new ErrorControlState(clist))
                {
                    MessageBox.Show("入力に問題があります");
                    return;
                }
            }

            //出力処理の実行
            try
            {
                using (Clarity.GUI.AsyncControlState cs = new AsyncControlState(this, this.progressBar1))
                {
                    await this.ExportAnime();
                }

                MessageBox.Show($"完了");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"出力失敗 mes={ex.Message}");
            }
        }

        /// <summary>
        /// 閉じるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キーが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
