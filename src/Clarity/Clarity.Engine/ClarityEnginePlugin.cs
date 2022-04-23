using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Clarity.Engine
{
    /// <summary>
    /// パラメータ基底・・・共通の外だしともいう
    /// </summary>
    public class ClarityEngineParam
    {
        /// <summary>
        /// 管理コントロール
        /// </summary>
        public Control Con = null;
    }

    /// <summary>
    /// 初期化パラメータ
    /// </summary>
    public class ClarityEngineInitParam : ClarityEngineParam
    {
        /// <summary>
        /// 描画領域サイズ
        /// </summary>
        //public Size RenderingViewSize = new Size(800, 600);
    }

    /// <summary>
    /// 周回処理パラメータ
    /// </summary>
    public class ClarityEngineCyclingParam : ClarityEngineParam
    {
        public FrameInfo Frame;        
    }

    /// <summary>
    /// ClarityEngine追加動作
    /// </summary>
    public abstract class ClarityEnginePlugin : IDisposable
    {
        /// <summary>
        /// 初期化動作を定義する
        /// </summary>
        public virtual void Init(ClarityEngineInitParam pdata)
        {
        }

        /// <summary>
        /// 周回処理動作を定義する
        /// </summary>
        public virtual void CyclingProc(ClarityEngineCyclingParam pdata)
        {
        }


        /// <summary>
        /// Viewのサイズ変更処理
        /// </summary>
        public virtual void ResizeView(Size ns)
        {
        }


        /// <summary>
        /// 破棄
        /// </summary>
        public virtual void Dispose()
        {
        }

        
    }
}
