using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAid.AidProcess
{


    /// <summary>
    /// Aidバッチ処理IF
    /// </summary>
    internal interface IAidProcess
    {
        string ClassName
        {
            get;
        }

        /// <summary>
        /// 対象の固定値一覧を取得する
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        /// <remarks>
        /// BuildInIndexを含めてしまう試み
        /// </remarks>
        static List<IdData> CreateSystemConst(Type ct)
        {
            string prefix = "System_";

            
            MemberInfo[] minfovec = ct.GetMembers(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

            List<IdData> anslist = new List<IdData>();
            foreach (MemberInfo minfo in minfovec)
            {
                string name = minfo.Name;
                int val = (int)ct.GetField(name).GetValue(null);

                IdData data = new IdData(prefix + name, val);
                anslist.Add(data);
            }


            return anslist;
        }

        void Proc(ArgParam param);

    }
}
