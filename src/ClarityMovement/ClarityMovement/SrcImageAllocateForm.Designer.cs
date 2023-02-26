namespace ClarityMovement
{
    partial class SrcImageAllocateForm
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownOffset = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownFrame = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSelectedDataCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewSrcImage
            // 
            this.listViewSrcImage.Location = new System.Drawing.Point(12, 53);
            this.listViewSrcImage.Size = new System.Drawing.Size(776, 351);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(632, 415);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(713, 415);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // numericUpDownOffset
            // 
            this.numericUpDownOffset.Location = new System.Drawing.Point(55, 24);
            this.numericUpDownOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownOffset.Name = "numericUpDownOffset";
            this.numericUpDownOffset.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownOffset.TabIndex = 4;
            // 
            // numericUpDownInterval
            // 
            this.numericUpDownInterval.Location = new System.Drawing.Point(249, 24);
            this.numericUpDownInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownInterval.Name = "numericUpDownInterval";
            this.numericUpDownInterval.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownInterval.TabIndex = 4;
            this.numericUpDownInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "offset";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "間隔";
            // 
            // numericUpDownFrame
            // 
            this.numericUpDownFrame.Location = new System.Drawing.Point(668, 26);
            this.numericUpDownFrame.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFrame.Name = "numericUpDownFrame";
            this.numericUpDownFrame.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownFrame.TabIndex = 4;
            this.numericUpDownFrame.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(621, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "フレーム";
            // 
            // labelSelectedDataCount
            // 
            this.labelSelectedDataCount.AutoSize = true;
            this.labelSelectedDataCount.Location = new System.Drawing.Point(70, 419);
            this.labelSelectedDataCount.Name = "labelSelectedDataCount";
            this.labelSelectedDataCount.Size = new System.Drawing.Size(13, 15);
            this.labelSelectedDataCount.TabIndex = 6;
            this.labelSelectedDataCount.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 419);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Select = ";
            // 
            // SrcImageAllocateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSelectedDataCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownFrame);
            this.Controls.Add(this.numericUpDownInterval);
            this.Controls.Add(this.numericUpDownOffset);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Name = "SrcImageAllocateForm";
            this.Text = "SrcImageAllocateForm";
            this.Load += new System.EventHandler(this.SrcImageAllocateForm_Load);
            this.Controls.SetChildIndex(this.listViewSrcImage, 0);
            this.Controls.SetChildIndex(this.buttonCancel, 0);
            this.Controls.SetChildIndex(this.buttonOK, 0);
            this.Controls.SetChildIndex(this.numericUpDownOffset, 0);
            this.Controls.SetChildIndex(this.numericUpDownInterval, 0);
            this.Controls.SetChildIndex(this.numericUpDownFrame, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.labelSelectedDataCount, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonOK;
        private Button buttonCancel;
        private NumericUpDown numericUpDownOffset;
        private NumericUpDown numericUpDownInterval;
        private Label label1;
        private Label label2;
        private NumericUpDown numericUpDownFrame;
        private Label label3;
        private Label labelSelectedDataCount;
        private Label label4;
    }
}