namespace ClarityOrbit.OrbitView
{
    partial class OrbitEditView
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
            panelDx = new Panel();
            SuspendLayout();
            // 
            // panelDx
            // 
            panelDx.Dock = DockStyle.Fill;
            panelDx.Location = new Point(0, 0);
            panelDx.Name = "panelDx";
            panelDx.Size = new Size(150, 150);
            panelDx.TabIndex = 0;
            panelDx.MouseDown += panelDx_MouseDown;
            panelDx.MouseMove += panelDx_MouseMove;
            panelDx.MouseUp += panelDx_MouseUp;
            // 
            // OrbitEditView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelDx);
            Name = "OrbitEditView";
            Load += OrbitEditView_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelDx;
    }
}
