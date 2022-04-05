using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Element
{
    /// <summary>
    /// 一回処理が動いたら消えるElement、描画すらなし
    /// </summary>
    public class OnetimeElement : BaseElement
    {
        protected override void ProcCleanup()
        {
            ClarityEngine.RemoveManage(this);
        }
    }
}
