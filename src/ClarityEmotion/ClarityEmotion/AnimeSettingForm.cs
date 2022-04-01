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
    /// アニメ設定画面
    /// </summary>
    public partial class AnimeSettingForm : Form
    {
        public AnimeSettingForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// データの表示
        /// </summary>
        private void DispData()
        {
            EmotionProjectDataBasic data = EmotionProject.Mana.BasicInfo;

            //画像サイズ
            this.numericUpDownCanvasSizeW.Value = data.ImageWidth;
            this.numericUpDownCanvasSizeH.Value = data.ImageHeight;

            ///最大フレーム
            this.numericUpDownFrame.Value = data.MaxFrame;
        }


        /// <summary>
        /// 入力情報の取得
        /// </summary>
        private void GetInputData()
        {
            EmotionProjectDataBasic data = EmotionProject.Mana.BasicInfo;

            //画像サイズ
            data.ImageWidth = Convert.ToInt32(this.numericUpDownCanvasSizeW.Value);
            data.ImageHeight = Convert.ToInt32(this.numericUpDownCanvasSizeH.Value); 

            ///最大フレーム
            data.MaxFrame = Convert.ToInt32(this.numericUpDownFrame.Value);
        }
        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimeSettingForm_Load(object sender, EventArgs e)
        {
            this.DispData();
        }

        /// <summary>
        /// OKボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.GetInputData();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キャンセルボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
