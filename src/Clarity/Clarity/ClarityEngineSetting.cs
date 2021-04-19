using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    /// <summary>
    /// エンジン設定ファイルデータ
    /// </summary>
    [Serializable]
    public class ClarityEngineSetting
    {
        /// <summary>
        /// ログレベル
        /// </summary>
        public EClarityLogLevel LogLevel = EClarityLogLevel.ALL;

        /// <summary>
        /// 複数ViewPort描画（画面分割）対応可否(false=一つだけ true=複数対応する)
        /// </summary>
        public bool MultiViewPort = false;

        /// <summary>
        /// 描画Viewのサイズ(画面サイズではない)
        /// </summary>
        public Size RenderingViewSize = new Size(800, 600);

        
    }
}
