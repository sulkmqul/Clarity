using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clarity
{
    /// <summary>
    /// ログ表示画面
    /// </summary>
    partial class ClarityLogForm : Form
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClarityLogForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 非活性透明度
        /// </summary>
        private double DeactiveOpacity = 1.0;

        /// <summary>
        /// 最新行への強制スクロール可否
        /// </summary>
        private bool ScrollUpdateLineFlag = true;


        /// <summary>
        /// ログの追加
        /// </summary>
        /// <param name="s">出力文字列</param>
        private void AddLog(string s)
        {
            this.textBoxLog.Text += s;
            this.textBoxLog.Text += Environment.NewLine;

            //最新の表示位置へスクロール
            if (this.ScrollUpdateLineFlag == true)
            {
                this.textBoxLog.SelectionStart = this.textBoxLog.Text.Length;
                this.textBoxLog.ScrollToCaret();
            }
        }

        
        /// <summary>
        /// ログの追加
        /// </summary>
        /// <param name="s"></param>
        public void AddLogSafe(string s)
        {
            if (this.textBoxLog.InvokeRequired)
            {
                this.textBoxLog.BeginInvoke((MethodInvoker)delegate { this.AddLog(s); });
            }
            else
            {
                this.AddLog(s);
            }
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrabLogForm_Load(object sender, EventArgs e)
        {
            this.checkBoxTopMost.Checked = this.TopMost;
        }

        /// <summary>
        /// クリアボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            this.textBoxLog.Text = "";
        }

        /// <summary>
        /// アクティブ化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrabLogForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        /// <summary>
        /// 非アクティブ化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrabLogForm_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = this.DeactiveOpacity;
        }

        /// <summary>
        /// 透過度の値が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarAlpha_ValueChanged(object sender, EventArgs e)
        {
            this.DeactiveOpacity = (double)this.trackBarAlpha.Value / 100.0;
        }

        /// <summary>
        /// 閉じたいとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrabLogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        /// <summary>
        /// 最前面可否の変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.checkBoxTopMost.Checked;
        }
    }
}
