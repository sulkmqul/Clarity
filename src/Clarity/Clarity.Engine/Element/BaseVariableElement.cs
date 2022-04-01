using Clarity.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Element
{
    /// <summary>
    /// 可変フレームレート反映所作
    /// </summary>
    public class BaseVariableFrameRateBehavior<T> : BaseModelBehavior<T> where T : BaseVariableElement
    {
        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="obj"></param>
        protected sealed override void ExecuteBehavior(T obj)
        {
            //可変速度の繁栄
            this.ApplySpeed(obj);

            //速度反映後、初期化
            obj.FrameSpeed = new SpeedSet(0);
        }


        /// <summary>
        /// 可変速度の繁栄
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void ApplySpeed(T obj)
        {
            float rate = obj.FrameInfo.BaseRate;

            //速度の反映
            obj.TransSet.Pos3D += obj.FrameSpeed.Pos3D * rate;
            obj.TransSet.Rot += obj.FrameSpeed.Rot * rate;
            obj.TransSet.Scale3D += obj.FrameSpeed.Scale3D * rate;
            obj.TransSet.ScaleRate += obj.FrameSpeed.ScaleRate * rate;            
        }



    }

    /// <summary>
    /// 可変FPS対応Element基底
    /// </summary>
    public abstract class BaseVariableElement : BaseElement
    {
        public BaseVariableElement(long oid = 0) : base(oid)
        {

        }

        /// <summary>
        /// 今回の移動量(フレームにおける1sあたりの移動量を設定する)
        /// </summary>
        public SpeedSet FrameSpeed = new SpeedSet();
    }
}
