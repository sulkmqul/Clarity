namespace ClarityOrbit.TipSelectView
{
    partial class TipSelectViewDockingContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipSelectViewDockingContent));
            this.tabControlTips = new System.Windows.Forms.TabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonTipAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonTipRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlTips
            // 
            this.tabControlTips.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlTips.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlTips.Location = new System.Drawing.Point(0, 28);
            this.tabControlTips.Name = "tabControlTips";
            this.tabControlTips.SelectedIndex = 0;
            this.tabControlTips.Size = new System.Drawing.Size(800, 422);
            this.tabControlTips.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonTipAdd,
            this.toolStripButtonTipRemove});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonTipAdd
            // 
            this.toolStripButtonTipAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTipAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTipAdd.Image")));
            this.toolStripButtonTipAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTipAdd.Name = "toolStripButtonTipAdd";
            this.toolStripButtonTipAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTipAdd.Text = "toolStripButton1";
            this.toolStripButtonTipAdd.ToolTipText = "チップ追加";
            // 
            // toolStripButtonTipRemove
            // 
            this.toolStripButtonTipRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonTipRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonTipRemove.Image")));
            this.toolStripButtonTipRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonTipRemove.Name = "toolStripButtonTipRemove";
            this.toolStripButtonTipRemove.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonTipRemove.Text = "toolStripButton2";
            this.toolStripButtonTipRemove.ToolTipText = "チップ削除";
            // 
            // TipSelectViewDockingContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControlTips);
            this.Name = "TipSelectViewDockingContent";
            this.Text = "チップ";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlTips;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonTipAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonTipRemove;
    }
}