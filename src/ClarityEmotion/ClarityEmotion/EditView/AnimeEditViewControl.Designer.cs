
namespace ClarityEmotion.EditView
{
    partial class AnimeEditViewControl
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
            this.pictureBoxView = new System.Windows.Forms.PictureBox();
            this.pictureBoxControl = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxControl)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxView
            // 
            this.pictureBoxView.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxView.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxView.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxView.Name = "pictureBoxView";
            this.pictureBoxView.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxView.TabIndex = 0;
            this.pictureBoxView.TabStop = false;
            // 
            // pictureBoxControl
            // 
            this.pictureBoxControl.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxControl.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxControl.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxControl.Name = "pictureBoxControl";
            this.pictureBoxControl.Size = new System.Drawing.Size(300, 300);
            this.pictureBoxControl.TabIndex = 1;
            this.pictureBoxControl.TabStop = false;
            this.pictureBoxControl.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxControl_Paint);
            this.pictureBoxControl.DoubleClick += new System.EventHandler(this.pictureBoxControl_DoubleClick);
            this.pictureBoxControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxControl_MouseDown);
            this.pictureBoxControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxControl_MouseMove);
            this.pictureBoxControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxControl_MouseUp);
            // 
            // AnimeEditViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.pictureBoxView);
            this.Controls.Add(this.pictureBoxControl);
            this.DoubleBuffered = true;
            this.Name = "AnimeEditViewControl";
            this.Size = new System.Drawing.Size(300, 300);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxView;
        private System.Windows.Forms.PictureBox pictureBoxControl;
    }
}
