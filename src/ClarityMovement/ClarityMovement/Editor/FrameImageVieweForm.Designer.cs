namespace ClarityMovement.Editor
{
    partial class FrameImageVieweForm
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
            clarityImageViewer1 = new Clarity.GUI.ClarityImageViewer();
            SuspendLayout();
            // 
            // clarityImageViewer1
            // 
            clarityImageViewer1.BorderLineColor = Color.Red;
            clarityImageViewer1.BorderLineRendering = false;
            clarityImageViewer1.ClearColor = Color.Black;
            clarityImageViewer1.DisplayAreaLineColor = Color.Red;
            clarityImageViewer1.DisplayAreaLineWidth = 1F;
            clarityImageViewer1.DisplayerRendering = true;
            clarityImageViewer1.Dock = DockStyle.Fill;
            clarityImageViewer1.DoubleClickFitCentering = true;
            clarityImageViewer1.ImageClippingEnabled = true;
            clarityImageViewer1.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            clarityImageViewer1.Location = new Point(0, 0);
            clarityImageViewer1.MinimapBackColor = Color.White;
            clarityImageViewer1.MinimapDisplayMerginRate = 0.05F;
            clarityImageViewer1.MinimapVisible = false;
            clarityImageViewer1.Name = "clarityImageViewer1";
            clarityImageViewer1.Size = new Size(800, 450);
            clarityImageViewer1.SrcBackColor = Color.Red;
            clarityImageViewer1.TabIndex = 0;
            clarityImageViewer1.MouseUp += clarityImageViewer1_MouseUp;
            // 
            // FrameImageVieweForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(clarityImageViewer1);
            Name = "FrameImageVieweForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FrameImageViewer";
            Load += FrameImageViewer_Load;
            ResumeLayout(false);
        }

        #endregion

        private Clarity.GUI.ClarityImageViewer clarityImageViewer1;
    }
}