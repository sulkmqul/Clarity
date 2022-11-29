using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clarity;
using System.Threading.Tasks;
using Clarity.Engine;
using Clarity.Engine.Element;
using System.Drawing;
using Vortice.Mathematics;
using System.Numerics;
using Clarity.Engine.Element.Behavior;
using Clarity.Collider;

namespace ClarityOrbit.EditView
{
    /// <summary>
    /// Gird描画Behavior
    /// </summary>
    class GirdFrameRenderBehavior : BaseRenderBehavior
    {
        //protected override void RenderSetShaderData(ClarityObject obj)
        //{
        //    OrbitGridFrame? frame = obj as OrbitGridFrame;
        //    if (frame == null)
        //    {
        //        return;
        //    }

        //    FrameGridShaderData data = new FrameGridShaderData();
        //    data.WorldViewProj = obj.TransSet.CreateTransposeMat();
        //    data.Color = obj.Color;
        //    data.BorderWidth = 0.1f;

        //    //選択色の変更
        //    if (OrbitEditViewControl.TempInfo.SelectTileIndex.Equals(frame.Pos))
        //    {
        //        data.Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
        //    }

        //    ClarityEngine.SetShaderData<FrameGridShaderData>((int)EShaderCode.FrameGrid, data);

        //}

        protected override void RenderSetVertex(ClarityObject obj)
        {
            using (PrimitiveTopologyLineState st = new PrimitiveTopologyLineState())
            {
                base.RenderSetVertex(obj);
            }
        }
    }


    /// <summary>
    /// グリッドフレームの管理と描画、ついでに操作の本体
    /// </summary>
    internal class OrbitGridFrame : BaseTileObject
    {
        public OrbitGridFrame(Point tpos) : base()
        {
            this.Pos = tpos;

            var pro = new TipInfoControlBehavior();
            pro.Execute(this);
        }


        /// <summary>
        /// 初期化関数
        /// </summary>
        protected override void InitElement()
        {
            //this.VertexID = ClarityEngine.BuildInPolygonModelIndex.Rect;
            this.SetVertexCode(EVertexCode.VGrid);
            this.Color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);


            this.SetShaderCode(EShaderCode.System_NoTexture);
            //this.SetShaderCode(EShaderCode.FrameGrid);
            this.RenderBehavior = new GirdFrameRenderBehavior();

            //タイル共通所作
            //this.AddProcBehavior(new TipInfoControlBehavior());
        }


        
    }


}
