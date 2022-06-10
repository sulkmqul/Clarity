using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;

namespace ClarityAid.AidProcess
{
    class InitialParamCodeGenerator : IAidProcess
    {
        public string ClassName => "EInitialParameterCode";


        /// <summary>
        /// 処理本体
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            List<string> inlist = this.CreateSrcPathList(param);

            //入力の読み込み
            ClarityInitialParameter ip = new ClarityInitialParameter();
            ip.LoadFile(inlist);
                        

            //管理IDの一覧を取得して変換する
            List<IdData> idlist = ip.CreateCodeIdList().Select(x => new IdData(x.code, x.id)).ToList();

            //Default追加
            idlist.Insert(0, new IdData("Default", -99));

            string opath = param.CreateOutputFile($"{this.ClassName}.cs");
            CodeClassFile fp = new CodeClassFile(false);
            fp.Write(param.Mode, opath, this.ClassName, idlist, "Initial Parameter Code");

        }

        /// <summary>
        /// パス一覧を作成する
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private List<string> CreateSrcPathList(ArgParam param)
        {
            //入力フォルダの取得
            List<string> inlist = param.GetInputList();
            //検索拡張子の取得
            List<string> klist = param.GetParameter("-k");

            //全データのリストアップ
            List<string> anslist = Clarity.Util.ClarityUtil.ListupSpecifiedFiles(inlist, klist);
            return anslist;
        }
    }
}
