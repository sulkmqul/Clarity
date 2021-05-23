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


        protected sealed override void RenderElement()
        {
            
        }


        internal sealed override void Render(FrameRenderParam rparam)
        {
            base.Render(rparam);
        }



    }
}
