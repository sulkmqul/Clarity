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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxTipSize = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownTipSizeH = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownTipSizeW = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownHeight = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.groupBoxTipCount = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownTipCountH = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownTipCountW = new System.Windows.Forms.NumericUpDown();
            this.groupBoxTipSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipSizeH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipSizeW)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            this.groupBoxTipCount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipCountH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipCountW)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(211, 224);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(130, 224);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBoxTipSize
            // 
            this.groupBoxTipSize.Controls.Add(this.label3);
            this.groupBoxTipSize.Controls.Add(this.numericUpDownTipSizeH);
            this.groupBoxTipSize.Controls.Add(this.label2);
            this.groupBoxTipSize.Controls.Add(this.numericUpDownTipSizeW);
            this.groupBoxTipSize.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTipSize.Name = "groupBoxTipSize";
            this.groupBoxTipSize.Size = new System.Drawing.Size(271, 58);
            this.groupBoxTipSize.TabIndex = 0;
            this.groupBoxTipSize.TabStop = false;
            this.groupBoxTipSize.Text = "チップサイズ(Pixel)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "縦";
            // 
            // numericUpDownTipSizeH
            // 
            this.numericUpDownTipSizeH.Location = new System.Drawing.Point(155, 22);
            this.numericUpDownTipSizeH.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTipSizeH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTipSizeH.Name = "numericUpDownTipSizeH";
            this.numericUpDownTipSizeH.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownTipSizeH.TabIndex = 6;
            this.numericUpDownTipSizeH.Tag = "2";
            this.numericUpDownTipSizeH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTipSizeH.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownTipSizeH.ValueChanged += new System.EventHandler(this.numericUpDownTip_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "横";
            // 
            // numericUpDownTipSizeW
            // 
            this.numericUpDownTipSizeW.Location = new System.Drawing.Point(44, 22);
            this.numericUpDownTipSizeW.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownTipSizeW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTipSizeW.Name = "numericUpDownTipSizeW";
            this.numericUpDownTipSizeW.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownTipSizeW.TabIndex = 4;
            this.numericUpDownTipSizeW.Tag = "1";
            this.numericUpDownTipSizeW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTipSizeW.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownTipSizeW.ValueChanged += new System.EventHandler(this.numericUpDownTip_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numericUpDownHeight);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numericUpDownWidth);
            this.groupBox1.Location = new System.Drawing.Point(12, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(271, 58);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "全体サイズ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(130, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "縦";
            // 
            // numericUpDownHeight
            // 
            this.numericUpDownHeight.Location = new System.Drawing.Point(155, 22);
            this.numericUpDownHeight.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownHeight.Name = "numericUpDownHeight";
            this.numericUpDownHeight.ReadOnly = true;
            this.numericUpDownHeight.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownHeight.TabIndex = 6;
            this.numericUpDownHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "横";
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(44, 22);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            1215752192,
            23,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.ReadOnly = true;
            this.numericUpDownWidth.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownWidth.TabIndex = 4;
            this.numericUpDownWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBoxTipCount
            // 
            this.groupBoxTipCount.Controls.Add(this.label1);
            this.groupBoxTipCount.Controls.Add(this.numericUpDownTipCountH);
            this.groupBoxTipCount.Controls.Add(this.label4);
            this.groupBoxTipCount.Controls.Add(this.numericUpDownTipCountW);
            this.groupBoxTipCount.Location = new System.Drawing.Point(12, 76);
            this.groupBoxTipCount.Name = "groupBoxTipCount";
            this.groupBoxTipCount.Size = new System.Drawing.Size(271, 58);
            this.groupBoxTipCount.TabIndex = 1;
            this.groupBoxTipCount.TabStop = false;
            this.groupBoxTipCount.Text = "チップ数";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "縦";
            // 
            // numericUpDownTipCountH
            // 
            this.numericUpDownTipCountH.Location = new System.Drawing.Point(155, 22);
            this.numericUpDownTipCountH.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownTipCountH.Name = "numericUpDownTipCountH";
            this.numericUpDownTipCountH.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownTipCountH.TabIndex = 6;
            this.numericUpDownTipCountH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTipCountH.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTipCountH.ValueChanged += new System.EventHandler(this.numericUpDownTip_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "横";
            // 
            // numericUpDownTipCountW
            // 
            this.numericUpDownTipCountW.Location = new System.Drawing.Point(44, 22);
            this.numericUpDownTipCountW.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownTipCountW.Name = "numericUpDownTipCountW";
            this.numericUpDownTipCountW.Size = new System.Drawing.Size(66, 23);
            this.numericUpDownTipCountW.TabIndex = 4;
            this.numericUpDownTipCountW.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownTipCountW.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTipCountW.ValueChanged += new System.EventHandler(this.numericUpDownTip_ValueChanged);
            // 
            // ProjectSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 259);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxTipCount);
            this.Controls.Add(this.groupBoxTipSize);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Name = "ProjectSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Project Setting";
            this.Load += new System.EventHandler(this.ProjectSettingForm_Load);
            this.groupBoxTipSize.ResumeLayout(false);
            this.groupBoxTipSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipSizeH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipSizeW)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            this.groupBoxTipCount.ResumeLayout(false);
            this.groupBoxTipCount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipCountH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTipCountW)).EndInit();
            this.ResumeLayout(false);

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