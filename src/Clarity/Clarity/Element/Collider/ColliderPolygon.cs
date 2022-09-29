using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Collider
{
    /// <summary>
    /// ポリゴンの判定
    /// </summary>
    public class ColliderPolygon : BaseCollider
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="v1">頂点1</param>
        /// <param name="v2">頂点2</param>
        /// <param name="v3">頂点3</param>
        public ColliderPolygon(Vector3 v1, Vector3 v2, Vector3 v3) : this(new List<Vector3>() { v1, v2, v3 })
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vlist">頂点3つ</param>
        public ColliderPolygon(List<Vector3> vlist) : base(EColMode.Polygon)
        {
            if (vlist.Count < 3)
            {
                throw new Exception("ColliderPolygon is Set 3 vertexes");
            }
                        
            this.VertexList.AddRange(vlist);
        }

        /// <summary>
        /// 頂点一式
        /// </summary>
        public List<Vector3> VertexList = new List<Vector3>(3);

        /// <summary>
        /// 辺A
        /// </summary>
        public Vector3 SideLineA
        {
            get
            {
                return this.VertexList[1] - this.VertexList[0];
            }
        }

        /// <summary>
        /// 辺B
        /// </summary>
        public Vector3 SideLineB
        {
            get
            {
                return this.VertexList[2] - this.VertexList[0];
            }
        }

        /// <summary>
        /// 法線の計算
        /// </summary>
        /// <returns></returns>
        public Vector3 CalcuNormal()
        {
            Vector3 vc = Vector3.Cross(this.SideLineA, this.SideLineB);
            //return vc;
            return Vector3.Normalize(vc);
            

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 移動変形
        /// </summary>
        /// <param name="tset"></param>
        protected override void TranslateCollider(TransposeSet tset)
        {
            for (int i = 0; i < this.VertexList.Count; i++)
            {
                this.VertexList[i] += tset.Pos3D;
            }
        }

        /// <summary>
        /// 回転変形
        /// </summary>
        /// <param name="tset"></param>
        protected override void RotationCollider(TransposeSet tset)
        {            
            for (int i = 0; i < this.VertexList.Count; i++)
            {
                Matrix4x4 rmat = tset.CreateTransposeRotationMat();
                Vector3 rpos = Vector3.Transform(this.VertexList[i], rmat);
                this.VertexList[i] = rpos;
            }
        }

        /// <summary>
        /// 拡大変形
        /// </summary>
        /// <param name="tset"></param>
        /// <exception cref="NotImplementedException"></exception>
        protected override void ScalingCollider(TransposeSet tset)
        {
            for (int i = 0; i < this.VertexList.Count; i++)
            {
                this.VertexList[i] *= tset.ScaleRate;

                Matrix4x4 sm = Matrix4x4.CreateScale(tset.Scale3D);
                this.VertexList[i] = Vector3.Transform(this.VertexList[i], sm);
            }
        }

        /// <summary>
        /// データコピー
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override object Clone()
        {
            ColliderPolygon ans = new ColliderPolygon(this.VertexList);
            ans.ColiderTransposeMode = this.ColiderTransposeMode;
            return ans;
        }
    }



    /// <summary>
    /// 平面の判定
    /// </summary>
    /// <remarks>Polygonが二枚と判断する</remarks>
    public class ColliderPlaneRect : BaseCollider
    {
        public ColliderPlaneRect(Vector3 lt, Vector3 rt, Vector3 lb, Vector3 rb) : base(EColMode.PlaneRect)
        {
            this.Poly.Add(new ColliderPolygon(lt, rt, rb));
            this.Poly.Add(new ColliderPolygon(lt, rb, lb));
        }
        private ColliderPlaneRect() : base(EColMode.PlaneRect)
        {
        }

        public List<ColliderPolygon> Poly = new List<ColliderPolygon>(8);

        protected override void TranslateCollider(TransposeSet tset)
        {
            this.Poly.ForEach(x =>
            {
                x.ColiderTransposeMode = this.ColiderTransposeMode;
                x.TransformCollider(tset);                
            });
        }

        protected override void RotationCollider(TransposeSet tset)
        {
            this.Poly.ForEach(x =>
            {
                x.ColiderTransposeMode = this.ColiderTransposeMode;
                //x.TransformCollider(tset);
            });
        }

        protected override void ScalingCollider(TransposeSet tset)
        {
            this.Poly.ForEach(x =>
            {
                x.ColiderTransposeMode = this.ColiderTransposeMode;
                //x.TransformCollider(tset);
            });
        }

        public override object Clone()
        {
            ColliderPlaneRect ans = new ColliderPlaneRect();            
            ans.ColiderTransposeMode = this.ColiderTransposeMode;
            this.Poly.ForEach(x =>
            {
                ans.Poly.Add((ColliderPolygon)x.Clone());
            });

            return ans;
        }
    }
}
