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
        /// <summary>
        /// Dirext2D Factory
        /// </summary>
        private Vortice.Direct2D1.ID2D1Factory1? _Fact2D = null;

        /// <summary>
        /// Dirext2D Factory
        /// </summary>
        public Vortice.Direct2D1.ID2D1Factory1 Fact2D
        {
            get
            {
                if (_Fact2D == null)
                {
                    throw new Exception("Dx2D factory is not Initalized.");
                }
                return this._Fact2D;
            }
        }

        /// <summary>
        /// WriteFactory
        /// </summary>
        private  Vortice.DirectWrite.IDWriteFactory1? _FactDWrite = null;

        /// <summary>
        /// WriteFactory
        /// </summary>
        public Vortice.DirectWrite.IDWriteFactory1 FactDWrite
        {
            get
            {
                if(this._FactDWrite == null)
                {
                    throw new Exception("Dx2D write factory is not Initalized.");
                }
                return this._FactDWrite;
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Direct2Dの初期化
        /// </summary>
        /// <param name="swc"></param>
        public void InitDirect2D()
        {
            this._Fact2D = D2D1.D2D1CreateFactory<Vortice.Direct2D1.ID2D1Factory1>(FactoryType.SingleThreaded);
            this._FactDWrite = Vortice.DirectWrite.DWrite.DWriteCreateFactory<Vortice.DirectWrite.IDWriteFactory1>();
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
            this._FactDWrite?.Dispose();            
            this._Fact2D?.Dispose();            
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    }
}
