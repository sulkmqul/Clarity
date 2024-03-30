using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.Engine;
using Clarity.Engine.Shader;
using Clarity.Engine.Texture;
using Clarity.GUI;
using Clarity.Image.PNG;

namespace ClarityImageViewer.Viewer
{
    public enum DxControlEventID
    {
        ImageLoadFinished,
        FramePositionChanged,
        ScaleChanged,
    }

    public class ImageInfo
    {
        /// <summary>
        /// ファイルパス
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// 画像サイズ
        /// </summary>
        public Size ImageSize { get; set; } = new Size(0, 0);

        /// <summary>
        /// フレーム数
        /// </summary>
        public int FrameCount { get; set; } = 0;

        public string Infomation { get; set; } = "";

    }



    /// <summary>
    /// ClarityEngineの基底コントロール
    /// </summary>
    public partial class DxControl : UserControl
    {
        public DxControl()
        {
            InitializeComponent();

            //マウスホイールイベント処理
            this.MouseWheel += DxControl_MouseWheel;
        }


        /// <summary>
        /// Image管理object
        /// </summary>
        private ViewImageObject? ImageObject = null;

        /// <summary>
        /// 描画ループTask管理
        /// </summary>
        private Task? ClarityEngineLoopTask = null;
        /// <summary>
        /// 描画ループタスク破棄管理
        /// </summary>
        private CancellationTokenSource ClarityEngineLoopCanceller = new CancellationTokenSource();

        /// <summary>
        /// マウス管理
        /// </summary>
        private MouseInfo Minfo = new MouseInfo();


        /// <summary>
        /// 拡大率を取得
        /// </summary>
        public float ScaleRate
        {
            get
            {
                return this.ImageObject?.ScaleRate ?? 1.0f;
            }
        }

        /// <summary>
        /// 変化通知
        /// </summary>
        public Subject<(DxControlEventID, object?)> EventSubject { get; private set; } = new Subject<(DxControlEventID, object?)>();


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //エンジンの初期化
            ClarityEngine.Init(this);
            //背景色の設定            
            ClarityEngine.EngineSetting.SetEngineParam(EClarityEngineSettingKeys.ViewDisplay_ClearColor, new System.Numerics.Vector4(0.2f, 0.2f, 0.2f, 1.0f));


            //世界設定
            this.SetWorld();

            //ClariyEngineLoop開始
            this.ClarityEngineLoopTask = ClarityEngine.Native.ProcLoop(1000.0f / 60.0f, this.ClarityEngineLoopCanceller.Token);



        }


        /// <summary>
        /// テキスチャの読込
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public async Task LoadImage(string filepath)
        {
            //既存オブジェクトの管理を解除
            if (this.ImageObject != null)
            {
                ClarityEngine.RemoveManage(this.ImageObject);
            }
            //既存のテクスチャ解放
            ClarityEngine.Texture.ClearTexture();

            //テクスチャの読込
            var vdata = await this.CreateTextureImages(filepath);

            //objectの作成
            ViewImageObject obj = new ViewImageObject(this, vdata.Item1);
            this.ImageObject = obj;

            //ManageADD
            ClarityEngine.AddManage(obj);

            //リサイズ
            this.FitImage();

            //接続情報作成
            ImageInfo info = new ImageInfo();
            info.FilePath = filepath;
            info.ImageSize = new Size(vdata.Item1.ImageWidth, vdata.Item1.ImageHeight);
            info.FrameCount = vdata.Item1.FrameList.Count;
            info.Infomation = vdata.Item2;
            this.EventSubject.OnNext((DxControlEventID.ImageLoadFinished, info));
        }


        /// <summary>
        /// 解放
        /// </summary>
        /// <returns></returns>
        public async Task RelaseControl()
        {
            if (this.ClarityEngineLoopTask != null)
            {
                this.ClarityEngineLoopCanceller.Cancel();
                await this.ClarityEngineLoopTask;
            }
        }


        #region 操作関数各種

        /// <summary>
        /// 画像のFit表示
        /// </summary>
        public void FitImage()
        {
            this.ImageObject?.FitImage(this.Width, this.Height);
            this.EventSubject.OnNext((DxControlEventID.ScaleChanged, this.ScaleRate));
        }
        /// <summary>
        /// 画像の実サイズ表示
        /// </summary>
        public void ChangeActuallySize()
        {
            this.ImageObject?.ChangeScaling(this.Width, this.Height, 1.0f);
            this.EventSubject.OnNext((DxControlEventID.ScaleChanged, this.ScaleRate));
        }

        /// <summary>
        /// 回転
        /// </summary>
        /// <param name="f">回転方向</param>
        public void RotateImage(bool f)
        {
            if(this.ImageObject == null)
            {
                return;
            }
            float rad = (float)Math.PI * 0.5f;
            float r = (f) ? rad : -rad;
            this.ImageObject.TransSet.RotZ += r;

            //
            this.FitImage();
        }
        #endregion

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 世界設定
        /// </summary>
        private void SetWorld()
        {
            WorldData wdata = this.CreateWorld();
            ClarityEngine.SetWorld(1, wdata);
        }

        /// <summary>
        /// 世界作成
        /// </summary>
        /// <returns></returns>
        private WorldData CreateWorld()
        {
            WorldData data = new WorldData();
            //VP
            data.VPort = new ViewPortData(0, 0, this.Width, this.Height, 0.0f, 1.0f);
            //カメラ位置
            data.DefaultCameraMat = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -100.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);

            //projection
            data.ProjectionMat = Matrix4x4.CreateOrthographic(this.Width, this.Height, 1.0f, 10000.0f);

            //計算
            data.ReCalcu();

            return data;
        }

        /// <summary>
        /// テクスチャの作成
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private async Task<(TextureAnimationInfo, string)> CreateTextureImages(string filepath)
        {
            //Apngの読込
            (TextureAnimationInfo, string)? tdata  = await this.LoadAPng(filepath);
            if (tdata != null)
            {
                return (tdata.Value.Item1, tdata.Value.Item2);
            }

            //Pngの読込
            tdata = await this.LoadPng(filepath);
            if (tdata != null)
            {
                return (tdata.Value.Item1, tdata.Value.Item2);
            }


            //
            TextureAnimationInfo ans = new TextureAnimationInfo();
            using (Bitmap bit = new Bitmap(filepath))
            {
                ClarityEngine.Texture.LoadTexture(1, bit);
                ans.ImageWidth = bit.Width;
                ans.ImageHeight = bit.Height;
                ans.FrameList.Add(new AnimeFrameInfo(1, -1));

            }

            return (ans, "");

        }

        /// <summary>
        /// Apngの読込
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private async Task<(TextureAnimationInfo, string)?> LoadAPng(string filepath)
        {
            try
            {
                //読込
                APngFile ap = new APngFile();
                await ap.Load(filepath);


                //開始
                TextureAnimationInfo ans = new TextureAnimationInfo();
                ans.ImageWidth = ap.Width;
                ans.ImageHeight = ap.Height;


                //フレーム情報のテクスチャとデータを作成
                int tid = 1;
                ap.FrameList.ForEach(frame =>
                {
                    ClarityEngine.Texture.LoadTexture(tid, frame.Width, frame.Height, frame.FrameData);
                    ans.FrameList.Add(new AnimeFrameInfo(tid, frame.Time));
                    tid++;
                });

                return (ans, ap.Text);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Pngの読みこみ
        /// </summary>
        /// <param name="filepath">読込ファイルパス</param>
        /// <returns></returns>
        private async Task<(TextureAnimationInfo, string)?> LoadPng(string filepath)
        {
            try
            {
                //png読込
                PngFile png = new PngFile();
                await png.Load(filepath);
                
                //texure情報の作成
                int tid = 1;
                ClarityEngine.Texture.LoadTexture(tid, png.Width, png.Height, png.Data);

                //表示情報作成
                TextureAnimationInfo ans = new TextureAnimationInfo();
                ans.ImageWidth = png.Width;
                ans.ImageHeight = png.Height;
                ans.FrameList.Add(new AnimeFrameInfo(tid, -1));

                return (ans, png.Text);
            }
            catch(Exception ex)
            {
                return null;
            }

        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DxControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// サイズが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DxControl_SizeChanged(object sender, EventArgs e)
        {
            if (ClarityEngine.IsEngineInit == false)
            {
                return;
            }

            {
                WorldData data = this.CreateWorld();
                ClarityEngine.SetWorld(1, data);
            }


            //リサイズ
            this.ImageObject?.FitImage(this.Width, this.Height);
            this.EventSubject.OnNext((DxControlEventID.ScaleChanged, this.ScaleRate));

        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DxControl_MouseDown(object sender, MouseEventArgs e)
        {
            this.Minfo.DownMouse(e);
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DxControl_MouseMove(object sender, MouseEventArgs e)
        {
            this.Minfo.MoveMouse(e);
            if (this.Minfo.DownFlag == true)
            {
                this.ImageObject?.MoveImage(this.Width, this.Height, new Vector2(this.Minfo.PrevMoveLength.X, this.Minfo.PrevMoveLength.Y));
            }
        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DxControl_MouseUp(object sender, MouseEventArgs e)
        {
            this.Minfo.UpMouse(e);
        }

        /// <summary>
        /// マウスホイール処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void DxControl_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                this.ImageObject?.ChangeScalingStep(this.Width, this.Height, new Vector2(e.X, e.Y), false);
            }
            else
            {
                this.ImageObject?.ChangeScalingStep(this.Width, this.Height, new Vector2(e.X, e.Y), true);
            }
            this.EventSubject.OnNext((DxControlEventID.ScaleChanged, this.ScaleRate));
        }

        /// <summary>
        /// ダブルクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
   
        private void DxControl_DoubleClick(object sender, EventArgs e)
        {
            this.FitImage();
        }
    }
}
