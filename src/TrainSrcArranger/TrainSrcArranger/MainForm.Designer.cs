namespace TrainSrcArranger
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
            panel1 = new Panel();
            checkBoxSavePromptFlag = new CheckBox();
            label1 = new Label();
            textBoxPrompt = new TextBox();
            panel2 = new Panel();
            buttonNext = new Button();
            labelScaleRate = new Label();
            buttonImageFit = new Button();
            labelProgress = new Label();
            buttonSameSize = new Button();
            menuStrip1 = new MenuStrip();
            ファイルFToolStripMenuItem = new ToolStripMenuItem();
            新規NToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            終了XToolStripMenuItem = new ToolStripMenuItem();
            splitter1 = new Splitter();
            arrangeEditor1 = new ArrangeEditor.ArrangeEditor();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(checkBoxSavePromptFlag);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBoxPrompt);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(649, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(324, 488);
            panel1.TabIndex = 0;
            // 
            // checkBoxSavePromptFlag
            // 
            checkBoxSavePromptFlag.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            checkBoxSavePromptFlag.AutoSize = true;
            checkBoxSavePromptFlag.Location = new Point(6, 371);
            checkBoxSavePromptFlag.Name = "checkBoxSavePromptFlag";
            checkBoxSavePromptFlag.Size = new Size(92, 19);
            checkBoxSavePromptFlag.TabIndex = 2;
            checkBoxSavePromptFlag.Text = "Save Prompt";
            checkBoxSavePromptFlag.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 3);
            label1.Name = "label1";
            label1.Size = new Size(46, 15);
            label1.TabIndex = 1;
            label1.Text = "Prompt";
            // 
            // textBoxPrompt
            // 
            textBoxPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxPrompt.Location = new Point(6, 21);
            textBoxPrompt.Multiline = true;
            textBoxPrompt.Name = "textBoxPrompt";
            textBoxPrompt.Size = new Size(306, 344);
            textBoxPrompt.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonNext);
            panel2.Controls.Add(labelScaleRate);
            panel2.Controls.Add(buttonImageFit);
            panel2.Controls.Add(labelProgress);
            panel2.Controls.Add(buttonSameSize);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 512);
            panel2.Name = "panel2";
            panel2.Size = new Size(973, 35);
            panel2.TabIndex = 1;
            // 
            // buttonNext
            // 
            buttonNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonNext.Location = new Point(574, 6);
            buttonNext.Name = "buttonNext";
            buttonNext.Size = new Size(75, 23);
            buttonNext.TabIndex = 4;
            buttonNext.Text = "次";
            buttonNext.UseVisualStyleBackColor = true;
            buttonNext.Click += buttonNext_Click;
            // 
            // labelScaleRate
            // 
            labelScaleRate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelScaleRate.Location = new Point(723, 10);
            labelScaleRate.Name = "labelScaleRate";
            labelScaleRate.RightToLeft = RightToLeft.Yes;
            labelScaleRate.Size = new Size(76, 15);
            labelScaleRate.TabIndex = 3;
            labelScaleRate.Text = "1.0";
            // 
            // buttonImageFit
            // 
            buttonImageFit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonImageFit.Location = new Point(805, 6);
            buttonImageFit.Name = "buttonImageFit";
            buttonImageFit.Size = new Size(75, 23);
            buttonImageFit.TabIndex = 2;
            buttonImageFit.Text = "Fit";
            buttonImageFit.UseVisualStyleBackColor = true;
            buttonImageFit.Click += buttonImageFit_Click;
            // 
            // labelProgress
            // 
            labelProgress.AutoSize = true;
            labelProgress.Location = new Point(12, 10);
            labelProgress.Name = "labelProgress";
            labelProgress.Size = new Size(22, 15);
            labelProgress.TabIndex = 1;
            labelProgress.Text = "*/*";
            // 
            // buttonSameSize
            // 
            buttonSameSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSameSize.Location = new Point(886, 6);
            buttonSameSize.Name = "buttonSameSize";
            buttonSameSize.Size = new Size(75, 23);
            buttonSameSize.TabIndex = 0;
            buttonSameSize.Text = "1:1";
            buttonSameSize.UseVisualStyleBackColor = true;
            buttonSameSize.Click += buttonSameSize_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { ファイルFToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(973, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            ファイルFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 新規NToolStripMenuItem, toolStripSeparator1, 終了XToolStripMenuItem });
            ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            ファイルFToolStripMenuItem.Size = new Size(67, 20);
            ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 新規NToolStripMenuItem
            // 
            新規NToolStripMenuItem.Name = "新規NToolStripMenuItem";
            新規NToolStripMenuItem.Size = new Size(115, 22);
            新規NToolStripMenuItem.Text = "新規(&N)";
            新規NToolStripMenuItem.Click += 新規NToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(112, 6);
            // 
            // 終了XToolStripMenuItem
            // 
            終了XToolStripMenuItem.Name = "終了XToolStripMenuItem";
            終了XToolStripMenuItem.Size = new Size(115, 22);
            終了XToolStripMenuItem.Text = "終了(&X)";
            終了XToolStripMenuItem.Click += 終了XToolStripMenuItem_Click;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(646, 24);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 488);
            splitter1.TabIndex = 1;
            splitter1.TabStop = false;
            // 
            // arrangeEditor1
            // 
            arrangeEditor1.AllowDrop = true;
            arrangeEditor1.Dock = DockStyle.Fill;
            arrangeEditor1.Location = new Point(0, 24);
            arrangeEditor1.Name = "arrangeEditor1";
            arrangeEditor1.Size = new Size(646, 488);
            arrangeEditor1.TabIndex = 2;
            arrangeEditor1.DragDrop += arrangeEditor1_DragDrop;
            arrangeEditor1.DragEnter += arrangeEditor1_DragEnter;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(973, 547);
            Controls.Add(arrangeEditor1);
            Controls.Add(splitter1);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "LoRA Arranger";
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem ファイルFToolStripMenuItem;
        private ToolStripMenuItem 新規NToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 終了XToolStripMenuItem;
        private Panel panel2;
        private TextBox textBoxPrompt;
        private Splitter splitter1;
        private Button buttonSameSize;
        private CheckBox checkBoxSavePromptFlag;
        private Label label1;
        private Button buttonImageFit;
        private Label labelProgress;
        private Label labelScaleRate;
        private Button buttonNext;
        private ArrangeEditor.ArrangeEditor arrangeEditor1;
    }
}