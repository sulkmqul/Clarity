using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using Vortice.Mathematics;

namespace Clarity
{
    /// <summary>
    /// 位置、回転、拡縮のセット
    /// </summary>
    /// <remarks>
    /// PosとScaleはかなり特殊なことをしています。
    /// 双方ともVector3とVector2二つの変数を保持していますが、内部的に無理やり同じ値であることを保証しています。
    /// これはなぜかというと、Vector3だけをもってVec2に変換するとき、毎回変換Newが発生し、
    /// 2Dゲームを作る上では基本的にVec2の方にアクセスするため、物凄く重いんじゃね？と思ったからです。
    /// 回転はあまり関係なさそうなのでそのままです、一応一貫性のためプロパティ化ぐらいはしますが・・・
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


        private Vector3 _Pos3D = new Vector3(0.0f);
        private Vector2 _Pos2D = new Vector2(0.0f);
        private Vector3 _Rot = new Vector3(0.0f);
        private Vector3 _Scale3D = new Vector3(1.0f);
        private Vector2 _Scale2D = new Vector2(0.0f);


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
                this._Pos3D.X = this._Pos2D.X = value.X;
                this._Pos3D.Y = this._Pos2D.Y = value.Y;
            }

        }
        public float RotZ
        {
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
                return this._Pos3D.X;
            }
            set
            {
                this._Pos3D.X = this._Pos2D.X = value;
            }

        }
        /// <summary>
        /// PosY値
        /// </summary>
        public float PosY
        {
            get
            {
                return this._Pos3D.Y;
            }
            set
            {
                this._Pos3D.Y = this._Pos2D.Y = value;
            }

        }

        /// <summary>
        /// PosZ値
        /// </summary>
        public float PosZ
        {
            get
            {
                return this._Pos3D.Z;
            }
            set
            {
                this._Pos3D.Z = value;
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
