using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Resources;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Util;

namespace ClarityEmotion.LayerControl
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

        ///// <summary>
        ///// これ管理データ
        ///// </summary>
        //private AnimeElement AData
        //{
        //    get
        //    {
        //        return CeGlobal.Project.SelectLayerData;
        //    }
        //}

        private AnimeElement? SelectedData = null;


        /// <summary>
        /// 現在表示中(trueの場合、データ取得を無効化する)
        /// </summary>
        private bool SetControlFlag = false;

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Init()
        {   
            this.InitFlipComboBox();

            //ここで変更イベントを仕掛ける
            //アニメが変更された
            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.AnimeDefinitionUpdate) == EEventID.AnimeDefinitionUpdate).Subscribe(x =>
            {
                //コントロールの初期化
                this.InitAnimeDefinitionComboBox();
            });

            //レイヤー情報の更新
            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.LayerUpdate) == EEventID.LayerUpdate).Subscribe(x =>
            {
                AnimeElement? ae = x.Data as AnimeElement;
                if (ae == null)
                {
                    return;
                }

                this.SelectedData = ae;                

                this.SetControlFlag = true;
                //再描画
                this.DisplayLayerData(ae);

                this.SetControlFlag = false;
            });

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

            foreach (AnimeDefinitionData adata in CeGlobal.Project.Anime.AnimeDefinitionDic.Values)
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
        /// <param name="elemdata">表示データ</param>
        private void DisplayLayerData(AnimeElement elemdata)
        {
            try
            {
                EmotionAnimeData adata = elemdata.EaData;

                //名前
                this.textBoxLayerName.Text = adata.Name;

                //有効可否
                this.checkBoxEnabled.Checked = adata.Enabled;
                                
                //アニメ定義
                this.comboBoxAnimeDefinition.SelectedItem = elemdata.SelectAnime;

                //透明度
                this.valueScrollControlAlpha.ValueFixedPoint = adata.Alpha;

                //フレーム
                this.numericUpDownStartFrame.Value = Convert.ToDecimal(adata.StartFrame);
                this.numericUpDownSpan.Value = Convert.ToDecimal(adata.FrameSpan);

                //再生速度
                this.valueScrollControlSpeedRate.ValueFixedPoint = adata.SpeedRate;

                //フレームオフセット
                this.numericUpDownFrameOffset.Value = adata.FrameOffset;

                //ループ可否
                this.checkBoxLoopFlag.Checked = adata.LoopFlag;

                //位置
                this.numericUpDownPosX.Value = Convert.ToDecimal(adata.Pos2D.X);
                this.numericUpDownPosY.Value = Convert.ToDecimal(adata.Pos2D.Y);

                //表示サイズ
                this.numericUpDownDispSizeX.Value = Convert.ToDecimal(adata.DispSize.Width);
                this.numericUpDownDispSizeY.Value = Convert.ToDecimal(adata.DispSize.Height);

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
            AnimeElement? adata = this.SelectedData;
            if (adata == null)
            {
                return;
            }

            //レイヤー名
            adata.EaData.Name = this.textBoxLayerName.Text;

            //有効可否
            adata.EaData.Enabled = this.checkBoxEnabled.Checked;

            //アニメ定義
            AnimeDefinitionData? adef = this.comboBoxAnimeDefinition.SelectedItem as AnimeDefinitionData;
            adata.EaData.AnimeID = adef?.Id ?? -1;

            //透明度
            adata.EaData.Alpha = Convert.ToSingle(this.valueScrollControlAlpha.ValueFixedPoint);

            //フレーム
            adata.EaData.StartFrame = Convert.ToInt32(this.numericUpDownStartFrame.Value);
            adata.EaData.FrameSpan = Convert.ToInt32(this.numericUpDownSpan.Value);


            //再生速度
            adata.EaData.SpeedRate = this.valueScrollControlSpeedRate.ValueFixedPoint;

            //ループ可否
            adata.EaData.LoopFlag = this.checkBoxLoopFlag.Checked;

            //フレームオフセット
            adata.EaData.FrameOffset = Convert.ToInt32(this.numericUpDownFrameOffset.Value);

            //位置
            adata.EaData.Pos2D.X  = Convert.ToInt32(this.numericUpDownPosX.Value);
            adata.EaData.Pos2D.Y = Convert.ToInt32(this.numericUpDownPosY.Value);

            //サイズ            
            int sw = Convert.ToInt32(this.numericUpDownDispSizeX.Value);
            int sh = Convert.ToInt32(this.numericUpDownDispSizeY.Value);
            adata.EaData.DispSize = new Size(sw, sh);

            //Flip
            adata.EaData.FlipType = (EFlipType)this.comboBoxFlipState.SelectedItem;
        }


        /// <summary>
        /// 値変更検知
        /// </summary>
        private void ChangeValueProcess()
        {
            if (this.SelectedData == null)
            {
                return;
            }
            if (this.SetControlFlag == true)
            {
                return;
            }

            //入力の取得
            this.CollectInputData();

            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerUpdate, this.SelectedData);

        }

        //---------------------------------------------------------------------------------------------------
        //以下は値変更検知
        /// <summary>
        /// アニメが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comboBoxAnimeDefinition_SelectedIndexChanged(object sender, EventArgs e)
        {            
            AnimeElement adata = CeGlobal.Project?.SelectLayerData;
            if (adata == null)
            {
                return;
            }

            this.ChangeValueProcess();

            //選択アニメの取得
            this.labelAnimeDefinitionFrame.Text = "--";
            if (adata.SelectAnime == null)
            {
                return;
            }
            this.labelAnimeDefinitionFrame.Text = adata.SelectAnime.ImageDataList.Count.ToString();
            

            


            //デフォルトサイズを入れておく            
            var bsize = await Task.Run(() =>
            {
                try
                {
                    Bitmap bit = new Bitmap(adata.SelectAnime.ImageDataList.First().FilePath);
                    return bit.Size;
                }
                catch (Exception ex)
                {
                    return new Size(0, 0);
                }                
            });
            this.numericUpDownDispSizeX.Value = bsize.Width;
            this.numericUpDownDispSizeY.Value = bsize.Height;

            this.numericUpDownSpan.Value = adata.SelectAnime.ImageDataList.Count;

        }

        private void textBoxLayerName_TextChanged(object sender, EventArgs e)
        {
            this.ChangeValueProcess();
        }

        private void numericUpDownChanged(object sender, EventArgs e)
        {
            this.ChangeValueProcess();
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeValueProcess();
        }

        private void comboBoxFlipState_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ChangeValueProcess();
        }

        private void valueScrollControl_ValueChanged(object sender, EventArgs e)
        {
            this.ChangeValueProcess();
        }

        /// <summary>
        /// 再生速度が変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void valueScrollControlSpeedRate_ValueChanged(object sender, EventArgs e)
        {
            if (this.SelectedData == null)
            {
                return;
            }

            double baseframe = this.SelectedData.SelectAnime?.ImageDataList.Count ?? 0.0;
            int newspan = Convert.ToInt32(baseframe * this.valueScrollControlSpeedRate.ValueFixedPoint);
            this.numericUpDownSpan.Value = newspan;

        }
    }
}
