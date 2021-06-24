using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    /// <summary>
    /// 汎用ロジック
    /// </summary>
    public class CommonLogic
    {
        /// <summary>
        /// 浮動小数点の等価チェック
        /// </summary>
        /// <param name="val">値</param>
        /// <param name="ck">比較対象</param>
        /// <returns></returns>
        public static bool EqualSingle(float val, float ck)
        {
            float sp = 0.00001f;

            float min = ck - sp;
            float max = ck + sp;

            if (min < val && val < max)
            {
                return true;
            }

            return false;

        }
    }
}
