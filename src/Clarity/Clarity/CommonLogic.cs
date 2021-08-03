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


        /// <summary>
        /// 浮動小数点の線形補間
        /// </summary>
        /// <param name="stt"></param>
        /// <param name="edt"></param>
        /// <param name="nowt"></param>
        /// <param name="sval"></param>
        /// <param name="eval"></param>
        /// <returns></returns>
        public static float ShiftLinearFloatValue(long stt, long edt, long nowt, float sval, float eval)
        {
            //開始より前だった
            if (stt > nowt)
            {
                return sval;
            }
            //終了より後だった
            if (edt <= nowt)
            {
                return eval;
            }

            float ans = 0.0f;

            float nt = nowt - stt;
            float span = edt - stt;
            float vlen = eval - sval;

            ans = sval + (nt / span) * vlen;

            return ans;
        }
    }
}
