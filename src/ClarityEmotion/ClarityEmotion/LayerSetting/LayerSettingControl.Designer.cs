
namespace ClarityEmotion.LayerSetting
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
            this.labelLayerName = new System.Windows.Forms.Label();
            this.checkBoxEnabled = new System.Windows.Forms.CheckBox();
            this.comboBoxAnimeDefinition = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.valueScrollControlAlpha = new Clarity.GUI.ValueScrollControl();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxLoopFlag = new System.Windows.Forms.CheckBox();
            this.numericUpDownStartFrame = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownEndFrame = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.valueScrollControlSpeedRate = new Clarity.GUI.ValueScrollControl();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownPosY = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownPosX = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.valueScrollControlScaleRate = new Clarity.GUI.ValueScrollControl();
            this.labelAnimeDefinitionFrame = new System.Windows.Forms.Label();
            this.labelFrameSpan = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).BeginInit();
            this.SuspendLayout();
            // 
            // labelLayerName
            // 
            this.labelLayerName.AutoSize = true;
            this.labelLayerName.Location = new System.Drawing.Point(17, 12);
            this.labelLayerName.Name = "labelLayerName";
            this.labelLayerName.Size = new System.Drawing.Size(48, 21);
            this.labelLayerName.TabIndex = 0;
            this.labelLayerName.Text = "Layer";
            // 
            // checkBoxEnabled
            // 
            this.checkBoxEnabled.AutoSize = true;
            this.checkBoxEnabled.Location = new System.Drawing.Point(17, 47);
            this.checkBoxEnabled.Name = "checkBoxEnabled";
            this.checkBoxEnabled.Size = new System.Drawing.Size(93, 25);
            this.checkBoxEnabled.TabIndex = 1;
            this.checkBoxEnabled.Text = "有効可否";
            this.checkBoxEnabled.UseVisualStyleBackColor = true;
            // 
            // comboBoxAnimeDefinition
            // 
            this.comboBoxAnimeDefinition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAnimeDefinition.FormattingEnabled = true;
            this.comboBoxAnimeDefinition.Location = new System.Drawing.Point(17, 99);
            this.comboBoxAnimeDefinition.Name = "comboBoxAnimeDefinition";
            this.comboBoxAnimeDefinition.Size = new System.Drawing.Size(222, 29);
            this.comboBoxAnimeDefinition.TabIndex = 2;
            this.comboBoxAnimeDefinition.SelectedIndexChanged += new System.EventHandler(this.comboBoxAnimeDefinition_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "アニメ";
            // 
            // valueScrollControlAlpha
            // 
            this.valueScrollControlAlpha.FixedPoint = 100;
            this.valueScrollControlAlpha.Location = new System.Drawing.Point(17, 167);
            this.valueScrollControlAlpha.Margin = new System.Windows.Forms.Padding(4);
            this.valueScrollControlAlpha.MaxValue = 100;
            this.valueScrollControlAlpha.MinValue = 0;
            this.valueScrollControlAlpha.Name = "valueScrollControlAlpha";
            this.valueScrollControlAlpha.Size = new System.Drawing.Size(279, 25);
            this.valueScrollControlAlpha.TabIndex = 4;
            this.valueScrollControlAlpha.Value = 100;
            this.valueScrollControlAlpha.ValueFixedPoint = 1D;
            this.valueScrollControlAlpha.ValueFormat = "{0:F2}";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "透明度";
            // 
            // checkBoxLoopFlag
            // 
            this.checkBoxLoopFlag.AutoSize = true;
            this.checkBoxLoopFlag.Location = new System.Drawing.Point(17, 334);
            this.checkBoxLoopFlag.Name = "checkBoxLoopFlag";
            this.checkBoxLoopFlag.Size = new System.Drawing.Size(96, 25);
            this.checkBoxLoopFlag.TabIndex = 6;
            this.checkBoxLoopFlag.Text = "ループ再生";
            this.checkBoxLoopFlag.UseVisualStyleBackColor = true;
            // 
            // numericUpDownStartFrame
            // 
            this.numericUpDownStartFrame.Location = new System.Drawing.Point(17, 231);
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
            this.numericUpDownStartFrame.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownStartFrame.TabIndex = 7;
            this.numericUpDownStartFrame.ValueChanged += new System.EventHandler(this.numericUpDownFrame_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Start";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "End";
            // 
            // numericUpDownEndFrame
            // 
            this.numericUpDownEndFrame.Location = new System.Drawing.Point(143, 231);
            this.numericUpDownEndFrame.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownEndFrame.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDownEndFrame.Name = "numericUpDownEndFrame";
            this.numericUpDownEndFrame.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownEndFrame.TabIndex = 9;
            this.numericUpDownEndFrame.ValueChanged += new System.EventHandler(this.numericUpDownFrame_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 21);
            this.label5.TabIndex = 12;
            this.label5.Text = "再生速度";
            // 
            // valueScrollControlSpeedRate
            // 
            this.valueScrollControlSpeedRate.FixedPoint = 10;
            this.valueScrollControlSpeedRate.Location = new System.Drawing.Point(17, 300);
            this.valueScrollControlSpeedRate.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.valueScrollControlSpeedRate.MaxValue = 100;
            this.valueScrollControlSpeedRate.MinValue = 1;
            this.valueScrollControlSpeedRate.Name = "valueScrollControlSpeedRate";
            this.valueScrollControlSpeedRate.Size = new System.Drawing.Size(278, 25);
            this.valueScrollControlSpeedRate.TabIndex = 13;
            this.valueScrollControlSpeedRate.Value = 10;
            this.valueScrollControlSpeedRate.ValueFixedPoint = 1D;
            this.valueScrollControlSpeedRate.ValueFormat = "{0:F1}";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(143, 385);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 21);
            this.label6.TabIndex = 17;
            this.label6.Text = "Y";
            // 
            // numericUpDownPosY
            // 
            this.numericUpDownPosY.Location = new System.Drawing.Point(143, 409);
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
            this.numericUpDownPosY.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownPosY.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "X";
            // 
            // numericUpDownPosX
            // 
            this.numericUpDownPosX.Location = new System.Drawing.Point(17, 409);
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
            this.numericUpDownPosX.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownPosX.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 466);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 21);
            this.label8.TabIndex = 18;
            this.label8.Text = "拡大率";
            // 
            // valueScrollControlScaleRate
            // 
            this.valueScrollControlScaleRate.FixedPoint = 10;
            this.valueScrollControlScaleRate.Location = new System.Drawing.Point(17, 495);
            this.valueScrollControlScaleRate.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.valueScrollControlScaleRate.MaxValue = 20;
            this.valueScrollControlScaleRate.MinValue = 1;
            this.valueScrollControlScaleRate.Name = "valueScrollControlScaleRate";
            this.valueScrollControlScaleRate.Size = new System.Drawing.Size(283, 25);
            this.valueScrollControlScaleRate.TabIndex = 19;
            this.valueScrollControlScaleRate.Value = 10;
            this.valueScrollControlScaleRate.ValueFixedPoint = 1D;
            this.valueScrollControlScaleRate.ValueFormat = "x {0:F1}";
            // 
            // labelAnimeDefinitionFrame
            // 
            this.labelAnimeDefinitionFrame.AutoSize = true;
            this.labelAnimeDefinitionFrame.Location = new System.Drawing.Point(248, 102);
            this.labelAnimeDefinitionFrame.Name = "labelAnimeDefinitionFrame";
            this.labelAnimeDefinitionFrame.Size = new System.Drawing.Size(22, 21);
            this.labelAnimeDefinitionFrame.TabIndex = 20;
            this.labelAnimeDefinitionFrame.Text = "--";
            // 
            // labelFrameSpan
            // 
            this.labelFrameSpan.AutoSize = true;
            this.labelFrameSpan.Location = new System.Drawing.Point(269, 233);
            this.labelFrameSpan.Name = "labelFrameSpan";
            this.labelFrameSpan.Size = new System.Drawing.Size(22, 21);
            this.labelFrameSpan.TabIndex = 21;
            this.labelFrameSpan.Text = "--";
            // 
            // LayerSettingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelFrameSpan);
            this.Controls.Add(this.labelAnimeDefinitionFrame);
            this.Controls.Add(this.valueScrollControlScaleRate);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericUpDownPosY);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownPosX);
            this.Controls.Add(this.valueScrollControlSpeedRate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownEndFrame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownStartFrame);
            this.Controls.Add(this.checkBoxLoopFlag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.valueScrollControlAlpha);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxEnabled);
            this.Controls.Add(this.comboBoxAnimeDefinition);
            this.Controls.Add(this.labelLayerName);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LayerSettingControl";
            this.Size = new System.Drawing.Size(300, 600);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStartFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEndFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPosX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLayerName;
        private System.Windows.Forms.CheckBox checkBoxEnabled;
        private System.Windows.Forms.ComboBox comboBoxAnimeDefinition;
        private System.Windows.Forms.Label label1;
        private Clarity.GUI.ValueScrollControl valueScrollControlAlpha;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxLoopFlag;
        private System.Windows.Forms.NumericUpDown numericUpDownStartFrame;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownEndFrame;
        private System.Windows.Forms.Label label5;
        private Clarity.GUI.ValueScrollControl valueScrollControlSpeedRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownPosY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownPosX;
        private System.Windows.Forms.Label label8;
        private Clarity.GUI.ValueScrollControl valueScrollControlScaleRate;
        private System.Windows.Forms.Label labelAnimeDefinitionFrame;
        private System.Windows.Forms.Label labelFrameSpan;
    }
}
