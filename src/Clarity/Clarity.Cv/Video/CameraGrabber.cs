using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Reactive.Subjects;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Clarity.Cv.Video
{
    /// <summary>
    /// カメラ初期化用データ
    /// </summary>
    public class CameraGrabberInitParam
    {
        /// <summary>
        /// カメラIndex
        /// </summary>
        public int? CameraIndex = null;
        /// <summary>
        /// カメラ
        /// </summary>
        public string? CameraHost = null;

        /// <summary>
        /// 要求解像度
        /// </summary>
        public System.Drawing.Size? Resulution = null;


    }

    /// <summary>
    /// カメラ取得動作モード
    /// </summary>
    public enum ECameraGrabMode
    {
        Single,
        Multi,

        //--
        None,
    }

    /// <summary>
    /// カメラ映像取得情報
    /// </summary>
    public class CameraGrabInfo
    {
        /// <summary>
        /// 開始からのindex
        /// </summary>
        public int Index { get; set; } = 0;

        /// <summary>
        /// 取得画像
        /// </summary>
        public List<Mat> CaptureImageList { get; set; } = new List<Mat>();

        /// <summary>
        /// 映像取得開始からの時間
        /// </summary>
        public long ElapsedMilliseconds { get; set; } = 0;

    }


    /// <summary>
    /// カメラ映像取得クラス
    /// </summary>
    /// <remarks>OpenCVSharpを利用したカメラ取得</remarks>
    public class CameraGrabber : IAsyncDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CameraGrabber()
        {
            
        }


        /// <summary>
        /// 映像取得情報
        /// </summary>
        class GrabDevice : IDisposable
        {
            public GrabDevice(int id, VideoCapture cap, CameraGrabberInitParam para)
            {
                this.ID = id;
                this.VCap = cap;
                this.Param = para;

            }

            public int ID { get; init; } = 0;

            /// <summary>
            /// 映像取得管理
            /// </summary>
            public VideoCapture VCap { get; init; }

            /// <summary>
            /// パラメータ
            /// </summary>
            public CameraGrabberInitParam Param { get; init; }

            public void Dispose()
            {
                this.VCap.Dispose();
            }
        }


        /// <summary>
        /// カメラ映像取得Stream
        /// </summary>
        public Subject<CameraGrabInfo> CameraGrabSub { get; private set; } = new Subject<CameraGrabInfo>();

        /// <summary>
        /// FPS計測(1sごとにfps更新を通知する)
        /// </summary>
        public Subject<double> CameraGrabFPSSub { get; private set; } = new Subject<double>();

        #region メンバ変数

        /// <summary>
        /// カメラ映像取得タスク null出ないときはキャプチャ中
        /// </summary>
        private Task? GrabTask = null;

        /// <summary>
        /// 取得タスクをキャンセルするもの
        /// </summary>
        private CancellationTokenSource CancelGrab = new CancellationTokenSource();

        /// <summary>
        /// 初期化パラメータ
        /// </summary>
        private List<CameraGrabberInitParam> InitParamList = new List<CameraGrabberInitParam>();


        /// <summary>
        /// 映像取得管理データ
        /// </summary>
        /// <remarks>
        /// stop後、disposeすること。
        /// これに値が入っている場合はデバイスが開いている=disposしてクリアしてから使うこと。
        /// </remarks>
        private List<GrabDevice> DeviceList = new List<GrabDevice>();

        /// <summary>
        /// FPS計測用取得回数
        /// </summary>
        private int GrabCount = 0;


        /// <summary>
        /// FPS計算タスクの管理
        /// </summary>
        private IDisposable? MeasureFPS = null;
        #endregion

        /// <summary>
        /// 現在取得中か？ true=カメラ映像取得中
        /// </summary>
        public bool IsGrabbing
        {
            get
            {
                if (this.GrabTask == null)
                {
                    return false;
                }
                return true;
            }

        }
        /// <summary>
        /// カメラ取得モード
        /// </summary>
        public ECameraGrabMode CameraMode
        {
            get
            {
                if (this.InitParamList.Count <= 0)
                {
                    return ECameraGrabMode.None;
                }

                if (this.InitParamList.Count == 1)
                {
                    return ECameraGrabMode.Single;
                }

                return ECameraGrabMode.Multi;
            }
        }


        /// <summary>
        /// 現在のFPS
        /// </summary>
        public double FPS { get; private set; } = 0.0;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// カメラ設定初期化(singleカメラ)
        /// </summary>
        /// <param name="data">取得カメラ情報</param>
        public void InitSingle(CameraGrabberInitParam data)
        {
            this.Init(new List<CameraGrabberInitParam>() { data });
        }


        /// <summary>
        /// ステレオカメラでのカメラ設定初期化
        /// </summary>
        /// <param name="data1">カメラ1</param>
        /// <param name="data2">カメラ2</param>
        public void InitStereo(CameraGrabberInitParam data1, CameraGrabberInitParam data2)
        {
            this.Init(new List<CameraGrabberInitParam>() { data1, data2 });
        }


        /// <summary>
        /// デバイスのオープン
        /// </summary>
        public void OpenDevice()
        {
            if (this.DeviceList.Count > 0)
            {
                System.Diagnostics.Trace.WriteLine("device is already opened");
                return;
            }

            //デバイスの初期化
            this.DeviceList = this.CreateGrabDevice(this.InitParamList);
        }

        /// <summary>
        /// カメラキャプチャループの開始
        /// </summary>
        public void StartLoop()
        {
            if (this.IsGrabbing == true)
            {
                System.Diagnostics.Trace.WriteLine("grab task is already started");
                return;
            }

            //デバイスの初期化
            if (this.DeviceList.Count <= 0)
            {
                this.DeviceList = this.CreateGrabDevice(this.InitParamList);
            }

            //処理開始
            this.FPS = 0.0;
            this.CancelGrab = new CancellationTokenSource();
            this.GrabTask = this.GrabberLoop(this.DeviceList, this.CancelGrab.Token);
        }




        /// <summary>
        /// カメラキャプチャの終了
        /// </summary>
        /// <param name="dclose">同時にデバイスクローズを行うか？</param>
        /// <returns></returns>
        public async Task StopLoop(bool dclose = false)
        {
            if (this.GrabTask == null)
            {
                System.Diagnostics.Trace.WriteLine("grab task is not started");
                return;
            }

            try
            {
                //キャンセルの送信と終了待ち
                this.CancelGrab.Cancel();
                await this.GrabTask;
                
            }
            catch (OperationCanceledException cex)
            {
                //これは無視でよい
            }


            //解放処理
            this.GrabTask = null;

            //デバイスのクローズ
            if (dclose == true)
            {
                this.CloseDevice();
            }

        }

        /// <summary>
        /// デバイスを閉じる。
        /// </summary>
        public void CloseDevice()
        {
            this.DeviceList.ForEach(x => x.Dispose());
            this.DeviceList.Clear();
        }

        /// <summary>
        /// 解放されるとき
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            //取得タスクの終了
            if (this.IsGrabbing == true)
            {
                await this.StopLoop();
            }

            //FPS計測タスクの終了
            this.MeasureFPS?.Dispose();

            //デバイスクリア
            this.CloseDevice();
        }


        /// <summary>
        /// 一枚画像の取得(キャプチャ中は動作しない)
        /// </summary>
        /// <returns></returns>
        public async Task<List<Mat>> ReadImageAsync()
        {
            if (this.IsGrabbing == true)
            {
                throw new Exception("capture task is started");
            }
            if (this.DeviceList.Count <= 0)
            {
                throw new Exception("device is not opened");
            }

            return await this.ReadImageAsync(this.DeviceList);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//     
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// カメラ初期化
        /// </summary>
        /// <param name="ilist">初期化リスト</param>
        private void Init(List<CameraGrabberInitParam> ilist)
        {
            //初期化物の保存
            this.InitParamList = ilist;

            //FPS計測タスクを仕掛ける
            this.StartMeasureFPSTask();
        }


        /// <summary>
        /// 取得デバイス一覧の作成
        /// </summary>
        private List<GrabDevice> CreateGrabDevice(List<CameraGrabberInitParam> paramlist)
        {
            List<GrabDevice> anslist = new List<GrabDevice>();

            int id = 0;
            paramlist.ForEach(x =>
            {
            //デバイスの初期化                
            VideoCapture? vcap = null;
            if (x.CameraIndex != null)
            {
                vcap = new VideoCapture(x.CameraIndex ?? 0);
            }
            else if (x.CameraHost != null)
            {
                vcap = new VideoCapture(x.CameraHost);
            }
            if (vcap == null)
            {
                throw new Exception("Video device initialize exception");
            }

                //パラメータの設定
                if (x.Resulution != null)
                {

                    vcap.Set(VideoCaptureProperties.FrameWidth, x.Resulution?.Width ?? 0);
                    vcap.Set(VideoCaptureProperties.FrameHeight, x.Resulution?.Height ?? 0);

                    vcap.Set(VideoCaptureProperties.Fps, 30.0);
                }

                



                //取得デバイスの作成
                GrabDevice ans = new GrabDevice(id, vcap, x);
                anslist.Add(ans);

                id++;
            });

            return anslist;

        }


        /// <summary>
        /// カメラ取得処理本体
        /// </summary>
        /// <param name="devlist">取得デバイス一式</param>
        /// <param name="ct">キャンセル処理</param>
        /// <returns></returns>
        private async Task GrabberLoop(List<GrabDevice> devlist, CancellationToken ct)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //取得回数初期化
            this.GrabCount = 0;

            int index = 0;
            while (true)
            {
                //終了通知確認
                ct.ThrowIfCancellationRequested();

                //カメラ映像の取得
                List<Mat> datalist = await this.ReadImageAsync(devlist);

                long ms = sw.ElapsedMilliseconds;

                //取得情報の作成
                CameraGrabInfo cinfo = new CameraGrabInfo();
                cinfo.Index = index;
                cinfo.CaptureImageList = datalist;
                cinfo.ElapsedMilliseconds = ms;

                //通知
                this.CameraGrabSub.OnNext(cinfo);
                index++;

                //取得回数増加
                this.GrabCount += 1;
            }
        }

        /// <summary>
        /// 全映像の同期取得
        /// </summary>
        /// <param name="devlist">デバイス一式</param>
        /// <returns></returns>
        private async Task<List<Mat>> ReadImageAsync(List<GrabDevice> devlist)
        {
            return await Task.Run(() =>
            {
                //パラレルだと順番が保証できないのでindex付のdicに受けて変換
                System.Collections.Concurrent.ConcurrentDictionary<int, Mat> cd = new System.Collections.Concurrent.ConcurrentDictionary<int, Mat>();
                Parallel.ForEach(devlist, (dev) =>
                {
                    Mat ma = new Mat();
                    dev.VCap.Read(ma);

                    
                    cd.AddOrUpdate(dev.ID, ma, (n, m) => { return m; });
                });

                //値の取得
                List<Mat> anslist = new List<Mat>();
                for (int i = 0; i < devlist.Count ; i++)
                {
                    anslist.Add(cd[i]);
                }
                return anslist;
                
            });

        }


        /// <summary>
        /// FPS計測タスクの開始
        /// </summary>
        private void StartMeasureFPSTask()
        {
            //既存のが動いているなら解放
            this.MeasureFPS?.Dispose();

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            //1sごとにFPSタスクを起動
            this.MeasureFPS = Observable.Timer(TimeSpan.FromMilliseconds(0), TimeSpan.FromMilliseconds(1000)).Subscribe(x =>
            {
                if (this.IsGrabbing == false)
                {
                    sw.Restart();
                    this.FPS = 0.0;
                    return;
                }

                //System.Diagnostics.Trace.WriteLine($"FPS Task::{this.GrabCount} {sw.ElapsedMilliseconds} {fps}");

                //正確なFPS値を計算する
                double span = sw.ElapsedMilliseconds;
                double count = this.GrabCount;
                this.GrabCount = 0;


                this.FPS = count / span * 1000.0;
                this.CameraGrabFPSSub.OnNext(this.FPS);
                
                //再初期化
                sw.Restart();
            });


        }

        
    }
}
