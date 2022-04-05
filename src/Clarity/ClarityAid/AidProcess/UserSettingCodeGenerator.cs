using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;

namespace ClarityAid.AidProcess
{
    class UserSettingCodeGenerator : IAidProcess
    {
        public string ClassName => "ESettingCode";

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            //入力パスの取得
            List<string> inpathlist = param.GetInputList();
            if (inpathlist.Count <= 0)
            {
                throw new Exception("Input argument failed");
            }
            string inpath = inpathlist[0];

            //設定を読み込みcodeを取得
            Clarity.ClaritySetting set = new ClaritySetting();
            set.Read(inpath);
            List<IdData> idlist = set.GetManagedKeyCode().Select(x => new IdData(x.code.Replace('.', '_'), x.key)).ToList();



            //出力ファイルパスの作成
            string ofilepath = param.CreateOutputFile($"{this.ClassName}.cs");
            CodeClassFile es = new CodeClassFile(false);
            es.Write(param.Mode, ofilepath, this.ClassName, idlist, "Clarity Engine Setting Code");

        }
    }
}
