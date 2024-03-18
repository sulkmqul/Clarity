using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;


namespace Clarity
{
    /// <summary>
    /// 位置、回転、拡縮のセット
    /// </summary>
    /// <remarks>
    /// PosとScaleは特殊処理。posはvector4～2 scaleはvector3～2と複数の値を持ち、同一であることを担保している
    /// これはVetor4でもった場合、2Dゲームだと毎回newが発生して死ぬほど重いのでは？と思ったから
    /// 一応マスタは一番次元が高い奴ってことにしてはいますが
    /// </remarks>
    public class VectorSet
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VectorSet()
        {
        }
        public VectorSet(float f)
        {
            this.Pos3D = new Vector3(f);
            this._Rot = new Vector3(f);
            this.Scale3D = new Vector3(f);
        }


        private Vector4 _Pos = new Vector4(0.0f, 0.0f, 0.0f, 1.0f);
        private Vector3 _Pos3D = new Vector3(0.0f);
        private Vector2 _Pos2D = new Vector2(0.0f);

        private Vector3 _Rot = new Vector3(0.0f);

        private Vector3 _Scale3D = new Vector3(1.0f);
        private Vector2 _Scale2D = new Vector2(0.0f);

        public Vector4 Pos
        {
            get
            {
                return this._Pos;
            }
            set
            {
                this.PosX = value.X;
                this.PosY = value.Y;
                this.PosZ = value.Z;
                this.PosW = value.W;

            }
        }


        /// <summary>
        /// 位置3D
        /// </summary>
        public Vector3 Pos3D
        {
            get
            {
                return this._Pos3D;
            }
            set
            {
                this.PosX = value.X;
                this.PosY = value.Y;
                this.PosZ = value.Z;
            }
        }

        /// <summary>
        /// 拡縮3D
        /// </summary>
        public Vector3 Scale3D
        {
            get
            {
                return this._Scale3D;
            }
            set
            {
                this._Scale3D.X = this._Scale2D.X = value.X;
                this._Scale3D.Y = this._Scale2D.Y = value.Y;
                this._Scale3D.Z = value.Z;
            }
        }

        /// <summary>
        /// 回転
        /// </summary>
        public Vector3 Rot
        {
            get
            {
                return this._Rot;
            }
            set
            {
                this._Rot = value;
            }
        }

        public Vector2 Pos2D
        {
            get
            {
                return this._Pos2D;
            }
            set
            {
                this.PosX = value.X;
                this.PosY = value.Y;
            }

        }
        public float RotZ
        {
            get
            {
                return this._Rot.Z;
            }
            set
            {
                this._Rot.Z = value;
            }
        }

        public Vector2 Scale2D
        {
            get
            {
                return this._Scale2D;
            }
            set
            {
                this._Scale3D.X = this._Scale2D.X = value.X;
                this._Scale3D.Y = this._Scale2D.Y = value.Y;
            }
        }


        /// <summary>
        /// PosX値
        /// </summary>
        public float PosX
        {
            get
            {
                return this._Pos.X;
            }
            set
            {
                this._Pos.X = this._Pos3D.X = this._Pos2D.X = value;
            }

        }
        /// <summary>
        /// PosY値
        /// </summary>
        public float PosY
        {
            get
            {
                return this._Pos.Y;
            }
            set
            {
                this._Pos.Y = this._Pos3D.Y = this._Pos2D.Y = value;
            }

        }

        /// <summary>
        /// PosZ値
        /// </summary>
        public float PosZ
        {
            get
            {
                return this._Pos.Z;
            }
            set
            {
                this._Pos.Z = this._Pos3D.Z = value;
            }

        }
        /// <summary>
        /// PosW値
        /// </summary>
        public float PosW
        {
            get
            {
                return this._Pos.W;
            }
            set
            {
                this._Pos.W = value;
            }

        }

        /// <summary>
        /// ScaleX値
        /// </summary>
        public float ScaleX
        {
            get
            {
                return this._Scale3D.X;
            }
            set
            {
                this._Scale3D.X = this._Scale2D.X = value;                
            }
        }
        /// <summary>
        /// ScaleY値
        /// </summary>
        public float ScaleY
        {
            get
            {
                return this._Scale3D.Y;
            }
            set
            {
                this._Scale3D.Y = this._Scale2D.Y = value;
            }
        }

        /// <summary>
        /// ScaleZ値
        /// </summary>
        public float ScaleZ
        {
            get
            {
                return this._Scale3D.Z;
            }
            set
            {
                this._Scale3D.Z = value;
            }
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
        /// ScaleRateを加味した実サイズ3D
        /// </summary>
        public Vector3 Size3D
        {
            get
            {
                return this.Scale3D * this.ScaleRate;
            }
        }

        /// <summary>
        /// ScaleRateを加味した実サイズ2D
        /// </summary>
        public Vector2 Size2D
        {
            get
            {
                return this.Scale2D * this.ScaleRate;
            }
        }


        /// <summary>
        /// 回転のみの行列を作成
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 CreateTransposeRotationMat()
        {
            Matrix4x4 rxm = Matrix4x4.CreateRotationX(this.Rot.X);
            Matrix4x4 rym = Matrix4x4.CreateRotationY(this.Rot.Y);
            Matrix4x4 rzm = Matrix4x4.CreateRotationZ(this.Rot.Z);
            Matrix4x4 ans = rzm * rym * rxm;

            return ans;
        }


        /// <summary>
        /// 拡縮回転のみの行列を作成
        /// </summary>
        /// <returns></returns>
        public Matrix4x4 CreateTransposeSizeRotationMat()
        {

            Matrix4x4 scm = Matrix4x4.CreateScale(this.Scale3D);
            Matrix4x4 wmrate = Matrix4x4.CreateScale(this.ScaleRate);

            Matrix4x4 rm = this.CreateTransposeRotationMat();
            Matrix4x4 ans = scm * wmrate * rm;

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
        /// 速度倍率情報
        /// </summary>
        public float Rate = 1.0f;

        /// <summary>
        /// 拡縮変更速度
        /// </summary>
        public float ScaleRate = 0.0f;

        /// <summary>
        /// 色変更速度
        /// </summary>
        public Vector4 Color = new Vector4();


    }
}
