using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Clarity.Vertex
{
    /// <summary>
    /// 頂点情報 DirectXに渡すため、structで定義
    /// </summary>
    public struct VertexInfo
    {
        /// <summary>
        /// 頂点位置
        /// </summary>
        public Vector4 Pos;

        /// <summary>
        /// 頂点色
        /// </summary>
        public Color4 Col;

        /// <summary>
        /// テクスチャUV
        /// </summary>
        public Vector2 Tex;
    }
}
