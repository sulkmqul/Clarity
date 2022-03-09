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
using ClarityEmotion.Core;

namespace ClarityEmotion.EditView
{
    /// <summary>
    /// アニメ編集コントロール
    /// </summary>
    public partial class AnimeEditViewControl : UserControl
    {
        public AnimeEditViewControl()
        {
            InitializeComponent();

            this.pictureBoxControl.Parent = this.pictureBoxView;
        }

        /// <summary>
        /// マウス管理
        /// </summary>
        private MouseInfo MInfo = new MouseInfo();

        /// <summary>
        /// 描画管理
        /// </summary>
        private EmotionGenerator EGene = new EmotionGenerator(true);

        /// <summary>
        /// 現在のフレーム
        /// </summary>
        private int DispFrame
        {
            get
            {
                return EmotionProject.Mana.FramePosition;
            }
        }

        /// <summary>
        /// 現在の表示一式
        /// </summary>
        private List<AnimeElement> CurrentList = new List<AnimeElement>();

        /// <summary>
        /// 描画更新
        /// </summary>
        private Clarity.Util.ClarityCycling UpdateCycle = null;

        /// <summary>
        /// 拡縮率
        /// </summary>
        private double ScaleRate = 1.0;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 画面の初期化
        /// </summary>
        public void Init()
        {

            this.UpdateCycle = new Clarity.Util.ClarityCycling();
            this.UpdateCycle.StartCycling(() =>
            {
                this.Refresh();
            }, 50);

            //マウスホイールの追加
            this.pictureBoxControl.MouseWheel += pictureBoxControl_MouseWheel;
        }
                     
        



        /// <summary>
        /// 新しいサイズの設定
        /// </summary>
        public void ResetView()
        {
            Size nsize = new Size(EmotionProject.Mana.BasicInfo.ImageWidth, EmotionProject.Mana.BasicInfo.ImageHeight);
            this.ResetImageSize(nsize);
        }

        /// <summary>
        /// キャンバスサイズの設定
        /// </summary>
        /// <param name="nsize"></param>
        public void ResetImageSize(Size nsize)
        {
            this.EGene.ImageSize = nsize;
            this.SetDispSize(nsize);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 表示サイズの設定
        /// </summary>
        /// <param name="dsize"></param>
        private void SetDispSize(Size dsize)
        {
            this.Width = dsize.Width;
            this.Height = dsize.Height;

            this.EGene.DispSize = dsize;
        }


        /// <summary>
        /// 選択候補の取得
        /// </summary>
        /// <returns></returns>
        private List<AnimeElement> CreateSelectList()
        {
            List<AnimeElement> anslist = new List<AnimeElement>();

            //選択を最優先
            AnimeElement sel = this.CurrentList.Where(x => x.LayerNo == EmotionProject.Mana.SelectLayerData.LayerNo).FirstOrDefault();
            if (sel != null)
            {
                anslist.Add(sel);
            }

            //残りはlayer降順
            var aa = this.CurrentList.OrderByDescending(x => x.LayerNo).ToList();
            anslist.AddRange(aa);

            return anslist;
            
        }

        /// <summary>
        /// マウス位置から対象を取得する
        /// </summary>
        /// <param name="pos">位置</param>
        /// <returns></returns>
        private AnimeElement DetectMousePos(Point pos)
        {
            List<AnimeElement> srclist = this.CreateSelectList();

            AnimeElement ans = srclist.Where(x => x.CheckDisplayPoint(pos)).FirstOrDefault();
            return ans;

        }

        /// <summary>
        /// 拡縮率の初期化
        /// </summary>
        private void ResetScale()
        {
            this.ScaleRate = 1.0;
            this.SetDispSize(this.EGene.ImageSize);
        }

        /// <summary>
        /// 拡縮率変更
        /// </summary>
        /// <param name="f">true=拡大 false=縮小</param>
        private void ChangeScaleImage(bool f)
        {
            if (f == true)
            {
                this.ScaleRate += 0.1;
            }
            else
            {
                this.ScaleRate -= 0.1;
                if (this.ScaleRate < 0.1)
                {
                    this.ScaleRate = 0.1;
                }
            }


            double ndw = this.EGene.ImageSize.Width * this.ScaleRate;
            double ndh = this.EGene.ImageSize.Height * this.ScaleRate;

            this.SetDispSize(new Size((int)ndw, (int)ndh));
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.Focus();
            this.MInfo.DownMouse(e);

            //選択の取得
            AnimeElement ae = this.DetectMousePos(this.MInfo.NowPos);
            if (ae == null)
            {
                return;
            }

            EmotionProject.Mana.Info.SelectLayerNo = ae.LayerNo;
            this.MInfo.SetMemory(ae.EaData.Pos2D.X, 0);
            this.MInfo.SetMemory(ae.EaData.Pos2D.Y, 1);
        }


        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxControl_MouseMove(object sender, MouseEventArgs e)
        {
            this.MInfo.MoveMouse(e);

            if (EmotionProject.Mana.SelectLayerData == null)
            {
                return;
            }

            if (this.MInfo.DownFlag == true)
            {
                Point p = this.EGene.DispToImage(this.MInfo.DownLength);

                int px = this.MInfo.GetMemory<int>(0);
                int py = this.MInfo.GetMemory<int>(1);

                EmotionProject.Mana.SelectLayerData.EaData.Pos2D.X = px + p.X;
                EmotionProject.Mana.SelectLayerData.EaData.Pos2D.Y = py + p.Y;

                return;
            }

            //選択の取得
            EmotionProject.Mana.Anime.LayerList.ForEach(x => x.TempData.MouseOverFlag = false);
            AnimeElement ae = this.DetectMousePos(this.MInfo.NowPos);
            if (ae != null)
            {
                ae.TempData.MouseOverFlag = true;
            }

        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.MInfo.UpMouse(e);
        }

        /// <summary>
        /// マウスがホイールされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                //拡大
                this.ChangeScaleImage(true);
            }
            else
            {
                //縮小
                this.ChangeScaleImage(false);
            }
        }


        /// <summary>
        /// 描画されるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxControl_Paint(object sender, PaintEventArgs e)
        {
            if (EmotionProject.Mana == null)
            {
                return;
            }

            e.Graphics.Clear(EmotionProject.Mana.Option.EditViewClearColor);

            //表示サイズ設定
            this.EGene.DispSize = this.pictureBoxView.Size;

            //描画
            List<AnimeElement> elist = EmotionProject.Mana.Anime.LayerList;
            this.EGene.EditFlag = !EmotionProject.Mana.Info.PlayFlag;
            this.CurrentList = this.EGene.Render(e.Graphics, elist, this.DispFrame);
            
        }
        /// <summary>
        /// ダブルクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxControl_DoubleClick(object sender, EventArgs e)
        {
            this.ResetScale();
        }
    }
}
