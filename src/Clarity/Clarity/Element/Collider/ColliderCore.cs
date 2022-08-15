using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Collider
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

            //srcが点
            Dictionary<EColMode, DetectColliderFuncDelegate> dotdic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                dotdic.Add(EColMode.Dot, null);
                dotdic.Add(EColMode.Circle, this.DetectCircleDot);
                dotdic.Add(EColMode.Line, null);
            }

            //srcが円
            Dictionary<EColMode, DetectColliderFuncDelegate> cirdic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                cirdic.Add(EColMode.Dot, this.DetectCircleDot);
                cirdic.Add(EColMode.Circle, this.DetectCircleCircle);
                cirdic.Add(EColMode.Line, this.DetectCircleLine);
            }

            //srcが線
            Dictionary<EColMode, DetectColliderFuncDelegate> linedic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                linedic.Add(EColMode.Dot, null);
                linedic.Add(EColMode.Circle, this.DetectCircleLine);
                linedic.Add(EColMode.Line, null);
                linedic.Add(EColMode.Polygon, this.DetectLinePolygon);
                linedic.Add(EColMode.PlaneRect, this.DetectLinePlaneRect);
            }
            //srcがpolygon
            Dictionary<EColMode, DetectColliderFuncDelegate> polydic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                polydic.Add(EColMode.Line, this.DetectLinePolygon);                
            }
            //srcがplanerect
            Dictionary<EColMode, DetectColliderFuncDelegate> plrdic = new Dictionary<EColMode, DetectColliderFuncDelegate>();
            {
                plrdic.Add(EColMode.Line, this.DetectLinePlaneRect);
            }


            ans.Add(EColMode.Dot, dotdic);
            ans.Add(EColMode.Circle, cirdic);
            ans.Add(EColMode.Line, linedic);
            ans.Add(EColMode.Polygon, polydic);
            ans.Add(EColMode.PlaneRect, plrdic);

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
            bool ret = false;

            try
            {
                DetectColliderFuncDelegate f = this.DetectDic[src.ColMode][target.ColMode];
                if (f == null)
                {
                    throw new Exception("DetectColliderFuncDelegate NULL");
                }

                //衝突判定！！！
                ret = f(src, target);
            }
            catch (Exception e)
            {
                string degs = string.Format("src={0} target={1}", src.ColMode, target.ColMode);
                throw new Exception("ColliderCore DetectCollider 定義されていない衝突判定です：" + degs, e);
            }

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



        /// <summary>
        /// 点と円の当たり判定 (2Dです)
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool DetectCircleDot(BaseCollider src, BaseCollider target)
        {
            //データ変換
            ColliderCircle cir = src as ColliderCircle;
            ColliderDot dot = target as ColliderDot;
            if (cir == null)
            {
                cir = target as ColliderCircle;
                dot = src as ColliderDot;
            }

            //距離を計算して半径と比べる。弐乗のまま計算
            Vector3 vec = cir.Center - dot.Center;
            float len = (vec.X * vec.X) + (vec.Y * vec.Y);

            double plen = Math.Sqrt((double)len);

            float rr = cir.Radius;
            if (plen < rr)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// 外積・・・・ここにあるべきかは疑問
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private float Vec2Cross(Vector3 a, Vector3 b)
        {
            float ans = 0.0f;

            ans = a.X * b.Y - a.Y * b.X;

            return ans;
        }

        /// <summary>
        /// 内積計算
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private float Vec2Dot(Vector3 a, Vector3 b)
        {
            float ans = 0.0f;
            ans = a.X * b.X + a.Y * b.Y;
            return ans;
        }





        /// <summary>
        /// 円と線分の当たり判定
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool DetectCircleLine(BaseCollider src, BaseCollider target)
        {
            //データ変換
            ColliderCircle cir = src as ColliderCircle;
            ColliderLine line = target as ColliderLine;
            if (cir == null)
            {
                cir = target as ColliderCircle;
                line = src as ColliderLine;
            }

            //まず開始点と終了点が円の範囲内かを調べる。
            {
                //点の判定を作成
                ColliderDot dst = new ColliderDot(line.StartPos);
                ColliderDot ded = new ColliderDot(line.EndPos);

                bool dotet = this.DetectCircleDot(cir, dst);
                if (dotet == true)
                {
                    return true;
                }
                dotet = this.DetectCircleDot(cir, ded);
                if (dotet == true)
                {
                    return true;
                }

            }


            //ここまで来たら点が円の外なので直線判定を行う。

            //開始位置から円へのベクトル
            Vector3 stcen = cir.Center - line.StartPos;
            Vector3 lvec = line.Dir;

            //線の長さ
            float linelen = line.Dir.Length();

            //
            float cros = this.Vec2Cross(lvec, stcen);
            cros /= linelen;
            cros = Math.Abs(cros);
            if (cros > cir.Radius)
            {
                return false;
            }

            //ここまできたら無限直線では当たっている。
            //あとは線の端から中心までの内積をとり、双方が鋭角ならとりあえずあたりとする(端の判定はしていないが、まぁ不要)
            //終了点から円へのベクトル
            Vector3 edcen = cir.Center - line.EndPos;


            lvec = Vector3.Normalize(lvec);
            stcen = Vector3.Normalize(stcen);
            edcen = Vector3.Normalize(edcen);

            float stdot = this.Vec2Dot(stcen, lvec);
            float eddot = this.Vec2Dot(edcen, lvec);
            //float stdot = Vector2.Dot(new Vector2(stcen.X, stcen.Y), new Vector2(lvec.X, lvec.Y));
            //float eddot = Vector2.Dot(new Vector2(edcen.X, edcen.Y), new Vector2(lvec.X, lvec.Y));

            //符号が違うならあたり・・・かもしれない。
            //ようは符号が違う場合、交点が線の中であることは確定＝あたり
            //しかし、交点が外側でも地味に端が範囲内、ってことがあり得る。
            if ((stdot < 0.0 && eddot < 0.0) || (stdot > 0.0 && eddot > 0.0))
            {
                return false;
            }

            return true;

        }

        /// <summary>
        /// 直線と三角ポリゴンの当たり判定
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// <remarks>https://risalc.info/src/line-triangle-intersection.html</remarks>
        private bool DetectLinePolygon(BaseCollider src, BaseCollider target)
        {
            
            return this.DetectLinePolygonEx(src, target);

            //データ変換            
            ColliderPolygon pol = src as ColliderPolygon;
            ColliderLine line = target as ColliderLine;
            if (pol == null)
            {
                pol = target as ColliderPolygon;
                line = src as ColliderLine;
            }

            //ポリゴンの存在する無限平面を表すものを求める
            Vector3 h = pol.CalcuNormal();


            //無限平面と直線と交点を求める
            float nm = Vector3.Dot(h, line.Dir);
            float nr = Vector3.Dot(h, line.StartPos);
            Vector3 hcorsspos = line.StartPos + ((nr / nm) * line.Dir);


            //係数を出す
            Matrix4x4 cmat = Matrix4x4.Identity;
            cmat.M11 = pol.SideLineA.X;
            cmat.M21 = pol.SideLineA.Y;
            cmat.M31 = pol.SideLineA.Z;

            cmat.M12 = pol.SideLineB.X;
            cmat.M22 = pol.SideLineB.Y;
            cmat.M32 = pol.SideLineB.Z;

            cmat.M13 = h.X;
            cmat.M23 = h.Y;
            cmat.M33 = h.Z;

            Matrix4x4 cmatinv;
            Matrix4x4.Invert(cmat, out cmatinv);

            Vector3 r = Vector3.Transform(hcorsspos, cmatinv);

            //交点を算出
            Vector3 tpos = (r.X * pol.SideLineA) + (r.Y * pol.SideLineB) + (r.Z * h);

            //交点が三角形に含まれているかを確認

            if (r.X > 0 && r.Y > 0 && (r.X + r.Y) < 1.0f)
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <remarks>http://ft-lab.ne.jp/cgi-bin/wiki.cgi?page=%B8%F2%BA%B9%C8%BD%C4%EA_3DCG</remarks>
        /// <returns></returns>
        private bool DetectLinePolygonEx(BaseCollider src, BaseCollider target)
        {
            //データ変換            
            ColliderPolygon pol = src as ColliderPolygon;
            ColliderLine line = target as ColliderLine;
            if (pol == null)
            {
                pol = target as ColliderPolygon;
                line = src as ColliderLine;
            }

            Vector3 dnor = Vector3.Normalize(line.Dir);

            Vector3 pvec = Vector3.Cross(dnor, pol.SideLineB);
            float det = Vector3.Dot(pvec, pol.SideLineA);

            float v = 0.0f;
            float u = 0.0f;
            Vector3 qvec = new Vector3();
            if (det > 0.000001f)
            {
                Vector3 tvec = line.StartPos - pol.VertexList[0];
                u = Vector3.Dot(pvec, tvec);
                if (u < 0.0f || u > det)
                {
                    return false;
                }
                qvec = Vector3.Cross(tvec, pol.SideLineA);
                v = Vector3.Dot(dnor, qvec);
                if (v < 0.0f || (u + v) > det)
                {
                    return false;
                }
            }
            else if (det < -0.000001f)
            {
                Vector3 tvec = line.StartPos - pol.VertexList[0];
                u = Vector3.Dot(pvec, tvec);
                if (u > 0.0f || u < det)
                {
                    return false;
                }
                qvec = Vector3.Cross(tvec, pol.SideLineA);
                v = Vector3.Dot(dnor, qvec);
                if (v > 0.0f || (u + v) < det)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            float invdet = 1.0f / det;
            float t = Vector3.Dot(pol.SideLineB, qvec);

            t *= invdet;
            u *= invdet;
            v *= invdet;

            //これまでで無限直線との交差点が求まる
            Vector3 cosspos = (t * dnor) + line.StartPos;


            //交差点が開始と終了の間にあればよい


            return true; 
        }



        /// <summary>
        /// 直線と平面矩形の判定
        /// </summary>
        /// <param name="src"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool DetectLinePlaneRect(BaseCollider src, BaseCollider target)
        {
            //データ変換            
            ColliderPlaneRect prect = src as ColliderPlaneRect;
            ColliderLine line = target as ColliderLine;
            if (prect == null)
            {
                prect = target as ColliderPlaneRect;
                line = src as ColliderLine;
            }

            //内包しているpolygonと判定
            for (int i = 0; i < prect.Poly.Count; i++)
            {
                //当たったら終わり
                bool f = this.DetectLinePolygon(line, prect.Poly[i]);
                if (f == true)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
