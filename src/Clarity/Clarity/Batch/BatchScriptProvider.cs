using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Batch
{
    

    /// <summary>
    /// バッチスクリプトファイル実行に特化したモジュール管理
    /// </summary>
    internal abstract class BatchScriptProvider : BaseModule
    {
        /// <summary>
        /// 区切り文字
        /// </summary>
        protected virtual string SHELL_DEV
        {
            get
            {
                return "^";
            }
        }

        /// <summary>
        /// chcpを利用してバッチ先頭に付与する文字コード(不必要な場合はnull)
        /// </summary>
        public virtual string? CharacterCode
        {
            get
            {
                return "65001";
            }
        }

        /// <summary>
        /// パラメータ管理
        /// </summary>
        class BatchParam
        {
            public string Name;
            public bool IncludingScript = true;
            public int OrderNo = 0;
            public object Value;

            public override string ToString()
            {
                return this.Value.ToString();
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// パラメータ管理
        /// </summary>
        private Dictionary<string, BatchParam> ParamDic = new Dictionary<string, BatchParam>();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// バッチ正常終了チェック
        /// </summary>
        /// <returns></returns>
        public abstract bool CheckResult();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// パラメータの追加
        /// </summary>
        /// <param name="key">キー</param>
        /// <param name="val">値</param>
        /// <param name="orderno">順序</param>
        /// <param name="including">作成バッチ、スクリプトに含めるか</param>
        public void AddParam(string key, object val, int? orderno=null, bool including = true)
        {
            BatchParam data = new BatchParam()
            {
                Name = key,
                Value = val,
                OrderNo = orderno ?? this.ParamDic.Count + 1,
                IncludingScript = including
            };
            this.ParamDic.Add(key, data);
        }

        /// <summary>
        /// パラメータ取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetParam<T>(string key)
        {
            T ans = (T)this.ParamDic[key].Value;
            object a;
            return ans;
        }

        /// <summary>
        /// パラメータ取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetParam(string key)
        {
            return this.ParamDic[key].Value;
        }
        /// <summary>
        /// パラメータを文字列で取得
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetParamString(string key)
        {
            return this.ParamDic[key].ToString();
        }

        /// <summary>
        /// パラメータの削除
        /// </summary>
        /// <param name="key">削除キー</param>
        /// <returns>成功可否</returns>
        public bool RemoveParam(string key)
        {
            return this.ParamDic.Remove(key);
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--
        /// <summary>
        /// 指定パスが全て存在するかを確認する
        /// </summary>
        /// <param name="cpathvec">チェックパス一式</param>
        /// <returns>ある=true</returns>
        protected bool CheckAllFileExists(string[] cpathvec)
        {
            foreach (string cpath in cpathvec)
            {
                bool ret = System.IO.File.Exists(cpath);
                if (ret == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// batファイルの作成
        /// </summary>
        /// <param name="shellfilepath">作成shellファイルパス.</param>
        /// <param name="modulepath">実行モジュールパス.</param>        
        protected void CreateShellScript(string shellfilepath, string modulepath)
        {
            //スクリプト文字列の作成
            List<string> scriptlinelist = this.CreateShellScriptString(modulepath);

            //書き込み
            using (FileStream fp = new FileStream(shellfilepath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fp))
                {
                    //念のためbatchにおけるUTF8指定を初めに入れておく
                    if (this.CharacterCode != null)
                    {
                        sw.WriteLine($"chcp {this.CharacterCode}");
                    }

                    foreach (string line in scriptlinelist)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--


        /// <summary>
        /// batch書き出し文字列の作成
        /// </summary>
        /// <returns>書き出しスクリプト各行</returns>
        /// <param name="modulepath">実行モジュールパス.</param>        
        private List<string> CreateShellScriptString(string modulepath)
        {
            string sc = "";
            List<string> anslist = new List<string>();

            //必要なパラメータを設定する
            List<string> paramlist = this.ParamDic.Values.Where(x => x.IncludingScript == true).OrderBy(x => x.OrderNo).Select(x => x.ToString()).ToList();

            //実行モジュール
            sc = modulepath + SHELL_DEV;
            anslist.Add(sc);
            //パラメータ
            foreach (string param in paramlist)
            {
                sc = param + SHELL_DEV;
                anslist.Add(sc);
            }

            return anslist;
        }

    }
}
