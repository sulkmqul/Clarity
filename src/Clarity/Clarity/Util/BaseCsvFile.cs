using System;
using System.Collections.Generic;
using System.IO;

namespace Clarity.Util
{
    /// <summary>
    /// CSVファイル処理基底クラス
    /// </summary>    
    public abstract class BaseCsvFile
    {
        /// <summary>
        /// 区切り文字列定義
        /// </summary>
        public virtual char DevChar
        {
            get; protected set;

        } = ',';

        /// <summary>
        /// コメント行を示すもの (先頭にこの文字列があった場合、対象行は無視される)
        /// </summary>
        protected virtual string CommentString { get; } = "#";



        /// <summary>
        /// コメント行チェック true=この行はコメントである。
        /// </summary>
        /// <param name="sline"></param>
        /// <returns></returns>
        private bool CheckComment(string sline)
        {
            int pos = sline.Trim().IndexOf(CommentString);
            //先頭の場合、コメントと解釈する
            if (pos == 0)
            {
                return true;
            }
            return false;

        }

        //-------------------------------------------------------------

        /// <summary>
        /// 空白チェック、csvで区切られた内容がすべて空白の場合をチェックする true=空白行である。
        /// </summary>
        /// <param name="datavec"></param>
        /// <returns></returns>
        protected bool CheckBlank(string[] datavec)
        {
            int len = 0;
            foreach (string s in datavec)
            {
                len += s.Trim().Length;
            }
            if (len <= 0)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// CSV読み込み本体
        /// </summary>
        /// <param name="st">読み込みStream</param>
        /// <param name="dels">削除文字空文字=削除なし</param>
        /// <param name="commentskip">コメントスキップ可否 true=コメント読み込まない false=読みこむ</param>
        /// <returns></returns>
        protected List<string[]> ReadCsvStream(Stream st, string dels = "", bool commentskip = true)
        {
            List<string[]> anslist = new List<string[]>();

            using (StreamReader sr = new StreamReader(st))
            {
                //全行読み込み
                while (sr.EndOfStream != true)
                {
                    string sline = sr.ReadLine();

                    //空白は無きものとする
                    sline = sline.Trim();

                    //コメントチェック
                    bool ckcom = CheckComment(sline);
                    if (ckcom == true && commentskip == true)
                    {
                        continue;
                    }

                    //削除文字の削除
                    foreach (char c in dels)
                    {
                        sline = sline.Replace(c.ToString(), "");
                    }

                    //空行はスキップする
                    if (sline.Length <= 0)
                    {
                        continue;
                    }

                    //ADD
                    string[] ss = sline.Split(DevChar);
                    anslist.Add(ss);
                }
            }


            return anslist;
        }


        /// <summary>
        /// Csvファイル読み込み 失敗=NULL
        /// </summary>
        /// <param name="filepath">読み込みファイル名</param>
        /// <param name="dels">削除する文字一式 空文字＝削除しない 文字列ではなく単品の文字として解釈abと指定した場合、aとbを削除する。</param>
        /// <param name="commentskip">コメント読み込みを行うか否か  true=コメント読み込まない false=読みこむ</param>
        /// <returns>作成物</returns>
        protected List<string[]> ReadCsvFile(string filepath, string dels = "", bool commentskip = true)
        {
            List<string[]> anslist = new List<string[]>();

            try
            {
                //ファイルを読み込み、
                using (FileStream fp = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                {
                    anslist = ReadCsvStream(fp, dels, commentskip);
                }
            }
            catch (Exception e)
            {
                throw new Exception("ReadCsv Exception", e);
            }


            return anslist;
        }

        /// <summary>
        /// CSVファイル書き込み
        /// </summary>
        /// <param name="filepath">書き込みファイル名</param>
        /// <param name="datalist">書き込みデータ</param>
        /// <returns>成功可否</returns>
        protected void WriteCsvFile(string filepath, List<List<string>> datalist)
        {
            try
            {
                //ファイルOpen
                using (FileStream fp = new FileStream(filepath, FileMode.Create, FileAccess.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fp))
                    {
                        //データを書き込み
                        foreach (List<string> slist in datalist)
                        {
                            string sline = "";
                            foreach (string s in slist)
                            {
                                sline += s;
                                sline += DevChar;
                            }
                            //最後のコンマを消す
                            sline = sline.Remove(sline.Length - 1);

                            //書き込み
                            sw.WriteLine(sline);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("WriteCsv Exception", e);
            }

        }

    }
}
