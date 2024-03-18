using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Util
{
    /// <summary>
    /// FPS計測を行い時の経過を確認する
    /// </summary>
    public class FpsRuler
    {
        public FpsRuler(float delay_ms)
        {
            this.DelayTime = delay_ms;
        }


        /// <summary>
        /// check後からの時間
        /// </summary>
        public float Span { get; private set; } = 0.0f;

        /// <summary>
        /// check時の総経過時間
        /// </summary>
        public float TotalTime { get; private set; } = 0;


        /// <summary>
        /// 前回の時間(ms)
        /// </summary>
        private float PrevTime = 0.0f;

        /// <summary>
        /// 要求時間(ms)
        /// </summary>
        private float DelayTime = 0.0f;

        /// <summary>
        /// 時間管理
        /// </summary>
        private Stopwatch Sw = new Stopwatch();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 計測開始
        /// </summary>
        public void Start()
        {
            this.Sw.Start();
        }

        /// <summary>
        /// FPSの確認者
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {

            float nowtimw = this.Sw.ElapsedMilliseconds;
            return this.Check(nowtimw);
        }

        /// <summary>
        /// 次へ進む
        /// </summary>
        public void Next()
        {
            this.Next(this.Sw.ElapsedMilliseconds);
        }

        /// <summary>
        /// FPSの確認。時間経過していたら次へ備える
        /// </summary>
        /// <returns></returns>
        public bool CheckNext()
        {
            float nowtimw = this.Sw.ElapsedMilliseconds;

            bool f = this.Check(nowtimw);
            if(f == true)
            {
                this.Next(this.Sw.ElapsedMilliseconds);
            }
            return f;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 時間経過したかチェック
        /// </summary>
        /// <param name="nowtime">現在経過ms</param>
        /// <returns></returns>
        /// <remarks>
        /// checkとnextを分けたいので経過時間を渡すようにしたが、一つにまとめてsw.ElapsedMillisecondsでも
        /// 全く問題がないと思われる。
        /// </remarks>
        private bool Check(float nowtime)
        {
            this.TotalTime = nowtime;
            float span = nowtime - this.PrevTime;
            if (this.DelayTime > span)
            {
                return false;
            }
            this.Span = span;
            return true;
        }

        /// <summary>
        /// 次の計測へ進む
        /// </summary>
        /// <param name="nowtime">現在経過ms</param>
        private void Next(float nowtime)
        {
            //this.PrevTime = nowtime;
            this.PrevTime += this.DelayTime;
        }


    }
}
