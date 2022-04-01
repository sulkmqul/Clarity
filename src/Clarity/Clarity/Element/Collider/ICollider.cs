using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Collider
{
    /// <summary>
    /// 当たり判定処理所作 これを継承して作成する
    /// </summary>
    public class ColliderBehavior
    {
        /// <summary>
        /// 判定コールバック処理
        /// </summary>
        /// <param name="obj">当たった自分</param>
        /// <param name="opptant">処理対象</param>
        public virtual void ProcColliderAction(Clarity.Element.Collider.ICollider obj, Clarity.Element.Collider.ICollider opptant)
        {
        }
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
        ColliderBehavior ColliderBehavior { get; set; }

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
