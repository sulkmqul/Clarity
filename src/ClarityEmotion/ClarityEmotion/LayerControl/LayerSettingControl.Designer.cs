
namespace ClarityEmotion.LayerControl
{
    partial class LayerSettingControl
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
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.comboBoxAnimeDefinition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.valueScrollControlAlpha = new Clarity.GUI.ValueScrollControl();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxLoopFlag = new System.Windows.Forms.CheckBox();
            this.numericUpDownStartFrame = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownSpan = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.valueScrollControlSpeedRate = new Clarity.GUI.ValueScrollControl();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownPosY = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownPosX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.labelAnimeDefinitionFrame = new System.Windows.Forms.Label();
            this.numericUpDownFrameOffset = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxFlipState = new System.Windows.Forms.ComboBox();
            this.numericUpDownDispSizeY = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownDispSizeX = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxLayerName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDispSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDispSizeX)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(13, 34);
            this.checkBoxEnabled.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(74, 19);
            this.checkBoxEnabled.TabIndex = 1;
            this.checkBoxEnabled.Text = "有効可否";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabled.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // comboBoxAnimeDefinition
            // 
            this.comboBoxAnimeDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnimeDefinition.FormattingEnabled = true;
            this.comboBoxAnimeDefinition.Location = new System.Drawing.Point(13, 71);
            this.comboBoxAnimeDefinition.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxAnimeDefinition.Name = "comboBoxAnimeDefinition";
            this.comboBoxAnimeDefinition.Size = new System.Drawing.Size(174, 23);
            this.comboBoxAnimeDefinition.TabIndex = 2;
            this.comboBoxAnimeDefinition.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnimeDefinition_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 54);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "アニメ";
            // 
            // valueScrollControlAlpha
            // 
            this.valueScrollControlAlpha.FixedPoint = 100;
            this.valueScrollControlAlpha.Location = new System.Drawing.Point(13, 119);
            this.valueScrollControlAlpha.MaxValue = 100;
            this.valueScrollControlAlpha.MinValue = 0;
            this.valueScrollControlAlpha.Name = "valueScrollControlAlpha";
            this.valueScrollControlAlpha.Size = new System.Drawing.Size(217, 25);
            this.valueScrollControlAlpha.TabIndex = 4;
            this.valueScrollControlAlpha.Value = 100;
            this.valueScrollControlAlpha.ValueFixedPoint = 1D;
            this.valueScrollControlAlpha.ValueFormat = "{0:F2}";
            this.valueScrollControlAlpha.ValueChanged += new System.EventHandler(this.valueScrollControl_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "透明度";
            // 
            // checkBoxLoopFlag
            // 
            this.checkBoxLoopFlag.AutoSize = true;
            this.checkBoxLoopFlag.Location = new System.Drawing.Point(13, 248);
            this.checkBoxLoopFlag.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxLoopFlag.Name = "checkBoxLoopFlag";
            this.checkBoxLoopFlag.Size = new System.Drawing.Size(77, 19);
            this.checkBoxLoopFlag.TabIndex = 6;
            this.checkBoxLoopFlag.Text = "ループ再生";
            this.checkBoxLoopFlag.UseVisualStyleBackColor = true;
            this.checkBoxLoopFlag.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // numericUpDownStartFrame
            // 
            this.numericUpDownStartFrame.Location = new System.Drawing.Point(13, 165);
            this.numericUpDownStartFrame.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownStartFrame.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStartFrame.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownStartFrame.Name = "numericUpDownStartFrame";
            this.numericUpDownStartFrame.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownStartFrame.TabIndex = 7;
            this.numericUpDownStartFrame.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 148);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Span";
            // 
            // numericUpDownSpan
            // 
            this.numericUpDownSpan.Location = new System.Drawing.Point(111, 165);
            this.numericUpDownSpan.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownSpan.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownSpan.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownSpan.Name = "numericUpDownSpan";
            this.numericUpDownSpan.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownSpan.TabIndex = 9;
            this.numericUpDownSpan.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 195);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "再生速度";
            // 
            // valueScrollControlSpeedRate
            // 
            this.valueScrollControlSpeedRate.FixedPoint = 10;
            this.valueScrollControlSpeedRate.Location = new System.Drawing.Point(13, 214);
            this.valueScrollControlSpeedRate.Margin = new System.Windows.Forms.Padding(4);
            this.valueScrollControlSpeedRate.MaxValue = 200;
            this.valueScrollControlSpeedRate.MinValue = 1;
            this.valueScrollControlSpeedRate.Name = "valueScrollControlSpeedRate";
            this.valueScrollControlSpeedRate.Size = new System.Drawing.Size(216, 25);
            this.valueScrollControlSpeedRate.TabIndex = 13;
            this.valueScrollControlSpeedRate.Value = 10;
            this.valueScrollControlSpeedRate.ValueFixedPoint = 1D;
            this.valueScrollControlSpeedRate.ValueFormat = "{0:F1}";
            this.valueScrollControlSpeedRate.ValueChanged += new System.EventHandler(this.valueScrollControlSpeedRate_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 327);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "PosY";
            // 
            // numericUpDownPosY
            // 
            this.numericUpDownPosY.Location = new System.Drawing.Point(111, 344);
            this.numericUpDownPosY.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownPosY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPosY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownPosY.Name = "numericUpDownPosY";
            this.numericUpDownPosY.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownPosY.TabIndex = 16;
            this.numericUpDownPosY.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 327);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "PosX";
            // 
            // numericUpDownPosX
            // 
            this.numericUpDownPosX.Location = new System.Drawing.Point(13, 344);
            this.numericUpDownPosX.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownPosX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownPosX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownPosX.Name = "numericUpDownPosX";
            this.numericUpDownPosX.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownPosX.TabIndex = 14;
            this.numericUpDownPosX.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 376);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "SizeX";
            // 
            // labelAnimeDefinitionFrame
            // 
            this.labelAnimeDefinitionFrame.AutoSize = true;
            this.labelAnimeDefinitionFrame.Location = new System.Drawing.Point(193, 73);
            this.labelAnimeDefinitionFrame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAnimeDefinitionFrame.Name = "labelAnimeDefinitionFrame";
            this.labelAnimeDefinitionFrame.Size = new System.Drawing.Size(17, 15);
            this.labelAnimeDefinitionFrame.TabIndex = 20;
            this.labelAnimeDefinitionFrame.Text = "--";
            // 
            // numericUpDownFrameOffset
            // 
            this.numericUpDownFrameOffset.Location = new System.Drawing.Point(13, 285);
            this.numericUpDownFrameOffset.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownFrameOffset.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownFrameOffset.Name = "numericUpDownFrameOffset";
            this.numericUpDownFrameOffset.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownFrameOffset.TabIndex = 22;
            this.numericUpDownFrameOffset.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 268);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 15);
            this.label9.TabIndex = 23;
            this.label9.Text = "フレームOffset";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 427);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(26, 15);
            this.label10.TabIndex = 24;
            this.label10.Text = "Flip";
            // 
            // comboBoxFlipState
            // 
            this.comboBoxFlipState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFlipState.FormattingEnabled = true;
            this.comboBoxFlipState.Location = new System.Drawing.Point(13, 444);
            this.comboBoxFlipState.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxFlipState.Name = "comboBoxFlipState";
            this.comboBoxFlipState.Size = new System.Drawing.Size(174, 23);
            this.comboBoxFlipState.TabIndex = 25;
            this.comboBoxFlipState.SelectedIndexChanged += new System.EventHandler(this.comboBoxFlipState_SelectedIndexChanged);
            // 
            // numericUpDownDispSizeY
            // 
            this.numericUpDownDispSizeY.Location = new System.Drawing.Point(111, 393);
            this.numericUpDownDispSizeY.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownDispSizeY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDispSizeY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownDispSizeY.Name = "numericUpDownDispSizeY";
            this.numericUpDownDispSizeY.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownDispSizeY.TabIndex = 27;
            this.numericUpDownDispSizeY.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // numericUpDownDispSizeX
            // 
            this.numericUpDownDispSizeX.Location = new System.Drawing.Point(13, 393);
            this.numericUpDownDispSizeX.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDownDispSizeX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDispSizeX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownDispSizeX.Name = "numericUpDownDispSizeX";
            this.numericUpDownDispSizeX.Size = new System.Drawing.Size(93, 23);
            this.numericUpDownDispSizeX.TabIndex = 26;
            this.numericUpDownDispSizeX.ValueChanged += new System.EventHandler(this.numericUpDownChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(111, 376);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 15);
            this.label11.TabIndex = 18;
            this.label11.Text = "SizeY";
            // 
            // textBoxLayerName
            // 
            this.textBoxLayerName.Location = new System.Drawing.Point(13, 3);
            this.textBoxLayerName.Name = "textBoxLayerName";
            this.textBoxLayerName.Size = new System.Drawing.Size(197, 23);
            this.textBoxLayerName.TabIndex = 28;
            this.textBoxLayerName.TextChanged += new System.EventHandler(this.textBoxLayerName_TextChanged);
            // 
            // LayerSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxLayerName);
            this.Controls.Add(this.numericUpDownDispSizeY);
            this.Controls.Add(this.numericUpDownDispSizeX);
            this.Controls.Add(this.comboBoxFlipState);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.numericUpDownFrameOffset);
            this.Controls.Add(this.labelAnimeDefinitionFrame);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownPosY);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownPosX);
            this.Controls.Add(this.valueScrollControlSpeedRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownSpan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownStartFrame);
            this.Controls.Add(this.checkBoxLoopFlag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.valueScrollControlAlpha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxEnabled);
            this.Controls.Add(this.comboBoxAnimeDefinition);
            this.Name = "LayerSettingControl";
            this.Size = new System.Drawing.Size(300, 600);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDispSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDispSizeX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.ComboBox comboBoxAnimeDefinition;
        private System.Windows.Forms.Label label1;
        private Clarity.GUI.ValueScrollControl valueScrollControlAlpha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxLoopFlag;
        private System.Windows.Forms.NumericUpDown numericUpDownStartFrame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownSpan;
        private System.Windows.Forms.Label label5;
        private Clarity.GUI.ValueScrollControl valueScrollControlSpeedRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownPosY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownPosX;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelAnimeDefinitionFrame;
        private System.Windows.Forms.NumericUpDown numericUpDownFrameOffset;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxFlipState;
        private NumericUpDown numericUpDownDispSizeY;
        private NumericUpDown numericUpDownDispSizeX;
        private Label label11;
        private TextBox textBoxLayerName;
    }
}
