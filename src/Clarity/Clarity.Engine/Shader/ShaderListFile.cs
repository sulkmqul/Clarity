using Clarity.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Shader
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
        public List<ShaderSrcData> ShaderList = new List<ShaderSrcData>();
    }

    /// <summary>
    /// Shader作成元データ
    /// </summary>
    public class ShaderSrcData
    {
        /// <summary>
        /// コンストラクタ LoadShaderSrc()を呼び出すこと
        /// </summary>
        /// <param name="code">shader識別コード</param>
        /// <param name="id">shader識別ID</param>
        /// <param name="vs_name">VertexShader名</param>
        /// <param name="ps_name">PixelShader名</param>
        public ShaderSrcData(string code, int id, string vs_name, string ps_name)
        {
            this.Code = code;
            this.Id = id;
            this.VsName = vs_name;
            this.PsName = ps_name;            
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="code">shader識別コード</param>
        /// <param name="id">shader識別ID</param>
        /// <param name="vs_name">VertexShader名</param>
        /// <param name="ps_name">PixelShader名</param>
        /// <param name="src_code">コンパイルするSrcCode</param>
        public ShaderSrcData(string code, int id, string vs_name, string ps_name, string src_code) 
        {
            this.Code = code;
            this.Id = id;
            this.VsName = vs_name;
            this.PsName = ps_name;
            this.SrcCode = src_code;
        }

        /// <summary>
        /// コード
        /// </summary>
        public string Code { get; init; } = "";
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; init; } = 0;

        /// <summary>
        /// VertexShader名
        /// </summary>
        public string VsName { get; init; } = "";
        /// <summary>
        /// PixelShader名
        /// </summary>
        public string PsName { get; init; } = "";

        /// <summary>
        /// 読み込みソースコード
        /// </summary>
        internal string SrcCode { get; set; } = "";


        /// <summary>
        /// Shaderソースコードの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        public async Task LoadShaderSrc(string filepath)
        {
            using(FileStream fp = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fp))
                {
                    this.SrcCode = await sr.ReadToEndAsync();
                }
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
        /// <summary>
        /// 一覧ファイルの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public async Task<ShaderListFileDataRoot> ReadFile(string filepath)
        {
            //csvファイルの読み込み
            List<string[]> datalist = this.ReadCsvFile(filepath);

            //読み込み
            ShaderListFileDataRoot ans = await this.ReadCsvString(datalist);

            return ans;
        }

        /// <summary>
        /// 一覧文字列の読み込み
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        public async Task<ShaderListFileDataRoot> ReadCsvString(string csv)
        {
            List<string[]> datalist = new List<string[]>();

            //csvの読み込み
            using (System.IO.MemoryStream mst = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(csv)))
            {
                datalist = this.ReadCsvStream(mst);
            }

            //読み込み
            ShaderListFileDataRoot ans = await this.ReadCsvString(datalist);

            return ans;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// CSV一行の解析
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private async Task<ShaderListFileDataRoot> ReadCsvString(List<string[]> datalist)
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
                                
                int pos = 0;
                //Code
                string scode = data[pos];
                pos++;

                //IDを割り振り
                int sid = ans.RootID + i;

                //ファイルパス
                string filepath = data[pos];
                pos++;

                //Vs名
                string vsname = data[pos];
                pos++;

                //Ps名
                string psname = data[pos];
                pos++;

                ShaderSrcData sdata = new ShaderSrcData(scode, sid, vsname, psname);
                await sdata.LoadShaderSrc(filepath);

                ans.ShaderList.Add(sdata);
            }

            return ans;
        }

        
    }
}
