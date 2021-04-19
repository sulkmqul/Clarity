using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Clarity.Shader
{

    /// <summary>
    /// Index4型を定義
    /// </summary>
    public struct Index4
    {
        public int a;
        public int b;
        public int c;
        public int d;

    }

    internal struct ShaderDataDefault
    {
        /// <summary>
        /// ワールド一覧
        /// </summary>
        public Matrix WorldViewProjMat;

        /// <summary>
        /// 色
        /// </summary>
        public Vector4 Color;

        /// <summary>
        /// テクスチャ分割サイズ(4分割なら 1/4で0.25と設定)
        /// </summary>
        public Vector2 TexDiv;

        /// <summary>
        /// TextureOffset(0 < x < 1.0)
        /// </summary>
        public Vector2 TextureOffset;

    }
}
