using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clarity
{

    /// <summary>
    /// ログレベル
    /// </summary>
    public enum EClarityLogLevel
    {
        None = 0,

        Info = 1 << 1,
        Error = 1 << 2,
        Debug = 1 << 3,
        System = 1 << 4,


        ALL = Info | Error | Debug | System,
    }

    /// <summary>
    /// ログの場所
    /// </summary>
    public enum EClarityLogMode
    {
        Console = 1 << 0,
        File = 1 << 1,
        Window = 1 << 2,
    }

    /// <summary>
    /// Zettaログクラス
    /// </summary>
    public class ClarityLog : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private ClarityLog()
        {
        }

        #region シングルトン実装
        /// <summary>
        /// 実態
        /// </summary>
        private static ClarityLog Instance = null;


        public static ClarityLog Log
        {
            get
            {
                return ClarityLog.Instance;
            }
        }
        #endregion

        #region メンバ変数
        /// <summary>
        /// ログ出力レベル
        /// </summary>
        private EClarityLogLevel LogLevel = EClarityLogLevel.None;

        /// <summary>
        /// ログのモード
        /// </summary>
        private EClarityLogMode LogMode = EClarityLogMode.Console;

        /// <summary>
        /// 出力ログファイルパス
        /// </summary>
        private string LogFilePath = "";

        /// <summary>
        /// ログ画面
        /// </summary>
        private ClarityLogForm LogForm = null;
        #endregion

        
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="level">EClarityLogLevelをOR指定</param>
        /// <param name="mode">ログの出力先</param>
        /// <param name="outfol">ELogModeがFileの時、ログ出力フォルダ</param>
        /// <param name="identname">ELogModeがFileの時、ログファイルの固有名</param>
        public static void Init(EClarityLogLevel level = EClarityLogLevel.None, EClarityLogMode mode = EClarityLogMode.Console, string outfol = ".", string identname = "clarity.log")
        {
            ClarityLog.Instance = new ClarityLog();

            //レベル設定
            ClarityLog.Instance.LogLevel = level;
            ClarityLog.Instance.LogMode = mode;

            //ログファイルの書き込み
            ClarityLog.Instance.LogFilePath = ClarityLog.Instance.CreateLogFilePath(outfol, identname);

            //画面表示
            if ((mode & EClarityLogMode.Window) == EClarityLogMode.Window)            
            {
                ClarityLog.Instance.LogForm = new ClarityLogForm();
                //一回showしないと問題あり
                ClarityLog.Instance.LogForm.Show();
                //ClarityLog.Instance.LogForm.Hide();
            }

        }

        /// <summary>
        /// ログ画面の表示
        /// </summary>
        public static void ShowLogForm()
        {
            ClarityLog.Instance.LogForm?.Show();
        }


        /// <summary>
        /// Infoログの書き込み
        /// </summary>
        /// <param name="s"></param>
        public static void WriteInfo(string s)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Info, true, s);
        }

        /// <summary>
        /// 操作ログの書き込み
        /// </summary>
        /// <param name="mes">追加メッセージ</param>
        /// <param name="cmn">不要</param>
        public static void WriteControlInfo(string mes = "", [CallerMemberName] string cmn = "")
        {
            //呼び出し元クラス名の取得
            System.Diagnostics.StackFrame cal = new System.Diagnostics.StackFrame(1, false);
            string cname = cal.GetMethod()?.DeclaringType?.FullName ?? "";

            ClarityLog.WriteDebug($"{cname}.{cmn}() {mes}");            
        }

        /// <summary>
        /// Debugログの書き込み
        /// </summary>
        /// <param name="mes"></param>
        public static void WriteDebug(string mes)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Debug, true, mes);
        }

        /// <summary>
        /// Errorログの書き込み
        /// </summary>
        /// <param name="mes"></param>
        public static void WriteError(string mes)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Error, true, mes);
        }

        /// <summary>
        /// Ssytemログの書き込み
        /// </summary>
        /// <param name="mes"></param>
        public static void WriteSystem(string mes)
        {
            ClarityLog.WriteLog(EClarityLogLevel.System, true, mes);
        }

        /// <summary>
        /// 例外の書き込み
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrorException(Exception ex)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Error, true, "===============================");
            ClarityLog.WriteLog(EClarityLogLevel.Error, false, $"{ex.Message} {ex.ToString()}");
            Exception ie = ex.InnerException;
            while (ie != null)
            {
                ClarityLog.WriteLog(EClarityLogLevel.Error, false, $"{ex.ToString()} {ex.Message}");
                ie = ie.InnerException;
            }
            ClarityLog.WriteLog(EClarityLogLevel.Error, false, "===============================");
        }

        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose()
        {
            var f = this.LogForm;
            this.LogForm = null;
            f?.Close();
        }

        /// <summary>
        /// 解放
        /// </summary>
        public static void Release()
        {
            Instance.Dispose();
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// ファイルへの書き込み
        /// </summary>
        /// <param name="a">書き込み文字列</param>
        private void WriteFile(string a)
        {
            try
            {
                using (StreamWriter fp = new StreamWriter(this.LogFilePath, true))
                {
                    fp.WriteLine(a);
                }
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(a + " ログ書き込み失敗:: mes=" + e.Message);
                System.Diagnostics.Trace.WriteLine(a + " ログ書き込み失敗:: mes=" + e.Message);
            }
        }

        /// <summary>
        /// ログの出力本体
        /// </summary>
        /// <param name="a"></param>
        private void Write(string a)
        {
            using (TextBox tb = new TextBox())
            {
                tb.ScrollToCaret();
            }

            (EClarityLogMode m, Action ac)[] dmap = {
                (EClarityLogMode.Console, ()=>{ Console.WriteLine(a); System.Diagnostics.Trace.WriteLine(a); }),
                (EClarityLogMode.File, ()=>{ this.WriteFile(a); }),
                (EClarityLogMode.Window, ()=>{
                    this.LogForm.AddLogSafe(a);
                }),
            };

            //種別にあったログへ書き込み
            foreach (var t in dmap)
            {
                if ((this.LogMode & t.m) == t.m)
                {
                    t.ac();
                }
            }
        }

        /// <summary>
        /// ログの書き込み
        /// </summary>
        /// <param name="lev">ログレベル</param>
        /// <param name="f">日付出力可否</param>
        /// <param name="mes">書き込みメッセージ</param>
        private void _WriteLog(EClarityLogLevel lev, bool f, string mes)
        {
            if ((ClarityLog.Log.LogLevel & lev) == 0)
            {
                return;
            }

            string logline = "";

            //日付を描くなら
            if (f == true)
            {
                string sd = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                string fss = string.Format("{0} ", sd);
                logline += fss;
            }
            else
            {
                logline += "\t";
            }

            logline += $"[{lev}] {mes}";

            ClarityLog.Log.Write(logline);

        }

        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="lev"></param>
        /// <param name="f"></param>
        /// <param name="obj"></param>
        private static void WriteLog(EClarityLogLevel lev, bool f, string mes)
        {
            lock (ClarityLog.Instance)
            {
                ClarityLog.Instance._WriteLog(lev, f, mes);
            }
        }


        /// <summary>
        /// 出力ログファイルのパスを作成する
        /// </summary>
        /// <param name="fol">出力フォルダ</param>
        /// <param name="identname">名称</param>
        /// <returns></returns>
        private string CreateLogFilePath(string fol, string identname)
        {
            string ans = "";

            //ファイル名を拡張子と名称に分離
            string name = Path.GetFileNameWithoutExtension(identname);
            string ext = Path.GetExtension(identname);

            string fname = name + "_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ext;
            ans = fol + Path.DirectorySeparatorChar + fname;

            return ans;
        }
    }
}
