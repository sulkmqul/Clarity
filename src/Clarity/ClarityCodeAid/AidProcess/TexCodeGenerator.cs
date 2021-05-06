using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Texture;

namespace ClarityCodeAid.AidProcess
{
    /// <summary>
    /// テクスチャコードの生成
    /// </summary>
    public class TexCodeGenerator : IAidProcess
    {
        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateWriteFilePath(InputParam param)
        {
            string fname = "ETexCode.cs";
            string ans = param.OutputPath + System.IO.Path.AltDirectorySeparatorChar + fname;
            return ans;
        }


        /// <summary>
        /// 全テクスチャファイルの読み込み
        /// </summary>
        /// <param name="fpathlist">読み込みファイル一覧</param>
        /// <returns></returns>
        private List<TextureListFileDataRoot> ReadTextureListFile(List<string> fpathlist)
        {
            List<TextureListFileDataRoot> anslist = new List<TextureListFileDataRoot>();

            fpathlist.ForEach(x =>
            {
                //対象ファイルの読み込み
                TextureListFile fp = new TextureListFile();
                TextureListFileDataRoot rdata = fp.ReadFile(x);
                anslist.Add(rdata);
            });

            return anslist;
        }


        /// <summary>
        /// テクスチャコードの生成
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            List<string> inputfilelist = param.CreateInputFileList();
            ClarityLog.WriteInfo("Generate TexCode Start Input File Count", inputfilelist.Count);

            //処理対象ファイルの読み込み
            List<TextureListFileDataRoot> datalist = this.ReadTextureListFile(inputfilelist);

            //Code一覧の作成
            List<IdData> idlist = new List<IdData>();

            //全データのファイル名とIDの一覧を作成する
            foreach (TextureListFileDataRoot rdata in datalist)
            {
                int rid = rdata.RootID;
                rdata.TextureList.ForEach(x =>
                {
                    IdData idd = new IdData() { IDName = x.Filename, Id = rid };
                    idlist.Add(idd);
                    rid++;
                });
            }


            //ファイルへの書き込み
            CodeClassFile fp = new CodeClassFile();
            string outfilepath = this.CreateWriteFilePath(param);
            fp.Write(outfilepath, "ETexCode", idlist, "Texture Code");

            ClarityLog.WriteInfo("Generate TexCode Complete!!");
        }
    }
}
