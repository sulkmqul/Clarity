using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Element.Collider;


namespace Clarity.Element
{
    

    /// <summary>
    /// ゲームObject管理クラス・・・これは特殊なためBaseClarityFactoryの継承は一時保留
    /// </summary>
    internal class ElementManager : BaseClaritySingleton<ElementManager>
    {
        private ElementManager()
        {
        }

        public static void Create()
        {
            Instance = new ElementManager();
            Instance.Init();
        }

        /// <summary>
        /// 追加情報クラス
        /// </summary>
        protected class ReqData
        {
            /// <summary>
            /// 追加データID
            /// </summary>
            public long ID;
            /// <summary>
            /// 追加データ本体
            /// </summary>
            public BaseElement Ele;
            /// <summary>
            /// 当たり判定可否
            /// </summary>
            public bool ColFlag;
        }

        #region メンバ変数
        /// <summary>
        /// 管理オブジェクト一式[object id, manage object list]
        /// </summary>
        private Dictionary<long, List<BaseElement>> ElementDic = new Dictionary<long, List<BaseElement>>();

        /// <summary>
        /// 追加申請データ
        /// </summary>
        protected Queue<ReqData> AddReqQue = new Queue<ReqData>();
        /// <summary>
        /// 削除申請一式
        /// </summary>
        protected Queue<ReqData> RemoveReqQue = new Queue<ReqData>();

        /// <summary>
        /// 衝突判定管理
        /// </summary>
        private ColliderManager ColMana = new ColliderManager();
        #endregion

        /// <summary>
        /// 初期化
        /// </summary>
        protected void Init()
        {
            
        }

        /// <summary>
        /// 当たり判定への追加
        /// </summary>
        /// <param name="ele"></param>
        private void AddColliderManager(BaseElement ele)
        {
            ICollider ic = ele as ICollider;
            if (ic == null)
            {
                return;
            }
            this.ColMana.AddCollider(ic);
        }
        /// <summary>
        /// 当たり判定から削除
        /// </summary>
        /// <param name="ele"></param>
        private void RemoveColliderManager(BaseElement ele)
        {
            ICollider ic = ele as ICollider;
            if (ic == null)
            {
                return;
            }
            this.ColMana.RemoveCollider(ic);
        }

        /// <summary>
        /// 申請Objectの追加処理
        /// </summary>
        /// <param name="frame_time">実行フレーム時間</param>
        protected void AddObject(long frame_time)
        {
            while (true)
            {
                if (this.AddReqQue.Count <= 0)
                {
                    return;
                }

                ReqData req = this.AddReqQue.Dequeue();                

                //無い場合は新たに作成する・・・あらかじめ分かっている場合は事前追加の方が良いが・・・
                if (this.ElementDic.ContainsKey(req.ID) == false)
                {
                    this.ElementDic.Add(req.ID, new List<BaseElement>());
                }

                //管理登録
                req.Ele.Init(frame_time);
                this.ElementDic[req.ID].Add(req.Ele);

                //当たり判定登録
                this.AddColliderManager(req.Ele);


            }

        }

        /// <summary>
        /// 申請objectの削除
        /// </summary>
        protected void RemoveObject()
        {
            while (true)
            {
                if (this.RemoveReqQue.Count <= 0)
                {
                    return;
                }

                ReqData req = this.RemoveReqQue.Dequeue();
                this.ElementDic[req.ID].Remove(req.Ele);

                //当たり判定削除
                this.RemoveColliderManager(req.Ele);
            }
        }


        /// <summary>
        /// 管理オブジェクトの処理実行
        /// </summary>
        /// <param name="frame_time"></param>
        /// <param name="prev_frame_time"></param>
        protected void ProcManageObject(FrameProcParam fparam)
        {   

            //全データの処理
            foreach (List<BaseElement> olist in this.ElementDic.Values)
            {
                int index = 0;
                foreach (BaseElement obj in olist)
                {
                    //一応再生成
                    FrameProcParam fpa = new FrameProcParam(fparam);
                    fpa.ProcIndex = index;
                    obj.Proc(fpa);
                    index++;
                }
            }

            //全ての処理が終わったら衝突判定を実行する
            this.ColMana.ExecuteCollision();
        }

        /// <summary>
        /// 管理オブジェクトの描画
        /// </summary>
        /// <param name="vindex">描画viewIndex</param>
        protected void RenderManageObject(int vindex)
        {
            //全データの処理
            foreach (List<BaseElement> olist in this.ElementDic.Values)
            {
                int index = 0;
                olist.ForEach(obj =>
                {
                    FrameRenderParam rparam = new FrameRenderParam() { ViewIndex = vindex, RenderIndex = index };
                    obj.Render(rparam);
                    index++;
                });                
            }


            //当たり判定の描画を行う？
            if (ClarityEngine.Setting.Debug.RenderColliderFlag)
            {
                this.ColMana.RenderCollider(vindex);
            }

        }

        //////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// データクリア
        /// </summary>
        public void Clear()
        {
            this.ElementDic.Clear();

            //当たり判定関係の削除？

            //申請のクリア
            this.AddReqQue.Clear();
            this.RemoveReqQue.Clear();
        }

        /// <summary>
        /// 処理の実行
        /// </summary>
        /// <param name="fparam">フレーム情報</param>
        public void ProcObject(FrameProcParam fparam)
        {
            //追加申請の処理
            this.AddObject(fparam.FrameTime);

            //各オブジェクトの処理を実行する
            this.ProcManageObject(fparam);

            //当たり判定処理


            //削除申請の処理
            this.RemoveObject();
        }

        /// <summary>
        /// 描画の実行
        /// </summary>
        /// <param name="vindex">描画view index</param>
        public void RenderObject(int vindex)
        {
            this.RenderManageObject(vindex);
        }


        /// <summary>
        /// 管理へ追加
        /// </summary>
        /// <param name="ele"></param>
        public void AddRequest(BaseElement ele)
        {
            ReqData data = new ReqData();
            data.ID = ele.ObjectID;
            data.Ele = ele;
            data.ColFlag = false;

            this.AddReqQue.Enqueue(data);

        }

        /// <summary>
        /// 管理削除申請
        /// </summary>
        /// <param name="ele"></param>
        public void RemoveRequest(BaseElement ele)
        {
            ReqData data = new ReqData();
            data.ID = ele.ObjectID;
            data.Ele = ele;            

            this.RemoveReqQue.Enqueue(data);
        }
    }
}
