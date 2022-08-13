using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Engine;

namespace ClarityOrbit
{
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
        /// 元画像ファイルパス(新規読み込み時のみ)
        /// </summary>
        public string SrcFilePath { get; set; }

        /// <summary>
        /// 表示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// これのコメント
        /// </summary>
        public string Comment { get; set; }


        /// <summary>
        /// チップ画像情報
        /// </summary>
        public Bitmap TipImage { get; set; }

        /// <summary>
        /// 画像サイズ
        /// </summary>
        public Size ImageSize
        {
            get
            {
                return this.TipImage.Size;
            }
        }

        /// <summary>
        /// 含まれるタイル数
        /// </summary>
        public Size TileCount
        {
            get
            {
                Size ans = new Size();
                ans.Width = this.ImageSize.Width / this.Project.BaseInfo.TileSize.Width;
                ans.Height = this.ImageSize.Height / this.Project.BaseInfo.TileSize.Height;
                return ans;
            }
        }

        



        /// <summary>
        /// シェーダー登録
        /// </summary>
        public void RegistShader()
        {
            ClarityEngine.Texture.LoadTexture(this.TileImageSrcID, this.TipImage);

        }

        /// <summary>
        /// シェーダー削除
        /// </summary>
        public void UnregistShader()
        {
            ClarityEngine.Texture.RemoveTexture(this.TileImageSrcID);
        }

        /// <summary>
        /// 元画像上の点からindex位置を割り出す
        /// </summary>
        /// <param name="ix">元画像上点X(pixel)</param>
        /// <param name="iy">元画像上点Y(pixel)</param>
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

    }
}
