using Clarity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TrainSrcArranger
{
    enum EControlEvent
    {
        New,
        Next,
        ZoomChanged,
        Finish,
    }

    public class NextProcData
    {
        public NextProcData(Bitmap bit, bool nextflag = true)
        {
            this.Image = bit;
            this.NextImageFlag = nextflag;
        }

        public Bitmap Image;
        public bool NextImageFlag = false;
    }

    internal class TsGlobal : BaseClarityConstSingleton<TsGlobal>
    {
        /// <summary>
        /// 使用画像拡張子
        /// </summary>
        public static readonly string[] ImageExt = {
            ".jpg",
            ".jpeg",
            ".png",
            ".bmp",
        };

        /// <summary>
        /// 作業データ
        /// </summary>
        public ArrangeData? _Data { get; set; } = null;

        public static ArrangeData Data
        {
            get
            {
                if(TsGlobal.Mana._Data == null)
                {
                    throw new Exception("");
                }

                return TsGlobal.Mana._Data;
            }
        }

        /// <summary>
        /// 画像領域の確定処理
        /// </summary>
        public static Subject<NextProcData> NextProcSub { get; } = new Subject<NextProcData>();

        /// <summary>
        /// コントロールイベント
        /// </summary>
        public static Subject<EControlEvent> EventSub { get; } = new Subject<EControlEvent>();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 処理対象ファイルの列挙
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static async Task<List<string>> ListupProcFiles(string filepath)
        {
            List<string> anslist = new List<string>();

            //列挙
            IEnumerable<string> flist = await Task.Run(() => { return Directory.EnumerateFiles(filepath, "*"); });


            foreach(string fpath in flist)
            {                
                bool f = await Task.Run(() => { return CheckImages(fpath); });
                if (f == false)
                {
                    continue;
                }

                anslist.Add(fpath);
            }

            return anslist;
        }

        /// <summary>
        /// 対象のファイルが処理対象画像かをチェックする
        /// </summary>
        /// <returns></returns>
        public static bool CheckImages(string filename)
        {
            //拡張子取得
            string ckext = Path.GetExtension(filename);

            //確認
            foreach (string s in TsGlobal.ImageExt)
            {
                //大文字で比較
                if (ckext.ToUpper() == s.ToUpper())
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// 画像の保存
        /// </summary>
        /// <param name="index"></param>
        /// <param name="image"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public static async Task SaveArrangeImages(int index, Bitmap image, string? prompt = null)
        {
            //保存パスの作成
            var path = TsGlobal.CreateSavePath(index);

            await Task.Run(() => { image.Save(path.imagepath); });           

            if(prompt == null)
            {
                return;
            }
            if (prompt.Length <= 0)
            {
                return;
            }

            using (FileStream fp = new FileStream(path.textpath, FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(fp))
                {
                    await sw.WriteLineAsync(prompt);
                }
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        private static (string imagepath, string textpath) CreateSavePath(int index)
        {
            //画像パスの作成
            string imagepath = TsGlobal.Data.OutputFolderPath + "\\" + string.Format(AppConfig.Mana.SaveFileNameFormat, index);

            //拡張子変更
            string textpath = Path.ChangeExtension(imagepath, ".txt");


            return (imagepath, textpath);
        }

    }


    /// <summary>
    /// データ一式
    /// </summary>
    public class ArrangeData
    {
        public ArrangeData(List<string> inlist, string opath, int w, int h, int startindex)
        {
            this.WorkingFilePathList = new List<string>();
            this.WorkingFilePathList.AddRange(inlist);
            this.OutputFolderPath = opath;
            this.CutSize = new Size(w, h);
            this.NowFileIndex = 0;
            this.NowProcIndex = startindex;
        }

        /// <summary>
        /// 処理ファイル一覧
        /// </summary>
        public List<string> WorkingFilePathList { get; private set; } = new List<string>();

        /// <summary>
        /// 出力フォルダ
        /// </summary>
        public string OutputFolderPath { get; private set; } = "";

        /// <summary>
        /// 切り出しサイズ
        /// </summary>
        public Size CutSize { get; private set; } = new Size();


        /// <summary>
        /// 現在のファイルindex
        /// </summary>
        public int NowFileIndex { get; private set; } = 0;

        /// <summary>
        /// 現在の処理パス
        /// </summary>
        public int NowProcIndex { get; private set; } = 0;


        public string NowPath
        {
            get
            {
                if (this.WorkingFilePathList.Count >= this.NowFileIndex)
                {
                    return "";
                }


                return this.WorkingFilePathList[this.NowFileIndex];
            }
        }

        /// <summary>
        /// 処理対象のパスを取得
        /// </summary>
        /// <returns></returns>
        public string? ProcCurrentPath()
        {
            if(this.WorkingFilePathList.Count <= this.NowFileIndex)
            {
                return null;
            }

            string s = this.WorkingFilePathList[this.NowFileIndex];
            this.NowFileIndex += 1;
            return s;
        }

        //次へ
        public int ProcNext()
        {
            return this.NowProcIndex++;
        }
    }
}
