using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// 根幹親子関係定義
    /// </summary>
    internal class ElementSystemLink
    {
        /// <summary>
        /// 自分の処理上の親
        /// </summary>
        public BaseElement ParentElement = null;

        /// <summary>
        /// 自分の処理する子供
        /// </summary>
        public LinkedList<BaseElement> ChildList = new LinkedList<BaseElement>();

    }

    /// <summary>
    /// 要素イベント
    /// </summary>
    /// <param name="eid">イベントID</param>
    /// <param name="sender">イベント発生元</param>
    public delegate void ElementEventDelegate(int eid, BaseElement sender);

    /// <summary>
    /// フレーム情報
    /// </summary>
    public class FrameInfo
    {
        /// <summary>
        /// 差分時間(ms)
        /// </summary>
        public long Span { init; get; }

        /// <summary>
        /// システムでカウントしている絶対時間
        /// </summary>
        public long FrameTime { init; get; }
    }


    /// <summary>
    ///基底要素
    /// </summary>
    public abstract class BaseElement
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseElement(long oid = 0)
        {
            this.ID = oid;
        }

        /// <summary>
        /// ID
        /// </summary>
        public long ID { internal init; get; }

        #region メンバ変数

        /// <summary>
        /// 自身の有効可否
        /// </summary>
        internal bool Enabled = true;

        /// <summary>
        /// 親子関係
        /// </summary>
        internal ElementSystemLink SystemLink = new ElementSystemLink();

        /// <summary>
        /// 処理所作一式
        /// </summary>
        internal List<BaseBehavior> ProcBehaviorList = new List<BaseBehavior>(10);

        /// <summary>
        /// 描画所作
        /// </summary>
        protected BaseBehavior RenderBehavior; 

        /// <summary>
        /// Elementのイベント
        /// </summary>
        private event ElementEventDelegate _ElementEvent;

        /// <summary>
        /// ElementEvent
        /// </summary>
        public event ElementEventDelegate ElementEvent
        {
            add
            {
                this._ElementEvent += value;
            }
            remove
            {
                this._ElementEvent -= value;
            }
        }



        /// <summary>
        /// 処理番号
        /// </summary>
        protected int ProcIndex = 0;

        /// <summary>
        /// 今回のフレーム情報
        /// </summary>
        protected FrameInfo FrameInfo = null;

        /// <summary>
        /// 処理情報
        /// </summary>
        public TransposeSet TransSet = new TransposeSet();
        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 処理所作の追加
        /// </summary>
        /// <param name="bh">追加所作</param>
        public void AddProcBehavior(BaseBehavior bh)
        {
            this.ProcBehaviorList.Add(bh);            
            
        }

        /// <summary>
        /// 所作の削除
        /// </summary>
        /// <param name="bh">削除所作</param>
        public void RemoveProcBehavior(BaseBehavior bh)
        {
            this.ProcBehaviorList.Remove(bh);
        }

        /// <summary>
        /// 所作の削除
        /// </summary>
        /// <param name="oid">削除対象ID</param>
        public void RemoveProcBehavior(long oid)
        {
            this.ProcBehaviorList.RemoveAll((x) => x.ID == oid);
        }
        
        /// <summary>
        /// 所作の全削除
        /// </summary>
        internal void ClearProcBehavior()
        {
            this.ProcBehaviorList.Clear();
        }

        /// <summary>
        /// イベントの送付
        /// </summary>
        /// <param name="eid">送付イベントID</param>
        protected void SendElementEvent(int eid)
        {
            this._ElementEvent.Invoke(eid, this);
        }
                
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化処理
        /// </summary>
        protected virtual void InitElement()
        {
        }

        /// <summary>
        /// 前処理
        /// </summary>
        protected virtual void ProcBefore()
        {
        }

        /// <summary>
        /// 後処理
        /// </summary>
        protected virtual void ProcAfter()
        {
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化処理
        /// </summary>
        internal void Init()
        {
            this.Enabled = true;
            this.InitElement();
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="pid">処理Index</param>
        /// <param name="finfo">処理フレーム情報</param>
        internal void Proc(int pid, FrameInfo finfo)
        {
            if (this.Enabled == false)
            {
                return;
            }

            //フレーム情報の保存
            this.ProcIndex = pid;
            this.FrameInfo = finfo;

            //前処理
            this.ProcBefore();

            //所作の実行
            this.ExecuteProcBehavior();

            //後処理
            this.ProcAfter();

            //子供の処理実行
            foreach (var c in this.SystemLink.ChildList)
            {
                int cp = ElementManager.GetProcIndex();
                c.Proc(cp, finfo);
            }
        }



        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="rid">描画Index</param>
        internal void Render(int rid)
        {
            if (this.Enabled == false)
            {
                return;
            }

            this.ProcIndex = rid;

            //描画所作の実行            
            this.RenderBehavior?.Execute(this);

            //子供の描画実行
            foreach (var c in this.SystemLink.ChildList)
            {
                int cp = ElementManager.GetProcIndex();
                c.Render(cp);
            }

        }


        /// <summary>
        /// 処理Element数を数える
        /// </summary>
        /// <returns></returns>
        internal int CountElement()
        {
            int count = this.SystemLink.ChildList.Count;
            foreach (var c in this.SystemLink.ChildList)
            {
                count += c.CountElement();
            }
            return count;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 処理所作の実行
        /// </summary>
        private void ExecuteProcBehavior()
        {
            //処理中の追加削除を許容するため、コピーして実行する
            //速度出ない場合はrequestを実装せよ
            List<BaseBehavior> templist = new List<BaseBehavior>(this.ProcBehaviorList);
            templist.ForEach(x =>
            {
                x.Execute(this);
            });

        }
    }
}
