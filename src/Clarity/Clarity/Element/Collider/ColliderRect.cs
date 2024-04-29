using Clarity.Collider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Collider
{
    /// <summary>
    /// 当たり判定矩形
    /// </summary>
    internal class ColliderRect : BaseCollider
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        
        public ColliderRect(Vector2 pos, Vector2 size) : base(EColMode.Rect)
        {
            this.Pos = pos;
            this.Size = size;
        }

        /// <summary>
        /// 位置
        /// </summary>
        Vector2 Pos;

        /// <summary>
        /// サイズ
        /// </summary>
        Vector2 Size;

        public override object Clone()
        {
            ColliderRect ans = new ColliderRect(new Vector2(this.Pos.X, this.Pos.X), new Vector2(this.Size.X, this.Size.Y));
            ans.ColiderTransposeMode = this.ColiderTransposeMode;
            return ans;
        }

        protected override void TranslateCollider(TransposeSet tset)
        {
            throw new NotImplementedException();
        }

        protected override void ScalingCollider(TransposeSet tset)
        {
            throw new NotImplementedException();
        }

        protected override void RotationCollider(TransposeSet tset)
        {
            throw new NotImplementedException();
        }
    }
}
