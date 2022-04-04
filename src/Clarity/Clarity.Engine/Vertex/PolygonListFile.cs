using Clarity.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Vertex
{
    /// <summary>
    /// Poly一覧ファイルデータ
    /// </summary>
    internal class PolygonListFileDataRoot
    {
        /// <summary>
        /// ルートID
        /// </summary>
        public int RootID = ClarityEngine.INVALID_ID;

        /// <summary>
        /// ポリゴンファイル一覧
        /// </summary>
        public List<PolygonListData> PolyFileList = new List<PolygonListData>();

    }

    /// <summary>
    /// ポリゴン一覧データ
    /// </summary>
    internal class PolygonListData
    {
        /// <summary>
        /// これのID
        /// </summary>
        public int Id;

        /// <summary>
        /// これのコード
        /// </summary>
        public string Code
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(this.FilePath);
            }
        }
            


        /// <summary>
        /// ポリゴンオブジェクトファイルパス
        /// </summary>
        public string FilePath = "";


    }

    /// <summary>
    /// ポリゴン一覧ファイルの読み取り
    /// </summary>
    internal class PolygonListFile : BaseCsvFile
    {
        /// <summary>
        /// ポリゴン一覧ファイルの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public PolygonListFileDataRoot ReadFile(string filepath)
        {
            PolygonListFileDataRoot ans = new PolygonListFileDataRoot();

            //csvの読み込み
            List<string[]> datalist = this.ReadCsvFile(filepath);

            //一行目のrootid読み込み
            ans.RootID = Convert.ToInt32(datalist[0][0]);

            //特にコンマなしの予定なので0番目を読む込む
            for(int i=1; i<datalist.Count; i++)
            {
                string[] data = datalist[i];

                PolygonListData pdata = new PolygonListData();
                int pos = 0;

                //ファイルパス
                pdata.FilePath = data[pos];                
                pos++;

                //ID
                pdata.Id = ans.RootID + i;

                ans.PolyFileList.Add(pdata);
            }

            

            return ans;
        }
    }
}
