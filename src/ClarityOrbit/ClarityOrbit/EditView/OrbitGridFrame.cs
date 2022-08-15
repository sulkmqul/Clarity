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

            //当たり判定設定を行う
            this.ColInfo = new Clarity.Collider.ColliderInfo(this);
            {
                

                //頂点の取得               
                List<Vector3> vlist = ClarityEngine.GetVertexList((int)EVertexCode.VGrid);
                //矩形判定
                Clarity.Collider.ColliderPlaneRect cpol = new Clarity.Collider.ColliderPlaneRect(vlist[0], vlist[1], vlist[3], vlist[2]);

                //当たり判定設定
                this.ColInfo.ColType = OrbitColType.GridColType;
                this.ColInfo.TargetColType = OrbitColType.MouseColType;
                this.ColInfo.SrcColliderList.Add(cpol);
                this.ColliderBehavior = new GridFrameColliderBehavior();
            }


            //タイル共通所作
            this.AddProcBehavior(new TipInfoControlBehavior());
        }


        
    }


    /// <summary>
    /// Grid当たり判定(EditView編集処理)
    /// </summary>
    internal class GridFrameColliderBehavior : ColliderBehavior
    {
        public override void ProcColliderAction(ICollider obj, ICollider opptant)
        {
            //データ変換
            MouseManageElement? me = opptant as MouseManageElement;
            OrbitGridFrame? grid = obj as OrbitGridFrame;
            if (grid == null || me== null)
            {
                return;
            }
            //マウス選択位置の更新
            this.UpdateMouseSelectRect(grid);

            //ClarityEngine.SetSystemText($"あたった({grid?.Pos.X},{grid?.Pos.Y})", 2);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// EditViewでの選択エリアの確定
        /// </summary>
        /// <param name="grid"></param>
        private void UpdateMouseSelectRect(OrbitGridFrame grid)
        {
            int w = OrbitGlobal.ControlInfo.SrcSelectedInfo?.SelectedIndexRect.Width ?? 1;
            int h = OrbitGlobal.ControlInfo.SrcSelectedInfo?.SelectedIndexRect.Height ?? 1;
            OrbitEditViewControl.TempInfo.SelectTileRect = new Rectangle(grid.Pos.X, grid.Pos.Y, w, h);
        }


        
    }
}
