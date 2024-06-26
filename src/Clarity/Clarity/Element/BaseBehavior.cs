﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    /// <summary>
    /// 所作基底
    /// </summary>
    public abstract class BaseBehavior
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="oid">所作番号</param>
        public BaseBehavior(long oid = 0)
        {
            this.ID = oid;
        }
        
        /// <summary>
        /// これの管理ID
        /// </summary>
        public long ID { internal init; get; }

        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="beo">所作対象</param>
        public abstract void Execute(object beo);
    }


    /// <summary>
    /// 対象actionを実行する所作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionBehavior<T> : BaseBehavior
    {
        public ActionBehavior(Action<T> ac, long oid = 0) : base(oid)
        {
            this.Proc = ac;
        }

        private Action<T> Proc;

        public override void Execute(object beo)
        {
            this.Proc((T)beo);
        }
    }

    /// <summary>
    /// Actionで動く所作
    /// </summary>
    public class ActionBehavior : ActionBehavior<object>
    {
        public ActionBehavior(Action<object> ac, long oid = 0) : base(ac, oid)
        {
            
        }
    }

    /// <summary>
    /// 切り替え管理所作基底
    /// </summary>
    public class BehaviorController : BaseBehavior
    {
        public BehaviorController(long oid = 0) : base(oid)
        {
        }

        /// <summary>
        /// 実行所作一式
        /// </summary>
        protected List<BaseBehavior> ExecBehaviorList = new List<BaseBehavior>(8);


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="beo"></param>
        public override void Execute(object beo)
        {
            //処理中の追加削除を許容するため、コピーして実行する
            //速度出ない場合は、事後削除処理を実装する必要がある
            List<BaseBehavior> templist = new List<BaseBehavior>(this.ExecBehaviorList);
            templist.ForEach(x =>
            {
                x.Execute(beo);
            });
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 処理所作の追加
        /// </summary>
        /// <param name="bh">追加所作</param>
        public void AddProcBehavior(BaseBehavior bh)
        {
            this.ExecBehaviorList.Add(bh);

        }
                
        /// <summary>
        /// 所作の削除
        /// </summary>
        /// <param name="bh">削除所作</param>
        public void RemoveProcBehavior(BaseBehavior bh)
        {
            this.ExecBehaviorList.Remove(bh);
        }

        /// <summary>
        /// 対象ID所作の全削除
        /// </summary>
        /// <param name="oid">削除対象ID</param>
        public void RemoveProcBehavior(long oid)
        {
            this.ExecBehaviorList.RemoveAll((x) => x.ID == oid);
        }

        /// <summary>
        /// 所作の全削除
        /// </summary>        
        /// <remarks>基本的にはRemoveProcBehaviorを利用する、oid同一で削除させればよい。</remarks>
        internal void ClearProcBehavior()
        {
            this.ExecBehaviorList.Clear();
        }

        /// <summary>
        /// 全所作に対して処理を実行する
        /// </summary>
        /// <param name="ac"></param>
        /*public void  ProcActionExecList(Action<BaseBehavior> ac)
        {
            this.ExecBehaviorList.ForEach(x =>
            {
                ac.Invoke(x);
            });
        }*/
    }

    /// <summary>
    /// 引数castの基底所作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseModelBehavior<T> : BaseBehavior where T : BaseElement
    {
        public BaseModelBehavior(long oid = 0) : base(oid)
        {
        }

        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="obj">所作対象</param>
        protected abstract void ExecuteBehavior(T obj);

        /// <summary>
        /// 所作の実行
        /// </summary>
        /// <param name="beo">所作対象</param>
        public override void Execute(object beo)
        {
            T? a = beo as T;
            if (a == null)
            {
                return;
            }            
            this.ExecuteBehavior(a);
        }
    }

}
