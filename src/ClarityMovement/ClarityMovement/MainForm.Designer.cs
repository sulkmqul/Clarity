namespace ClarityMovement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelMainView = new System.Windows.Forms.Panel();
            this.clarityDxViewer1 = new ClarityMovement.Viewer.ClarityEngineViewer();
            this.panelContoller = new System.Windows.Forms.Panel();
            this.panelEditorScroll = new System.Windows.Forms.Panel();
            this.frameEditControlEditor = new ClarityMovement.FrameEdit.FrameEditControl();
            this.toolStripEditor = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonEditZoomPlus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditZoomMinus = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新規作成NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.開くOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存WToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.出力EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.閉じるXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.フレーム画像追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMainView.SuspendLayout();
            this.panelContoller.SuspendLayout();
            this.panelEditorScroll.SuspendLayout();
            this.toolStripEditor.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 358);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(784, 3);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // panelMainView
            // 
            this.panelMainView.Controls.Add(this.clarityDxViewer1);
            this.panelMainView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainView.Location = new System.Drawing.Point(0, 0);
            this.panelMainView.Name = "panelMainView";
            this.panelMainView.Size = new System.Drawing.Size(784, 358);
            this.panelMainView.TabIndex = 1;
            // 
            // clarityDxViewer1
            // 
            this.clarityDxViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clarityDxViewer1.Location = new System.Drawing.Point(0, 0);
            this.clarityDxViewer1.Name = "clarityDxViewer1";
            this.clarityDxViewer1.Size = new System.Drawing.Size(784, 358);
            this.clarityDxViewer1.TabIndex = 0;
            // 
            // panelContoller
            // 
            this.panelContoller.Controls.Add(this.panelEditorScroll);
            this.panelContoller.Controls.Add(this.toolStripEditor);
            this.panelContoller.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelContoller.Location = new System.Drawing.Point(0, 361);
            this.panelContoller.Name = "panelContoller";
            this.panelContoller.Size = new System.Drawing.Size(784, 200);
            this.panelContoller.TabIndex = 0;
            // 
            // panelEditorScroll
            // 
            this.panelEditorScroll.AutoScroll = true;
            this.panelEditorScroll.Controls.Add(this.frameEditControlEditor);
            this.panelEditorScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEditorScroll.Location = new System.Drawing.Point(0, 25);
            this.panelEditorScroll.Name = "panelEditorScroll";
            this.panelEditorScroll.Size = new System.Drawing.Size(784, 175);
            this.panelEditorScroll.TabIndex = 2;
            // 
            // frameEditControlEditor
            // 
            this.frameEditControlEditor.Location = new System.Drawing.Point(3, 3);
            this.frameEditControlEditor.Name = "frameEditControlEditor";
            this.frameEditControlEditor.Size = new System.Drawing.Size(100, 100);
            this.frameEditControlEditor.TabIndex = 0;
            // 
            // toolStripEditor
            // 
            this.toolStripEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEditZoomPlus,
            this.toolStripButtonEditZoomMinus});
            this.toolStripEditor.Location = new System.Drawing.Point(0, 0);
            this.toolStripEditor.Name = "toolStripEditor";
            this.toolStripEditor.Size = new System.Drawing.Size(784, 25);
            this.toolStripEditor.TabIndex = 1;
            this.toolStripEditor.Text = "toolStrip1";
            // 
            // toolStripButtonEditZoomPlus
            // 
            this.toolStripButtonEditZoomPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonEditZoomPlus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditZoomPlus.Image")));
            this.toolStripButtonEditZoomPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditZoomPlus.Name = "toolStripButtonEditZoomPlus";
            this.toolStripButtonEditZoomPlus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEditZoomPlus.Text = "+";
            this.toolStripButtonEditZoomPlus.Click += new System.EventHandler(this.toolStripButtonEditZoomPlus_Click);
            // 
            // toolStripButtonEditZoomMinus
            // 
            this.toolStripButtonEditZoomMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonEditZoomMinus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditZoomMinus.Image")));
            this.toolStripButtonEditZoomMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditZoomMinus.Name = "toolStripButtonEditZoomMinus";
            this.toolStripButtonEditZoomMinus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonEditZoomMinus.Text = "-";
            this.toolStripButtonEditZoomMinus.Click += new System.EventHandler(this.toolStripButtonEditZoomMinus_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新規作成NToolStripMenuItem,
            this.toolStripSeparator1,
            this.開くOToolStripMenuItem,
            this.保存WToolStripMenuItem,
            this.toolStripSeparator2,
            this.出力EToolStripMenuItem,
            this.toolStripSeparator3,
            this.閉じるXToolStripMenuItem});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成NToolStripMenuItem
            // 
            this.新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
            this.新規作成NToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.新規作成NToolStripMenuItem.Text = "新規作成(&N)";
            this.新規作成NToolStripMenuItem.Click += new System.EventHandler(this.新規作成NToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(136, 6);
            // 
            // 開くOToolStripMenuItem
            // 
            this.開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
            this.開くOToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.開くOToolStripMenuItem.Text = "開く(&O)";
            this.開くOToolStripMenuItem.Click += new System.EventHandler(this.開くOToolStripMenuItem_Click);
            // 
            // 保存WToolStripMenuItem
            // 
            this.保存WToolStripMenuItem.Name = "保存WToolStripMenuItem";
            this.保存WToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.保存WToolStripMenuItem.Text = "保存(&S)";
            this.保存WToolStripMenuItem.Click += new System.EventHandler(this.保存WToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // 出力EToolStripMenuItem
            // 
            this.出力EToolStripMenuItem.Name = "出力EToolStripMenuItem";
            this.出力EToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.出力EToolStripMenuItem.Text = "出力(&E)";
            this.出力EToolStripMenuItem.Click += new System.EventHandler(this.出力EToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(136, 6);
            // 
            // 閉じるXToolStripMenuItem
            // 
            this.閉じるXToolStripMenuItem.Name = "閉じるXToolStripMenuItem";
            this.閉じるXToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.閉じるXToolStripMenuItem.Text = "閉じる(&X)";
            this.閉じるXToolStripMenuItem.Click += new System.EventHandler(this.閉じるXToolStripMenuItem_Click);
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.フレーム画像追加ToolStripMenuItem});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // フレーム画像追加ToolStripMenuItem
            // 
            this.フレーム画像追加ToolStripMenuItem.Name = "フレーム画像追加ToolStripMenuItem";
            this.フレーム画像追加ToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.フレーム画像追加ToolStripMenuItem.Text = "フレーム画像追加";
            this.フレーム画像追加ToolStripMenuItem.Click += new System.EventHandler(this.フレーム画像追加ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelMainView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelContoller);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClarityMovement";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panelMainView.ResumeLayout(false);
            this.panelContoller.ResumeLayout(false);
            this.panelContoller.PerformLayout();
            this.panelEditorScroll.ResumeLayout(false);
            this.toolStripEditor.ResumeLayout(false);
            this.toolStripEditor.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Splitter splitter1;
        private Panel panelMainView;
        private Panel panelContoller;
        private Viewer.ClarityEngineViewer clarityDxViewer1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem 新規作成NToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 開くOToolStripMenuItem;
        private ToolStripMenuItem 保存WToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem 出力EToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem 閉じるXToolStripMenuItem;
        private FrameEdit.FrameEditControl frameEditControlEditor;
        private ToolStripMenuItem 編集EToolStripMenuItem;
        private ToolStripMenuItem フレーム画像追加ToolStripMenuItem;
        private ToolStrip toolStripEditor;
        private ToolStripButton toolStripButtonEditZoomPlus;
        private ToolStripButton toolStripButtonEditZoomMinus;
        private Panel panelEditorScroll;
    }
}