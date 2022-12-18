using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion.LayerControl
{
    /// <summary>
    /// フレーム位置設定画面
    /// </summary>
    public partial class FramePosSettingForm : Form
    {
        public FramePosSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 結果
        /// </summary>
        public int FramePos
        {
            get
            {
                int n = Convert.ToInt32(this.numericUpDownFramePos.Value);
                return n;
            }
            set
            {
                this.numericUpDownFramePos.Value = value;
            }
        }

        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FramePosSettingForm_Load(object sender, EventArgs e)
        {
            this.numericUpDownFramePos.Minimum = 0;
            this.numericUpDownFramePos.Maximum = CeGlobal.Project.BasicInfo.MaxFrame;

        }

        /// <summary>
        /// okが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
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
