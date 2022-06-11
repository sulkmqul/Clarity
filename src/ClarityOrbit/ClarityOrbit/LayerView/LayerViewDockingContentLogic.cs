using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClarityOrbit.LayerView
{
    /// <summary>
    /// ロジック
    /// </summary>
    internal class LayerViewDockingContentLogic
    {
        public LayerViewDockingContentLogic(LayerViewDockingContent parent)
        {
            this.Form = parent;
        }


        /// <summary>
        /// 管理画面
        /// </summary>
        private LayerViewDockingContent Form { get; init; }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            
        }

        /// <summary>
        /// 解除
        /// </summary>
        public void Release()
        {
            
        }

    }
}
