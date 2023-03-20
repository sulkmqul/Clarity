namespace Clarity.GUI
{
    partial class ClarityDataInputControl
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
            label1 = new Label();
            textBoxTagName = new TextBox();
            buttonRemove = new Button();
            buttonAdd = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 8);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 13;
            label1.Text = "code";
            // 
            // textBoxTagName
            // 
            textBoxTagName.Location = new Point(63, 5);
            textBoxTagName.Name = "textBoxTagName";
            textBoxTagName.Size = new Size(114, 23);
            textBoxTagName.TabIndex = 11;
            textBoxTagName.Text = "Tag";
            // 
            // buttonRemove
            // 
            buttonRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRemove.Location = new Point(508, 3);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new Size(40, 23);
            buttonRemove.TabIndex = 15;
            buttonRemove.Text = "-";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAdd.Location = new Point(462, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(40, 23);
            buttonAdd.TabIndex = 15;
            buttonAdd.Text = "+";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // ClarityDataInputControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonAdd);
            Controls.Add(buttonRemove);
            Controls.Add(label1);
            Controls.Add(textBoxTagName);
            Name = "ClarityDataInputControl";
            Size = new Size(583, 150);
            Load += ClarityDataInputControl_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox textBoxTagName;
        private Button buttonRemove;
        private Button buttonAdd;
    }
}
