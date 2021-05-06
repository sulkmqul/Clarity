using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using System.Windows.Forms;
using System.Drawing;

namespace Clarity.Core
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
            get;
            protected set;
        }


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
        protected RenderTargetView CurrentRenderTargetView
        {
            get
            {
                int index = (int)this.RenderTargetNo;
                return this.RenderTargetList[index];

            }
        }

        #region DX関連

        /// <summary>
        /// スワップチェイン設定
        /// </summary>
        protected SwapChain SwapChain = null;

        


        /// <summary>
        /// RenderTarget一式
        /// </summary>
        protected List<RenderTargetView> RenderTargetList = new List<RenderTargetView>();


        /// <summary>
        /// 深さView
        /// </summary>
        protected DepthStencilView DepthView = null;

        /// <summary>
        /// Rasterize方式
        /// </summary>
        protected RasterizerState RastState = null;


        #region AlphaBlend
        /// <summary>
        /// 通常のアルファブレンド
        /// </summary>
        BlendState AlphaNormal = null;

        /// <summary>
        /// 加算合成
        /// </summary>
        BlendState AlphaPlus = null;

        /// <summary>
        /// アルファブレンドしない
        /// </summary>
        BlendState AlphaDisabled = null;
        #endregion


        #region DepthStencil
        /// <summary>
        /// DepthStencilの有効化
        /// </summary>
        DepthStencilState DepthStencilEnabled = null;
        /// <summary>
        /// DepthStencilの無効化
        /// </summary>
        DepthStencilState DepthStencilDisabled = null;
        #endregion

        /// <summary>
        /// レンダリングテクスチャ用シェーダーリソース
        /// </summary>
        public ShaderResourceView RenderingTextureResource
        {
            get;
            protected set;
        }
        #endregion

        #endregion

        /// <summary>
        /// Direct3D11デバイス
        /// </summary>
        public SharpDX.Direct3D11.Device DxDevice = null;

        //-----------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SharpDXの初期化
        /// </summary>
        /// <param name="mc"></param>        
        private void InitDX(Control mc)
        {
            try
            {
                this.MCont = mc;

                #region スワップチェインとデバイス設定
                //スワップチェインの設定
                SwapChainDescription swc = new SwapChainDescription();
                {

                    //スワップチェインのバッファ数。特に困らないのであれば1
                    swc.BufferCount = 1;

                    //表示モード
                    ModeDescription mder = new ModeDescription()
                    {
                        //解像度
                        Width = this.WindowSize.Width,
                        Height = this.WindowSize.Height,

                        //リフレッシュレート
                        RefreshRate = new Rational(60, 1),

                        //表示フォーマット
                        Format = Format.R8G8B8A8_UNorm,

                    };
                    swc.ModeDescription = mder;

                    //ウィンドウかフルスクリーンか　falseでフルスクリーン。
                    swc.IsWindowed = true;

                    //表示ウィンドウハンドル
                    swc.OutputHandle = this.MCont.Handle;

                    //スワップチェインのマルチサンプルパラメータ
                    swc.SampleDescription = new SampleDescription()
                    {
                        Count = 1,      //ピクセル単位のマルチサンプル数
                        Quality = 0,    //イメージの品質 0～ID3D10Device::CheckMultiSampleQualityLevels -1の値まで。高いほどクオリティが高いが遅くなる。
                    };

                    //サーフェイス処理後の動作
                    swc.SwapEffect = SwapEffect.Discard;

                    //サーフェイス使用法
                    swc.Usage = Usage.RenderTargetOutput;
                }

                #endregion


                //デバイス作成
                //作成するデバイスの種類
                //有効にするランタイムレイアリスト
                //スワップチェイン設定
                //デバイス取得
                //スワップチェイン取得                            
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swc, out this.DxDevice, out this.SwapChain);
                

                //イベントを無効にする
                //DirectXはAlt + Enterで自動でフルスクリーンにする機能がある。これで無効化できる。フルスクリーンの方法が見つかったら消すこと。
                //詳しくはMakeWindowAssociationでサーチ
                Factory fac = this.SwapChain.GetParent<Factory>();
                fac.MakeWindowAssociation(this.MCont.Handle, WindowAssociationFlags.IgnoreAll);

                //描画領域の初期化
                this.InitRenderTarget();

                //ラスタライズ方式初期化            
                RasterizerStateDescription rastdec = new RasterizerStateDescription();
                rastdec.CullMode = CullMode.Front;   //カリング可否（裏描画可否）
                rastdec.FillMode = FillMode.Solid;  //塗りつぶすか、ワイヤー表示か。
                rastdec.IsDepthClipEnabled = true;  //Ｚクリップ有効

                //ラスタライズクラス作成と設定
                this.RastState = new RasterizerState(this.DxDevice, rastdec);
                this.DxDevice.ImmediateContext.Rasterizer.State = this.RastState;

                //三角ポリンゴンの描画
                this.DxDevice.ImmediateContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
                

                //アルファブレンドの初期化
                this.InitAlphaBlend();

                //Zバッファの初期化
                this.InitDepthStencilState();
            }
            catch (Exception e)
            {
                throw e;
            }
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
            using (Texture2D backbuf = Texture2D.FromSwapChain<Texture2D>(this.SwapChain, 0))
            {
                RenderTargetView swapchainview = new RenderTargetView(this.DxDevice, backbuf);
                //ADD
                this.RenderTargetList.Add(swapchainview);
            }

            //////////////////////////////////////////////////////////////////////////////////////////
            //Ｚバッファ初期化
            //元はD3D11_TEXTURE2D_DESC
            Texture2DDescription depdec = new Texture2DDescription();
            #region Zバッファ領域テクスチャの初期化
            depdec.Format = Format.D32_Float_S8X24_UInt;    //バッファフォーマット
            depdec.ArraySize = 1;       //テクスチャの数
            depdec.MipLevels = 1;       //ミップレベル 基本1

            depdec.Width = this.WindowSize.Width;     //テクスチャサイズＷ
            depdec.Height = this.WindowSize.Height;   //テクスチャサイズH
            depdec.SampleDescription = new SampleDescription(1, 0); //マルチサンプリングの値、count=1pixelあたりのサンプル数、quality=クオリティ0～  CheckMultiSampleQualityLevels  - 1まで
            depdec.Usage = ResourceUsage.Default;   //使い方：基本default
            depdec.BindFlags = BindFlags.DepthStencil;      //深度ステンシルとして使用。
            depdec.CpuAccessFlags = CpuAccessFlags.None;    //許可するCPUアクセス、基本none
            depdec.OptionFlags = ResourceOptionFlags.None;  //基本none
            #endregion



            //Zバッファ領域作成
            using (Texture2D depbuf = new Texture2D(this.DxDevice, depdec))
            {
                DepthStencilViewDescription dsdes = new DepthStencilViewDescription();
                dsdes.Format = depdec.Format;
                dsdes.Dimension = DepthStencilViewDimension.Texture2D;
                dsdes.Texture2D.MipSlice = 0;

                //Z View作成
                this.DepthView = new DepthStencilView(this.DxDevice, depbuf, dsdes);
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
                using (Texture2D tex = new Texture2D(this.DxDevice, rentexdec))
                {

                    //レンダーターゲットの生成
                    RenderTargetView rentexrt = new RenderTargetView(this.DxDevice, tex);
                    //ADD
                    this.RenderTargetList.Add(rentexrt);


                    //テクスチャの生成
                    this.RenderingTextureResource = new ShaderResourceView(this.DxDevice, tex);
                }

            }
            #endregion


            //初期切り替え
            this.DxDevice.ImmediateContext.OutputMerger.SetTargets(this.DepthView, this.RenderTargetList[0]);

        }


        /// <summary>
        /// RenderTargetに関する情報を削除する
        /// </summary>
        private void ReleaseRenderTarget()
        {
            //開放
            foreach (RenderTargetView tv in this.RenderTargetList)
            {
                tv.Dispose();
            }
            this.RenderTargetList.Clear();

            //DepthView開放
            this.DepthView.Dispose();
            this.DepthView = null;

            //ShaderResource開放
            this.RenderingTextureResource.Dispose();
        }


        /// <summary>
        /// アルファブレンドの初期化
        /// </summary>
        private void InitAlphaBlend()
        {
            {
                //アルファブレンド通常
                BlendStateDescription bds = new BlendStateDescription();
                RenderTargetBlendDescription rtbs = new RenderTargetBlendDescription(true, BlendOption.SourceAlpha, BlendOption.InverseSourceAlpha, BlendOperation.Add, BlendOption.One, BlendOption.Zero, BlendOperation.Add, ColorWriteMaskFlags.All);
                bds.RenderTarget[0] = rtbs;

                this.AlphaNormal = new BlendState(this.DxDevice, bds);
            }

            {
                //アルファブレンド加算
                BlendStateDescription bds = new BlendStateDescription();
                RenderTargetBlendDescription rtbs = new RenderTargetBlendDescription(true, BlendOption.SourceAlpha, BlendOption.One, BlendOperation.Add, BlendOption.One, BlendOption.Zero, BlendOperation.Add, ColorWriteMaskFlags.All);
                bds.RenderTarget[0] = rtbs;

                this.AlphaPlus = new BlendState(this.DxDevice, bds);
            }

            {
                //アルファブレンドしない
                BlendStateDescription bds = new BlendStateDescription();
                RenderTargetBlendDescription rtbs = new RenderTargetBlendDescription(false, BlendOption.SourceAlpha, BlendOption.InverseSourceAlpha, BlendOperation.Add, BlendOption.One, BlendOption.Zero, BlendOperation.Add, ColorWriteMaskFlags.All);
                bds.RenderTarget[0] = rtbs;

                this.AlphaDisabled = new BlendState(this.DxDevice, bds);
            }
        }


        /// <summary>
        /// DepthStencil状態の初期化
        /// </summary>
        private void InitDepthStencilState()
        {
            //有効化
            {
                DepthStencilStateDescription desc = new DepthStencilStateDescription();
                desc.IsDepthEnabled = true;
                desc.DepthComparison = Comparison.LessEqual;
                desc.DepthWriteMask = DepthWriteMask.All;
                desc.IsStencilEnabled = false;

                this.DepthStencilEnabled = new DepthStencilState(this.DxDevice, desc);
            }
            //無効化
            {
                DepthStencilStateDescription desc = new DepthStencilStateDescription();
                desc.IsDepthEnabled = false;
                desc.IsStencilEnabled = false;
                this.DepthStencilDisabled = new DepthStencilState(this.DxDevice, desc);
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------
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

                DxManager.Instance.GameSize = gsize;


                //初期化
                DxManager.Instance.InitDX(mc);

                //デフォルト設定
                //背景透過
                DxManager.Mana.EnabledAlphaBlendNormal();

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
            this.DxDevice.ImmediateContext.OutputMerger.SetTargets(this.DepthView, this.CurrentRenderTargetView);
        }


        /// <summary>
        /// 描画Viewのクリア
        /// </summary>
        /// <param name="col">クリア色</param>
        /// <returns>性交可否</returns>
        public void ClearTargetView(Color4 col)
        {
            DeviceContext cont = this.DxDevice.ImmediateContext;

            //ターゲットクリア
            cont.ClearDepthStencilView(this.DepthView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
            cont.ClearRenderTargetView(this.CurrentRenderTargetView, col);

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
            DxManager.Instance.DxDevice.ImmediateContext.OutputMerger.SetBlendState(this.AlphaNormal);
        }
        /// <summary>
        /// アルファブレンドの無効化
        /// </summary>
        public void DisabledAlphaBlend()
        {

            DxManager.Instance.DxDevice.ImmediateContext.OutputMerger.SetBlendState(this.AlphaDisabled);
        }

        /// <summary>
        /// アルファブレンドの有効化(加算合成)
        /// </summary>
        public void EnabledAlphaBlendPlus()
        {
            DxManager.Instance.DxDevice.ImmediateContext.OutputMerger.SetBlendState(this.AlphaPlus);
        }

        /// <summary>
        /// Zバッファ有効化
        /// </summary>
        public void EnabledDepthStencil()
        {
            DxManager.Instance.DxDevice.ImmediateContext.OutputMerger.SetDepthStencilState(this.DepthStencilEnabled);
        }

        /// <summary>
        /// Zバッファ無効化
        /// </summary>
        public void DisabledDepthStencil()
        {
            DxManager.Instance.DxDevice.ImmediateContext.OutputMerger.SetDepthStencilState(this.DepthStencilDisabled);
        }


        /// <summary>
        /// サイズの再作成
        /// </summary>
        public void ResizeSwapChain()
        {
            int w = this.WindowSize.Width;
            int h = this.WindowSize.Height;

            this.ReleaseRenderTarget();

            this.SwapChain.ResizeBuffers(1, w, h, Format.R8G8B8A8_UNorm, SwapChainFlags.None);
            
            this.InitRenderTarget();
        }

        /// <summary>
        /// 破棄されるとき
        /// </summary>
        public void Dispose()
        {
            //レンダリングテクスチャリソース
            this.RenderingTextureResource?.Dispose();

            //RenderTargetの開放
            foreach (RenderTargetView tv in this.RenderTargetList)
            {
                tv.Dispose();
            }
            this.RenderTargetList.Clear();


            //ステート
            this.RastState?.Dispose();

            //SwapChain
            this.SwapChain?.Dispose();

            //Device
            this.DxDevice?.Dispose();


            //解放
            DxManager.Instance = null;
        }
    }


    /// <summary>
    /// 加算合成State
    /// </summary>
    internal class AlphaBlendPlusState : IDisposable
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
    /// アルファブレンド無効State
    /// </summary>
    internal class AlphaBlendDisabledState : IDisposable
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
}
