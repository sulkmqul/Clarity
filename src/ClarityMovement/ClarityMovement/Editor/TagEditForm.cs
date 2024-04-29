using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Editor
{
    /// <summary>
    /// タグ編集画面
    /// </summary>
    public partial class TagEditForm : Form
    {
        public TagEditForm(BaseEditTag? tag = null)
        {
            InitializeComponent();

            this.ETag = tag;
        }

        /// <summary>
        /// 編集タグ nullで新規
        /// </summary>
        private BaseEditTag? ETag = null;

        /// <summary>
        /// タグ編集者
        /// </summary>
        private ITagEditControl? TagControl = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// タグ情報の取得
        /// </summary>
        /// <returns></returns>
        public BaseEditTag GetInput()
        {
            //tagtypeに応じて取得項目を変える。
            BaseEditTag ans = new EditTagCollison();

            //基礎を最後に設定する。

            ans.Type = (EMovementTagType)this.comboBoxTagType.SelectedItem;

            //開始終了フレーム
            ans.StartFrame = (int)this.numericUpDownStartFrame.Value;
            ans.EndFrame = (int)this.numericUpDownEndFrame.Value;

            //相対位置
            float rex = (float)this.numericUpDownRePosX.Value;
            float rey = (float)this.numericUpDownRePosY.Value;
            float rez = (float)this.numericUpDownRePosZ.Value;

            ans.RelativePos = new System.Numerics.Vector4(rex, rey, rez, 1.0f);


            return ans;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitForm()
        {
            //control最大値の設定
            int max = MvGlobal.Project.BaseImageList.Count;
            this.numericUpDownStartFrame.Maximum = max;
            this.numericUpDownEndFrame.Maximum = max;

            //tag type選択肢の差k末井
            this.CreateTagTypeCombo(this.comboBoxTagType);
        }

        /// <summary>
        /// tagcomboの作成
        /// </summary>
        /// <param name="combo"></param>
        private void CreateTagTypeCombo(ComboBox combo)
        {
            combo.Items.Clear();
            var vec = Enum.GetValues<EMovementTagType>();
            foreach (EMovementTagType t in vec)
            {
                combo.Items.Add(t);
            }
            combo.SelectedIndex = 0;
        }

        /// <summary>
        /// タグタイプの変更
        /// </summary>
        /// <param name="mt"></param>
        private void ChangeTagTypeControl(EMovementTagType mt)
        {
            this.panelTagTypeControl.Controls.Clear();

            Dictionary<EMovementTagType, Control> dic = new Dictionary<EMovementTagType, Control>();
            dic.Add(EMovementTagType.Collision, new TagEditCollisionControl());
            dic.Add(EMovementTagType.Image, new TagEditImageControl());
            dic.Add(EMovementTagType.Info, new TagEditInfoControl());

            Control con = dic[mt];
            con.Dock = DockStyle.Fill;
            this.TagControl = con as ITagEditControl;
            this.panelTagTypeControl.Controls.Add(con);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagEditForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 表示された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagEditForm_Shown(object sender, EventArgs e)
        {
            this.InitForm();
        }


        /// <summary>
        /// OKボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キャンセルボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// TagType選択が変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTagType_SelectedIndexChanged(object sender, EventArgs e)
        {
            EMovementTagType mt = (EMovementTagType)this.comboBoxTagType.SelectedItem;
            this.ChangeTagTypeControl(mt);
        }
    }
}
