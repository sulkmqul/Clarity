using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clarity.Util
{
    /// <summary>
    /// 定常処理をバックグラウンドである程度厳密に行うもの
    /// </summary>
    public class ClarityCycling
    {
        public ClarityCycling()
        {
            //タイマーの精度を規定
            Clarity.DLL.Winmm.timeBeginPeriod(1);
        }

        /// <summary>
        /// 実行タスク
        /// </summary>
        private Task? CycleTask = null;
        /// <summary>
        /// タスクキャンセル
        /// </summary>
        private CancellationTokenSource CycleCancelSource = new CancellationTokenSource();

        /// <summary>
        /// 実行中可否 true=実行中
        /// </summary>
        public bool IsClycling
        {
            get
            {
                if (this.CycleTask == null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// サイクリングの開始
        /// </summary>
        /// <param name="ac">定常処理 Action引数：開始からの継続起動時間(ms)</param>
        /// <param name="ms">起動間隔</param>
        public void StartCycling(Action<double> ac, double ms = 25.0)
        {
            //既存タスク実行中なら終わり
            if (this.IsClycling == true)
            {
                System.Diagnostics.Trace.WriteLine("cycling is already started");
                return;
            }

            //実行開始
            this.CycleCancelSource = new CancellationTokenSource();
            this.CycleTask = this.PlayLoop(ac, ms, this.CycleCancelSource.Token);
        }

        /// <summary>
        /// 再生の終了
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            if (this.CycleTask == null)
            {
                System.Diagnostics.Trace.WriteLine("cycling is not started");
                return;
            }

            try
            {
                //終了通知
                this.CycleCancelSource.Cancel();
                await this.CycleTask;
            }
            catch (OperationCanceledException ce)
            {
                //これは問題ないので無視

            }
            this.CycleTask = null;
        }


        /// <summary>
        /// サイクル処理本体
        /// </summary>
        /// <param name="ac">実行処理</param>
        /// <param name="fps">起動間隔(ms)</param>
        /// <param name="ct">キャンセル</param>
        /// <returns></returns>
        private async Task PlayLoop(Action<double> ac, double fps, CancellationToken ct)
        {

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            //次の時間
            double nexttime = fps;
            sw.Start();

            while (true)
            {
                //キャンセルされていたら例外を投げる
                ct.ThrowIfCancellationRequested();

                //FPS処理
                //時が来ているか？
                double nowtime = (double)sw.ElapsedMilliseconds;
                if (nowtime < nexttime)
                {
                    //時間が来ていないならsleep
                    //こっちのほうがほんのちょっとだけ精度が良い？
                    await Task.Run(() => System.Threading.Thread.Sleep(1));
                    //await Task.Delay(1);
                    continue;
                }

                //処理の実行
                ac.Invoke(nowtime);

                //次の時間を設定
                nexttime += fps;

            }
        }
    }

    #region 昔のソース(23/2/7無効化) 適当な期間が過ぎたら削除
#if false
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
#endif
    #endregion
}
