namespace ClarityMovement
{
    partial class CreateProjectForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownSizeW = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownSizeH = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownFPS = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownFrame = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeW)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size";
            // 
            // numericUpDownSizeW
            // 
            this.numericUpDownSizeW.Location = new System.Drawing.Point(51, 28);
            this.numericUpDownSizeW.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSizeW.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSizeW.Name = "numericUpDownSizeW";
            this.numericUpDownSizeW.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownSizeW.TabIndex = 0;
            this.numericUpDownSizeW.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "W";
            // 
            // numericUpDownSizeH
            // 
            this.numericUpDownSizeH.Location = new System.Drawing.Point(51, 57);
            this.numericUpDownSizeH.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSizeH.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSizeH.Name = "numericUpDownSizeH";
            this.numericUpDownSizeH.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownSizeH.TabIndex = 1;
            this.numericUpDownSizeH.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "H";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "FPS";
            // 
            // numericUpDownFPS
            // 
            this.numericUpDownFPS.Enabled = false;
            this.numericUpDownFPS.Location = new System.Drawing.Point(51, 166);
            this.numericUpDownFPS.Name = "numericUpDownFPS";
            this.numericUpDownFPS.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownFPS.TabIndex = 3;
            this.numericUpDownFPS.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Frame";
            // 
            // numericUpDownFrame
            // 
            this.numericUpDownFrame.Location = new System.Drawing.Point(51, 116);
            this.numericUpDownFrame.Maximum = new decimal(new int[] {
            10000,
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
            this.numericUpDownFrame.TabIndex = 2;
            this.numericUpDownFrame.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(96, 218);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(15, 218);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // CreateProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(188, 266);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.numericUpDownFrame);
            this.Controls.Add(this.numericUpDownFPS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownSizeH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownSizeW);
            this.Controls.Add(this.label1);
            this.Name = "CreateProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateProjectForm";
            this.Load += new System.EventHandler(this.CreateProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeW)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSizeH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private NumericUpDown numericUpDownSizeW;
        private Label label2;
        private NumericUpDown numericUpDownSizeH;
        private Label label3;
        private Label label4;
        private NumericUpDown numericUpDownFPS;
        private Label label5;
        private NumericUpDown numericUpDownFrame;
        private Button buttonCancel;
        private Button buttonOk;
    }
}