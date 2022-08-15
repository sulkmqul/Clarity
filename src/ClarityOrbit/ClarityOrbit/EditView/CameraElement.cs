using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Numerics;
using Clarity.Engine;
using System.Drawing;

namespace ClarityOrbit.EditView
{
    /// <summary>
    /// カメラ管理
    /// </summary>
    internal class CameraElement : BaseElement
    {

        /// <summary>
        /// カメラ位置
        /// </summary>
        public Vector3 CameraPos { get; set; }
        /// <summary>
        /// カメラ見る位置
        /// </summary>
        public Vector3 AtPos = new Vector3();// { get; set; }
        /// <summary>
        /// 上の方向
        /// </summary>
        public Vector3 UpDir { get; protected set; }

        public Vector3? reqcampos =  null;
        public Vector3? reqcamat = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// カメラ位置をv分移動させる
        /// </summary>
        /// <param name="v">移動量</param>
        public void SlideCamera(Vector3 v)
        {
            this.CameraPos += v;
            this.AtPos += v;
        }
        /// <summary>
        /// カメラ位置を設定する
        /// </summary>
        /// <param name="v"></param>
        public void SetCamera(Vector3 v)
        {
            //this.CameraPos = v;
            //this.AtPos = new Vector3(v.X, v.Y, this.AtPos.Z);
            this.reqcampos = new Vector3(v.X, v.Y, this.CameraPos.Z);
            this.reqcamat = new Vector3(v.X, v.Y, this.AtPos.Z);
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            this.TransSet.WorldID = OrbitGlobal.OrbitWorldID;
            this.UpDir = Vector3.UnitY;

            //カメラ所作
            this.AddProcBehavior(new CameraProcBehavior());
            
        }
    }


    /// <summary>
    /// カメラ所作
    /// </summary>
    class CameraProcBehavior : BaseModelBehavior<CameraElement>
    {
        /// <summary>
        /// 所作
        /// </summary>
        /// <param name="obj"></param>
        protected override void ExecuteBehavior(CameraElement obj)
        {
            //リクエスト反映
            if (obj.reqcampos != null)
            {
                obj.CameraPos = obj.reqcampos.Value;
            }
            if (obj.reqcamat != null)
            {
                obj.AtPos = obj.reqcamat.Value;
            }
            obj.reqcampos = null;
            obj.reqcamat = null;

            //カメラの再計算
            Matrix4x4 cmat = Matrix4x4.CreateLookAt(obj.CameraPos, obj.AtPos, obj.UpDir);
            ClarityEngine.UpdateCamera(obj.TransSet.WorldID, cmat);

            if (OrbitGlobal.Project == null)
            {
                return;
            }

            //View範囲の再計算
            Rectangle vrc = this.CalcuViewAreaIndex();
            OrbitEditViewControl.TempInfo.ViewAreaIndexRect = vrc;
            ClarityEngine.SetSystemText($"{vrc.Left},{vrc.Top},{vrc.Right},{vrc.Bottom}", 0);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// カメラ範囲のViewIndexを計算する
        /// </summary>
        /// <returns></returns>
        private Rectangle CalcuViewAreaIndex()
        {
            //上から見ること前提になっていますが・・・ま

            Size vsize = ClarityEngine.GetViewSize();

            //四隅のindex位置を取得
            Point lt = this.CalcuFormPosToTileIndex(0, 0);
            Point rt = this.CalcuFormPosToTileIndex(vsize.Width, 0);
            Point lb = this.CalcuFormPosToTileIndex(0, vsize.Height);
            Point rb = this.CalcuFormPosToTileIndex(vsize.Width, vsize.Height);

            //+-を考慮して正しいindex範囲を確定させる
            int tl = Math.Min(lb.X, lt.X);
            int tr = Math.Max(rt.X, rb.X);
            int tt = Math.Min(lt.Y, rt.Y);
            int tb = Math.Max(lb.Y, lb.Y);

            int l = Math.Min(tl, tr);
            int r = Math.Max(tl, tr);
            int t = Math.Min(tt, tb);
            int b = Math.Max(tt, tb);

            //余白
            int ho = 0;
            l -= ho; t -= ho; r += ho; b += ho;

            return new Rectangle(l, t, r - l, b - t);
        }

        /// <summary>
        /// 画面座標位置からtile indexを計算する
        /// </summary>
        /// <param name="px">画面x</param>
        /// <param name="py">画面y</param>
        /// <returns></returns>
        private Point CalcuFormPosToTileIndex(int px, int py)
        {
            Vector3 zro = this.CalcuZeroPos(px, py);
            return OrbitGlobal.WorldToTileIndex(zro);
        }

        

        /// <summary>
        /// 画面座標から計算された直線において、Zが0となるworld座標を計算する
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <returns></returns>
        private Vector3 CalcuZeroPos(int px, int py)
        {
            Vector3 st = Clarity.Engine.ClarityEngine.WindowToWorld(0, px, py, 0.0f);
            Vector3 ed = Clarity.Engine.ClarityEngine.WindowToWorld(0, px, py, 1.0f);
            Vector3 dir = ed - st;

            Vector3 stzvec = new Vector3(st.X, st.Y, 0.0f) - st;
            Vector3 stznvec = Vector3.Normalize(stzvec);

            //角度の計算
            Vector3 ndir = Vector3.Normalize(dir);
            float cos = Vector3.Dot(stznvec, ndir);

            float len = stzvec.Z / cos;
            Vector3 zerolen = ndir * len;
            Vector3 apos = st + zerolen;

            Clarity.Engine.ClarityEngine.SetSystemText($"{apos.X},{apos.Y},{apos.Z}", 0);
            return apos;
        }
    }
}
