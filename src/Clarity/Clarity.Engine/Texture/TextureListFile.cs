using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.File;
using System.Drawing;
using System.IO;
using System.Xml.Linq;

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
    internal class TextureListFile
    {


        /// <summary>
        /// 一覧ファイルの読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public TextureListFileDataRoot ReadTextureList(string filepath)
        {
            //作成
            TextureListFileDataRoot ans = new TextureListFileDataRoot();

            using (FileStream fp = new FileStream(filepath, FileMode.Open))
            {
                ans = this.ReadTextureList(fp);
            }

            return ans;
        }

        /// <summary>
        /// 一覧ファイルの読み込み
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public TextureListFileDataRoot ReadTextureList(Stream fp)
        {
            //作成
            TextureListFileDataRoot ans = new TextureListFileDataRoot();

            try
            {
                XElement xml = XElement.Load(fp);

                //RootIDの読み込み
                ans.RootID = Convert.ToInt32(xml.Attribute("root_id").Value);

                //読み込み
                ans.TextureList = this.ReadData(xml);

            }
            catch (Exception ex)
            {
                throw new Exception("ReadTextureList", ex);
            }
            return ans;
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="rxml"></param>
        /// <returns></returns>
        private List<TextureListFileData> ReadData(XElement rxml)
        {
            List<TextureListFileData> anslist = new List<TextureListFileData>();

            //Blockの読み込み
            var tbem = rxml.Elements("TextureBlock");
            foreach (XElement tb in tbem)
            {
                var a = this.ReadTextureBlock(tb);
                anslist.AddRange(a);
            }

            return anslist;
        }

        /// <summary>
        /// テクスチャブロックの読み込み
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private List<TextureListFileData> ReadTextureBlock(XElement xml)
        {
            List<TextureListFileData> anslist = new List<TextureListFileData>();

            //読み込みパス
            string path = xml.Attribute("path").Value;

            //データの取得
            var tem = xml.Elements("Texture");
            foreach (XElement tex in tem)
            {
                TextureListFileData ans = new TextureListFileData();

                //分割サイズ
                string divx = tex.Attribute("divx")?.Value ?? "1";
                string divy = tex.Attribute("divy")?.Value ?? "1";
                ans.IndexSize = new Size(Convert.ToInt32(divx), Convert.ToInt32(divy));

                //透過色
                ans.TransColor = -1;
                string? tcol = tex.Attribute("tcol")?.Value;
                if (tcol != null)
                {
                    ans.TransColor = Convert.ToInt32(tcol, 16);
                }

                //パス
                ans.FilePath = path + "\\" + tex.Value;

                anslist.Add(ans);
            }

            return anslist;
        }

    }
}
