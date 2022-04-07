using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Clarity.Engine;
using Clarity.Engine.Shader;
using Clarity;
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
            //基底描画設定
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
        public TextureAnimeController TexAnimeCont { get; protected set; } = null;
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
        public int TextureAnimeID
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

        #region RendererSetアクセサ
        /// <summary>
        /// 頂点ID
        /// </summary>
        public int VertexID
        {
            get
            {
                return this.RenderSet.VertexID;
            }
            set
            {
                this.RenderSet.VertexID = value;
            }
        }
        /// <summary>
        /// テクスチャコード
        /// </summary>
        public int TextureID
        {
            get
            {
                return this.RenderSet.TextureID;
            }
            set
            {
                this.RenderSet.TextureID = value;
            }
        }
        /// <summary>
        /// シェーダーコード
        /// </summary>
        public int ShaderID
        {
            get
            {
                return this.RenderSet.ShaderID;
            }
            set
            {
                this.RenderSet.ShaderID = value;
            }
        }

        /// <summary>
        /// 色の設定
        /// </summary>
        public Vortice.Mathematics.Color4 Color
        {
            get
            {
                return this.RenderSet.Color;
            }
            set
            {
                this.RenderSet.Color = value;
            }
        }
        
        /// <summary>
        /// TextureOffset < 1.0
        /// </summary>
        public Vector2 TextureOffset
        {
            get
            {
                return this.RenderSet.TextureOffset;
            }
            set
            {
                this.RenderSet.TextureOffset = value;
            }
        }

        /// <summary>
        /// TextureIDの設定
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="texid"></param>
        public void SetTextureID(int slot, int texid)
        {
            this.RenderSet.SetTextureId(slot, texid);
        }

        /// <summary>
        /// TextureID値の取得
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public int GetTextureID(int slot)
        {
            return this.RenderSet.TextureIdList[slot];
        }
        #endregion

        /// <summary>
        /// 自身の削除を行う
        /// </summary>
        public void RemoveSelf()
        {
            ClarityEngine.RemoveManage(this);            
        }


        /// <summary>
        /// 現在のテクスチャサイズを自身のサイズとして設定する
        /// </summary>
        public void FitTextureSize()
        {
            this.TransSet.Scale2D = ClarityEngine.GetTextureSize(this.TextureID);
        }

    }
}
