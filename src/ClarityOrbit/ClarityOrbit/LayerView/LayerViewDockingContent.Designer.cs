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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerViewDockingContent));
            toolStrip1 = new ToolStrip();
            toolStripButtonLayerAdd = new ToolStripButton();
            toolStripButtonLayerRemove = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripButtonLayerUp = new ToolStripButton();
            toolStripButtonLayerDown = new ToolStripButton();
            dataGridViewLayerGird = new DataGridView();
            imageList1 = new ImageList(components);
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLayerGird).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButtonLayerAdd, toolStripButtonLayerRemove, toolStripSeparator1, toolStripButtonLayerUp, toolStripButtonLayerDown });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(800, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStripLayerMenu";
            // 
            // toolStripButtonLayerAdd
            // 
            toolStripButtonLayerAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerAdd.Image = (Image)resources.GetObject("toolStripButtonLayerAdd.Image");
            toolStripButtonLayerAdd.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerAdd.Name = "toolStripButtonLayerAdd";
            toolStripButtonLayerAdd.Size = new Size(23, 22);
            toolStripButtonLayerAdd.Text = "toolStripButton1";
            toolStripButtonLayerAdd.ToolTipText = "レイヤー追加";
            toolStripButtonLayerAdd.Click += toolStripButtonLayerAdd_Click;
            // 
            // toolStripButtonLayerRemove
            // 
            toolStripButtonLayerRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerRemove.Image = (Image)resources.GetObject("toolStripButtonLayerRemove.Image");
            toolStripButtonLayerRemove.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerRemove.Name = "toolStripButtonLayerRemove";
            toolStripButtonLayerRemove.Size = new Size(23, 22);
            toolStripButtonLayerRemove.Text = "toolStripButton1";
            toolStripButtonLayerRemove.ToolTipText = "レイヤー削除";
            toolStripButtonLayerRemove.Click += toolStripButtonLayerRemove_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // toolStripButtonLayerUp
            // 
            toolStripButtonLayerUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerUp.Image = (Image)resources.GetObject("toolStripButtonLayerUp.Image");
            toolStripButtonLayerUp.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerUp.Name = "toolStripButtonLayerUp";
            toolStripButtonLayerUp.Size = new Size(23, 22);
            toolStripButtonLayerUp.Text = "toolStripButton1";
            toolStripButtonLayerUp.ToolTipText = "上へ";
            toolStripButtonLayerUp.Click += toolStripButtonLayerUp_Click;
            // 
            // toolStripButtonLayerDown
            // 
            toolStripButtonLayerDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            toolStripButtonLayerDown.Image = (Image)resources.GetObject("toolStripButtonLayerDown.Image");
            toolStripButtonLayerDown.ImageTransparentColor = Color.Magenta;
            toolStripButtonLayerDown.Name = "toolStripButtonLayerDown";
            toolStripButtonLayerDown.Size = new Size(23, 22);
            toolStripButtonLayerDown.Text = "toolStripButton1";
            toolStripButtonLayerDown.ToolTipText = "下へ";
            toolStripButtonLayerDown.Click += toolStripButtonLayerDown_Click;
            // 
            // dataGridViewLayerGird
            // 
            dataGridViewLayerGird.AllowUserToAddRows = false;
            dataGridViewLayerGird.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewLayerGird.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLayerGird.ColumnHeadersVisible = false;
            dataGridViewLayerGird.Location = new Point(12, 28);
            dataGridViewLayerGird.MultiSelect = false;
            dataGridViewLayerGird.Name = "dataGridViewLayerGird";
            dataGridViewLayerGird.RowHeadersVisible = false;
            dataGridViewLayerGird.RowTemplate.Height = 25;
            dataGridViewLayerGird.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewLayerGird.Size = new Size(776, 410);
            dataGridViewLayerGird.TabIndex = 1;
            dataGridViewLayerGird.CellDoubleClick += dataGridViewLayerGird_CellDoubleClick;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth24Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "lf_visible.bmp");
            imageList1.Images.SetKeyName(1, "lf_lock.bmp");
            // 
            // LayerViewDockingContent
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewLayerGird);
            Controls.Add(toolStrip1);
            Name = "LayerViewDockingContent";
            Text = "レイヤー";
            Load += LayerViewDockingContent_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLayerGird).EndInit();
            ResumeLayout(false);
            PerformLayout();
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