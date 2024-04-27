namespace TrainSrcArranger.ArrangeEditor
{
    partial class ArrangeEditor
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
            clarityImageViewer1 = new Clarity.GUI.ClarityImageViewer();
            SuspendLayout();
            // 
            // clarityImageViewer1
            // 
            clarityImageViewer1.BorderLineColor = Color.Red;
            clarityImageViewer1.BorderLineRendering = false;
            clarityImageViewer1.ClearColor = Color.Black;
            clarityImageViewer1.DisplayAreaLineColor = Color.Red;
            clarityImageViewer1.DisplayAreaLineWidth = 1F;
            clarityImageViewer1.DisplayerRendering = true;
            clarityImageViewer1.Dock = DockStyle.Fill;
            clarityImageViewer1.DoubleClickFitCentering = true;
            clarityImageViewer1.ImageClippingEnabled = false;
            clarityImageViewer1.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            clarityImageViewer1.Location = new Point(0, 0);
            clarityImageViewer1.MinimapBackColor = Color.White;
            clarityImageViewer1.MinimapDisplayMerginRate = 0.05F;
            clarityImageViewer1.MinimapVisible = false;
            clarityImageViewer1.Name = "clarityImageViewer1";
            clarityImageViewer1.Size = new Size(306, 321);
            clarityImageViewer1.SrcBackColor = Color.Red;
            clarityImageViewer1.TabIndex = 0;
            clarityImageViewer1.ClarityViewerZoomChangedEvent += clarityImageViewer1_ClarityViewerZoomChangedEvent;
            clarityImageViewer1.KeyUp += clarityImageViewer1_KeyUp;
            // 
            // ArrangeEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(clarityImageViewer1);
            Name = "ArrangeEditor";
            Size = new Size(306, 321);
            Load += ArrangeEditor_Load;
            ResumeLayout(false);
        }

        #endregion

        private Clarity.GUI.ClarityImageViewer clarityImageViewer1;
    }
}
