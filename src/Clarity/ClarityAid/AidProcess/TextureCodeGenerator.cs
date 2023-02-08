using Clarity.Engine.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAid.AidProcess
{
    /// <summary>
    /// テクスチャコードの生成
    /// </summary>
    internal class TextureCodeGenerator : IAidProcess
    {
        public string ClassName => "ETextureCode";



        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            //入力一覧の取得
            List<string> inputpath = param.GetInputList();

            //エンジン側の定義を取得
            List<IdData> idlist = IAidProcess.CreateSystemConst(typeof(Clarity.Engine.ClarityEngine.BuildInTextureIndex));

            //一覧の一括読み込み
            TextureManager.Create();
            TextureManager.Mana.CreateResource(inputpath, false);

            //書き込み用Listの作成
            List<IdData> tlist = TextureManager.Mana.CreateIDList().Select(x => new IdData(x.code, x.id)).ToList();


            idlist.AddRange(tlist);

            string opath = param.CreateOutputFile($"{this.ClassName}.cs");
            CodeClassFile fp = new CodeClassFile(false);
            fp.Write(param.Mode, opath, this.ClassName, idlist, "Texture ID");
        }
    }
}
