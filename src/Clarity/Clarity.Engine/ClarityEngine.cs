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
        public TextObject SystemText = null;

        /// <summary>
        /// エンジン構造
        /// </summary>
        public EngineSystemStructureManager SystemStructure = null;

        /// <summary>
        /// 提供構造管理
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
            Vector2 vec = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize", new Vector2(con.Width, con.Height));
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
            this.Core = null;
        }

        /// <summary>
        /// エンジンの実行
        /// </summary>
        /// <param name="cep">追加動作</param>
        private async Task RunEngineAsync(ClarityEnginePlugin cep)
        {
            await this.Core.StartClarityAsync(cep);

            this.Core.Dispose();
            this.Core = null;
        }
    }
}
