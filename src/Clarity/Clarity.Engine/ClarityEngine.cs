using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Engine.Element;

namespace Clarity.Engine
{
    /// <summary>
    /// システム構造管理ID
    /// </summary>
    enum ESystemStructureID : long
    {
        System = -100,
        User = -99,
        Cleanup = -98,
    }
    


    internal class ClarityEngineData
    {
        /// <summary>
        /// システム表示者
        /// </summary>
        public TextObject SystemText;

        /// <summary>
        /// エンジン構造
        /// </summary>
        public EngineSystemStructureManager SystemStructure = new EngineSystemStructureManager();

        /// <summary>
        /// 提供構造管理
        /// </summary>
        public EngineStructureManager UserStructure = new EngineStructureManager();

    }


    /// <summary>
    /// ClarityEngine
    /// </summary>
    public partial class ClarityEngine
    {
        /// <summary>
        /// ClarityEngine実体
        /// </summary>
        private static ClarityEngine? Engine = null;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ClarityEngineコア処理
        /// </summary>
        internal Core.ClarityCore Core = new Clarity.Engine.Core.ClarityCore();

        /// <summary>
        /// エンジン設定
        /// </summary>
        internal ClaritySetting _EngineSetting = new ClaritySetting();
        internal static ClaritySetting EngineSetting
        {
            get
            {
                if (ClarityEngine.Engine == null)
                {
                    throw new InvalidOperationException("ClarityEngine is not Initialized.");
                }
                return ClarityEngine.Engine._EngineSetting;
            }
        }

        /// <summary>
        /// 全体で使用するEngineデータ
        /// </summary>
        internal ClarityEngineData EngineData = new ClarityEngineData();

        /// <summary>
        /// 管理管理コントロール
        /// </summary>
        private Control Con;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// エンジン初期化可否
        /// </summary>
        public static bool IsEngineInit
        {
            get
            {
                if (ClarityEngine.Engine == null)
                {
                    return false;
                }
                return true;
            }
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Clarityエンジンの初期化
        /// </summary>
        /// <param name="con">描画コントロール情報</param>
        /// <param name="cesfilepath">エンジン設定ファイルパス</param>
        public static void Init(Control con, string? cesfilepath = null)
        {
            try
            {
                //ClarityEngineの複数存在を許さない
                if (ClarityEngine.Engine != null)
                {
                    throw new Exception("ClarityEngine already exists!!");
                }

                //エンジンの初期化
                ClarityEngine.Engine = new ClarityEngine();                
                ClarityEngine.Engine.InitEngine(con, cesfilepath);

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
            ClarityEngine.Engine?.EngineData.SystemText.SetText(s, line);
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// エンジンの初期化
        /// </summary>
        /// <param name="con">初期化コントロール</param>
        /// <param name="cesfilepath">エンジン設定ファイル名</param>
        private void InitEngine(Control con, string? cesfilepath = null)
        {
            //管理コントロールの設定
            this.Con = con;

            //エンジン設定の読み込み
            this._EngineSetting = new ClaritySetting();
            if (cesfilepath != null)
            {
                ClarityEngine.EngineSetting.Read(cesfilepath);
            }

            //エンジン基底設定
            this.SetEngineDefault();
            //追加設定
            this.SetEngineExtraSetting();

            //ログの初期化
            {
                EClarityLogLevel lev = ClarityEngine.EngineSetting.GetEnum<EClarityLogLevel>("Log.Level");
                EClarityLogMode lmode = ClarityEngine.EngineSetting.GetEnum<EClarityLogMode>("Log.Mode");
                string logpath = ClarityEngine.EngineSetting.GetString("Log.OutputPath");
                string logname = ClarityEngine.EngineSetting.GetString("Log.FileName");
                ClarityLog.Init(lev, lmode, logpath, logname);
            }

            //エンジン基礎の初期化
            DLL.Winmm.timeBeginPeriod(1);
            Util.RandomMaker.Init();

            //画面設定
            Vector2 vec = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize");
            con.ClientSize = new System.Drawing.Size((int)vec.X, (int)vec.Y);


            //データの作成
            this.EngineData = new ClarityEngineData();

            //作成
            this.Core = new Core.ClarityCore();
            this.Core.Init(con);

            //システムテキストの作成
            {
                string fontname = ClarityEngine.EngineSetting.GetString("Debug.SystemText.Font");
                float fontsize = ClarityEngine.EngineSetting.GetFloat("Debug.SystemText.FontSize");

                TextObject stext = new Element.TextObject("", 0, fontname, fontsize);
                stext.Enabled = ClarityEngine.EngineSetting.GetBool("Debug.SystemText.Enabled");
                stext.TransSet.Pos2D = ClarityEngine.EngineSetting.GetVec2("Debug.SystemText.Pos");
                this.EngineData.SystemText = stext;

                //SwapChain描画命令
                this.Core.AddSwapChainElement(this.EngineData.SystemText);
            }

            //システムElement構造の作成
            this.CreateSystemStructure();

        }


        /// <summary>
        /// デフォルト設定を適応する。
        /// </summary>
        private void SetEngineDefault()
        {
            //必須のデフォルト設定
            //随時追加すること
            (string, object)[] setvec =  {
                ("Log.Level", EClarityLogLevel.None.ToString()),
                ("Log.Mode", EClarityLogMode.Console.ToString()),
                ("Log.OutputPath", ""),
                ("Log.FileName", ""),

                ("DisplayViewSize", new Vector2(-1)),
                ("RenderingViewSize", new Vector2(-1)),

                ("VertexShaderVersion", "vs_5_0"),
                ("PixelShaderVersion", "ps_5_0"),
                ("RenderingThreadCount", 1),

                ("Debug.Enabled", false),
                ("Debug.SystemText.Enabled", false),
                ("Debug.SystemText.Pos", new Vector2(10.0f)),
                ("Debug.SystemText.Font", "Arial"),
                ("Debug.SystemText.FontSize", 20.0f),
                ("Debug.Collider.Visible", false),
                ("Debug.Collider.DefaultColor", new Vector3(1.0f, 0.0f, 0.0f)),
                ("Debug.Collider.ContactColor", new Vector3(1.0f, 1.0f, 0.0f)),
            };

            foreach (var data in setvec)
            {
                //登録済みかを確認
                bool exf = this._EngineSetting.CheckExists(data.Item1);
                if (exf == false)
                {
                    //登録してないならデフォルト値を設定
                    this._EngineSetting.SetData(data.Item1, data.Item2);
                }
            }

        }


        /// <summary>
        /// 設定ファイルから必要なエンジン設定を生成、上書きする
        /// </summary>
        private void SetEngineExtraSetting()
        {
            string prefix = "CE";
            //DisplayViewSizeの設定
            {
                bool dvf = false;
                Vector2 dsize = this._EngineSetting.GetVec2("DisplayViewSize");
                if (dsize.X < 0 || dsize.Y < 0)
                {
                    dsize = new Vector2(this.Con.Width, this.Con.Height);
                    dvf = true;
                }
                this._EngineSetting.SetData("DisplayViewSize", dsize);
                this._EngineSetting.SetData($"{prefix}.FixedDisplayViewSize", dvf);
            }
            //RenderingViewSizeの設定
            {
                bool rvf = false;
                Vector2 rsize = this._EngineSetting.GetVec2("RenderingViewSize");
                if (rsize.X < 0 || rsize.Y < 0)
                {
                    rsize = new Vector2(this.Con.Width, this.Con.Height);
                    rvf = true;
                }
                this._EngineSetting.SetData("RenderingViewSize", rsize);
                this._EngineSetting.SetData($"{prefix}.FixedRenderingViewSize", rvf);
            }
        }

        /// <summary>
        /// システムElememt構造の作成
        /// </summary>
        /// <remarks>ClarityEngineに適したElement構造を作成する</remarks>
        private void CreateSystemStructure()
        {
            this.EngineData.SystemStructure = new EngineSystemStructureManager();

            //ノードを定義
            (string code, ESystemStructureID oid)[] datavec = {
                ("System", ESystemStructureID.System),
                ("User", ESystemStructureID.User),
                ("Clean", ESystemStructureID.Cleanup)
            };

            //管理へ追加
            foreach (var data in datavec)
            {
                ClarityStructure st = new ClarityStructure(data.code, (long)data.oid);
                this.EngineData.SystemStructure.AddManage(st);
                ElementManager.AddRequest(st);
            }

            //------------------------------------------------------------------------------
            //必要なControllerの作成

            

        }

        /// <summary>
        /// エンジンの実行
        /// </summary>
        /// <param name="cep">追加動作</param>        
        private void RunEngine(ClarityEnginePlugin cep)
        {
            this.Core.StartClarity(cep);
            this.Core.Dispose();
            
        }

        
    }
}
