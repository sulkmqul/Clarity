﻿using Clarity.Shader;
using Clarity.Texture;
using Clarity.Vertex;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
  

    /// <summary>
    /// 毎フレーム自動で更新される情報
    /// </summary>
    public class ElementFrameInfo
    {   

        #region 処理関係・・・分かり易くProcを接頭語に
        /// <summary>
        /// 今回の処理順番
        /// </summary>
        public int ProcIndex = 0;
        /// <summary>
        /// 今回の処理実行時間(ms)
        /// </summary>
        public long ProcFrameTime = 0;
        /// <summary>
        /// 前回の実行時間(ms)
        /// </summary>
        public long PrevProcFrameTime = 0;

        /// <summary>
        /// 処理レート
        /// </summary>
        public float ProcBaseRate = 1.0f;

        /// <summary>
        /// 処理間の経過時間
        /// </summary>
        public long Span
        {
            get
            {
                return this.ProcFrameTime - this.PrevProcFrameTime;
            }
        }
        #endregion


        #region 描画情報　Renderを接頭語奨励
        /// <summary>
        /// ViewIndex
        /// </summary>
        public int RenderViewIndex = 0;
        /// <summary>
        /// 描画Index
        /// </summary>
        public int RenderIndex = 0;
        #endregion
    }


    /// <summary>
    /// エレメントクラス基底
    /// </summary>
    public abstract class BaseElement
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseElement(long oid)
        {
            this.ObjectID = oid;

        }

        /// <summary>
        /// 追加動作Delegate
        /// </summary>
        protected delegate void AdditionalProcDelegate();

        /// <summary>
        /// 追加処理イベント
        /// </summary>
        protected event AdditionalProcDelegate AdditionalProc;

        /// <summary>
        /// コルーチン処理、ある場合のみ、初期化時に作成せよ
        /// </summary>
        protected IEnumerator CurrentCoroutine = null;

        #region メンバ変数

        /// <summary>
        /// フレーム情報データ(毎フレーム情報が更新される情報たち)
        /// </summary>
        private ElementFrameInfo FrameInfo = new ElementFrameInfo();
        #region 基本データプロパティ
        /// <summary>
        /// 今回の処理順番
        /// </summary>
        protected int ProcIndex
        {
            get
            {
                return this.FrameInfo.ProcIndex;
            }
        }
        /// <summary>
        /// 今回フレームの基準時間
        /// </summary>
        protected long FrameTime
        {
            get
            {
                return this.FrameInfo.ProcFrameTime;
            }
        }
        /// <summary>
        /// 前回の基準時間
        /// </summary>
        protected long PrevFrameTime
        {
            get
            {
                return this.FrameInfo.PrevProcFrameTime;
            }
        }
        /// <summary>
        /// 今回と前回の差分時間
        /// </summary>
        protected long FrameSpan
        {
            get
            {
                return this.FrameInfo.Span;
            }
        }
        #endregion


        /// <summary>
        /// これの識別番号
        /// </summary>
        public long ObjectID;

        /// <summary>
        /// 有効可否
        /// </summary>
        public bool Enabled = true;

        /// <summary>
        /// これの作成時間
        /// </summary>        
        public long CreateTime = 0;


        /// <summary>
        /// 速度 これが可変対応として拡縮され、transetに加算されます。1秒値の移動量を定義します。
        /// </summary>
        public SpeedSet FrameSpeed = new SpeedSet(0.0f);
        /// <summary>
        /// 位置、回転、拡縮
        /// </summary>
        public TransposeSet TransSet = new TransposeSet();
        /// <summary>
        /// 描画情報・・・これの直接アクセスは良くない。必要に応じてアクセサを定義せよ
        /// </summary>
        internal RendererSet RenderSet = new RendererSet();

                
        #region RendererSetアクセサ
        /// <summary>
        /// 頂点ID
        /// </summary>
        public int VertexID
        {
            get
            {
                return this.RenderSet.VertexID;
            }
            set
            {
                this.RenderSet.VertexID = value;
            }
        }
        /// <summary>
        /// テクスチャコード
        /// </summary>
        public int TextureID
        {
            get
            {
                return this.RenderSet.TextureID;
            }
            set
            {
                this.RenderSet.TextureID = value;
            }
        }
        /// <summary>
        /// シェーダーコード
        /// </summary>
        public int ShaderID
        {
            get
            {
                return this.RenderSet.ShaderID;
            }
            set
            {
                this.RenderSet.ShaderID = value;
            }
        }    
        
        /// <summary>
        /// 色の設定
        /// </summary>
        public SharpDX.Vector4 Color
        {
            get
            {
                return this.RenderSet.Color;
            }
            set
            {
                this.RenderSet.Color = value;
            }
        }
        #endregion


        /// <summary>
        /// これの親
        /// </summary>
        public BaseElement ParentObj = null;


        #endregion

        protected abstract void InitElement();
        protected abstract void ProcElement();
        protected virtual void RenderElement() {
            this.RenderDefault();
        }

        

        /// <summary>
        /// 初期化関数
        /// </summary>
        internal void Init(long frame_time)            
        {
            this.CreateTime = frame_time;

            this.InitElement();
        }

        /// <summary>
        /// 処理関数
        /// </summary>        
        /// <param name="fparam">フレーム処理パラメータ</param>
        internal void Proc(FrameProcParam fparam)
        {
            #region フレーム情報更新処理
            if (fparam != null)
            {
                //フレーム基準情報の初期化
                this.FrameInfo.ProcIndex = fparam.ProcIndex;
                this.FrameInfo.ProcFrameTime = fparam.FrameTime;
                this.FrameInfo.PrevProcFrameTime = fparam.ProcIndex;
                this.FrameInfo.ProcBaseRate = fparam.FrameBaseRate;
                
            }

            //今回の処理フレーム初期化
            this.FrameSpeed = new SpeedSet();
            #endregion


            //通常処理
            this.ProcElement();
            //コルーチン処理
            var coret = this.CurrentCoroutine?.MoveNext();
            if (coret != true)
            {
                this.CurrentCoroutine = null;
            }
            
            //追加処理の実行
            this.AdditionalProc?.Invoke();


            //作成した値を加算・・・ここはフレームを考慮する            
            this.TransSet.Pos += this.FrameSpeed.Pos * this.FrameInfo.ProcBaseRate;
            this.TransSet.Rot += this.FrameSpeed.Rot * this.FrameInfo.ProcBaseRate;
            this.TransSet.Scale += this.FrameSpeed.Scale * this.FrameInfo.ProcBaseRate;
            this.TransSet.ScaleRate += this.FrameSpeed.ScaleRate * this.FrameInfo.ProcBaseRate;

        }


        /// <summary>
        /// デフォルト描画関数
        /// </summary>
        protected void RenderDefault()
        {
            RendererSet rset = this.RenderSet;

            //Shaderに対する設定
            ShaderDataDefault data = new ShaderDataDefault();
            data.WorldViewProjMat = this.TransSet.CreateTransposeMat();
            data.Color = rset.Color;

            ShaderManager.SetShaderDataDefault(data, rset.ShaderID);
            

            //描画
            TextureManager.SetTexture(rset.TextureID);
            VertexManager.RenderData(rset.VertexID);
        }


        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="rparam">フレーム描画パラメータ</param>
        internal virtual void Render(FrameRenderParam rparam)
        {
            //フレーム描画情報の更新
            this.FrameInfo.RenderViewIndex = rparam.ViewIndex;
            this.FrameInfo.RenderIndex = rparam.RenderIndex;            

            this.RenderElement();

        }

    }
}
