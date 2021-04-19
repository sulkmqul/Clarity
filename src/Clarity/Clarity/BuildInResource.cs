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
        /// シェーダーデフォルト表示
        /// </summary>
        public const int Shader_Default = -1;
        /// <summary>
        /// シェーダーテクスチャなし表示
        /// </summary>
        public const int Shader_NoTexture = -2;
        /// <summary>
        /// シェーダーテクスチャアニメーション
        /// </summary>
        public const int Shader_TextureAnime = -3;
    }
}
