using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Element;

namespace Clarity.Engine
{
    /// <summary>
    /// ClarityEngine
    /// </summary>
    public partial class ClarityEngine
    {
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ClarityEngineコア処理
        /// </summary>
        internal static Core.ClarityCore Core = null;

        /// <summary>
        /// エンジン設定
        /// </summary>
        internal static ClaritySetting EngineSetting = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        public static void TestSpace()
        {
            /*
            string datapath = @"F:\作業領域\Game\Clarity\src\ClarityIntensity\";
            Texture.TextureManager.Mana.AddTexture(1, datapath + "T0000.png", new System.Drawing.Size(1, 1));

            Vertex.VertexManager.Mana.AddResource(1, datapath + "vertex.cpo");

            //Buildin Shader Listの読み込み            
            {
                Shader.ShaderListFileDataRoot rdata = new Shader.ShaderListFileDataRoot();
                rdata.RootID = 1;
                rdata.ShaderList = new List<Shader.ShaderListData>();
                rdata.ShaderList.Add(new Shader.ShaderListData() { Code = "TEST", FilePath = datapath + "aabbcc.fx", VsName = "VsDefault", PsName = "PsDefault" });
                //rdata.ShaderList.Add(new Shader.ShaderListData() { Code = "TEST", FilePath = datapath + "honi.hlsl", VsName = "VSMain", PsName = "PSMain" });
                Shader.ShaderManager.Mana.CreateDefaultResource(rdata);
            }*/


            Texture.TextureManager.Mana.AddTexture(1, @"F:\作業領域\Game\Stellamaris\data\img\char\plj_000.png", new System.Drawing.Size(8, 1));

            Texture.TextureAnimeFactory.Create();
            Texture.TextureAnimeFactory.Mana.ReadTextureAnimeFile(@"F:\作業領域\Game\Clarity\src\ClarityIntensity\0mayu.cta");
            Clarity.Engine.Element.ClarityObject obj = new Element.ClarityObject(10);
            obj.TextureAnimeEnabled = true;
            obj.TransSet.WorldID = 0;
            obj.TexAnimeID = 0;
            obj.RenderSet.TextureID = 1;
            obj.RenderSet.VertexID = -100;
            obj.RenderSet.Color = new Vortice.Mathematics.Color4(1.0f, 0.0f, 0.0f, 1.0f);
            obj.RenderSet.ShaderID = -98;
            obj.TransSet.Scale2D = new Vector2(192, 256.0f) * 2.0f;
            obj.TransSet.Pos2D = new Vector2(0.0f, 0.0f);
            obj.AddProcBehavior(new ActionBehavior((x) =>
            {
                //obj.FrameSpeed.RotZ = 1.0f;
            }));
            obj.TexAnimeCont.EndAnimeEvent += (int aid) => {
                ClarityEngine.SetSystemText($"{obj.ProcTime}");
            };

            ElementManager.AddRequest(obj);


            //obj = new Element.ClarityObject(10);
            //obj.TransSet.WorldID = 0;
            //obj.RenderSet.TextureID =-100;
            //obj.RenderSet.VertexID = -100;
            //obj.RenderSet.Color = new Vortice.Mathematics.Color4(1.0f, 0.5f, 0.8f, 1.0f);
            //obj.RenderSet.ShaderID = -100;
            //obj.TransSet.Scale2D = new Vector2(100.0f, 100.0f);
            //obj.TransSet.Pos2D = new Vector2(0.0f, 0.0f);
            //ElementManager.AddRequest(obj);



            //文字の描画
            Element.TextObject  text = new Element.TextObject("あかさ\nたな");
            text.TransSet.Pos2D = new Vector2(200.0f, 100.0f);
            text.AddProcBehavior(new ActionBehavior((x) => {                
                text.FrameSpeed.PosX += 100f;
                text.Text = $"x={text.TransSet.PosX} y={text.TransSet.PosY}";
                if (text.TransSet.PosX > 500)
                {
                    text.RemoveProcBehavior(0);
                }
            }));
            text.AddProcBehavior(new ActionBehavior((x) => {
                text.FrameSpeed.PosY += 100f;
                text.Text = $"x={text.TransSet.PosX} y={text.TransSet.PosY}";
                if (text.TransSet.PosY > 500)
                {
                    text.RemoveProcBehavior(1);
                }
            }, 1));
            ElementManager.AddRequest(text);

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Clarityエンジンの初期化
        /// </summary>
        /// <param name="con">描画コントロール情報</param>
        /// <param name="cesfilepath">エンジン設定ファイルパス</param>
        public static void Init(Control con, string cesfilepath = "")
        {
            try
            {
                if (ClarityEngine.Core != null)
                {
                    throw new Exception("ClarityEngine already exists!!");
                }

                //エンジン設定の読み込み
                ClarityEngine.EngineSetting = new ClaritySetting();
                ClarityEngine.EngineSetting.Read(cesfilepath);

                //ログの初期化
                {
                    EClarityLogLevel lev = EngineSetting.GetEnum<EClarityLogLevel>("Log.Level", EClarityLogLevel.None);
                    EClarityLogMode lmode = EngineSetting.GetEnum<EClarityLogMode>("Log.Mode", EClarityLogMode.Console);
                    string logpath = EngineSetting.GetString("Log.OutputPath", ".");
                    string logname = EngineSetting.GetString("Log.FileName", "cl.log");
                    ClarityLog.Init(lev, lmode, logpath, logname);
                }

                //エンジンの初期化
                DLL.Winmm.timeBeginPeriod(1);
                Util.RandomMaker.Init();

                //画面設定
                Vector2 vec = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize", new Vector2(640, 480));
                con.ClientSize = new System.Drawing.Size((int)vec.X, (int)vec.Y);

                //作成
                ClarityEngine.Core = new Core.ClarityCore();
                ClarityEngine.Core.Init(con);

                
                //
                TestSpace();
            }
            catch (Exception ex)
            {　　　　
                throw new Exception("ClarityEngine Init", ex);
            }
        }


        /// <summary>
        /// Clarityエンジンの実行
        /// </summary>
        /// <param name="cep">追加動作</param>
        public static void Run(ClarityEnginePlugin cep)
        {
            if (ClarityEngine.Core == null)
            {
                throw new Exception("ClarityEngine initialize");
            }

            ClarityEngine.Core.StartClarity(cep);

            ClarityEngine.Core.Dispose();
            ClarityEngine.Core = null;


            //ログの終了
            ClarityLog.Release();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


    }
}
