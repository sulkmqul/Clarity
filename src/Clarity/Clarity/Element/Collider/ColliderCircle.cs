using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Element.Collider
{
    /// <summary>
    /// 当たり判定円
    /// </summary>
    public class ColliderCircle : BaseCollider
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c">中心位置</param>
        /// <param name="r">半径</param>
        public ColliderCircle(Vector3 c, float r) : base(EColMode.Circle)
        {
            this.Center = c;
            this.Radius = r;
        }

        /// <summary>
        /// 中心位置
        /// </summary>
        public Vector3 Center = new Vector3();


        /// <summary>
        /// 半径
        /// </summary>
        private float r = 0.0f;
        /// <summary>
        /// 半径の二乗
        /// </summary>
        private float rr = 0.0f;
        /// <summary>
        /// 半径
        /// </summary>
        public float Radius
        {
            get
            {
                return this.r;
            }
            set
            {
                this.r = value;
                this.rr = value * value;
            }
        }

        /// <summary>
        /// 半径の二乗
        /// </summary>
        public float RR
        {
            get
            {
                return this.rr;
            }
        }


        


        /// <summary>
        /// 判定の回転処理
        /// </summary>
        /// <param name="tset">親の遷移情報</param>
        protected override void RotationCollider(TransposeSet tset)
        {
            Matrix4x4 rmat = tset.CreateTransposeRotationMat();
            this.Center = Vector3.Transform(this.Center, rmat);
        }

        /// <summary>
        /// 判定の拡縮処理
        /// </summary>
        /// <param name="tset">親の遷移情報</param>
        protected override void ScalingCollider(TransposeSet tset)
        {
            this.Center *= tset.ScaleRate;
            this.Radius *= tset.ScaleRate;
        }

        /// <summary>
        /// 判定の移動処理
        /// </summary>
        /// <param name="tset">親の遷移情報</param>
        protected override void TranslateCollider(TransposeSet tset)
        {
            this.Center += tset.Pos3D;
        }

        /// <summary>
        /// これのコピーを作成する
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ColliderCircle ans = new ColliderCircle(new Vector3(this.Center.X, this.Center.Y, this.Center.Z), this.Radius);
            ans.ColiderTransposeMode = this.ColiderTransposeMode;
            return ans;
        }

    }
}
