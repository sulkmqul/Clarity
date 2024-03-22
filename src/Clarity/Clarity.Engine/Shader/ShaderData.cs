using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Engine.Shader
{

    /// <summary>
    /// Index4型を定義
    /// </summary>
    internal struct Index4
    {
        public int a;
        public int b;
        public int c;
        public int d;

    }

    /// <summary>
    /// デフォルトのShader設定データ
    /// </summary>
    public struct ShaderDataDefault
    {
        /// <summary>
        /// ワールド一覧
        /// </summary>
        public Matrix4x4 WorldViewProjMat;

        /// <summary>
        /// 色
        /// </summary>
        public Vector4 Color;

        /// <summary>
        /// 色
        /// </summary>
        public Vector4 AddColor;

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
