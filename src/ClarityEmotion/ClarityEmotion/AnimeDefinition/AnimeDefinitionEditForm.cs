using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.GUI;

namespace ClarityEmotion.AnimeDefinition
{
    /// <summary>
    /// アニメ定義画面
    /// </summary>
    public partial class AnimeDefinitionEditForm : Form
    {
        public AnimeDefinitionEditForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 管理リスト
        /// </summary>
        private List<AnimeDefinitionData> AnimeDataList = new List<AnimeDefinitionData>();

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="inlist"></param>
        public void Init(List<AnimeDefinitionData> inlist)
        {
            this.AnimeDataList = new List<AnimeDefinitionData>();
            this.AnimeDataList.AddRange(inlist);

            //データの表示
            this.DispAnimeDefinitionList();
        }


        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public List<AnimeDefinitionData> GetInputData()
        {
            return this.AnimeDataList;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// データの表示
        /// </summary>
        private void DispAnimeDefinitionList()
        {
            //データの表示
            this.listBoxAnimeList.Items.Clear();
            this.listBoxAnimeList.Items.AddRange(this.AnimeDataList.ToArray());
        }

        /// <summary>
        /// アニメの確定処理
        /// </summary>
        private async Task ApplyAnime()
        {

            AnimeDefinitionData adata = await Task.Run(() =>
            {
                return this.animeDefinitionControl1.GetInputData();
            });

            //データの反映
            int sindex = this.listBoxAnimeList.SelectedIndex;
            this.AnimeDataList[sindex] = adata;

            this.DispAnimeDefinitionList();

            this.listBoxAnimeList.SelectedIndex = sindex;


        }

        /// <summary>
        /// アニメの削除
        /// </summary>
        private void RemoveAnime()
        {
            int sindex = this.listBoxAnimeList.SelectedIndex;
            this.AnimeDataList.RemoveAt(sindex);
            this.DispAnimeDefinitionList();

            this.animeDefinitionControl1.SetData(null);
        }

        /// <summary>
        /// データのチェック
        /// </summary>
        /// <returns></returns>
        private void CheckData()
        {
            //名前被りのチェック
            int n = this.AnimeDataList.Select(x => x.Name).Distinct().Count();
            if (n != this.AnimeDataList.Count)
            {
                throw new Exception("同じ名前は許されていません");
            }

            //アニメ画像無しのチェック
            this.AnimeDataList.ForEach(x =>
            {
                if (x.ImageDataList.Count <= 0)
                {
                    throw new Exception("アニメ情報がないデータがあります");
                }
            });
        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimeDefinitionEditForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// アニメリストが選択された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxAnimeList_SelectedValueChanged(object sender, EventArgs e)
        {
            AnimeDefinitionData adata = this.listBoxAnimeList.SelectedItem as AnimeDefinitionData;
            this.animeDefinitionControl1.SetData(adata);
        }
        
        /// <summary>
        /// アニメ追加ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnimeAdd_Click(object sender, EventArgs e)
        {
            this.AnimeDataList.Add(new AnimeDefinitionData() { Name = "new animation" });
            this.DispAnimeDefinitionList();

        }

        /// <summary>
        /// アニメ削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnimeRemove_Click(object sender, EventArgs e)
        {
            this.RemoveAnime();
        }

        /// <summary>
        /// アニメ確定ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void buttonAnimeApply_Click(object sender, EventArgs e)
        {
            try
            {
                using (AsyncControlState st = new AsyncControlState(this))
                {
                    await this.ApplyAnime();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("失敗しました");
            }

        }


        /// <summary>
        /// OKが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.CheckData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キャンセルが押された時z
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
