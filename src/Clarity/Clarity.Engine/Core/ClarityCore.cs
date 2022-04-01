﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using Vortice.Mathematics;
using Clarity.Element;

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
            public Control Con = null;

            /// <summary>
            /// 実行プラグイン
            /// </summary>
            public ClarityEnginePlugin ExProc = null;

            /// <summary>
            /// 描画View
            /// </summary>
            public SystemViewElement SystemView = null;


            
        }

        /// <summary>
        /// エンジンデータ
        /// </summary>
        private ClarityCoreData FData = null;

        /// <summary>
        /// システム表示者
        /// </summary>
        public Element.TextObject SystemText = null;
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


            //システムViewの作成
            this.FData.SystemView = new SystemViewElement();
            this.FData.SystemView.InitSystemView();


            //システムテキストの初期化
            {
                string fontname = ClarityEngine.EngineSetting.GetString("Debug.SystemText.Font", "Arial");
                float fontsize = ClarityEngine.EngineSetting.GetFloat("Debug.SystemText.FontSize", 10.0f);
                this.SystemText = new Element.TextObject("", 0, fontname, fontsize);
                this.SystemText.Enabled = ClarityEngine.EngineSetting.GetBool("Debug.SystemText.Enabled", false);
                this.SystemText.TransSet.Pos2D = ClarityEngine.EngineSetting.GetVec2("Debug.SystemText.Pos", new Vector2(0.0f));
            }

        }

        /// <summary>
        /// ClarityEngineメイン処理開始
        /// </summary>
        /// <param name="cep">追加動作</param>
        public void StartClarity(ClarityEnginePlugin cep)
        {
            this.FData.ExProc = cep;

            //リサイズ処理の設定

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
            //Element.ElementManager.Mana.Clear(true);

            //世界管理
            WorldManager.Mana.Dispose();

            //入力管理
            //InputManager.Mana.Dispose();

            //テクスチャアニメーション
            Texture.TextureAnimeFactory.Mana.Dispose();
            //テクスチャ
            Texture.TextureManager.Mana.Dispose();

            //頂点
            Vertex.VertexManager.Mana.Dispose();

            //Shader管理
            Shader.ShaderManager.Mana.Dispose();

            //DirectX
            DxManager.Mana.Dispose();
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
            ClarityTimeManager.Mana.Start();

            long prev_time = 0;

            //実行ループ
            ClarityLoop.Run(this.FData.Con, () =>
            {
                long time = ClarityTimeManager.TotalMilliseconds;
                FrameInfo finfo = new FrameInfo() { FrameTime = time, Span = time - prev_time };

                //フレーム処理
                this.ProcFrame(finfo);
                fps.ProcCount++;

                //フレーム描画処理
                this.RenderFrame();
                fps.RenderCount++;
                
                //今回のフレーム時間を保存
                prev_time = finfo.FrameTime;

                #region FPSの計算
                {
                    long fpsbasetime = ClarityTimeManager.TotalMilliseconds;
                    long dur = fpsbasetime - fps.PrevCalcuMs;
                    if (dur > 500)
                    {
                        //計算
                        var data = fps.CalcuFPS(fpsbasetime);
                        string fpsstring = string.Format("Proc:{0:F} Render:{1:F}", data.proc, data.render);
                        //System.Diagnostics.Trace.WriteLine(fpsstring);
                        this.SystemText.SetText(fpsstring, 0);

                        //初期化
                        fps.ProcCount = 0;
                        fps.RenderCount = 0;
                        fps.PrevCalcuMs = fpsbasetime;
                    }
                }
                #endregion

                //ちょい待ち
                System.Threading.Thread.Sleep(1);
            });
        }


        /// <summary>
        /// フレーム処理
        /// </summary>
        private void ProcFrame(FrameInfo finfo)
        {
            //処理
            ElementManager.Mana.Proc(finfo);
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

            DxManager.Mana.SwapChainPresent();
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
            DxManager.Mana.BeginRendering(new Color4(0.0f, 1.0f, 1.0f, 1.0f));

            {
                //処理の設定
                WorldData wd = WorldManager.Mana.Get(0);                
                DxManager.Mana.DxContext.RSSetViewport(wd.VPort.VPort);

                //描画処理
                ElementManager.Mana.Render();
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
            DxManager.Mana.BeginRendering(new Vortice.Mathematics.Color4(0.0f, 0.0f, 0.0f));

            //ViewPort設定
            WorldData wd = WorldManager.Mana.Get(WorldManager.SystemViewID);
            DxManager.Mana.DxContext.RSSetViewport(wd.VPort.VPort);

            //描画リソース取得
            var texres = DxManager.Mana.SystemViewTextureResource;

            this.FData.SystemView.TransSet.Pos2D = new Vector2(0.0f, 0.0f);
            this.FData.SystemView.TransSet.Scale2D = new Vector2(wd.VPort.VPort.Width*0.8f, wd.VPort.VPort.Height*0.8f);
            this.FData.SystemView.Render(0, 0);

            //システムテキストの描画
            this.SystemText.Render(0, 0);

            DxManager.Mana.EndRendering();
        }



        /// <summary>
        /// 管理クラスの作成
        /// </summary>
        private void CreateEngineManagers()
        {
            #region エンジン管理クラスの作成
            //DirectXの初期化
            Vector2 rvsize = ClarityEngine.EngineSetting.GetVec2("RenderingViewSize", new Vector2(640.0f, 480.0f));
            DxManager.Init(this.FData.Con, new System.Drawing.Size((int)rvsize.X, (int)rvsize.Y));

            //時間管理
            ClarityTimeManager.Create();

            //Shader            
            Shader.ShaderManager.Create();

            //頂点管理の作成
            Vertex.VertexManager.Create();

            //テクスチャ管理
            Texture.TextureManager.Create();

            //テクスチャアニメ管理
            Texture.TextureAnimeFactory.Create();

            //入力管理
            //InputManager.Create();

            //世界管理
            WorldManager.Create();

            //object管理の作成
            ElementManager.Create();
            
            //シーン管理
            //Element.Scene.SceneManager.Manager = new Element.Scene.SceneManager();
            //Element.ElementManager.Mana.AddRequest(Element.Scene.SceneManager.Manager);

            #endregion

            //全体デフォルト
            Vector2 vsize = ClarityEngine.EngineSetting.GetVec2("DisplayViewSize", new Vector2(this.FData.Con.ClientSize.Width, this.FData.Con.ClientSize.Height));
            WorldManager.Mana.CreateSystemViewWorld((int)vsize.X, (int)vsize.Y);

            //基本世界の作成
            this.CreateDefaultWorld();
        }


        /// <summary>
        /// デフォルト世界の作成(RenderingTexture内ゲームのデフォルト)
        /// </summary>
        private void CreateDefaultWorld()
        {
            Vector2 vsize = ClarityEngine.EngineSetting.GetVec2("RenderingViewSize", new Vector2());

            
            
            
            //デフォルト世界の登録
            WorldData wdata = new WorldData();
            wdata.DefaultCameraMat = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -10000.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);            
            wdata.ProjectionMat = Matrix4x4.CreateOrthographic(vsize.X, vsize.Y, 1.0f, 15000.0f);
            //wdata.ProjectionMat = Matrix4x4.CreatePerspectiveFieldOfView((float)(Math.PI / 4), vsize.Y / vsize.X, 0.01f, 10000.0f);
            
            wdata.ReCalcu();

            //ViewPortの作成と登録
            Viewport vp = new Viewport(0, 0, vsize.X, vsize.Y, 0.0f, 1.0f);
            wdata.VPort = new ViewPortData() { VPort = vp };
            
            WorldManager.Mana.Set(0, wdata);



        }

    }
}
