using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.Element;
using SharpDX;

namespace ClarityIntensity
{
    /// <summary>
    /// ゲーム実行関数
    /// </summary>
    public class IntensityMain : ClarityEngineExtension
    {
        ClarityObject TData = null;

        /// <summary>
        /// 初期化関数
        /// </summary>
        public override void Init(ClarityInitParam pdata)
        {
            //テスト
            ClarityEngine.TestSpaceInit();

            //オブジェクトの登録
            /*Clarity.Element.ClarityObject data = new Clarity.Element.ClarityObject(1);
            data.VertexID = ClarityDataIndex.Vertex_Display;           

            data.ShaderID = ClarityDataIndex.Shader_OnlyAlpha;
            data.TextureID = ClarityDataIndex.Texture_Circle;
            data.Color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
            data.TransSet.WorldID = 0;
            data.TransSet.Pos = new Vector3(0.0f, -0.0f, 0.0f);
            
            data.TransSet.Scale = new Vector3(30.0f, 30.0f, 1.0f);            
            data.TextureAnimationEnabled = false;
            data.AnimeID = 1;
            ClarityEngine.AddElement(data);

            this.TData = data;*/

            this.TData = new TestObject();
            ClarityEngine.AddElement(this.TData);


            TestEnemy te = new TestEnemy();
            ClarityEngine.AddElement(te);

        }

        /// <summary>
        /// 周回実行関数
        /// </summary>
        public override void CyclingProc(ClarityCyclingProcParam pdata)
        {
            //this.TData.FrameSpeed.Pos2D = new Vector2(0.0f, 100.0f);
            //System.Threading.Thread.Sleep(200);
        }

        /// <summary>
        /// 解放
        /// </summary>
        public override void Dispose()
        {
            
        }


    }
}
