using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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


        ALL = Info | Error | Debug,
    }

    /// <summary>
    /// ログの場所
    /// </summary>
    public enum ELogMode
    {
        Console,
        File,        
    }

    /// <summary>
    /// Clarityログクラス
    /// </summary>
    public class ClarityLog : BaseClaritySingleton<ClarityLog>
    {
        

        /// <summary>
        /// ログ出力レベル設定
        /// </summary>
        public static EClarityLogLevel LogLevel
        {
            get
            {
                return ClarityLog.Mana._LogLevel;
            }
            set
            {
                ClarityLog.Mana._LogLevel = value;
            }
        }



        /// <summary>
        /// ログ出力レベル
        /// </summary>
        private EClarityLogLevel _LogLevel = EClarityLogLevel.None;

        /// <summary>
        /// ログのモード
        /// </summary>
        private ELogMode _LogMode = ELogMode.Console;

        /// <summary>
        /// 出力ログファイルパス
        /// </summary>
        private string LogFilePath = "";

        


        /// <summary>
        /// ログの出力本体
        /// </summary>
        /// <param name="a"></param>
        private void Write(object a)
        {
            switch (this._LogMode)
            {
                case ELogMode.Console:
                    Console.Write(a);
                    break;
                case ELogMode.File:
                    //ファイルに出すならここ
                    break;
            }
        }

        /// <summary>
        /// ログの書き込み
        /// </summary>
        /// <param name="obj"></param>
        private static void WriteLog(EClarityLogLevel lev, params object[] obj)
        {
            if ((ClarityLog.LogLevel & lev) == 0)
            {
                return;
            }

            //selectの第二引数が対象のindexとなる。これからindexと中身がsetになった新しいタプル配列を作成指定ループすることで配列添え字突きを実現する
            foreach (var o in obj.Select((a, b) => { return (a, b); }))
            {
                ClarityLog.Mana.Write(o.a);
                if (o.b != (obj.Length - 1))
                {
                    ClarityLog.Mana.Write(" :: ");
                }
            }
            ClarityLog.Mana.Write(System.Environment.NewLine);
            
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




        //////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="level">EClarityLogLevelをOR指定</param>
        /// <param name="mode">ログの出力先</param>
        /// <param name="outfol">ELogModeがFileの時、ログ出力フォルダ</param>
        /// <param name="identname">ELogModeがFileの時、ログファイルの固有名</param>
        public static void Init(EClarityLogLevel level = EClarityLogLevel.None, ELogMode mode = ELogMode.Console, string outfol = ".", string identname = "clarity.log")
        {
            ClarityLog.Instance = new ClarityLog();
            ClarityLog.LogLevel = level;
            ClarityLog.Mana._LogMode = mode;
            ClarityLog.Mana.LogFilePath = ClarityLog.Mana.CreateLogFilePath(outfol, identname);
        }


        /// <summary>
        /// Infoログの書き込み
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteInfo(params object[] obj)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Info, obj);
        }

        /// <summary>
        /// Debugログの書き込み
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteDebug(params object[] obj)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Debug, obj);
        }

        /// <summary>
        /// Errorログの書き込み
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteError(params object[] obj)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Error, obj);
        }

        /// <summary>
        /// 例外の書き込み
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteErrorException(Exception ex)
        {
            ClarityLog.WriteLog(EClarityLogLevel.Error, "===============================");
            ClarityLog.WriteLog(EClarityLogLevel.Error, ex.Message, ex.ToString());
            Exception ie = ex.InnerException;
            while(ie != null)
            {
                ClarityLog.WriteLog(EClarityLogLevel.Error, "", ex.ToString(), ex.Message);
                ie = ie.InnerException;
            }
            ClarityLog.WriteLog(EClarityLogLevel.Error, "===============================");
        }

    }
}
