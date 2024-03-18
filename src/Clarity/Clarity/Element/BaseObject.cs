using Clarity.Collider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// 基底オブジェクト 描画や判定があるもの
    /// </summary>
    public class BaseObject : BaseElement, ICollider
    {
        public BaseObject(long oid) : base(oid)
        {

        }


        /// <summary>
        /// 当たり判定情報
        /// </summary>
        public ColliderInfo? ColInfo { get; set; } = null;
        /// <summary>
        /// 当たり判定処理所作
        /// </summary>
        public ColliderBehavior? ColliderBehavior { get; set; } = null;

        /// <summary>
        /// 処理情報
        /// </summary>
        public TransposeSet TransSet { get; private set; } = new TransposeSet();
    }
}
