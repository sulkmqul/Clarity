using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;
using Clarity.Engine.Element;
using System.Drawing;
using System.Numerics;
using Clarity.Engine.Element.Behavior;

namespace ClarityOrbit
{
    //これはEditViewに分離するべき
    internal struct TilemapShaderData
    {
        public Matrix4x4 WorldViewProj;
        public Vector4 Color;

        public Vector2 TexOffset;
        public Vector2 TexAreaSize;

    }

    /// <summary>
    /// Tile描画Behavior
    /// </summary>
    class TileMapRenderBehavior : BaseRenderBehavior
    {
        protected override void RenderSetShaderData(ClarityObject obj)
        {
            TipInfo? tipinfo = obj as TipInfo;
            if (tipinfo == null)
            {
                return;
            }

            TilemapShaderData data = new TilemapShaderData();
            data.WorldViewProj = obj.TransSet.CreateTransposeMat();
            data.Color = obj.Color;

            Vector2 texdiv = tipinfo?.SrcInfo?.TipImageInfo?.TexDiv ?? new Vector2(0.0f, 0.0f);            
            
            data.TexOffset = new Vector2((float)tipinfo.SrcInfo.SrcPosIndex.X * texdiv.X, (float)tipinfo.SrcInfo.SrcPosIndex.Y * texdiv.Y);
            data.TexAreaSize = new Vector2(texdiv.X, texdiv.Y);

            obj.TextureID = tipinfo.SrcInfo.TileImageSrcID;

            ClarityEngine.SetShaderData<TilemapShaderData>((int)EShaderCode.TileMap, data);

        }

        
    }


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

        
        protected override void RenderElemenet()
        {
            bool ckret = this.CheckViewArea();
            if (ckret == false)
            {
                return;
            }
            

            base.RenderElemenet();
        }

        /// <summary>
        /// 自身が表示範囲か否かを確認する
        /// </summary>
        /// <returns></returns>
        public bool CheckViewArea()
        {
            Rectangle rc = EditView.OrbitEditViewControl.TempInfo.ViewAreaIndexRect;

            //ちょっと余白を考慮
            int ho = 2;
            rc.X -= ho;
            rc.Y -= 2;
            rc.Width += ho * 2;
            rc.Height += ho * 2;


            //範囲外ならかかない
            bool f = rc.Contains(this.Pos);
            if (f == false)
            {
                return false;
            }

            return true;
        }
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
    /// 配置チップ画像一枚・・・これデータと分離してClarityObjectの部分はEditView送りが分け方として適切・・・な気がする
    /// </summary>
    internal class TipInfo : BaseTileObject
    {
        public TipInfo(LayerInfo palay, Point pos) : base()
        {
            this.ParentLayer = palay;
            this.Pos = pos;
            this.ShaderID = ClarityEngine.BuildInShaderIndex.NoTexture;
            this.SetVertexCode(EVertexCode.Tile);
            //処理の追加
            this.Beh = new TipInfoControlBehavior();
            this.AddProcBehavior(this.Beh);

            //描画処理
            this.RenderBehavior = new TileMapRenderBehavior();

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

        /// <summary>
        /// データ設定可否
        /// </summary>
        public bool DataExists
        {
            get
            {
                if (this.SrcInfo == null)
                {
                    return false;
                }
                return true;
            }
        }


        /// <summary>
        /// 描画処理
        /// </summary>
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
            obj.TransSet.Pos3D = OrbitGlobal.TileIndexToWorld(obj.Pos.X, obj.Pos.Y);
            obj.TransSet.Scale2D = new System.Numerics.Vector2(tsize.Width, tsize.Height);

            //カメラ範囲なら当たり判定有効化
            bool f = obj.CheckViewArea();
            if (obj.ColInfo != null)
            {
                obj.ColInfo.Enabled = f;
            }

        }

    }
}
