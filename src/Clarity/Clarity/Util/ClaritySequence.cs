using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Util
{
    /// <summary>
    /// 順番の値を制御するもの
    /// </summary>
    public class ClaritySequence
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="startval">初期値</param>
        public ClaritySequence(int startval = 1)
        {
            this.Current = startval;
        }

        /// <summary>
        /// 現在値
        /// </summary>
        public int Current { get; private set; } = 1;

        /// <summary>
        /// 次の値を取得
        /// </summary>
        public int NextValue
        {
            get
            {
                this.Current += 1;
                return this.Current;
            }
        }

    }
}
