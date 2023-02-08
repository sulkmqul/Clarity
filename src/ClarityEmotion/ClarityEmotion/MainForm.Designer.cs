namespace ClarityEmotion
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新規作成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.出力ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.出力連番ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.アニメ定義ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.アニメーション定義ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ログ表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelLayerDock = new System.Windows.Forms.Panel();
            this.layerControl1 = new ClarityEmotion.LayerControl.LayerControl();
            this.panelMainDock = new System.Windows.Forms.Panel();
            this.panelMainViewDock = new System.Windows.Forms.Panel();
            this.clarityViewer1 = new Clarity.GUI.ClarityImageViewer();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panelControlDock = new System.Windows.Forms.Panel();
            this.layerSettingControl1 = new ClarityEmotion.LayerControl.LayerSettingControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.menuStrip1.SuspendLayout();
            this.panelLayerDock.SuspendLayout();
            this.panelMainDock.SuspendLayout();
            this.panelMainViewDock.SuspendLayout();
            this.panelControlDock.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.アニメ定義ToolStripMenuItem,
            this.ツールToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルToolStripMenuItem
            // 
            this.ファイルToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成ToolStripMenuItem,
            this.開くToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.toolStripSeparator1,
            this.出力ToolStripMenuItem,
            this.出力連番ToolStripMenuItem,
            this.toolStripSeparator2,
            this.閉じるToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成ToolStripMenuItem
            // 
            this.新規作成ToolStripMenuItem.Name = "新規作成ToolStripMenuItem";
            this.新規作成ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.新規作成ToolStripMenuItem.Text = "新規作成";
            this.新規作成ToolStripMenuItem.Click += new System.EventHandler(this.新規作成ToolStripMenuItem_Click);
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.開くToolStripMenuItem.Text = "開く";
            this.開くToolStripMenuItem.Click += new System.EventHandler(this.開くToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // 出力ToolStripMenuItem
            // 
            this.出力ToolStripMenuItem.Name = "出力ToolStripMenuItem";
            this.出力ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.出力ToolStripMenuItem.Text = "出力";
            this.出力ToolStripMenuItem.Click += new System.EventHandler(this.出力ToolStripMenuItem_Click);
            // 
            // 出力連番ToolStripMenuItem
            // 
            this.出力連番ToolStripMenuItem.Name = "出力連番ToolStripMenuItem";
            this.出力連番ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.出力連番ToolStripMenuItem.Text = "出力（連番ファイル）";
            this.出力連番ToolStripMenuItem.Click += new System.EventHandler(this.出力連番ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // 閉じるToolStripMenuItem
            // 
            this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
            this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.閉じるToolStripMenuItem.Text = "閉じる(&X)";
            this.閉じるToolStripMenuItem.Click += new System.EventHandler(this.閉じるToolStripMenuItem_Click);
            // 
            // アニメ定義ToolStripMenuItem
            // 
            this.アニメ定義ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アニメーション定義ToolStripMenuItem,
            this.設定ToolStripMenuItem});
            this.アニメ定義ToolStripMenuItem.Name = "アニメ定義ToolStripMenuItem";
            this.アニメ定義ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.アニメ定義ToolStripMenuItem.Text = "編集";
            // 
            // アニメーション定義ToolStripMenuItem
            // 
            this.アニメーション定義ToolStripMenuItem.Name = "アニメーション定義ToolStripMenuItem";
            this.アニメーション定義ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.アニメーション定義ToolStripMenuItem.Text = "アニメーション定義";
            this.アニメーション定義ToolStripMenuItem.Click += new System.EventHandler(this.アニメーション定義ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.設定ToolStripMenuItem.Text = "設定";
            this.設定ToolStripMenuItem.Click += new System.EventHandler(this.設定ToolStripMenuItem_Click);
            // 
            // ツールToolStripMenuItem
            // 
            this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ログ表示ToolStripMenuItem});
            this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
            this.ツールToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.ツールToolStripMenuItem.Text = "ツール";
            // 
            // ログ表示ToolStripMenuItem
            // 
            this.ログ表示ToolStripMenuItem.Name = "ログ表示ToolStripMenuItem";
            this.ログ表示ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ログ表示ToolStripMenuItem.Text = "ログ表示";
            this.ログ表示ToolStripMenuItem.Click += new System.EventHandler(this.ログ表示ToolStripMenuItem_Click);
            // 
            // panelLayerDock
            // 
            this.panelLayerDock.Controls.Add(this.layerControl1);
            this.panelLayerDock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLayerDock.Location = new System.Drawing.Point(0, 311);
            this.panelLayerDock.Name = "panelLayerDock";
            this.panelLayerDock.Size = new System.Drawing.Size(784, 250);
            this.panelLayerDock.TabIndex = 1;
            // 
            // layerControl1
            // 
            this.layerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerControl1.Location = new System.Drawing.Point(0, 0);
            this.layerControl1.Name = "layerControl1";
            this.layerControl1.Size = new System.Drawing.Size(784, 250);
            this.layerControl1.TabIndex = 0;
            // 
            // panelMainDock
            // 
            this.panelMainDock.Controls.Add(this.panelMainViewDock);
            this.panelMainDock.Controls.Add(this.splitter2);
            this.panelMainDock.Controls.Add(this.panelControlDock);
            this.panelMainDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainDock.Location = new System.Drawing.Point(0, 24);
            this.panelMainDock.Name = "panelMainDock";
            this.panelMainDock.Size = new System.Drawing.Size(784, 287);
            this.panelMainDock.TabIndex = 2;
            // 
            // panelMainViewDock
            // 
            this.panelMainViewDock.Controls.Add(this.clarityViewer1);
            this.panelMainViewDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainViewDock.Location = new System.Drawing.Point(0, 0);
            this.panelMainViewDock.Name = "panelMainViewDock";
            this.panelMainViewDock.Size = new System.Drawing.Size(531, 287);
            this.panelMainViewDock.TabIndex = 3;
            // 
            // clarityViewer1
            // 
            this.clarityViewer1.BorderLineColor = System.Drawing.Color.Aqua;
            this.clarityViewer1.BorderLineRendering = true;
            this.clarityViewer1.ClearColor = System.Drawing.SystemColors.Control;
            this.clarityViewer1.DisplayAreaLineColor = System.Drawing.Color.Red;
            this.clarityViewer1.DisplayAreaLineWidth = 1F;
            this.clarityViewer1.DisplayerRendering = true;
            this.clarityViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clarityViewer1.DoubleClickFitCentering = true;
            this.clarityViewer1.ImageClippingEnabled = true;
            this.clarityViewer1.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.clarityViewer1.Location = new System.Drawing.Point(0, 0);
            this.clarityViewer1.MinimapBackColor = System.Drawing.Color.Black;
            this.clarityViewer1.MinimapDisplayMerginRate = 0.05F;
            this.clarityViewer1.MinimapVisible = false;
            this.clarityViewer1.Name = "clarityViewer1";
            this.clarityViewer1.PosMode = Clarity.GUI.EClarityImageViewerPositionMode.LeftTop;
            this.clarityViewer1.Size = new System.Drawing.Size(531, 287);
            this.clarityViewer1.SrcBackColor = System.Drawing.Color.Black;
            this.clarityViewer1.TabIndex = 0;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(531, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 287);
            this.splitter2.TabIndex = 2;
            this.splitter2.TabStop = false;
            // 
            // panelControlDock
            // 
            this.panelControlDock.Controls.Add(this.layerSettingControl1);
            this.panelControlDock.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControlDock.Location = new System.Drawing.Point(534, 0);
            this.panelControlDock.Name = "panelControlDock";
            this.panelControlDock.Size = new System.Drawing.Size(250, 287);
            this.panelControlDock.TabIndex = 1;
            // 
            // layerSettingControl1
            // 
            this.layerSettingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerSettingControl1.Location = new System.Drawing.Point(0, 0);
            this.layerSettingControl1.Name = "layerSettingControl1";
            this.layerSettingControl1.Size = new System.Drawing.Size(250, 287);
            this.layerSettingControl1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 308);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(784, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelMainDock);
            this.Controls.Add(this.panelLayerDock);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Clarity E-Motion";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelLayerDock.ResumeLayout(false);
            this.panelMainDock.ResumeLayout(false);
            this.panelMainViewDock.ResumeLayout(false);
            this.panelControlDock.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルToolStripMenuItem;
        private ToolStripMenuItem 新規作成ToolStripMenuItem;
        private ToolStripMenuItem 開くToolStripMenuItem;
        private ToolStripMenuItem 保存ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 出力ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem 閉じるToolStripMenuItem;
        private ToolStripMenuItem アニメ定義ToolStripMenuItem;
        private ToolStripMenuItem アニメーション定義ToolStripMenuItem;
        private ToolStripMenuItem 設定ToolStripMenuItem;
        private ToolStripMenuItem ツールToolStripMenuItem;
        private ToolStripMenuItem ログ表示ToolStripMenuItem;
        private Panel panelLayerDock;
        private Panel panelMainDock;
        private Splitter splitter1;
        private Panel panelMainViewDock;
        private Splitter splitter2;
        private Panel panelControlDock;
        private LayerControl.LayerControl layerControl1;
        private LayerControl.LayerSettingControl layerSettingControl1;
        private Clarity.GUI.ClarityImageViewer clarityViewer1;
        private ToolStripMenuItem 出力連番ToolStripMenuItem;
    }
}