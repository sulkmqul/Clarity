using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Util
{
    /// <summary>
    /// 一定周期でバックグラウンド処理を行うもの
    /// </summary>
    public class ClarityCycling  : IDisposable
    {
        /// <summary>
        /// 有効可否判定Delegate return:true=有効 false=無効
        /// </summary>
        /// <returns></returns>
        public delegate bool CyclingEnabledDelegate();

        /// <summary>
        /// ループ可否
        /// </summary>
        private bool Loop = true;

        /// <summary>
        /// タスク
        /// </summary>
        private Task CyclingTask = null;

        /// <summary>
        /// 有効可否判断処理 ret=true有効
        /// </summary>
        public CyclingEnabledDelegate EnabledProc = null;


        /// <summary>
        /// サイクル処理開始
        /// </summary>
        /// <param name="ac">定常処理関数</param>
        /// <param name="ms">起動間隔</param>
        public void StartCycling(Action ac, long ms = 25)
        {
            this.CyclingTask = this.Cycling(ac, ms);
        }
        
        /// <summary>
        /// サイクル処理終了
        /// </summary>
        /// <returns></returns>
        public async Task StopCycling()
        {
            this.Loop = false;
            await this.CyclingTask;
        }

        /// <summary>
        /// 破棄
        /// </summary>
        public void Dispose()
        {
            this.StopCycling().Wait();
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 周回処理
        /// </summary>
        /// <param name="ac"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        private async Task Cycling(Action ac, long ms)
        {            
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            long prev = 0;

            while (this.Loop)
            {
                //起動時間確認
                long sp = sw.ElapsedMilliseconds - prev;
                //有効か？
                bool enabeld = this.EnabledProc?.Invoke() ?? true;
                //時間が経過していない、もしくは無効な場合はsleep
                if (sp < ms || enabeld == false)
                {
                    await Task.Delay(1);                    
                    continue;
                }
                //起動時間保存
                prev = sw.ElapsedMilliseconds;

                //処理
                ac.Invoke();
                               
            }
        }

        
    }
}
