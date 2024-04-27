using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Editor
{
    enum EUiElementType
    {
        FrameImage,
        Tag,
        TagStart,
        TagEnd,

    }


    /// <summary>
    /// UI編集者基底
    /// </summary>    
    class BaseUiElement
    {
        public BaseUiElement(EUiElementType et)
        {
            this.Type = et;
            this.ID = MvGlobal.GetUiElementID();

            //選択が変更された、自身でないなら選択フラグを下す
            SelectChangedSubject.Subscribe(x =>
            {
                if(this.ID != x.ID)
                {
                    this._SelectedFlag = false;
                }
            });

        }

        /// <summary>
        /// 識別ID
        /// </summary>
        public ulong ID { get; private init; }

        /// <summary>
        /// タイプ
        /// </summary>
        public EUiElementType Type { get; init; }



        private static Subject<BaseUiElement> SelectChangedSubject = new Subject<BaseUiElement>();
        private bool _SelectedFlag = false;
        /// <summary>
        /// 選択可否
        /// </summary>
        public bool SelectedFlag
        {
            get
            {
                return this._SelectedFlag;
            }
            set
            {
                this._SelectedFlag = value;
                SelectChangedSubject.OnNext(this);
            }
        }

        /// <summary>
        /// 自身の領域
        /// </summary>
        public virtual RectangleF Area { get; }



        /// <summary>
        /// マウスの判定
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public virtual bool ColMouse(PointF pos)
        {
            return this.Area.Contains(pos);
        }


        /// <summary>
        /// 自身の描画領域の再計算
        /// </summary>
        /// <param name="infolist"></param>
        public virtual void ReCalcuArea(List<FrameRenderingInfo> infolist)
        {

        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="gra"></param>
        public virtual void Render(MvProject proj, Graphics gra)
        {

        }
                        

        /// <summary>
        /// マウスオーバー処理
        /// </summary>
        public virtual void MouseOver(MouseInfo minfo)
        {
            
        }

        /// <summary>
        /// マウスで選択した時
        /// </summary>
        /// <param name="minfo"></param>
        public virtual void Grab(MouseInfo minfo, FrameRenderingInfo info)
        {
            this.SelectedFlag = true;
        }
        /// <summary>
        /// マウスで掴んで動かした時
        /// </summary>
        /// <param name="minfo"></param>
        public virtual void GrabMove(MouseInfo minfo, FrameRenderingInfo sframe, FrameRenderingInfo nframe)
        {

        }


    }


    /// <summary>
    /// 画像領域選択情報
    /// </summary>
    internal class FrameImageUiElement : BaseUiElement
    {
        public FrameImageUiElement(FrameRenderingInfo data) : base(EUiElementType.FrameImage)
        {
            this.Data = data;
        }

        public FrameRenderingInfo Data { get; init; }

        /// <summary>
        /// 自身の範囲
        /// </summary>
        public override RectangleF Area
        {
            get
            {
                return this.Data.BaseImageArea;
            }
        }


        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="gra"></param>
        public override void Render(MvProject proj, Graphics gra)
        {
            if (this.Data.RenderEnabled == false)
            {
                return;
            }

            int fi = this.Data.FrameIndex;
            var image = proj.BaseImageList[fi];

            gra.DrawImage(image, this.Area);


            if(this.SelectedFlag == true)
            {
                using (Pen pe = new Pen(Color.Red, 2.0f))
                {
                    gra.DrawRectangle(pe, this.Area.X, this.Area.Y, this.Area.Width, this.Area.Height);
                }
            }
        }

    }
}
