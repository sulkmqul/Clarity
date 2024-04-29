namespace ClarityMovement.Editor
{
    partial class TagEditCollisionControl
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
            panel1 = new Panel();
            radioButtonColRect = new RadioButton();
            radioButtonColCircle = new RadioButton();
            panelCircle = new Panel();
            label1 = new Label();
            numericUpDownRedius = new NumericUpDown();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panelRect = new Panel();
            label3 = new Label();
            label2 = new Label();
            numericUpDownRectH = new NumericUpDown();
            numericUpDownRectW = new NumericUpDown();
            label4 = new Label();
            textBoxColType = new TextBox();
            textBoxTargetColType = new TextBox();
            label5 = new Label();
            panel1.SuspendLayout();
            panelCircle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRedius).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            panelRect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectH).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectW).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(radioButtonColRect);
            panel1.Controls.Add(radioButtonColCircle);
            panel1.Location = new Point(6, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(122, 20);
            panel1.TabIndex = 0;
            // 
            // radioButtonColRect
            // 
            radioButtonColRect.AutoSize = true;
            radioButtonColRect.Location = new Point(63, 0);
            radioButtonColRect.Name = "radioButtonColRect";
            radioButtonColRect.Size = new Size(48, 19);
            radioButtonColRect.TabIndex = 1;
            radioButtonColRect.TabStop = true;
            radioButtonColRect.Text = "Rect";
            radioButtonColRect.UseVisualStyleBackColor = true;
            radioButtonColRect.CheckedChanged += radioButtonColRect_CheckedChanged;
            // 
            // radioButtonColCircle
            // 
            radioButtonColCircle.AutoSize = true;
            radioButtonColCircle.Location = new Point(3, 0);
            radioButtonColCircle.Name = "radioButtonColCircle";
            radioButtonColCircle.Size = new Size(54, 19);
            radioButtonColCircle.TabIndex = 0;
            radioButtonColCircle.TabStop = true;
            radioButtonColCircle.Text = "Circle";
            radioButtonColCircle.UseVisualStyleBackColor = true;
            radioButtonColCircle.CheckedChanged += radioButtonColCircle_CheckedChanged;
            // 
            // panelCircle
            // 
            panelCircle.Controls.Add(label1);
            panelCircle.Controls.Add(numericUpDownRedius);
            panelCircle.Location = new Point(3, 3);
            panelCircle.Name = "panelCircle";
            panelCircle.Size = new Size(173, 33);
            panelCircle.TabIndex = 1;
            panelCircle.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(14, 15);
            label1.TabIndex = 5;
            label1.Text = "R";
            // 
            // numericUpDownRedius
            // 
            numericUpDownRedius.DecimalPlaces = 2;
            numericUpDownRedius.Location = new Point(53, 3);
            numericUpDownRedius.Name = "numericUpDownRedius";
            numericUpDownRedius.Size = new Size(74, 23);
            numericUpDownRedius.TabIndex = 4;
            numericUpDownRedius.TextAlign = HorizontalAlignment.Right;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.Controls.Add(panelCircle);
            flowLayoutPanel1.Controls.Add(panelRect);
            flowLayoutPanel1.Location = new Point(3, 85);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(381, 151);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // panelRect
            // 
            panelRect.Controls.Add(label3);
            panelRect.Controls.Add(label2);
            panelRect.Controls.Add(numericUpDownRectH);
            panelRect.Controls.Add(numericUpDownRectW);
            panelRect.Location = new Point(3, 42);
            panelRect.Name = "panelRect";
            panelRect.Size = new Size(200, 100);
            panelRect.TabIndex = 2;
            panelRect.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(4, 34);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 7;
            label3.Text = "Height";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 5);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 6;
            label2.Text = "Width";
            // 
            // numericUpDownRectH
            // 
            numericUpDownRectH.DecimalPlaces = 2;
            numericUpDownRectH.Location = new Point(53, 32);
            numericUpDownRectH.Name = "numericUpDownRectH";
            numericUpDownRectH.Size = new Size(74, 23);
            numericUpDownRectH.TabIndex = 6;
            numericUpDownRectH.TextAlign = HorizontalAlignment.Right;
            // 
            // numericUpDownRectW
            // 
            numericUpDownRectW.DecimalPlaces = 2;
            numericUpDownRectW.Location = new Point(53, 3);
            numericUpDownRectW.Name = "numericUpDownRectW";
            numericUpDownRectW.Size = new Size(74, 23);
            numericUpDownRectW.TabIndex = 5;
            numericUpDownRectW.TextAlign = HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 36);
            label4.Name = "label4";
            label4.Size = new Size(48, 15);
            label4.TabIndex = 3;
            label4.Text = "ColType";
            // 
            // textBoxColType
            // 
            textBoxColType.Location = new Point(117, 29);
            textBoxColType.Name = "textBoxColType";
            textBoxColType.Size = new Size(100, 23);
            textBoxColType.TabIndex = 4;
            // 
            // textBoxTargetColType
            // 
            textBoxTargetColType.Location = new Point(117, 56);
            textBoxTargetColType.Name = "textBoxTargetColType";
            textBoxTargetColType.Size = new Size(100, 23);
            textBoxTargetColType.TabIndex = 6;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(9, 59);
            label5.Name = "label5";
            label5.Size = new Size(80, 15);
            label5.TabIndex = 5;
            label5.Text = "TargetColType";
            // 
            // TagEditCollisionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBoxTargetColType);
            Controls.Add(label5);
            Controls.Add(textBoxColType);
            Controls.Add(label4);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(panel1);
            Name = "TagEditCollisionControl";
            Size = new Size(387, 253);
            Load += TagEditCollisionControl_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelCircle.ResumeLayout(false);
            panelCircle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRedius).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            panelRect.ResumeLayout(false);
            panelRect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectH).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRectW).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private RadioButton radioButtonColRect;
        private RadioButton radioButtonColCircle;
        private Panel panelCircle;
        private Label label1;
        private NumericUpDown numericUpDownRedius;
        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panelRect;
        private Label label3;
        private Label label2;
        private NumericUpDown numericUpDownRectH;
        private NumericUpDown numericUpDownRectW;
        private Label label4;
        private TextBox textBoxColType;
        private TextBox textBoxTargetColType;
        private Label label5;
    }
}
