using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;

namespace Clarity
{
    /// <summary>
    /// 時間管理クラス
    /// </summary>
    public class ClarityTimeManager : BaseClaritySingleton<ClarityTimeManager>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ClarityTimeManager()
        {
        }

        /// <summary>
        /// 作成
        /// </summary>
        public static void Create()
        {
            ClarityTimeManager.Instance = new ClarityTimeManager();
        }


        /// <summary>
        /// 時間管理
        /// </summary>
        private Stopwatch Sw = null;

        /// <summary>
        /// 時間計測stack
        /// </summary>
        public Stack<long> MeasureStack;


        /// <summary>
        /// 経過時間合計
        /// </summary>
        public static long TotalMilliseconds
        {
            get
            {
                long ans = Mana.Sw.ElapsedMilliseconds;
                return ans;
            }
        }


        /// <summary>
        /// 時間管理の開始
        /// </summary>
        public void Start()
        {
            this.MeasureStack = new Stack<long>();
            this.Sw = new Stopwatch();
            this.Sw.Start();
                
        }

        /// <summary>
        /// 計測開始
        /// </summary>
        public static void StartMeasure()
        {
            ClarityTimeManager.Mana.MeasureStack.Push(ClarityTimeManager.TotalMilliseconds);
        }

        /// <summary>
        /// 計測結果の取得
        /// </summary>
        /// <returns></returns>
        public static long StopMeasure()
        {
            long now = ClarityTimeManager.TotalMilliseconds;
            long st = ClarityTimeManager.Mana.MeasureStack.Pop();

            return now - st;
        }


        
        
    }
}
