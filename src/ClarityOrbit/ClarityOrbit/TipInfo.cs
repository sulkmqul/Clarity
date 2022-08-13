using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;
using Clarity.Engine.Element;
using System.Drawing;

namespace ClarityOrbit
{
    internal abstract class BaseTileObject : ClarityObject
    {
        public BaseTileObject() : base(0)
        {
            this.TransSet.WorldID = OrbitGlobal.OrbitWorldID;
        }

        /// <summary>
        /// 自身のindex位置
        /// </summary>
        public Point Pos { get; protected set; }
    }


    /// <summary>
    /// 元データからのチップ抜き出し情報
    /// </summary>
    internal class TileBitInfo
    {
        /// <summary>
        /// 元画像情報
        /// </summary>
        internal TileImageSrcInfo? TipImageInfo { get; set; }

        /// <summary>
        /// 元チップID
        /// </summary>
        public int TileImageSrcID
        {
            get
            {
                return this.TipImageInfo?.TileImageSrcID ?? OrbitGlobal.EVal;
            }
        }

        /// <summary>
        /// 元画像からの矩形位置index
        /// </summary>
        public Point SrcPosIndex = new Point();

    }

    /// <summary>
    /// 配置チップ画像一枚
    /// </summary>
    internal class TipInfo : BaseTileObject
    {
        public TipInfo(LayerInfo palay, Point pos) : base()
        {
            this.ParentLayer = palay;
            this.Pos = pos;
            this.ShaderID = ClarityEngine.BuildInShaderIndex.NoTexture;
            this.VertexID = ClarityEngine.BuildInPolygonModelIndex.Rect;
            //処理の追加
            this.Beh = new TipInfoControlBehavior();
            this.AddProcBehavior(this.Beh);
         
        }

        /// <summary>
        /// 親レイヤー情報
        /// </summary>
        private LayerInfo ParentLayer;

        /// <summary>
        /// これに設定されたtip情報
        /// </summary>
        public TileBitInfo? SrcInfo = null;

        
        private TipInfoControlBehavior Beh = new TipInfoControlBehavior();


        protected override void RenderElemenet()
        {
            //設定されていないなら描かない
            if (this.SrcInfo == null)
            {
                return;
            }
            base.RenderElemenet();
        }

    }


    /// <summary>
    /// Tip制御所作
    /// </summary>
    internal class TipInfoControlBehavior : BaseModelBehavior<BaseTileObject>
    {
        protected override void ExecuteBehavior(BaseTileObject obj)
        {
            //描画情報取得
            var tsize = OrbitGlobal.Project.BaseInfo.TileSize;

            //サイズと位置の設定
            obj.TransSet.Pos2D = new System.Numerics.Vector2(obj.Pos.X * tsize.Width, obj.Pos.Y * tsize.Height);
            obj.TransSet.Scale2D = new System.Numerics.Vector2(tsize.Width, tsize.Height);
            
        }

    }
}
