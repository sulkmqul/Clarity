using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Clarity;

namespace TrainSrcArranger
{
    /// <summary>
    /// 構成設定の取得
    /// </summary>
    internal class AppConfig : BaseAppConfigManager<AppConfig>
    {
        /// <summary>
        /// 切り出し画像の初期設定値
        /// </summary>
        public Size InitialCutSize
        {
            get
            {
                try
                {
                    string val = this.GetString("InitialCutSize", "512,512");
                    string[] data = val.Split(",");
                    int w = Convert.ToInt32(data[0]);
                    int h = Convert.ToInt32(data[1]);

                    return new Size(w, h); 
                }
                catch
                {
                    return new Size(512, 512);
                }
                
            }
        }

        /// <summary>
        /// 画像保存のフォーマット
        /// </summary>
        public string SaveFileNameFormat
        {
            get
            {
                return this.GetString("SaveFileNameFormat", "{0}.png");
            }
        }

        /// <summary>
        /// 規定の出力フォルダ
        /// </summary>
        public string InitialOutputFolder
        {
            get
            {
                return this.GetString("InitialOutputFolder", "");
            }
        }


    }
}
