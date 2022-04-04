using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
namespace ClarityAid
{
    class AidFuncManage
    {
        /// <summary>
        /// 関数の取得
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static string GetAidFunc(EAidMode mode)
        {
            string ans = "";

            //元ネタを取得
            string code = Properties.Resources.AidFunc;

            using (StringReader sr = new StringReader(code))
            {
                //XMLの読み込み
                XElement xml = XElement.Load(sr);

                //対象モードのタグを探し、値を取得
                string addfuncs = xml.Element(mode.ToString())?.Value ?? "";

                //追加クラスに埋め込み
                string classcode = Properties.Resources.AidCode;
                ans = classcode.Replace("__AID_CODE__", addfuncs);
            }


            return ans;
        }
    }
}
