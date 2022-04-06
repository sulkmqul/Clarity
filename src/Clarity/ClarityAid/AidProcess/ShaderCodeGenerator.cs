using Clarity.Engine.Shader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAid.AidProcess
{
    /// <summary>
    /// ShaderCodeの生成
    /// </summary>
    internal class ShaderCodeGenerator : IAidProcess
    {
        public string ClassName => "EShaderCode";


        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            //入力ファイル一覧の取得と読み込み
            List<string> inputlist = param.GetInputList();
            List<ShaderListFileDataRoot> rdatalist = this.ReadShaderListFile(inputlist);

            //BuildInIndexの作成
            List<IdData> idlist = IAidProcess.CreateSystemConst(typeof(Clarity.Engine.Core.BuildInShaderIndex));

            //全IDの追加
            rdatalist.ForEach(x =>
            {
                x.ShaderList.ForEach(sh =>
                {
                    idlist.Add(new IdData(sh.Code, sh.Id));
                });
            });

            //ファイルの書き込み
            string opath = param.CreateOutputFile($"{this.ClassName}.cs");
            CodeClassFile fp = new CodeClassFile(false);
            fp.Write(param.Mode, opath, this.ClassName, idlist, "Shader ID");

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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
    }
}
