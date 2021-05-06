using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// 管理者オブジェクト基底
    /// </summary>
    public abstract class BaseClarityAdminObject : BaseClarityProcessor
    {
        public BaseClarityAdminObject() : base(ElementManager.AdminObjectID)
        {
            
        }

        
        

    }
}
