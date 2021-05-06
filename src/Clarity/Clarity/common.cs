using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace Clarity
{
    /// <summary>
    /// フレーム処理情報
    /// </summary>
    internal class FrameProcParam
    {
        public FrameProcParam()
        {
        }
        public FrameProcParam(FrameProcParam fp)
        {
            this.ProcIndex = fp.ProcIndex;
            this.FrameTime = fp.FrameTime;
            this.PrevFrameTime = fp.PrevFrameTime;
            this.FrameBaseRate = fp.FrameBaseRate;
        }

        /// <summary>
        /// 処理index
        /// </summary>
        public int ProcIndex;
        /// <summary>
        /// 今回のフレーム基準時間
        /// </summary>
        public long FrameTime;
        /// <summary>
        /// 前回の処理の基準時間
        /// </summary>
        public long PrevFrameTime;

        /// <summary>
        /// 基準時間レート(1sからどれだけ減るかの倍率)
        /// </summary>
        public float FrameBaseRate { get; private set; }

        /// <summary>
        /// 倍率の計算
        /// </summary>
        public void CalcuFrameBaseRate()
        {
            //可変時間を算出
            long span = this.FrameTime - this.PrevFrameTime;
            this.FrameBaseRate = (float)span / 1000.0f;
        }
    }

    /// <summary>
    /// フレーム描画情報
    /// </summary>
    internal class FrameRenderParam
    {
        /// <summary>
        /// ViewIndex
        /// </summary>
        public int ViewIndex;
        /// <summary>
        /// 描画Index
        /// </summary>
        public int RenderIndex;
    }



    /// <summary>
    /// アニメ終了Delegate
    /// </summary>
    /// <param name="aid"></param>
    public delegate void EndAnimeDelegate(int aid);

    



    /// <summary>
    /// 位置、回転、拡縮のセット
    /// </summary>
    public class Vector3Set
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Vector3Set()
        {
        }
        public Vector3Set(float f)
        {
            this.Pos = new Vector3(f);
            this.Rot = new Vector3(f);
            this.Scale = new Vector3(f);
        }

        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Pos = new Vector3(0.0f);
        /// <summary>
        /// 回転
        /// </summary>
        public Vector3 Rot = new Vector3(0.0f);

        /// <summary>
        /// 拡縮
        /// </summary>
        public Vector3 Scale = new Vector3(1.0f);


        #region 2D IF        
        public Vector2 Pos2D
        {
            set
            {
                this.Pos.X = value.X;
                this.Pos.Y = value.Y;
            }
        }
        public float RotZ
        {
            set
            {
                this.Rot.Z = value;
            }
        }
        public Vector2 Scale2D
        {
            set
            {
                this.Scale.X = value.X;
                this.Scale.Y = value.Y;
            }
        }
        #endregion
    }


    /// <summary>
    /// 速度情報データ
    /// </summary>
    public class SpeedSet : Vector3Set
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="f"></param>
        public SpeedSet(float f = 0.0f) : base(f)
        {
        }
        /// <summary>
        /// 速度倍率情報・・・こちらはフレームというよりはよりゲーム的な利用を想定しています
        /// </summary>
        public float Rate = 1.0f;

        /// <summary>
        /// 速度レート
        /// </summary>
        public float ScaleRate = 0.0f;

        /// <summary>
        /// 色
        /// </summary>
        public Vector4 Color = new Vector4();


        /// <summary>
        /// Rateを適応する
        /// </summary>
        /// <param name="proc_rate"></param>
        public void ApplyRate(float proc_rate)
        {
            this.Pos *= proc_rate;
            this.Rot *= proc_rate;
            this.Scale *= proc_rate;
            this.ScaleRate *= proc_rate;
            this.Color *= proc_rate;
        }
    }


    /// <summary>
    /// 位置、回転、拡縮セット
    /// </summary>
    public class TransposeSet : Vector3Set
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TransposeSet()
        {
        }
        public TransposeSet(float f)
        {
            this.Pos = new Vector3(f);
            this.Rot = new Vector3(f);
            this.Scale = new Vector3(f);
        }

        /// <summary>
        /// これの属する世界
        /// </summary>
        public int WorldID = 0;

        

        /// <summary>
        /// 拡大倍率
        /// </summary>
        public float ScaleRate = 1.0f;

        /// <summary>
        /// 回転のみの行列を作成
        /// </summary>
        /// <returns></returns>
        public Matrix CreateTransposeRotationMat()
        {


            Matrix rxm = Matrix.RotationZ(this.Rot.X);
            Matrix rym = Matrix.RotationZ(this.Rot.Y);
            Matrix rzm = Matrix.RotationZ(this.Rot.Z);
            Matrix ans = rzm * rym * rxm;

            return ans;
        }


        /// <summary>
        /// 拡縮回転のみの行列を作成
        /// </summary>
        /// <returns></returns>
        public Matrix CreateTransposeSizeRotationMat()
        {

            Matrix scm = Matrix.Scaling(this.Scale);
            Matrix wmrate = Matrix.Scaling(this.ScaleRate);

            Matrix rxm = Matrix.RotationZ(this.Rot.X);
            Matrix rym = Matrix.RotationZ(this.Rot.Y);
            Matrix rzm = Matrix.RotationZ(this.Rot.Z);
            Matrix ans = scm * wmrate * rzm * rym * rxm;

            return ans;
        }


        /// <summary>
        /// 現在のデータに即した物体の作成
        /// </summary>
        /// <returns></returns>
        public Matrix CreateTransposeMat()
        {
            WorldData wdata = WorldManager.Mana.Get(this.WorldID);

            Matrix tm = Matrix.Translation(this.Pos);
            Matrix wm = Matrix.Scaling(this.Scale);
            Matrix wmrate = Matrix.Scaling(this.ScaleRate);
            Matrix rmat = this.CreateTransposeRotationMat();
            Matrix ans = wm * wmrate * rmat * tm * wdata.CamProjectionMat;
            ans.Transpose();

            return ans;
        }


        /// <summary>
        /// 現在のデータに即した物体の作成
        /// </summary>
        /// <returns></returns>
        public Matrix CreateTransposeMat(Vector3 scalerate)
        {
            WorldData wdata = WorldManager.Mana.Get(this.WorldID);

            Matrix tm = Matrix.Translation(this.Pos);
            Matrix wm = Matrix.Scaling(this.Scale);
            Matrix wmrate = Matrix.Scaling(scalerate);
            Matrix rmat = this.CreateTransposeRotationMat();
            Matrix ans = wm * wmrate * rmat * tm * wdata.CamProjectionMat;
            ans.Transpose();

            return ans;
        }

    }


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
        /// 使用するTextureID
        /// </summary>
        public int TextureID = -1;
        /// <summary>
        /// 使用するシェーダーID
        /// </summary>
        public int ShaderID = -1;

        /// <summary>
        /// これの色
        /// </summary>
        public Vector4 Color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
    }

}
