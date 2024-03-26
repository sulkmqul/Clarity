using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.Engine.Element;
using System.Runtime.CompilerServices;
using System.IO;
using System.Xml.Linq;

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
        private static ClarityEngine? _Engine = null;

        /// <summary>
        /// エンジンへのアクセス
        /// </summary>
        private static ClarityEngine Engine
        {
            get
            {
                if(ClarityEngine._Engine == null)
                {
                    throw new InvalidOperationException("ClarityEngine is not initialized");
                }

                return ClarityEngine._Engine;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ClarityEngineコア処理
        /// </summary>
        internal Core.ClarityCore Core = new Clarity.Engine.Core.ClarityCore();

        /// <summary>
        /// エンジン設定(使用する時はEngineSettingを経由する。)
        /// </summary>
        private ClaritySetting _EngineSetting = new ClaritySetting();

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
                if (ClarityEngine._Engine == null)
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
                if (ClarityEngine._Engine != null)
                {
                    throw new Exception("ClarityEngine already exists!!");
                }

                //エンジンの初期化
                ClarityEngine._Engine = new ClarityEngine();                
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
        /// <remarks>Application.Runをした場合はこれは実行しないこと。</remarks>
        public static void Run(ClarityEnginePlugin? cep)
        {
            //エンジンの実行
            ClarityEngine.Engine.RunEngine(cep);

            //ログの終了
            ClarityLog.Release();
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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
        /// <param name="con">初期化コントロール</param>
        /// <param name="cesfilepath">エンジン設定ファイル名</param>
        private void InitEngine(Control con, string? cesfilepath = null)
        {
            //管理コントロールの設定
            this.Con = con;

            //エンジン設定の読み込み
            this.LoadEngineSetting(cesfilepath);


            //ログの初期化
            {
                EClarityLogLevel lev = ClarityEngine.EngineSetting.GetEnum<EClarityLogLevel>(EClarityEngineSettingKeys.Log_Level);
                EClarityLogMode lmode = ClarityEngine.EngineSetting.GetEnum<EClarityLogMode>(EClarityEngineSettingKeys.Log_Mode);
                string logpath = ClarityEngine.EngineSetting.GetString(EClarityEngineSettingKeys.Log_OutputPath);
                string logname = ClarityEngine.EngineSetting.GetString(EClarityEngineSettingKeys.Log_FileName);
                ClarityLog.Init(lev, lmode, logpath, logname);
            }

            //
            DLL.Winmm.timeBeginPeriod(1);
            Util.RandomMaker.Init();

            //画面設定
            //Vector2 vec = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize");
            //con.ClientSize = new System.Drawing.Size((int)vec.X, (int)vec.Y);

            //データの作成
            this.EngineData = new ClarityEngineData();

            //作成
            this.Core = new Core.ClarityCore();
            this.Core.Init(con);


            //システム表示textの作成
            this.CreateSystemText();            

            //システムElement構造の作成
            this.CreateSystemStructure();

        }

        /// <summary>
        /// エンジン設定の読み込み
        /// </summary>
        /// <param name="cefilepath"></param>
        private void LoadEngineSetting(string? cefilepath)
        {   
            this._EngineSetting = new ClaritySetting();

            //既存設定の取得
            using (MemoryStream mst = new MemoryStream(Properties.Resources.default_setting))
            {
                this._EngineSetting.Load(mst);
            }
            //ユーザー設定の読み込み
            if (cefilepath != null)
            {
                //defaultに上書きする形で追記する。
                this._EngineSetting.Load(cefilepath, true);
            }

            var list = this._EngineSetting.GetManagedKeyCode();
            using (FileStream mmm = new FileStream("settingkey.txt", FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(mmm))
                {
                    list.ForEach(x => sw.WriteLine(x.code));
                }
            }
            
        }


        /// <summary>
        /// システムテキストの作成
        /// </summary>
        private void CreateSystemText()
        {
            string fontname = ClarityEngine.EngineSetting.GetString(EClarityEngineSettingKeys.Debug_SystemText_Font);
            float fontsize = ClarityEngine.EngineSetting.GetFloat(EClarityEngineSettingKeys.Debug_SystemText_FontSize);

            TextObject stext = new Element.TextObject("", 0, fontname, fontsize);
            stext.Enabled = ClarityEngine.EngineSetting.GetBool(EClarityEngineSettingKeys.Debug_SystemText_Enabled);
            stext.TransSet.Pos2D = ClarityEngine.EngineSetting.GetVec2(EClarityEngineSettingKeys.Debug_SystemText_Pos);
            this.EngineData.SystemText = stext;

            //SwapChain描画命令
            this.Core.AddSwapChainElement(this.EngineData.SystemText);
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
        private void RunEngine(ClarityEnginePlugin? cep)
        {
            this.Core.StartClarity(cep);
            this.Core.Dispose();
            
        }

        
    }
}
