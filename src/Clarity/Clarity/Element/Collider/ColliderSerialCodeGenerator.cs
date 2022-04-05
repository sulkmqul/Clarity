using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Collider
{
    /// <summary>
    /// シリアル値の生成
    /// </summary>
    class ColliderSerialCodeGenerator
    {
        private static long SerialCode = 0;

        /// <summary>
        /// 取得
        /// </summary>
        /// <returns></returns>
        public static long GetNextSerial()
        {
            //念のためスレッドセーフを保証しておく
            System.Threading.Interlocked.Increment(ref ColliderSerialCodeGenerator.SerialCode);
            return ColliderSerialCodeGenerator.SerialCode;
        }
    }
}
