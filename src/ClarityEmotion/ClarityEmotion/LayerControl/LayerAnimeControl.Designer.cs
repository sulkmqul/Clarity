
namespace ClarityEmotion.LayerControl
{
    partial class LayerAnimeControl
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerAnimeControl));
            this.panelControl = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelDispFrame = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonScalePlus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonScaleMinus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlayStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlayStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResetFrame = new System.Windows.Forms.ToolStripButton();
            this.panelLayerTimeControl = new System.Windows.Forms.Panel();
            this.panelControl.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.toolStrip1);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(791, 40);
            this.panelControl.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelDispFrame,
            this.toolStripSeparator2,
            this.toolStripButtonScalePlus,
            this.toolStripButtonScaleMinus,
            this.toolStripSeparator1,
            this.toolStripButtonPlayStart,
            this.toolStripButtonPlayStop,
            this.toolStripButtonResetFrame});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(791, 40);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelDispFrame
            // 
            this.toolStripLabelDispFrame.AutoSize = false;
            this.toolStripLabelDispFrame.Name = "toolStripLabelDispFrame";
            this.toolStripLabelDispFrame.Size = new System.Drawing.Size(60, 37);
            this.toolStripLabelDispFrame.Text = "0";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonScalePlus
            // 
            this.toolStripButtonScalePlus.AutoSize = false;
            this.toolStripButtonScalePlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonScalePlus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonScalePlus.Image")));
            this.toolStripButtonScalePlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonScalePlus.Name = "toolStripButtonScalePlus";
            this.toolStripButtonScalePlus.Size = new System.Drawing.Size(37, 37);
            this.toolStripButtonScalePlus.Text = "+";
            this.toolStripButtonScalePlus.Click += new System.EventHandler(this.toolStripButtonScalePlus_Click);
            // 
            // toolStripButtonScaleMinus
            // 
            this.toolStripButtonScaleMinus.AutoSize = false;
            this.toolStripButtonScaleMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonScaleMinus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonScaleMinus.Image")));
            this.toolStripButtonScaleMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonScaleMinus.Name = "toolStripButtonScaleMinus";
            this.toolStripButtonScaleMinus.Size = new System.Drawing.Size(37, 37);
            this.toolStripButtonScaleMinus.Text = "-";
            this.toolStripButtonScaleMinus.Click += new System.EventHandler(this.toolStripButtonScaleMinus_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripButtonPlayStart
            // 
            this.toolStripButtonPlayStart.AutoSize = false;
            this.toolStripButtonPlayStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPlayStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlayStart.Image")));
            this.toolStripButtonPlayStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlayStart.Name = "toolStripButtonPlayStart";
            this.toolStripButtonPlayStart.Size = new System.Drawing.Size(37, 37);
            this.toolStripButtonPlayStart.Text = "再生";
            this.toolStripButtonPlayStart.Click += new System.EventHandler(this.toolStripButtonPlayStart_Click);
            // 
            // toolStripButtonPlayStop
            // 
            this.toolStripButtonPlayStop.AutoSize = false;
            this.toolStripButtonPlayStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPlayStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlayStop.Image")));
            this.toolStripButtonPlayStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlayStop.Name = "toolStripButtonPlayStop";
            this.toolStripButtonPlayStop.Size = new System.Drawing.Size(37, 37);
            this.toolStripButtonPlayStop.Text = "停止";
            this.toolStripButtonPlayStop.Click += new System.EventHandler(this.toolStripButtonPlayStop_Click);
            // 
            // toolStripButtonResetFrame
            // 
            this.toolStripButtonResetFrame.AutoSize = false;
            this.toolStripButtonResetFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonResetFrame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResetFrame.Image")));
            this.toolStripButtonResetFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResetFrame.Name = "toolStripButtonResetFrame";
            this.toolStripButtonResetFrame.Size = new System.Drawing.Size(37, 37);
            this.toolStripButtonResetFrame.Text = "f0";
            this.toolStripButtonResetFrame.Click += new System.EventHandler(this.toolStripButtonResetFrame_Click);
            // 
            // panelLayerTimeControl
            // 
            this.panelLayerTimeControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLayerTimeControl.AutoScroll = true;
            this.panelLayerTimeControl.Location = new System.Drawing.Point(0, 40);
            this.panelLayerTimeControl.Margin = new System.Windows.Forms.Padding(0);
            this.panelLayerTimeControl.Name = "panelLayerTimeControl";
            this.panelLayerTimeControl.Size = new System.Drawing.Size(791, 282);
            this.panelLayerTimeControl.TabIndex = 0;
            // 
            // LayerAnimeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLayerTimeControl);
            this.Controls.Add(this.panelControl);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LayerAnimeControl";
            this.Size = new System.Drawing.Size(791, 322);
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.Panel panelLayerTimeControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonScalePlus;
        private System.Windows.Forms.ToolStripButton toolStripButtonScaleMinus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlayStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlayStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonResetFrame;
        internal System.Windows.Forms.ToolStripLabel toolStripLabelDispFrame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
