using Clarity;
using ClarityMovement.MotionFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClarityMovement
{
    /// <summary>
    /// タグ入力画面
    /// </summary>
    public partial class TagInputForm : Form
    {
        public TagInputForm(FrameTagModifier? srcdata = null)
        {
            InitializeComponent();

            //元データ設定
            this.SrcData = srcdata;

        }

        /// <summary>
        /// タグの元データ
        /// </summary>
        private FrameTagModifier? SrcData { get; init; } = null;


        /// <summary>
        /// 入力情報 nullでなし
        /// </summary>
        public FrameTagModifier? InputData { get; private set; } = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitForm()
        {
            //タイプComboの初期化
            EClaritySettingDataType[] typevec = (EClaritySettingDataType[])Enum.GetValues(typeof(EClaritySettingDataType));
            this.comboBoxType.Items.Clear();
            foreach(var tv in typevec)
            {
                this.comboBoxType.Items.Add(tv);
            }
            this.comboBoxType.SelectedIndex = 0;

            //元データの表示
            if(this.SrcData != null)
            {
                this.DispData(this.SrcData);
            }
        }

        /// <summary>
        /// データの描画
        /// </summary>
        /// <param name="mdata"></param>
        private void DispData(FrameTagModifier mdata)
        {
            this.textBoxCode.Text = mdata.Data.TagName;
            this.comboBoxType.SelectedItem = mdata.Data.DataType;
            this.textBoxValue.Text =  mdata.Data.ToString()?.Replace("<", "").Replace(">", "");
        }


        /// <summary>
        /// 入力情報の取得
        /// </summary>        
        private ClarityMotionTag? GetInputData()
        {
            ClaritySettingCoreReader cr = new ClaritySettingCoreReader();

            EClaritySettingDataType dt = (EClaritySettingDataType)this.comboBoxType.SelectedItem;
            string tag = this.textBoxCode.Text.Trim();
            string sval = this.textBoxValue.Text.Trim();

            return cr.Analyze<ClarityMotionTag>(dt, sval, tag);
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagInputForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 表示されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagInputForm_Shown(object sender, EventArgs e)
        {
            this.InitForm();
        }

        /// <summary>
        /// OKボタンが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                //入力の取得
                var data = this.GetInputData();
                if (data == null)
                {
                    throw new Exception("入力情報を取得できません。");
                }
                
                //入力情報の作成
                this.InputData = new FrameTagModifier();
                if (this.SrcData != null)
                {
                    this.InputData = this.SrcData;
                }
                //値の設定
                this.InputData.Data = data;

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message);
            }
        }

        /// <summary>
        /// キャンセルが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.InputData = null;
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
