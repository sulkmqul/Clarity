using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;

namespace Clarity.Core
{
    /// <summary>
    /// Direct2D管理
    /// </summary>
    class Dx2D : IDisposable
    {
        /// <summary>
        /// Direct2D描画の
        /// </summary>
        private SharpDX.Direct2D1.Factory Fact2D = null;

        /// <summary>
        /// 2D描画ターゲット
        /// </summary>
        public SharpDX.Direct2D1.RenderTarget RenderTarget2D = null;

        /// <summary>
        /// DirectWriteの生成者
        /// </summary>
        public SharpDX.DirectWrite.Factory FactDWrite = null;

        /// <summary>
        /// Direct2Dの初期化
        /// </summary>        
        /// <param name="swc">描画SwapChain null=default作成しない</param>
        public void InitDirect2D(SharpDX.DXGI.SwapChain swc = null)
        {

            //Factory作成
            this.Fact2D = new Factory(FactoryType.SingleThreaded);

            //RenderTarget2Dの作成
            if (swc != null)
            {
                this.CreateRenderTarget2D(swc);
            }

            //DirectWrite生成
            this.FactDWrite = new SharpDX.DirectWrite.Factory();
                        
        }


        /// <summary>
        /// SwapChainからRenderTarget2Dの作成
        /// </summary>
        /// <param name="swc"></param>
        private void CreateRenderTarget2D(SharpDX.DXGI.SwapChain swc)
        {
            //描画バッファの作成
            using (Texture2D btx = Texture2D.FromSwapChain<Texture2D>(swc, 0))
            {
                this.RenderTarget2D = this.CreateRenderTarget2D(btx);
            }
        }


        /// <summary>
        /// RenderTarget2Dの作成
        /// </summary>
        /// <param name="rvt"></param>
        /// <returns></returns>
        public SharpDX.Direct2D1.RenderTarget CreateRenderTarget2D(Texture2D rvt)
        {

            SharpDX.Direct2D1.RenderTarget ans = null;

            //描画ターゲットの作成
            using (var surface = rvt.QueryInterface<SharpDX.DXGI.Surface>())
            {
                //Premultipliedはalphaブレンド有効と同義
                ans = new RenderTarget(this.Fact2D, surface,
                new RenderTargetProperties(new PixelFormat(SharpDX.DXGI.Format.Unknown, AlphaMode.Premultiplied)));
            }


            //アンチエイリアスモード
            //テキスト描画以外のアンチエイリアスモード(D2D1_ANTIALIAS_MODE)
            ans.AntialiasMode = AntialiasMode.PerPrimitive;     //PerPrimitiveはhigh-quality antialiasing.らしい

            //テキストのアンチエイリアスモード(D2D1_TEXT_ANTIALIAS_MODE )
            ans.TextAntialiasMode = TextAntialiasMode.Cleartype;

            return ans;
        }


        /// <summary>
        /// RenderTarget2Dの解放
        /// </summary>
        public void ReleaseRenderTarget2D()
        {
            this.RenderTarget2D?.Dispose();
            this.RenderTarget2D = null;
        }



        /// <summary>
        /// 描画開始
        /// </summary>
        public void BeginDraw()
        {
            this.RenderTarget2D.BeginDraw();
        }

        /// <summary>
        /// 描画終了
        /// </summary>
        public void EndDraw()
        {
            this.RenderTarget2D.EndDraw();
        }


        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            this.FactDWrite?.Dispose();
            this.RenderTarget2D?.Dispose();
            this.Fact2D?.Dispose();
        }
    }
}
