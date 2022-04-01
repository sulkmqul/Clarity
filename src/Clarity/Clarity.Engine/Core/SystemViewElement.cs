using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Element;
using Clarity.Engine.Element;
using Clarity.Engine.Element.Behavior;

namespace Clarity.Engine.Core
{
    /// <summary>
    /// 描画所作
    /// </summary>
    class RenderingTextureBehavior : RenderDefaultBehavior
    {
        protected override void ExecuteBehavior(ClarityObject obj)
        {
            SystemViewElement sv = obj as SystemViewElement;
            if (sv == null)
            {
                return;
            }

            using (AlphaBlendDisabledState des = new AlphaBlendDisabledState())
            {
                //テクスチャの描画
                Clarity.Engine.Texture.TextureManager.SetTexture(sv.RenderingTextureResource);

                //シェーダー処理の設定
                this.RenderSetShaderData(obj);

                //描画頂点設定
                this.RenderSetVertex(obj);
            }
        }


        
    }

    /// <summary>
    /// Game描画RenderingTextureを描画するための管理element
    /// </summary>
    internal class SystemViewElement : ClarityObject
    {   
        public SystemViewElement() : base(0)
        {
            this.RenderBehavior = new RenderingTextureBehavior();
            
            
        }

        internal Vortice.Direct3D11.ID3D11ShaderResourceView RenderingTextureResource = null;

        /// <summary>
        /// 初期化
        /// </summary>
        public void InitSystemView()
        {
            this.TransSet.WorldID = WorldManager.SystemViewID;
            this.RenderSet.VertexID = BuildInDataIndex.VertexRect;
            this.RenderSet.ShaderID = BuildInDataIndex.ShaderDefault;
            this.RenderingTextureResource = DxManager.Mana.SystemViewTextureResource;
        }

        
    }
}
