namespace ClarityMovement.Editor
{
    partial class MovementEditor
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
            flowLayoutPanelTools = new FlowLayoutPanel();
            buttonAddTag = new Button();
            pictureBoxEditor = new PictureBox();
            hScrollBar1 = new HScrollBar();
            flowLayoutPanelTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEditor).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanelTools
            // 
            flowLayoutPanelTools.AutoSize = true;
            flowLayoutPanelTools.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanelTools.Controls.Add(buttonAddTag);
            flowLayoutPanelTools.Dock = DockStyle.Top;
            flowLayoutPanelTools.Location = new Point(0, 0);
            flowLayoutPanelTools.Name = "flowLayoutPanelTools";
            flowLayoutPanelTools.Size = new Size(804, 29);
            flowLayoutPanelTools.TabIndex = 0;
            // 
            // buttonAddTag
            // 
            buttonAddTag.Location = new Point(3, 3);
            buttonAddTag.Name = "buttonAddTag";
            buttonAddTag.Size = new Size(75, 23);
            buttonAddTag.TabIndex = 0;
            buttonAddTag.Text = "button1";
            buttonAddTag.UseVisualStyleBackColor = true;
            buttonAddTag.Click += buttonAddTag_Click;
            // 
            // pictureBoxEditor
            // 
            pictureBoxEditor.Dock = DockStyle.Fill;
            pictureBoxEditor.Location = new Point(0, 29);
            pictureBoxEditor.Name = "pictureBoxEditor";
            pictureBoxEditor.Size = new Size(804, 207);
            pictureBoxEditor.TabIndex = 1;
            pictureBoxEditor.TabStop = false;
            pictureBoxEditor.Paint += pictureBoxEditor_Paint;
            pictureBoxEditor.MouseDown += pictureBoxEditor_MouseDown;
            pictureBoxEditor.MouseMove += pictureBoxEditor_MouseMove;
            pictureBoxEditor.MouseUp += pictureBoxEditor_MouseUp;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Dock = DockStyle.Bottom;
            hScrollBar1.Location = new Point(0, 219);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(804, 17);
            hScrollBar1.TabIndex = 2;
            hScrollBar1.ValueChanged += hScrollBar1_ValueChanged;
            // 
            // MovementEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(hScrollBar1);
            Controls.Add(pictureBoxEditor);
            Controls.Add(flowLayoutPanelTools);
            Name = "MovementEditor";
            Size = new Size(804, 236);
            Load += MovementEditor_Load;
            Resize += MovementEditor_Resize;
            flowLayoutPanelTools.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxEditor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanelTools;
        private Button buttonAddTag;
        private PictureBox pictureBoxEditor;
        private HScrollBar hScrollBar1;
    }
}
