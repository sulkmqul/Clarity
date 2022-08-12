using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vortice;
using Vortice.DXGI;
using Vortice.Direct3D11;
using Vortice.Direct3D;
using System.Windows.Forms;
using System.Drawing;
using Vortice.Mathematics;

namespace Clarity.Engine.Core
{
    

    /// <summary>
    /// DirectX管理コア
    /// </summary>
    internal class DxManager : BaseClaritySingleton<DxManager>, IDisposable
    {
        /// <summary>
        /// 描画場所 RenderTargetListの添え字に使えます
        /// </summary>
        public enum ERenderTargetNo
        {
            SwapChain = 0,
            RenderingTexture,
        }
        
        /// <summary>
        /// 描画ターゲットデータ
        /// </summary>
        class RenderTargetSet : IDisposable
        {
            /// <summary>
            /// Direct3D RenderTarget
            /// </summary>
            public ID3D11RenderTargetView Target3D = null;

            /// <summary>
            /// Direct2D Write RenderTarget
            /// </summary>
            //public ID3D11RenderTargetView Target2D = null;
            public Vortice.Direct2D1.ID2D1RenderTarget Target2D = null;

            /// <summary>
            /// 使用するDepthStencilView
            /// </summary>
            public ID3D11DepthStencilView DepthView = null;

            public void Dispose()
            {                
                this.Target3D?.Dispose();
                this.Target3D = null;

                this.Target2D?.Dispose();
                this.Target2D = null;

                this.DepthView?.Dispose();
                this.DepthView = null;
            }
        }


        #region メンバ変数

        /// <summary>
        /// 親コントロール(描画場所)
        /// </summary>
        protected Control MCont = null;

        /// <summary>
        /// ゲーム解像度
        /// </summary>
        public Size GameSize
        {
            get
            {
                if (this.GameSizeParam.Width < 0 || this.GameSize.Height < 0)
                {
                    return this.WindowSize;
                }

                return this.GameSizeParam;
            }
        }

        /// <summary>
        /// ゲーム解像度の設定値
        /// </summary>
        private Size GameSizeParam;

        /// <summary>
        /// 画面サイズ
        /// </summary>
        public Size WindowSize
        {
            get
            {
                return this.MCont.ClientSize;
            }
        }

        /// <summary>
        /// 選択RenderTarget番号
        /// </summary>
        protected ERenderTargetNo RenderTargetNo = ERenderTargetNo.SwapChain;

        /// <summary>
        /// 現在の選択RenderTargetを取得
        /// </summary>
        private RenderTargetSet CurrentRenderTarget
        {
            get
            {
                int index = (int)this.RenderTargetNo;
                return this.RenderTargetList[index];

            }
        }

        #region DX関連

        /// <summary>
        /// DXGI関係のものを作成するもの
        /// </summary>
        protected IDXGIFactory2 DxFactory;

        /// <summary>
        /// スワップチェイン設定
        /// </summary>
        protected IDXGISwapChain SwapChain = null;
        
        /// <summary>
        /// RenderTarget一式
        /// </summary>
        private List<RenderTargetSet> RenderTargetList = new List<RenderTargetSet>();


        /// <summary>
        /// 深さView
        /// </summary>
        //protected ID3D11DepthStencilView DepthView = null;

        /// <summary>
        /// Rasterize方式
        /// </summary>
        protected ID3D11RasterizerState RastState = null;


        #region AlphaBlend
        /// <summary>
        /// 通常のアルファブレンド
        /// </summary>
        ID3D11BlendState AlphaNormal = null;
        /// <summary>
        /// 加算合成
        /// </summary>
        ID3D11BlendState AlphaPlus = null;

        /// <summary>
        /// アルファブレンドしない
        /// </summary>
        ID3D11BlendState AlphaDisabled = null;
        #endregion


        #region DepthStencil
        /// <summary>
        /// DepthStencilの有効化
        /// </summary>
        ID3D11DepthStencilState DepthStencilEnabled = null;

        /// <summary>
        /// DepthStencilの無効化
        /// </summary>
        ID3D11DepthStencilState DepthStencilDisabled = null;
        #endregion

        /// <summary>
        /// レンダリングテクスチャ用シェーダーリソース
        /// </summary>
        public ID3D11ShaderResourceView SystemViewTextureResource
        {
            get;
            protected set;
        }
        
        #endregion

        #endregion

        /// <summary>
        /// Direct3D11デバイス
        /// </summary>
        private ID3D11Device _DxDevice = null;
        /// <summary>
        /// Direct3D11デバイス
        /// </summary>
        public ID3D11Device DxDevice
        {
            get
            {   
                return this._DxDevice;
            }
            
        }

        private ID3D11DeviceContext _DxContext = null;
        public ID3D11DeviceContext DxContext
        {
            get
            {
                return this._DxContext;
            }

        }


        /// <summary>
        /// Direct2D1管理
        /// </summary>
        private Dx2D D2DMana = null;


        public Vortice.DirectWrite.IDWriteFactory1 FactDWrite
        {
            get
            {
                return this.D2DMana.FactDWrite;
            }
        }

        public Vortice.Direct2D1.ID2D1RenderTarget CurrentTarget2D
        {
            get
            {
                return this.CurrentRenderTarget.Target2D;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 総合初期化
        /// </summary>
        /// <param name="mc">親画面</param>
        /// <param name="gsize">ゲーム解像度</param>
        public static void Init(Control mc, Size gsize)
        {
            try
            {
                //作成済みだった
                if (DxManager.Instance != null)
                {
                    throw new Exception("Already Created");
                }

                //作成
                DxManager.Instance = new DxManager();

                //解像度パラメータの設定
                DxManager.Instance.GameSizeParam = gsize;

                //初期化
                DxManager.Instance.InitDX(mc);

                //デフォルト設定
                //背景透過
                DxManager.Mana.EnabledAlphaBlendNormal();
                //Zバッファ有効
                DxManager.Mana.EnabledDepthStencil();

            }
            catch (Exception e)
            {
                throw new Exception("DXManager Initialize Exception", e);

            }
        }



        /// <summary>
        /// RenderTargetの切り替え
        /// </summary>
        /// <param name="tno">切り替えるTarget番号</param>
        public void ChangeRenderTarget(ERenderTargetNo tno)
        {
            //保存して切り替え
            this.RenderTargetNo = tno;

            //this.DisabledDepthStencil();
            RenderTargetSet cset = this.CurrentRenderTarget;
            this.DxContext.OMSetRenderTargets(cset.Target3D, cset.DepthView);
            //this.DxContext.OMSetRenderTargets(cset.Target3D, this.DepthView);
        }


        /// <summary>
        /// 描画の開始
        /// </summary>
        /// <param name="col">クリア色</param>
        /// <returns>成功可否</returns>
        public void BeginRendering(Color4 col)
        {
            ID3D11DeviceContext cont = this.DxContext;

            RenderTargetSet cset = this.CurrentRenderTarget;

            //ターゲットクリア
            //cont.ClearDepthStencilView(cset.DepthView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
            cont.ClearDepthStencilView(cset.DepthView, DepthStencilClearFlags.Depth, 1.0f, 0);
            cont.ClearRenderTargetView(cset.Target3D, col);

        }

        /// <summary>
        /// 描画の終了
        /// </summary>
        public void EndRendering()
        {
            RenderTargetSet cset = this.CurrentRenderTarget;
        }


        /// <summary>
        /// SwapChain描画
        /// </summary>
        public void SwapChainPresent()
        {
            this.SwapChain.Present(0, PresentFlags.None);
        }


        /// <summary>
        /// アルファブレンドの有効化(通常)
        /// </summary>
        public void EnabledAlphaBlendNormal()
        {
            this.DxContext.OMSetBlendState(this.AlphaNormal);
        }
        /// <summary>
        /// アルファブレンドの無効化
        /// </summary>
        public void DisabledAlphaBlend()
        {
            this.DxContext.OMSetBlendState(this.AlphaDisabled);
        }

        /// <summary>
        /// アルファブレンドの有効化(加算合成)
        /// </summary>
        public void EnabledAlphaBlendPlus()
        {
            this.DxContext.OMSetBlendState(this.AlphaPlus);
        }

        /// <summary>
        /// Zバッファ有効化
        /// </summary>
        public void EnabledDepthStencil()
        {
            this.DxContext.OMSetDepthStencilState(this.DepthStencilEnabled);
        }

        /// <summary>
        /// Zバッファ無効化
        /// </summary>
        public void DisabledDepthStencil()
        {
            this.DxContext.OMSetDepthStencilState(this.DepthStencilDisabled);
        }


        /// <summary>
        /// サイズの再作成
        /// </summary>
        public void ResizeSwapChain()
        {
            int w = this.WindowSize.Width;
            int h = this.WindowSize.Height;

            //SwapChainを使用しているViewを解放
            this.ReleaseRenderTarget();

            //SwapChainのリサイズ
            this.SwapChain.ResizeBuffers(1, w, h, Format.R8G8B8A8_UNorm, SwapChainFlags.None);
            
            //Viewの再作成
            this.InitRenderTarget();

        }


        /// <summary>
        /// 破棄されるとき
        /// </summary>
        public void Dispose()
        {
            //2Dの初期化
            //this.D2DMana?.Dispose();

            //RenderTarget関連の解放
            this.ReleaseRenderTarget();

            //ステート
            this.RastState?.Dispose();

            //SwapChain
            this.SwapChain?.Dispose();

            //Context
            this.DxContext?.Dispose();

            //Device
            this.DxDevice?.Dispose();

            //Factory
            this.DxFactory?.Dispose();

            //解放
            DxManager.Instance = null;
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// デバイスとswapchainの作成
        /// </summary>
        private void CreateDeviceSwapChain()
        {
            ID3D11Device tdev = null;
            FeatureLevel slev;
            ID3D11DeviceContext idx;
            
            //デバイスの作成
            SharpGen.Runtime.Result mret = D3D11.D3D11CreateDevice(null, DriverType.Hardware, DeviceCreationFlags.BgraSupport, null, out tdev, out slev, out idx);
            if (mret != SharpGen.Runtime.Result.Ok)
            {
                throw new Exception($"D3D11CreateDevice FAILED ret={mret}"); 

            }
            //this._DxDevice =  tdev.QueryInterface<ID3D11Device1>();
            this._DxDevice = tdev;
            this._DxContext = idx;

            SwapChainDescription1 swc = new SwapChainDescription1();
            {
                //スワップチェインのバッファ数。
                swc.BufferCount = 1;

                swc.Width = this.WindowSize.Width;
                swc.Height = this.WindowSize.Height;
                //表示フォーマット
                swc.Format = Format.R8G8B8A8_UNorm;

                //スワップチェインのマルチサンプルパラメータ
                swc.SampleDescription = new SampleDescription()
                {
                    Count = 1,      //ピクセル単位のマルチサンプル数
                    Quality = 0,    //イメージの品質 0～ID3D10Device::CheckMultiSampleQualityLevels -1の値まで。高いほどクオリティが高いが遅くなる。
                };

                //サーフェイス処理後の動作
                swc.SwapEffect = SwapEffect.Discard;

                swc.Scaling = Scaling.Stretch;

                //サーフェイス使用法
                swc.BufferUsage = Usage.RenderTargetOutput;
            }

            SwapChainFullscreenDescription fscdec = new SwapChainFullscreenDescription
            {
                Windowed = true
            };

            //swapchainの作成
            this.SwapChain = this.DxFactory.CreateSwapChainForHwnd(this.DxDevice, this.MCont.Handle, swc, fscdec);

        }

        /// <summary>
        /// DirectXの初期化
        /// </summary>
        /// <param name="mc">管理コントロール</param>
        private void InitDX(Control mc)
        {
            try
            {

                this.MCont = mc;

                this.DxFactory = DXGI.CreateDXGIFactory1<IDXGIFactory2>();

                //Swapchainとdeviceの作成
                this.CreateDeviceSwapChain();

                //イベントを無効にする
                //DirectXはAlt + Enterで自動でフルスクリーンにする機能がある。これで無効化できる。フルスクリーンの方法が見つかったら消すこと。
                //詳しくはMakeWindowAssociationでサーチ                
                this.DxFactory.MakeWindowAssociation(this.MCont.Handle, WindowAssociationFlags.IgnoreAltEnter);
                                

                //2D描画の初期化
                this.D2DMana = new Dx2D();
                this.D2DMana.InitDirect2D();


                //描画領域の初期化
                this.InitRenderTarget();

                //ラスタライズ方式初期化                
                Vortice.Direct3D11.RasterizerDescription rastdec = new RasterizerDescription(CullMode.Back, FillMode.Solid);
                rastdec.DepthClipEnable = true;
                rastdec.FrontCounterClockwise = true;   //ポリゴン正面の方向基底、時計回り正面＝true
                rastdec.ScissorEnable = false;          //シーザー矩形有効可否(描画領域設定)
                rastdec.MultisampleEnable = false;      //アンチエイリアスの方法指定らしい true=the quadrilateral line anti-aliasing algorithm、FALSE= the alpha line anti-aliasing algorithm.

                this.RastState = this.DxDevice.CreateRasterizerState(rastdec);

                //ラスタライズの設定
                this.DxContext.RSSetState(this.RastState);

                //三角ポリンゴンの描画                
                this.DxContext.IASetPrimitiveTopology(PrimitiveTopology.TriangleList);
                

                //アルファブレンドの初期化
                this.InitAlphaBlend();

                //Zバッファの初期化
                this.InitDepthStencilState();


            }
            catch (Exception ex)
            {
                throw new Exception("InitDX", ex);
            }
        }



        /// <summary>
        /// DepthStencilViewの作成
        /// </summary>
        /// <param name="dsize">作成サイズ</param>
        /// <returns></returns>
        private ID3D11DepthStencilView CreateDepthStencilView(Size dsize)
        {
            ID3D11DepthStencilView ans = null;
            //////////////////////////////////////////////////////////////////////////////////////////
            //Ｚバッファ初期化
            //元はD3D11_TEXTURE2D_DESC
            Texture2DDescription depdec = new Texture2DDescription();
            #region Zバッファ領域テクスチャの初期化
            depdec.Format = Format.D32_Float_S8X24_UInt;    //バッファフォーマット
            depdec.ArraySize = 1;       //テクスチャの数
            depdec.MipLevels = 1;       //ミップレベル 基本1

            depdec.Width = dsize.Width;     //テクスチャサイズＷ
            depdec.Height = dsize.Height;   //テクスチャサイズH
            depdec.SampleDescription = new SampleDescription(1, 0); //マルチサンプリングの値、count=1pixelあたりのサンプル数、quality=クオリティ0～  CheckMultiSampleQualityLevels  - 1まで
            depdec.Usage = ResourceUsage.Default;   //使い方：基本default
            depdec.BindFlags = BindFlags.DepthStencil;      //深度ステンシルとして使用。
            depdec.CpuAccessFlags = CpuAccessFlags.None;    //許可するCPUアクセス、基本none
            depdec.OptionFlags = ResourceOptionFlags.None;  //基本none
            #endregion

            //Zバッファ領域作成
            using (ID3D11Texture2D depbuf = this.DxDevice.CreateTexture2D(depdec))
            {
                DepthStencilViewDescription dsdes = new DepthStencilViewDescription();
                dsdes.Format = depdec.Format;
                dsdes.ViewDimension = DepthStencilViewDimension.Texture2D;
                dsdes.Texture2D.MipSlice = 0;

                ans = this.DxDevice.CreateDepthStencilView(depbuf, dsdes);
            }

            return ans;
        }


        /// <summary>
        /// 描画領域の初期化
        /// </summary>
        /// <returns></returns>
        private void InitRenderTarget()
        {
            //クリア
            this.RenderTargetList.Clear();

            //描画バッファの初期化            
            //using (Texture2D backbuf = Texture2D.FromSwapChain<Texture2D>(this.SwapChain, 0))
            using (ID3D11Texture2D backbuf = this.SwapChain.GetBuffer<ID3D11Texture2D>(0))
            {
                RenderTargetSet tset = new RenderTargetSet();

                //RenderTargetView swapchainview = new RenderTargetView(this.DxDevice, backbuf);
                ID3D11RenderTargetView swcv = this.DxDevice.CreateRenderTargetView(backbuf);
                tset.Target3D = swcv;
                tset.Target2D = this.D2DMana.CreateRenderTarget2D(backbuf);
                tset.DepthView = this.CreateDepthStencilView(this.WindowSize);

                //ADD
                this.RenderTargetList.Add(tset);
            }

            #region レンダリングテクスチャの作成
            {
                Texture2DDescription rentexdec = new Texture2DDescription();
                #region TextureDescriptionの作製
                rentexdec.Format = Format.B8G8R8A8_UNorm;    //バッファフォーマット　8bitRGBA
                rentexdec.ArraySize = 1;       //テクスチャの数
                rentexdec.MipLevels = 1;       //ミップレベル 基本1

                rentexdec.Width = this.GameSize.Width;     //テクスチャサイズＷ
                rentexdec.Height = this.GameSize.Height;   //テクスチャサイズH
                rentexdec.SampleDescription = new SampleDescription(1, 0); //マルチサンプリングの値、count=1pixelあたりのサンプル数、quality=クオリティ0～  CheckMultiSampleQualityLevels  - 1まで
                rentexdec.Usage = ResourceUsage.Default;   //使い方：基本default
                rentexdec.BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget;      //ShaderResource + RenderTagetとしての使用
                rentexdec.CpuAccessFlags = CpuAccessFlags.None;    //許可するCPUアクセス、基本none
                rentexdec.OptionFlags = ResourceOptionFlags.None;  //基本none
                #endregion

                //Texture作製
                using (ID3D11Texture2D tex = this.DxDevice.CreateTexture2D(rentexdec))
                //using (ID3D11Texture2D tex = this.DxDevice.CreateTexture2D(this.GameSize.Width, this.GameSize.Height, Format.R8G8B8A8_UNorm, 1, 1, null, BindFlags.ShaderResource | BindFlags.RenderTarget))
                {
                    RenderTargetSet tset = new RenderTargetSet();

                    //レンダーターゲットの生成                    
                    tset.Target3D = this.DxDevice.CreateRenderTargetView(tex);
                    tset.Target2D = this.D2DMana.CreateRenderTarget2D(tex);
                    tset.DepthView = this.CreateDepthStencilView(this.GameSize);
                    //ADD
                    this.RenderTargetList.Add(tset);

                    //テクスチャの生成
                    this.SystemViewTextureResource = this.DxDevice.CreateShaderResourceView(tex);
                }


            }
#endregion
            
            //初期切り替え
            this.ChangeRenderTarget(ERenderTargetNo.SwapChain);

        }


        /// <summary>
        /// RenderTargetに関する情報を削除する
        /// </summary>
        private void ReleaseRenderTarget()
        {
            //開放
            this.RenderTargetList.ForEach(x => x.Dispose());
            this.RenderTargetList.Clear();

            //DepthView開放
            //this.DepthView.Dispose();
            //this.DepthView = null;

            //ShaderResource開放
            this.SystemViewTextureResource.Dispose();
        }


        /// <summary>
        /// アルファブレンドの初期化
        /// </summary>
        private void InitAlphaBlend()
        {
            {
                //アルファブレンド通常
                //BlendStateDescription bds = new BlendStateDescription();
                //RenderTargetBlendDescription rtbs = new RenderTargetBlendDescription(true, BlendOption.SourceAlpha,
                //BlendOption.InverseSourceAlpha, BlendOperation.Add, BlendOption.One, BlendOption.Zero, BlendOperation.Add, ColorWriteMaskFlags.All);
                //bds.RenderTarget[0] = rtbs;

                //this.AlphaNormal = new BlendState(this.DxDevice, bds);

                BlendDescription bds = new BlendDescription(Blend.SourceAlpha, Blend.InverseSourceAlpha, Blend.One, Blend.Zero);
                bds.RenderTarget[0].IsBlendEnabled = true;
                bds.RenderTarget[0].SourceBlend = Blend.SourceAlpha;
                bds.RenderTarget[0].DestinationBlend = Blend.InverseSourceAlpha;
                bds.RenderTarget[0].BlendOperation = BlendOperation.Add;
                bds.RenderTarget[0].RenderTargetWriteMask = ColorWriteEnable.All;

                this.AlphaNormal = this.DxDevice.CreateBlendState(bds);

                

                
                
            }

            {
                //アルファブレンド加算
                //BlendStateDescription bds = new BlendStateDescription();
                //RenderTargetBlendDescription rtbs = new RenderTargetBlendDescription(true, BlendOption.SourceAlpha, BlendOption.One, BlendOperation.Add, BlendOption.One, BlendOption.Zero, BlendOperation.Add, ColorWriteMaskFlags.All);
                //bds.RenderTarget[0] = rtbs;
                //this.AlphaPlus = new BlendState(this.DxDevice, bds);

                BlendDescription bds = new BlendDescription(Blend.SourceAlpha, Blend.One, Blend.One, Blend.Zero);
                bds.RenderTarget[0].IsBlendEnabled = true;
                bds.RenderTarget[0].SourceBlend = Blend.SourceAlpha;
                bds.RenderTarget[0].DestinationBlend = Blend.One;
                bds.RenderTarget[0].BlendOperation = BlendOperation.Add;
                bds.RenderTarget[0].RenderTargetWriteMask = ColorWriteEnable.All;

                this.AlphaPlus = this.DxDevice.CreateBlendState(bds);
            }


            {
                //アルファブレンドしない
                //BlendStateDescription bds = new BlendStateDescription();
                //RenderTargetBlendDescription rtbs = new RenderTargetBlendDescription(false, BlendOption.SourceAlpha, BlendOption.InverseSourceAlpha, BlendOperation.Add, BlendOption.One, BlendOption.Zero, BlendOperation.Add, ColorWriteMaskFlags.All);
                //bds.RenderTarget[0] = rtbs;                
                //this.AlphaDisabled = new BlendState(this.DxDevice, bds);

                BlendDescription bds = new BlendDescription(Blend.SourceAlpha, Blend.InverseSourceAlpha, Blend.One, Blend.Zero);
                bds.RenderTarget[0].IsBlendEnabled = false;
                bds.RenderTarget[0].SourceBlend = Blend.SourceAlpha;
                bds.RenderTarget[0].DestinationBlend = Blend.InverseSourceAlpha;
                bds.RenderTarget[0].BlendOperation = BlendOperation.Add;
                bds.RenderTarget[0].RenderTargetWriteMask = ColorWriteEnable.All;

                this.AlphaDisabled = this.DxDevice.CreateBlendState(bds);


            }
        }


        /// <summary>
        /// DepthStencil状態の初期化
        /// </summary>
        private void InitDepthStencilState()
        {            
            //有効化
            {
                DepthStencilDescription desc = new DepthStencilDescription(true, DepthWriteMask.All, ComparisonFunction.LessEqual);
                desc.StencilEnable = false;

                this.DepthStencilEnabled = this.DxDevice.CreateDepthStencilState(desc);
            }
            //無効化
            {
                DepthStencilDescription desc = new DepthStencilDescription(false, DepthWriteMask.All, ComparisonFunction.LessEqual);
                desc.StencilEnable = false;

                this.DepthStencilDisabled = this.DxDevice.CreateDepthStencilState(desc);
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        
    }


    
}