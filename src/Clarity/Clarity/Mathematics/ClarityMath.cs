using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Principal;

namespace Clarity.Mathematics
{
    /// <summary>
    /// 数学クラス
    /// </summary>
    public class ClarityMath
    {
        /// <summary>
        /// 無限平面と直線の交点を計算する
        /// </summary>
        /// <param name="pp">平面上の点</param>
        /// <param name="pn">無限平面を表す法線単位ベクトル(単位)</param>
        /// <param name="lst">直線開始点</param>
        /// <param name="ldir">直線の長さと方向</param>
        /// <returns>交点 (交点なし=null)</returns>
        public static Vector3? CalcuCrossPointInfinityPlaneLine(Vector3 pp, Vector3 pn, Vector3 lst, Vector3 ldir)
        {
            //直線の終点
            Vector3 led = lst + ldir;

            //平面から開始点の距離
            float stlen = Math.Abs(Vector3.Dot(pn, (lst - pp)));
            //平面から終了点の距離
            float edlen = Math.Abs(Vector3.Dot(pn, (led - pp)));
            if ((stlen + edlen) < 0.01)
            {
                //並行で交点なし
                return null;
            }

            //比を出す
            float rate = stlen / (stlen + edlen);

            //交点までのベクトル
            Vector3 cdir  = ldir * rate;

            //開始点を付与して交点座標を求める
            Vector3 ans = lst + cdir;

            return ans;
        }
        
    }
}
