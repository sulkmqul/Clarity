namespace ClarityOrbit
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
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            新規作成ToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            終了XToolStripMenuItem = new ToolStripMenuItem();
            dockPanelToolWindow = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            splitter1 = new Splitter();
            orbitEditView1 = new OrbitView.OrbitEditView();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルFToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新規作成ToolStripMenuItem, toolStripSeparator1, 終了XToolStripMenuItem });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            ファイルFToolStripMenuItem.Size = new Size(67, 20);
            ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規作成ToolStripMenuItem
            // 
            新規作成ToolStripMenuItem.Name = "新規作成ToolStripMenuItem";
            新規作成ToolStripMenuItem.Size = new Size(122, 22);
            新規作成ToolStripMenuItem.Text = "新規作成";
            新規作成ToolStripMenuItem.Click += 新規作成ToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(119, 6);
            // 
            // 終了XToolStripMenuItem
            // 
            終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            終了XToolStripMenuItem.Size = new Size(122, 22);
            終了XToolStripMenuItem.Text = "終了(&X)";
            終了XToolStripMenuItem.Click += 終了XToolStripMenuItem_Click;
            // 
            // dockPanelToolWindow
            // 
            dockPanelToolWindow.Dock = DockStyle.Right;
            dockPanelToolWindow.Location = new Point(400, 24);
            dockPanelToolWindow.Name = "dockPanelToolWindow";
            dockPanelToolWindow.Size = new Size(400, 426);
            dockPanelToolWindow.TabIndex = 5;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(395, 24);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(5, 426);
            splitter1.TabIndex = 6;
            splitter1.TabStop = false;
            // 
            // orbitEditView1
            // 
            orbitEditView1.Dock = DockStyle.Fill;
            orbitEditView1.Location = new Point(0, 24);
            orbitEditView1.Name = "orbitEditView1";
            orbitEditView1.Size = new Size(395, 426);
            orbitEditView1.TabIndex = 7;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(orbitEditView1);
            Controls.Add(splitter1);
            Controls.Add(dockPanelToolWindow);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "ClarityOrbit";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem 新規作成ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 終了XToolStripMenuItem;
        private Splitter splitter1;
        internal WeifenLuo.WinFormsUI.Docking.DockPanel dockPanelToolWindow;
        internal OrbitView.OrbitEditView orbitEditView1;
    }
}