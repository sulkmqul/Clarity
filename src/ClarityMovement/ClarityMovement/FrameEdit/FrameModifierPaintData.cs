using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Disposables;

namespace ClarityMovement.FrameEdit
{
    /// <summary>
    /// 編集描画データまとめ
    /// </summary>
    internal class EditorData
    {
        /// <summary>
        /// カーソル情報
        /// </summary>
        public FrameEditorSelection? MouseCursor = null;

        /// <summary>
        /// テンプレート選択データ
        /// </summary>
        public BaseFrameModifierPaintData? TempSelect = null;

        /// <summary>
        /// 描画用データ一式
        /// </summary>
        public List<BaseFrameModifierPaintData> PaintDataList = new List<BaseFrameModifierPaintData>();
    }

    /// <summary>
    /// 選択情報
    /// </summary>
    internal class FrameEditorSelection
    {
        /// <summary>
        /// 選択フレーム番号
        /// </summary>
        public int FrameNo = 0;

        /// <summary>
        /// エリア種別
        /// </summary>
        public ETagType Area = ETagType.None;

        /// <summary>
        /// フレーム矩形情報
        /// </summary>
        public Rectangle SelectionFrameRect = new Rectangle();

        /// <summary>
        /// タグindex
        /// </summary>
        public int TagIndex = -1;

        /// <summary>
        /// 選択対象(あるなら)
        /// </summary>
        //public BaseFrameModifier? SelectedData = null;
    }


    /// <summary>
    /// 編集作業のデータ
    /// </summary>
    public enum EChangeWork
    {
        Left,
        Right,
        Top,
        Bottom,
        Position,


        None
    }


    /// <summary>
    /// tag描画用データ基底
    /// </summary>
    internal abstract class BaseFrameModifierPaintData
    {
        public BaseFrameModifierPaintData(BaseFrameModifier src)
        {
            this.SrcData = src;
        }

        #region メンバ変数      
        /// <summary>
        /// 元ネタ
        /// </summary>
        public BaseFrameModifier SrcData { get; init; }

        /// <summary>
        /// 自身のフレーム
        /// </summary>
        public int Frame
        {
            get
            {
                return this.SrcData.Frame;
            }
            set
            {
                this.SrcData.Frame = value;
            }
        }

        /// <summary>
        /// 自身の長さ
        /// </summary>
        public int FrameSpan
        {
            get
            {
                return this.SrcData.FrameSpan;
            }
            set
            {
                this.SrcData.FrameSpan = value;
            }
        }


        /// <summary>
        /// タグ情報
        /// </summary>
        public ETagType TagType
        {
            get
            {
                return this.SrcData.TagType;
            }
        }

        /// <summary>
        /// 自身の現在のエリア（判定領域と描画領域）
        /// </summary>
        public Rectangle FixedArea { get; set; } = new Rectangle();
                
        /// <summary>
        /// 選択可否
        /// </summary>
        public bool SelectedFlag { get; set; } = false;

        /// <summary>
        /// マウス動作のデータ
        /// </summary>
        public EChangeWork ChangeWork { get; set; } = EChangeWork.None;

        /// <summary>
        /// 現在のマウスオーバー可否
        /// </summary>
        public bool MouseOverFlag { get; set; } = false;


        public Font ParentFont;
        #endregion


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="con"></param>
        /// <param name="paint"></param>
        public virtual void Init(FrameEditControl con, FrameEditorPainter paint)
        {
            this.ParentFont = con.Font;
        }

        /// <summary>
        /// 自身の領域の再計算
        /// </summary>
        /// <param name="paint"></param>
        public abstract void CalcuArea(FrameEditorPainter paint);

        /// <summary>
        /// 自身の描画
        /// </summary>
        /// <param name="gra"></param>
        public virtual void Paint(Graphics gra)
        {
            //自身の領域を描画
            using (Pen pe = new Pen(Color.Black, 1.0f))
            {
                gra.DrawRectangle(pe, this.FixedArea);
            }

            if (this.SelectedFlag == true)
            {
                //選択時は強調して囲う
                using (Pen pe = new Pen(Color.Red, 2.0f))
                {
                    gra.DrawRectangle(pe, this.FixedArea);
                }
            }
        }

        /// <summary>
        /// マウスの判定
        /// </summary>
        /// <param name="mpos">マウス位置</param>
        /// <returns>自身にヒットした</returns>
        public virtual bool DetectMouse(Point mpos)
        {
            this.ChangeWork = EChangeWork.None;

            //エリアに入っている？
            bool ans = this.FixedArea.Contains(mpos);
            this.MouseOverFlag = ans;
            if (ans == true)
            {
                this.ChangeWork = EChangeWork.Position;
            }

            return ans;
        }

        /// <summary>
        /// ドラッグ処理
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        public virtual void DragWork(FrameEditorSelection prev, FrameEditorSelection next)
        {
            int n = prev.FrameNo - next.FrameNo;
            
            switch (this.ChangeWork)
            {
                case EChangeWork.Position:
                    this.Frame = next.FrameNo;
                    break;
                case EChangeWork.Left:
                    this.Frame = next.FrameNo;
                    this.FrameSpan += n;
                    break;
                case EChangeWork.Right:
                    this.FrameSpan -= n;                    
                    break;
            }

            System.Diagnostics.Trace.WriteLine($"Drag:{this.ChangeWork}:{n}");



        }
        
    }


    /// <summary>
    /// 画像タグ描画クラス
    /// </summary>
    internal class FrameModifierPaintDataImage : BaseFrameModifierPaintData
    {
        public FrameModifierPaintDataImage(FrameImageModifier data) : base(data)
        {
        }

        /// <summary>
        /// 自身の左端移動の領域(fixed areaの一部で端の判定領域)
        /// </summary>
        public Rectangle LeftArea { get; set; } = new Rectangle();

        /// <summary>
        /// 自身の右端移動の領域(fixed areaの一部で端の判定領域)
        /// </summary>
        public Rectangle RightArea { get; set; } = new Rectangle();


        /// <summary>
        /// 真ん中のエリアの計算
        /// </summary>
        public Rectangle CenterArea
        {
            get
            {
                int x = this.LeftArea.Right;
                int y = this.LeftArea.Top;
                int w = this.FixedArea.Width - this.LeftArea.Width - this.RightArea.Width;
                int h = this.FixedArea.Height;
                

                Rectangle ans = new Rectangle(x, y, w, h);
                return ans;
            }
        }

        /// <summary>
        /// データの取得
        /// </summary>
        public FrameImageModifier ImageTag
        {
            get
            {
                return (FrameImageModifier)this.SrcData;
            }
        }

        /// <summary>
        /// データエリアの計算
        /// </summary>
        /// <param name="paint"></param>
        public override void CalcuArea(FrameEditorPainter paint)
        {
            //エリア計算
            this.FixedArea = paint.FrameToImageArea(this.Frame, this.FrameSpan);

            //端の領域の計算
            int sa = 10;
            this.LeftArea = new Rectangle(this.FixedArea.X, this.FixedArea.Top, sa, this.FixedArea.Height);
            this.RightArea = new Rectangle(this.FixedArea.Right - sa, this.FixedArea.Top, sa, this.FixedArea.Height);
        }

        /// <summary>
        /// 左右のサイズ変更を付け加える
        /// </summary>
        /// <param name="mpos"></param>
        /// <returns></returns>
        public override bool DetectMouse(Point mpos)
        {
            bool ans = base.DetectMouse(mpos);

            //左
            bool f = this.LeftArea.Contains(mpos);
            if (f == true)
            {
                this.ChangeWork = EChangeWork.Left;
            }
            //右
            f = this.RightArea.Contains(mpos);
            if (f == true)
            {
                this.ChangeWork = EChangeWork.Right;
            }

            return ans;
        }

        /// <summary>
        /// データの描画
        /// </summary>
        /// <param name="gra"></param>
        public override void Paint(Graphics gra)
        {
            //全体の領域描画
            using (SolidBrush sb = new SolidBrush(Color.LightPink))
            {
                gra.FillRectangle(sb, this.FixedArea);
            }

            //左右の端描画
            using (SolidBrush sb = new SolidBrush(Color.HotPink))
            {
                gra.FillRectangle(sb, this.LeftArea);
                gra.FillRectangle(sb, this.RightArea);
            }

            //設定画像の描画
            int id = this.ImageTag.ImageDataID;
            var data = CmGlobal.Project.Value?.ImageDataMana.GetImage(id);
            if (data != null)
            {
                //書き出し領域が大きいなら画像もつける
                var carea = this.CenterArea;
                if (carea.Width > 50)
                {
                    var imgrect = this.CalcuImageStretchArea(data.Image, carea);
                    //gra.DrawImage(data.Image, carea);
                    gra.DrawImage(data.Image, imgrect);
                }


                //識別名の書き出し
                Font fo = this.ParentFont;
                //gra.MeasureString(data.ImageDataName, fo);
                using (Brush pe = new SolidBrush(Color.Black))
                {
                    gra.DrawString(data.ImageDataName, fo, pe, this.FixedArea);
                }
            }
            

            base.Paint(gra);
        }


        /// <summary>
        /// 対象画像が比を保ったままピッタリおさまるサイズを計算する
        /// </summary>
        /// <param name="bit">画像</param>
        /// <param name="rect">この中に収める</param>
        /// <returns></returns>
        private Rectangle CalcuImageStretchArea(Bitmap bit, Rectangle rect)
        {
            //double wrate = (double)bit.Width / (double)rect.Width;
            //double hrate = (double)bit.Height / (double)rect.Height;
            double wrate = (double)rect.Width / (double)bit.Width;
            double hrate = (double)rect.Height / (double)bit.Height;

            double rate = Math.Min(wrate, hrate);

            double nw = (double)bit.Width * rate;
            double nh = (double)bit.Height * rate;

            Rectangle ans = new Rectangle(rect.X, rect.Y, (int)nw, (int)nh);
            return ans;

        }
    }


    /// <summary>
    /// Tag描画エリア
    /// </summary>
    internal class FrameModifierPaintDataTag : BaseFrameModifierPaintData
    {
        public FrameModifierPaintDataTag(FrameTagModifier data) : base(data)
        {
        }

        /// <summary>
        /// データの取得
        /// </summary>
        public FrameTagModifier TagData
        {
            get
            {
                return (FrameTagModifier)this.SrcData;
            }
        }

        /// <summary>
        /// 自身のエリアの計算
        /// </summary>
        /// <param name="paint"></param>
        public override void CalcuArea(FrameEditorPainter paint)
        {
            int tid = this.TagData.TagId;
            this.FixedArea = paint.FrameToTagArea(this.Frame, tid);
        }

        

        /// <summary>
        /// データの描画
        /// </summary>
        /// <param name="gra"></param>
        public override void Paint(Graphics gra)
        {
            using (SolidBrush sb = new SolidBrush(Color.LightGreen))
            {
                gra.FillRectangle(sb, this.FixedArea);
            }

            base.Paint(gra);
        }
    }
}
