using Clarity.Shader;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clarity.Core
{
    /// <summary>
    /// フレーム計算用データ
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
        public static (float proc, float render) CalcuFPS(long nowms, FrameRateCalcuData srcdata)
        {
            //経過時間の算出
            long span = nowms - srcdata.PrevCalcuMs;
            //秒に変換
            float spanf = (float)span * 0.001f;


            float prc_fps = srcdata.ProcCount / spanf;
            float render_fps = srcdata.RenderCount / spanf;

            return (prc_fps, render_fps);
        }
    }

    /// <summary>
    /// ゲームエンジンコア
    /// </summary>
    internal class ClarityCore : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ClarityCore()
        {

        }


        #region メンバ変数

        /// <summary>
        /// 管理コントロール
        /// </summary>
        private Control ManaCon = null;
        /// <summary>
        /// 表示領域のサイズ
        /// </summary>
        private Size DisplaySize
        {
            get
            {
                return this.ManaCon.ClientSize;
            }
        }
        /// <summary>
        /// 起動オプション
        /// </summary>
        private EngineSetupOption SetupOption = null;


        /// <summary>
        /// 拡張実行一式
        /// </summary>
        private ClarityEngineExtension Proc = null;


        /// <summary>
        /// SystemView管理
        /// </summary>
        private SystemView SView = null;

        /// <summary>
        /// System文字
        /// </summary>
        private SystemText SText = null;
        #endregion




        /// <summary>
        /// エンジンコア初期化
        /// </summary>
        /// <param name="con">管理コントロール</param>
        /// <param name="op">起動オプション</param>
        public void Init(Control con, EngineSetupOption op)
        {
            this.SetupOption = op;
            this.ManaCon = con;
                       

            //エンジン管理クラスたちの作成
            this.CreateEngineManagers();


            //Viewの作成
            this.SView = new SystemView();

            {
                //描画文字列の定義                
                this.SText = new SystemText(ClarityEngine.Setting.Debug.SystemTextSize, ClarityEngine.Setting.Debug.SystemTextColor, 1);                
                this.SText.Pos2D = ClarityEngine.Setting.Debug.SystemTextPos;
            }

        }


        /// <summary>
        /// エンジンメイン処理
        /// </summary>
        /// <param name="ice"></param>        
        public void StartClarity(ClarityEngineExtension ice)
        {
            this.Proc = ice;            

            ClarityInitParam iparam = new ClarityInitParam();
            iparam.Con = this.ManaCon;

            //リサイズ処理を行う
            this.ManaCon.Resize += new EventHandler(this.ResizeViewEvent);

            //実行前初期化
            this.Proc?.Init(iparam);

            //ループ処理
            this.Loop();

            //解放処理
            this.Proc?.Dispose();
        }


        /// <summary>
        /// 解放されるとき
        /// </summary>
        public void Dispose()
        {
            //使用物の解放

            //オブジェクト管理
            Element.ElementManager.Mana.Clear(true);

            //世界管理
            WorldManager.Mana.Dispose();
            
            //入力管理
            InputManager.Mana.Dispose();
            
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

        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// エンジン管理クラスの作成
        /// </summary>
        private void CreateEngineManagers()
        {
            #region エンジン管理クラスの作成
            //DirectXの初期化
            DxManager.Init(this.ManaCon, ClarityEngine.Setting.RenderingViewSize);

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
            InputManager.Create();

            //世界管理
            WorldManager.Create();

            //object管理の作成
            Element.ElementManager.Create();

            //シーン管理
            Element.Scene.SceneManager.Manager = new Element.Scene.SceneManager();
            Element.ElementManager.Mana.AddRequest(Element.Scene.SceneManager.Manager);
            #endregion

            //全体デフォルト
            WorldManager.Mana.CreateSystemViewWorld(this.DisplaySize.Width, this.DisplaySize.Height);

            //基本世界の作成
            this.CreateDefaultWorld();
        }



        /// <summary>
        /// デフォルト世界の作成(RenderingTexture内ゲームのデフォルト)
        /// </summary>
        private void CreateDefaultWorld()
        {
            Size vsize = ClarityEngine.Setting.RenderingViewSize;

            //ViewPortの作成と登録
            Viewport vp = new Viewport(0, 0, vsize.Width, vsize.Height, 0.0f, 1.0f);
            WorldManager.Mana.SetViewPort(vp);


            //デフォルト世界の登録
            WorldData wdata = new WorldData();
            wdata.DefaultCameraMat = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, 10000.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            wdata.ProjectionMat = Matrix.OrthoLH(vsize.Width, vsize.Height, 1.0f, 15000.0f);
            wdata.ReCalcu();

            WorldManager.Mana.Set(0, wdata);


            
        }


        /// <summary>
        /// メインループ処理
        /// </summary>
        private void Loop()
        {

            //FSP計算用
            FrameRateCalcuData fpsdata = new FrameRateCalcuData();

            //次の描画を飛ばすか否か
            bool nextskip = false;

            //パラメータ
            ClarityCyclingProcParam cparam = new ClarityCyclingProcParam();
            cparam.Con = this.ManaCon;


            long frametime = 0;
            //カウント開始
            ClarityTimeManager.Mana.Start();


            //フレーム情報
            FrameProcParam frame_info = new FrameProcParam();

            //前回の実行時間の初期化
            frame_info.PrevFrameTime = ClarityTimeManager.TotalMilliseconds;
            fpsdata.PrevCalcuMs = ClarityTimeManager.TotalMilliseconds;

            RenderLoop.Run(this.ManaCon, () =>
            {

                ClarityTimeManager.StartMeasure();

                //今回の基準実行時間を取得
                frame_info.FrameTime = ClarityTimeManager.TotalMilliseconds;
                frame_info.CalcuFrameBaseRate();
                

                #region フレーム処理
                {

                    //入力の取得
                    InputManager.Mana.GetInput();

                    //Cycling追加処理
                    this.Proc?.CyclingProc(cparam);

                    //ObjectManager処理
                    Element.ElementManager.Mana.ProcObject(frame_info);

                    fpsdata.ProcCount += 1;
                }
                #endregion


                //前回の処理時間を記憶
                frame_info.PrevFrameTime = frame_info.FrameTime;


                #region フレーム描画
                //描画処理
                if (nextskip == false)
                {
                    this.RenderFrame();
                    fpsdata.RenderCount += 1;
                }
                #endregion


                //今フレームの処理時間を計測
                frametime = ClarityTimeManager.StopMeasure();
                //基底時間を超えているなら次の描画をキャンセルしてクオリティを保つ
                nextskip = (frametime >= ClarityEngine.Setting.LimitTime);


                //FPS計算                
                long fpsspanmili = ClarityTimeManager.TotalMilliseconds - fpsdata.PrevCalcuMs;
                if (fpsspanmili > 1000)
                {
                    //フレームレートの計算
                    long calcutime = ClarityTimeManager.TotalMilliseconds;
                    var fps = FrameRateCalcuData.CalcuFPS(calcutime, fpsdata);

                    //FPSの表示
                    string fpsstring = string.Format("Proc:{0:F} Render:{1:F}", fps.proc, fps.render);
                    ClarityLog.WriteInfo(fpsstring);
                    this.SText.SetFPS(fpsstring);

                    //初期化
                    fpsdata = new FrameRateCalcuData() { PrevCalcuMs = calcutime, ProcCount = 0, RenderCount = 0 };

                }


                if (nextskip == false)
                {
                    //スキップしないならちょい待ち処理を入れる
                    System.Threading.Thread.Sleep(1);
                }


            });


        }


        /// <summary>
        /// RenderingTextureへの画面描画
        /// </summary>
        private void RenderRenderingTexture()
        {
            //基本描画設定
            DxManager.Mana.EnabledAlphaBlendNormal();
            DxManager.Mana.ChangeRenderTarget(DxManager.ERenderTargetNo.RenderingTexture);

            //描画開始
            DxManager.Mana.BeginRendering(new Color4(0.5f, 0.5f, 0.5f, 1.0f));
            
            //ViewPort数を取得
            int vpcount = (ClarityEngine.Setting.MultiViewPort) ? WorldManager.MaxViewPort : 1;
            for (int i = 0; i < vpcount; i++)
            {   

                Viewport vp = WorldManager.Mana.GetViewPort(i);
                DxManager.Mana.DxDevice.ImmediateContext.Rasterizer.SetViewport(vp);

                //描画処理
                Element.ElementManager.Mana.RenderObject(i);
            }
            
            DxManager.Mana.EndRendering();
            
        }


        /// <summary>
        /// フレーム描画処理
        /// </summary>
        private void RenderFrame()
        {

            

            //ゲーム要素すべてを描画
            this.RenderRenderingTexture();

            

            //システム描画の開始
            DxManager.Mana.DisabledAlphaBlend();
            DxManager.Mana.ChangeRenderTarget(DxManager.ERenderTargetNo.SwapChain);
            DxManager.Mana.BeginRendering(new Color4(1.0f, 0.0f, 0.0f, 1.0f));

            Viewport vp = WorldManager.Mana.GetSystemViewPort();
            DxManager.Mana.DxDevice.ImmediateContext.Rasterizer.SetViewport(vp);

            
            SystemView sv = this.SView;
            
            //RenderingTextureのサイズのまま
            sv.TransSet.Scale2D = new Vector2(ClarityEngine.Setting.RenderingViewSize.Width, ClarityEngine.Setting.RenderingViewSize.Height);
            
            //画面サイズの合わせる・・・比を維持したいならここのサイズを計算してだすこと
            //sv.TransSet.Scale2D = new Vector2(this.DisplaySize.Width, this.DisplaySize.Height);

            ShaderResourceView srvt = DxManager.Mana.RenderingTextureResource;
            sv.Render(srvt);


            //システム文字描画
            if (ClarityEngine.Setting.Debug.RenderSystemTextFlag == true)
            {
                this.SText.Render(new FrameRenderParam() { Crt = DxManager.Mana.CurrentTarget2D });
            }

            //システム描画の終了
            DxManager.Mana.EndRendering();
            //------------------------------------------------------

            //更新処理
            DxManager.Mana.SwapChainPresent();
        }

        /// <summary>
        /// 画面リサイズイベント
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        public void ResizeViewEvent(object o, EventArgs e)
        {
            //最小化対策 WindowState判定でもよいが、Size=0の方が汎用性がある気がする。
            Control con = o as Control;
            if (con?.ClientSize.Width <= 0 || con?.ClientSize.Height <= 0)
            {
                ClarityLog.WriteDebug("最小化された");
                return;
            }


            //スワップチェインのリサイズ
            Clarity.Core.DxManager.Mana.ResizeSwapChain();

            //SystemViewの作り直し
            WorldManager.Mana.CreateSystemViewWorld(this.DisplaySize.Width, this.DisplaySize.Height);

            //リサイズ関数
            this.Proc?.ResizeView(this.DisplaySize);
        }
    }
}
