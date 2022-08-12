using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Numerics;
using Clarity.Engine;

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
        public Vector3 AtPos { get; set; }
        /// <summary>
        /// 上の方向
        /// </summary>
        public Vector3 UpDir { get; protected set; }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


        public void SlideCamera(Vector3 v)
        {
            this.CameraPos += v;
            this.AtPos += v;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            this.TransSet.WorldID = OrbitGlobal.OrbitWorldID;
            this.UpDir = new Vector3(0.0f, -1.0f, 0.0f);

            this.AddProcBehavior(new ActionBehavior((x) =>
            {
                //設定された情報から上方向を計算する・・・とりあえず保留

                //カメラの再計算
                Matrix4x4 cmat = Matrix4x4.CreateLookAt(this.CameraPos, this.AtPos, this.UpDir);

                ClarityEngine.UpdateCamera(this.TransSet.WorldID, cmat);


            }));
        }
    }
}
