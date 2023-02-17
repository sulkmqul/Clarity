using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Reactive.Subjects;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace Clarity.Cv.Video
{

    /// <summary>
    /// 映像保存基底
    /// </summary>
    public abstract class BaseVideoImageWriter : IDisposable
    {
        /// <summary>
        /// Rx管理
        /// </summary>
        protected CompositeDisposable RxRem { get; } = new CompositeDisposable();

        /// <summary>
        /// 保存stream
        /// </summary>
        protected Subject<Mat> WriteSubject { get; } = new Subject<Mat>();

      
        /// <summary>
        /// 映像の追加
        /// </summary>
        /// <param name="mat"></param>
        public virtual void PushImage(Mat mat)
        {
            this.WriteSubject.OnNext(mat);            
        }

        /// <summary>
        /// 終了
        /// </summary>
        public void Close()
        {
            this.WriteSubject.OnCompleted();

        }

        public virtual void Dispose()
        {
            this.RxRem.Dispose();
        }
    }

    /// <summary>
    /// 動画保存クラス
    /// </summary>
    /// <remarks>OpenCVSharpを利用した映像保存クラス</remarks>
    public class MovieFileWriter : BaseVideoImageWriter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MovieFileWriter()
        {

        }
                

        /// <summary>
        /// 動画書き込み
        /// </summary>
        private VideoWriter? Vw = null;

        /// <summary>
        /// 保存パス
        /// </summary>
        private string SavePath = "";


        /// <summary>
        /// 動画保存
        /// </summary>
        /// <param name="savepath"></param>
        public void Init(string savepath, double fps = 30.0)
        {
            if (this.Vw != null)
            {
                throw new Exception("this class is already initialized");
            }

            //保存パスの保存
            this.SavePath = savepath;

            this.Vw = new VideoWriter();


            //初回処理
            var ftdis = this.WriteSubject.Take(1).Subscribe(x =>
            {
                //ファイルを開いて保存準備
                this.Vw.Open(this.SavePath, FourCC.H264, fps, x.Size());
            });
            this.RxRem.Add(ftdis);


            //保存処理
            var wdis = this.WriteSubject.Subscribe(x =>
            {
                this.Vw?.Write(x);
            },
            ()=>
            {
                this.Dispose();
            });

            this.RxRem.Add(wdis);


            //
        }

        
        
        /// <summary>
        /// 解放処理
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            this.Vw?.Dispose();
            this.Vw = null;
        }
    }




    /// <summary>
    /// 連番画像の保存
    /// </summary>
    public class ImageFileWriter : BaseVideoImageWriter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ImageFileWriter()
        {

        }

        /// <summary>
        /// 保存フォルダ
        /// </summary>
        private string SavePath = "";

        /// <summary>
        /// 保存index
        /// </summary>
        private int SaveIndex { get; set; } = 0;

        /// <summary>
        /// 画像保存
        /// </summary>
        /// <param name="savepath">保存フォルダ</param>
        /// <param name="prefix">保存名頭文字</param>
        public void Init(string savepath, string prefix = "")
        {
            //保存パスの保存
            this.SavePath = savepath;

            //保存indexの初期化
            this.SaveIndex = 0;

            //保存処理
            var wdis = this.WriteSubject.Subscribe(async x =>
            {
                await Task.Run(() =>
                {
                    //System.Drawing.BitmapはasyncするとGDI+で問題が起きるのでcv標準を使用する。
                    string savepath = @$"{this.SavePath}\{prefix}{this.SaveIndex.ToString("00000000")}.png";
                    Cv2.ImWrite(savepath, x);
                });

                this.SaveIndex += 1;
            },
            () =>
            {
                this.Dispose();
                
            });

            this.RxRem.Add(wdis);


            //
        }
    }
}
