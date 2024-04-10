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
            panelEditor = new Panel();
            movementEditor1 = new Editor.MovementEditor();
            panelViewer = new Panel();
            movementEditViewerControl1 = new Viwer.MovementEditViewerControl();
            splitter1 = new Splitter();
            menuStrip1 = new MenuStrip();
            fileFToolStripMenuItem = new ToolStripMenuItem();
            新規作成NToolStripMenuItem = new ToolStripMenuItem();
            開くOToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            閉じるXToolStripMenuItem = new ToolStripMenuItem();
            panelEditor.SuspendLayout();
            panelViewer.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panelEditor
            // 
            panelEditor.BackColor = SystemColors.ControlDark;
            panelEditor.Controls.Add(movementEditor1);
            panelEditor.Dock = DockStyle.Bottom;
            panelEditor.Location = new Point(0, 311);
            panelEditor.Name = "panelEditor";
            panelEditor.Size = new Size(1264, 370);
            panelEditor.TabIndex = 0;
            // 
            // movementEditor1
            // 
            movementEditor1.Dock = DockStyle.Fill;
            movementEditor1.Location = new Point(0, 0);
            movementEditor1.Name = "movementEditor1";
            movementEditor1.Size = new Size(1264, 370);
            movementEditor1.TabIndex = 0;
            // 
            // panelViewer
            // 
            panelViewer.Controls.Add(movementEditViewerControl1);
            panelViewer.Dock = DockStyle.Fill;
            panelViewer.Location = new Point(0, 24);
            panelViewer.Name = "panelViewer";
            panelViewer.Size = new Size(1264, 287);
            panelViewer.TabIndex = 1;
            // 
            // movementEditViewerControl1
            // 
            movementEditViewerControl1.Dock = DockStyle.Fill;
            movementEditViewerControl1.Location = new Point(0, 0);
            movementEditViewerControl1.Name = "movementEditViewerControl1";
            movementEditViewerControl1.Size = new Size(1264, 287);
            movementEditViewerControl1.TabIndex = 0;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Bottom;
            splitter1.Location = new Point(0, 308);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(1264, 3);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileFToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1264, 24);
            menuStrip1.TabIndex = 3;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            fileFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新規作成NToolStripMenuItem, 開くOToolStripMenuItem, toolStripSeparator1, 閉じるXToolStripMenuItem });
            fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            fileFToolStripMenuItem.Size = new Size(67, 20);
            fileFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成NToolStripMenuItem
            // 
            新規作成NToolStripMenuItem.Name = "新規作成NToolStripMenuItem";
            新規作成NToolStripMenuItem.Size = new Size(139, 22);
            新規作成NToolStripMenuItem.Text = "新規作成(&N)";
            新規作成NToolStripMenuItem.Click += 新規作成NToolStripMenuItem_Click;
            // 
            // 開くOToolStripMenuItem
            // 
            開くOToolStripMenuItem.Name = "開くOToolStripMenuItem";
            開くOToolStripMenuItem.Size = new Size(139, 22);
            開くOToolStripMenuItem.Text = "開く(&O)";
            開くOToolStripMenuItem.Click += 開くOToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(136, 6);
            // 
            // 閉じるXToolStripMenuItem
            // 
            閉じるXToolStripMenuItem.Name = "閉じるXToolStripMenuItem";
            閉じるXToolStripMenuItem.Size = new Size(139, 22);
            閉じるXToolStripMenuItem.Text = "閉じる(&X)";
            閉じるXToolStripMenuItem.Click += 閉じるXToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1264, 681);
            Controls.Add(splitter1);
            Controls.Add(panelViewer);
            Controls.Add(panelEditor);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "ClarityMovement";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            panelEditor.ResumeLayout(false);
            panelViewer.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelEditor;
        private Panel panelViewer;
        private Splitter splitter1;
        private Viwer.MovementEditViewerControl movementEditViewerControl1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileFToolStripMenuItem;
        private ToolStripMenuItem 新規作成NToolStripMenuItem;
        private ToolStripMenuItem 開くOToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 閉じるXToolStripMenuItem;
        private Editor.MovementEditor movementEditor1;
    }
}