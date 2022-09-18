namespace ClarityOrbit.LayerView
{
    partial class LayerViewDockingContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerViewDockingContent));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonLayerAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLayerRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLayerUp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLayerDown = new System.Windows.Forms.ToolStripButton();
            this.dataGridViewLayerGird = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLayerGird)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonLayerAdd,
            this.toolStripButtonLayerRemove,
            this.toolStripSeparator1,
            this.toolStripButtonLayerUp,
            this.toolStripButtonLayerDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStripLayerMenu";
            // 
            // toolStripButtonLayerAdd
            // 
            this.toolStripButtonLayerAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLayerAdd.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLayerAdd.Image")));
            this.toolStripButtonLayerAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLayerAdd.Name = "toolStripButtonLayerAdd";
            this.toolStripButtonLayerAdd.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLayerAdd.Text = "toolStripButton1";
            this.toolStripButtonLayerAdd.ToolTipText = "レイヤー追加";
            // 
            // toolStripButtonLayerRemove
            // 
            this.toolStripButtonLayerRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLayerRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLayerRemove.Image")));
            this.toolStripButtonLayerRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLayerRemove.Name = "toolStripButtonLayerRemove";
            this.toolStripButtonLayerRemove.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLayerRemove.Text = "toolStripButton1";
            this.toolStripButtonLayerRemove.ToolTipText = "レイヤー削除";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonLayerUp
            // 
            this.toolStripButtonLayerUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLayerUp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLayerUp.Image")));
            this.toolStripButtonLayerUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLayerUp.Name = "toolStripButtonLayerUp";
            this.toolStripButtonLayerUp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLayerUp.Text = "toolStripButton1";
            this.toolStripButtonLayerUp.ToolTipText = "上へ";
            // 
            // toolStripButtonLayerDown
            // 
            this.toolStripButtonLayerDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLayerDown.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonLayerDown.Image")));
            this.toolStripButtonLayerDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLayerDown.Name = "toolStripButtonLayerDown";
            this.toolStripButtonLayerDown.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonLayerDown.Text = "toolStripButton1";
            this.toolStripButtonLayerDown.ToolTipText = "下へ";
            // 
            // dataGridViewLayerGird
            // 
            this.dataGridViewLayerGird.AllowUserToAddRows = false;
            this.dataGridViewLayerGird.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLayerGird.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLayerGird.ColumnHeadersVisible = false;
            this.dataGridViewLayerGird.Location = new System.Drawing.Point(12, 28);
            this.dataGridViewLayerGird.MultiSelect = false;
            this.dataGridViewLayerGird.Name = "dataGridViewLayerGird";
            this.dataGridViewLayerGird.RowHeadersVisible = false;
            this.dataGridViewLayerGird.RowTemplate.Height = 25;
            this.dataGridViewLayerGird.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewLayerGird.Size = new System.Drawing.Size(776, 410);
            this.dataGridViewLayerGird.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "lf_visible.bmp");
            this.imageList1.Images.SetKeyName(1, "lf_lock.bmp");
            // 
            // LayerViewDockingContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridViewLayerGird);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LayerViewDockingContent";
            this.Text = "レイヤー";
            this.Load += new System.EventHandler(this.LayerViewDockingContent_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLayerGird)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dataGridViewLayerGird;
        private System.Windows.Forms.ToolStripButton toolStripButtonLayerAdd;
        private System.Windows.Forms.ToolStripButton toolStripButtonLayerRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonLayerUp;
        private System.Windows.Forms.ToolStripButton toolStripButtonLayerDown;
        private System.Windows.Forms.ImageList imageList1;
    }
}