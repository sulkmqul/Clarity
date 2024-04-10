namespace ClarityMovement.Editor
{
    partial class TagEditForm
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
            buttonOK = new Button();
            buttonCancel = new Button();
            comboBoxTagType = new ComboBox();
            numericUpDownRePosX = new NumericUpDown();
            numericUpDownRePosY = new NumericUpDown();
            numericUpDownRePosZ = new NumericUpDown();
            numericUpDownStartFrame = new NumericUpDown();
            numericUpDownEndFrame = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRePosX).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRePosY).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRePosZ).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStartFrame).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEndFrame).BeginInit();
            SuspendLayout();
            // 
            // buttonOK
            // 
            buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonOK.Location = new Point(536, 289);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 0;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Location = new Point(617, 289);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 1;
            buttonCancel.Text = "キャンセル";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // comboBoxTagType
            // 
            comboBoxTagType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTagType.FormattingEnabled = true;
            comboBoxTagType.Location = new Point(12, 12);
            comboBoxTagType.Name = "comboBoxTagType";
            comboBoxTagType.Size = new Size(121, 23);
            comboBoxTagType.TabIndex = 2;
            // 
            // numericUpDownRePosX
            // 
            numericUpDownRePosX.DecimalPlaces = 2;
            numericUpDownRePosX.Location = new Point(12, 81);
            numericUpDownRePosX.Name = "numericUpDownRePosX";
            numericUpDownRePosX.Size = new Size(74, 23);
            numericUpDownRePosX.TabIndex = 3;
            numericUpDownRePosX.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownRePosY
            // 
            numericUpDownRePosY.DecimalPlaces = 2;
            numericUpDownRePosY.Location = new Point(92, 81);
            numericUpDownRePosY.Name = "numericUpDownRePosY";
            numericUpDownRePosY.Size = new Size(74, 23);
            numericUpDownRePosY.TabIndex = 3;
            numericUpDownRePosY.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownRePosZ
            // 
            numericUpDownRePosZ.DecimalPlaces = 2;
            numericUpDownRePosZ.Location = new Point(172, 81);
            numericUpDownRePosZ.Name = "numericUpDownRePosZ";
            numericUpDownRePosZ.Size = new Size(74, 23);
            numericUpDownRePosZ.TabIndex = 3;
            numericUpDownRePosZ.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownStartFrame
            // 
            numericUpDownStartFrame.Location = new Point(12, 41);
            numericUpDownStartFrame.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownStartFrame.Name = "numericUpDownStartFrame";
            numericUpDownStartFrame.Size = new Size(120, 23);
            numericUpDownStartFrame.TabIndex = 4;
            numericUpDownStartFrame.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownEndFrame
            // 
            numericUpDownEndFrame.Location = new Point(138, 41);
            numericUpDownEndFrame.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownEndFrame.Name = "numericUpDownEndFrame";
            numericUpDownEndFrame.Size = new Size(120, 23);
            numericUpDownEndFrame.TabIndex = 4;
            numericUpDownEndFrame.TextAlign = HorizontalAlignment.Right;
            // 
            // TagEditForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(704, 324);
            Controls.Add(numericUpDownEndFrame);
            Controls.Add(numericUpDownStartFrame);
            Controls.Add(numericUpDownRePosZ);
            Controls.Add(numericUpDownRePosY);
            Controls.Add(numericUpDownRePosX);
            Controls.Add(comboBoxTagType);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Name = "TagEditForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "TagEditForm";
            Load += TagEditForm_Load;
            Shown += TagEditForm_Shown;
            ((System.ComponentModel.ISupportInitialize)numericUpDownRePosX).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRePosY).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRePosZ).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStartFrame).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEndFrame).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonOK;
        private Button buttonCancel;
        private ComboBox comboBoxTagType;
        private NumericUpDown numericUpDownRePosX;
        private NumericUpDown numericUpDownRePosY;
        private NumericUpDown numericUpDownRePosZ;
        private NumericUpDown numericUpDownStartFrame;
        private NumericUpDown numericUpDownEndFrame;
    }
}