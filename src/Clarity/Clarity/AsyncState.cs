using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity
{
    internal class AsyncState
    {
    }

    /// <summary>
    /// 作業フォルダ一時作成ステート
    /// </summary>
    /// <remarks>try catchをしておくこと</remarks>
    public class WorkingFolderState : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="wfolpath">作業フォルダパス</param>
        /// <param name="f">終わった時の削除可否 true=削除</param>
        public WorkingFolderState(string wfolpath, bool f = true)
        {
            this.WorkFolderPath = wfolpath;
            this.CleanupFlag = f;
            this.CreateWorkFolder();
        }

        /// <summary>
        /// 管理作業フォルダパス
        /// </summary>
        public string WorkFolderPath { get; protected set; }

        /// <summary>
        /// 作業フォルダ削除可否 true=削除する
        /// </summary>
        private bool CleanupFlag = true;


        public void Dispose()
        {
            if (this.CleanupFlag == true)
            {
                this.RemoveWorkFolder();
            }
        }


        /// <summary>
        /// 作業フォルダの作成
        /// </summary>
        /// <returns>作成した作業フォルダのパス</returns>
        public void CreateWorkFolder()
        {
            string folpath = this.WorkFolderPath;
            System.IO.Directory.CreateDirectory(folpath);

        }

        /// <summary>
        /// 作業フォルダの削除
        /// </summary>
        protected void RemoveWorkFolder()
        {
            string folpath = this.WorkFolderPath;
            bool f = System.IO.Directory.Exists(folpath);
            if (f == false)
            {
                return;
            }
            System.IO.Directory.Delete(folpath, true);
        }
    }


    /// <summary>
    /// 開始と終了に規定処理を行うもの
    /// </summary>
    public class BlockProcedureState : IDisposable
    {
        public BlockProcedureState(Action st, Action ed)
        {
            this.StartProc = st;
            this.EndProc = ed;

            this.StartProc();
        }

        private Action StartProc;
        private Action EndProc;

        public void Dispose()
        {
            this.EndProc();
        }
    }




}
