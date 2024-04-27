namespace TrainSrcArranger
{
    partial class SettingForm
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
            textBoxInputFolder = new TextBox();
            buttonSelectInput = new Button();
            label1 = new Label();
            label2 = new Label();
            buttonSelectOtput = new Button();
            textBoxOutputFolder = new TextBox();
            buttonCancel = new Button();
            buttonOK = new Button();
            label3 = new Label();
            numericUpDownCutSizeWidth = new NumericUpDown();
            numericUpDownCutSizeHeight = new NumericUpDown();
            numericUpDownStartIndex = new NumericUpDown();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCutSizeWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCutSizeHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStartIndex).BeginInit();
            SuspendLayout();
            // 
            // textBoxInputFolder
            // 
            textBoxInputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxInputFolder.Location = new Point(12, 27);
            textBoxInputFolder.Name = "textBoxInputFolder";
            textBoxInputFolder.Size = new Size(479, 23);
            textBoxInputFolder.TabIndex = 0;
            // 
            // buttonSelectInput
            // 
            buttonSelectInput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSelectInput.Location = new Point(497, 27);
            buttonSelectInput.Name = "buttonSelectInput";
            buttonSelectInput.Size = new Size(75, 23);
            buttonSelectInput.TabIndex = 1;
            buttonSelectInput.Text = "...";
            buttonSelectInput.UseVisualStyleBackColor = true;
            buttonSelectInput.Click += buttonSelectInput_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(66, 15);
            label1.TabIndex = 2;
            label1.Text = "入力フォルダ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 70);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 5;
            label2.Text = "出力フォルダ";
            // 
            // buttonSelectOtput
            // 
            buttonSelectOtput.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSelectOtput.Location = new Point(497, 88);
            buttonSelectOtput.Name = "buttonSelectOtput";
            buttonSelectOtput.Size = new Size(75, 23);
            buttonSelectOtput.TabIndex = 4;
            buttonSelectOtput.Text = "...";
            buttonSelectOtput.UseVisualStyleBackColor = true;
            buttonSelectOtput.Click += buttonSelectOtput_Click;
            // 
            // textBoxOutputFolder
            // 
            textBoxOutputFolder.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxOutputFolder.Location = new Point(12, 88);
            textBoxOutputFolder.Name = "textBoxOutputFolder";
            textBoxOutputFolder.Size = new Size(479, 23);
            textBoxOutputFolder.TabIndex = 3;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(497, 254);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 6;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(416, 254);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 7;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 135);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 8;
            label3.Text = "切り出しサイズ";
            // 
            // numericUpDownCutSizeWidth
            // 
            numericUpDownCutSizeWidth.Location = new Point(12, 153);
            numericUpDownCutSizeWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownCutSizeWidth.Name = "numericUpDownCutSizeWidth";
            numericUpDownCutSizeWidth.Size = new Size(120, 23);
            numericUpDownCutSizeWidth.TabIndex = 9;
            numericUpDownCutSizeWidth.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownCutSizeHeight
            // 
            numericUpDownCutSizeHeight.Location = new Point(138, 153);
            numericUpDownCutSizeHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownCutSizeHeight.Name = "numericUpDownCutSizeHeight";
            numericUpDownCutSizeHeight.Size = new Size(120, 23);
            numericUpDownCutSizeHeight.TabIndex = 10;
            numericUpDownCutSizeHeight.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownStartIndex
            // 
            numericUpDownStartIndex.Location = new Point(12, 209);
            numericUpDownStartIndex.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownStartIndex.Name = "numericUpDownStartIndex";
            numericUpDownStartIndex.Size = new Size(120, 23);
            numericUpDownStartIndex.TabIndex = 12;
            numericUpDownStartIndex.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 191);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 11;
            label4.Text = "開始index";
            // 
            // SettingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 289);
            Controls.Add(numericUpDownStartIndex);
            Controls.Add(label4);
            Controls.Add(numericUpDownCutSizeHeight);
            Controls.Add(numericUpDownCutSizeWidth);
            Controls.Add(label3);
            Controls.Add(buttonOK);
            Controls.Add(buttonCancel);
            Controls.Add(label2);
            Controls.Add(buttonSelectOtput);
            Controls.Add(textBoxOutputFolder);
            Controls.Add(label1);
            Controls.Add(buttonSelectInput);
            Controls.Add(textBoxInputFolder);
            Name = "SettingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Setting";
            Load += SettingForm_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDownCutSizeWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownCutSizeHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStartIndex).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxInputFolder;
        private Button buttonSelectInput;
        private Label label1;
        private Label label2;
        private Button buttonSelectOtput;
        private TextBox textBoxOutputFolder;
        private Button buttonCancel;
        private Button buttonOK;
        private Label label3;
        private NumericUpDown numericUpDownCutSizeWidth;
        private NumericUpDown numericUpDownCutSizeHeight;
        private NumericUpDown numericUpDownStartIndex;
        private Label label4;
    }
}