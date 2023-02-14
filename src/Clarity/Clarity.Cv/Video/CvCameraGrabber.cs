using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Reactive.Subjects;
using System.Drawing;

namespace Clarity.Cv.Video
{
    /// <summary>
    /// カメラ初期化用データ
    /// </summary>
    public class CvCameraInitParam
    {
        /// <summary>
        /// カメラIndex
        /// </summary>
        public int? CameraIndex = null;
        /// <summary>
        /// カメラアドレス(index優先)
        /// </summary>
        public string? CameraHost = null;
        /// <summary>
        /// 要求解像度
        /// </summary>
        public System.Drawing.Size? Resulution = null;


    }

    /// <summary>
    /// カメラ動作モード
    /// </summary>
    public enum ECameraMode
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
    /// カメラ取得関数
    /// </summary>
    public class CvCameraGrabber
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CvCameraGrabber()
        {
            
        }


        /// <summary>
        /// 映像取得情報
        /// </summary>
        class GrabDevice : IDisposable
        {
            public GrabDevice()
            {
            }

            public int ID { get; init; } = 0;

            /// <summary>
            /// 映像取得管理
            /// </summary>
            public VideoCapture VCap;

            /// <summary>
            /// パラメータ
            /// </summary>
            public CvCameraInitParam Param;

            public void Dispose()
            {
                this.VCap.Dispose();
            }
        }

        #region メンバ変数
        /// <summary>
        /// カメラ映像取得Stream
        /// </summary>
        public Subject<CameraGrabInfo> CameraGrabSub { get; private set; } = new Subject<CameraGrabInfo>();

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
        private List<CvCameraInitParam> InitParamList = new List<CvCameraInitParam>();


        /// <summary>
        /// 映像取得管理データ
        /// </summary>
        /// <remarks>
        /// stop後、disposeすること。
        /// これに値が入っている場合はデバイスが開いている=disposしてクリアしてから使うこと。
        /// </remarks>
        private List<GrabDevice> DeviceList = new List<GrabDevice>();
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
        public ECameraMode CameraMode
        {
            get
            {
                if (this.InitParamList.Count <= 0)
                {
                    return ECameraMode.None;
                }

                if (this.InitParamList.Count == 1)
                {
                    return ECameraMode.Single;
                }

                return ECameraMode.Multi;
            }
        }


        /// <summary>
        /// カメラ設定初期化(singleカメラ)
        /// </summary>
        /// <param name="data">取得カメラ情報</param>
        public void InitSingle(CvCameraInitParam data)
        {
            this.Init(new List<CvCameraInitParam>() { data });
        }


        /// <summary>
        /// ステレオカメラでのカメラ設定初期化
        /// </summary>
        /// <param name="data1">カメラ1</param>
        /// <param name="data2">カメラ2</param>
        public void InitStereo(CvCameraInitParam data1, CvCameraInitParam data2)
        {
            this.Init(new List<CvCameraInitParam>() { data1, data2 });
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
            this.CancelGrab = new CancellationTokenSource();
            this.GrabTask = this.GrabberLoop(this.DeviceList, this.CancelGrab.Token);
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
        /// カメラキャプチャの終了
        /// </summary>
        /// <returns></returns>
        public async Task StopLoop()
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
            this.CloseDevice();

        }

        /// <summary>
        /// デバイスを閉じる。
        /// </summary>
        public void CloseDevice()
        {
            this.DeviceList.ForEach(x => x.Dispose());
            this.DeviceList.Clear();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//     
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// カメラ初期化
        /// </summary>
        /// <param name="ilist">初期化リスト</param>
        private void Init(List<CvCameraInitParam> ilist)
        {
            //初期化物の保存
            this.InitParamList = ilist;
        }


        /// <summary>
        /// 取得デバイス一覧の作成
        /// </summary>
        private List<GrabDevice> CreateGrabDevice(List<CvCameraInitParam> paramlist)
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
                    return;
                }

                //パラメータの設定
                if (x.Resulution != null)
                {
                    vcap.Set(VideoCaptureProperties.FrameWidth, x.Resulution?.Width ?? 0);
                    vcap.Set(VideoCaptureProperties.FrameHeight, x.Resulution?.Height ?? 0);
                }


                //取得デバイスの作成
                GrabDevice ans = new GrabDevice() { ID = id };
                ans.VCap = vcap;
                ans.Param = x;
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
    }
}
