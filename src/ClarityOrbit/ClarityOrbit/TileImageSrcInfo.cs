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
        public Bitmap TipImage;



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

    }
}
