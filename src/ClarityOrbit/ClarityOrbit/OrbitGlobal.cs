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


        public static string ClarityEngineSettingFilePath
        {
            get
            {
                return "cs.xml";
            }
        }
        public static string StructureSettingFilePath
        {
            get
            {
                return "structure.xml";
            }
        }

        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public OrbitProject Project = null;


    }
}
