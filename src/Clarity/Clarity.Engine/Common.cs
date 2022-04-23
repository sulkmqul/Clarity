using System;
using System.Collections.Generic;
using System.Numerics;
using Vortice.Mathematics;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("ClarityAid")]

namespace Clarity.Engine
{
    /// <summary>
    /// アニメ終了Delegate
    /// </summary>
    /// <param name="aid">終了animeID</param>
    public delegate void EndTextureAnimeDelegate(int aid, ref bool nextflag);


    /// <summary>
    /// 描画情報のまとめ
    /// </summary>
    public class RendererSet
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public RendererSet()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vid"></param>
        /// <param name="texid"></param>
        /// <param name="shid"></param>
        public RendererSet(int vid, int texid, int shid)
        {
            this.VertexID = vid;
            this.TextureID = texid;
            this.ShaderID = shid;
        }

        /// <summary>
        /// 使用する頂点ID
        /// </summary>
        public int VertexID = -1;
        /// <summary>
        /// 使用するTextureID一覧
        /// </summary>        
        public List<int> TextureIdList = new List<int>() { -1 };
        /// <summary>
        /// テクスチャID
        /// </summary>
        public int TextureID
        {
            get
            {
                return this.TextureIdList[0];
            }
            set
            {
                this.SetTextureId(0, value);
            }
        }

        /// <summary>
        /// 使用するシェーダーID
        /// </summary>
        public int ShaderID = -1;

        /// <summary>
        /// これの色
        /// </summary>
        public Color4 Color = new Color4(1.0f, 1.0f, 1.0f, 1.0f);

        /// <summary>
        /// テクスチャ < 1.0f
        /// </summary>
        public Vector2 TextureOffset = new Vector2(0.0f);


        /// <summary>
        /// TextureID値の追加
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="texid"></param>
        public void SetTextureId(int slot, int texid)
        {
            //Slot以内？
            int c = (this.TextureIdList.Count - 1) - slot;
            if (c < 0)
            {
                c = Math.Abs(c);
                for (int i = 0; i < c; i++)
                {
                    this.TextureIdList.Add(-1);
                }
            }

            this.TextureIdList[slot] = texid;
        }
    }

    public static class Common
    {
        /// <summary>
        /// 指定方向を計算する
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="trans">180°判定可否 = trueで反転</param>
        /// <returns></returns>
        public static float CalcuZDirection(Vector2 vec, bool trans = false)
        {
            float ans = 0.0f;

            double rad = Math.Atan2(vec.X, -vec.Y);
            if (rad < 0.0)
            {
                rad = (Math.PI * 2) + rad;
            }
            if (trans == false)
            {
                rad += Math.PI;
            }
            ans = Convert.ToSingle(rad);
            return ans;
        }




        /// <summary>
        /// Indexで色の取得
        /// </summary>
        /// <param name="col"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static float GetIndex(this Vortice.Mathematics.Color4 col, int i)
        {
            switch (i)
            {
                case 0:
                    return col.R;
                case 1:
                    return col.G;
                case 2:
                    return col.B;
                case 3:
                    return col.A;
                default:
                    throw new ArgumentOutOfRangeException("Color4 range 0 .. 3");
            }
        }

        /// <summary>
        /// 転置行列の計算
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Matrix4x4 CreateTransposeMat(this Clarity.TransposeSet data)
        {
            WorldData wdata = WorldManager.Mana.Get(data.WorldID);

            Matrix4x4 tm = Matrix4x4.CreateTranslation(data.Pos3D);
            Matrix4x4 wm = Matrix4x4.CreateScale(data.Scale3D);
            Matrix4x4 wmrate = Matrix4x4.CreateScale(data.ScaleRate);
            Matrix4x4 rmat = data.CreateTransposeRotationMat();


            //Matrix4x4 ans = wm * wmrate * rmat * tm * wdata.CamProjectionMat;            
            Matrix4x4 ans = wm * wmrate * rmat * tm * wdata.CamProjectionMat;
            ans.Transpose();    //転置必要・・・CPUからGPUに転送するときの制約らしいが？
            return ans;
        }

        /// <summary>
        /// SharpDXのMatrix.Transposeのコピー　転置行列を作成、
        /// </summary>
        /// <param name="value"></param>
        public static void Transpose(ref this Matrix4x4 value)
        {   
            Matrix4x4 temp = new Matrix4x4();
            temp.M11 = value.M11;
            temp.M12 = value.M21;
            temp.M13 = value.M31;
            temp.M14 = value.M41;
            temp.M21 = value.M12;
            temp.M22 = value.M22;
            temp.M23 = value.M32;
            temp.M24 = value.M42;
            temp.M31 = value.M13;
            temp.M32 = value.M23;
            temp.M33 = value.M33;
            temp.M34 = value.M43;
            temp.M41 = value.M14;
            temp.M42 = value.M24;
            temp.M43 = value.M34;
            temp.M44 = value.M44;

            value = temp;
        }

    }



}
