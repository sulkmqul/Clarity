using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{

    /*class RendererStatement
    {
    }*/


    /// <summary>
    /// 深度ステンシル無効化
    /// </summary>
    public class DepthStencilDisabledState : IDisposable
    {
        public DepthStencilDisabledState()
        {
            Core.DxManager.Mana.DisabledDepthStencil();
        }

        public void Dispose()
        {
            Core.DxManager.Mana.EnabledDepthStencil();
        }
    }


    /// <summary>
    /// αブレンド無効化
    /// </summary>
    public class AlphaBlendDisabledState : IDisposable
    {
        public AlphaBlendDisabledState()
        {
            Core.DxManager.Mana.DisabledAlphaBlend();
        }

        public void Dispose()
        {
            Core.DxManager.Mana.EnabledAlphaBlendNormal();
        }
    }


    /// <summary>
    /// 加算合成有効化
    /// </summary>
    public class AlphaBlendPlusEnabledState : IDisposable
    {
        public AlphaBlendPlusEnabledState()
        {
            Core.DxManager.Mana.EnabledAlphaBlendPlus();
        }

        public void Dispose()
        {
            Core.DxManager.Mana.EnabledAlphaBlendNormal();
        }
    }
}
