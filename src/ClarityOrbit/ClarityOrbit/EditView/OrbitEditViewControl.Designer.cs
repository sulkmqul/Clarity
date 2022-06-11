namespace ClarityOrbit.EditView
{
    partial class OrbitEditViewControl
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
            this.panelDx = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelDx
            // 
            this.panelDx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDx.Location = new System.Drawing.Point(0, 0);
            this.panelDx.Margin = new System.Windows.Forms.Padding(0);
            this.panelDx.Name = "panelDx";
            this.panelDx.Size = new System.Drawing.Size(150, 150);
            this.panelDx.TabIndex = 0;
            // 
            // OrbitEditViewControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelDx);
            this.Name = "OrbitEditViewControl";
            this.Load += new System.EventHandler(this.OrbitEditViewControl_Load);
            this.Resize += new System.EventHandler(this.OrbitEditViewControl_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel panelDx;
    }
}
