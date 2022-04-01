using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion.LayerControl
{
    /// <summary>
    /// レイヤーアニメーション定義
    /// </summary>
    public partial class LayerAnimeControl : UserControl
    {
        public LayerAnimeControl()
        {
            InitializeComponent();

            this.FData = new LayerAnimeControlData();
            this.Logic = new LayerAnimeControlLogic(this, this.FData);
        }

        #region メンバ変数
        /// <summary>
        /// これのデータ
        /// </summary>
        private LayerAnimeControlData FData = null;
        /// <summary>
        /// これのロジック
        /// </summary>
        private LayerAnimeControlLogic Logic = null;
        
        #endregion


        /// <summary>
        /// コントロールの初期化
        /// </summary>
        public void InitControl()
        {
            this.Logic.Init(this.panelLayerTimeControl);
        }

        /// <summary>
        /// コントロールの再初期化
        /// </summary>
        public void ReInitControl()
        {
            this.FData.ControlList.ForEach(x => x.Layer.SetScale(this.FData.FramePixelRate, EmotionProject.Mana.BasicInfo.MaxFrame));
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 拡大ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonScalePlus_Click(object sender, EventArgs e)
        {
            this.Logic.ChangeScale(true);
        }

        /// <summary>
        /// 縮小ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonScaleMinus_Click(object sender, EventArgs e)
        {
            this.Logic.ChangeScale(false);
        }

        /// <summary>
        /// 再生開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPlayStart_Click(object sender, EventArgs e)
        {
            EmotionProject.Mana.Info.PlayFlag = true;
        }

        /// <summary>
        /// 再生停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonPlayStop_Click(object sender, EventArgs e)
        {
            EmotionProject.Mana.Info.PlayFlag = false;
        }

        /// <summary>
        /// 再生位置初期化が押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonResetFrame_Click(object sender, EventArgs e)
        {
            EmotionProject.Mana.Info.FramePosition = 0;
        }
    }
}
