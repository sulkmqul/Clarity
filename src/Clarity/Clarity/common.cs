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

        /// <summary>
        /// CurrentRenderTarget
        /// </summary>
        public SharpDX.Direct2D1.RenderTarget Crt = null;

    }



    /// <summary>
    /// アニメ終了Delegate
    /// </summary>
    /// <param name="aid"></param>
    public delegate void EndAnimeDelegate(int aid);

    



    


    /// <summary>
    /// 速度情報データ
    /// </summary>
    public class SpeedSet : VectorSet
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
            this.Pos3D *= proc_rate;
            this.Rot *= proc_rate;
            this.Scale3D *= proc_rate;
            this.ScaleRate *= proc_rate;
            this.Color *= proc_rate;
        }

        public void ApplyRate(float proc_rate, out SpeedSet oset)
        {
            oset = new SpeedSet();
            oset.Pos3D = this.Pos3D;
            oset.Rot = this.Rot;
            oset.Scale3D = this.Scale3D;
            oset.ScaleRate = this.ScaleRate;
            oset.Color = this.Color;
            oset.ApplyRate(proc_rate);
        }
    }


    /// <summary>
    /// 位置、回転、拡縮セット
    /// </summary>
    public class TransposeSet : VectorSet
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TransposeSet()
        {
        }
        public TransposeSet(float f)
        {
            this.Pos3D = new Vector3(f);
            this.Rot = new Vector3(f);
            this.Scale3D = new Vector3(f);
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

            Matrix scm = Matrix.Scaling(this.Scale3D);
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

            Matrix tm = Matrix.Translation(this.Pos3D);
            Matrix wm = Matrix.Scaling(this.Scale3D);
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

            Matrix tm = Matrix.Translation(this.Pos3D);
            Matrix wm = Matrix.Scaling(this.Scale3D);
            Matrix wmrate = Matrix.Scaling(scalerate);
            Matrix rmat = this.CreateTransposeRotationMat();
            Matrix ans = wm * wmrate * rmat * tm * wdata.CamProjectionMat;
            ans.Transpose();

            return ans;
        }



        /// <summary>
        /// このデータの境界線情報を作成する
        /// </summary>
        /// <returns></returns>
        public RectangleF CreateBolderRect2D()
        {
            float hl = this.Scale3D.X * 0.5f;
            float ht = this.Scale3D.Y * 0.5f;

            RectangleF ans = new RectangleF(this.Pos3D.X - hl, this.Pos3D.Y - ht, this.Scale3D.X, this.Scale3D.Y);
            
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
        public Vector4 Color = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);

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

}
