using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Engine.Core;
using Vortice.Direct3D;

namespace Clarity.Engine
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
            DxManager.Mana.DisabledDepthStencil();
        }

        public void Dispose()
        {
            DxManager.Mana.EnabledDepthStencil();
        }
    }


    /// <summary>
    /// αブレンド無効化
    /// </summary>
    public class AlphaBlendDisabledState : IDisposable
    {
        public AlphaBlendDisabledState()
        {
            DxManager.Mana.DisabledAlphaBlend();
        }

        public void Dispose()
        {
            DxManager.Mana.EnabledAlphaBlendNormal();
        }
    }


    /// <summary>
    /// 加算合成有効化
    /// </summary>
    public class AlphaBlendPlusEnabledState : IDisposable
    {
        public AlphaBlendPlusEnabledState()
        {
            DxManager.Mana.EnabledAlphaBlendPlus();
        }

        public void Dispose()
        {
            DxManager.Mana.EnabledAlphaBlendNormal();
        }
    }


    /// <summary>
    /// 加算合成State
    /// </summary>
    public class AlphaBlendPlusState : IDisposable
    {
        public AlphaBlendPlusState()
        {
            DxManager.Mana.EnabledAlphaBlendPlus();
        }

        public void Dispose()
        {
            DxManager.Mana.EnabledAlphaBlendNormal();
        }
    }


    /// <summary>
    ///  描画primitiveステート
    /// </summary>
    public class PrimitiveTopologyLineState : IDisposable
    {
        public PrimitiveTopologyLineState()
        {
            DxManager.Mana.DxContext.IASetPrimitiveTopology(PrimitiveTopology.LineStrip);
        }

        public void Dispose()
        {
            DxManager.Mana.DxContext.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
        }
    }
}
