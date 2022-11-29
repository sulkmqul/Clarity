using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
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

        /// <summary>
        /// 無効値
        /// </summary>
        public const int EVal = Clarity.Engine.ClarityEngine.INVALID_ID;

        /// <summary>
        /// WorldID
        /// </summary>
        public const int OrbitWorldID = 0;

        /// <summary>
        /// 画像選択フィルター
        /// </summary>
        public static readonly string ImageFileFilter = "ビットマップ(*.bmp)|*.bmp|Jpegファイル(*.jpg)|*.jpg|Pngファイル(*.png)|*.png|全てのファイル(*.*)|*.*";

        /// <summary>
        /// 最大構造内包の縦横サイズ
        /// </summary>
        public const int MAX_STRUCT_ELEMENT_COUNT = 20;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// プロジェクト情報
        /// </summary>
        private OrbitProject? _Project = null;

        /// <summary>
        /// 操作情報
        /// </summary>
        private OrbitControlInfo _ControlInfo = new OrbitControlInfo();

        /// <summary>
        /// 全体制御管理
        /// </summary>
        private OrbitSubject _Subject = new OrbitSubject();

        /// <summary>
        /// 本体画面
        /// </summary>
        public MainForm MForm;
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public static OrbitProject? Project
        {
            get
            {
                return OrbitGlobal.Mana._Project;
            }
            set
            {
                OrbitGlobal.Mana._Project = value;
            }
        }


        /// <summary>
        /// コントロール情報
        /// </summary>
        public static OrbitControlInfo ControlInfo
        {
            get
            {
                return OrbitGlobal.Mana._ControlInfo;
            }
        }

        /// <summary>
        /// Stream制御取得
        /// </summary>
        public static OrbitSubject Subject
        {
            get
            {
                return OrbitGlobal.Mana._Subject;
            }
        }

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




        /// <summary>
        /// World座標からtile indexを計算する
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Point WorldToTileIndex(Vector3 pos)
        {
            Point ans = new Point();
            ans.X = Convert.ToInt32((pos.X / (float)-OrbitGlobal.Project.BaseInfo.TileSize.Width));
            ans.Y = Convert.ToInt32((pos.Y / (float)-OrbitGlobal.Project.BaseInfo.TileSize.Height));

            return ans;
        }

        /// <summary>
        /// Tile indexからwolrd位置を計算する
        /// </summary>
        /// <param name="ix"></param>
        /// <param name="iy"></param>
        /// <returns></returns>
        public static Vector3 TileIndexToWorld(int ix, int iy)
        {
            if (OrbitGlobal.Project == null)
            {
                return new Vector3(0.0f);
            }

            Size tsize = OrbitGlobal.Project.BaseInfo.TileSize;

            Vector3 ans = new Vector3(ix * -tsize.Width, iy * -tsize.Height, 0.0f);
            return ans;
        }
    }
}
