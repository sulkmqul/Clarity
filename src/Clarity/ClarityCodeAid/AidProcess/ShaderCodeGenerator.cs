using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Shader;

namespace ClarityCodeAid.AidProcess
{
    /// <summary>
    /// ShaderCodeの生成
    /// </summary>
    public class ShaderCodeGenerator : IAidProcess
    {

        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateWriteFilePath(InputParam param)
        {
            string fname = "EShaderCode.cs";
            string ans = param.OutputPath + System.IO.Path.AltDirectorySeparatorChar + fname;
            return ans;
        }

        /// <summary>
        /// ShadeListの読み込み
        /// </summary>
        /// <param name="inputlist">読み込み対象一覧</param>
        /// <returns></returns>
        private List<ShaderListFileDataRoot> ReadShaderListFile(List<string> inputlist)
        {
            List<ShaderListFileDataRoot> anslist = new List<ShaderListFileDataRoot>();

            //対象全部の読み込み
            foreach (string sif in inputlist)
            {
                ShaderListFile fp = new ShaderListFile();
                ShaderListFileDataRoot rdata = fp.ReadFile(sif);
                anslist.Add(rdata);
            }

            return anslist;
        }

        /// <summary>
        /// Shaderコードの作成
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            //入力の取得
            List<string> inputlist = param.CreateInputFileList();
            ClarityLog.WriteInfo("Generate ShaderCode Start Input File Count", inputlist.Count);

            //ファイル一覧の読み込み
            List<ShaderListFileDataRoot> rdatalist = this.ReadShaderListFile(inputlist);


            //IDの割り振り
            List<IdData> idlist = new List<IdData>();
            foreach (ShaderListFileDataRoot rdata in rdatalist)
            {
                int n = rdata.RootID;

                rdata.ShaderList.ForEach(x =>
               {
                   IdData idd = new IdData() { IDName = x.Code, Id = n };
                   idlist.Add(idd);
                   n++;
               });
            }


            //ファイルへの書き込み
            CodeClassFile fp = new CodeClassFile();
            string outfilepath = this.CreateWriteFilePath(param);
            fp.Write(outfilepath, "EShaderCode", idlist, "Shader Code");

            ClarityLog.WriteInfo("Generate ShaderCode Complete!!");
        }
    }
}
