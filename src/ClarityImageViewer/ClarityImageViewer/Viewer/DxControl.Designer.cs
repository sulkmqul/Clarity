namespace ClarityImageViewer.Viewer
{
    partial class DxControl
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
            SuspendLayout();
            // 
            // DxControl
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "DxControl";
            Load += DxControl_Load;
            SizeChanged += DxControl_SizeChanged;
            DoubleClick += DxControl_DoubleClick;
            MouseDown += DxControl_MouseDown;
            MouseMove += DxControl_MouseMove;
            MouseUp += DxControl_MouseUp;
            ResumeLayout(false);
        }

        #endregion
    }
}
