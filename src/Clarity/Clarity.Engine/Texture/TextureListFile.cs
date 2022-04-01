using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.File;
using System.Drawing;

namespace Clarity.Engine.Texture
{
    //仕様
    //一行名はルートID 以後
    //[ファイルパス],[透過色：マイナスで透過無し],[画像数X],[画像数Y]
    //パス無し拡張子無しのファイル名が中での識別名になる予定

    internal class TextureListFileDataRoot
    {
        /// <summary>
        /// このファイルのRootID
        /// </summary>
        public int RootID = ClarityEngine.INVALID_ID;

        /// <summary>
        /// ファイルのデータ一覧
        /// </summary>
        public List<TextureListFileData> TextureList = new List<TextureListFileData>();
    }



    /// <summary>
    /// テクスチャファイルデータ一覧名
    /// </summary>
    internal class TextureListFileData
    {   
        /// <summary>
        /// テクスチャファイルパス
        /// </summary>
        public string FilePath = "";
                
        /// <summary>
        /// 透過色　マイナス値で透過色を指定しない
        /// </summary>
        public int TransColor = -1;

        /// <summary>
        /// 含まれる画像index数
        /// </summary>
        public Size IndexSize = new Size(1, 1);

        /// <summary>
        /// これのファイル名だけを取得する
        /// </summary>
        public string Filename
        {
            get
            {
                string ans = System.IO.Path.GetFileNameWithoutExtension(this.FilePath);
                return ans;
            }
        }



        /// <summary>
        /// Colorで取得
        /// </summary>
        public System.Drawing.Color? Color
        {
            get
            {
                if (this.TransColor < 0)
                {
                    return null;
                }

                System.Drawing.Color col = System.Drawing.Color.FromArgb(this.TransColor);
                return col;
            }
        }
    }


    /// <summary>
    /// テクスチャデータ一覧ファイル
    /// </summary>
    internal class TextureListFile : BaseCsvFile
    {


        /// <summary>
        /// 一覧ファイルの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public TextureListFileDataRoot ReadFile(string filepath)
        {
            //csvの読み込み
            List<string[]> datalist = this.ReadCsvFile(filepath);

            //作成
            TextureListFileDataRoot ans = new TextureListFileDataRoot();

            //一行目はIDのはず
            ans.RootID = Convert.ToInt32(datalist[0][0]);            

            //特にコンマなしの予定なので0番目を読む込む
            //foreach (string[] data in datalist)
            for(int i=1; i<datalist.Count; i++)
            {
                string[] data = datalist[i];


                bool ckblank = this.CheckBlank(data);
                if (ckblank == true)
                {
                    continue;
                }

                TextureListFileData tdata = new TextureListFileData();
                int pos = 0;

                //ファイルパス
                tdata.FilePath = data[pos];
                pos++;
                //透過色
                tdata.TransColor = Convert.ToInt32(data[pos]);
                pos++;

                //画像数
                int icx = Convert.ToInt32(data[pos]);
                pos++;
                int icy = Convert.ToInt32(data[pos]);
                pos++;
                tdata.IndexSize = new Size(icx, icy);

                ans.TextureList.Add(tdata);
            }

            return ans;
        }
    }
}
