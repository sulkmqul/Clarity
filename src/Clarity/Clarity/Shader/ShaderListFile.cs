using Clarity.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Shader
{
    /// <summary>
    /// Shader一覧ファイルデータ
    /// </summary>
    public class ShaderListFileDataRoot
    {
        /// <summary>
        /// ファイルルートID
        /// </summary>
        public int RootID = ClarityEngine.INVALID_ID;

        /// <summary>
        /// 読み込みデータ一式
        /// </summary>
        public List<ShaderListData> ShaderList = new List<ShaderListData>();
    }

    /// <summary>
    /// シェーダーデータ
    /// </summary>
    public class ShaderListData
    {
        /// <summary>
        /// これのコード
        /// </summary>
        public string Code;
        /// <summary>
        /// 読み込みShaderファイルパス
        /// </summary>
        public string FilePath;
        /// <summary>
        /// VertexShader名
        /// </summary>
        public string VsName;
        /// <summary>
        /// PixelShader名
        /// </summary>
        public string PsName;


        /// <summary>
        /// ファイルパスが有効化否か(ここがfalseの場合、DefaultShaderファイルを読む)
        /// </summary>
        public bool EnabledFilePath
        {
            get
            {
                if (this.FilePath.Length <= 0)
                {
                    return false;
                }
                return true;
            }
        }

    }

    //ファイル仕様
    //一行目にルート番号、以後は
    //[Code],[Shaderファイルパス],[VertexShader名],[PixelShader名]
    //以上が続く。ジオメトリやオプションなどの拡張を考慮する

    /// <summary>
    /// 使用するシェーダー一覧ファイル
    /// </summary>
    public class ShaderListFile : BaseCsvFile
    {

        private ShaderListFileDataRoot ReadCsvString(List<string[]> datalist)
        {
            ShaderListFileDataRoot ans = new ShaderListFileDataRoot();

            //一行目のRootIDを読みこみ
            ans.RootID = Convert.ToInt32(datalist[0][0]);

            //特にコンマなしの予定なので0番目を読む込む
            for (int i = 1; i < datalist.Count; i++)
            {
                string[] data = datalist[i];

                if (data.Length < 2)
                {
                    continue;
                }

                ShaderListData sdata = new ShaderListData();
                int pos = 0;
                //Code
                sdata.Code = data[pos];
                pos++;

                //ファイルパス
                sdata.FilePath = data[pos];
                pos++;

                //Vs名
                sdata.VsName = data[pos];
                pos++;

                //Ps名
                sdata.PsName = data[pos];
                pos++;


                ans.ShaderList.Add(sdata);
            }

            return ans;
        }

        /// <summary>
        /// 一覧ファイルの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public ShaderListFileDataRoot ReadFile(string filepath)
        {
            //csvファイルの読み込み
            List<string[]> datalist = this.ReadCsvFile(filepath);

            //読み込み
            ShaderListFileDataRoot ans = this.ReadCsvString(datalist);

            return ans;
        }


        /// <summary>
        /// 一覧文字列の読み込み
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        public ShaderListFileDataRoot ReadCsvString(string csv)
        {
            List<string[]> datalist = new List<string[]>();

            //csvの読み込み
            using (System.IO.MemoryStream mst = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(csv)))
            {
                datalist = this.ReadCsvStream(mst);
            }

            //読み込み
            ShaderListFileDataRoot ans = this.ReadCsvString(datalist);

            return ans;
        }
    }
}
