using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Clarity.Engine;
using Clarity.Engine.Shader;
using Clarity.Element;
using Clarity.Engine.Element.Behavior;
using Clarity.Engine.Texture;

namespace Clarity.Engine.Element
{

    /// <summary>
    /// 可変フレーム対応所作
    /// </summary>
    public class VariableFrameRateBehavior : BaseVariableFrameRateBehavior<ClarityObject>
    {
        protected override void ApplySpeed(ClarityObject obj)
        {
            base.ApplySpeed(obj);


            float rate = obj.FrameInfo.BaseRate;

            //色の反映
            float a = obj.RenderSet.Color.A + (obj.FrameSpeed.Color.W * rate);
            float r = obj.RenderSet.Color.R + (obj.FrameSpeed.Color.X * rate);
            float g = obj.RenderSet.Color.G + (obj.FrameSpeed.Color.Y * rate);
            float b = obj.RenderSet.Color.B + (obj.FrameSpeed.Color.Z * rate);
            obj.RenderSet.Color = new Vortice.Mathematics.Color4(r, g, b, a);
        }


    }


    /// <summary>
    /// テクスチャアニメ用処理
    /// </summary>
    internal class TextureAnimeProcBehavior : BaseClarityObjectBehavior
    {
        protected override void ExecuteBehavior(ClarityObject obj)
        {
            if (obj.TexAnimeCont == null)
            {
                return;
            }

            //アニメーション処理
            obj.TexAnimeCont.Anime(obj.ProcTime);

            //適切なTextureの設定
            obj.RenderSet.TextureID = obj.TexAnimeCont.CurrentFrameInfo.TextureID;
            obj.RenderSet.TextureOffset = obj.TexAnimeCont.CurrentFrameInfo.TextureOffset;
        }
    }


    /// <summary>
    /// ClarityEngine描画オブジェクト
    /// </summary>
    public class ClarityObject : BaseVariableElement
    {
        public ClarityObject(long oid) : base(oid)
        {
            this.RenderBehavior = new RenderDefaultBehavior();

            //可変フレーム所作を追加
            this.ProcBehavior.AddFixedProcess(new VariableFrameRateBehavior());

            //テクスチャアニメ所作
            this.ProcBehavior.AddFixedProcess(new TextureAnimeProcBehavior());
        }

        #region メンバ変数
        /// <summary>
        /// 描画情報
        /// </summary>
        internal RendererSet RenderSet = new RendererSet();


        /// <summary>
        /// テクスチャアニメ管理
        /// </summary>
        internal TextureAnimeController TexAnimeCont = null;
        #endregion

        /// <summary>
        /// テクスチャアニメーション有効可否
        /// </summary>
        public bool TextureAnimeEnabled
        {
            get
            {
                if (this.TexAnimeCont == null)
                {
                    return false;
                }
                return true;
            }
            set
            {
                if (value == true && this.TexAnimeCont == null)
                {                    
                    this.TexAnimeCont = new TextureAnimeController();
                    return;
                }
                this.TexAnimeCont = null;
            }
        }


        /// <summary>
        /// Textureアニメの番号を設定
        /// </summary>
        public int TexAnimeID
        {
            get
            {
                return this.TexAnimeCont.CurrentAnimeID;
            }
            set
            {
                this.TexAnimeCont?.ChangeAnime(value);
            }
        }
    }
}
