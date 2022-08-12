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


        public const int OrbitWorldID = 0;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public OrbitProject? Project = null;


        //保存情報以外のアプリ情報

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//



        /// <summary>
        /// 初期化
        /// </summary>
        public static void Init()
        {
            OrbitGlobal.Instance = new OrbitGlobal();
        }


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
        /// Shader一覧ファイル
        /// </summary>
        public static string ShaderListFile
        {
            get
            {
                return "data\\shlist.txt";
            }
        }

        /// <summary>
        /// Polygon一覧ファイル
        /// </summary>
        public static string PolyListFilePath
        {
            get
            {
                return "data\\polylist.txt";
            }
        }


        


    }
}
