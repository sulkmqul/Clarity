namespace ClarityMovement
{
    partial class BaseSrcImageForm
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
            this.components = new System.ComponentModel.Container();
            this.listViewSrcImage = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewSrcImage
            // 
            this.listViewSrcImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewSrcImage.LargeImageList = this.imageList1;
            this.listViewSrcImage.Location = new System.Drawing.Point(12, 36);
            this.listViewSrcImage.Name = "listViewSrcImage";
            this.listViewSrcImage.Size = new System.Drawing.Size(433, 368);
            this.listViewSrcImage.TabIndex = 1;
            this.listViewSrcImage.UseCompatibleStateImageBehavior = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            // 
            // BaseSrcImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 450);
            this.Controls.Add(this.listViewSrcImage);
            this.Name = "BaseSrcImageForm";
            this.Text = "BaseSrcImageForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BaseSrcImageForm_FormClosed);
            this.Load += new System.EventHandler(this.BaseSrcImageForm_Load);
            this.Shown += new System.EventHandler(this.BaseSrcImageForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion
        public ListView listViewSrcImage;
        public ImageList imageList1;
    }
}