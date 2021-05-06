using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;


namespace Clarity.Element
{
    /// <summary>
    /// フェードイン、フェードアウトオブジェクト1
    /// </summary>
    public class ClarityFadeObject : ClarityObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="alspeed">1sにおけるアルファ色減衰速度(マイナス値ならフェードイン 正ならフェードアウトと判断)</param>
        public ClarityFadeObject(float alspeed) : base(10000)
        {
            
            this.AlphaSpeed = alspeed;
            this.WorkFlag = (alspeed < 0);
        }


        /// <summary>
        /// 動作モード true=フェードイン
        /// </summary>
        private bool WorkFlag;

        /// <summary>
        /// 減衰速度
        /// </summary>
        public float AlphaSpeed = 0.0f;

        /// <summary>
        /// フェード画像色
        /// </summary>
        public Vector3 DefaultColor = new Vector3(0.0f);

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            this.Color = new Vector4(this.DefaultColor, 0.0f);
            this.ShaderID = ClarityDataIndex.Shader_NoTexture;
            if (this.WorkFlag == true)
            {
                this.Color = new Vector4(this.DefaultColor, 1.0f);
            }

            this.TransSet.Scale2D = new Vector2(Core.DxManager.Mana.WindowSize.Width, Core.DxManager.Mana.WindowSize.Height);


        }

        /// <summary>
        /// 処理
        /// </summary>
        protected override void ProcElement()
        {
            //完全透明もしくは完全不透明になったら役目を終えたので消える
            if (this.Color.W < 0 || this.Color.W > 1)
            {
                ClarityEngine.RemoveElement(this);
                return;
            }

            this.FrameSpeed.Color.W = this.AlphaSpeed;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        protected override void RenderElement()
        {
            Core.DxManager.Mana.DisabledDepthStencil();
            base.RenderElement();
            Core.DxManager.Mana.EnabledDepthStencil();
        }
    }
}
