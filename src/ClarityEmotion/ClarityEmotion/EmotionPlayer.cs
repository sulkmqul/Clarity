using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Clarity.Util;

namespace ClarityEmotion
{
    



    /// <summary>
    /// データ再生管理
    /// </summary>
    internal class EmotionPlayer
    {
        /// <summary>
        /// 実行タスク
        /// </summary>
        private ClarityCycling PlayCycle = new ClarityCycling();

        public Subject<int> StopSubject = new Subject<int>();

        public bool IsPlay
        {
            get
            {
                return this.PlayCycle.IsClycling;

            }
        }

        /// <summary>
        /// 再生の開始
        /// </summary>
        /// <param name="loopflag"></param>
        /// <returns></returns>
        public void Play(bool loopflag)
        {
            
            double fps = 1000.0 / (double)CeGlobal.Project.Info.FPS;

            //this.PlayCycle= new ClarityCycling();
            this.PlayCycle.StartCycling((double a) => this.PlayProc(a, loopflag), fps);

            CeGlobal.Event.SendValueChangeEvent(EEventID.PlayerStart, this.IsPlay);
        }

        /// <summary>
        /// 再生の終了
        /// </summary>
        /// <returns></returns>
        public async Task Stop()
        {
            await this.PlayCycle.Stop();

            CeGlobal.Event.SendValueChangeEvent(EEventID.PlayerStop, this.IsPlay);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 再生処理
        /// </summary>
        /// <param name="ms">実行時間</param>
        /// <param name="loopflag">ループ処理を行うか？ true=loopする</param>        
        /// <returns></returns>
        private void PlayProc(double ms, bool loopflag)
        {
            int end = CeGlobal.Project.BasicInfo.MaxFrame;

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
                    return;
                }
            }
            CeGlobal.Event.SendFrameSelectEvent(CeGlobal.Project.Info.FramePosition);

            //精度評価            
            //System.Diagnostics.Trace.WriteLine($"{CeGlobal.Project.Info.FramePosition}:{ms}");
            
        }

    }
}
