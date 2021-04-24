using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SharpDX;
using Clarity.Element;

namespace Clarity
{

    /// <summary>
    /// ClarityEngine初期化設定オプション
    /// </summary>
    public class EngineSetupOption
    {
        /// <summary>
        /// エンジン設定ファイル読み込みパス(空文字時、読み込みを行わずデフォルトで動作)
        /// </summary>
        public string EngineSettingFilePath = "";
        
    }

 

    /// <summary>
    /// パラメータ基底・・・共通の外だしともいう
    /// </summary>
    public class ClarityParam
    {
        /// <summary>
        /// 管理コントロール
        /// </summary>
        public Control Con = null;
    }

    /// <summary>
    /// 初期化パラメータ
    /// </summary>
    public class ClarityInitParam : ClarityParam
    {
        /// <summary>
        /// 描画領域サイズ
        /// </summary>
        public Size RenderingViewSize = new Size(800, 600);
    }

    /// <summary>
    /// 周回処理パラメータ
    /// </summary>
    public class ClarityCyclingProcParam : ClarityParam
    {
        
    }
    

    /// <summary>
    /// ClarityEngine入り口クラス
    /// これを介してClarityに関する情報に触れることができる
    /// </summary>
    public partial class ClarityEngine
    {
        /// <summary>
        /// 無効ID値
        /// </summary>
        public const int INVALID_ID = int.MinValue;

        #region メンバ変数
        /// <summary>
        /// ゲームエンジンコア
        /// </summary>
        internal static Core.ClarityCore core = null;
        /// <summary>
        /// エンジン設定
        /// </summary>
        internal static ClarityEngineSetting Setting = null;
        #endregion


        #region 試験場所。消すこと

        /// <summary>
        /// 試験場所、そのうち消すこと
        /// </summary>
        public static void TestSpaceInit()
        {
            //データ読み込み
            //Vertex.VertexManager.Mana.AddResource(1, @"F:\作業領域\Game\Stellamaris\data\Poly\V001.cpo");
            //テクスチャ
            Texture.TextureManager.Mana.AddTexture(1, @"F:\作業領域\Game\Clarity\src\ClarityIntensity\testdata\bul_000.png", new Size(4, 1));
            Texture.TextureManager.Mana.AddTexture(2, @"F:\作業領域\Game\Clarity\src\ClarityIntensity\testdata\eff_000.bmp", new Size(8, 1));
            Texture.TextureManager.Mana.AddTexture(3, @"F:\作業領域\Game\Clarity\src\ClarityIntensity\testdata\plj_000.png", new Size(8, 1));

            


            {
                /*
                //暫定テクスチャアニメファイル書き込み
                Texture.TextureAnimeFileDataRoot rdata = new Texture.TextureAnimeFileDataRoot();
                rdata.RootID = 5;
                rdata.AnimeDataList = new List<Texture.TextureAnimeFileDataElement>()
                {
                    new Texture.TextureAnimeFileDataElement(){ 
                        AnimeCode="test", 
                        Kind=Texture.ETextureAnimationKind.Loop, 
                        NextAnimeCode="", 
                        FrameData = new List<string>(){ "bul_000T,0,0,100" } 
                    }
                };
                Texture.TextureAnimeFile.WriteFile("testanime.txt", rdata);
                */
            }

            //テクスチャアニメファイルの読み込み
            Texture.TextureAnimeFactory.Mana.ReadTextureAnimeFile(@"F:\作業領域\Game\Clarity\src\ClarityIntensity\testdata\texanime.txt");





        }


        #endregion



        /// <summary>
        /// Clarityエンジン全体初期化クラス
        /// </summary>
        /// <param name="con">描画対象コントロール</param>
        /// <param name="op">初期化オプション</param>
        /// <returns>成功可否</returns>
        public static bool Init(Control con, EngineSetupOption op)
        {
            if (core != null)
            {
                throw new Exception("ClarityEngine Exists!!");
            }

            //エンジン設定の読み込み
            Setting = new ClarityEngineSetting();
            //パス文字列が設定してある、読み込みを行う
            if (op.EngineSettingFilePath.Length > 0)
            {
                ClarityEngine.Setting = File.ClarityEngineSettingFile.ReadSetting(op.EngineSettingFilePath);
            }

            //外からもアクセスできる全体関数の初期化
            //ログの作成
            ClarityLog.Init(Setting.LogLevel);
            
            //乱数ライブラリの初期化
            Util.RandomMaker.Init();

            //コアエンジン初期化
            core = new Core.ClarityCore();
            core.Init(con, op);

            //ビルドインデータの読み込み
            BuildInResource.LoadBuildInData();
            
            return true;
        }


        /// <summary>
        /// 実行開始
        /// </summary>
        /// <param name="ice">実行関数</param>
        /// <param name="op">実行オプション</param>
        public static void Run(ClarityEngineExtension ice)
        {
            //処理開始
            core.StartClarity(ice);

            //解放
            core.Dispose();
            core = null;
        }



        /// <summary>
        /// Viewのリサイズ処理
        /// </summary>
        public static void ResetView()
        {
            core.ResizeView();
        }


    }
}
