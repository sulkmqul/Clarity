using Clarity.DLL;
using Clarity.Engine.Texture;
using Clarity.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vortice.Mathematics;

namespace Clarity.Engine
{
    public partial class ClarityEngine    
    {
        /// <summary>
        /// ClarityEngineの低レベルAPI群
        /// </summary>        
        public static class Native
        {
            /// <summary>
            /// 処理ループの呼び出し(GUI用) 
            /// </summary>
            /// <param name="fps">FPS(目安) 30fps程度でよいと思う</param>
            /// <param name="ct">TaskLoopCancel</param>
            /// <returns></returns>
            /// <remarks>
            /// ClarityEngine.Init後のみ呼び出し可能
            /// ClarityEngine.Runとの併用は不可。
            /// </remarks>
            public static async Task ProcLoop(float fps, CancellationToken ct)
            {
                //エンジンの初期化されているか？
                if (ClarityEngine.IsEngineInit == false)
                {
                    throw new Exception("ClarityEngine is not Initialized");
                }

                try
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    int proc_count = 0;

                    FpsRuler frule = new FpsRuler(fps);
                    frule.Start();
                    while (true)
                    {
                        //CancelCheck
                        ct.ThrowIfCancellationRequested();

                        //処理確認
                        bool ck = frule.CheckNext();
                        if (ck == false)
                        {
                            await Task.Delay(1);                                                     
                            //Thread.Sleep(1);
                            continue;
                        }

                        await Task.Run(() =>
                        {
                            FrameInfo finfo = new FrameInfo((long)frule.TotalTime, frule.Span);
                            ClarityEngine.Engine.Core.Process(finfo);
                        });

                        ClarityEngine.Engine.Core.Rendering();

                        proc_count++;
                        if(sw.ElapsedMilliseconds > 1000)
                        {
                            //FPS表示
                            //System.Diagnostics.Trace.WriteLine($"TaskFPS={proc_count} {frule.Span}");
                            //proc_count = 0;
                            //sw.Restart();
                        }
                    }

                }
                catch (OperationCanceledException)
                {
                    //Cancelなので問題なし                 
                }

                ClarityEngine._Engine?.Core.Dispose();
                ClarityEngine._Engine = null;

            }

            /// <summary>
            /// クリア色の設定
            /// </summary>
            /// <param name="col"></param>
            public static void SetClearColor(Vector4 col)
            {
                ClarityEngine.Engine.Core.ClearColor = new Color4(col.X, col.Y, col.Z, col.W);
            }
        }
    }
}
