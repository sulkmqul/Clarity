using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Clarity.Element.Collider
{
    /// <summary>
    /// 当たり判定コア演算クラス
    /// </summary>
    class ColliderCore
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColliderCore()
        {
            //当たり判定テーブルの作成
            this.DetectDic = this.CreateDetectColliderFuncTable();
        }

        /// <summary>
        /// 当たり判定関数のコア
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private delegate bool DetectColliderFuncDelegate(BaseCollider src, BaseCollider target);


        /// <summary>
        /// 当たり判定実行関数テーブル
        /// </summary>
        private Dictionary<EColMode, Dictionary<EColMode, DetectColliderFuncDelegate>> DetectDic = new Dictionary<EColMode, Dictionary<EColMode, DetectColliderFuncDelegate>>();

        /// <summary>
        /// 当たり判定実行関数テーブルの作成
        /// </summary>
        /// <returns></returns>
        private Dictionary<EColMode, Dictionary<EColMode, DetectColliderFuncDelegate>> CreateDetectColliderFuncTable()
        {
            Dictionary<EColMode, Dictionary<EColMode, DetectColliderFuncDelegate>> ans = new Dictionary<EColMode, Dictionary<EColMode, DetectColliderFuncDelegate>>();

            //srcが円
            Dictionary<EColMode, DetectColliderFuncDelegate> cirdic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                cirdic.Add(EColMode.Circle, DetectCircleCircle);
                cirdic.Add(EColMode.Line, null);
            }

            //srcが線
            Dictionary<EColMode, DetectColliderFuncDelegate> linedic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                linedic.Add(EColMode.Circle, null);
                linedic.Add(EColMode.Line, null);
            }


            ans.Add(EColMode.Circle, cirdic);
            ans.Add(EColMode.Line, linedic);

            return ans;

        }


        /// <summary>
        /// 当たり判定関数 true=衝突が発生した
        /// </summary>
        /// <param name="src">判定元</param>
        /// <param name="target">判定対象</param>
        /// <returns></returns>
        public bool DetectCollider(BaseCollider src, BaseCollider target)
        {
            DetectColliderFuncDelegate f = this.DetectDic[src.ColMode][target.ColMode];
            if (f == null)
            {
                string degs = string.Format("src={0} target={1}", src.ColMode, target.ColMode);
                throw new Exception("ColliderCore DetectCollider 定義されていない衝突判定です：" + degs);
            }

            //衝突判定！！！
            bool ret = f(src, target);

            return ret;

        }



        /// <summary>
        /// 円同士の当たり判定 円は2D判定です。必要なら3D版、sphereの球判定を作成せよ
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool DetectCircleCircle(BaseCollider src, BaseCollider target)
        {
            ColliderCircle cirsrc = src as ColliderCircle;
            ColliderCircle cirtar = target as ColliderCircle;

            //距離を計算して半径と比べる。弐乗のまま計算
            Vector3 vec = cirsrc.Center - cirtar.Center;
            float len = (vec.X * vec.X) + (vec.Y * vec.Y);

            double plen = Math.Sqrt((double)len);

            float rr = cirsrc.Radius + cirtar.Radius;
            if (plen < rr)
            {
                return true;
            }

            return false;
        }
    }
}
