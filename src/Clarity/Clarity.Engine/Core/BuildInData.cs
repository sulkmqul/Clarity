﻿using Clarity.Engine.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Core
{
    internal class BuildInDataIndex
    {
        public const int VertexRect = -100;

        public const int ShaderDefault = -100;
        public const int ShaderNoTexture = -99;
        public const int ShaderTextureAnime = -98;
    }

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
                new PolyCsvData(){ VNo = -100, Text = Properties.Resources.VRect },
            };

            VertexManager.Mana.LoadCSV(vdata.ToList());
        }

        /// <summary>
        /// Shaderの読み込み
        /// </summary>
        private static void LoadShader()
        {
            Shader.ShaderListFileDataRoot rdata = new Shader.ShaderListFileDataRoot();
            rdata.RootID = -100;
            rdata.ShaderList = new List<Shader.ShaderListData>();
            rdata.ShaderList.Add(new Shader.ShaderListData() { Code = "Default", SrcCode = Properties.Resources.SDef, VsName = "VsDefault", PsName = "PsDefault" });
            rdata.ShaderList.Add(new Shader.ShaderListData() { Code = "NoTexture", SrcCode = Properties.Resources.SDef, VsName = "VsDefault", PsName = "PsNoTex" });
            rdata.ShaderList.Add(new Shader.ShaderListData() { Code = "TextureAnime", SrcCode = Properties.Resources.SDef, VsName = "VsTextureAnimation", PsName = "PsDefault" });
            Shader.ShaderManager.Mana.CreateDefaultResource(rdata);
        }


        /// <summary>
        /// テクスチャの読み込み
        /// </summary>
        private static void LoadTexture()
        {
            Texture.TextureManager.Mana.AddTexture(-100, Properties.Resources.T0000, "T0000", new System.Drawing.Size(1, 1));
            Texture.TextureManager.Mana.AddTexture(-99, Properties.Resources.T0001, "T0001", new System.Drawing.Size(1, 1));
        }
    }
}
