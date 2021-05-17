using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Clarity.Util
{
    public class RandomMaker
    {
        /// <summary>
        /// ランダム
        /// </summary>
        private static Random Rand = null;


        /// <summary>
        /// ランダムの初期化 今回の初期化値
        /// </summary>
        internal static int Init()
        {
            int val = (int)(DateTime.Now.Ticks);
            RandomMaker.Rand = new Random(val);
            return val;
        }

        /// <summary>
        /// Float値の返却
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float GetSingle(float min = 0.0f, float max = 1.0f)
        {

            float ans = RandomUtil.NextFloat(RandomMaker.Rand, min, max);
            return ans;
        }

        /// <summary>
        /// /Intの返却
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetInt(int min = 0, int max = 100)
        {
            int ans = min;
            try
            {
                ans = Convert.ToInt32(RandomUtil.NextLong(RandomMaker.Rand, min, max));
            }
            catch
            {
                ans = min;
            }
            return ans;
        }


        /// <summary>
        /// ランダムVector2の取得(normalize済み)
        /// </summary>
        /// <param name="rad">中心角度(rad)</param>
        /// <param name="range">発射のランダムの範囲(rad)</param>
        /// <returns></returns>
        public static Vector2 GetRandomVector(float rad = 0.0f, float range = (float)(Math.PI * 2))
        {
            //角度を取得
            float a = GetSingle(rad - range, rad + range);

            //角度の作成
            Vector2 ans = new Vector2();
            ans.X = (float)Math.Cos(a);
            ans.Y = (float)Math.Sin(a);

            return ans;
        }

        /// <summary>
        /// ランダムベクトルの生成
        /// </summary>
        /// <param name="dir">中心方向(単位ベクトルで)</param>
        /// <param name="range">発射の範囲</param>
        /// <returns></returns>
        public static Vector2 GetRandomVector(Vector2 dir, float range)
        {
            //発射方向を計算
            double centerrad = Math.Atan2(dir.X, -dir.Y);
            if (centerrad < 0.0)
            {
                centerrad = (Math.PI * 2) + centerrad;
            }
            centerrad -= (Math.PI * 0.5);

            float rad = (float)centerrad;

            //ランダムの取得
            Vector2 ans = GetRandomVector(rad, range);
            return ans;
        }
    }
}
