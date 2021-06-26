using Clarity.Shader;
using Clarity.Texture;
using Clarity.Vertex;
using SharpDX;
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
        protected internal ElementFrameInfo FrameInfo = new ElementFrameInfo();
        #region 基本データプロパティ
        /// <summary>
        /// 今回の処理順番
        /// </summary>
        public int ProcIndex
        {
            get
            {
                return this.FrameInfo.ProcIndex;
            }
        }
        /// <summary>
        /// 今回フレームの基準時間
        /// </summary>
        public long FrameTime
        {
            get
            {
                return this.FrameInfo.ProcFrameTime;
            }
        }
        /// <summary>
        /// 前回の基準時間
        /// </summary>
        public long PrevFrameTime
        {
            get
            {
                return this.FrameInfo.PrevProcFrameTime;
            }
        }
        /// <summary>
        /// 今回と前回の差分時間
        /// </summary>
        public long FrameSpan
        {
            get
            {
                return this.FrameInfo.Span;
            }
        }

        /// <summary>
        /// 描画index
        /// </summary>
        public int RenderIndex
        {
            get
            {
                return this.FrameInfo.RenderIndex;
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
        public long CreateTime { get; internal set; }


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
        /// <summary>
        /// 透明色の設定
        /// </summary>
        public float ColorAlpha
        {
            get
            {
                return this.RenderSet.Color.W;
            }
            set
            {
                this.RenderSet.Color.W = value;
            }
        }

        /// <summary>
        /// TextureOffset < 1.0
        /// </summary>
        public Vector2 TextureOffset
        {
            get
            {
                return this.RenderSet.TextureOffset;
            }
            set
            {
                this.RenderSet.TextureOffset = value;
            }
        }

        /// <summary>
        /// TextureIDの設定
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="texid"></param>
        public void SetTextureID(int slot, int texid)
        {
            this.RenderSet.SetTextureId(slot, texid);
        }

        /// <summary>
        /// TextureID値の取得
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public int GetTextureID(int slot)
        {
            return this.RenderSet.TextureIdList[slot];
        }
        #endregion


        /// <summary>
        /// これの親
        /// </summary>
        public BaseElement ParentObj = null;

        /// <summary>
        /// 自身の配下のobject ここに登録したものは通常の描画からは外れ、このobject内で描画が行われる。AddElementはしてはいけない。
        /// また処理は親objectの実行後行われる。
        /// </summary>
        private List<BaseElement> DominateList = new List<BaseElement>();

        /// <summary>
        /// イベント送付対象
        /// </summary>
        private List<IClarityElementEvent> EventSenderList = new List<IClarityElementEvent>();

        #endregion

        protected abstract void InitElement();
        protected abstract void ProcElement();
        protected abstract void RenderElement();

        

        /// <summary>
        /// 初期化関数
        /// </summary>
        internal void Init()
        {
            this.ShaderID = ClarityDataIndex.Shader_Default;

            this.InitElement();
        }

        /// <summary>
        /// Speedのフレームレート処理
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        private SpeedSet ApplayFrameRate(SpeedSet src)
        {
            SpeedSet ans = new SpeedSet();
            ans.Pos3D = this.ApplayFrameRate(src.Pos3D);
            ans.Rot = this.ApplayFrameRate(src.Rot);
            ans.Scale3D = this.ApplayFrameRate(src.Scale3D);
            ans.ScaleRate = this.ApplayFrameRate(src.ScaleRate);
            ans.Color = this.ApplayFrameRate(src.Color);

            return ans;
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
            #endregion
            
            //今回の処理フレーム初期化
            this.FrameSpeed = new SpeedSet();


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


            //フレームレートを考慮した値へ変換
            SpeedSet frate = this.ApplayFrameRate(this.FrameSpeed);

            this.TransSet.Pos3D += frate.Pos3D;
            this.TransSet.Rot += frate.Rot;
            this.TransSet.Scale3D += frate.Scale3D;
            this.TransSet.ScaleRate += frate.ScaleRate;
            this.Color += frate.Color;

            //配下の処理
            this.DominateList?.ForEach(x =>
            {
                x.Proc(fparam);
            });
        }

        #region フレームレート処理
        /// <summary>
        /// フレームレートを勘定した値にする。float
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        protected float ApplayFrameRate(float v)
        {
            float ans = v * this.FrameInfo.ProcBaseRate;
            return ans;
        }


        /// <summary>
        /// フレームレートを勘定した値にする vector4
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        protected Vector4 ApplayFrameRate(Vector4 v)
        {
            Vector4 ans = v * this.FrameInfo.ProcBaseRate;
            return ans;
        }
        /// <summary>
        /// フレームレートを勘定した値にする Vector3
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        protected Vector3 ApplayFrameRate(Vector3 v)
        {
            Vector3 ans = v * this.FrameInfo.ProcBaseRate;
            return ans;
        }
        /// <summary>
        /// フレームレートを勘定した値にする Vector2
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        protected Vector2 ApplayFrameRate(Vector2 v)
        {
            Vector2 ans = v * this.FrameInfo.ProcBaseRate;
            return ans;
        }

        #endregion

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

            //配下の処理
            this.DominateList?.ForEach(x =>
            {
                x.Render(rparam);
            });

        }

        /// <summary>
        /// オブジェクトが破棄
        /// </summary>
        internal virtual void Remove()
        {
            //削除イベント送付
            this.SendEvent(ClarityElementEventID.Remove);
        }



        /// <summary>
        /// イベントの送付
        /// </summary>
        /// <param name="eid">送付イベントID</param>
        public void SendEvent(int eid)
        {
            this.EventSenderList.ForEach(x =>
            {
                x.EventCallback(eid, this);
            });
        }

        /// <summary>
        /// イベント送付対象のクリア
        /// </summary>
        public void ClearEventSenderList()
        {
            this.EventSenderList.Clear();
        }


        /// <summary>
        /// イベント送付対象への追加
        /// </summary>
        /// <param name="iee"></param>
        public void AddEventSenderList(IClarityElementEvent iee)
        {
            this.EventSenderList.Add(iee);
        }


        /// <summary>
        /// 自身の配下リストへAdd
        /// </summary>
        /// <param name="de"></param>
        protected void AddDominateList(BaseElement de)
        {
            de.Init();
            this.DominateList.Add(de);
        }
        /// <summary>
        /// 配下リストの削除
        /// </summary>
        /// <param name="de"></param>
        protected void RemoveDominateList(BaseElement de)
        {
            de.Enabled = false;
            this.DominateList.Remove(de);
        }
        /// <summary>
        /// 配下登録のクリア
        /// </summary>
        protected void ClearDominateList()
        {
            this.DominateList.Clear();
        }
        /// <summary>
        /// 配下リスト処理
        /// </summary>
        /// <param name="ac">index、対象</param>
        protected void ProcDominateList(Action<int, BaseElement> ac)
        {
            int i = 0;
            this.DominateList.ForEach((x)=>
            {
                ac.Invoke(i, x);
                i++;
            });
        }

        

    }
}
