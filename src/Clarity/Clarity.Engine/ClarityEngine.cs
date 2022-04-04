﻿using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Element;
using Clarity.Engine.Element;

namespace Clarity.Engine
{

    class SystemStructureID
    {
        public const long System = -100;
        public const long User = -99;
        public const long Cleanup = -98;
    }
    


    internal class ClarityEngineData
    {
        /// <summary>
        /// システム表示者
        /// </summary>
        public TextObject SystemText = null;

        /// <summary>
        /// エンジン構造
        /// </summary>
        public EngineStructureManager SystemStructure = null;

        /// <summary>
        /// 提供構造
        /// </summary>
        public EngineStructureManager UserStructure = null;
    }


    /// <summary>
    /// ClarityEngine
    /// </summary>
    public partial class ClarityEngine
    {        

        /// <summary>
        /// ClarityEngine実体
        /// </summary>
        private static ClarityEngine Engine = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ClarityEngineコア処理
        /// </summary>
        internal Core.ClarityCore Core = null;

        /// <summary>
        /// エンジン設定
        /// </summary>
        internal ClaritySetting _EngineSetting = null;
        internal static ClaritySetting EngineSetting
        {
            get
            {
                return ClarityEngine.Engine._EngineSetting;
            }
        }

        /// <summary>
        /// 全体で使用するEngineデータ
        /// </summary>
        internal ClarityEngineData EngineData = null;

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
                //ClarityEngineの複数存在を許さない
                if (ClarityEngine.Engine != null)
                {
                    throw new Exception("ClarityEngine already exists!!");
                }
                ClarityEngine.Engine = new ClarityEngine();


                //初期化
                ClarityEngine.Engine.InitEngine(con, cesfilepath);

                
                //試験、そのうち消す
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
            if (ClarityEngine.Engine == null)
            {
                throw new Exception("ClarityEngine initialize");
            }

            //エンジンの実行
            ClarityEngine.Engine.RunEngine(cep);


            //ログの終了
            ClarityLog.Release();
        }

        /// <summary>
        /// システムテキストの設定(内部用)
        /// </summary>
        /// <param name="s"></param>
        /// <param name="line"></param>
        internal static void SetSystemTextForEngine(string s, int line = 0)
        {
            ClarityEngine.Engine.EngineData.SystemText.SetText(s, line);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// エンジンの初期化
        /// </summary>
        /// <param name="con"></param>
        /// <param name="cesfilepath"></param>
        private void InitEngine(Control con, string cesfilepath = "")
        {
            //エンジン設定の読み込み
            this._EngineSetting = new ClaritySetting();
            ClarityEngine.EngineSetting.Read(cesfilepath);

            //ログの初期化
            {
                EClarityLogLevel lev = ClarityEngine.EngineSetting.GetEnum<EClarityLogLevel>("Log.Level", EClarityLogLevel.None);
                EClarityLogMode lmode = ClarityEngine.EngineSetting.GetEnum<EClarityLogMode>("Log.Mode", EClarityLogMode.Console);
                string logpath = ClarityEngine.EngineSetting.GetString("Log.OutputPath", ".");
                string logname = ClarityEngine.EngineSetting.GetString("Log.FileName", "cl.log");
                ClarityLog.Init(lev, lmode, logpath, logname);
            }

            //エンジンの初期化
            DLL.Winmm.timeBeginPeriod(1);
            Util.RandomMaker.Init();

            //画面設定
            Vector2 vec = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize", new Vector2(640, 480));
            con.ClientSize = new System.Drawing.Size((int)vec.X, (int)vec.Y);


            //データの作成
            this.EngineData = new ClarityEngineData();

            //作成
            this.Core = new Core.ClarityCore();
            this.Core.Init(con);

            //システムテキストの初期化
            {
                string fontname = ClarityEngine.EngineSetting.GetString("Debug.SystemText.Font", "Arial");
                float fontsize = ClarityEngine.EngineSetting.GetFloat("Debug.SystemText.FontSize", 10.0f);
                TextObject stext = new Element.TextObject("", 0, fontname, fontsize);
                stext.Enabled = ClarityEngine.EngineSetting.GetBool("Debug.SystemText.Enabled", false);
                stext.TransSet.Pos2D = ClarityEngine.EngineSetting.GetVec2("Debug.SystemText.Pos", new Vector2(0.0f));
                this.EngineData.SystemText = stext;

                //SwapChain描画命令
                this.Core.AddSwapChainElement(this.EngineData.SystemText);
            }

            //システムElement構造の作成
            this.CreateSystemStructure();

        }


        /// <summary>
        /// システムElememt構造の作成
        /// </summary>
        /// <remarks>ClarityEngineに適したElement構造を作成する</remarks>
        private void CreateSystemStructure()
        {
            this.EngineData.SystemStructure = new EngineStructureManager();

            //ノードを定義
            (string code, long oid)[] datavec = {
                ("System", SystemStructureID.System),
                ("User", SystemStructureID.User),
                ("Clean", SystemStructureID.Cleanup)
            };

            //管理へ追加
            foreach (var data in datavec)
            {
                ClarityStructure st = new ClarityStructure(data.code, data.oid);
                this.EngineData.SystemStructure.AddManage(st);
                ElementManager.AddRequest(st);
            }

            
        }


        /// <summary>
        /// エンジンの実行
        /// </summary>
        /// <param name="cep">追加動作</param>
        private void RunEngine(ClarityEnginePlugin cep)
        {
            this.Core.StartClarity(cep);

            this.Core.Dispose();
            this.Core = null;
        }

    }
}
