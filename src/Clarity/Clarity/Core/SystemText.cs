using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Core
{
    /// <summary>
    /// システム文字描画
    /// </summary>
    internal class SystemText : Element.MultiTextObject
    {
        public static readonly int SlotFPS = 0;
        public static readonly int SlotElementCount = 1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fontsize">文字サイズ</param>
        /// <param name="col">文字色</param>
        /// <param name="lines">文字行数</param>
        public SystemText(float fontsize, SharpDX.Color col, int lines = 1) : base(fontsize, col, lines)
        {
            
        }

        

        /// <summary>
        /// FPSの設定
        /// </summary>
        /// <param name="s"></param>
        public void SetFPS(string s)
        {
            this.SetText(SlotFPS, s);
        }

        /// <summary>
        /// 管理Element数の設定
        /// </summary>
        /// <param name="s"></param>
        public void SetElementCount(string s)
        {
            this.SetText(SlotElementCount, s);
        }
    }
}
