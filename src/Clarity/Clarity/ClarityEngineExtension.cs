using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    /// <summary>
    /// Clarityエンジン基本IF
    /// </summary>
    public class ClarityEngineExtension : IDisposable
    {
        /// <summary>
        /// 初期化動作を定義する
        /// </summary>
        public virtual void Init(ClarityInitParam pdata) { }

        /// <summary>
        /// 周回処理動作を定義する
        /// </summary>
        public virtual void CyclingProc(ClarityCyclingProcParam pdata) { }

        /// <summary>
        /// 関数
        /// </summary>
        public virtual void Dispose() { }

        /// <summary>
        /// Viewのサイズ変更処理
        /// </summary>
        public virtual void ResizeView()
        {
            ClarityEngine.ResetView();
        }
    }
}
