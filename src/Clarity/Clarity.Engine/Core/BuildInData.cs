using Clarity.Engine.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Core
{
    
    /// <summary>
    /// ビルドインデータ管理
    /// </summary>
    class BuildInData
    {

        /// <summary>
        /// ビルドインなデフォルトデータの読み込み
        /// </summary>
        public static void LoadBuildInData()
        {
            try
            {
                //頂点の読みお込み
                BuildInData.LoadVertex();

                ///シェーダー情報の読み込み
                BuildInData.LoadShader();

                //テクスチャの読み込み
                BuildInData.LoadTexture();

            }
            catch (Exception ex)
            {
                throw new Exception("LoadBuildInData()", ex);
            }

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 頂点情報の読み込み
        /// </summary>
        private static void LoadVertex()
        {
            PolyCsvData[] vdata = {
                new PolyCsvData(){ VNo = ClarityEngine.BuildInPolygonModelIndex.Rect, Text = Properties.Resources.VRect },
                new PolyCsvData(){ VNo = ClarityEngine.BuildInPolygonModelIndex.Line, Text = Properties.Resources.VLine },
            };

            VertexManager.Mana.LoadCSV(vdata.ToList());
        }

        /// <summary>
        /// Shaderの読み込み
        /// </summary>
        private static void LoadShader()
        {
            Shader.ShaderManager.Mana.AddResource(new Shader.ShaderSrcData("Default", ClarityEngine.BuildInShaderIndex.Default, "VsDefault", "PsDefault", Properties.Resources.SDef));
            Shader.ShaderManager.Mana.AddResource(new Shader.ShaderSrcData("NoTexture", ClarityEngine.BuildInShaderIndex.NoTexture, "VsDefault", "PsNoTex", Properties.Resources.SDef));
            Shader.ShaderManager.Mana.AddResource(new Shader.ShaderSrcData("TextureAnime", ClarityEngine.BuildInShaderIndex.TextureAnime, "VsTextureAnimation", "PsDefault", Properties.Resources.SDef));
            Shader.ShaderManager.Mana.AddResource(new Shader.ShaderSrcData("TextureAlpha", ClarityEngine.BuildInShaderIndex.TextureUseAlpha, "VsDefault", "PsTextureAlphaOnlyBind", Properties.Resources.SDef));

        }


        /// <summary>
        /// テクスチャの読み込み
        /// </summary>
        private static void LoadTexture()
        {
            Texture.TextureManager.Mana.AddTexture(-100, "T0000", Properties.Resources.T0000, new System.Drawing.Size(1, 1));
            Texture.TextureManager.Mana.AddTexture(-99, "T0001", Properties.Resources.T0001, new System.Drawing.Size(1, 1));
        }
    }
}
