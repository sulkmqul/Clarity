using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Numerics;
using Clarity.Engine;
using Clarity.GUI;
using Clarity.Engine.Element;
using Clarity.Collider;
using System.Drawing;

namespace ClarityOrbit.EditView
{
    
    internal class OrbitColType
    {
        public const int MouseColType = 1 << 0;
        public const int GridColType = 1 << 2;
    }


    /// <summary>
    /// マウス管理エレメント
    /// </summary>
    internal class MouseManageElement : ClarityObject
    {
        public MouseManageElement()
        {
        }

        /// <summary>
        /// これのマウス情報
        /// </summary>
        public MouseInfo MInfo = new MouseInfo();

        /// <summary>
        /// 選択エリア描画可否
        /// </summary>
        public bool SelectAreaRenderFlag = false;
        

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            //マウスの処理
            this.TransSet.WorldID = OrbitGlobal.OrbitWorldID;

            //自身の色を規定
            this.Color = new Vortice.Mathematics.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            //描画設定
            this.SetVertexCode(EVertexCode.VGrid);
            this.SetShaderCode(EShaderCode.System_NoTexture);            
            this.RenderBehavior = new GirdFrameRenderBehavior();

            //当たり判定の追加
            this.ColInfo = new Clarity.Collider.ColliderInfo(this);
            this.ColInfo.ColType = OrbitColType.MouseColType;
            this.ColInfo.TargetColType = OrbitColType.GridColType;
            
            Clarity.Collider.ColliderLine line = new Clarity.Collider.ColliderLine(new Vector3(0.0f), new Vector3(0.0f));                        
            this.ColInfo.SrcColliderList.Add(line);

            //当たり判定動作設定
            this.ColliderBehavior = new MouseColBehavior();

            //所作は当たり判定を更新する物体が必要
            //これはscale rateなどの既存変換とは一線を画す処理であるため
            this.AddProcBehavior(new ActionBehavior((x) =>
            {
                //当たり判定位置を修正する
                //マウス位置のworld座標を取得
                Vector3 stpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.NowPos.X, this.MInfo.NowPos.Y, 0.0f);
                Vector3 edpos = ClarityEngine.WindowToWorld(this.TransSet.WorldID, this.MInfo.NowPos.X, this.MInfo.NowPos.Y, 1.0f);                

                //当たり判定を作り直す
                Vector3 dir = edpos - stpos;         
                
                this.ColInfo.SrcColliderList.Clear();
                Clarity.Collider.ColliderLine line = new Clarity.Collider.ColliderLine(stpos, dir);
                line.ColiderTransposeMode = Clarity.Collider.EColiderTransposeMode.None;    //自分でやるので変換不要
                this.ColInfo.SrcColliderList.Add(line);

                //描画を初期化
                this.SelectAreaRenderFlag = false;

                
                
            }));
        }

        

        /// <summary>
        /// 描画設定
        /// </summary>
        protected override void RenderElemenet()
        {
            if (this.SelectAreaRenderFlag == false)
            {
                return;
            }

            //気に食わないが例外的にここで描画位置の設定をする
            {
                if (OrbitGlobal.Project == null)
                {
                    return;
                }

                var rect = OrbitEditViewControl.TempInfo.SelectTileRect;
                Size tsize = OrbitGlobal.Project.BaseInfo.TileSize;

                float px = rect.X * tsize.Width;
                float py = rect.Y * tsize.Height;                
                this.TransSet.Scale2D = new System.Numerics.Vector2(rect.Width * tsize.Width, rect.Height * tsize.Height);

                //Vector2 offset = new Vector2(this.TransSet.ScaleX * 0.5f, this.TransSet.ScaleY * 0.5f);
                Vector2 offset = new Vector2();

                this.TransSet.Pos2D = new System.Numerics.Vector2(px + offset.X, py + offset.Y);

            }

            base.RenderElemenet();
        }

    }

    /// <summary>
    /// エリア判定
    /// </summary>
    internal class MouseColBehavior : ColliderBehavior
    {
        public override void ProcColliderAction(ICollider obj, ICollider opptant)
        {
            MouseManageElement? mouse = obj as MouseManageElement;
            OrbitGridFrame? grid = opptant as OrbitGridFrame;
            if (mouse == null || grid == null)
            {
                return;
            }
            //当たったら描画しても良い
            mouse.SelectAreaRenderFlag = true;
        }
    }
}
