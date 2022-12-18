using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion.LayerControl
{
    /// <summary>
    /// レイヤー管理
    /// </summary>
    public partial class LayerControl : UserControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }

        class LayerControlData
        {
            /// <summary>
            /// 管理コントロール一式
            /// </summary>
            public List<LayerEditControl> ControlList = new List<LayerEditControl>();

            /// <summary>
            /// 拡縮率テーブル
            /// </summary>
            public double[] FramePixelRateTable = { 0 };
            /// <summary>
            /// 拡縮率テーブル選択場所
            /// </summary>
            public int FramePixelRateTableIndex = 4;

            /// <summary>
            /// 1フレーム何Pixelで表示するかのレート
            /// </summary>
            public double FramePixelRate
            {
                get
                {
                    return this.FramePixelRateTable[this.FramePixelRateTableIndex];
                }
            }


            public int ControlLeftPos = 10;

        }


        /// <summary>
        /// これのデータ
        /// </summary>
        private LayerControlData FData = new LayerControlData();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// アニメの取得
        /// </summary>
        private EmotionProjectDataAnime Anime
        {
            get
            {
                return CeGlobal.Project.Anime;
            }
        }
        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //拡大率テーブルの作成
            this.CreateScaleTable();            

            //コントロール初期化
            this.InitializeLayerControl();

            this.Enabled = false;

            //プロジェクトが作成された時
            CeGlobal.Event.ValueChange.Where(x => (x.EventID & EEventID.CreateProject) == EEventID.CreateProject).Subscribe(x =>
            {
                this.Enabled = true;

                //再描画
                this.RefreshLayer();

                //レイヤーを一つ追加する
                this.AddNewLayer();
            });

            //フレーム位置が変更された時
            CeGlobal.Event.FrameChange.Subscribe(x =>
            {
                this.toolStripLabelFramePos.Text = x.Frame.ToString();                
            });

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 拡縮率のテーブルを計算
        /// </summary>
        private void CreateScaleTable()
        {
            List<double> ratelist = new List<double>();
            double[] invalvec = { 0.1, 0.25, 0.5, 0.75 };
            ratelist.AddRange(invalvec);

            double now = 1.0;
            while (true)
            {
                ratelist.Add(now);
                now += 0.5;

                if (now > 30)
                {
                    break;
                }
            }

            this.FData.FramePixelRateTable = ratelist.ToArray();


        }

        /// <summary>
        /// LayerEditControlの追加
        /// </summary>
        /// <param name="con"></param>
        private void AddLayerEditControl(LayerEditControl con)
        {
            this.FData.ControlList.Add(con);
            this.panelLayer.Controls.Add(con);
        }

        /// <summary>
        /// LayerEditControlの削除
        /// </summary>
        /// <param name="con"></param>
        private void RemoveLayerEditControl(LayerEditControl con)
        {
            this.FData.ControlList.Remove(con);
            this.panelLayer.Controls.Remove(con);
        }

        /// <summary>
        /// Layer表示部分の初期化
        /// </summary>
        private void InitializeLayerControl()
        {
            using (LayoutChangingState st = new LayoutChangingState(this))
            {
                //目盛りの追加
                LayerEditControl con = new LayerEditControl();
                con.Init(null, 1, 0);
                this.AddLayerEditControl(con);

                //再配置
                this.ReCalcuLayerControlPosition();
            }
        }

        /// <summary>
        /// 既存のコントロールの位置を再計算
        /// </summary>
        /// <remarks>
        /// LayoutChangingStateで囲うことが望ましい
        /// </remarks>
        private void ReCalcuLayerControlPosition()
        {
            int tpos = 0;
            int tmergin = 1;
            this.FData.ControlList.ForEach(x =>
            {
                x.Left = this.FData.ControlLeftPos;
                x.Top = tpos;
                tpos += x.Height + tmergin;
            });

        }


        /// <summary>
        /// レイヤーの追加
        /// </summary>        
        private void AddNewLayer()
        {
            AnimeElement ae = CeGlobal.Project.AddNewLayer();
            LayerEditControl con = new LayerEditControl();
            con.Init(ae, this.FData.FramePixelRate, CeGlobal.Project.BasicInfo.MaxFrame);
            this.AddLayerEditControl(con);

            CeGlobal.Event.SendValueChangeEvent(EEventID.AddLayer, ae);

            //再配置
            using (LayoutChangingState st = new LayoutChangingState(this))
            {
                this.ReCalcuLayerControlPosition();
            }

            //追加したレイヤーを選択
            CeGlobal.Project.Info.SelectLayerNo = ae.LayerNo;
            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerSelectedChanged, ae);

        }


        /// <summary>
        /// レイヤーの削除
        /// </summary>
        private void RemoveLayer()
        {
            //現在の選択レイヤーのコントロールを取得
            var selc = this.FData.ControlList.Where(x => x.LayerNo == CeGlobal.Project.Info.SelectLayerNo && x.LayerNo >= 0).FirstOrDefault();
            if (selc == null)
            {
                return;
            }            
            this.RemoveLayerEditControl(selc);
            CeGlobal.Project.RemoveSelectLayer(selc.LayerNo);
            //削除
            CeGlobal.Event.SendValueChangeEvent(EEventID.RemoveLayer, null);

            //レイヤーの再選択
            CeGlobal.Project.Info.SelectLayerNo = -1;
            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerSelectedChanged, null);

            //再計算
            using (LayoutChangingState st = new LayoutChangingState(this))
            {
                this.ReCalcuLayerControlPosition();
            }

        }

        /// <summary>
        /// スケールの変更
        /// </summary>
        /// <param name="f">true=拡大 false=縮小</param>
        public void ChangeScale(bool f)
        {
            //拡縮率選択異動
            this.FData.FramePixelRateTableIndex = (f == true) ? this.FData.FramePixelRateTableIndex + 1 : this.FData.FramePixelRateTableIndex - 1;
            if (this.FData.FramePixelRateTableIndex <= 0)
            {
                this.FData.FramePixelRateTableIndex = 0;
            }
            if (this.FData.FramePixelRateTable.Length <= this.FData.FramePixelRateTableIndex)
            {
                this.FData.FramePixelRateTableIndex = this.FData.FramePixelRateTable.Length - 1;
            }
            

            //再描画
            this.RefreshLayer();
        }

        /// <summary>
        /// レイヤーの再描画
        /// </summary>
        public void RefreshLayer()
        {
            this.FData.ControlList.ForEach(x =>
            {
                x.SetScale(this.FData.FramePixelRate, CeGlobal.Project.BasicInfo.MaxFrame);
            });
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// レイヤー追加ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerAdd_Click(object sender, EventArgs e)
        {
            this.AddNewLayer();
        }

        /// <summary>
        /// レイヤー削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerRemove_Click(object sender, EventArgs e)
        {
            this.RemoveLayer();
        }

        /// <summary>
        /// 拡大ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonZoomPlus_Click(object sender, EventArgs e)
        {
            this.ChangeScale(true);
        }
        /// <summary>
        /// 縮小ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonZoomMinus_Click(object sender, EventArgs e)
        {
            this.ChangeScale(false);
        }

        private void toolStripButtonPlayStart_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButtonPlayStop_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// フレーム位置のリセット
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonResetFrame_Click(object sender, EventArgs e)
        {
            CeGlobal.Project.FramePosition = 0;
            CeGlobal.Event.SendFrameSelectEvent(0);

        }

        /// <summary>
        /// フレーム位置クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripLabelFramePos_Click(object sender, EventArgs e)
        {
            FramePosSettingForm f = new FramePosSettingForm();
            f.FramePos = CeGlobal.Project.FramePosition;
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }
            CeGlobal.Project.FramePosition = f.FramePos;
            CeGlobal.Event.SendFrameSelectEvent(f.FramePos);
        }
    }
}
