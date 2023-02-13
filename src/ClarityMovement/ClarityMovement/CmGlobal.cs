using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    internal class CmGlobal : Clarity.BaseClarityConstSingleton<CmGlobal>
    {
        /// <summary>
        /// Projectデータ
        /// </summary>
        public ReactiveProperty<CmProject?> ProjectData { get; set; } = new ReactiveProperty<CmProject?>();
        /// <summary>
        /// project static access support
        /// </summary>
        public static ReactiveProperty<CmProject?> Project
        {
            get
            {
                return CmGlobal.Mana.ProjectData;
            }
        }
    }
}
