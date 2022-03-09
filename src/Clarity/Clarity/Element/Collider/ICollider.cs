using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Collider
{
    public abstract class BaseColliderBehavior
    {
        /// <summary>
        /// 判定コールバック処理
        /// </summary>
        /// <param name="obj">当たった自分</param>
        /// <param name="opptant">処理対象</param>
        public abstract void ProcColliderAction(Clarity.Element.Collider.ICollider obj, Clarity.Element.Collider.ICollider opptant);
    }

    /// <summary>
    /// 衝突判定提供インターフェースインターフェース
    /// </summary>
    public interface ICollider
    {
        /// <summary>
        /// 当たり判定情報
        /// </summary>
        ColliderInfo ColInfo
        {
            get; set;
        }

        /// <summary>
        /// 当たり判定実行所作
        /// </summary>
        BaseColliderBehavior ColliderBehavior { get; set; }

        /// <summary>
        /// 当たり判定コールバック
        /// </summary>
        /// <param name="obj">衝突対象</param>
        void ColliderCallback(ICollider obj)
        {
            this.ColliderBehavior?.ProcColliderAction(this, obj);
        }

    }
}
