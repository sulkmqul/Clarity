
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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.閉じるToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.アニメ定義ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ログ表示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelImageControl = new System.Windows.Forms.Panel();
            this.layerSettingControl1 = new ClarityEmotion.LayerSetting.LayerSettingControl();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelMainView = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.animeEditViewControl1 = new ClarityEmotion.EditView.AnimeEditViewControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panelAnimeConrol = new System.Windows.Forms.Panel();
            this.layerAnimeControl1 = new ClarityEmotion.LayerControl.LayerAnimeControl();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.layer初期化ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panelImageControl.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelMainView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelAnimeConrol.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルToolStripMenuItem,
            this.編集ToolStripMenuItem,
            this.ツールToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1264, 29);
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
            this.toolStripSeparator2,
            this.閉じるToolStripMenuItem});
            this.ファイルToolStripMenuItem.Name = "ファイルToolStripMenuItem";
            this.ファイルToolStripMenuItem.Size = new System.Drawing.Size(67, 25);
            this.ファイルToolStripMenuItem.Text = "ファイル";
            // 
            // 新規作成ToolStripMenuItem
            // 
            this.新規作成ToolStripMenuItem.Name = "新規作成ToolStripMenuItem";
            this.新規作成ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.新規作成ToolStripMenuItem.Text = "新規作成";
            this.新規作成ToolStripMenuItem.Click += new System.EventHandler(this.新規作成ToolStripMenuItem_Click);
            // 
            // 開くToolStripMenuItem
            // 
            this.開くToolStripMenuItem.Name = "開くToolStripMenuItem";
            this.開くToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.開くToolStripMenuItem.Text = "開く";
            this.開くToolStripMenuItem.Click += new System.EventHandler(this.開くToolStripMenuItem_Click);
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.保存ToolStripMenuItem.Text = "保存";
            this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // 出力ToolStripMenuItem
            // 
            this.出力ToolStripMenuItem.Name = "出力ToolStripMenuItem";
            this.出力ToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.出力ToolStripMenuItem.Text = "出力";
            this.出力ToolStripMenuItem.Click += new System.EventHandler(this.出力ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(141, 6);
            // 
            // 閉じるToolStripMenuItem
            // 
            this.閉じるToolStripMenuItem.Name = "閉じるToolStripMenuItem";
            this.閉じるToolStripMenuItem.Size = new System.Drawing.Size(144, 26);
            this.閉じるToolStripMenuItem.Text = "閉じる";
            this.閉じるToolStripMenuItem.Click += new System.EventHandler(this.閉じるToolStripMenuItem_Click);
            // 
            // 編集ToolStripMenuItem
            // 
            this.編集ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.アニメ定義ToolStripMenuItem,
            this.設定ToolStripMenuItem,
            this.layer初期化ToolStripMenuItem});
            this.編集ToolStripMenuItem.Name = "編集ToolStripMenuItem";
            this.編集ToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
            this.編集ToolStripMenuItem.Text = "編集";
            // 
            // アニメ定義ToolStripMenuItem
            // 
            this.アニメ定義ToolStripMenuItem.Name = "アニメ定義ToolStripMenuItem";
            this.アニメ定義ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.アニメ定義ToolStripMenuItem.Text = "アニメ定義";
            this.アニメ定義ToolStripMenuItem.Click += new System.EventHandler(this.アニメ定義ToolStripMenuItem_Click);
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.設定ToolStripMenuItem.Text = "アニメ設定";
            this.設定ToolStripMenuItem.Click += new System.EventHandler(this.設定ToolStripMenuItem_Click);
            // 
            // ツールToolStripMenuItem
            // 
            this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ログ表示ToolStripMenuItem});
            this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
            this.ツールToolStripMenuItem.Size = new System.Drawing.Size(57, 25);
            this.ツールToolStripMenuItem.Text = "ツール";
            // 
            // ログ表示ToolStripMenuItem
            // 
            this.ログ表示ToolStripMenuItem.Name = "ログ表示ToolStripMenuItem";
            this.ログ表示ToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.ログ表示ToolStripMenuItem.Text = "ログ表示";
            // 
            // panelImageControl
            // 
            this.panelImageControl.Controls.Add(this.layerSettingControl1);
            this.panelImageControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelImageControl.Location = new System.Drawing.Point(914, 0);
            this.panelImageControl.Name = "panelImageControl";
            this.panelImageControl.Size = new System.Drawing.Size(350, 629);
            this.panelImageControl.TabIndex = 1;
            // 
            // layerSettingControl1
            // 
            this.layerSettingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerSettingControl1.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.layerSettingControl1.Location = new System.Drawing.Point(0, 0);
            this.layerSettingControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layerSettingControl1.Name = "layerSettingControl1";
            this.layerSettingControl1.Size = new System.Drawing.Size(350, 629);
            this.layerSettingControl1.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelMainView);
            this.panelMain.Controls.Add(this.splitter2);
            this.panelMain.Controls.Add(this.panelAnimeConrol);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 29);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1264, 832);
            this.panelMain.TabIndex = 3;
            // 
            // panelMainView
            // 
            this.panelMainView.AutoScroll = true;
            this.panelMainView.Controls.Add(this.panel1);
            this.panelMainView.Controls.Add(this.splitter1);
            this.panelMainView.Controls.Add(this.panelImageControl);
            this.panelMainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainView.Location = new System.Drawing.Point(0, 0);
            this.panelMainView.Name = "panelMainView";
            this.panelMainView.Size = new System.Drawing.Size(1264, 629);
            this.panelMainView.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.animeEditViewControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(911, 629);
            this.panel1.TabIndex = 3;
            // 
            // animeEditViewControl1
            // 
            this.animeEditViewControl1.AutoScroll = true;
            this.animeEditViewControl1.Location = new System.Drawing.Point(0, 0);
            this.animeEditViewControl1.Margin = new System.Windows.Forms.Padding(4);
            this.animeEditViewControl1.Name = "animeEditViewControl1";
            this.animeEditViewControl1.Size = new System.Drawing.Size(201, 200);
            this.animeEditViewControl1.TabIndex = 0;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(911, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 629);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 629);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1264, 3);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // panelAnimeConrol
            // 
            this.panelAnimeConrol.Controls.Add(this.layerAnimeControl1);
            this.panelAnimeConrol.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelAnimeConrol.Location = new System.Drawing.Point(0, 632);
            this.panelAnimeConrol.Name = "panelAnimeConrol";
            this.panelAnimeConrol.Size = new System.Drawing.Size(1264, 200);
            this.panelAnimeConrol.TabIndex = 0;
            // 
            // layerAnimeControl1
            // 
            this.layerAnimeControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerAnimeControl1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.layerAnimeControl1.Location = new System.Drawing.Point(0, 0);
            this.layerAnimeControl1.Margin = new System.Windows.Forms.Padding(4);
            this.layerAnimeControl1.Name = "layerAnimeControl1";
            this.layerAnimeControl1.Size = new System.Drawing.Size(1264, 200);
            this.layerAnimeControl1.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // layer初期化ToolStripMenuItem
            // 
            this.layer初期化ToolStripMenuItem.Name = "layer初期化ToolStripMenuItem";
            this.layer初期化ToolStripMenuItem.Size = new System.Drawing.Size(180, 26);
            this.layer初期化ToolStripMenuItem.Text = "Layer初期化";
            this.layer初期化ToolStripMenuItem.Click += new System.EventHandler(this.layer初期化ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 861);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clarity E-Motion";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelImageControl.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMainView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelAnimeConrol.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新規作成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 出力ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 閉じるToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 編集ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem アニメ定義ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ツールToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ログ表示ToolStripMenuItem;
        private System.Windows.Forms.Panel panelImageControl;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelMainView;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panelAnimeConrol;
        private System.Windows.Forms.Splitter splitter1;
        internal LayerControl.LayerAnimeControl layerAnimeControl1;
        internal EditView.AnimeEditViewControl animeEditViewControl1;
        internal LayerSetting.LayerSettingControl layerSettingControl1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layer初期化ToolStripMenuItem;
    }
}

