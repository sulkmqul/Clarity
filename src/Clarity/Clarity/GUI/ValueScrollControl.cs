using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clarity.GUI
{
    /// <summary>
    /// 値スクロール選択コントロール
    /// </summary>
    public partial class ValueScrollControl : UserControl
    {
        public ValueScrollControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 値の表示フォーマット string.format書式
        /// </summary>
        public string ValueFormat { get; set; } = "{0}";

        /// <summary>
        /// 固定小数点値(10の倍数を設定)
        /// </summary>
        public int FixedPoint { get; set; } = 1;

        /// <summary>
        /// 最小値
        /// </summary>
        public int MinValue
        {
            get
            {
                return this.trackBarValue.Minimum;
            }
            set
            {
                this.trackBarValue.Minimum = value;
            }
        }

        /// <summary>
        /// 最大値
        /// </summary>
        public int MaxValue
        {
            get
            {
                return this.trackBarValue.Maximum;
            }
            set
            {
                this.trackBarValue.Maximum = value;
            }
        }

        /// <summary>
        /// 値 固定小数点を利用した少数設定の場合はValueFixedPointを利用する。
        /// </summary>
        public int Value
        {
            get
            {
                return this.trackBarValue.Value;
            }
            set
            {
                this.trackBarValue.Value = value;
            }
        }

        /// <summary>
        /// 固定小数点込み値
        /// </summary>
        public double ValueFixedPoint
        {
            get
            {
                return this.CalcuFiexedPoint();
            }
            set
            {
                int val = Convert.ToInt32(value * (double)this.FixedPoint);
                this.trackBarValue.Value = val;
            }
        }

        /// <summary>
        /// 値の変更
        /// </summary>
        public event System.EventHandler ValueChanged
        {
            add
            {
                this.trackBarValue.ValueChanged += value;
            }
            remove
            {
                this.trackBarValue.ValueChanged -= value;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 固定小数点値の計算
        /// </summary>
        /// <returns></returns>
        private double CalcuFiexedPoint()
        {
            double fix = this.FixedPoint;
            double value = this.trackBarValue.Value;

            return value / fix;
        }


        /// <summary>
        /// 値の表示
        /// </summary>
        private void DispValue()
        {
            if (this.FixedPoint != 1)
            {
                double val = this.CalcuFiexedPoint();
                this.labelValue.Text = string.Format(ValueFormat, val);
                return;
            }

            this.labelValue.Text = string.Format(ValueFormat, this.trackBarValue.Value);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 値が変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarValue_ValueChanged(object sender, EventArgs e)
        {
            this.DispValue();
        }
    }
}
