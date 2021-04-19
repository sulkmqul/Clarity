using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// 描画の無い処理者基底
    /// </summary>
    public abstract class BaseClarityProcessor : BaseElement
    {
        public BaseClarityProcessor(long oid) : base(oid)
        {
            
        }
        

        /// <summary>
        /// 処理者の描画を無効化する。
        /// </summary>
        /// <param name="rparam"></param>
        internal sealed override void Render(FrameRenderParam rparam)
        {
            
        }
    }
}
