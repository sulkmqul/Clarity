namespace ClarityMovement.Export
{
    partial class MotionExportForm
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
            buttonOk = new Button();
            buttonCancel = new Button();
            label1 = new Label();
            textBoxExportFilePath = new TextBox();
            buttonSelectExportFile = new Button();
            label2 = new Label();
            textBoxMotionCode = new TextBox();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOk.Location = new Point(231, 296);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 6;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(312, 296);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "キャンセル";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(84, 15);
            label1.TabIndex = 8;
            label1.Text = "出力ファイルパス";
            // 
            // textBoxExportFilePath
            // 
            textBoxExportFilePath.Location = new Point(12, 27);
            textBoxExportFilePath.Name = "textBoxExportFilePath";
            textBoxExportFilePath.Size = new Size(294, 23);
            textBoxExportFilePath.TabIndex = 9;
            // 
            // buttonSelectExportFile
            // 
            buttonSelectExportFile.Location = new Point(312, 26);
            buttonSelectExportFile.Name = "buttonSelectExportFile";
            buttonSelectExportFile.Size = new Size(75, 23);
            buttonSelectExportFile.TabIndex = 10;
            buttonSelectExportFile.Text = "...";
            buttonSelectExportFile.UseVisualStyleBackColor = true;
            buttonSelectExportFile.Click += buttonSelectExportFile_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 65);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 11;
            label2.Text = "MotionCode";
            // 
            // textBoxMotionCode
            // 
            textBoxMotionCode.Location = new Point(12, 83);
            textBoxMotionCode.Name = "textBoxMotionCode";
            textBoxMotionCode.Size = new Size(294, 23);
            textBoxMotionCode.TabIndex = 12;
            // 
            // MotionExportForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(399, 331);
            Controls.Add(textBoxMotionCode);
            Controls.Add(label2);
            Controls.Add(buttonSelectExportFile);
            Controls.Add(textBoxExportFilePath);
            Controls.Add(label1);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Name = "MotionExportForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "MotionExportForm";
            Load += MotionExportForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonOk;
        private Button buttonCancel;
        private Label label1;
        private TextBox textBoxExportFilePath;
        private Button buttonSelectExportFile;
        private Label label2;
        private TextBox textBoxMotionCode;
    }
}