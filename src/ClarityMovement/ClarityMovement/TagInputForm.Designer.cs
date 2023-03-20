namespace ClarityMovement
{
    partial class TagInputForm
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
            clarityDataInputControl1 = new Clarity.GUI.ClarityDataInputControl();
            SuspendLayout();
            // 
            // buttonOk
            // 
            buttonOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonOk.Location = new Point(372, 291);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(75, 23);
            buttonOk.TabIndex = 6;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Location = new Point(453, 291);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "キャンセル";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // clarityDataInputControl1
            // 
            clarityDataInputControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            clarityDataInputControl1.Location = new Point(12, 12);
            clarityDataInputControl1.Name = "clarityDataInputControl1";
            clarityDataInputControl1.Size = new Size(516, 273);
            clarityDataInputControl1.TabIndex = 10;
            // 
            // TagInputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 326);
            Controls.Add(clarityDataInputControl1);
            Controls.Add(buttonOk);
            Controls.Add(buttonCancel);
            Name = "TagInputForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "TagInputForm";
            Load += TagInputForm_Load;
            Shown += TagInputForm_Shown;
            ResumeLayout(false);
        }

        #endregion
        private Button buttonOk;
        private Button buttonCancel;
        private Clarity.GUI.ClarityDataInputControl clarityDataInputControl1;
    }
}