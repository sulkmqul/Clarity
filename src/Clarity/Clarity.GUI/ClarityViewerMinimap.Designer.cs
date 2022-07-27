namespace Clarity.GUI
{
    internal partial class ClarityViewerMinimap
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
            this.pictureBoxMinimap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimap)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMinimap
            // 
            this.pictureBoxMinimap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxMinimap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMinimap.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxMinimap.Name = "pictureBoxMinimap";
            this.pictureBoxMinimap.Size = new System.Drawing.Size(150, 150);
            this.pictureBoxMinimap.TabIndex = 0;
            this.pictureBoxMinimap.TabStop = false;
            this.pictureBoxMinimap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxMinimap_Paint);
            this.pictureBoxMinimap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMinimap_MouseDown);
            this.pictureBoxMinimap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMinimap_MouseMove);
            this.pictureBoxMinimap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMinimap_MouseUp);
            // 
            // ClarityViewerMinimap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxMinimap);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ClarityViewerMinimap";
            this.Load += new System.EventHandler(this.ClarityViewerMinimap_Load);
            this.SizeChanged += new System.EventHandler(this.ClarityViewerMinimap_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinimap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBoxMinimap;
    }
}
