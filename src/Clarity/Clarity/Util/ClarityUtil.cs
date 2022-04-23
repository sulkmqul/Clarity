using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Clarity.Util
{
    /// <summary>
    /// お役立ち関数一式
    /// </summary>
    public class ClarityUtil
    {

        /// <summary>
        /// 対象がフォルダか否かをチェックする
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static  bool CheckDirectory(string s)
        {
            return Directory.Exists(s);


        }

        /// <summary>
        /// フォルダとファイルの一覧からフォルダ内を検索し、ファイルだけを列挙する
        /// </summary>
        /// <param name="iplist">元パスリスト</param>
        /// <param name="keylist">フォルダ検索キー(*.ext)形式</param>
        /// <returns></returns>
        public static List<string> ListupSpecifiedFiles(List<string> iplist, List<string> keylist)
        {
            List<string> anslist = new List<string>();

            iplist.ForEach(s =>
            {
                //これはディレクトリ？
                bool fr = ClarityUtil.CheckDirectory(s);
                if (fr == true)
                {
                    //ディレクトリだった
                    List<string> flist = new List<string>();
                    foreach (string extkey in keylist)
                    {
                        string[] filevec = Directory.GetFiles(s, extkey);
                        flist.AddRange(filevec);
                    }

                    //全部追加
                    anslist.AddRange(flist);
                }
                else
                {
                    //ファイルだった
                    anslist.Add(s);
                }
            });


            return anslist;
        }
    }
}
