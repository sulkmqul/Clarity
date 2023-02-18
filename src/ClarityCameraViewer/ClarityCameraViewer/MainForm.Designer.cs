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
            this.panelControl = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownFPS = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownImageHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownImageWidth = new System.Windows.Forms.NumericUpDown();
            this.checkBoxRec = new System.Windows.Forms.CheckBox();
            this.labelFPS = new System.Windows.Forms.Label();
            this.buttonGrabStop = new System.Windows.Forms.Button();
            this.buttonGrabStart = new System.Windows.Forms.Button();
            this.labelRec = new System.Windows.Forms.Label();
            this.clarityImageViewer1 = new Clarity.GUI.ClarityImageViewer();
            this.numericUpDownDeviceID = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeviceID)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.label4);
            this.panelControl.Controls.Add(this.numericUpDownDeviceID);
            this.panelControl.Controls.Add(this.label3);
            this.panelControl.Controls.Add(this.label2);
            this.panelControl.Controls.Add(this.numericUpDownFPS);
            this.panelControl.Controls.Add(this.numericUpDownImageHeight);
            this.panelControl.Controls.Add(this.label1);
            this.panelControl.Controls.Add(this.numericUpDownImageWidth);
            this.panelControl.Controls.Add(this.checkBoxRec);
            this.panelControl.Controls.Add(this.labelFPS);
            this.panelControl.Controls.Add(this.buttonGrabStop);
            this.panelControl.Controls.Add(this.buttonGrabStart);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 434);
            this.panelControl.Margin = new System.Windows.Forms.Padding(2);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(855, 101);
            this.panelControl.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "FPS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "H";
            // 
            // numericUpDownFPS
            // 
            this.numericUpDownFPS.Location = new System.Drawing.Point(299, 48);
            this.numericUpDownFPS.Name = "numericUpDownFPS";
            this.numericUpDownFPS.Size = new System.Drawing.Size(40, 23);
            this.numericUpDownFPS.TabIndex = 3;
            this.numericUpDownFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownFPS.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // numericUpDownImageHeight
            // 
            this.numericUpDownImageHeight.Location = new System.Drawing.Point(206, 48);
            this.numericUpDownImageHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownImageHeight.Name = "numericUpDownImageHeight";
            this.numericUpDownImageHeight.Size = new System.Drawing.Size(55, 23);
            this.numericUpDownImageHeight.TabIndex = 2;
            this.numericUpDownImageHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownImageHeight.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "W";
            // 
            // numericUpDownImageWidth
            // 
            this.numericUpDownImageWidth.Location = new System.Drawing.Point(121, 48);
            this.numericUpDownImageWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownImageWidth.Name = "numericUpDownImageWidth";
            this.numericUpDownImageWidth.Size = new System.Drawing.Size(55, 23);
            this.numericUpDownImageWidth.TabIndex = 1;
            this.numericUpDownImageWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDownImageWidth.Value = new decimal(new int[] {
            640,
            0,
            0,
            0});
            // 
            // checkBoxRec
            // 
            this.checkBoxRec.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxRec.Location = new System.Drawing.Point(717, 36);
            this.checkBoxRec.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxRec.Name = "checkBoxRec";
            this.checkBoxRec.Size = new System.Drawing.Size(127, 43);
            this.checkBoxRec.TabIndex = 6;
            this.checkBoxRec.Text = "REC";
            this.checkBoxRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxRec.UseVisualStyleBackColor = true;
            this.checkBoxRec.CheckedChanged += new System.EventHandler(this.checkBoxRec_CheckedChanged);
            // 
            // labelFPS
            // 
            this.labelFPS.AutoSize = true;
            this.labelFPS.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFPS.Location = new System.Drawing.Point(2, 0);
            this.labelFPS.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(83, 37);
            this.labelFPS.TabIndex = 1;
            this.labelFPS.Text = "00.00";
            // 
            // buttonGrabStop
            // 
            this.buttonGrabStop.Location = new System.Drawing.Point(497, 36);
            this.buttonGrabStop.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGrabStop.Name = "buttonGrabStop";
            this.buttonGrabStop.Size = new System.Drawing.Size(127, 43);
            this.buttonGrabStop.TabIndex = 5;
            this.buttonGrabStop.Text = "取得終了";
            this.buttonGrabStop.UseVisualStyleBackColor = true;
            this.buttonGrabStop.Click += new System.EventHandler(this.buttonGrabStop_Click);
            // 
            // buttonGrabStart
            // 
            this.buttonGrabStart.Location = new System.Drawing.Point(366, 36);
            this.buttonGrabStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGrabStart.Name = "buttonGrabStart";
            this.buttonGrabStart.Size = new System.Drawing.Size(127, 43);
            this.buttonGrabStart.TabIndex = 4;
            this.buttonGrabStart.Text = "取得開始";
            this.buttonGrabStart.UseVisualStyleBackColor = true;
            this.buttonGrabStart.Click += new System.EventHandler(this.buttonGrabStart_Click);
            // 
            // labelRec
            // 
            this.labelRec.AutoSize = true;
            this.labelRec.BackColor = System.Drawing.Color.Transparent;
            this.labelRec.Font = new System.Drawing.Font("Yu Gothic UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelRec.ForeColor = System.Drawing.Color.Red;
            this.labelRec.Location = new System.Drawing.Point(8, 5);
            this.labelRec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelRec.Name = "labelRec";
            this.labelRec.Size = new System.Drawing.Size(44, 37);
            this.labelRec.TabIndex = 3;
            this.labelRec.Text = "●";
            this.labelRec.Visible = false;
            // 
            // clarityImageViewer1
            // 
            this.clarityImageViewer1.BorderLineColor = System.Drawing.Color.Red;
            this.clarityImageViewer1.BorderLineRendering = false;
            this.clarityImageViewer1.ClearColor = System.Drawing.Color.Black;
            this.clarityImageViewer1.DisplayAreaLineColor = System.Drawing.Color.Red;
            this.clarityImageViewer1.DisplayAreaLineWidth = 1F;
            this.clarityImageViewer1.DisplayerRendering = true;
            this.clarityImageViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clarityImageViewer1.DoubleClickFitCentering = true;
            this.clarityImageViewer1.ImageClippingEnabled = true;
            this.clarityImageViewer1.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.clarityImageViewer1.Location = new System.Drawing.Point(0, 0);
            this.clarityImageViewer1.MinimapBackColor = System.Drawing.Color.White;
            this.clarityImageViewer1.MinimapDisplayMerginRate = 0.05F;
            this.clarityImageViewer1.MinimapVisible = true;
            this.clarityImageViewer1.Name = "clarityImageViewer1";
            this.clarityImageViewer1.PosMode = Clarity.GUI.EClarityImageViewerPositionMode.LeftTop;
            this.clarityImageViewer1.Size = new System.Drawing.Size(855, 434);
            this.clarityImageViewer1.SrcBackColor = System.Drawing.Color.Red;
            this.clarityImageViewer1.TabIndex = 4;
            // 
            // numericUpDownDeviceID
            // 
            this.numericUpDownDeviceID.Location = new System.Drawing.Point(50, 48);
            this.numericUpDownDeviceID.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDeviceID.Name = "numericUpDownDeviceID";
            this.numericUpDownDeviceID.Size = new System.Drawing.Size(35, 23);
            this.numericUpDownDeviceID.TabIndex = 0;
            this.numericUpDownDeviceID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "DID";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 535);
            this.Controls.Add(this.labelRec);
            this.Controls.Add(this.clarityImageViewer1);
            this.Controls.Add(this.panelControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "ClarityCameraViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDeviceID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Label label4;
        private NumericUpDown numericUpDownDeviceID;
    }
}