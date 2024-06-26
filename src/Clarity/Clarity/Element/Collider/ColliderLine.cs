﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Collider
{
    /// <summary>
    /// 当たり判定定義クラス　線分
    /// </summary>
    public class ColliderLine : BaseCollider
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="spos">開始点</param>
        /// <param name="dir">方向(現時点Y軸に長さだけを設定し、回転で方向を変えることを前提にしています。そのうち任意方向を実装予定です)</param>
        public ColliderLine(Vector3 spos, Vector3 dir) : base(EColMode.Line)
        {
            this.StartPos = spos;
            this.Dir = dir;
        }


        /// <summary>
        /// 開始点
        /// </summary>
        public Vector3 StartPos = new Vector3();

        /// <summary>
        /// 方向と長さ
        /// </summary>
        public Vector3 Dir = new Vector3();

        /// <summary>
        /// 終了位点の計算
        /// </summary>
        public Vector3 EndPos
        {
            get
            {
                Vector3 ans = this.StartPos + this.Dir;
                return ans;
            }
        }


        /// <summary>
        /// これ回転(親の回転であり、完全に描画用)
        /// </summary>
        internal Vector3 ParentRot = new Vector3();


        
        /// <summary>
        /// 判定の回転処理
        /// </summary>
        /// <param name="tset">親の遷移情報</param>
        protected override void RotationCollider(TransposeSet tset)
        {
            this.ParentRot = tset.Rot;

            Matrix4x4 rmat = tset.CreateTransposeRotationMat();
            this.StartPos = Vector3.Transform(this.StartPos, rmat);
            this.Dir = Vector3.Transform(this.Dir, rmat);

            
        }

        /// <summary>
        /// 判定の拡縮処理
        /// </summary>
        /// <param name="tset">親の遷移情報</param>
        protected override void ScalingCollider(TransposeSet tset)
        {
            this.StartPos *= tset.ScaleRate;            
        }

        /// <summary>
        /// 判定の移動処理
        /// </summary>
        /// <param name="tset">親の遷移情報</param>
        protected override void TranslateCollider(TransposeSet tset)
        {
            this.StartPos += tset.Pos3D;
        }


        /// <summary>
        /// これのコピーを作成する
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            ColliderLine ans = new ColliderLine(this.StartPos, this.Dir);
            ans.ColiderTransposeMode = this.ColiderTransposeMode;
            return ans;
        }
    }
}
