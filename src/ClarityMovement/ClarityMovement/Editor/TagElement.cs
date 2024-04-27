using Clarity.GUI;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Editor
{

    class BaseTagUiElement : BaseUiElement
    {
        public BaseTagUiElement(BaseEditTag tag, EUiElementType ut) : base(ut)
        {
            this.Data = tag;
        }
                

        public BaseEditTag Data { get; init; }

        protected RectangleF TagArea = new RectangleF();

        public override RectangleF Area => this.TagArea;
    }

    /// <summary>
    /// タグ全体
    /// </summary>
    internal class TagUiElement : BaseTagUiElement
    {
        public TagUiElement(BaseEditTag data) : base(data, EUiElementType.Tag)
        {
            
        }

        PointF StartPoint;
        PointF EndPoint;

        /// <summary>
        /// 自身のエリアの計算
        /// </summary>
        /// <param name="infolist"></param>
        public override void ReCalcuArea(List<FrameRenderingInfo> infolist)
        {
            int tindex = this.Data.Index;

            //開始と終了のフレームを取得
            var sframe = infolist[this.Data.StartFrame];
            var eframe = infolist[this.Data.EndFrame];

            float h = sframe.TagHeight;
            float hh = sframe.TagHeight * 0.5f;

            PointF stpos = sframe.TagPointList[tindex];
            PointF edpos = eframe.TagPointList[tindex];

            this.StartPoint = stpos;
            this.EndPoint = edpos;

            float tw = edpos.X - stpos.X;
            float th = edpos.Y - stpos.Y;

            this.TagArea = new RectangleF(stpos.X - hh, stpos.Y - hh, tw + hh, h);

        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="gra"></param>
        public override void Render(MvProject proj, Graphics gra)
        {

            using (Pen pe = new Pen(MvGlobal.Setting.Editor.TagColor, 2.0f))
            {
                gra.DrawLine(pe, this.StartPoint, this.EndPoint);
            }
        }

        public override void Grab(MouseInfo minfo, FrameRenderingInfo info)
        {
            minfo.SetMemory(this.Data.StartFrame, 0);
            minfo.SetMemory(this.Data.EndFrame, 1);

            this.SelectedFlag = true;
        }

        public override void GrabMove(MouseInfo minfo, FrameRenderingInfo sframe, FrameRenderingInfo nframe)
        {
            int ms = minfo.GetMemory<int>(0);
            int es = minfo.GetMemory<int>(1);


            int sa = nframe.FrameIndex - sframe.FrameIndex;
            this.Data.StartFrame = ms + sa;
            this.Data.EndFrame = es + sa;


            this.Data.StartFrame = Math.Max(this.Data.StartFrame, 0);
            this.Data.StartFrame = Math.Min(this.Data.StartFrame, this.Data.EndFrame);

            this.Data.EndFrame = Math.Max(this.Data.EndFrame, this.Data.StartFrame);
            this.Data.EndFrame = Math.Min(this.Data.EndFrame, MvGlobal.Project.BaseImageList.Count - 1);
        }

        
    }

    /// <summary>
    /// タグ開始位置の処理
    /// </summary>
    internal class TagStartElement : BaseTagUiElement
    {
        public TagStartElement(BaseEditTag data) : base(data, EUiElementType.TagStart) 
        {
            
        }


        /// <summary>
        /// 自身のエリアの計算
        /// </summary>
        /// <param name="infolist"></param>
        public override void ReCalcuArea(List<FrameRenderingInfo> infolist)
        {
            int tindex = this.Data.Index;

            //開始と終了のフレームを取得
            var sframe = infolist[this.Data.StartFrame];
            
            float h = 10;
            float hh = h * 0.5f;

            PointF stpos = sframe.TagPointList[tindex];
            
            this.TagArea = new RectangleF(stpos.X - hh, stpos.Y - hh, h, h);

        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="gra"></param>
        public override void Render(MvProject proj, Graphics gra)
        {

            using (SolidBrush bru = new SolidBrush(MvGlobal.Setting.Editor.TagColor))
            {
                gra.FillEllipse(bru, this.TagArea);
            }
        }

        /// <summary>
        /// 選択
        /// </summary>
        /// <param name="minfo"></param>
        /// <param name="info"></param>
        public override void Grab(MouseInfo minfo, FrameRenderingInfo info)
        {
            minfo.SetMemory(this.Data.StartFrame, 0);
            minfo.SetMemory(this.Data.EndFrame, 1);

            this.SelectedFlag = true;
        }

        /// <summary>
        /// 選択移動
        /// </summary>
        /// <param name="minfo"></param>
        /// <param name="sframe"></param>
        /// <param name="nframe"></param>
        public override void GrabMove(MouseInfo minfo, FrameRenderingInfo sframe, FrameRenderingInfo nframe)
        {
            int ms = minfo.GetMemory<int>(0);
            int es = minfo.GetMemory<int>(1);


            int sa = nframe.FrameIndex - sframe.FrameIndex;
            this.Data.StartFrame = ms + sa;


            this.Data.StartFrame = Math.Max(this.Data.StartFrame, 0);
            this.Data.StartFrame = Math.Min(this.Data.StartFrame, this.Data.EndFrame);


        }
    }

    /// <summary>
    /// タグ終了位置の処理
    /// </summary>
    internal class TagEndElement : BaseTagUiElement
    {
        public TagEndElement(BaseEditTag data) : base(data, EUiElementType.TagStart)
        {

        }

        /// <summary>
        /// 自身のエリアの計算
        /// </summary>
        /// <param name="infolist"></param>
        public override void ReCalcuArea(List<FrameRenderingInfo> infolist)
        {
            int tindex = this.Data.Index;

            //終了のフレームを取得
            var eframe = infolist[this.Data.EndFrame];

            float h = 10;
            float hh = h * 0.5f;

            PointF stpos = eframe.TagPointList[tindex];

            this.TagArea = new RectangleF(stpos.X - hh, stpos.Y - hh, h, h);

        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="proj"></param>
        /// <param name="gra"></param>
        public override void Render(MvProject proj, Graphics gra)
        {

            using (SolidBrush bru = new SolidBrush(MvGlobal.Setting.Editor.TagColor))
            {
                gra.FillEllipse(bru, this.TagArea);
            }
        }

        /// <summary>
        /// 選択
        /// </summary>
        /// <param name="minfo"></param>
        /// <param name="info"></param>
        public override void Grab(MouseInfo minfo, FrameRenderingInfo info)
        {
            minfo.SetMemory(this.Data.StartFrame, 0);
            minfo.SetMemory(this.Data.EndFrame, 1);

            this.SelectedFlag = true;
        }

        /// <summary>
        /// 選択移動
        /// </summary>
        /// <param name="minfo"></param>
        /// <param name="sframe"></param>
        /// <param name="nframe"></param>
        public override void GrabMove(MouseInfo minfo, FrameRenderingInfo sframe, FrameRenderingInfo nframe)
        {
            int ms = minfo.GetMemory<int>(0);
            int es = minfo.GetMemory<int>(1);


            int sa = nframe.FrameIndex - sframe.FrameIndex;
            this.Data.EndFrame = es + sa;
                        
            this.Data.EndFrame = Math.Max(this.Data.EndFrame, this.Data.StartFrame);
            this.Data.EndFrame = Math.Min(this.Data.EndFrame, MvGlobal.Project.BaseImageList.Count - 1);
        }



    }
}
