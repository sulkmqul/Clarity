
namespace Clarity.GUI
{
    partial class ClarityScrollBar
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
            this.pictureBoxScroll = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScroll)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxScroll
            // 
            this.pictureBoxScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScroll.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxScroll.Name = "pictureBoxScroll";
            this.pictureBoxScroll.Size = new System.Drawing.Size(250, 20);
            this.pictureBoxScroll.TabIndex = 0;
            this.pictureBoxScroll.TabStop = false;
            this.pictureBoxScroll.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxScroll_Paint);
            this.pictureBoxScroll.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScroll_MouseDown);
            this.pictureBoxScroll.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScroll_MouseMove);
            this.pictureBoxScroll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxScroll_MouseUp);
            // 
            // ClarityScrollBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxScroll);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ClarityScrollBar";
            this.Size = new System.Drawing.Size(250, 20);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScroll)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxScroll;
    }
}
