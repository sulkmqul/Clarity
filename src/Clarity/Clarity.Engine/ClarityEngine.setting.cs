using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine
{
    /// <summary>
    /// Engine設定のキー
    /// </summary>
    public enum EClarityEngineSettingKeys
    {
        [StringAttribute("FileVersion")]
        FileVersion,        
        [StringAttribute("Log.Level")]
        Log_Level,
        [StringAttribute("Log.Mode")]
        Log_Mode,
        [StringAttribute("Log.OutputPath")]
        Log_OutputPath,
        [StringAttribute("Log.FileName")]
        Log_FileName,        
        [StringAttribute("ViewDisplay.FixedDisplayFlag")]
        ViewDisplay_FixedDisplayFlag,
        [StringAttribute("ViewDisplay.DisplayViewSize")]
        ViewDisplay_DisplayViewSize,
        [StringAttribute("ViewDisplay.RenderingViewSize")]
        ViewDisplay_RenderingViewSize,
        [StringAttribute("ViewDisplay.ClearColor")]
        ViewDisplay_ClearColor,
        [StringAttribute("VertexShaderVersion")]
        VertexShaderVersion,
        [StringAttribute("PixelShaderVersion")]
        PixelShaderVersion,
        [StringAttribute("RenderingThreadCount")]
        RenderingThreadCount,

        [StringAttribute("Debug.Enabled")]
        Debug_Enabled,

        [StringAttribute("Debug.SystemText.Enabled")]
        Debug_SystemText_Enabled,
        [StringAttribute("Debug.SystemText.Pos")]
        Debug_SystemText_Pos,
        [StringAttribute("Debug.SystemText.Font")]
        Debug_SystemText_Font,
        [StringAttribute("Debug.SystemText.FontSize")]
        Debug_SystemText_FontSize,


        [StringAttribute("Debug.Collider.Visible")]
        Debug_Collider_Visible,
        [StringAttribute("Debug.Collider.DefaultColor")]
        Debug_Collider_DefaultColor,
        [StringAttribute("Debug.Collider.ContactColor")]
        Debug_Collider_ContactColor,
    }


    public partial class ClarityEngine
    {
        public static class EngineSetting
        {
            /// <summary>
            /// エンジンん設定の上書き
            /// </summary>
            /// <param name="key">設定key</param>
            /// <param name="value">設定値</param>
            /// <exception cref="Exception"></exception>
            public static void SetEngineParam(EClarityEngineSettingKeys key, object value)
            {
                string? s = key.GetAttributeString();
                if (s == null)
                {
                    throw new Exception("iligal EClarityEngineSettingKeys");
                }
                ClarityEngine.Engine._EngineSetting.SetData(s, value);
            }


            //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

            /// <summary>
            /// boolの取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static bool GetBool(EClarityEngineSettingKeys code, bool def = false)
            {
                return ClarityEngine.Engine._EngineSetting.GetBool(code.GetAttributeString() ?? "", def);
            }

            /// <summary>
            /// Integer設定の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static int GetInteger(EClarityEngineSettingKeys code, int def = 0)
            {
                return ClarityEngine.Engine._EngineSetting.GetInteger(code.GetAttributeString() ?? "", def);
            }

            /// <summary>
            /// Float設定の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static float GetFloat(EClarityEngineSettingKeys code, float def = 0.0f)
            {
                return ClarityEngine.Engine._EngineSetting.GetFloat(code.GetAttributeString() ?? "", def);
            }

            /// <summary>
            /// String設定の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static string GetString(EClarityEngineSettingKeys code, string def = "")
            {
                return ClarityEngine.Engine._EngineSetting.GetString(code.GetAttributeString() ?? "", def);
            }


            /// <summary>
            /// Vector2設定の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static Vector2 GetVec2(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetVec2(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Vector3設定の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static Vector3 GetVec3(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetVec3(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Vector4設定の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static Vector4 GetVec4(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetVec4(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Integer設定配列の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static int[] GetIntegerArray(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetIntegerArray(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Float設定配列の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static float[] GetFloatArray(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetFloatArray(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// String設定配列の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static string[] GetStringArray(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetStringArray(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Vector2設定配列の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static Vector2[] GetVec2Array(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetVec2Array(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Vector3設定配列の取得
            /// </summary>
            /// <param name="code"></param>
            /// <returns></returns>
            public static Vector3[] GetVec3Array(EClarityEngineSettingKeys code)
            {
                return ClarityEngine.Engine._EngineSetting.GetVec3Array(code.GetAttributeString() ?? "");
            }

            /// <summary>
            /// Enum値の取得
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="id"></param>
            /// <returns></returns>
            public static T GetEnum<T>(EClarityEngineSettingKeys code) where T : Enum
            {
                return ClarityEngine.Engine._EngineSetting.GetEnum<T>(code.GetAttributeString() ?? "");
            }


        }

        
    }
}
