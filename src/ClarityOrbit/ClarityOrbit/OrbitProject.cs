using ClarityOrbit.EditView;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public List<TileImageSrcInfo> TileImageSrcList = new List<TileImageSrcInfo>();

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="binfo"></param>
        public void Init(OrbitProjectBase binfo)
        {
            this.BaseInfo = binfo;
            this.Layer = new OrbitProjectLayer(this);
        }


        /// <summary>
        /// 新しい元画像情報の作成
        /// </summary>
        /// <param name="filepath">元画像ファイルパス</param>
        /// <returns></returns>
        public TileImageSrcInfo AddNewTileImageSrc(string filepath)
        {
            TileImageSrcInfo data = new TileImageSrcInfo(this);
            data.SrcFilePath = filepath;
            data.Name = Path.GetFileName(filepath);
            data.TipImage = new Bitmap(filepath);
            
            //シェーダー登録
            data.RegistShader();

            return data;

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

        /// <summary>
        /// タイルサイズ(pixel)
        /// </summary>
        public Size TileSize
        {
            get
            {
                return this.Project.BaseInfo.TileSize;
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

    


    

}
