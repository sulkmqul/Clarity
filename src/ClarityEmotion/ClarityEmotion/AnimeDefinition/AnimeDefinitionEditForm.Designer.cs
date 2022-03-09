
namespace ClarityEmotion.AnimeDefinition
{
    partial class AnimeDefinitionEditForm
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
            this.listBoxAnimeList = new System.Windows.Forms.ListBox();
            this.buttonAnimeAdd = new System.Windows.Forms.Button();
            this.buttonAnimeRemove = new System.Windows.Forms.Button();
            this.animeDefinitionControl1 = new ClarityEmotion.AnimeDefinition.AnimeDefinitionControl();
            this.buttonAnimeApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxAnimeList
            // 
            this.listBoxAnimeList.FormattingEnabled = true;
            this.listBoxAnimeList.ItemHeight = 21;
            this.listBoxAnimeList.Location = new System.Drawing.Point(12, 12);
            this.listBoxAnimeList.Name = "listBoxAnimeList";
            this.listBoxAnimeList.Size = new System.Drawing.Size(331, 487);
            this.listBoxAnimeList.TabIndex = 0;
            this.listBoxAnimeList.SelectedValueChanged += new System.EventHandler(this.listBoxAnimeList_SelectedValueChanged);
            // 
            // buttonAnimeAdd
            // 
            this.buttonAnimeAdd.Location = new System.Drawing.Point(157, 505);
            this.buttonAnimeAdd.Name = "buttonAnimeAdd";
            this.buttonAnimeAdd.Size = new System.Drawing.Size(90, 35);
            this.buttonAnimeAdd.TabIndex = 5;
            this.buttonAnimeAdd.Text = "追加";
            this.buttonAnimeAdd.UseVisualStyleBackColor = true;
            this.buttonAnimeAdd.Click += new System.EventHandler(this.buttonAnimeAdd_Click);
            // 
            // buttonAnimeRemove
            // 
            this.buttonAnimeRemove.Location = new System.Drawing.Point(253, 505);
            this.buttonAnimeRemove.Name = "buttonAnimeRemove";
            this.buttonAnimeRemove.Size = new System.Drawing.Size(90, 35);
            this.buttonAnimeRemove.TabIndex = 5;
            this.buttonAnimeRemove.Text = "削除";
            this.buttonAnimeRemove.UseVisualStyleBackColor = true;
            this.buttonAnimeRemove.Click += new System.EventHandler(this.buttonAnimeRemove_Click);
            // 
            // animeDefinitionControl1
            // 
            this.animeDefinitionControl1.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.animeDefinitionControl1.Location = new System.Drawing.Point(350, 12);
            this.animeDefinitionControl1.Margin = new System.Windows.Forms.Padding(4);
            this.animeDefinitionControl1.Name = "animeDefinitionControl1";
            this.animeDefinitionControl1.Size = new System.Drawing.Size(442, 437);
            this.animeDefinitionControl1.TabIndex = 6;
            // 
            // buttonAnimeApply
            // 
            this.buttonAnimeApply.Location = new System.Drawing.Point(702, 456);
            this.buttonAnimeApply.Name = "buttonAnimeApply";
            this.buttonAnimeApply.Size = new System.Drawing.Size(90, 35);
            this.buttonAnimeApply.TabIndex = 7;
            this.buttonAnimeApply.Text = "確定";
            this.buttonAnimeApply.UseVisualStyleBackColor = true;
            this.buttonAnimeApply.Click += new System.EventHandler(this.buttonAnimeApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(703, 514);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 35);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "キャンセル";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(607, 514);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(90, 35);
            this.buttonOk.TabIndex = 9;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // AnimeDefinitionEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 561);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonAnimeApply);
            this.Controls.Add(this.animeDefinitionControl1);
            this.Controls.Add(this.buttonAnimeRemove);
            this.Controls.Add(this.buttonAnimeAdd);
            this.Controls.Add(this.listBoxAnimeList);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AnimeDefinitionEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AnimeDefinitionEditForm";
            this.Load += new System.EventHandler(this.AnimeDefinitionEditForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAnimeList;
        private System.Windows.Forms.Button buttonAnimeAdd;
        private System.Windows.Forms.Button buttonAnimeRemove;
        private AnimeDefinitionControl animeDefinitionControl1;
        private System.Windows.Forms.Button buttonAnimeApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
    }
}