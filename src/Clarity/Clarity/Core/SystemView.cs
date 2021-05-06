using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Element;
using Clarity.Shader;
using Clarity.Texture;
using Clarity.Vertex;
using SharpDX.Direct3D11;

namespace Clarity.Core
{
    /// <summary>
    /// RenderingTextureの描画 かなり変則的なことをしていますが、汎用Viewでもないのでまぁｾｰﾌとしたい。
    /// </summary>
    class SystemView : BaseElement
    {
        public SystemView() : base(0)
        {
            this.TransSet.WorldID = WorldManager.SystemViewID;
            this.VertexID = ClarityDataIndex.Vertex_Display;
            this.ShaderID = ClarityDataIndex.Shader_Default;
        }


        protected override void InitElement()
        {

        }

        protected override void ProcElement()
        {

        }


        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sv"></param>
        internal void Render(ShaderResourceView sv)
        {
            RendererSet rset = this.RenderSet;

            //Shaderに対する設定
            ShaderDataDefault data = new ShaderDataDefault();
            data.WorldViewProjMat = this.TransSet.CreateTransposeMat();
            data.Color = rset.Color;

            ShaderManager.SetShaderDataDefault(data, rset.ShaderID);
            TextureManager.SetTexture(sv);

            //描画
            VertexManager.RenderData(rset.VertexID);
        }
    }

}
