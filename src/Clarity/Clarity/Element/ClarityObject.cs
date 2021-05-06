using Clarity.Shader;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Texture;
using Clarity.Element.Collider;

namespace Clarity.Element
{
    /// <summary>
    /// 汎用オブジェクトクラス
    /// </summary>
    /// <remarks>
    /// 単発ならこのまま使うもよし、継承してカスタムするもよしなクラス。
    /// 必要要素の全部入りをする。
    /// </remarks>
    public class ClarityObject : BaseElement, ICollider
    {
        public ClarityObject(long id) : base(id)
        {
            //テクスチャアニメーション実行追加関数
            this.AdditionalProc += this.ProcTextureAnimation;                        
            this.AnimeCon.EndAnimeDelegateProc = this.EndTextureAnimation;

            //3Dならborneでも追加でもだが当分？永劫？ないはず
        }

        #region メンバ変数

        /// <summary>
        /// テクスチャアニメーション管理
        /// </summary>
        private TextureAnimeController AnimeCon = new TextureAnimeController();



        /// <summary>
        /// 追加切り替え時の登録者
        /// </summary>
        protected int ProcDicNo = 0;
        /// <summary>
        /// 追加切り替えDic null出ない時、処理が実行される。
        /// </summary>
        protected Dictionary<int, AdditionalProcDelegate> ProcChangeDic = null;
        #endregion


        #region プロパティ
        /// <summary>
        /// テクスチャアニメーション可否 trueの場合 設定したTextureIDは無視され、AnimeIDを優先した動作となる
        /// </summary>
        public bool TextureAnimationEnabled { get; set; }

        /// <summary>
        /// 使用するアニメーションID TextureAnimationEnabledがtrueの時有効
        /// </summary>
        public int AnimeID
        {
            get
            {
                return this.AnimeCon.CurrentAnimeID;
            }
            set
            {
                this.AnimeCon.ChangeAnime(value);
            }
        }

        /// <summary>
        /// 当たり判定情報
        /// </summary>
        public ColliderInfo ColInfo { get; set; } = null;

        #endregion

        /// <summary>
        /// このオブジェクトの初期化
        /// </summary>
        protected override void InitElement()
        {
            
        }


        
                
        /// <summary>
        /// 処理関数
        /// </summary>
        protected override void ProcElement()
        {
            this.ProcChangeDic?[this.ProcDicNo]();
        }


        /// <summary>
        /// テクスチャアニメーション処理
        /// </summary>
        private void ProcTextureAnimation()
        {
            if (this.TextureAnimationEnabled == false)
            {
                return;
            }

            this.AnimeCon.Anime(this.FrameTime);
            this.RenderSet.TextureID = this.AnimeCon.CurrentFrameInfo.TextureID;
            
        }


        /// <summary>
        /// テクスチャアニメーションの位置データが終了した
        /// </summary>
        /// <param name="aid">終わったアニメーションID</param>
        /// <remarks>Loop、Once問わず発生します。Loop時は巻き戻った時、onceは最後まで行ったときに発生します</remarks>
        protected virtual void EndTextureAnimation(int aid)
        {
            //ClarityLog.WriteDebug("EndAnime", aid);
        }

        

        /// <summary>
        ///テクスチャアニメーション描画
        /// </summary>
        protected void RenderTextureAnimation()
        {
            RendererSet rset = this.RenderSet;

            //テクスチャの設定
            Vector2 tdiv = Texture.TextureManager.SetTexture(rset.TextureID);

            //Shaderに対する設定
            ShaderDataDefault data = new ShaderDataDefault();
            data.WorldViewProjMat = this.TransSet.CreateTransposeMat();
            data.Color = rset.Color;
            data.TexDiv = tdiv;

            data.TextureOffset = this.AnimeCon.CurrentFrameInfo.TextureOffset;
            
            ShaderManager.SetShaderDataDefault(data, rset.ShaderID);
            

            //描画            
            Vertex.VertexManager.RenderData(rset.VertexID);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        protected override void RenderElement()
        {
            if (this.TextureAnimationEnabled == true)
            {
                this.RenderTextureAnimation();
            }
            else
            {
                this.RenderDefault();
            }
        }

        /// <summary>
        /// 当たり判定処理
        /// </summary>
        /// <param name="obj"></param>
        public virtual void ColliderCallback(ICollider obj)
        {
            
        }
    }



}
