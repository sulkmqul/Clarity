using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity.Util;

namespace ClarityOrbit
{
    /// <summary>
    /// プロジェクト情報
    /// </summary>
    public class OrbitData
    {
        public OrbitData()
        {

        }
        /// <summary>
        /// タイル一つのサイズ
        /// </summary>
        public Size TileSize { get; private set; } = new Size(0, 0);
        /// <summary>
        /// タイル数(基本的に無限は考慮しない)
        /// </summary>
        public Size TileCount { get; private set; } = new Size(1, 1);

        /// <summary>
        /// レイヤー情報一式
        /// </summary>
        public List<OrbitLayer> LayerList { get; init; } = new List<OrbitLayer>();

        /// <summary>
        /// タイル元画像情報
        /// </summary>
        public List<TileSrcImageInfo> TileSrcImageList { get; init; }  = new List<TileSrcImageInfo>();


        /// <summary>
        /// プロジェクトの作成と初期化
        /// </summary>
        /// <param name="tsize">タイルサイズ</param>
        /// <param name="tcount">タイル数</param>
        public void CreateNewProject(Size tsize, Size tcount)
        {
            this.TileSize = tsize;
            this.TileCount = tcount;
        }
        
    }

    /// <summary>
    /// タイル一つを表すデータクラス
    /// </summary>
    public class TileData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TileData() 
        {
        }

        /// <summary>
        /// 親画像ID
        /// </summary>
        public int TileSrcImageID { get; set; } = 0;

        /// <summary>
        /// 画像Tile位置ID
        /// </summary>
        public int TileID { get; set; } = 0;

    }

    /// <summary>
    /// レイヤー情報
    /// </summary>
    public class OrbitLayer
    {
        private static ClaritySequence IDSeq = new ClaritySequence(9);
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OrbitLayer() 
        {
            this.LayerID = OrbitLayer.IDSeq.NextValue;
        }

        /// <summary>
        /// レイヤー情報
        /// </summary>
        public int LayerID { get; private init; } = 0;

        /// <summary>
        /// レイヤー名
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 描画可否
        /// </summary>
        public bool Visibled { get; set; } = true;

        /// <summary>
        /// 有効可否
        /// </summary>
        public bool Enabled { get; set; } = true;        

        
        /// <summary>
        /// このレイヤーのZ位置
        /// </summary>
        public float PosZ { get; set; } = 0.0f;

        /// <summary>
        /// レイヤータイル情報 [y][x]
        /// </summary>
        public List<List<TileData>> Layer { get; set; } = new List<List<TileData>>();

    }


    /// <summary>
    /// タイル元画像情報
    /// </summary>
    public class TileSrcImageInfo
    {
        /// <summary>
        /// 内部IDの発行者
        /// </summary>
        private static ClaritySequence IDSeq = new ClaritySequence();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="imagepath">読み込み画像パス</param>
        public TileSrcImageInfo(string imagepath)
        {
            this.TileSrcImageID = TileSrcImageInfo.IDSeq.NextValue;
            this.FilePath = imagepath;
            this.ImageData = new Bitmap(imagepath);
        }

        /// <summary>
        /// タイル画像ID
        /// </summary>
        public int TileSrcImageID { get; init; } = 0;

        /// <summary>
        /// 画像パス
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// 画像パス
        /// </summary>
        public Bitmap ImageData;

    }
}
