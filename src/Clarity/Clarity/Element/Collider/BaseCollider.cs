using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Collider
{

    /// <summary>
    /// 当たり判定種別
    /// </summary>
    public enum EColMode
    {
        /// <summary>
        /// 点
        /// </summary>
        Dot,
        /// <summary>
        /// 円
        /// </summary>
        Circle,
        /// <summary>
        /// 直線
        /// </summary>
        Line,
        /// <summary>
        /// 矩形
        /// </summary>
        Rect,
        /// <summary>
        /// ポリゴン(三角形)
        /// </summary>
        Polygon,
        /// <summary>
        /// 平面矩形
        /// </summary>
        PlaneRect,
        //-----------------------
        MAX,
    }

    /// <summary>
    /// 当たり判定変形種別
    /// </summary>
    public class EColiderTransposeMode
    {
        public const int None = 0;
        public const int Translation = 1 << 0;
        public const int Rotation = 1 << 1;
        public const int Scaling = 1 << 2;

        public const int ALL = EColiderTransposeMode.Translation | EColiderTransposeMode.Rotation | EColiderTransposeMode.Scaling;
    }

    /*
    /// <summary>
    /// 当たり判定情報描画所作
    /// </summary>
    class RenderColliderBehavior : BaseModelBehavior<BaseCollider>
    {
        protected override void ExecuteBehavior(BaseCollider obj)
        {
            //送られてきた物体が所作の場合は起動する
            BaseBehavior? beh = obj.RenderInfo as BaseBehavior;
            beh?.Execute(obj);
        }
    }*/

    /// <summary>
    /// 当たり判定描画所作
    /// </summary>
    public abstract class BaseRenderColliderBehavior : BaseModelBehavior<BaseCollider>
    {
    }

    /// <summary>
    /// 当たり判定オブジェクト基底
    /// </summary>
    public abstract class BaseCollider : BaseElement, ICloneable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="cmode">当たり判定種別</param>
        public BaseCollider(EColMode cmode) : base(-99)
        {
            this.ColMode = cmode;            
        }

        /// <summary>
        /// 当たり判定種別
        /// </summary>
        public EColMode ColMode { get; private set; } = EColMode.MAX;

        /// <summary>
        /// 当たり判定変形モード  EColiderTransposeModeをor指定し、親の影響を判断せよ
        /// </summary>
        public int ColiderTransposeMode = EColiderTransposeMode.ALL;

        /// <summary>
        /// true=今回のフレームでこれが誰かと衝突した(Debug描画用 これによって色を変更する)
        /// </summary>
        internal bool HitTempFlag = false;


        /// <summary>
        /// 初期化・・・・これは呼ばれないので無効化
        /// </summary>
        protected sealed override void InitElement()
        {
        }



        /// <summary>
        /// 当たり判定の変形
        /// </summary>
        /// <param name="tset"></param>
        public virtual void TransformCollider(TransposeSet tset)
        {
            //回転
            if ((this.ColiderTransposeMode & (int)EColiderTransposeMode.Rotation) != 0)
            {
                this.RotationCollider(tset);
            }            
            //拡縮
            if ((this.ColiderTransposeMode & (int)EColiderTransposeMode.Scaling) != 0)
            {
                this.ScalingCollider(tset);
            }
            //移動
            if ((this.ColiderTransposeMode & (int)EColiderTransposeMode.Translation) != 0)
            {
                this.TranslateCollider(tset);
            }
        }

        /// <summary>
        /// 当たり判定情報の移動変形
        /// </summary>
        /// <param name="tset"></param>
        protected abstract void TranslateCollider(TransposeSet tset);
        /// <summary>
        /// 当たり判定情報の回転変形
        /// </summary>
        /// <param name="tset"></param>
        protected abstract void RotationCollider(TransposeSet tset);
        /// <summary>
        /// 当たり判定情報の拡縮変形
        /// </summary>
        /// <param name="tset"></param>
        protected abstract void ScalingCollider(TransposeSet tset);

        /// <summary>
        /// これのコピー
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();
    }
}
