using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Batch
{
    /// <summary>
    /// 外部モジュール管理基底
    /// </summary>
    internal abstract class BaseModule
    {
        /// <summary>
        /// モジュール画面表示可否
        /// </summary>
        protected virtual bool ModuleWindowsEnabled
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 起動したプロセス情報を保持する
        /// </summary>
        protected Process? ModuleProcess { get; private set; } = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 即時kill
        /// </summary>
        public void KillProcess()
        {
            this.ModuleProcess?.Kill();
        }

        /// <summary>
        /// 非同期モジュール実行
        /// </summary>
        /// <param name="execfilepath">実行パス</param>
        /// <param name="arguments">起動引数</param>
        /// <param name="wfol">カレントフォルダ null=実行パスと同じ</param>
        /// <returns></returns>
        protected Task ExecuteModuleAsync(string execfilepath, string arguments = "", string? wfol = null)
        {
            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

            //プロセスの作成
            this.ModuleProcess = this.CreateModuleProcess(execfilepath, arguments, wfol);
            Process pro = this.ModuleProcess;

            //終了イベントの設定
            pro.Exited += (sernder, ars) =>
            {
                tcs.SetResult(true);
                pro.Dispose();
            };

            //実行開始
            pro.Start();
            return tcs.Task;

        }


        /// <summary>
        /// 実行プロセスの作成
        /// </summary>
        /// <param name="execfilepath">実行ファイルパス</param>
        /// <param name="arguments">起動引数</param>
        /// <param name="workingdirectorypath">カレントフォルダ null=実行パスと同じ</param>
        /// <returns></returns>
        private Process CreateModuleProcess(string execfilepath, string arguments, string? workingdirectorypath)
        {
            Process pro = new Process();

            //コマンド実行設定            
            //linuxするならこっち
            //pro.StartInfo.FileName = "sh";
            //pro.StartInfo.Arguments = execfilepath;
            pro.StartInfo.CreateNoWindow = true;
            pro.StartInfo.FileName = execfilepath;
            pro.StartInfo.Arguments = arguments;
            pro.StartInfo.UseShellExecute = true;
            pro.StartInfo.WorkingDirectory = workingdirectorypath ?? Path.GetDirectoryName(execfilepath);

            //終了時のイベント発生
            pro.EnableRaisingEvents = true;

            if (this.ModuleWindowsEnabled == false)
            {
                //window画像表示
                pro.StartInfo.CreateNoWindow = false;
                pro.StartInfo.UseShellExecute = true;
                pro.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            }

            return pro;
        }

        

        
    }
}
