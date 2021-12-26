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

        TestScrollObject TS = null;
        /// <summary>
        /// 初期化関数
        /// </summary>
        public override void Init(ClarityInitParam pdata)
        {

            //テスト
            ClarityEngine.TestSpaceInit();

            //追加Shaderの読み込み
            ClarityEngine.ClearShader();
            ClarityEngine.AddShader<ShaderDataScroll>("shader/shlist.txt");



            this.TData = new TestObject();
            //ClarityEngine.AddElement(this.TData);


            TestEnemy te = new TestEnemy();
            //ClarityEngine.AddElement(te);

            TestScrollObject ts = new TestScrollObject();
            ClarityEngine.AddElement(ts);
            //this.TS = ts;


            TextObject t = new TextObject("abc");
            t.Pos2D = new Vector2(200, 200);
            ClarityEngine.AddElement(t);


            ClarityEngine.SetClearColor(new Color4(0.5f));


        }

        /// <summary>
        /// 周回実行関数
        /// </summary>
        public override void CyclingProc(ClarityCyclingProcParam pdata)
        {
            //this.TData.FrameSpeed.Pos2D = new Vector2(0.0f, 100.0f);
            //System.Threading.Thread.Sleep(200);

            if (ClarityEngine.TestKey(GameKey.Up))
            {
                this.TS.OffsetY += 0.01f;
            }
            if (ClarityEngine.TestKey(GameKey.Down))
            {
                this.TS.OffsetY -= 0.01f;
            }


            if (ClarityEngine.TestKey(GameKey.Button1))
            {
                this.TS.Change(101, 102);
            }
            if (ClarityEngine.TestKey(GameKey.Button2))
            {
                this.TS.Change(101, 100);
            }
        }

        /// <summary>
        /// 解放
        /// </summary>
        public override void Dispose()
        {   
        }


    }
}
