using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element.Collider
{
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
        /// 当たり判定コールバック
        /// </summary>
        /// <param name="obj">衝突対象</param>
        void ColliderCallback(ICollider obj);

    }
}
