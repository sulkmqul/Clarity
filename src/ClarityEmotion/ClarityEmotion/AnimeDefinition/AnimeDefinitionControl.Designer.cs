﻿
namespace ClarityEmotion.AnimeDefinition
{
    partial class AnimeDefinitionControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.listBoxImageFilePath = new System.Windows.Forms.ListBox();
            this.buttonAddPath = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "名前";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(3, 39);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(374, 29);
            this.textBoxName.TabIndex = 1;
            // 
            // listBoxImageFilePath
            // 
            this.listBoxImageFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxImageFilePath.FormattingEnabled = true;
            this.listBoxImageFilePath.ItemHeight = 21;
            this.listBoxImageFilePath.Location = new System.Drawing.Point(3, 130);
            this.listBoxImageFilePath.Name = "listBoxImageFilePath";
            this.listBoxImageFilePath.Size = new System.Drawing.Size(424, 361);
            this.listBoxImageFilePath.TabIndex = 2;
            // 
            // buttonAddPath
            // 
            this.buttonAddPath.Location = new System.Drawing.Point(3, 89);
            this.buttonAddPath.Name = "buttonAddPath";
            this.buttonAddPath.Size = new System.Drawing.Size(80, 35);
            this.buttonAddPath.TabIndex = 3;
            this.buttonAddPath.Text = "追加";
            this.buttonAddPath.UseVisualStyleBackColor = true;
            this.buttonAddPath.Click += new System.EventHandler(this.buttonAddPath_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // AnimeDefinitionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAddPath);
            this.Controls.Add(this.listBoxImageFilePath);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AnimeDefinitionControl";
            this.Size = new System.Drawing.Size(430, 500);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.ListBox listBoxImageFilePath;
        private System.Windows.Forms.Button buttonAddPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
