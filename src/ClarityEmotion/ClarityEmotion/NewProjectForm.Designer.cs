﻿
namespace ClarityEmotion
{
    partial class NewProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownImageWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownImageHeight = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownMaxFrame = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 43);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "W";
            // 
            // numericUpDownImageWidth
            // 
            this.numericUpDownImageWidth.Location = new System.Drawing.Point(74, 35);
            this.numericUpDownImageWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownImageWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageWidth.Name = "numericUpDownImageWidth";
            this.numericUpDownImageWidth.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownImageWidth.TabIndex = 1;
            this.numericUpDownImageWidth.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // numericUpDownImageHeight
            // 
            this.numericUpDownImageHeight.Location = new System.Drawing.Point(274, 35);
            this.numericUpDownImageHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownImageHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownImageHeight.Name = "numericUpDownImageHeight";
            this.numericUpDownImageHeight.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownImageHeight.TabIndex = 3;
            this.numericUpDownImageHeight.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "H";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(139, 118);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(90, 35);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(235, 118);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 35);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Max Frame";
            // 
            // numericUpDownMaxFrame
            // 
            this.numericUpDownMaxFrame.Location = new System.Drawing.Point(274, 70);
            this.numericUpDownMaxFrame.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMaxFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMaxFrame.Name = "numericUpDownMaxFrame";
            this.numericUpDownMaxFrame.Size = new System.Drawing.Size(120, 29);
            this.numericUpDownMaxFrame.TabIndex = 1;
            this.numericUpDownMaxFrame.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 165);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.numericUpDownImageHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownMaxFrame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownImageWidth);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NewProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New";
            this.Load += new System.EventHandler(this.NewProjectForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownImageHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMaxFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownImageWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownImageHeight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private Label label3;
        private NumericUpDown numericUpDownMaxFrame;
    }
}