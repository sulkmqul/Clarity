using Clarity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityEmotion.LayerControl
{
    class LayerControlSet
    {
        public Label LabelLayer = null;
        public LayerControl Layer = null;
    }

    class LayerAnimeControlData
    {
        /// <summary>
        /// 管理コントロール一式
        /// </summary>
        public List<LayerControlSet> ControlList = new List<LayerControlSet>();

        /// <summary>
        /// 拡縮率テーブル
        /// </summary>
        public double[] FramePixelRateTable;
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
    }
    class LayerAnimeControlLogic
    {
        public LayerAnimeControlLogic(LayerAnimeControl con, LayerAnimeControlData data)
        {
            this.Con = con;
            this.FData = data;
        }

        LayerAnimeControl Con = null;
        LayerAnimeControlData FData = null;

        /// <summary>
        ///　更新サイクル
        /// </summary>
        ClarityCycling UpdateCycle = new ClarityCycling();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="pane"></param>
        public void Init(Panel pane)
        {
            this.CreateScaleTable();

            this.CreateLayerControl(pane);


            //更新サイクル開始
            this.UpdateCycle.StartCycling(() =>
            {
                this.Con.toolStripLabelDispFrame.Text = string.Format("{0,5}", EmotionProject.Mana.FramePosition);

                //コントロール更新
                this.FData.ControlList.ForEach(x => x.Layer.CylcleUpdate());
            });
        }

        


        /// <summary>
        /// スケールの変更
        /// </summary>
        /// <param name="f">true=拡大</param>
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

            //通知
            this.FData.ControlList.ForEach(x =>
            {
                x.Layer.SetScale(this.FData.FramePixelRate);
            });
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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

                if (now > 100)
                {
                    break;
                }
            }

            this.FData.FramePixelRateTable = ratelist.ToArray();

        }

        /// <summary>
        /// レイヤーパネルの初期化、プロジェクトに従ってコントロールを作成する
        /// </summary>
        /// <param name="pane">格納パネル</param>        
        private void CreateLayerControl(Panel pane)
        {

            try
            {
                this.Con.SuspendLayout();

                //既存のコントロールをクリア
                pane.Controls.Clear();
                this.FData.ControlList = new List<LayerControlSet>();

                EmotionProjectDataAnime adata = EmotionProject.Mana.Anime;
                var pinfo = EmotionProject.Mana.Info;

                int margin = 5;
                int toppos = margin;

                //アニメコントロールをADD
                adata.LayerList.ForEach(ae =>
                {
                    Label la = new Label();
                    {
                        la.AutoSize = false;
                        la.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                        la.Text = ae.CreateLayerName();
                        la.Tag = ae;
                        la.BorderStyle = BorderStyle.FixedSingle;
                        la.Left = margin;
                        la.Top = toppos;
                        la.Width = 100;
                        la.Height = 30;
                    }

                    LayerControl con = new LayerControl();
                    {
                        con.Top = toppos;
                        con.BorderStyle = BorderStyle.FixedSingle;
                        con.Left = la.Right + margin;
                        con.Init(ae.EaData.LayerNo, this.FData.FramePixelRate, EmotionProject.Mana.BasicInfo.MaxFrame);

                    }

                    pane.Controls.Add(la);
                    pane.Controls.Add(con);

                    toppos += la.Height + margin;

                    //管理へ
                    this.FData.ControlList.Add(new LayerControlSet() { LabelLayer = la, Layer = con });
                });

                //pane.Height = toppos;                

            }
            finally
            {
                this.Con.ResumeLayout();
            }
        }

    }
}
