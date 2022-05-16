using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Util;

namespace ClarityEmotion.LayerSetting
{
    /// <summary>
    /// レイヤー設定コントロール
    /// </summary>
    public partial class LayerSettingControl : UserControl
    {
        public LayerSettingControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// これ管理データ
        /// </summary>
        private AnimeElement AData
        {
            get
            {
                return EmotionProject.Mana.Anime.LayerList[EmotionProject.Mana.Info.SelectLayerNo];
            }
        }

        /// <summary>
        /// 更新サイクル
        /// </summary>
        ClarityCycling UpdateCycle = null;

        /// <summary>
        /// データ収集サイクル
        /// </summary>
        ClarityCycling CollectCycle = null;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init()
        {

            this.UpdateCycle = new ClarityCycling();
            {
                //更新サイクル有効可否設定
                this.UpdateCycle.EnabledProc = () =>
                {
                    //有効なときは自動更新を無効化する
                    if (this.ActiveControl == null)
                    {                        
                        return true;
                    }
                    
                    return false;
                };

                //更新処理開始
                this.UpdateCycle.StartCycling(() =>
                {
                    this.DisplayLayerData();
                });
            }

            this.CollectCycle = new ClarityCycling();
            {
                //入力収集サイクル。有効な時のみ
                this.CollectCycle.EnabledProc = () =>
                {
                    //有効なときのみ収集する
                    if (this.ActiveControl == null)
                    {                        
                        return false;
                    }

                    return true;
                };

                //収集処理
                this.CollectCycle.StartCycling(() =>
                {
                    this.CollectInputData();

                }, 500);
            }

            //コントロールの初期化
            this.InitAnimeDefinitionComboBox();
            this.InitFlipComboBox();
        }

        /// <summary>
        /// アニメ定義の初期化
        /// </summary>
        public void InitDefinition()
        {
            //コントロールの初期化
            this.InitAnimeDefinitionComboBox();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// アニメ定義コンボボックスの初期化
        /// </summary>
        private void InitAnimeDefinitionComboBox()
        {
            this.comboBoxAnimeDefinition.Items.Clear();

            foreach (AnimeDefinitionData adata in EmotionProject.Mana.Anime.AnimeDefinitionDic.Values)
            {
                this.comboBoxAnimeDefinition.Items.Add(adata);
            }
        }


        /// <summary>
        /// 反転可否コンボの作成
        /// </summary>
        private void InitFlipComboBox()
        {
            EFlipType[] stvec = (EFlipType[])Enum.GetValues(typeof(EFlipType));
            this.comboBoxFlipState.Items.Clear();
            foreach (var ee in stvec)
            {
                this.comboBoxFlipState.Items.Add(ee);
            }
        }

        /// <summary>
        /// データの表示
        /// </summary>
        private void DisplayLayerData()
        {
            try
            {
                //選択レイヤの取得
                AnimeElement elemdata = EmotionProject.Mana.SelectLayerData;
                if (elemdata == null)
                {
                    return;
                }
                EmotionAnimeData adata = elemdata.EaData;

                //名前
                this.labelLayerName.Text = elemdata.CreateLayerName();

                //有効可否
                this.checkBoxEnabled.Checked = adata.Enabled;

                //アニメ定義
                this.comboBoxAnimeDefinition.SelectedItem = elemdata.SelectAnime;

                //透明度
                this.valueScrollControlAlpha.ValueFixedPoint = adata.Alpha;

                //フレーム
                this.numericUpDownStartFrame.Value = Convert.ToDecimal(adata.StartFrame);
                this.numericUpDownEndFrame.Value = Convert.ToDecimal(adata.EndFrame);

                //再生速度
                this.valueScrollControlSpeedRate.ValueFixedPoint = adata.SpeedRate;

                //フレームオフセット
                this.numericUpDownFrameOffset.Value = adata.FrameOffset;

                //ループ可否
                this.checkBoxLoopFlag.Checked = adata.LoopFlag;

                //位置
                this.numericUpDownPosX.Value = Convert.ToDecimal(adata.Pos2D.X);
                this.numericUpDownPosY.Value = Convert.ToDecimal(adata.Pos2D.Y);

                //拡縮率
                this.valueScrollControlScaleRate.ValueFixedPoint = adata.ScaleRate;

                //Flip
                this.comboBoxFlipState.SelectedItem = adata.FlipType;
            }
            catch (Exception ex)
            {
                //System.Diagnostics.Trace.WriteLine("tonda");
                //表示できなくても無視
            }

        }

        /// <summary>
        /// 入力情報の収集
        /// </summary>
        private void CollectInputData()
        {
            //選択レイヤの取得
            AnimeElement adata = EmotionProject.Mana.SelectLayerData;
            if (adata == null)
            {
                return;
            }

            //有効可否
            adata.EaData.Enabled = this.checkBoxEnabled.Checked;

            //アニメ定義
            AnimeDefinitionData adef = this.comboBoxAnimeDefinition.SelectedItem as AnimeDefinitionData;
            adata.EaData.AnimeID = adef?.Id ?? -1;

            //透明度
            adata.EaData.Alpha = Convert.ToSingle(this.valueScrollControlAlpha.ValueFixedPoint);

            //フレーム
            adata.EaData.StartFrame = Convert.ToInt32(this.numericUpDownStartFrame.Value);
            adata.EaData.EndFrame = Convert.ToInt32(this.numericUpDownEndFrame.Value);


            //再生速度
            adata.EaData.SpeedRate = this.valueScrollControlSpeedRate.ValueFixedPoint;

            //ループ可否
            adata.EaData.LoopFlag = this.checkBoxLoopFlag.Checked;

            //フレームオフセット
            adata.EaData.FrameOffset = Convert.ToInt32(this.numericUpDownFrameOffset.Value);

            //位置
            adata.EaData.Pos2D.X  = Convert.ToInt32(this.numericUpDownPosX.Value);
            adata.EaData.Pos2D.Y = Convert.ToInt32(this.numericUpDownPosY.Value);

            //拡縮率
            adata.EaData.ScaleRate = Convert.ToSingle(this.valueScrollControlScaleRate.ValueFixedPoint);

            //Flip
            adata.EaData.FlipType = (EFlipType)this.comboBoxFlipState.SelectedItem;
        }

        /// <summary>
        /// アニメが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxAnimeDefinition_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnimeElement adata = EmotionProject.Mana.SelectLayerData;

            this.labelAnimeDefinitionFrame.Text = "--";
            if (adata.SelectAnime != null)
            {
                this.labelAnimeDefinitionFrame.Text = adata.SelectAnime.ImageDataList.Count.ToString();
            }

        }

        /// <summary>
        /// 値が変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownFrame_ValueChanged(object sender, EventArgs e)
        {
            AnimeElement adata = EmotionProject.Mana.SelectLayerData;

            this.labelFrameSpan.Text = "--";
            if (adata.SelectAnime != null)
            {
                this.labelFrameSpan.Text = (adata.EndFrame - adata.StartFrame).ToString();
            }
        }

        private void LayerSettingControl_Load(object sender, EventArgs e)
        {

        }
    }
}
