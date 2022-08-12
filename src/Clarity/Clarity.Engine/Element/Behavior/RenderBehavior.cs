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

    public abstract class BaseClarityObjectBehavior : BaseModelBehavior<ClarityObject>
    {
    }

    

    /// <summary>
    /// デフォルト描画所作
    /// </summary>
    public class BaseRenderBehavior : BaseClarityObjectBehavior
    {
        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="obj"></param>
        protected override void ExecuteBehavior(ClarityObject obj)
        {
            //using (AlphaBlendPlusEnabledState ed = new AlphaBlendPlusEnabledState())
            {
                //Viewport設定
                this.RenderSetViewPort(obj);

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
        protected virtual void RenderSetTexture(ClarityObject obj)
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
        protected virtual void RenderSetShaderData(ClarityObject obj)
        {
            RendererSet rset = obj.RenderSet;

            //テクスチャの設定
            Vector2 tdiv = Clarity.Engine.Texture.TextureManager.GetTextureDivSize(rset.TextureID) ?? new Vector2(1.0f, 1.0f);

            //Shaderに対する設定
            ShaderDataDefault data = new ShaderDataDefault();
            data.WorldViewProjMat = obj.TransSet.CreateTransposeMat();
            data.Color = rset.Color;
            data.AddColor = rset.AddColor;
            data.TexDiv = tdiv;

            data.TextureOffset = rset.TextureOffset;


            ShaderManager.SetShaderDataDefault(data, rset.ShaderID);

        }


        /// <summary>
        /// 頂点の設定と描画
        /// </summary>
        protected virtual void RenderSetVertex(ClarityObject obj)
        {
            Engine.Vertex.VertexManager.RenderData(obj.RenderSet.VertexID);
            
        }

        /// <summary>
        /// ViewPortの設定
        /// </summary>
        /// <param name="obj"></param>
        protected virtual void RenderSetViewPort(ClarityObject obj)
        {
            //Viewportの設定
            WorldData wd = WorldManager.Mana.Get(obj.TransSet.WorldID);
            Core.DxManager.Mana.DxContext.RSSetViewport(wd.VPort.VPort);
        }
    }


    internal class RenderDefaultBehavior : BaseRenderBehavior
    {
    }


}
