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
        public FrameModifierPaintData? TempSelect = null;

        /// <summary>
        /// 描画用データ一式
        /// </summary>
        public List<FrameModifierPaintData> PaintDataList = new List<FrameModifierPaintData>();
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
        /// 選択対象(あるなら)
        /// </summary>
        //public BaseFrameModifier? SelectedData = null;
    }


    public enum EChangeWork
    {
        Left,
        Right,
        Position,


        None
    }


    /// <summary>
    /// tag描画用データ
    /// </summary>
    internal class FrameModifierPaintData : IDisposable
    {
        public FrameModifierPaintData(BaseFrameModifier src)
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
        /// 自身の左端移動の領域(fixed areaの一部で端の判定領域)
        /// </summary>
        public Rectangle LeftArea { get; set; } = new Rectangle();

        /// <summary>
        /// 自身の右端移動の領域(fixed areaの一部で端の判定領域)
        /// </summary>
        public Rectangle RightArea { get; set; } = new Rectangle();
        /// <summary>
        /// 選択可否
        /// </summary>
        public bool SelectedFlag { get; set; } = false;

        /// <summary>
        /// マウス動作の場所
        /// </summary>
        public EChangeWork ChangeWork { get; set; } = EChangeWork.None;

        /// <summary>
        /// 現在のマウスオーバー可否
        /// </summary>
        public bool MouseOverFlag { get; set; } = false;


        private CompositeDisposable RxDist = new CompositeDisposable();
        #endregion


        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="con"></param>
        /// <param name="paint"></param>
        public void Init(FrameEditControl con, FrameEditorPainter paint)
        {   

        }

        /// <summary>
        /// 情報のアップデート
        /// </summary>
        /// <param name="mpos">マウス位置</param>
        /// <param name="down">マウス押す処理＝selectするか？</param>
        /// <returns>自身にヒットした</returns>
        public bool UpdateMouse(Point mpos, bool down)
        {
            this.ChangeWork = EChangeWork.None;

            //エリアに入っている？
            bool ans = this.FixedArea.Contains(mpos);
            this.MouseOverFlag = ans;
            if (ans == true)
            {
                this.ChangeWork = EChangeWork.Position;
            }

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

            //全体に入っているなら選択とする。
            if (down == true)
            {
                this.SelectedFlag = (ans == true && down == true);
            }

            return ans;
        }

        /// <summary>
        /// 自身の領域の再計算
        /// </summary>
        /// <param name="paint"></param>
        public void CalcuArea(FrameEditorPainter paint)
        {
            //画像タグ
            if (this.TagType == ETagType.Image)
            {
                this.FixedArea = paint.FrameToImageArea(this.Frame, this.FrameSpan);
            }
            //汎用
            if (this.TagType == ETagType.Tag)
            {
                FrameTagModifier mod = (FrameTagModifier)this.SrcData;
                int tid = mod.TagId;
                this.FixedArea = paint.FrameToTagArea(this.Frame, tid);
            }

            //端の領域の計算
            int sa = 10;
            this.LeftArea = new Rectangle(this.FixedArea.X, this.FixedArea.Top, sa, this.FixedArea.Height);
            this.RightArea = new Rectangle(this.FixedArea.Right - sa, this.FixedArea.Top, sa, this.FixedArea.Height);


        }

        


        /// <summary>
        /// 自身の描画
        /// </summary>
        /// <param name="gra"></param>
        public void Paint(Graphics gra)
        {
            //自身の領域に矩形を出す
            using (Pen pe = new Pen(Color.Red, 2))
            {
                gra.DrawRectangle(pe, this.FixedArea);
                gra.DrawRectangle(pe, this.LeftArea);
                gra.DrawRectangle(pe, this.RightArea);

            }
            if (this.MouseOverFlag == true)
            {
            }
            if (this.SelectedFlag == true)
            {
                
            }
        }


        /// <summary>
        /// 削除
        /// </summary>
        public void Dispose()
        {
            this.RxDist.Dispose();
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


        
    }
}
