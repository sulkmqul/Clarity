using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortice.Direct2D1;
using Vortice.Direct3D11;

namespace Clarity.Engine.Core
{
    /// <summary>
    /// Direct2D管理
    /// </summary>
    public class Dx2D : IDisposable
    {
        public Vortice.Direct2D1.ID2D1Factory1 Fact2D = null;
        public Vortice.DirectWrite.IDWriteFactory1 FactDWrite = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Direct2Dの初期化
        /// </summary>
        /// <param name="swc"></param>
        public void InitDirect2D()
        {
            this.Fact2D = D2D1.D2D1CreateFactory<Vortice.Direct2D1.ID2D1Factory1>(FactoryType.SingleThreaded);
            this.FactDWrite = Vortice.DirectWrite.DWrite.DWriteCreateFactory<Vortice.DirectWrite.IDWriteFactory1>();
        }

        /// <summary>
        /// RenderTarget2Dの作成
        /// </summary>
        /// <param name="rvt"></param>
        /// <returns></returns>
        public Vortice.Direct2D1.ID2D1RenderTarget CreateRenderTarget2D(ID3D11Texture2D rvt)
        {
            Vortice.Direct2D1.ID2D1RenderTarget ans = null;

            using (var su = rvt.QueryInterface<Vortice.DXGI.IDXGISurface>())
            {
                RenderTargetProperties rtp = new RenderTargetProperties();
                rtp.PixelFormat = new Vortice.DCommon.PixelFormat(Vortice.DXGI.Format.Unknown, Vortice.DCommon.AlphaMode.Premultiplied);
                ans = this.Fact2D.CreateDxgiSurfaceRenderTarget(su, rtp); 
            }


            //アンチエイリアスモード
            //テキスト描画以外のアンチエイリアスモード(D2D1_ANTIALIAS_MODE)
            ans.AntialiasMode = AntialiasMode.PerPrimitive;     //PerPrimitiveはhigh-quality antialiasing.らしい

            //テキストのアンチエイリアスモード(D2D1_TEXT_ANTIALIAS_MODE )
            ans.TextAntialiasMode = TextAntialiasMode.Cleartype;

            return ans;
        }

        

        /// <summary>
        /// 解放されるとき
        /// </summary>
        public void Dispose()
        {
            this.FactDWrite?.Dispose();            
            this.Fact2D?.Dispose();            
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    }
}
