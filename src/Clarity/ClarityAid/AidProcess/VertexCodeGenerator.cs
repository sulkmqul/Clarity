using Clarity.Engine.Vertex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityAid.AidProcess
{
    /// <summary>
    /// ポリゴンモデルコード生成
    /// </summary>
    internal class VertexCodeGenerator : IAidProcess
    {
        public string ClassName => "EVertexCode";

        /// <summary>
        /// Batch処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            //入力ファイルの取得と変換
            List<string> inputlist = param.GetInputList();
            List<PolygonListFileDataRoot> polist = this.ReadPolygonList(inputlist);

            //buildinのデータ取得
            List<IdData> idlist = IAidProcess.CreateSystemConst(typeof(Clarity.Engine.ClarityEngine.BuildInPolygonModelIndex));

            //IDの作成
            polist.ForEach(x =>
            {
                x.PolyFileList.ForEach(y =>
                {
                    idlist.Add(new IdData(y.Code, y.Id));
                });
            });


            //ファイルの書き込み
            string opath = param.CreateOutputFile($"{this.ClassName}.cs");
            CodeClassFile fp = new CodeClassFile(false);
            fp.Write(param.Mode, opath, this.ClassName, idlist, "Vertex ID");

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// PolygonListの読み込み
        /// </summary>
        /// <param name="inputlist">入力一式</param>
        /// <returns></returns>
        private List<PolygonListFileDataRoot> ReadPolygonList(List<string> inputlist)
        {
            List<PolygonListFileDataRoot> anslist = new List<PolygonListFileDataRoot>();

            //全ファイルの読み込み
            inputlist.ForEach(x =>
            {
                PolygonListFile fp = new PolygonListFile();
                PolygonListFileDataRoot ans = fp.ReadFile(x);
                anslist.Add(ans);
            });

            return anslist;
        }
    }
}
