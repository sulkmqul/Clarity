using Clarity.Collider;
using Clarity.Engine.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vortice.Mathematics;

namespace Clarity.Engine.Core
{
    /// <summary>
    /// 当たり判定情報描画所作
    /// </summary>
    internal class RenderColliderBehavior : BaseRenderColliderBehavior
    {
        public RenderColliderBehavior()
        {
            this.RenderObj = new ClarityObject(0);
            this.RenderObj.ShaderID = ClarityEngine.BuildInShaderIndex.TextureUseAlpha;
            

            //初期情報の取得
            {
                Vector3 defcol = ClarityEngine.EngineSetting.GetVec3("Debug.Collider.DefaultColor");
                this.DefaultColor = new Color4(defcol, 1.0f);

                Vector3 ccol = ClarityEngine.EngineSetting.GetVec3("Debug.Collider.ContactColor");
                this.ContanctColor = new Color4(ccol, 1.0f);
            }

            //処理の作成
            this.ProcDic = new Dictionary<EColMode, Action<BaseCollider>>();
            this.ProcDic.Add(EColMode.Dot, this.RenderDot);
            this.ProcDic.Add(EColMode.Circle, this.RenderCircle);
            this.ProcDic.Add(EColMode.Line, this.RenderLine);
        }

        /// <summary>
        /// 描画物
        /// </summary>
        private ClarityObject RenderObj;
        /// <summary>
        /// 表示対象切り替え辞書
        /// </summary>
        private Dictionary<EColMode, Action<BaseCollider>> ProcDic;

        /// <summary>
        /// 通常色
        /// </summary>
        private Color4 DefaultColor;
        /// <summary>
        /// 当たり色
        /// </summary>
        private Color4 ContanctColor;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 表示処理の実行
        /// </summary>
        /// <param name="obj"></param>
        protected override void ExecuteBehavior(BaseCollider obj)
        {   
            this.RenderObj.Color = this.DefaultColor;
            if (obj.HitTempFlag == true)
            {
                this.RenderObj.Color = this.ContanctColor;
            }
            this.ProcDic[obj.ColMode].Invoke(obj);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 当たり判定円表示
        /// </summary>
        /// <param name="col"></param>
        private void RenderCircle(BaseCollider bc)
        {
            ColliderCircle? col = bc as ColliderCircle;
            if (col == null)
            {
                return;
            }

            ClarityObject data = this.RenderObj;

            data.VertexID = ClarityEngine.BuildInPolygonModelIndex.Rect;
            data.TextureID = ClarityEngine.BuildInTextureIndex.CollisionCircle;

            data.TransSet.Pos3D = col.Center;
            float dia = col.Radius * 2.0f;
            data.TransSet.Scale3D = new Vector3(dia, dia, 1.0f);

            


            data.Render(0, 0);

        }

        /// <summary>
        /// 当たり判定点表示
        /// </summary>
        /// <param name="bc"></param>
        private void RenderDot(BaseCollider bc)
        {
            ColliderDot? col = bc as ColliderDot;
            if (col == null)
            {
                return;
            }

            ClarityObject data = this.RenderObj;

            data.VertexID = ClarityEngine.BuildInPolygonModelIndex.Rect;
            data.TextureID = ClarityEngine.BuildInTextureIndex.CollisionCircle;

            data.TransSet.Pos3D = col.Center;
            float dia = 10f;
            data.TransSet.Scale3D = new Vector3(dia, dia, 1.0f);




            data.Render(0, 0);
        }

        /// <summary>
        /// 当たり判定表示線
        /// </summary>
        /// <param name="bc"></param>
        private void RenderLine(BaseCollider bc)
        {
            ColliderLine? col = bc as ColliderLine;
            if (col == null)
            {
                return;
            }

            ClarityObject data = this.RenderObj;

            data.VertexID = ClarityEngine.BuildInPolygonModelIndex.Line;
            data.TextureID = ClarityEngine.BuildInTextureIndex.CollisionRect;

            data.TransSet.Pos3D = col.StartPos;
            data.TransSet.Rot = col.ParentRot;
            data.TransSet.Scale3D = new Vector3(5.0f, col.Dir.Length(), 1.0f);




            data.Render(0, 0);
        }
    }
}
