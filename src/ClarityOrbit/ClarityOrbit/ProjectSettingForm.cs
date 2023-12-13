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

        public class SettingData
        {
            /// <summary>
            /// タイル一つのサイズ
            /// </summary>
            public Size TileSize { get; set; } = new Size(0, 0);
            /// <summary>
            /// タイル数(基本的に無限は考慮しない)
            /// </summary>
            public Size TileCount { get; set; } = new Size(1, 1);
        }


        private SettingData SetInfo = new SettingData();

        /// <summary>
        /// 結果
        /// </summary>
        public SettingData Result
        {
            get
            {
                return this.SetInfo;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化(新規作成時)
        /// </summary>        
        public void Init()
        {
            this.SetInfo = new SettingData() { TileSize = new Size(64, 64), TileCount = new Size(10, 10) };
            this.Display();
        }
        /// <summary>
        /// 初期化更新時
        /// </summary>        
        public void Init(OrbitData data)
        {
            this.SetInfo = new SettingData() { TileSize = data.TileSize, TileCount = data.TileCount };
            this.Display();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="f">true=全体更新11 false=サイズだけ</param>
        private void Display(bool f = true)
        {
            //関連コントロール
            NumericUpDown[] ncvec =
            {
                this.numericUpDownTipSizeW,
                this.numericUpDownTipSizeH,
                this.numericUpDownTipCountW,
                this.numericUpDownTipCountH,
            };

            using (BlockProcedureState bs = new BlockProcedureState(() =>
            {
                foreach(var nc in ncvec)
                {
                    nc.ValueChanged -= numericUpDownTip_ValueChanged;
                }
            },
            () =>
            {
                foreach (var nc in ncvec)
                {
                    nc.ValueChanged += numericUpDownTip_ValueChanged;
                }
            }))
            {

                //全体
                this.numericUpDownWidth.Value = this.SetInfo.TileSize.Width * this.SetInfo.TileCount.Width;
                this.numericUpDownHeight.Value = this.SetInfo.TileSize.Height * this.SetInfo.TileCount.Height;
                if (f == false)
                {
                    return;
                }

                //タイルサイズ
                this.numericUpDownTipSizeW.Value = this.SetInfo.TileSize.Width;
                this.numericUpDownTipSizeH.Value = this.SetInfo.TileSize.Height;

                //タイル数
                this.numericUpDownTipCountW.Value = this.SetInfo.TileCount.Width;
                this.numericUpDownTipCountH.Value = this.SetInfo.TileCount.Height;
            }


        }

        /// <summary>
        /// 入力値の取得
        /// </summary>
        private void GetInputValue()
        {
            //タイルサイズ
            int tsw = Convert.ToInt32(this.numericUpDownTipSizeW.Value);
            int tsh = Convert.ToInt32(this.numericUpDownTipSizeH.Value);
            this.SetInfo.TileSize = new Size(tsw, tsh);

            //タイル数
            int tcw = Convert.ToInt32(this.numericUpDownTipCountW.Value);
            int tch = Convert.ToInt32(this.numericUpDownTipCountH.Value);
            this.SetInfo.TileCount = new Size(tcw, tch);
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
        private void numericUpDownTip_ValueChanged(object? sender, EventArgs e)
        {
            this.GetInputValue();
            this.Display(false);
        }
    }
}
