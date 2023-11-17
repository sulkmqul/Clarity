namespace ClarityOrbit
{
    partial class ProjectSettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonClose = new Button();
            buttonOk = new Button();
            groupBoxTipSize = new GroupBox();
            label3 = new Label();
            numericUpDownTipSizeH = new NumericUpDown();
            label2 = new Label();
            numericUpDownTipSizeW = new NumericUpDown();
            groupBox1 = new GroupBox();
            label7 = new Label();
            numericUpDownHeight = new NumericUpDown();
            label8 = new Label();
            numericUpDownWidth = new NumericUpDown();
            groupBoxTipCount = new GroupBox();
            label1 = new Label();
            numericUpDownTipCountH = new NumericUpDown();
            label4 = new Label();
            numericUpDownTipCountW = new NumericUpDown();
            groupBoxTipSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipSizeH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipSizeW).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).BeginInit();
            groupBoxTipCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipCountH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipCountW).BeginInit();
            SuspendLayout();
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClose.Location = new Point(211, 224);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(75, 23);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Location = new Point(130, 224);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 3;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // groupBoxTipSize
            // 
            groupBoxTipSize.Controls.Add(label3);
            groupBoxTipSize.Controls.Add(numericUpDownTipSizeH);
            groupBoxTipSize.Controls.Add(label2);
            groupBoxTipSize.Controls.Add(numericUpDownTipSizeW);
            groupBoxTipSize.Location = new Point(12, 12);
            groupBoxTipSize.Name = "groupBoxTipSize";
            groupBoxTipSize.Size = new Size(271, 58);
            groupBoxTipSize.TabIndex = 0;
            groupBoxTipSize.TabStop = false;
            groupBoxTipSize.Text = "チップサイズ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(130, 24);
            label3.Name = "label3";
            label3.Size = new Size(19, 15);
            label3.TabIndex = 7;
            label3.Text = "縦";
            // 
            // numericUpDownTipSizeH
            // 
            numericUpDownTipSizeH.Location = new Point(155, 22);
            numericUpDownTipSizeH.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownTipSizeH.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownTipSizeH.Name = "numericUpDownTipSizeH";
            numericUpDownTipSizeH.Size = new Size(66, 23);
            numericUpDownTipSizeH.TabIndex = 6;
            numericUpDownTipSizeH.Tag = "2";
            numericUpDownTipSizeH.TextAlign = HorizontalAlignment.Right;
            numericUpDownTipSizeH.Value = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDownTipSizeH.ValueChanged += numericUpDownTip_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(19, 24);
            label2.Name = "label2";
            label2.Size = new Size(19, 15);
            label2.TabIndex = 5;
            label2.Text = "横";
            // 
            // numericUpDownTipSizeW
            // 
            numericUpDownTipSizeW.Location = new Point(44, 22);
            numericUpDownTipSizeW.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numericUpDownTipSizeW.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownTipSizeW.Name = "numericUpDownTipSizeW";
            numericUpDownTipSizeW.Size = new Size(66, 23);
            numericUpDownTipSizeW.TabIndex = 4;
            numericUpDownTipSizeW.Tag = "1";
            numericUpDownTipSizeW.TextAlign = HorizontalAlignment.Right;
            numericUpDownTipSizeW.Value = new decimal(new int[] { 30, 0, 0, 0 });
            numericUpDownTipSizeW.ValueChanged += numericUpDownTip_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(numericUpDownHeight);
            groupBox1.Controls.Add(label8);
            groupBox1.Controls.Add(numericUpDownWidth);
            groupBox1.Location = new Point(12, 140);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(271, 58);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "全体サイズ";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(130, 24);
            label7.Name = "label7";
            label7.Size = new Size(19, 15);
            label7.TabIndex = 7;
            label7.Text = "縦";
            // 
            // numericUpDownHeight
            // 
            numericUpDownHeight.Location = new Point(155, 22);
            numericUpDownHeight.Maximum = new decimal(new int[] { 1215752192, 23, 0, 0 });
            numericUpDownHeight.Name = "numericUpDownHeight";
            numericUpDownHeight.ReadOnly = true;
            numericUpDownHeight.Size = new Size(66, 23);
            numericUpDownHeight.TabIndex = 6;
            numericUpDownHeight.TextAlign = HorizontalAlignment.Right;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(19, 24);
            label8.Name = "label8";
            label8.Size = new Size(19, 15);
            label8.TabIndex = 5;
            label8.Text = "横";
            // 
            // numericUpDownWidth
            // 
            numericUpDownWidth.Location = new Point(44, 22);
            numericUpDownWidth.Maximum = new decimal(new int[] { 1215752192, 23, 0, 0 });
            numericUpDownWidth.Name = "numericUpDownWidth";
            numericUpDownWidth.ReadOnly = true;
            numericUpDownWidth.Size = new Size(66, 23);
            numericUpDownWidth.TabIndex = 4;
            numericUpDownWidth.TextAlign = HorizontalAlignment.Right;
            // 
            // groupBoxTipCount
            // 
            groupBoxTipCount.Controls.Add(label1);
            groupBoxTipCount.Controls.Add(numericUpDownTipCountH);
            groupBoxTipCount.Controls.Add(label4);
            groupBoxTipCount.Controls.Add(numericUpDownTipCountW);
            groupBoxTipCount.Location = new Point(12, 76);
            groupBoxTipCount.Name = "groupBoxTipCount";
            groupBoxTipCount.Size = new Size(271, 58);
            groupBoxTipCount.TabIndex = 1;
            groupBoxTipCount.TabStop = false;
            groupBoxTipCount.Text = "チップ数";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(130, 24);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 7;
            label1.Text = "縦";
            // 
            // numericUpDownTipCountH
            // 
            numericUpDownTipCountH.Location = new Point(155, 22);
            numericUpDownTipCountH.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownTipCountH.Name = "numericUpDownTipCountH";
            numericUpDownTipCountH.Size = new Size(66, 23);
            numericUpDownTipCountH.TabIndex = 6;
            numericUpDownTipCountH.TextAlign = HorizontalAlignment.Right;
            numericUpDownTipCountH.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDownTipCountH.ValueChanged += numericUpDownTip_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(19, 24);
            label4.Name = "label4";
            label4.Size = new Size(19, 15);
            label4.TabIndex = 5;
            label4.Text = "横";
            // 
            // numericUpDownTipCountW
            // 
            numericUpDownTipCountW.Location = new Point(44, 22);
            numericUpDownTipCountW.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericUpDownTipCountW.Name = "numericUpDownTipCountW";
            numericUpDownTipCountW.Size = new Size(66, 23);
            numericUpDownTipCountW.TabIndex = 4;
            numericUpDownTipCountW.TextAlign = HorizontalAlignment.Right;
            numericUpDownTipCountW.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericUpDownTipCountW.ValueChanged += numericUpDownTip_ValueChanged;
            // 
            // ProjectSettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(298, 259);
            Controls.Add(groupBox1);
            Controls.Add(groupBoxTipCount);
            Controls.Add(groupBoxTipSize);
            Controls.Add(buttonOk);
            Controls.Add(buttonClose);
            Name = "ProjectSettingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Project Setting";
            Load += ProjectSettingForm_Load;
            groupBoxTipSize.ResumeLayout(false);
            groupBoxTipSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipSizeH).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipSizeW).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownWidth).EndInit();
            groupBoxTipCount.ResumeLayout(false);
            groupBoxTipCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipCountH).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTipCountW).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxTipSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownTipSizeH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownTipSizeW;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.GroupBox groupBoxTipCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTipCountH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownTipCountW;
    }
}