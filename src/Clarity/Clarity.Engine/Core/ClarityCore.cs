using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using Vortice.Mathematics;

namespace Clarity.Engine.Core
{


    /// <summary>
    /// FPS計算用データ
    /// </summary>
    class FrameRateCalcuData
    {
        /// <summary>
        /// 前回FPS計算した時間
        /// </summary>
        public long PrevCalcuMs = 0;

        /// <summary>
        /// 処理回数
        /// </summary>
        public float ProcCount = 0;

        /// <summary>
        /// 描画回数
        /// </summary>
        public float RenderCount = 0;

        /// <summary>
        /// srcdataのFPSを計算する
        /// </summary>
        /// <param name="nowms">今回の計算時間</param>
        /// <param name="srcdata">元データ</param>
        /// <returns></returns>
        public (float proc, float render) CalcuFPS(long nowms)
        {
            //経過時間の算出
            long span = nowms - this.PrevCalcuMs;
            //秒に変換
            float spanf = (float)span * 0.001f;


            float prc_fps = this.ProcCount / spanf;
            float render_fps = this.RenderCount / spanf;

            return (prc_fps, render_fps);
        }
    }


    /// <summary>
    /// Clarityエンジンコア処理
    /// </summary>
    internal class ClarityCore : IDisposable
    {
        public ClarityCore()
        {
            
        }

        class ClarityCoreData
        {
            /// <summary>
            /// 管理コントロール
            /// </summary>
            public Control Con;

            /// <summary>
            /// クリア色
            /// </summary>
            public Color4 ViewClearColor = new Color4(1.0f, 0.0f, 0.0f, 0.0f);

            /// <summary>
            /// 実行プラグイン
            /// </summary>
            public ClarityEnginePlugin? ExProc = null;

            /// <summary>
            /// SwapChainに描画する物体一覧
            /// </summary>
            public Element.ClarityStructure SwapChainElement;
            /// <summary>
            /// 描画View
            /// </summary>
            public SystemViewElement SystemView;

            /// <summary>
            /// 当たり判定描画可否
            /// </summary>
            public bool RenderColliderFlag = false;
            /// <summary>
            /// 当たり判定描画所作
            /// </summary>
            public RenderColliderBehavior RenderColBh;
        }

        /// <summary>
        /// エンジンデータ
        /// </summary>
        private ClarityCoreData FData;


        public Color4 ClearColor
        {
            set
            {
                this.FData.ViewClearColor = value;
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// エンジン初期化
        /// </summary>
        /// <param name="con">管理コントロール</param>        
        public void Init(Control con)
        {
            this.FData = new ClarityCoreData()
            {
                Con = con,
            };

            //管理クラス初期化
            this.CreateEngineManagers();

            //Buildinデータ読み込み
            BuildInData.LoadBuildInData();

            this.FData.SwapChainElement = new Element.ClarityStructure("SwapChain", 0);

            //システムViewの作成
            this.FData.SystemView = new SystemViewElement();
            this.FData.SystemView.InitSystemView();

            this.FData.SwapChainElement.AddChild(this.FData.SystemView);

            //リサイズ処理の設定
            this.FData.Con.Resize += Control_Resize;

            //その他初期情報の取得
            {
                //当たり判定描画可否
                this.FData.RenderColliderFlag = ClarityEngine.EngineSetting.GetBool("Debug.Collider.Visible");
                this.FData.RenderColBh = new RenderColliderBehavior();
            }
        }

        

        /// <summary>
        /// ClarityEngineメイン処理開始
        /// </summary>
        /// <param name="cep">追加動作</param>
        public void StartClarity(ClarityEnginePlugin? cep)
        {
            this.FData.ExProc = cep;

            

            //初期化
            ClarityEngineInitParam ceip = new ClarityEngineInitParam() { Con = this.FData.Con };
            this.FData.ExProc?.Init(ceip);

            //メインループ
            this.Loop();

            //
            this.FData.ExProc?.Dispose();
        }

        


        /// <summary>
        /// 解放されるとき
        /// </summary>
        public void Dispose()
        {
            //オブジェクト管理            
            ElementManager.Clear();

            //世界管理
            WorldManager.Mana?.Dispose();

            //入力管理
            //InputManager.Mana.Dispose();

            //テクスチャ
            Texture.TextureManager.Mana?.Dispose();

            //頂点
            Vertex.VertexManager.Mana?.Dispose();

            //Shader管理
            Shader.ShaderManager.Mana?.Dispose();

            //DirectX
            DxManager.Mana?.Dispose();
        }

        /// <summary>
        /// SwapChainに直接描画する物体の追加
        /// </summary>
        /// <param name="data"></param>
        internal void AddSwapChainElement(BaseElement data)
        {
            this.FData.SwapChainElement.AddChild(data);
        }


        /// <summary>
        /// 公開用処理
        /// </summary>
        internal void Process(FrameInfo f)
        {
            this.ProcFrame(f);
        }

        /// <summary>
        /// 公開用描画
        /// </summary>
        internal void Rendering()
        {
            this.RenderFrame();
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 実行ループ処理
        /// </summary>
        private void Loop()
        {
            //FPS計算用
            FrameRateCalcuData fps = new FrameRateCalcuData();
            
            //時間管理開始
            ClarityTimeManager.Mana?.Start();

            long prev_time = 0;

            float limittile = ClarityEngine.EngineSetting.GetFloat("FrameTimeLimit");
            double nexttime = limittile;

            //デバッグ可否
            bool deflag = ClarityEngine.EngineSetting.GetBool("Debug.Enabled");

            //実行ループ
            ClarityLoop.Run(this.FData.Con, () =>
            {
                //時間経過判断
                long time = ClarityTimeManager.TotalMilliseconds;
                if (nexttime > time)
                {
                    System.Threading.Thread.Sleep(1);
                    return;
                }

                FrameInfo finfo = new FrameInfo(time, time - prev_time);

                //フレーム処理
                this.ProcFrame(finfo);
                fps.ProcCount++;


                this.FData.ExProc?.CyclingProc(new ClarityEngineCyclingParam() { Con = this.FData.Con, Frame = finfo });

                //フレーム描画処理
                //if (renderskip == false)
                {
                    this.RenderFrame();
                    fps.RenderCount++;                    
                }

                //今回のフレーム時間を保存
                prev_time = finfo.FrameTime;


                

                #region FPSの計算
                if(deflag == true)
                {
                    long fpsbasetime = ClarityTimeManager.TotalMilliseconds;
                    long dur = fpsbasetime - fps.PrevCalcuMs;
                    if (dur > 1000)
                    {
                        //FPSの表示
                        var data = fps.CalcuFPS(fpsbasetime);
                        string fpsstring = string.Format("Proc:{0:F} Render:{1:F}", data.proc, data.render);                        
                        ClarityEngine.SetSystemTextForEngine(fpsstring, 0);

                        //現在の管理object数の計算
                        int m = ElementManager.Mana.CountElement();
                        ClarityEngine.SetSystemTextForEngine($"Object Count ={m}", 1);

                        //初期化
                        fps.ProcCount = 0;
                        fps.RenderCount = 0;
                        fps.PrevCalcuMs = fpsbasetime;
                    }
                }
                #endregion

                nexttime += limittile;

                //ちょい待ち
                System.Threading.Thread.Sleep(1);
            });
        }


        /// <summary>
        /// フレーム処理
        /// </summary>
        private void ProcFrame(FrameInfo finfo)
        {
            //入力情報の取得
            InputManager.Mana.GetInput();

            //処理
            ElementManager.Mana.Proc(finfo);

            //処理外SwapChaiの処理
            this.FData.SwapChainElement.Proc(0, finfo);
        }


        /// <summary>
        /// フレーム描画処理
        /// </summary>
        private void RenderFrame()
        {
            //RenderingTextureに対するゲーム描画
            this.RenderGame();

            //システム描画
            this.RenderSwapChain();

            DxManager.Mana?.SwapChainPresent();
        }



        /// <summary>
        /// ゲーム描画
        /// </summary>
        private void RenderGame()
        {
            //描画基礎設定
            //DxManager.Mana.DisabledAlphaBlend();

            //描画対象            
            DxManager.Mana.ChangeRenderTarget(DxManager.ERenderTargetNo.RenderingTexture);
            DxManager.Mana.BeginRendering(this.FData.ViewClearColor);

            {
                ////処理の設定
                //WorldData wd = WorldManager.Mana.Get(0);                
                //DxManager.Mana.DxContext.RSSetViewport(wd.VPort.VPort);

                //描画処理
                ElementManager.Mana.Render();

                //当たり判定の描画
                if (this.FData.RenderColliderFlag == true)
                {
                    using (DepthStencilDisabledState ds = new DepthStencilDisabledState())
                    {
                        ElementManager.Mana.RenderColliderInfo(0, this.FData.RenderColBh);
                    }
                }

            }
            DxManager.Mana.EndRendering();
        }

        /// <summary>
        /// SwapChainの描画
        /// </summary>
        private void RenderSwapChain()
        {
            //SwapChainへ切り替え
            DxManager.Mana.ChangeRenderTarget(DxManager.ERenderTargetNo.SwapChain);
            DxManager.Mana.BeginRendering(new Vortice.Mathematics.Color4(0.0f, 1.0f, 0.0f));

            //ViewPort設定
            WorldData wd = WorldManager.Mana.Get(WorldManager.SystemViewID);
            DxManager.Mana.DxContext.RSSetViewport(wd.VPort.VPort);

            //描画リソース取得
            //var texres = DxManager.Mana.SystemViewTextureResource;

            //SwapChainへの描画物の描画
            this.FData.SwapChainElement.Render(0, 0);

            DxManager.Mana.EndRendering();
        }



        /// <summary>
        /// 管理クラスの作成
        /// </summary>
        private void CreateEngineManagers()
        {
            #region エンジン管理クラスの作成
            //DirectXの初期化           
            Vector2 rvsize = ClarityEngine.EngineSetting.GetVec2("RenderingViewSize");
            bool frvf = ClarityEngine.EngineSetting.GetBool("CE.FixedRenderingViewSize");
            System.Drawing.Size? gsize = null;
            if (frvf == true)
            {
                gsize = new System.Drawing.Size((int)rvsize.X, (int)rvsize.Y);
            }
            DxManager.Init(this.FData.Con, gsize);

            //時間管理
            ClarityTimeManager.Create();

            //Shader            
            Shader.ShaderManager.Create();

            //頂点管理の作成
            Vertex.VertexManager.Create();

            //テクスチャ管理
            Texture.TextureManager.Create();

            //入力管理
            InputManager.Create();

            //世界管理
            WorldManager.Create();

            //object管理の作成
            ElementManager.Create();
            
            //シーン管理
            //Element.Scene.SceneManager.Manager = new Element.Scene.SceneManager();
            //Element.ElementManager.Mana.AddRequest(Element.Scene.SceneManager.Manager);

            #endregion

            //全体デフォルト
            Vector2 vsize = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize");
            WorldManager.Mana.CreateSystemViewWorld((int)vsize.X, (int)vsize.Y);

            //基本世界の作成
            this.CreateDefaultWorld();
        }


        /// <summary>
        /// デフォルト世界の作成(RenderingTexture内ゲームのデフォルト)
        /// </summary>
        private void CreateDefaultWorld()
        {
            Vector2 vsize = ClarityEngine.EngineSetting.GetVec2("RenderingViewSize");            
            
            
            //デフォルト世界の登録
            WorldData wdata = new WorldData();
            wdata.DefaultCameraMat = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -1000.0f), new Vector3(0.0f, 0.0f, 0.0f), -Vector3.UnitY);
            wdata.ProjectionMat = Matrix4x4.CreateOrthographic(vsize.X, vsize.Y, 1.0f, 15000.0f);

            //wdata.DefaultCameraMat = Matrix4x4.CreateLookAt(new Vector3(1000.0f, 1000.0f, -2000.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            //wdata.ProjectionMat = Matrix4x4.CreatePerspectiveFieldOfView((float)(Math.PI / 4), vsize.Y / vsize.X, 0.01f, 10000.0f);
            
            wdata.ReCalcu();

            //ViewPortの作成と登録
            Viewport vp = new Viewport(0, 0, vsize.X, vsize.Y, 0.0f, 1.0f);
            wdata.VPort = new ViewPortData() { VPort = vp };
            
            WorldManager.Mana.Set(0, wdata);



        }


        /// <summary>
        /// コントロールのリサイズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Control_Resize(object? sender, EventArgs e)
        {
            //サイズ0=最小化なら次の変更を待つのでとりあえず無視
            if(this.FData.Con.Width == 0 || this.FData.Con.Height == 0 )
            {
                return;
            }


            //デバイスの初期化を行う            
            DxManager.Mana?.ResizeSwapChain();

            //SystemViewの作り直し
            WorldManager.Mana?.CreateSystemViewWorld(this.FData.Con.Width, this.FData.Con.Height);

            //リサイズ処理
            this.FData.ExProc?.ResizeView(this.FData.Con.Size);
        }


    }
}
