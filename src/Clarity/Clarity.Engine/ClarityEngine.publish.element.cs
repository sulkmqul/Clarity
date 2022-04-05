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
        /// 管理削除申請
        /// </summary>
        /// <param name="data"></param>
        public static void RemoveManage(BaseElement data)
        {
            ElementManager.RemoveRequest(data);
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
