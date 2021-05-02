using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Element;
using Clarity.Element.Collider;
using SharpDX;

namespace ClarityIntensity
{
    public class TestObject :  ClarityObject
    {
        public TestObject() : base(0)
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitElement()
        {
            this.TextureAnimationEnabled = true;            
            this.AnimeID = 3;
            this.ShaderID = Clarity.ClarityDataIndex.Shader_TextureAnime;
            this.ObjectID = Clarity.ClarityDataIndex.Vertex_Display;

            this.TransSet.Scale = new SharpDX.Vector3(128.0f, 192.0f, 1.0f);
            this.TransSet.ScaleRate = 1.0f;

            //当たり判定設定
            {
                this.ColInfo = new Clarity.Element.Collider.ColliderInfo(this);

                this.ColInfo.ColType = 1;
                this.ColInfo.TargetColType = 4;
                
                this.ColInfo.SrcColliderList.Add(new ColliderCircle(new Vector3(0.0f, 50.0f, 0.0f), 40.0f) { ColiderTransposeMode = EColiderTransposeMode.ALL } );
            }

        }


        /// <summary>
        /// 処理
        /// </summary>
        protected override void ProcElement()
        {
            float speed = 200.0f;
            float rspeed = 2.0f;
            if (ClarityEngine.TestKey(GameKey.Left))
            {
                this.FrameSpeed.Pos.X = -speed;
            }
            if (ClarityEngine.TestKey(GameKey.Right))
            {
                this.FrameSpeed.Pos.X = speed;
            }

            if (ClarityEngine.TestKey(GameKey.Up))
            {
                this.FrameSpeed.Pos.Y = speed;
            }
            if (ClarityEngine.TestKey(GameKey.Down))
            {
                this.FrameSpeed.Pos.Y = -speed;
            }


            if (ClarityEngine.TestKey(GameKey.Button1))
            {
                this.FrameSpeed.RotZ = rspeed;
            }
            if (ClarityEngine.TestKey(GameKey.Button2))
            {
                this.FrameSpeed.RotZ = -rspeed;
            }


            if (ClarityEngine.TestKey(GameKey.Button3))
            {
                this.FrameSpeed.ScaleRate = rspeed;
            }
            if (ClarityEngine.TestKey(GameKey.Button4))
            {
                this.FrameSpeed.ScaleRate = -rspeed;
            }
        }
        
    }



    class TestEnemy : ClarityObject
    {
        public TestEnemy() : base(1)
        {

        }
        protected override void InitElement()
        {   
            this.ShaderID = Clarity.ClarityDataIndex.Shader_NoTexture;
            this.Color = new Vector4(0.0f, 1.0f, 1.0f, 1.0f);
            this.ObjectID = Clarity.ClarityDataIndex.Vertex_Display;

            this.TransSet.Pos = new Vector3(100.0f, 200.0f, 0.0f);

            this.TransSet.Scale = new SharpDX.Vector3(192.0f, 192.0f, 1.0f);
            this.TransSet.ScaleRate = 1.0f;

            //当たり判定設定
            {
                this.ColInfo = new Clarity.Element.Collider.ColliderInfo(this);

                this.ColInfo.ColType = 4;
                this.ColInfo.TargetColType = 1;

                this.ColInfo.SrcColliderList.Add(new ColliderCircle(new Vector3(0.0f, 0.0f, 0.0f), 80.0f) { ColiderTransposeMode = EColiderTransposeMode.Translation | EColiderTransposeMode.Scaling });
            }
        }
    }
}
