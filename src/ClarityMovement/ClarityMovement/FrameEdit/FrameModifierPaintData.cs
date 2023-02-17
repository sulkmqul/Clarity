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

            //ドラッグ

        }


        /// <summary>
        /// 自身の描画
        /// </summary>
        /// <param name="gra"></param>
        public void Paint(Graphics gra)
        {
            if (this.MouseOverFlag == true)
            {
            }
            if (this.SelectedFlag == true)
            {
                //自身の領域に矩形を出す
                using (Pen pe = new Pen(Color.Red, 2))
                {
                    gra.DrawRectangle(pe, this.FixedArea);
                }
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
