using Clarity.Engine.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAid.AidProcess
{
    internal class TextureAnimeCodeGenerator : IAidProcess
    {
        public string ClassName => "ETextureAnimeCode";


        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            //処理画像一覧の取得
            List<string> pathlist = this.CreateSrcPathList(param);

            //アニメ情報読み込み
            List<IdData> idlist = this.CreateTextureAnimeCode(pathlist);


            //ファイルの書き込み
            string opath = param.CreateOutputFile($"{this.ClassName}.cs");
            CodeClassFile fp = new CodeClassFile(false);
            fp.Write(param.Mode, opath, this.ClassName, idlist, "Texture Anime ID");
        }

        /// <summary>
        /// 合成対象のパス一覧を作成する
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private List<string> CreateSrcPathList(ArgParam param)
        {
            //入力フォルダの取得
            List<string> inlist = param.GetInputList();
            //検索拡張子の取得
            List<string> klist = param.GetParameter("-k");

            //全処理ファイルのリストアップ
            List<string> anslist = Clarity.Util.ClarityUtil.ListupSpecifiedFiles(inlist, klist);
            return anslist;
        }

        /// <summary>
        /// アニメを読み込みIDへ変換
        /// </summary>
        /// <param name="inlist"></param>
        /// <returns></returns>
        private List<IdData> CreateTextureAnimeCode(List<string> inlist)
        {
            List<TextureAnimeFileDataRoot> ralist = new List<TextureAnimeFileDataRoot>();

            //読み込み
            inlist.ForEach(x =>
            {
                TextureAnimeFileDataRoot rdata = TextureAnimeFile.ReadFile(x);
                ralist.Add(rdata);
            });

            List<IdData> anslist = new List<IdData>();
            ralist.ForEach(x =>
            {
                var ff = x.AnimeDataList.Select(x => new IdData(x.AnimeCode, x.Id)).ToList();
                anslist.AddRange(ff);
            });

            return anslist;
        }

    }
}
