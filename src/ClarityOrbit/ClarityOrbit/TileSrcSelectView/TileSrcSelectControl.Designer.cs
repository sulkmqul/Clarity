namespace ClarityOrbit.TileSrcSelectView
{
    partial class TileSrcSelectControl
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
            textBoxImagePath = new TextBox();
            clarityImageViewerTileSrc = new Clarity.GUI.ClarityImageViewer();
            SuspendLayout();
            // 
            // textBoxImagePath
            // 
            textBoxImagePath.Dock = DockStyle.Top;
            textBoxImagePath.Location = new Point(0, 0);
            textBoxImagePath.Name = "textBoxImagePath";
            textBoxImagePath.ReadOnly = true;
            textBoxImagePath.Size = new Size(310, 23);
            textBoxImagePath.TabIndex = 0;
            // 
            // clarityImageViewerTileSrc
            // 
            clarityImageViewerTileSrc.BorderLineColor = Color.Red;
            clarityImageViewerTileSrc.BorderLineRendering = false;
            clarityImageViewerTileSrc.ClearColor = Color.Black;
            clarityImageViewerTileSrc.DisplayAreaLineColor = Color.Red;
            clarityImageViewerTileSrc.DisplayAreaLineWidth = 1F;
            clarityImageViewerTileSrc.DisplayerRendering = true;
            clarityImageViewerTileSrc.Dock = DockStyle.Fill;
            clarityImageViewerTileSrc.DoubleClickFitCentering = true;
            clarityImageViewerTileSrc.ImageClippingEnabled = true;
            clarityImageViewerTileSrc.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            clarityImageViewerTileSrc.Location = new Point(0, 23);
            clarityImageViewerTileSrc.MinimapBackColor = Color.White;
            clarityImageViewerTileSrc.MinimapDisplayMerginRate = 0.05F;
            clarityImageViewerTileSrc.MinimapVisible = false;
            clarityImageViewerTileSrc.Name = "clarityImageViewerTileSrc";
            clarityImageViewerTileSrc.PosMode = Clarity.GUI.EClarityImageViewerPositionMode.LeftTop;
            clarityImageViewerTileSrc.Size = new Size(310, 225);
            clarityImageViewerTileSrc.SrcBackColor = Color.Red;
            clarityImageViewerTileSrc.TabIndex = 1;
            // 
            // TileSrcSelectControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(clarityImageViewerTileSrc);
            Controls.Add(textBoxImagePath);
            Name = "TileSrcSelectControl";
            Size = new Size(310, 248);
            Load += TileSrcSelectControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxImagePath;
        private Clarity.GUI.ClarityImageViewer clarityImageViewerTileSrc;
    }
}
