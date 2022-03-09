using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Element.Collider
{
    /// <summary>
    /// 当たり判定　点
    /// </summary>
    public class ColliderDot : BaseCollider
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c">位置</param>        
        public ColliderDot(Vector3 c) : base(EColMode.Dot)
        {
            this.Center = c;
            
        }


        /// <summary>
        /// 点位置
        /// </summary>
        public Vector3 Center = new Vector3();



        

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
            ColliderDot ans = new ColliderDot(new Vector3(this.Center.X, this.Center.Y, this.Center.Z));
            ans.ColiderTransposeMode = this.ColiderTransposeMode;
            return ans;
        }

    }
}
