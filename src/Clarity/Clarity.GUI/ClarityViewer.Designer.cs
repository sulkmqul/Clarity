namespace Clarity.GUI
{
    partial class ClarityViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClarityViewer));
            this.clarityViewerMinimapView = new Clarity.GUI.ClarityViewerMinimap();
            this.SuspendLayout();
            // 
            // clarityViewerMinimapView
            // 
            this.clarityViewerMinimapView.DisplayAreaLineColor = System.Drawing.Color.Red;
            this.clarityViewerMinimapView.DisplayAreaLineWidth = 1F;
            this.clarityViewerMinimapView.DisplayerRendering = true;
            this.clarityViewerMinimapView.DispRect = ((System.Drawing.RectangleF)(resources.GetObject("clarityViewerMinimapView.DispRect")));
            this.clarityViewerMinimapView.Location = new System.Drawing.Point(0, 0);
            this.clarityViewerMinimapView.Margin = new System.Windows.Forms.Padding(0);
            this.clarityViewerMinimapView.MinimapDisplayMerginRate = 0.05F;
            this.clarityViewerMinimapView.MinimapSizeRate = 0.3F;
            this.clarityViewerMinimapView.Name = "clarityViewerMinimapView";
            this.clarityViewerMinimapView.Size = new System.Drawing.Size(0, 0);
            this.clarityViewerMinimapView.SrcBackColor = System.Drawing.Color.White;
            this.clarityViewerMinimapView.SrcRect = ((System.Drawing.RectangleF)(resources.GetObject("clarityViewerMinimapView.SrcRect")));
            this.clarityViewerMinimapView.TabIndex = 0;
            this.clarityViewerMinimapView.Visible = false;
            this.clarityViewerMinimapView.PositonSelectEvent += new Clarity.GUI.ClarityViewerMinimap.PositonSelectDelegate(this.clarityViewerMinimapView_PositonSelectEvent);
            // 
            // ClarityViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clarityViewerMinimapView);
            this.Name = "ClarityViewer";
            this.Size = new System.Drawing.Size(304, 255);
            this.Load += new System.EventHandler(this.ClarityViewer_Load);
            this.SizeChanged += new System.EventHandler(this.ClarityViewer_SizeChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ClarityViewer_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ClarityViewer_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ClarityViewer_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ClarityViewer_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ClarityViewer_MouseUp);
            this.Resize += new System.EventHandler(this.ClarityViewer_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ClarityViewerMinimap clarityViewerMinimapView;
    }
}
