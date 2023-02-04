namespace ClarityEmotion.LayerControl
{
    partial class LayerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabelFramePos = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLayerAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLayerRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomPlus = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomMinus = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonPlayStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPlayStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResetFrame = new System.Windows.Forms.ToolStripButton();
            this.panelLayer = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabelFramePos,
            this.toolStripSeparator3,
            this.toolStripButtonLayerAdd,
            this.toolStripButtonLayerRemove,
            this.toolStripSeparator1,
            this.toolStripButtonZoomPlus,
            this.toolStripButtonZoomMinus,
            this.toolStripSeparator2,
            this.toolStripButtonPlayStart,
            this.toolStripButtonPlayStop,
            this.toolStripButtonResetFrame});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(605, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabelFramePos
            // 
            this.toolStripLabelFramePos.AutoSize = false;
            this.toolStripLabelFramePos.Name = "toolStripLabelFramePos";
            this.toolStripLabelFramePos.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabelFramePos.Text = "0";
            this.toolStripLabelFramePos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripLabelFramePos.Click += new System.EventHandler(this.toolStripLabelFramePos_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLayerAdd
            // 
            this.toolStripButtonLayerAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLayerAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLayerAdd.Image")));
            this.toolStripButtonLayerAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLayerAdd.Name = "toolStripButtonLayerAdd";
            this.toolStripButtonLayerAdd.Size = new System.Drawing.Size(61, 22);
            this.toolStripButtonLayerAdd.Text = "LayerAdd";
            this.toolStripButtonLayerAdd.ToolTipText = "LayerAdd";
            this.toolStripButtonLayerAdd.Click += new System.EventHandler(this.toolStripButtonLayerAdd_Click);
            // 
            // toolStripButtonLayerRemove
            // 
            this.toolStripButtonLayerRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonLayerRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLayerRemove.Image")));
            this.toolStripButtonLayerRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLayerRemove.Name = "toolStripButtonLayerRemove";
            this.toolStripButtonLayerRemove.Size = new System.Drawing.Size(81, 22);
            this.toolStripButtonLayerRemove.Text = "LayerRemove";
            this.toolStripButtonLayerRemove.Click += new System.EventHandler(this.toolStripButtonLayerRemove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonZoomPlus
            // 
            this.toolStripButtonZoomPlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomPlus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomPlus.Image")));
            this.toolStripButtonZoomPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomPlus.Name = "toolStripButtonZoomPlus";
            this.toolStripButtonZoomPlus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomPlus.Text = "+";
            this.toolStripButtonZoomPlus.Click += new System.EventHandler(this.toolStripButtonZoomPlus_Click);
            // 
            // toolStripButtonZoomMinus
            // 
            this.toolStripButtonZoomMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonZoomMinus.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomMinus.Image")));
            this.toolStripButtonZoomMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomMinus.Name = "toolStripButtonZoomMinus";
            this.toolStripButtonZoomMinus.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomMinus.Text = "-";
            this.toolStripButtonZoomMinus.Click += new System.EventHandler(this.toolStripButtonZoomMinus_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonPlayStart
            // 
            this.toolStripButtonPlayStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPlayStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlayStart.Image")));
            this.toolStripButtonPlayStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlayStart.Name = "toolStripButtonPlayStart";
            this.toolStripButtonPlayStart.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonPlayStart.Text = "再生";
            this.toolStripButtonPlayStart.Click += new System.EventHandler(this.toolStripButtonPlayStart_Click);
            // 
            // toolStripButtonPlayStop
            // 
            this.toolStripButtonPlayStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPlayStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPlayStop.Image")));
            this.toolStripButtonPlayStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPlayStop.Name = "toolStripButtonPlayStop";
            this.toolStripButtonPlayStop.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonPlayStop.Text = "停止";
            this.toolStripButtonPlayStop.Click += new System.EventHandler(this.toolStripButtonPlayStop_Click);
            // 
            // toolStripButtonResetFrame
            // 
            this.toolStripButtonResetFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonResetFrame.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResetFrame.Image")));
            this.toolStripButtonResetFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResetFrame.Name = "toolStripButtonResetFrame";
            this.toolStripButtonResetFrame.Size = new System.Drawing.Size(36, 22);
            this.toolStripButtonResetFrame.Text = "reset";
            this.toolStripButtonResetFrame.Click += new System.EventHandler(this.toolStripButtonResetFrame_Click);
            // 
            // panelLayer
            // 
            this.panelLayer.AutoScroll = true;
            this.panelLayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLayer.Location = new System.Drawing.Point(0, 25);
            this.panelLayer.Margin = new System.Windows.Forms.Padding(0);
            this.panelLayer.Name = "panelLayer";
            this.panelLayer.Size = new System.Drawing.Size(605, 177);
            this.panelLayer.TabIndex = 1;
            // 
            // LayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLayer);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LayerControl";
            this.Size = new System.Drawing.Size(605, 202);
            this.Load += new System.EventHandler(this.LayerControl_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LayerControl_KeyUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private Panel panelLayer;
        private ToolStripLabel toolStripLabelFramePos;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton toolStripButtonLayerAdd;
        private ToolStripButton toolStripButtonLayerRemove;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButtonZoomPlus;
        private ToolStripButton toolStripButtonZoomMinus;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButtonPlayStart;
        private ToolStripButton toolStripButtonPlayStop;
        private ToolStripButton toolStripButtonResetFrame;
    }
}
