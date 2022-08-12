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
    internal struct FrameGridShaderData
    {
        public Matrix4x4 WorldViewProj;
        public Vector4 Color;
        public float BorderWidth;

        public float Mergin;
        public float Mergin2;
        public float Mergin3;

    }

    class GirdFrameRenderBehavior : BaseRenderBehavior
    {
        protected override void RenderSetShaderData(ClarityObject obj)
        {
            OrbitGridFrame? frame = obj as OrbitGridFrame;
            if (frame == null)
            {
                return;
            }

            FrameGridShaderData data = new FrameGridShaderData();
            data.WorldViewProj = obj.TransSet.CreateTransposeMat();
            data.Color = obj.Color;
            data.BorderWidth = 0.1f;

            //選択色の変更
            if (OrbitEditViewControl.TempInfo.SelectTileIndex.Equals(frame.Pos))
            {
                data.Color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
            }

            ClarityEngine.SetShaderData<FrameGridShaderData>((int)EShaderCode.FrameGrid, data);

        }

        protected override void RenderSetVertex(ClarityObject obj)
        {
            using (PrimitiveTopologyLineState st = new PrimitiveTopologyLineState())
            {
                base.RenderSetVertex(obj);
            }
        }
    }


    /// <summary>
    /// グリッドフレームの管理と
    /// </summary>
    internal class OrbitGridFrame : BaseTileObject
    {
        public OrbitGridFrame(Point tpos) : base()
        {
            this.Pos = tpos;
        }


        /// <summary>
        /// 初期化関数
        /// </summary>
        protected override void InitElement()
        {
            //this.VertexID = ClarityEngine.BuildInPolygonModelIndex.Rect;
            this.SetVertexCode(EVertexCode.VGrid);
            this.Color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            this.SetShaderCode(EShaderCode.FrameGrid);


            this.RenderBehavior = new GirdFrameRenderBehavior();

            //当たり判定設定を行う
            this.ColInfo = new Clarity.Collider.ColliderInfo(this);
            {
                //頂点の取得               
                List<Vector3> vlist = ClarityEngine.GetVertexList((int)EVertexCode.VGrid);
                //Clarity.Collider.ColliderPolygon cpol = new Clarity.Collider.ColliderPolygon(vlist[0], vlist[1], vlist[2]);
                Clarity.Collider.ColliderPlaneRect cpol = new Clarity.Collider.ColliderPlaneRect(vlist[0], vlist[1], vlist[2], vlist[3]);

                this.ColInfo.ColType = EditViewDefine.GridColType;
                this.ColInfo.TargetColType = EditViewDefine.MouseColType;
                this.ColInfo.SrcColliderList.Add(cpol);
                this.ColliderBehavior = new GridFrameColliderBehavior();
            }


            //タイル共通所作
            this.AddProcBehavior(new TipInfoControlBehavior());
        }
    }


    internal class GridFrameColliderBehavior : ColliderBehavior
    {
        public override void ProcColliderAction(ICollider obj, ICollider opptant)
        {
            //System.Diagnostics.Trace.WriteLine("当たった！！！");

            OrbitGridFrame grid = obj as OrbitGridFrame;

            OrbitEditViewControl.TempInfo.SelectTileIndex = grid.Pos;

            //ClarityEngine.SetSystemText($"あたった({grid?.Pos.X},{grid?.Pos.Y})", 2);
        }
    }
}
