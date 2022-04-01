using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.GUI;
using Clarity.Util;

namespace ClarityEmotion.LayerControl
{

    
    


    /// <summary>
    /// レイヤー一枚のコントロール
    /// </summary>
    public partial class LayerControl : UserControl
    {
        public LayerControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        

        class LayerMouseInfo : MouseInfo
        {
            public EMouseMoveType DownType = EMouseMoveType.None;
        }

        /// <summary>
        /// 端のマウス感応幅
        /// </summary>
        internal const int MouseMoveDetectWidth = 10;

        
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// このコントロールの管理レイヤー番号
        /// </summary>
        private int LayerNo = -1;

        /// <summary>
        /// これ管理データ
        /// </summary>
        private AnimeElement AData
        {
            get
            {
                if (this.LayerNo < 0)
                {
                    return null;
                }

                return EmotionProject.Mana.Anime.LayerList[this.LayerNo];
            }
        }
        /// <summary>
        /// マウス操作情報
        /// </summary>
        LayerMouseInfo Minfo = new LayerMouseInfo();

        /// <summary>
        /// マウス処理アクション
        /// </summary>
        Action AnimeMouseMoveAction = null;

        /// <summary>
        /// 描画情報
        /// </summary>
        LayerControlDisplayData DispData = new LayerControlDisplayData();
        /// <summary>
        /// 描画者
        /// </summary>
        LayerControlRenderer Renderer = new LayerControlRenderer();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="layerno"></param>
        public void Init(int layerno, double pixel_rate, int max_frame)
        {
            this.LayerNo = layerno;
            this.SetScale(pixel_rate, max_frame);

            this.DispData.Con = this;
        }

        /// <summary>
        /// 定期更新関数
        /// </summary>
        public void CylcleUpdate()
        {
            this.Width = this.DispData.DisplayPixelRange;

            //アニメ位置再計算
            if (this.AData != null)
            {
                this.DispData.LayerData = this.AData;                
                this.DispData.CalcuAnimeDisplayPos(this.AData.StartFrame, this.AData.EndFrame);
            }

            //再描画
            this.Refresh();
        }

        /// <summary>
        /// 表示範囲の指定
        /// </summary>
        /// <param name="pixel_rate"></param>
        /// <param name="max_frame"></param>
        public void SetScale(double pixel_rate, int max_frame)
        {
            this.DispData.PixelRate = pixel_rate;
            this.DispData.MaxFrame = max_frame;
        }

        /// <summary>
        /// 表示範囲の指定
        /// </summary>
        /// <param name="pixel_rate"></param>        
        public void SetScale(double pixel_rate)
        {
            this.DispData.PixelRate = pixel_rate;
        }
               
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        
        
        /// <summary>
        /// マウス位置から処理の割り出し
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        private EMouseMoveType CalcuMoveType(Point pos)
        {
            if (this.DispData.LayerData == null)
            {
                return EMouseMoveType.None;
            }

            int clickrange = LayerControl.MouseMoveDetectWidth;

            int px = pos.X;
            int stpos = this.DispData.AnimeStartPos;
            int edpos = this.DispData.AnimeEndPos;

            //有効でない
            if (this.DispData.LayerData.EaData.Enabled == false)
            {
                return EMouseMoveType.None;
            }
            //範囲外
            if (px < stpos)
            {
                return EMouseMoveType.None;
            }
            if (px >= edpos)
            {
                return EMouseMoveType.None;
            }

            //開始地点
            if (px < (stpos + clickrange))
            {
                return EMouseMoveType.Start;
            }

            if ((edpos - clickrange) < px)
            {
                return EMouseMoveType.End;
            }

            return EMouseMoveType.Move;
        }

        /// <summary>
        /// マウスモードに応じたカーソルの変更
        /// </summary>
        /// <param name="mt"></param>
        private void ChangeAnimePanelCursor(EMouseMoveType mt)
        {
            if (mt == EMouseMoveType.Start || mt == EMouseMoveType.End)
            {
                this.pictureBoxFrame.Cursor = Cursors.SizeWE;
                return;
            }
            if (mt == EMouseMoveType.Move)
            {
                this.pictureBoxFrame.Cursor = Cursors.SizeAll;
                return;
            }

            this.pictureBoxFrame.Cursor = Cursors.Default;
        }
        /// <summary>
        /// マウス動作の確定
        /// </summary>
        /// <param name="mt"></param>
        private void SelectAnimeMouseMoveAction(EMouseMoveType mt)
        {
            switch (mt)
            {
                case EMouseMoveType.Start:
                    this.AnimeMouseMoveAction = this.AnimeMouseMoveActionStart;
                    break;
                case EMouseMoveType.End:
                    this.AnimeMouseMoveAction = this.AnimeMouseMoveActionEnd;
                    break;
                case EMouseMoveType.Move:
                    this.AnimeMouseMoveAction = this.AnimeMouseMoveActionMove;
                    break;
                default:
                    this.AnimeMouseMoveAction = null;
                    break;

            }
        }

        private void AnimeMouseMoveActionStart()
        {
            //押した位置と今の位置のフレーム差を計算
            int dpos = this.Minfo.DownPos.X;
            int dframe = this.DispData.PixelXToFrame(dpos);

            int npos = this.Minfo.NowPos.X;
            int nframe = this.DispData.PixelXToFrame(npos);

            int saframe = nframe - dframe;

            //処理
            int stf = this.Minfo.GetMemory<int>(0);            
            this.AData.StartFrame = stf + saframe;            

        }
        private void AnimeMouseMoveActionEnd()
        {
            //押した位置と今の位置のフレーム差を計算
            int dpos = this.Minfo.DownPos.X;
            int dframe = this.DispData.PixelXToFrame(dpos);

            int npos = this.Minfo.NowPos.X;
            int nframe = this.DispData.PixelXToFrame(npos);

            int saframe = nframe - dframe;

            //処理            
            int edf = this.Minfo.GetMemory<int>(1);            
            this.AData.EndFrame = edf + saframe;
        }

        /// <summary>
        /// アニメ動かす時
        /// </summary>
        private void AnimeMouseMoveActionMove()
        {
            //押した位置と今の位置のフレーム差を計算
            int dpos = this.Minfo.DownPos.X;
            int dframe = this.DispData.PixelXToFrame(dpos);

            int npos = this.Minfo.NowPos.X;
            int nframe = this.DispData.PixelXToFrame(npos);

            int saframe = nframe - dframe;

            //処理
            int stf = this.Minfo.GetMemory<int>(0);
            int edf = this.Minfo.GetMemory<int>(1);
            this.AData.StartFrame = stf + saframe;
            this.AData.EndFrame = edf + saframe;


        }

        /// <summary>
        /// レイヤー選択通知
        /// </summary>
        private void SelectedLayer()
        {
            if (this.LayerNo < 0)
            {
                return;
            }

            EmotionProject.Mana.Info.SelectLayerNo = this.LayerNo;
            this.Focus();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//        
        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelFrame_MouseDown(object sender, MouseEventArgs e)
        {
            this.SelectedLayer();

            this.Minfo.DownMouse(e);

            //位置を判定
            this.DispData.MouseType = this.CalcuMoveType(e.Location);
            if (this.DispData.MouseType == EMouseMoveType.None)
            {
                return;
            }
                        
            this.Minfo.SetMemory(this.AData.StartFrame, 0);
            this.Minfo.SetMemory(this.AData.EndFrame, 1);

            //アクションの設定
            this.SelectAnimeMouseMoveAction(this.DispData.MouseType);
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelFrame_MouseMove(object sender, MouseEventArgs e)
        {
            //位置を判定
            this.DispData.MouseType = this.CalcuMoveType(e.Location);                        

            this.Minfo.MoveMouse(e);
            if (this.Minfo.DownFlag == false)
            {
                //カーソル変更
                this.ChangeAnimePanelCursor(this.DispData.MouseType);
                return;
            }
            //処理の実行
            this.AnimeMouseMoveAction?.Invoke();

        }
        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelFrame_MouseUp(object sender, MouseEventArgs e)
        {
            this.Minfo.UpMouse(e);

            //位置を判定
            this.DispData.MouseType = this.CalcuMoveType(e.Location);

            int m = this.Minfo.DownLength.X + this.Minfo.DownLength.Y;
            if (m <= 0 && this.DispData.MouseType == EMouseMoveType.None)
            {
                int fpos = this.DispData.PixelXToFrame(this.Minfo.NowPos.X);
                EmotionProject.Mana.FramePosition = fpos;
            }
        }

        /// <summary>
        /// 背景が描画されるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelFrame_Paint(object sender, PaintEventArgs e)
        {
            Graphics gra = e.Graphics;

            this.Renderer.ClearColor = (EmotionProject.Mana.Info.SelectLayerNo == this.LayerNo) ? Color.White : Color.LightGray;
            this.Renderer.ClearColor = (this.AData == null) ? Color.LightBlue : this.Renderer.ClearColor;
            this.Renderer.RenderControl(gra, this.pictureBoxFrame, this.DispData, EmotionProject.Mana.FramePosition);
        }


        private void panelFrame_MouseLeave(object sender, EventArgs e)
        {
            this.DispData.MouseType = EMouseMoveType.None;               

        }
    }
}
