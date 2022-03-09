using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// Root要素
    /// </summary>
    internal class RootElement : BaseElement
    {
        
    }

    /// <summary>
    /// Element管理
    /// </summary>
    public class ElementManager : BaseClaritySingleton<ElementManager>
    {
        private ElementManager()
        {
        }

        /// <summary>
        /// 要求オブジェクト
        /// </summary>
        class ReqData
        {
            public BaseElement Parent = null;
            public BaseElement Item = null;
        }

        #region メンバ変数
        /// <summary>
        /// 初期ノード
        /// </summary>
        private BaseElement RootElement = new RootElement();

        /// <summary>
        /// 各Objectの処理Indexを保持する
        /// </summary>
        private int ProcIndex = 0;

        /// <summary>
        /// 追加申請データ
        /// </summary>
        private Queue<ReqData> AddReqQue = new Queue<ReqData>();
        /// <summary>
        /// 削除申請一式
        /// </summary>
        private Queue<ReqData> RemoveReqQue = new Queue<ReqData>();
        #endregion


        /// <summary>
        /// Element管理の作成
        /// </summary>
        public static void Create()
        {
            Instance = new ElementManager();            
        }

        /// <summary>
        /// 処理Indexの取得
        /// </summary>
        /// <returns></returns>
        internal static int GetProcIndex()
        {
            Instance.ProcIndex += 1;
            return Instance.ProcIndex;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// Element追加申請
        /// </summary>
        /// <param name="parent">追加する親</param>
        /// <param name="ele">追加element</param>
        public static void AddRequest(BaseElement parent, BaseElement ele)
        {
            ReqData data = new ReqData();
            data.Parent = parent;
            data.Item = ele;

            Instance.AddReqQue.Enqueue(data);

            //追加
            ele.Init();
        }

        /// <summary>
        /// Element追加申請(ROOT登録)
        /// </summary>
        /// <param name="ele">追加対象</param>
        public static void AddRequest(BaseElement ele)
        {
            ElementManager.AddRequest(ElementManager.Instance.RootElement, ele);
        }

        /// <summary>
        /// Element削除申請
        /// </summary>
        /// <param name="ele"></param>
        public static void RemoveRequest(BaseElement ele)
        {
            ReqData data = new ReqData();
            data.Parent = null;
            data.Item = ele;
            data.Item.Enabled = false;
            Instance.RemoveReqQue.Enqueue(data);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 処理実行
        /// </summary>
        /// <param name="finfo">フレーム情報</param>
        public void Proc(FrameInfo finfo)
        {
            //処理カウントリセット
            this.ProcIndex = 0;

            //追加処理
            this.AddElement();

            //処理
            this.RootElement.Proc(this.ProcIndex, finfo);

            //当たり判定

            //削除申請処理
            this.RemoveElement();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        public void Render()
        {
            this.ProcIndex = 0;

            //描画処理の実行
            this.RootElement.Render(this.ProcIndex);
        }

        /// <summary>
        /// 全オブジェクトの個数を数える
        /// </summary>
        /// <returns></returns>
        public int CountElement()
        {
            int ans = this.RootElement.CountElement();
            return ans;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Element追加処理
        /// </summary>
        private void AddElement()
        {
            while (true)
            {
                if (this.AddReqQue.Count <= 0)
                {
                    return;
                }

                ReqData req = this.AddReqQue.Dequeue();

                //追加処理
                req.Item.SystemLink.ParentElement = req.Parent;
                req.Parent.SystemLink.ChildList.AddLast(req.Item);

                //当たり判定へ登録・・・ここは再帰的に登録する必要はない


            }
        }


        /// <summary>
        /// Element削除処理
        /// </summary>
        private void RemoveElement()
        {
            while (true)
            {
                if (this.RemoveReqQue.Count <= 0)
                {
                    return;
                }

                ReqData req = this.RemoveReqQue.Dequeue();

                //削除処理
                BaseElement par = req.Item.SystemLink.ParentElement;
                par.SystemLink.ChildList.Remove(req.Item);
                req.Item.SystemLink.ParentElement = null;

                //当たり判定の削除・・・自身の子供を再帰的に全て削除する必要がある

            }
        }
    }
}
