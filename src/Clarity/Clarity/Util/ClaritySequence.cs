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
        public ClaritySequence(ulong startval = 0)
        {
            this.Current = startval;
        }

        /// <summary>
        /// 現在値
        /// </summary>
        public ulong Current { get; private set; } = 0;

        /// <summary>
        /// 次の値を取得
        /// </summary>
        public ulong NextValue
        {
            get
            {
                this.Current += 1;
                return this.Current;
            }
        }

        /// <summary>
        /// 値の初期化
        /// </summary>
        /// <param name="val">初期化値</param>
        public void Reset(ulong val = 0)
        {
            this.Current = val;
        }
    }
}
