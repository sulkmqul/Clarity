using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Clarity;
using Clarity.GUI;
using Clarity.Util;

namespace ClarityEmotion.LayerControl
{

    /// <summary>
    /// レイヤー一枚のコントロール
    /// </summary>
    public partial class LayerEditControl : UserControl
    {
        public LayerEditControl()
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
        public int LayerNo
        {
            get
            {
                return this.AnimeData?.LayerNo ?? -1;
            }
        }

        /// <summary>
        /// これのデータ(これがnullの時は目盛り表示となる)
        /// </summary>
        private AnimeElement? AnimeData
        {
            get
            {
                return this.DispData.LayerData;
            }
            set
            {
                this.DispData.LayerData = value;
            }
        }

        /// <summary>
        /// マウス操作情報
        /// </summary>
        private LayerMouseInfo Minfo = new LayerMouseInfo();

        /// <summary>
        /// マウス処理アクション
        /// </summary>
        private Action? AnimeMouseMoveAction = null;

        /// <summary>
        /// 描画情報
        /// </summary>
        private LayerControlDisplayData DispData = new LayerControlDisplayData();
        /// <summary>
        /// 描画者
        /// </summary>
        private LayerEditControlRenderer Renderer = new LayerEditControlRenderer();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="adata">設定アニメデータ null=目盛り運用</param>
        /// <param name="pixel_rate">拡大率</param>
        /// <param name="max_frame">描画フレーム数</param>
        public void Init(AnimeElement? adata, double pixel_rate, int max_frame)
        {
            this.AnimeData = adata;
            this.SetScale(pixel_rate, max_frame);

            if (adata == null)
            {
                this.labelTitle.Visible = false;
                this.Height -= this.labelTitle.Height;
            }
            else
            {
                this.labelTitle.Text = adata.EaData.Name;
                this.DispData.CalcuAnimeDisplayPos(adata.StartFrame, adata.EndFrame);
            }

            this.DispData.Con = this;


            if (adata != null)
            {
                //値変更イベント
                CeGlobal.Event.ValueChange.Where(x => x.EventID == EEventID.LayerUpdate && ((AnimeElement)x.Data).LayerNo == adata.LayerNo).Subscribe(x =>
                {
                    this.AnimeData = (AnimeElement)x.Data;

                    this.DispData.LayerData = this.AnimeData;
                    this.DispData.CalcuAnimeDisplayPos(this.AnimeData.StartFrame, this.AnimeData.EndFrame);

                    this.Refresh();
                });


                //選択フレームの変更
                CeGlobal.Event.ValueChange.Where(x => x.EventID == EEventID.LayerSelectedChanged).Subscribe(x =>
                {
                    this.Refresh();
                });
            }
            //フレーム設定イベント
            CeGlobal.Event.FrameChange.Subscribe(x =>
            {
                CeGlobal.Project.FramePosition = x.Frame;
                this.Refresh();
            });

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

            this.Width = this.DispData.DisplayPixelRange;

            this.DispData.CalcuAnimeDisplayPos(this.AnimeData?.StartFrame ?? 0, this.AnimeData?.EndFrame ?? 0);
            this.Refresh();
        }

        /// <summary>
        /// 表示範囲の指定
        /// </summary>
        /// <param name="pixel_rate"></param>        
        public void SetScale(double pixel_rate)
        {
            this.DispData.PixelRate = pixel_rate;
            this.DispData.CalcuAnimeDisplayPos(this.AnimeData?.StartFrame ?? 0, this.AnimeData?.EndFrame ?? 0);
            this.Refresh();
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

            int clickrange = LayerEditControl.MouseMoveDetectWidth;

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

        /// <summary>
        /// アニメ開始フレームの変更
        /// </summary>
        private void AnimeMouseMoveActionStart()
        {
            if (this.AnimeData == null)
            {
                return;
            }

            //押した位置と今の位置のフレーム差を計算
            int dpos = this.Minfo.DownPos.X;
            int dframe = this.DispData.PixelXToFrame(dpos);

            int npos = this.Minfo.NowPos.X;
            int nframe = this.DispData.PixelXToFrame(npos);

            int saframe = nframe - dframe;

            //処理
            int stf = this.Minfo.GetMemory<int>(0);
            int psp = this.Minfo.GetMemory<int>(1);
            this.AnimeData.StartFrame = stf + saframe;
            this.AnimeData.FrameSpan = psp - saframe;

            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerUpdate, this.AnimeData);

        }
        /// <summary>
        /// アニメ終了フレームの変更
        /// </summary>
        private void AnimeMouseMoveActionEnd()
        {
            if (this.AnimeData == null)
            {
                return;
            }

            //押した位置と今の位置のフレーム差を計算
            int dpos = this.Minfo.DownPos.X;
            int dframe = this.DispData.PixelXToFrame(dpos);

            int npos = this.Minfo.NowPos.X;
            int nframe = this.DispData.PixelXToFrame(npos);

            int saframe = nframe - dframe;

            //処理            
            int psp = this.Minfo.GetMemory<int>(1);
            this.AnimeData.FrameSpan = psp + saframe;

            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerUpdate, this.AnimeData);
        }

        /// <summary>
        /// アニメを移動させる時
        /// </summary>
        private void AnimeMouseMoveActionMove()
        {
            if (this.AnimeData == null)
            {
                return;
            }

            //押した位置と今の位置のフレーム差を計算
            int dpos = this.Minfo.DownPos.X;
            int dframe = this.DispData.PixelXToFrame(dpos);

            int npos = this.Minfo.NowPos.X;
            int nframe = this.DispData.PixelXToFrame(npos);

            int saframe = nframe - dframe;

            //処理
            int stf = this.Minfo.GetMemory<int>(0);            
            this.AnimeData.StartFrame = stf + saframe;
            
            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerUpdate, this.AnimeData);
        }

        /// <summary>
        /// レイヤー選択通知
        /// </summary>
        private void SelectedLayer()
        {
            if (this.LayerNo < 0 || this.AnimeData == null)
            {
                return;
            }
                        
            CeGlobal.Project.Info.SelectLayerNo = this.LayerNo;
            CeGlobal.Event.SendValueChangeEvent(EEventID.LayerSelectedChanged, this.AnimeData);
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
            if (this.AnimeData == null)
            {
                return;
            }

            //開始フレームと幅の保存
            this.Minfo.SetMemory(this.AnimeData.StartFrame, 0);
            this.Minfo.SetMemory(this.AnimeData.FrameSpan, 1);

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

            //何もないなら位置選択
            int m = this.Minfo.DownLength.X + this.Minfo.DownLength.Y;
            if (m <= 0 && this.DispData.MouseType == EMouseMoveType.None)
            {
                int fpos = this.DispData.PixelXToFrame(this.Minfo.NowPos.X);
                CeGlobal.Event.SendFrameSelectEvent(fpos);
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

            this.Renderer.ClearColor = (CeGlobal.Project.Info.SelectLayerNo == this.LayerNo) ? Color.White : Color.LightGray;
            this.Renderer.ClearColor = (this.AnimeData == null) ? Color.LightBlue : this.Renderer.ClearColor;            
            this.Renderer.RenderControl(gra, this.pictureBoxFrame, this.DispData, CeGlobal.Project.FramePosition);
        }

        /// <summary>
        /// マウスが範囲外へ出た時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelFrame_MouseLeave(object sender, EventArgs e)
        {
            this.DispData.MouseType = EMouseMoveType.None;               

        }
    }
}
