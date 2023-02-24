using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement
{
    /// <summary>
    /// プロジェクトの作成
    /// </summary>
    public partial class CreateProjectForm : Form
    {
        public CreateProjectForm()
        {
            InitializeComponent();
        }


        public class ResultData
        {
            public Vector2 Size = new Vector2();
            public float FPS = 60.0f;
            public int MaxFrame = 0;

        }



        /// <summary>
        /// 入力の取得
        /// </summary>
        /// <returns></returns>
        public ResultData GetInputData()
        {
            ResultData ans = new ResultData();

            ans.MaxFrame = Convert.ToInt32(this.numericUpDownFrame.Value);
            ans.FPS = Convert.ToSingle(this.numericUpDownFPS.Value);
            ans.Size.X = Convert.ToSingle(this.numericUpDownSizeW.Value);
            ans.Size.Y = Convert.ToSingle(this.numericUpDownSizeH.Value);

            return ans;
        }
        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateProjectForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// OKボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.GetInputData();

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"失敗:{ex}");
            }
        }

        /// <summary>
        /// キャンセルが押されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }
    }
}
