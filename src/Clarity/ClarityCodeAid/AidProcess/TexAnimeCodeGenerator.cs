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
    /// TextureAnimeコード生成
    /// </summary>
    public class TexAnimeCodeGenerator : IAidProcess
    {

        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateWriteFilePath(InputParam param)
        {
            string fname = "ETexAnimeCode.cs";
            string ans = param.OutputPath + System.IO.Path.AltDirectorySeparatorChar + fname;
            return ans;
        }


        /// <summary>
        /// 対象ファイルすべての読み込み
        /// </summary>
        /// <param name="inputlist"></param>
        /// <returns></returns>
        private List<TextureAnimeFileDataRoot> ReadTexAnimeFile(List<string> inputlist)
        {
            List<TextureAnimeFileDataRoot> anslist = new List<TextureAnimeFileDataRoot>();

            //対象ファイルすべての読み込み
            inputlist.ForEach(x =>
            {
                TextureAnimeFileDataRoot rdata = TextureAnimeFile.ReadFile(x);
                anslist.Add(rdata);
            });

            return anslist;
        }


        /// <summary>
        /// テクスチャアニメコードの生成
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            //処理対象ファイル一覧の取得
            List<string> inputlist = param.CreateInputFileList();
            ClarityLog.WriteInfo("Generate TexAnimeCode Start InputFileCount", inputlist.Count);

            //処理対象ファイルの読み込み
            List<TextureAnimeFileDataRoot> rdatalist = this.ReadTexAnimeFile(inputlist);

            //IDの作成
            List<IdData> idlist = new List<IdData>();
            rdatalist.ForEach(x =>
            {
                int n = x.RootID;
                x.AnimeDataList.ForEach(ad =>
               {
                   IdData idd = new IdData() { Id = n, IDName = ad.AnimeCode };
                   idlist.Add(idd);
                   n++;
               });
            });

            //ファイル書き込み
            CodeClassFile fp = new CodeClassFile();
            string outfilepath = this.CreateWriteFilePath(param);
            fp.Write(outfilepath, "ETexAnimeCode", idlist, "Texture Anime Code");

            ClarityLog.WriteInfo("Generate TexAnimeCode Complete!!");

        }
    }
}
