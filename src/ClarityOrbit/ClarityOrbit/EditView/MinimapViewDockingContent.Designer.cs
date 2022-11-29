namespace ClarityOrbit.EditView
{
    partial class MinimapViewDockingContent
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
            this.clarityViewerMinimap = new Clarity.GUI.ClarityViewer();
            this.SuspendLayout();
            // 
            // clarityViewerMinimap
            // 
            this.clarityViewerMinimap.ClearColor = System.Drawing.Color.White;
            this.clarityViewerMinimap.DisplayAreaLineColor = System.Drawing.Color.Red;
            this.clarityViewerMinimap.DisplayAreaLineWidth = 1F;
            this.clarityViewerMinimap.DisplayerRendering = true;
            this.clarityViewerMinimap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clarityViewerMinimap.DoubleClickFitCentering = true;
            this.clarityViewerMinimap.ImageClippingEnabled = true;
            this.clarityViewerMinimap.ImageInterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
            this.clarityViewerMinimap.Location = new System.Drawing.Point(0, 0);
            this.clarityViewerMinimap.MinimapBackColor = System.Drawing.Color.White;
            this.clarityViewerMinimap.MinimapDisplayMerginRate = 0.05F;
            this.clarityViewerMinimap.MinimapVisible = false;
            this.clarityViewerMinimap.Name = "clarityViewerMinimap";
            this.clarityViewerMinimap.PosMode = Clarity.GUI.EClarityViewerPositionMode.LeftTop;
            this.clarityViewerMinimap.Size = new System.Drawing.Size(800, 450);
            this.clarityViewerMinimap.SrcBackColor = System.Drawing.Color.Black;
            this.clarityViewerMinimap.TabIndex = 0;
            this.clarityViewerMinimap.ZoomMode = Clarity.GUI.EClarityViewerZoomMode.LimitFit;
            this.clarityViewerMinimap.ClarityViewerMouseMoveEvent += new Clarity.GUI.ClarityViewerMouseEventDelegate(this.clarityViewerMinimap_ClarityViewerMouseMoveEvent);
            this.clarityViewerMinimap.ClarityViewerMouseUpEvent += new Clarity.GUI.ClarityViewerMouseEventDelegate(this.clarityViewerMinimap_ClarityViewerMouseUpEvent);
            // 
            // MinimapViewDockingContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.clarityViewerMinimap);
            this.Name = "MinimapViewDockingContent";
            this.Text = "ミニマップ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MinimapDockingContent_FormClosed);
            this.Load += new System.EventHandler(this.MinimapDockingContent_Load);
            this.Resize += new System.EventHandler(this.MinimapViewDockingContent_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Clarity.GUI.ClarityViewer clarityViewerMinimap;
    }
}