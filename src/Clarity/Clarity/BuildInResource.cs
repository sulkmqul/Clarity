using Clarity.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    

    /// <summary>
    /// 組み込みリソース定義
    /// </summary>
    public class ClarityDataIndex
    {
        //ビルドインのコードはユーザー追加との混合を避けるため、マイナス値であること

        /// <summary>
        /// 全体表示データ
        /// </summary>
        public const int Vertex_Display = -1;

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 円テクスチャ
        /// </summary>
        public const int Texture_Circle = -1;

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// シェーダーデフォルト表示
        /// </summary>
        public const int Shader_Default = -100;
        /// <summary>
        /// シェーダーテクスチャなし表示
        /// </summary>
        public const int Shader_HitLight = -99;
        /// <summary>
        /// シェーダーテクスチャなし表示
        /// </summary>
        public const int Shader_NoTexture = -98;
        /// <summary>
        /// シェーダーテクスチャアニメーション
        /// </summary>
        public const int Shader_TextureAnime = -97;
    }


    class BuildInResource
    {
        /// <summary>
        /// 頂点データの読み込み
        /// </summary>
        private void LoadVertexData()
        {            

            //読み込みデフォルトデータ一式(VertexCode, ファイル内容CSV文字列)
            (int code, string csvs)[] defdatavec = {
                (ClarityDataIndex.Vertex_Display, Properties.Resources.VD000),
            };

            foreach (var defdata in defdatavec)
            {
                //対象の読み込み
                PolygonObjectFile pof = new PolygonObjectFile();
                PolyData pol = pof.ReadCsvString(defdata.csvs);

                //ADD                
                VertexManager.Mana.AddVertexDic(defdata.code, pol);

                

            }
        }


        /// <summary>
        /// シェーダーの読み込み
        /// </summary>
        private void LoadShaderData()
        {
            //Buildin Shader Listの読み込み
            Shader.ShaderListFile shlist = new Shader.ShaderListFile();
            Shader.ShaderListFileDataRoot rdata = shlist.ReadCsvString(Properties.Resources.shlist);


            Shader.ShaderManager.Mana.CreateResource(Properties.Resources.shader, rdata);
        }


        /// <summary>
        /// テクスチャの読み込み
        /// </summary>
        private void LoadTextureData()
        {
            Texture.TextureManager.Mana.AddTexture(ClarityDataIndex.Texture_Circle, Properties.Resources.T0000, "Texture_Circle", new System.Drawing.Size(1, 1));

        }

        /// <summary>
        /// リソースの読み込み
        /// </summary>
        public static void LoadBuildInData()
        {
            BuildInResource bir = new BuildInResource();

            //Vertex
            bir.LoadVertexData();

            //Shader
            bir.LoadShaderData();

            //Texture
            bir.LoadTextureData();
        }
    }
}
