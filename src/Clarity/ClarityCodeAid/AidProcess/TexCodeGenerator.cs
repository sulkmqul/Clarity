﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Aid.Support;

namespace ClarityCodeAid.AidProcess
{
    /// <summary>
    /// テクスチャコードの生成
    /// </summary>
    public class TexCodeGenerator : IAidProcess
    {
        static readonly string TypeName = "ETexCode";

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
        /// テクスチャコードの生成
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            List<string> inputfilelist = param.CreateInputFileList();
            ClarityLog.WriteInfo("Generate TexCode Start Input File Count", inputfilelist.Count);

            //Code一覧の作成
            List<IdData> idlist = CodeAidSupport.CreateTextureCodeList(inputfilelist);


            //ファイルへの書き込み
            CodeClassFile fp = new CodeClassFile();
            string outfilepath = this.CreateWriteFilePath(param);
            fp.Write(outfilepath, TypeName, idlist, "Texture Code");

            ClarityLog.WriteInfo("Generate TexCode Complete!!");
        }
    }
}
