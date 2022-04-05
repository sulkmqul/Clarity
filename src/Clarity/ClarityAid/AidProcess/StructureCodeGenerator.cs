using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Engine.Element;

namespace ClarityAid.AidProcess
{

    class StructureCode : EngineStructureManager
    {
        /// <summary>
        /// 全Nodeの取得
        /// </summary>
        /// <returns></returns>
        public List<ClarityStructure> GetAllNodes()
        {
            return this.NodeDic.Values.ToList();
        }
    }

    /// <summary>
    /// 構造コードの作成
    /// </summary>
    internal class StructureCodeGenerator : IAidProcess
    {
        public StructureCodeGenerator() 
        {
        }

        public string ClassName => "EStructureCode";

        /// <summary>
        /// 処理の実行
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            try
            {
                //入力ファイルの取得
                List<string> inputlist = param.GetInputList();                
                if (inputlist.Count <= 0)
                {
                    throw new Exception("Input argument failed");
                }

                //ファイルの読み込み
                ClarityStructureFile fp = new ClarityStructureFile();
                ClarityStructure m = fp.ReadStructure(inputlist[0]);
                
                //データの読み込み
                StructureCode mana = new StructureCode();
                mana.Init(m);

                //全ノードの取得し変換
                List<ClarityStructure> nodelist = mana.GetAllNodes();
                List<IdData> idlist = nodelist.Select(x => new IdData(x.Code, (int)x.ID)).ToList();



                //出力ファイルパスの作成
                string ofilepath = param.CreateOutputFile( $"{this.ClassName}.cs");
                CodeClassFile es = new CodeClassFile(false);
                es.Write(param.Mode, ofilepath, this.ClassName, idlist, "Clarity Engine Structure IDs");



            }
            catch (Exception ex)
            {
                throw new Exception("StructureCodeGenerator", ex);
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
    }
}
