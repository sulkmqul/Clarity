using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Aid.Support
{
    public class IdData
    {
        /// <summary>
        /// ID名
        /// </summary>
        public string IDName = "";

        /// <summary>
        /// ID値
        /// </summary>
        public int Id = Clarity.ClarityEngine.INVALID_ID;
    }

    public class CodeAidSupport
    {
        /// <summary>
        /// ユーザー設定IDの作成
        /// </summary>
        /// <param name="filepath">読み込み元ファイルパス</param>
        /// <returns></returns>
        public static List<IdData> CreateUserSettinfCodeList(string filepath)
        {
            List<IdData> anslist = new List<IdData>();

            Clarity.File.ClarityUserSetting fp = new File.ClarityUserSetting();
            fp.LoadSetting(filepath);

            var f = fp.DataDic.Values.Select(x =>
            {
                IdData a = new IdData() { Id = x.Id, IDName = x.Code };
                return a;
            });

            anslist = f.ToList();
            return anslist;
        }



        /// <summary>
        /// テクスチャコードの生成
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<IdData> CreateTextureCodeList(List<string> fpathlist)
        {
            List<IdData> anslist = new List<IdData>();

            //対象のテクスチャリストの読み込み
            Texture.TextureManager.Create();
            Texture.TextureManager.Mana.CreateResource(fpathlist, false);


            var f = Texture.TextureManager.Mana.ManaDic.Keys.Select(x =>
            {
                IdData a = new IdData();
                a.Id = x;
                a.IDName = Texture.TextureManager.Mana.ManaDic[x].Code;
                return a;
            });

            anslist = f.ToList();

            return anslist;
        }
    }
}
