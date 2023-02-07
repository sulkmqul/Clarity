using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ClarityEmotion
{
    /// <summary>
    /// データ再生管理
    /// </summary>
    internal class EmotionPlayer
    {

        private Task? PlayTask = null;
        public CancellationTokenSource PlayCancelSource;

        public Subject<int> StopSubject = new Subject<int>();

        public bool IsPlay
        {
            get
            {
                if (this.PlayTask == null)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 再生の開始
        /// </summary>
        /// <param name="loopflag"></param>
        /// <returns></returns>
        public void Play(bool loopflag)
        {
            if (PlayTask != null)
            {
                return;
            }
            this.PlayCancelSource = new CancellationTokenSource();
            this.PlayTask = this.PlayLoop(loopflag, this.PlayCancelSource.Token);
        }

        /// <summary>
        /// 再生の終了
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            if (this.PlayTask == null)
            {
                return;
            }

            try
            {
                this.PlayCancelSource.Cancel();
                await this.PlayTask;                
            }
            catch (OperationCanceledException ce)
            {
                //これは無視                

            }
            this.PlayTask = null;
        }


        /// <summary>
        /// 再生処理
        /// </summary>
        /// <param name="loopflag">ループ処理を行うか？ true=loopする</param>
        /// <param name="ct"></param>
        /// <returns></returns>
        private async Task PlayLoop(bool loopflag, CancellationToken ct)
        {
            int start = CeGlobal.Project.Info.FramePosition;
            int end = CeGlobal.Project.BasicInfo.MaxFrame;

            //FPSの計算
            double fps = 1000.0 / 60.0;


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
                    //こっちのほうがちょっとだけ精度が良い？
                    await Task.Run(() => System.Threading.Thread.Sleep(1));
                    //await Task.Delay(1);
                    continue;
                }

                //フレーム表示
                CeGlobal.Project.Info.FramePosition += 1;
                //最後まで行った時の動作を規定
                if (CeGlobal.Project.Info.FramePosition >= end)
                {
                    if (loopflag == true)
                    {
                        CeGlobal.Project.Info.FramePosition = 0;
                    }
                    else
                    {
                        //ストップ通知が望ましい？
                        this.StopSubject.OnNext(CeGlobal.Project.Info.FramePosition);
                        break;
                    }
                }
                CeGlobal.Event.SendFrameSelectEvent(CeGlobal.Project.Info.FramePosition);

                //精度評価
                //if ((start + 60) == CeGlobal.Project.Info.FramePosition)
                //{
                //    System.Diagnostics.Trace.WriteLine(sw.ElapsedMilliseconds);
                //}

                //次の時間を設定
                nexttime += fps;

            }
        }

    }
}
