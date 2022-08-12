using ClarityOrbit.EditView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit
{
    /// <summary>
    /// プロジェクト管理クラス
    /// </summary>
    internal class OrbitProject
    {
        #region シーケンス
        /// <summary>
        /// チップ画像のID値
        /// </summary>
        private static int _TipImageIDSeq = 0;
        /// <summary>
        /// チップ画像IDの取得
        /// </summary>
        public static int NextTileImageSrcIDSeq
        {
            get
            {
                OrbitProject._TipImageIDSeq += 1;
                return OrbitProject._TipImageIDSeq;
            }
        }
        #endregion

        /// <summary>
        /// 基本情報
        /// </summary>
        public OrbitProjectBase BaseInfo;


        /// <summary>
        /// レイヤー情報
        /// </summary>
        public OrbitProjectLayer Layer;

        /// <summary>
        /// 元チップ情報
        /// </summary>
        public List<TileImageSrcInfo> TipImageList = new List<TileImageSrcInfo>();

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="binfo"></param>
        public void Init(OrbitProjectBase binfo)
        {
            this.BaseInfo = binfo;
            this.Layer = new OrbitProjectLayer(this);
        }
    }

    internal abstract class BaseOrbitProjectInfo
    {
        public BaseOrbitProjectInfo(OrbitProject op)
        {
            this.Project = op;
        }

        protected OrbitProject Project { get; init; }

        protected OrbitProjectBase BaseInfo
        {
            get
            {
                return this.Project.BaseInfo;
            }
        }
    }

    /// <summary>
    /// プロジェクト基本情報
    /// </summary>
    internal class OrbitProjectBase
    {
        public OrbitProjectBase()
        {
        }

        /// <summary>
        /// 一枚のサイズ(pixel)
        /// </summary>
        public Size TileSize = new Size(0, 0);

        /// <summary>
        /// 縦横何枚敷き詰めるか？
        /// </summary>
        public Size TileCount = new Size(0, 0);

        /// <summary>
        /// 作業場全体サイズ(pixel)
        /// </summary>
        public Size ImageSize
        {
            get
            {
                int w = this.TileSize.Width * this.TileCount.Width;
                int h = this.TileSize.Height * this.TileCount.Height;
                return new Size(w, h);
            }
        }
    }

    


    /// <summary>
    /// タイル元画像情報
    /// </summary>
    internal class TileImageSrcInfo : BaseOrbitProjectInfo
    {        
        public TileImageSrcInfo(OrbitProject op) : base(op)
        {
    
            this.TileImageSrcID = OrbitProject.NextTileImageSrcIDSeq;
        }
        /// <summary>
        /// これのチップID(OrbitProject.NextTipImageIDSeqによって割り当てること)(テクスチャIDにもなります)
        /// </summary>
        public int TileImageSrcID { get; init; }

        /// <summary>
        /// これのコメント
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 元画像ファイルパス(新規読み込み時のみ)
        /// </summary>
        public string SrcFilePath { get; set; }

        /// <summary>
        /// チップ画像情報
        /// </summary>
        public Bitmap TipImage;

        

    }

}
