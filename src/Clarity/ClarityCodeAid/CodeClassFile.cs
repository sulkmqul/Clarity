using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Clarity.Aid.Support;

namespace ClarityCodeAid
{
    


    /// <summary>
    /// 出力するコード一覧クラスファイルの管理
    /// </summary>
    public class CodeClassFile
    {
        private static string SrcText = @"
namespace Clarity
{
    /// <summary>
    /// __CLASS_DESC__
    /// </summary>
    public class __CLASS_NAME__
    {
__MEMBER_CODE__
    }
}
";

        //private static string MemberText = "\t\tpublic static readonly int {0} = {1};";
        private static string MemberText = "\t\tpublic const int {0} = {1};";


        static readonly string NewLineCode = "___NNDDLL__";

        /// <summary>
        /// 全IDを生成する
        /// </summary>
        /// <param name="datalist">書き出し変数名一覧</param>
        /// <returns></returns>
        private string GenerateIDString(List<IdData> datalist)
        {
            string ans = "";


            //とりあえず一つの文字列として生成
            foreach (IdData data in datalist)
            {
                string s = string.Format(CodeClassFile.MemberText, data.IDName, data.Id);
                ans += s;
                ans += CodeClassFile.NewLineCode;
            }

            return ans;
        }




        /// <summary>
        /// 書き込み
        /// </summary>
        /// <param name="filepath">書き込みファイルパス</param>
        /// <param name="classname">出力クラス名</param>
        /// <param name="datalist">出力名一式</param>
        public void Write(string filepath, string classname, List<IdData> datalist, string comment = "")
        {

            try
            {
                //全ID文字列の作成
                string ids = this.GenerateIDString(datalist);

                //書き込み文字列の作成         
                //クラス名
                string ws = CodeClassFile.SrcText.Replace("__CLASS_NAME__", classname);
                //コメント説明
                ws = ws.Replace("__CLASS_DESC__", comment);
                //全ID
                ws = ws.Replace("__MEMBER_CODE__", ids);

                //最後に改行を埋め込む
                ws = ws.Replace(CodeClassFile.NewLineCode, Environment.NewLine);

                //ファイルOpen
                using (FileStream fp = new FileStream(filepath, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fp, System.Text.Encoding.UTF8))
                    {
                        sw.Write(ws);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("WriteText失敗", e);
            }
        }
    }
}
