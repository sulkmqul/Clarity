using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.GUI;


namespace ClarityOrbit
{
    /// <summary>
    /// プロジェクト新規作成画面
    /// </summary>
    internal partial class ProjectSettingForm : BaseClarityForm
    {
        public ProjectSettingForm()
        {
            InitializeComponent();
        }


        private OrbitProjectBase TempInfo;

        /// <summary>
        /// 結果
        /// </summary>
        public OrbitProjectBase Result
        {
            get
            {
                return this.TempInfo;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="bdata">プロジェクト情報(新規=null)</param>
        public void Init(OrbitProjectBase? bdata = null)
        {
            this.TempInfo = new OrbitProjectBase();
            if (bdata == null)
            {
                return;            
            }

            //コピーして保存する
            this.TempInfo = bdata.DeepClone();

            this.Display();

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="f">true=全体更新11 false=サイズだけ</param>
        private void Display(bool f = true)
        {
            //全体
            this.numericUpDownWidth.Value = this.TempInfo.ImageSize.Width;
            this.numericUpDownHeight.Value = this.TempInfo.ImageSize.Height;
            if (f == false)
            {
                return;
            }

            //タイルサイズ
            this.numericUpDownTipSizeW.Value = this.TempInfo.TileSize.Width;
            this.numericUpDownTipSizeH.Value = this.TempInfo.TileSize.Height;

            //タイル数
            this.numericUpDownTipCountW.Value = this.TempInfo.TileCount.Width;
            this.numericUpDownTipCountH.Value = this.TempInfo.TileCount.Height;


        }

        /// <summary>
        /// 入力値の取得
        /// </summary>
        private void GetInputValue()
        {
            //タイルサイズ
            this.TempInfo.TileSize.Width = Convert.ToInt32(this.numericUpDownTipSizeW.Value);
            this.TempInfo.TileSize.Height = Convert.ToInt32(this.numericUpDownTipSizeH.Value);

            //タイル数
            this.TempInfo.TileCount.Width = Convert.ToInt32(this.numericUpDownTipCountW.Value);
            this.TempInfo.TileCount.Height = Convert.ToInt32(this.numericUpDownTipCountH.Value);
        }

        
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProjectSettingForm_Load(object sender, EventArgs e)
        {
            this.GetInputValue();
            this.Display(false);
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
        /// 閉じるボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 値が変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownTip_ValueChanged(object sender, EventArgs e)
        {
            this.GetInputValue();
            this.Display(false);
        }
    }
}
