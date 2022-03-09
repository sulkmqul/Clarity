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
    /// <summary>
    /// 新規作成画面
    /// </summary>
    public partial class NewProjectForm : Form
    {
        public NewProjectForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public EmotionProjectDataBasic GetInputData()
        {
            EmotionProjectDataBasic ans = new EmotionProjectDataBasic();
            ans.ImageWidth = Convert.ToInt32(this.numericUpDownImageWidth.Value);
            ans.ImageHeight = Convert.ToInt32(this.numericUpDownImageHeight.Value);

            return ans;
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProjectForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// OKボタンが押された時
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
