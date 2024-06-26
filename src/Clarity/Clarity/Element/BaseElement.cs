﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Collider;

namespace Clarity
{
    /// <summary>
    /// 根幹親子関係定義
    /// </summary>
    internal class ElementSystemLink
    {
        /// <summary>
        /// 自分の処理上の親
        /// </summary>
        public BaseElement? ParentElement = null;

        /// <summary>
        /// 自分の処理する子供
        /// </summary>
        public LinkedList<BaseElement> ChildList = new LinkedList<BaseElement>();
        //public List<BaseElement> ChildList = new List<BaseElement>();

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
        public FrameInfo(long frametilme, float span)
        {
            this.FrameTime = frametilme;
            this.Span = span;            
        }

        public FrameInfo(FrameInfo f)
        {
            this.Span = f.Span;
            this.FrameTime = f.FrameTime;                        
        }

        private float _Span = 0;

        /// <summary>
        /// 差分時間(ms)
        /// </summary>
        public float Span
        {
            init
            {
                this._Span = value;

                //時間Rateの計算
                this.BaseRate = ((float)value * 0.001f);
            }
            get
            {
                return this._Span;
            }
        }

        /// <summary>
        /// 基準時間Rate
        /// </summary>
        public float BaseRate { get; private set; } = 0.0f;

        /// <summary>
        /// システムでカウントしている絶対時間
        /// </summary>
        public long FrameTime { init; get; }
    }


    /// <summary>
    /// Element処理所作
    /// </summary>
    public class ElementProcBehavior : BehaviorController
    {
        /// <summary>
        /// 切り替えを随時行わない固定処理はこちらを使う。(元はClearで削除される可能性がある。こちらは追加だけを想定)
        /// </summary>
        protected List<BaseBehavior> FixedProcBehaviorList = new List<BaseBehavior>(8);

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="beo"></param>
        public override void Execute(object beo)
        {
            base.Execute(beo);

            //固定処理の実行
            this.FixedProcBehaviorList.ForEach(x =>
            {
                x.Execute(beo);
            });
        }

        /// <summary>
        /// 固定処理の追加
        /// </summary>
        /// <param name="beh"></param>
        public void AddFixedProcess(BaseBehavior beh)
        {
            this.FixedProcBehaviorList.Add(beh);
        }

        /// <summary>
        /// 固定処理の解除
        /// </summary>
        /// <param name="beh"></param>
        protected void RemoveFiexedProcess(BaseBehavior beh)
        {
            this.FixedProcBehaviorList.Remove(beh);
        }
    }

    /// <summary>
    ///基底要素
    /// </summary>
    public abstract class BaseElement
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseElement(long eid = 0)
        {
            this.ElementID = eid;
        }


        #region メンバ変数

        /// <summary>
        /// ID
        /// </summary>
        public long ElementID { internal init; get; }        

        /// <summary>
        /// 自身の有効可否
        /// </summary>
        internal bool Enabled { get; set; } = true;

        #region 親子関係        
        /// <summary>
        /// 親子関係
        /// </summary>
        internal ElementSystemLink SystemLink { get; init; } = new ElementSystemLink();

        /// <summary>
        /// 子供の処理の実行可否を制御 true:実行 false:子供はskip
        /// </summary>
        /// <returns></returns>
        protected bool ControlChildProcEnabled { get; set; } = true;

        /// <summary>
        /// 子供の処理の描画可否を制御 true:実行 false:子供はskip
        /// </summary>
        /// <returns></returns>
        protected bool ControlChildRenderEnabled { get; set; } = true;
        #endregion

        /// <summary>
        /// 実行所作
        /// </summary>
        //protected ElementProcBehavior ProcBehavior = new ElementProcBehavior();
        protected BehaviorController ProcBehavior = new BehaviorController();

        /// <summary>
        /// 描画所作
        /// </summary>
        protected BaseBehavior? RenderBehavior = null; 

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



        #region ここはフレーム処理ごとに更新される情報、まとめたclassを作った方が良いかもしれない
        /// <summary>
        /// 処理番号
        /// </summary>
        protected int ProcIndex { get; private set; } = 0;

        /// <summary>
        /// 今回のフレーム情報
        /// </summary>
        public FrameInfo FrameInfo { get; private set; }


        /// <summary>
        /// 自身の基準時間
        /// </summary>
        //public long ProcTime { get; internal set; } = 0;
        public float ProcTime { get; internal set; } = 0;
        #endregion






        /// <summary>
        /// Clarityシステム管理可否
        /// </summary>
        public bool IsManaged
        {
            get
            {
                if (this.SystemLink.ParentElement == null)
                {
                    return false;
                }
                return true;
            }
        }
        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 処理所作の追加
        /// </summary>
        /// <param name="bh">追加所作</param>
        public void AddProcBehavior(BaseBehavior bh)
        {
            this.ProcBehavior.AddProcBehavior(bh);
        }

        /// <summary>
        /// 所作の削除
        /// </summary>
        /// <param name="bh">削除所作</param>
        public void RemoveProcBehavior(BaseBehavior bh)
        {
            this.ProcBehavior.RemoveProcBehavior(bh);
        }

        /// <summary>
        /// 所作の削除
        /// </summary>
        /// <param name="oid">削除対象ID</param>
        public void RemoveProcBehavior(long oid)
        {
            this.ProcBehavior.RemoveProcBehavior(oid);
        }

        /// <summary>
        /// 子供の並べ替え
        /// </summary>
        /// <param name="func"></param>
        public void ChildOrderBy(Func<BaseElement, int> func)
        {
            this.SystemLink.ChildList.OrderBy(func);
        }

        /// <summary>
        /// 子供に対して一括処理を掛ける
        /// </summary>
        /// <param name="ac"></param>
        //[Obsolete("何かを考慮して用意をしておくが、使わない方がスマートです")]
        public void ProcChildAll(Action<BaseElement> ac)
        {
            foreach (var c in this.SystemLink.ChildList)
            {
                ac(c);
            }
        }

        /// <summary>
        /// 所作の全削除
        /// </summary>
        internal void ClearProcBehavior()
        {
            this.ProcBehavior.ClearProcBehavior();
        }

        /// <summary>
        /// 描画所作の設定
        /// </summary>
        /// <param name="be"></param>
        internal void SetRenderBehavior(BaseBehavior be)
        {
            this.RenderBehavior = be;
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
        protected virtual void PreProcess()
        {
        }

        /// <summary>
        /// 後処理
        /// </summary>
        protected virtual void PostProcess()
        {
        }

        /// <summary>
        /// 子供の処理の後の後始末処理。これが最終処理
        /// </summary>
        protected virtual void ProcCleanup()
        {
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        protected virtual void RenderElemenet()
        {
            //描画所作の実行            
            this.RenderBehavior?.Execute(this);            
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
            this.FrameInfo = new FrameInfo(finfo);
            

            //自分の時間を経過させる
            this.ProcTime += this.FrameInfo.Span;

            //前処理
            this.PreProcess();

            //所作の実行
            this.ProcBehavior.Execute(this);

            //後処理
            this.PostProcess();

            //子供の実行可否を確認            
            if (this.ControlChildProcEnabled == false)
            {
                return;
            }


            //子供の処理実行
            var cfinfo = new FrameInfo(finfo);
            foreach (var c in this.SystemLink.ChildList)
            {   
                int cp = ElementManager.GetProcIndex();
                c.Proc(cp, cfinfo);
            }

            //子供処理の後
            this.ProcCleanup();
        }






        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="id">描画情報番号</param>
        /// <param name="rid">描画Index</param>        
        internal void Render(int id, int rid)
        {
            if (this.Enabled == false)
            {
                return;
            }

            this.ProcIndex = rid;            

            this.RenderElemenet();

            //子供の描画可否を確認            
            if (this.ControlChildRenderEnabled == false)
            {
                return;
            }

            //子供の描画実行
            foreach (var c in this.SystemLink.ChildList)
            {
                int cp = ElementManager.GetProcIndex();
                c.Render(id, cp);
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

        /// <summary>
        /// 子供に追加
        /// </summary>
        /// <param name="data"></param>
        internal void AddChild(BaseElement data)
        {
            data.SystemLink.ParentElement = this;
            this.SystemLink.ChildList.AddLast(data);
            //this.SystemLink.ChildList.Add(data);
        }


        /// <summary>
        /// 子供の削除
        /// </summary>
        /// <param name="data"></param>
        internal void RemoveChild(BaseElement data)
        {
            //削除処理            
            this.SystemLink.ChildList.Remove(data);
            data.SystemLink.ParentElement = null;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

      
    }




    
}
