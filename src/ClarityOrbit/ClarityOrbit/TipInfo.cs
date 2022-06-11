using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;
using Clarity.Engine.Element;
using System.Drawing;

namespace ClarityOrbit
{
    /// <summary>
    /// 配置チップ画像一枚
    /// </summary>
    internal class TipInfo : ClarityObject
    {
        public TipInfo(LayerInfo palay, Point pos) : base(0)
        {
            this.ParentLayer = palay;
            this.Pos = pos;
        }

        /// <summary>
        /// 親レイヤー情報
        /// </summary>
        private LayerInfo ParentLayer = null;

        /// <summary>
        /// 自身のチップID
        /// </summary>
        public int TipImageID = OrbitGlobal.EVal;

        /// <summary>
        /// 自身のindex位置
        /// </summary>
        public Point Pos { get; private set; }

        
    }
}
