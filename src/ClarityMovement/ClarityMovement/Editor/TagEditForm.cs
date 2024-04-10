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
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        public BaseEditTag GetInput()
        {
            BaseEditTag ans = new EditTagCollison();

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

        
    }
}
