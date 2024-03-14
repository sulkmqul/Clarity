using Clarity.Engine.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vortice.Mathematics;

namespace Clarity.Engine
{
    public partial class ClarityEngine    
    {
        /// <summary>
        /// ClarityEngineの低レベルAPI群
        /// </summary>
        /// <remarks>更新や処理などを分けて呼ぶことができる 基本的に任意更新が行いたいGUI用</remarks>
        public static class Native
        {
            /// <summary>
            /// 管理element処理を強制的に行う
            /// </summary>
            public static void Process()
            {
                ClarityEngine.Engine.Core.Process();
            }

            /// <summary>
            /// 管理elementの描画を強制的に行う
            /// </summary>
            public static void Rendering()
            {
                ClarityEngine.Engine.Core.Rendering();
            }


            /// <summary>
            /// 処理と描画を同時に呼び出す
            /// </summary>
            public static void ProcessRendering()
            {
                Native.Process();
                Native.Rendering();
            }

            /// <summary>
            /// 解放
            /// </summary>
            public static void Dispose()
            {
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
