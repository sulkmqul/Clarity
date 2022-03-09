using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion.AnimeDefinition
{
    public partial class AnimeDefinitionControl : UserControl
    {
        public AnimeDefinitionControl()
        {
            InitializeComponent();
        }

        AnimeDefinitionData AData = null;


        /// <summary>
        /// 表示データの設定
        /// </summary>
        /// <param name="adata">表示データ</param>
        public void SetData(AnimeDefinitionData adata)
        {
            this.AData = adata;

            this.textBoxName.Text = "";
            this.listBoxImageFilePath.Items.Clear();


            this.textBoxName.Text = adata?.Name;
            adata?.ImageDataList.ForEach(x =>
            {
                this.listBoxImageFilePath.Items.Add(x.FilePath);
            });

        }

        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public AnimeDefinitionData GetInputData()
        {
            AnimeDefinitionData adata = new AnimeDefinitionData();
            adata.Id = this.AData.Id;

            adata.Name = this.textBoxName.Text.Trim();

            adata.ImageDataList = new List<ImagePathSet>();
            foreach (object item in this.listBoxImageFilePath.Items)
            {
                ImagePathSet ipath = new ImagePathSet(item.ToString());
                adata.ImageDataList.Add(ipath);
            }

            if (adata.ImageDataList.Count <= 0)
            {
                throw new Exception("アニメ情報がありません");
            }

            return adata;
        } 


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// パスの追加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddPath_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Multiselect = true;
            DialogResult dret = this.openFileDialog1.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            foreach (string sp in this.openFileDialog1.FileNames)
            {
                this.listBoxImageFilePath.Items.Add(sp);
            }
        }

        
    }
}
