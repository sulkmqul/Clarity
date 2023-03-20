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
            splitter1 = new Splitter();
            panelMainView = new Panel();
            clarityDxViewer1 = new Viewer.ClarityEngineViewer();
            panelContoller = new Panel();
            panelEditorScroll = new Panel();
            frameEditControlEditor = new FrameEdit.FrameEditControl();
            toolStripEditor = new ToolStrip();
            toolStripButtonEditZoomPlus = new ToolStripButton();
            toolStripButtonEditZoomMinus = new ToolStripButton();
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            新規作成NToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            開くOToolStripMenuItem = new ToolStripMenuItem();
            保存WToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            出力EToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            閉じるXToolStripMenuItem = new ToolStripMenuItem();
            編集EToolStripMenuItem = new ToolStripMenuItem();
            フレーム画像追加ToolStripMenuItem = new ToolStripMenuItem();
            フレーム画像割り当てToolStripMenuItem = new ToolStripMenuItem();
            panelMainView.SuspendLayout();
            panelContoller.SuspendLayout();
            panelEditorScroll.SuspendLayout();
            toolStripEditor.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Bottom;
            splitter1.Location = new Point(0, 358);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(784, 3);
            splitter1.TabIndex = 0;
            splitter1.TabStop = false;
            // 
            // panelMainView
            // 
            panelMainView.Controls.Add(clarityDxViewer1);
            panelMainView.Dock = DockStyle.Fill;
            panelMainView.Location = new Point(0, 0);
            panelMainView.Name = "panelMainView";
            panelMainView.Size = new Size(784, 358);
            panelMainView.TabIndex = 1;
            // 
            // clarityDxViewer1
            // 
            clarityDxViewer1.Dock = DockStyle.Fill;
            clarityDxViewer1.Location = new Point(0, 0);
            clarityDxViewer1.Name = "clarityDxViewer1";
            clarityDxViewer1.Size = new Size(784, 358);
            clarityDxViewer1.TabIndex = 0;
            // 
            // panelContoller
            // 
            panelContoller.Controls.Add(panelEditorScroll);
            panelContoller.Controls.Add(toolStripEditor);
            panelContoller.Dock = DockStyle.Bottom;
            panelContoller.Location = new Point(0, 361);
            panelContoller.Name = "panelContoller";
            panelContoller.Size = new Size(784, 200);
            panelContoller.TabIndex = 0;
            // 
            // panelEditorScroll
            // 
            panelEditorScroll.AutoScroll = true;
            panelEditorScroll.Controls.Add(frameEditControlEditor);
            panelEditorScroll.Dock = DockStyle.Fill;
            panelEditorScroll.Location = new Point(0, 25);
            panelEditorScroll.Name = "panelEditorScroll";
            panelEditorScroll.Size = new Size(784, 175);
            panelEditorScroll.TabIndex = 2;
            // 
            // frameEditControlEditor
            // 
            frameEditControlEditor.Location = new Point(3, 3);
            frameEditControlEditor.Name = "frameEditControlEditor";
            frameEditControlEditor.Size = new Size(100, 100);
            frameEditControlEditor.TabIndex = 0;
            // 
            // toolStripEditor
            // 
            toolStripEditor.Items.AddRange(new ToolStripItem[] { toolStripButtonEditZoomPlus, toolStripButtonEditZoomMinus });
            toolStripEditor.Location = new Point(0, 0);
            toolStripEditor.Name = "toolStripEditor";
            toolStripEditor.Size = new Size(784, 25);
            toolStripEditor.TabIndex = 1;
            toolStripEditor.Text = "toolStrip1";
            // 
            // toolStripButtonEditZoomPlus
            // 
            toolStripButtonEditZoomPlus.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonEditZoomPlus.Image = (Image)resources.GetObject("toolStripButtonEditZoomPlus.Image");
            toolStripButtonEditZoomPlus.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditZoomPlus.Name = "toolStripButtonEditZoomPlus";
            toolStripButtonEditZoomPlus.Size = new Size(23, 22);
            toolStripButtonEditZoomPlus.Text = "+";
            toolStripButtonEditZoomPlus.Click += toolStripButtonEditZoomPlus_Click;
            // 
            // toolStripButtonEditZoomMinus
            // 
            toolStripButtonEditZoomMinus.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButtonEditZoomMinus.Image = (Image)resources.GetObject("toolStripButtonEditZoomMinus.Image");
            toolStripButtonEditZoomMinus.ImageTransparentColor = Color.Magenta;
            toolStripButtonEditZoomMinus.Name = "toolStripButtonEditZoomMinus";
            toolStripButtonEditZoomMinus.Size = new Size(23, 22);
            toolStripButtonEditZoomMinus.Text = "-";
            toolStripButtonEditZoomMinus.Click += toolStripButtonEditZoomMinus_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルFToolStripMenuItem, 編集EToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(784, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新規作成NToolStripMenuItem, toolStripSeparator1, 開くOToolStripMenuItem, 保存WToolStripMenuItem, toolStripSeparator2, 出力EToolStripMenuItem, toolStripSeparator3, 閉じるXToolStripMenuItem });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            ファイルFToolStripMenuItem.Size = new Size(67, 20);
            ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成NToolStripMenuItem
            // 
            新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
            新規作成NToolStripMenuItem.Size = new Size(139, 22);
            新規作成NToolStripMenuItem.Text = "新規作成(&N)";
            新規作成NToolStripMenuItem.Click += 新規作成NToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(136, 6);
            // 
            // 開くOToolStripMenuItem
            // 
            開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
            開くOToolStripMenuItem.Size = new Size(139, 22);
            開くOToolStripMenuItem.Text = "開く(&O)";
            開くOToolStripMenuItem.Click += 開くOToolStripMenuItem_Click;
            // 
            // 保存WToolStripMenuItem
            // 
            保存WToolStripMenuItem.Name = "保存WToolStripMenuItem";
            保存WToolStripMenuItem.Size = new Size(139, 22);
            保存WToolStripMenuItem.Text = "保存(&S)";
            保存WToolStripMenuItem.Click += 保存WToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(136, 6);
            // 
            // 出力EToolStripMenuItem
            // 
            出力EToolStripMenuItem.Name = "出力EToolStripMenuItem";
            出力EToolStripMenuItem.Size = new Size(139, 22);
            出力EToolStripMenuItem.Text = "出力(&E)";
            出力EToolStripMenuItem.Click += 出力EToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(136, 6);
            // 
            // 閉じるXToolStripMenuItem
            // 
            閉じるXToolStripMenuItem.Name = "閉じるXToolStripMenuItem";
            閉じるXToolStripMenuItem.Size = new Size(139, 22);
            閉じるXToolStripMenuItem.Text = "閉じる(&X)";
            閉じるXToolStripMenuItem.Click += 閉じるXToolStripMenuItem_Click;
            // 
            // 編集EToolStripMenuItem
            // 
            編集EToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { フレーム画像追加ToolStripMenuItem, フレーム画像割り当てToolStripMenuItem });
            編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            編集EToolStripMenuItem.Size = new Size(57, 20);
            編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // フレーム画像追加ToolStripMenuItem
            // 
            フレーム画像追加ToolStripMenuItem.Name = "フレーム画像追加ToolStripMenuItem";
            フレーム画像追加ToolStripMenuItem.Size = new Size(173, 22);
            フレーム画像追加ToolStripMenuItem.Text = "フレーム画像追加";
            フレーム画像追加ToolStripMenuItem.Click += フレーム画像追加ToolStripMenuItem_Click;
            // 
            // フレーム画像割り当てToolStripMenuItem
            // 
            フレーム画像割り当てToolStripMenuItem.Name = "フレーム画像割り当てToolStripMenuItem";
            フレーム画像割り当てToolStripMenuItem.Size = new Size(173, 22);
            フレーム画像割り当てToolStripMenuItem.Text = "フレーム画像割り当て";
            フレーム画像割り当てToolStripMenuItem.Click += フレーム画像割り当てToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 561);
            Controls.Add(menuStrip1);
            Controls.Add(panelMainView);
            Controls.Add(splitter1);
            Controls.Add(panelContoller);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ClarityMovement";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            panelMainView.ResumeLayout(false);
            panelContoller.ResumeLayout(false);
            panelContoller.PerformLayout();
            panelEditorScroll.ResumeLayout(false);
            toolStripEditor.ResumeLayout(false);
            toolStripEditor.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem 編集EToolStripMenuItem;
        private ToolStripMenuItem フレーム画像追加ToolStripMenuItem;
        private ToolStrip toolStripEditor;
        private ToolStripButton toolStripButtonEditZoomPlus;
        private ToolStripButton toolStripButtonEditZoomMinus;
        private Panel panelEditorScroll;
        private ToolStripMenuItem フレーム画像割り当てToolStripMenuItem;
        internal FrameEdit.FrameEditControl frameEditControlEditor;
    }
}