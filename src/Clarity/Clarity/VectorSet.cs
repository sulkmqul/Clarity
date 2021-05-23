using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

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
}
