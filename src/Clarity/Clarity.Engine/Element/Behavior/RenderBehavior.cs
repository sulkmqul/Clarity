using Clarity.Element;
using Clarity.Engine.Shader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Element.Behavior
{
    class RenderBehavior
    {
    }

    internal abstract class BaseClarityObjectBehavior : BaseModelBehavior<ClarityObject>
    {
    }

    

    /// <summary>
    /// デフォルト描画所作
    /// </summary>
    internal class RenderDefaultBehavior : BaseClarityObjectBehavior
    {
        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="obj"></param>
        protected override void ExecuteBehavior(ClarityObject obj)
        {
            //using (AlphaBlendPlusEnabledState ed = new AlphaBlendPlusEnabledState())
            {
                //テクスチャ設定
                this.RenderSetTexture(obj);

                //シェーダー処理の設定
                this.RenderSetShaderData(obj);

                //描画頂点設定
                this.RenderSetVertex(obj);
            }
        }
        //
        /// <summary>
        /// 描画テクスチャの設定
        /// </summary>
        internal protected void RenderSetTexture(ClarityObject obj)
        {
            int index = 0;
            foreach (int texid in obj.RenderSet.TextureIdList)
            {
                Clarity.Engine.Texture.TextureManager.SetTexture(texid, index);
                index++;
            }
        }

        /// <summary>
        /// デフォルト描画関数
        /// </summary>
        internal protected void RenderSetShaderData(ClarityObject obj)
        {
            RendererSet rset = obj.RenderSet;

            //テクスチャの設定
            Vector2 tdiv = Clarity.Engine.Texture.TextureManager.GetTextureDivSize(rset.TextureID) ?? new Vector2(1.0f, 1.0f);

            //Shaderに対する設定
            ShaderDataDefault data = new ShaderDataDefault();
            data.WorldViewProjMat = obj.TransSet.CreateTransposeMat();
            data.Color = rset.Color;

            data.TexDiv = tdiv;

            data.TextureOffset = rset.TextureOffset;


            ShaderManager.SetShaderDataDefault(data, rset.ShaderID);

        }


        /// <summary>
        /// 頂点の設定と描画
        /// </summary>
        internal protected void RenderSetVertex(ClarityObject obj)
        {
            Engine.Vertex.VertexManager.RenderData(obj.RenderSet.VertexID);
        }
    }
}
