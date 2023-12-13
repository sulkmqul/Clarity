namespace ClarityCameraViewer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelControl = new Panel();
            textBoxCameraPath = new TextBox();
            panel1 = new Panel();
            radioButtonCameraPath = new RadioButton();
            radioButtonCameraID = new RadioButton();
            numericUpDownDeviceID = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            numericUpDownFPS = new NumericUpDown();
            numericUpDownImageHeight = new NumericUpDown();
            label1 = new Label();
            numericUpDownImageWidth = new NumericUpDown();
            checkBoxRec = new CheckBox();
            labelFPS = new Label();
            buttonGrabStop = new Button();
            buttonGrabStart = new Button();
            labelRec = new Label();
            clarityImageViewer1 = new Clarity.GUI.ClarityImageViewer();
            panelControl.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDeviceID).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFPS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageHeight).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageWidth).BeginInit();
            SuspendLayout();
            // 
            // panelControl
            // 
            panelControl.Controls.Add(textBoxCameraPath);
            panelControl.Controls.Add(panel1);
            panelControl.Controls.Add(numericUpDownDeviceID);
            panelControl.Controls.Add(label3);
            panelControl.Controls.Add(label2);
            panelControl.Controls.Add(numericUpDownFPS);
            panelControl.Controls.Add(numericUpDownImageHeight);
            panelControl.Controls.Add(label1);
            panelControl.Controls.Add(numericUpDownImageWidth);
            panelControl.Controls.Add(checkBoxRec);
            panelControl.Controls.Add(labelFPS);
            panelControl.Controls.Add(buttonGrabStop);
            panelControl.Controls.Add(buttonGrabStart);
            panelControl.Dock = DockStyle.Bottom;
            panelControl.Location = new Point(0, 423);
            panelControl.Margin = new Padding(2);
            panelControl.Name = "panelControl";
            panelControl.Size = new Size(909, 101);
            panelControl.TabIndex = 0;
            // 
            // textBoxCameraPath
            // 
            textBoxCameraPath.Location = new Point(77, 72);
            textBoxCameraPath.Name = "textBoxCameraPath";
            textBoxCameraPath.Size = new Size(207, 23);
            textBoxCameraPath.TabIndex = 10;
            textBoxCameraPath.Tag = "1";
            // 
            // panel1
            // 
            panel1.Controls.Add(radioButtonCameraPath);
            panel1.Controls.Add(radioButtonCameraID);
            panel1.Location = new Point(12, 40);
            panel1.Name = "panel1";
            panel1.Size = new Size(60, 58);
            panel1.TabIndex = 9;
            // 
            // radioButtonCameraPath
            // 
            radioButtonCameraPath.AutoSize = true;
            radioButtonCameraPath.Location = new Point(3, 36);
            radioButtonCameraPath.Name = "radioButtonCameraPath";
            radioButtonCameraPath.Size = new Size(49, 19);
            radioButtonCameraPath.TabIndex = 0;
            radioButtonCameraPath.Tag = "1";
            radioButtonCameraPath.Text = "Path";
            radioButtonCameraPath.UseVisualStyleBackColor = true;
            // 
            // radioButtonCameraID
            // 
            radioButtonCameraID.AutoSize = true;
            radioButtonCameraID.Checked = true;
            radioButtonCameraID.Location = new Point(3, 6);
            radioButtonCameraID.Name = "radioButtonCameraID";
            radioButtonCameraID.Size = new Size(56, 19);
            radioButtonCameraID.TabIndex = 0;
            radioButtonCameraID.TabStop = true;
            radioButtonCameraID.Tag = "0";
            radioButtonCameraID.Text = "DevID";
            radioButtonCameraID.UseVisualStyleBackColor = true;
            radioButtonCameraID.CheckedChanged += radioButtonCamera_CheckedChanged;
            // 
            // numericUpDownDeviceID
            // 
            numericUpDownDeviceID.Location = new Point(77, 46);
            numericUpDownDeviceID.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownDeviceID.Name = "numericUpDownDeviceID";
            numericUpDownDeviceID.Size = new Size(35, 23);
            numericUpDownDeviceID.TabIndex = 0;
            numericUpDownDeviceID.Tag = "0";
            numericUpDownDeviceID.TextAlign = HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(388, 50);
            label3.Name = "label3";
            label3.Size = new Size(26, 15);
            label3.TabIndex = 6;
            label3.Text = "FPS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(290, 74);
            label2.Name = "label2";
            label2.Size = new Size(16, 15);
            label2.TabIndex = 6;
            label2.Text = "H";
            // 
            // numericUpDownFPS
            // 
            numericUpDownFPS.Location = new Point(420, 48);
            numericUpDownFPS.Name = "numericUpDownFPS";
            numericUpDownFPS.Size = new Size(40, 23);
            numericUpDownFPS.TabIndex = 3;
            numericUpDownFPS.TextAlign = HorizontalAlignment.Right;
            numericUpDownFPS.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // numericUpDownImageHeight
            // 
            numericUpDownImageHeight.Location = new Point(314, 72);
            numericUpDownImageHeight.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownImageHeight.Name = "numericUpDownImageHeight";
            numericUpDownImageHeight.Size = new Size(55, 23);
            numericUpDownImageHeight.TabIndex = 2;
            numericUpDownImageHeight.TextAlign = HorizontalAlignment.Right;
            numericUpDownImageHeight.Value = new decimal(new int[] { 480, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(290, 45);
            label1.Name = "label1";
            label1.Size = new Size(18, 15);
            label1.TabIndex = 4;
            label1.Text = "W";
            // 
            // numericUpDownImageWidth
            // 
            numericUpDownImageWidth.Location = new Point(314, 43);
            numericUpDownImageWidth.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownImageWidth.Name = "numericUpDownImageWidth";
            numericUpDownImageWidth.Size = new Size(55, 23);
            numericUpDownImageWidth.TabIndex = 1;
            numericUpDownImageWidth.TextAlign = HorizontalAlignment.Right;
            numericUpDownImageWidth.Value = new decimal(new int[] { 640, 0, 0, 0 });
            // 
            // checkBoxRec
            // 
            checkBoxRec.Appearance = Appearance.Button;
            checkBoxRec.Location = new Point(771, 36);
            checkBoxRec.Margin = new Padding(2);
            checkBoxRec.Name = "checkBoxRec";
            checkBoxRec.Size = new Size(127, 43);
            checkBoxRec.TabIndex = 6;
            checkBoxRec.Text = "REC";
            checkBoxRec.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxRec.UseVisualStyleBackColor = true;
            checkBoxRec.CheckedChanged += checkBoxRec_CheckedChanged;
            // 
            // labelFPS
            // 
            labelFPS.AutoSize = true;
            labelFPS.Font = new Font("Yu Gothic UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            labelFPS.Location = new Point(2, 0);
            labelFPS.Margin = new Padding(2, 0, 2, 0);
            labelFPS.Name = "labelFPS";
            labelFPS.Size = new Size(83, 37);
            labelFPS.TabIndex = 1;
            labelFPS.Text = "00.00";
            // 
            // buttonGrabStop
            // 
            buttonGrabStop.Location = new Point(596, 36);
            buttonGrabStop.Margin = new Padding(2);
            buttonGrabStop.Name = "buttonGrabStop";
            buttonGrabStop.Size = new Size(127, 43);
            buttonGrabStop.TabIndex = 5;
            buttonGrabStop.Text = "取得終了";
            buttonGrabStop.UseVisualStyleBackColor = true;
            buttonGrabStop.Click += buttonGrabStop_Click;
            // 
            // buttonGrabStart
            // 
            buttonGrabStart.Location = new Point(465, 36);
            buttonGrabStart.Margin = new Padding(2);
            buttonGrabStart.Name = "buttonGrabStart";
            buttonGrabStart.Size = new Size(127, 43);
            buttonGrabStart.TabIndex = 4;
            buttonGrabStart.Text = "取得開始";
            buttonGrabStart.UseVisualStyleBackColor = true;
            buttonGrabStart.Click += buttonGrabStart_Click;
            // 
            // labelRec
            // 
            labelRec.AutoSize = true;
            labelRec.BackColor = Color.Transparent;
            labelRec.Font = new Font("Yu Gothic UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            labelRec.ForeColor = Color.Red;
            labelRec.Location = new Point(8, 5);
            labelRec.Margin = new Padding(2, 0, 2, 0);
            labelRec.Name = "labelRec";
            labelRec.Size = new Size(44, 37);
            labelRec.TabIndex = 3;
            labelRec.Text = "●";
            labelRec.Visible = false;
            // 
            // clarityImageViewer1
            // 
            clarityImageViewer1.BorderLineColor = Color.Red;
            clarityImageViewer1.BorderLineRendering = false;
            clarityImageViewer1.ClearColor = Color.Black;
            clarityImageViewer1.DisplayAreaLineColor = Color.Red;
            clarityImageViewer1.DisplayAreaLineWidth = 1F;
            clarityImageViewer1.DisplayerRendering = true;
            clarityImageViewer1.Dock = DockStyle.Fill;
            clarityImageViewer1.DoubleClickFitCentering = true;
            clarityImageViewer1.ImageClippingEnabled = true;
            clarityImageViewer1.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            clarityImageViewer1.Location = new Point(0, 0);
            clarityImageViewer1.MinimapBackColor = Color.White;
            clarityImageViewer1.MinimapDisplayMerginRate = 0.05F;
            clarityImageViewer1.MinimapVisible = true;
            clarityImageViewer1.Name = "clarityImageViewer1";
            clarityImageViewer1.PosMode = Clarity.GUI.EClarityImageViewerPositionMode.LeftTop;
            clarityImageViewer1.Size = new Size(909, 423);
            clarityImageViewer1.SrcBackColor = Color.Red;
            clarityImageViewer1.TabIndex = 4;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(909, 524);
            Controls.Add(labelRec);
            Controls.Add(clarityImageViewer1);
            Controls.Add(panelControl);
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "ClarityCameraViewer";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            panelControl.ResumeLayout(false);
            panelControl.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDeviceID).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFPS).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageHeight).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownImageWidth).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelControl;
        private Button buttonGrabStop;
        private Button buttonGrabStart;
        private Label labelFPS;
        private CheckBox checkBoxRec;
        private Label labelRec;
        private Clarity.GUI.ClarityImageViewer clarityImageViewer1;
        private Label label3;
        private Label label2;
        private NumericUpDown numericUpDownFPS;
        private NumericUpDown numericUpDownImageHeight;
        private Label label1;
        private NumericUpDown numericUpDownImageWidth;
        private NumericUpDown numericUpDownDeviceID;
        private Panel panel1;
        private RadioButton radioButtonCameraPath;
        private RadioButton radioButtonCameraID;
        private TextBox textBoxCameraPath;
    }
}