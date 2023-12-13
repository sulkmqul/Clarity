using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity.Util;
using System.Diagnostics;

namespace ClarityOrbit
{
    /// <summary>
    /// プロジェクト情報
    /// </summary>
    public class OrbitData
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tsize">タイルサイズ</param>
        /// <param name="tcount">タイル数</param>
        public OrbitData(Size tsize, Size tcount)
        {
            this.TileSize = tsize;
            this.TileCount = tcount;

            //初期レイヤーのADD
            this.AddLayer();
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
        public List<TileSrcImageInfo> TileSrcImageList { get; private set; } = new List<TileSrcImageInfo>();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        


        /// <summary>
        /// 元タイル画像の追加 async候補
        /// </summary>
        /// <param name="filepath">読み込みタイル元画像ファイルパス</param>
        /// <returns>追加したデータ</returns>
        public TileSrcImageInfo AddTileSrcImage(string filepath)
        {
            //作成
            TileSrcImageInfo data = new TileSrcImageInfo(filepath, this.TileSize);
            this.TileSrcImageList.Add(data);

            return data;
        }

        /// <summary>
        /// 対象の元タイル画像の削除
        /// </summary>
        /// <param name="sinfo">削除対象</param>
        /// <returns>成功可否</returns>
        public bool RemoveTileSrcImage(TileSrcImageInfo sinfo)
        {
            return this.TileSrcImageList.Remove(sinfo);
        }

        /// <summary>
        /// レイヤーの追加処理
        /// </summary>
        public void AddLayer()
        {
            OrbitLayer lay = new OrbitLayer();
            //順番を編集する
            lay.OrderNo = this.LayerList.MaxBy(x => x.OrderNo)?.OrderNo + 1 ?? 1;
            this.LayerList.Add(lay);
        }

        /// <summary>
        /// 対象レイヤーの削除
        /// </summary>
        /// <param name="data">削除データ</param>
        public void RemoveLayer(OrbitLayer data)
        {
            //対象の削除
            this.LayerList.Remove(data);

            //order noの再割り当て
            this.LayerList.OrderBy(x => x.OrderNo).Select((x, i) => { x.OrderNo = i + 1; return x; }).ToList();
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
        private static ClaritySequence IDSeq = new ClaritySequence();
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
        /// 描画順(Z位置の等しい)
        /// </summary>
        public int OrderNo { get; set; } = 0;

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
        /// <param name="tilesize">タイル一つのサイズ(pixel)</param>
        public TileSrcImageInfo(string imagepath, Size tilesize)
        {
            this.TileSrcImageID = TileSrcImageInfo.IDSeq.NextValue;
            this.FilePath = imagepath;
            this.ImageData = new Bitmap(imagepath);
            this.TileSize = tilesize;
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
        /// 画像データ
        /// </summary>
        public Bitmap ImageData;

        /// <summary>
        /// 画像データサイズ
        /// </summary>
        public Size ImageSize
        {
            get
            {
                return this.ImageData.Size;
            }
        }

        /// <summary>
        /// タイルサイズ(pixel)(projectと同等)
        /// </summary>
        public Size TileSize { get; private set; }

        /// <summary>
        /// タイル数
        /// </summary>
        public Size TileCount
        {
            get
            {
                Size ans = new Size();
                ans.Width = Convert.ToInt32(Math.Ceiling((double)this.ImageData.Width / (double)this.TileSize.Width));
                ans.Height = Convert.ToInt32(Math.Ceiling((double)this.ImageData.Height / (double)this.TileSize.Height));

                return ans;

            }
        }

        /// <summary>
        /// 元画像上の点からindex位置を割り出す
        /// </summary>
        /// <param name="px">元画像上点X(pixel)</param>
        /// <param name="py">元画像上点Y(pixel)</param>
        /// <returns></returns>
        public Point CalcuImagePosIndex(int px, int py)
        {
            Point ans = new Point();

            ans.X = (px / this.TileSize.Width);
            ans.Y = (py / this.TileSize.Height);
            return ans;
        }

        /// <summary>
        /// Index位置から画像上の左上点(pixel)を割り出す
        /// </summary>
        /// <param name="ix">index x</param>
        /// <param name="iy">index y</param>
        /// <returns></returns>
        public Point CalcuIndexToImageLT(int ix, int iy)
        {
            Point ans = new Point();
            ans.X = ix * this.TileSize.Width;
            ans.Y = iy * this.TileSize.Height;

            return ans;

        }

        /// <summary>
        /// index位置からエリア情報を計算する
        /// </summary>
        /// <param name="ix">tile index x</param>
        /// <param name="iy">tile index y</param>
        /// <returns></returns>
        public Rectangle CalcuIndexToImageArea(int ix, int iy)
        {
            Point st = this.CalcuIndexToImageLT(ix, iy);
            Point ed = this.CalcuIndexToImageLT(ix + 1, iy + 1);

            Rectangle ans = new Rectangle(st.X, st.Y, ed.X - st.X, ed.Y - st.Y);
            return ans;

        }
    }
}
