using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;

namespace Clarity.File
{
    /// <summary>
    /// エンジン設定ファイルの読み込み
    /// </summary>
    internal class ClarityEngineSettingFile
    {
        /// <summary>
        /// 設定の読み込み
        /// </summary>
        /// <param name="filepath">設定ファイルパス</param>
        public static ClarityEngineSetting ReadSetting(string filepath)
        {
            ClarityEngineSetting ans = null;

            return ans;
        }


        /// <summary>
        /// 設定の書き込み
        /// </summary>
        /// <param name="filepath">設定ファイルパス</param>
        public static void WriteSetting(string filepath, ClarityEngineSetting data)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(ClarityEngineSetting));
                using (FileStream fp = new FileStream(filepath, FileMode.Create))
                {
                    xml.Serialize(fp, data);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Clarity Setting Write Exception", e);
            }
        }
    }
}
