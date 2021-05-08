using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;

namespace ClarityCodeAid.AidProcess
{
    /// <summary>
    /// ClarityEnine設定ファイルの書き出し
    /// </summary>
    public class ClarityEngineSettingCreator : IAidProcess
    {


        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateWriteFilePath(InputParam param)
        {
            string fname = "cs.xml";
            string ans = param.OutputPath + System.IO.Path.AltDirectorySeparatorChar + fname;
            return ans;
        }

        /// <summary>
        /// デフォルト設定書き出し処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            string writepath = this.CreateWriteFilePath(param);

            //データの書き出し
            //ClarityEngineSetting cdata = new ClarityEngineSetting();
            //Clarity.File.ClarityEngineSettingFile.WriteSetting(writepath, cdata);
            ClarityEngine.WriteDefaultEngineSetting(writepath);
            

        }
    }
}
