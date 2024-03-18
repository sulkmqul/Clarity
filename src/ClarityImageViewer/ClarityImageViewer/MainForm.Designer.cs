namespace ClarityImageViewer
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
            splitter1 = new Splitter();
            panelInfomationDock = new Panel();
            panelInfo = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            textBoxImageSize = new TextBox();
            label2 = new Label();
            textBoxFrameCount = new TextBox();
            label3 = new Label();
            textBoxFramePosition = new TextBox();
            label4 = new Label();
            textBoxInfomation = new TextBox();
            buttonInfoHiding = new Button();
            dxControl1 = new Viewer.DxControl();
            panelBottomBar = new Panel();
            labelScaleRate = new Label();
            buttonScaleFit = new Button();
            buttonScaleOriginal = new Button();
            panelDisplay = new Panel();
            buttonRotateImage = new Button();
            panelInfomationDock.SuspendLayout();
            panelInfo.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            panelBottomBar.SuspendLayout();
            panelDisplay.SuspendLayout();
            SuspendLayout();
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(479, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 412);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // panelInfomationDock
            // 
            panelInfomationDock.AutoSize = true;
            panelInfomationDock.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelInfomationDock.BackColor = SystemColors.ActiveCaption;
            panelInfomationDock.Controls.Add(panelInfo);
            panelInfomationDock.Controls.Add(buttonInfoHiding);
            panelInfomationDock.Dock = DockStyle.Right;
            panelInfomationDock.Location = new Point(482, 0);
            panelInfomationDock.Name = "panelInfomationDock";
            panelInfomationDock.Size = new Size(222, 412);
            panelInfomationDock.TabIndex = 1;
            // 
            // panelInfo
            // 
            panelInfo.Controls.Add(flowLayoutPanel1);
            panelInfo.Dock = DockStyle.Right;
            panelInfo.Location = new Point(22, 0);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(200, 412);
            panelInfo.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(textBoxImageSize);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(textBoxFrameCount);
            flowLayoutPanel1.Controls.Add(label3);
            flowLayoutPanel1.Controls.Add(textBoxFramePosition);
            flowLayoutPanel1.Controls.Add(label4);
            flowLayoutPanel1.Controls.Add(textBoxInfomation);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(200, 412);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 1;
            label1.Text = "ImageSize";
            // 
            // textBoxImageSize
            // 
            textBoxImageSize.Location = new Point(3, 18);
            textBoxImageSize.Name = "textBoxImageSize";
            textBoxImageSize.ReadOnly = true;
            textBoxImageSize.Size = new Size(194, 23);
            textBoxImageSize.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 44);
            label2.Name = "label2";
            label2.Size = new Size(70, 15);
            label2.TabIndex = 3;
            label2.Text = "FrameCount";
            // 
            // textBoxFrameCount
            // 
            textBoxFrameCount.Location = new Point(3, 62);
            textBoxFrameCount.Name = "textBoxFrameCount";
            textBoxFrameCount.ReadOnly = true;
            textBoxFrameCount.Size = new Size(194, 23);
            textBoxFrameCount.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 88);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 4;
            label3.Text = "FramePosition";
            // 
            // textBoxFramePosition
            // 
            textBoxFramePosition.Location = new Point(3, 106);
            textBoxFramePosition.Name = "textBoxFramePosition";
            textBoxFramePosition.ReadOnly = true;
            textBoxFramePosition.Size = new Size(194, 23);
            textBoxFramePosition.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 132);
            label4.Name = "label4";
            label4.Size = new Size(65, 15);
            label4.TabIndex = 6;
            label4.Text = "Infomation";
            // 
            // textBoxInfomation
            // 
            textBoxInfomation.Location = new Point(3, 150);
            textBoxInfomation.Multiline = true;
            textBoxInfomation.Name = "textBoxInfomation";
            textBoxInfomation.ReadOnly = true;
            textBoxInfomation.Size = new Size(194, 211);
            textBoxInfomation.TabIndex = 7;
            // 
            // buttonInfoHiding
            // 
            buttonInfoHiding.Dock = DockStyle.Left;
            buttonInfoHiding.Location = new Point(0, 0);
            buttonInfoHiding.Name = "buttonInfoHiding";
            buttonInfoHiding.Size = new Size(22, 412);
            buttonInfoHiding.TabIndex = 0;
            buttonInfoHiding.Text = "◀";
            buttonInfoHiding.UseVisualStyleBackColor = true;
            buttonInfoHiding.Click += buttonInfoHiding_Click;
            // 
            // dxControl1
            // 
            dxControl1.AllowDrop = true;
            dxControl1.Dock = DockStyle.Fill;
            dxControl1.Location = new Point(0, 0);
            dxControl1.Name = "dxControl1";
            dxControl1.Size = new Size(479, 412);
            dxControl1.TabIndex = 3;
            dxControl1.DragDrop += dxControl1_DragDrop;
            dxControl1.DragEnter += dxControl1_DragEnter;
            // 
            // panelBottomBar
            // 
            panelBottomBar.Controls.Add(buttonRotateImage);
            panelBottomBar.Controls.Add(labelScaleRate);
            panelBottomBar.Controls.Add(buttonScaleFit);
            panelBottomBar.Controls.Add(buttonScaleOriginal);
            panelBottomBar.Dock = DockStyle.Bottom;
            panelBottomBar.Location = new Point(0, 412);
            panelBottomBar.Name = "panelBottomBar";
            panelBottomBar.Size = new Size(704, 29);
            panelBottomBar.TabIndex = 0;
            // 
            // labelScaleRate
            // 
            labelScaleRate.Dock = DockStyle.Right;
            labelScaleRate.Location = new Point(565, 0);
            labelScaleRate.Name = "labelScaleRate";
            labelScaleRate.Size = new Size(59, 29);
            labelScaleRate.TabIndex = 2;
            labelScaleRate.Text = "100%";
            labelScaleRate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // buttonScaleFit
            // 
            buttonScaleFit.Dock = DockStyle.Right;
            buttonScaleFit.Location = new Point(624, 0);
            buttonScaleFit.Name = "buttonScaleFit";
            buttonScaleFit.Size = new Size(40, 29);
            buttonScaleFit.TabIndex = 1;
            buttonScaleFit.Text = "Fit";
            buttonScaleFit.UseVisualStyleBackColor = true;
            buttonScaleFit.Click += buttonScaleFit_Click;
            // 
            // buttonScaleOriginal
            // 
            buttonScaleOriginal.Dock = DockStyle.Right;
            buttonScaleOriginal.Location = new Point(664, 0);
            buttonScaleOriginal.Name = "buttonScaleOriginal";
            buttonScaleOriginal.Size = new Size(40, 29);
            buttonScaleOriginal.TabIndex = 0;
            buttonScaleOriginal.Text = "1:1";
            buttonScaleOriginal.UseVisualStyleBackColor = true;
            buttonScaleOriginal.Click += buttonScaleOriginal_Click;
            // 
            // panelDisplay
            // 
            panelDisplay.Controls.Add(dxControl1);
            panelDisplay.Controls.Add(splitter1);
            panelDisplay.Controls.Add(panelInfomationDock);
            panelDisplay.Dock = DockStyle.Fill;
            panelDisplay.Location = new Point(0, 0);
            panelDisplay.Name = "panelDisplay";
            panelDisplay.Size = new Size(704, 412);
            panelDisplay.TabIndex = 0;
            // 
            // buttonRotateImage
            // 
            buttonRotateImage.Dock = DockStyle.Right;
            buttonRotateImage.Location = new Point(525, 0);
            buttonRotateImage.Name = "buttonRotateImage";
            buttonRotateImage.Size = new Size(40, 29);
            buttonRotateImage.TabIndex = 3;
            buttonRotateImage.Text = "⤵";
            buttonRotateImage.UseVisualStyleBackColor = true;
            buttonRotateImage.Click += buttonRotateImage_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(704, 441);
            Controls.Add(panelDisplay);
            Controls.Add(panelBottomBar);
            Name = "MainForm";
            Text = "Clarity Image Viewer";
            FormClosing += MainForm_FormClosing;
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            panelInfomationDock.ResumeLayout(false);
            panelInfo.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            panelBottomBar.ResumeLayout(false);
            panelDisplay.ResumeLayout(false);
            panelDisplay.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Splitter splitter1;
        private Panel panelInfomationDock;
        private Viewer.DxControl dxControl1;
        private Panel panelBottomBar;
        private Panel panelDisplay;
        private Button buttonScaleFit;
        private Button buttonScaleOriginal;
        private Panel panelInfo;
        private Button buttonInfoHiding;
        private Label labelScaleRate;
        private FlowLayoutPanel flowLayoutPanel1;
        private TextBox textBoxImageSize;
        private Label label1;
        private Label label2;
        private TextBox textBoxFrameCount;
        private Label label3;
        private TextBox textBoxFramePosition;
        private Label label4;
        private TextBox textBoxInfomation;
        private Button buttonRotateImage;
    }
}