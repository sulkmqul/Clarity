using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine
{
    /// <summary>
    /// Element操作関連の分離定義
    /// </summary>
    public partial class ClarityEngine
    {

        /// <summary>
        /// 対象以下の管理構造に一括処理を適応する
        /// </summary>
        /// <param name="sid">対象構造ID</param>        
        /// <param name="ac">処理</param>
        public static void ExecuteManagementProc(long sid, Action<BaseElement> ac)
        {
            BaseElement parent = ClarityEngine.Engine.EngineData.UserStructure.GetNode(sid);
            foreach (var c in parent.SystemLink.ChildList)
            {
                ExecuteManagementProcRecall(c, ac);
            }
        }

        /// <summary>
        /// 再帰して全てのobjに適応する
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ac"></param>
        private static void ExecuteManagementProcRecall(BaseElement obj, Action<BaseElement> ac)
        {
            foreach (var c in obj.SystemLink.ChildList)
            {
                ac.Invoke(c);
                ExecuteManagementProcRecall(obj, ac);
            }
        }

        /// <summary>
        /// 管理へ追加
        /// </summary>
        /// <param name="sid">追加先構造ID</param>
        /// <param name="data">追加データ</param>
        public static void AddManage(long sid, BaseElement data)
        {
            BaseElement parent = ClarityEngine.Engine.EngineData.UserStructure.GetNode(sid);
            ElementManager.AddRequest(parent, data);
        }

        /// <summary>
        /// 親を指定して管理に追加
        /// </summary>
        /// <param name="parent">すでに管理に追加済みの親object</param>
        /// <param name="data">追加データ</param>
        public static void AddManage(BaseElement parent, BaseElement data)
        {            
            ElementManager.AddRequest(parent, data);
        }

        /// <summary>
        /// 管理削除申請
        /// </summary>
        /// <param name="data"></param>
        public static void RemoveManage(BaseElement data)
        {
            ElementManager.RemoveRequest(data);
        }

        /// <summary>
        /// 自身の子供を一括削除する
        /// </summary>
        /// <param name="parent"></param>
        public static void ClarChild(BaseElement parent)
        {
            foreach (var a in parent.SystemLink.ChildList)
            {
                ElementManager.RemoveRequest(a);
            }            
        }

        /// <summary>
        /// システム管理へ追加
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>User処理を外れてシステム部分から呼ばれるElementの場合はこれを利用する。これは真っ先に動く処理</remarks>
        public static void AddManageSystem(BaseElement data)
        {
            var parent = Engine.EngineData.SystemStructure.GetNode(ESystemStructureID.System);
            ElementManager.AddRequest(parent, data);
        }


        /// <summary>
        /// ユーザー管理へ直接追加
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>structure構造を経ず、直接ユーザー管理へ追加する</remarks> 
        public static void AddManageUser(BaseElement data)
        {
            var parent = Engine.EngineData.SystemStructure.GetNode(ESystemStructureID.User);
            ElementManager.AddRequest(parent, data);
        }

        /// <summary>
        /// システム管理後処理へ追加
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>User処理を外れてシステム部分から呼ばれるElementの場合はこれを利用する。これは全部の処理の後起動する</remarks> 
        public static void AddManageCleanup(BaseElement data)
        {
            var parent = Engine.EngineData.SystemStructure.GetNode(ESystemStructureID.Cleanup);
            ElementManager.AddRequest(parent, data);
        }


        
    }
}
