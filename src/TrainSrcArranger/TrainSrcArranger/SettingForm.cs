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

namespace TrainSrcArranger
{
    /// <summary>
    /// 設定画面
    /// </summary>
    public partial class SettingForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="inputpath"></param>
        public SettingForm(string inputpath = "")
        {
            InitializeComponent();

            this.textBoxInputFolder.Text = inputpath;


            this.textBoxOutputFolder.Text = AppConfig.Mana.InitialOutputFolder;
        }


        public ArrangeData? InputData { get; private set; } = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 入力データの取得
        /// </summary>
        /// <returns></returns>
        public async Task<ArrangeData?> GetInputData()
        {
            List<Control> clist = new List<Control>();

            //入力
            string inpath = this.textBoxInputFolder.Text.Trim();
            bool iret = Directory.Exists(inpath);
            if (iret == false)
            {
                clist.Add(this.textBoxInputFolder);
            }

            //出力
            string outpath = this.textBoxOutputFolder.Text.Trim();
            bool oret = Directory.Exists(outpath);
            if (oret == false)
            {
                clist.Add(this.textBoxOutputFolder);
            }

            int w = (int)this.numericUpDownCutSizeWidth.Value;
            int h = (int)this.numericUpDownCutSizeHeight.Value;
            if (w <= 0 || h <= 0)
            {
                clist.Add(this.numericUpDownCutSizeWidth);
                clist.Add(this.numericUpDownCutSizeHeight);
            }

            int startindex = (int)this.numericUpDownStartIndex.Value;

            if (clist.Count > 0)
            {
                using (ErrorControlState st = new ErrorControlState(clist))
                {
                    MessageBox.Show("入力に問題があります。");
                }
                return null;
            }

            //データがない時はエラーとする            
            var inlist = await TsGlobal.ListupProcFiles(inpath);
            if (inlist.Count <= 0)
            {
                MessageBox.Show("処理対象が存在しません。");
                return null;
            }



            ArrangeData ans = new ArrangeData(inlist, outpath, w, h, startindex);

            return ans;
        }

        //--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.numericUpDownCutSizeWidth.Value = AppConfig.Mana.InitialCutSize.Width;
            this.numericUpDownCutSizeHeight.Value = AppConfig.Mana.InitialCutSize.Height;
        }

        /// <summary>
        /// 入力フォルダの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectInput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fiag = new FolderBrowserDialog();
            fiag.SelectedPath = this.textBoxInputFolder.Text;
            var diag = fiag.ShowDialog(this);
            if (diag != DialogResult.OK)
            {
                return;
            }

            this.textBoxInputFolder.Text = fiag.SelectedPath;
        }

        /// <summary>
        /// 保存フォルダの選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSelectOtput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fiag = new FolderBrowserDialog();
            fiag.SelectedPath = this.textBoxOutputFolder.Text;
            var diag = fiag.ShowDialog(this);
            if (diag != DialogResult.OK)
            {
                return;
            }

            this.textBoxOutputFolder.Text = fiag.SelectedPath;
        }

        /// <summary>
        /// OKが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (AsyncControlState st = new AsyncControlState(this))
                {
                    var a = await this.GetInputData();
                    if (a == null)
                    {
                        return;
                    }
                    this.InputData = a;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"問題が発生しました {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キャンセルが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
