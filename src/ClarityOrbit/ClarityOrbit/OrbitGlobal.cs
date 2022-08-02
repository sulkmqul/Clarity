using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;

namespace ClarityOrbit
{
    /// <summary>
    /// Orbit全体クラス
    /// </summary>
    internal class OrbitGlobal : BaseClaritySingleton<OrbitGlobal>
    {
        private OrbitGlobal()
        {
        }

        public const int EVal = Clarity.Engine.ClarityEngine.INVALID_ID;


        /// <summary>
        /// ClarityEngine設定ファイルパス
        /// </summary>
        public static string ClarityEngineSettingFilePath
        {
            get
            {
                return "cs.xml";
            }
        }

        /// <summary>
        /// 構造設定ファイルパス
        /// </summary>
        public static string StructureSettingFilePath
        {
            get
            {
                return "data\\structure.xml";
            }
        }

        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public OrbitProject? Project = null;


    }
}
