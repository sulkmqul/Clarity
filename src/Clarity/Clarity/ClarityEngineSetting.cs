using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Clarity
{

    /// <summary>
    /// エンジンデバッグ文字描画設定
    /// </summary>
    [Serializable]
    public class ClarityEngineSettingDebugSystemText
    {
        /// <summary>
        /// システム文字描画可否
        /// </summary>
        public bool RenderSystemTextFlag = false;

        /// <summary>
        /// 管理Element更新頻度
        /// </summary>
        public long ElementCountRefreshMs = 100;

        /// <summary>
        /// システム文字位置
        /// </summary>
        public Vector2 SystemTextPos = new Vector2(0.0f, 0.0f);

        /// <summary>
        /// システム文字サイズ
        /// </summary>
        public float SystemTextSize = 20.0f;

        /// <summary>
        /// システム文字色
        /// </summary>
        public SharpDX.Color SystemTextColor = SharpDX.Color.Brown;


        
    }


    /// <summary>
    /// エンジンデバッグ用設定
    /// </summary>
    [Serializable]
    public class ClarityEngineSettingDebug
    {
        /// <summary>
        /// 当たり判定描画可否
        /// </summary>
        public bool RenderColliderFlag = false;

        /// <summary>
        /// 当たり判定描画基本色
        /// </summary>
        public Vector4 ColliderDefaultColor = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);

        /// <summary>
        /// 当たり判定描画接触色
        /// </summary>
        public Vector4 ColliderContactColor = new Vector4(1.0f, 1.0f, 0.0f, 1.0f);


        /// <summary>
        /// デバッグシステム文字表示
        /// </summary>
        public ClarityEngineSettingDebugSystemText SystemText = new ClarityEngineSettingDebugSystemText();

    }


    /// <summary>
    /// エンジン設定ファイルデータ
    /// </summary>
    [Serializable]
    public class ClarityEngineSetting
    {
        /// <summary>
        /// ファイルバージョン(現状意味なし)
        /// </summary>
        public string FileVersion = "1.0.0.0";

        /// <summary>
        /// ログレベル
        /// </summary>
        public EClarityLogLevel LogLevel = EClarityLogLevel.ALL;

        /// <summary>
        /// 複数ViewPort描画（画面分割）対応可否(false=一つだけ true=複数対応する) これなにか意味があるのか？
        /// </summary>
        public bool MultiViewPort = false;

        /// <summary>
        /// 描画Viewのサイズ(画面サイズではなくRenderingTextureのサイズ)
        /// </summary>
        public Size RenderingViewSize = new Size(800, 450);

        /// <summary>
        /// 描画Viewの表示サイズ(RenderingViewSizeの実画面の描画表示サイズ)
        /// </summary>
        public Size DisplayViewSize = new Size(800, 450);

        /// <summary>
        /// 処理と描画時間の合計が時間を超えた場合、次のフレームの描画をスキップする限界時間(ms)
        /// </summary>
        public long LimitTime = Int64.MaxValue;

        /// <summary>
        /// VertexShaderバージョン
        /// </summary>
        public string VertexShaderVersioon = "vs_4_0";

        /// <summary>
        /// PixelShaderのバージョンバージョン
        /// </summary>
        public string PixelShaderVersioon = "ps_4_0";


        /// <summary>
        /// 描画スレッド数(現状意味なし)
        /// </summary>
        public int RenderingThreadCount = 1;

        /// <summary>
        /// デバッグ設定
        /// </summary>
        public ClarityEngineSettingDebug Debug = new ClarityEngineSettingDebug();



    }
}
