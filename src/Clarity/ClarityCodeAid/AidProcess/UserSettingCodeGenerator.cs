using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Aid.Support;

namespace ClarityCodeAid.AidProcess
{
    /// <summary>
    /// ユーザー設定コードの書き出し
    /// </summary>
    public class UserSettingCodeGenerator : IAidProcess
    {
        static readonly string TypeName = "ESettingCode";

        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateWriteFilePath(InputParam param)
        {
            string fname = TypeName + ".cs";
            string ans = param.OutputPath + System.IO.Path.AltDirectorySeparatorChar + fname;
            return ans;
        }


        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            List<string> inputfilelist = param.CreateInputFileList();
            ClarityLog.WriteInfo("Generate UserSettingCode Start");

            //処理対象ファイルの読み込み
            string filepath = inputfilelist[0];


            //Code一覧の作成
            List<IdData> idlist = CodeAidSupport.CreateUserSettinfCodeList(filepath);


            //ファイルへの書き込み
            CodeClassFile fp = new CodeClassFile();
            string outfilepath = this.CreateWriteFilePath(param);
            fp.Write(outfilepath, TypeName, idlist, "User Setting Code");

            ClarityLog.WriteInfo("Generate UserSetting Complete!!");
        }
    }
}
