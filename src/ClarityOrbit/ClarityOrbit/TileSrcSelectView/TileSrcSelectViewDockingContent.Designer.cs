namespace ClarityOrbit.TileSrcSelectView
{
    partial class TileSrcSelectViewDockingContent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TileSrcSelectViewDockingContent));
            tabControlTileSrc = new TabControl();
            toolStrip1 = new ToolStrip();
            toolStripButtonTipAdd = new ToolStripButton();
            toolStripButtonTipRemove = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlTileSrc
            // 
            tabControlTileSrc.Alignment = TabAlignment.Bottom;
            tabControlTileSrc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlTileSrc.Location = new Point(0, 28);
            tabControlTileSrc.Name = "tabControlTileSrc";
            tabControlTileSrc.SelectedIndex = 0;
            tabControlTileSrc.Size = new Size(800, 422);
            tabControlTileSrc.TabIndex = 0;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButtonTipAdd, toolStripButtonTipRemove });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 1;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonTipAdd
            // 
            toolStripButtonTipAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonTipAdd.Image = (Image)resources.GetObject("toolStripButtonTipAdd.Image");
            toolStripButtonTipAdd.ImageTransparentColor = Color.Magenta;
            toolStripButtonTipAdd.Name = "toolStripButtonTipAdd";
            toolStripButtonTipAdd.Size = new Size(23, 22);
            toolStripButtonTipAdd.Text = "toolStripButton1";
            toolStripButtonTipAdd.ToolTipText = "チップ追加";
            toolStripButtonTipAdd.Click += toolStripButtonTipAdd_Click;
            // 
            // toolStripButtonTipRemove
            // 
            toolStripButtonTipRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonTipRemove.Image = (Image)resources.GetObject("toolStripButtonTipRemove.Image");
            toolStripButtonTipRemove.ImageTransparentColor = Color.Magenta;
            toolStripButtonTipRemove.Name = "toolStripButtonTipRemove";
            toolStripButtonTipRemove.Size = new Size(23, 22);
            toolStripButtonTipRemove.Text = "toolStripButton2";
            toolStripButtonTipRemove.ToolTipText = "チップ削除";
            toolStripButtonTipRemove.Click += toolStripButtonTipRemove_Click;
            // 
            // TileSrcSelectViewDockingContent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(toolStrip1);
            Controls.Add(tabControlTileSrc);
            Name = "TileSrcSelectViewDockingContent";
            Text = "チップ";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControlTileSrc;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTipAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonTipRemove;
    }
}