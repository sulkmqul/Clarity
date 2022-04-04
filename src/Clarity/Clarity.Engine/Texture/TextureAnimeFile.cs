using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Clarity.Engine.Texture
{   
    /// <summary>
    /// テクスチャアニメファイルルートデータ
    /// </summary>
    [XmlRoot("ClarityTextureAnime")]
    public class TextureAnimeFileDataRoot
    {
        /// <summary>
        /// 全体のアニメコード
        /// </summary>
        [XmlAttribute("root_id")]
        public int RootID = 0;

        [XmlElement("TextureAnimeData")]
        public List<TextureAnimeFileDataElement> AnimeDataList = new List<TextureAnimeFileDataElement>();
    }

    /// <summary>
    /// テクスチャアニメファイル、アニメ定義本体
    /// </summary>
    public class TextureAnimeFileDataElement
    {
        /// <summary>
        /// これのアニメーションコード
        /// </summary>
        [XmlAttribute("code")]
        public string AnimeCode = "Amida";

        /// <summary>
        /// アニメの取り扱い
        /// </summary>
        [XmlAttribute("loop")]
        public ETextureAnimationKind Kind = ETextureAnimationKind.Loop;

        /// <summary>
        /// アニメフレーム情報(TextureCode,IndexX,IndexY,表示時間ms)
        /// </summary>
        [XmlElement("Frame")]
        public List<string> FrameData = new List<string>();
    }

    /// <summary>
    /// ファイルデータフレーム情報
    /// </summary>
    public class TextureAnimeFileDataFrame
    {
        /// <summary>
        /// テクスチャコード
        /// </summary>
        public string TextureCode = "";

        /// <summary>
        /// 表示indexX
        /// </summary>
        public int IndexX = 0;
        /// <summary>
        /// 表示indexY
        /// </summary>
        public int IndexY = 0;

        /// <summary>
        /// 表示時間ms
        /// </summary>
        public long FrameTime;

        /// <summary>
        /// CSV文字列の作成
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string ans = "";
            ans = string.Format("{0},{1},{2},{3}", this.TextureCode, this.IndexX, this.IndexY, this.FrameTime);
            return ans;
        }

    }

    /// <summary>
    /// テクスチャアニメーション管理ファイル
    /// </summary>
    public class TextureAnimeFile
    {
        /// <summary>
        /// 設定の読み込み
        /// </summary>
        /// <param name="filepath">設定ファイルパス</param>
        public static TextureAnimeFileDataRoot ReadFile(string filepath)
        {
            TextureAnimeFileDataRoot ans = null;

            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(TextureAnimeFileDataRoot));
                using (FileStream fp = new FileStream(filepath, FileMode.Open))
                {
                    ans = (TextureAnimeFileDataRoot)xml.Deserialize(fp);
                }

            }
            catch (Exception e)
            {
                throw new Exception("Texture Anime File Read Exception", e);
            }

            return ans;
        }


        /// <summary>
        /// 設定の書き込み
        /// </summary>
        /// <param name="filepath">設定ファイルパス</param>
        public static void WriteFile(string filepath, TextureAnimeFileDataRoot data)
        {
            try
            {
                
                XmlSerializer xml = new XmlSerializer(typeof(TextureAnimeFileDataRoot));
                using (FileStream fp = new FileStream(filepath, FileMode.Create))
                {
                    //名前空間出力抑制
                    XmlSerializerNamespaces xmlnammespace = new XmlSerializerNamespaces();
                    xmlnammespace.Add("", "");
                    xml.Serialize(fp, data, xmlnammespace);                    
                }
            }
            catch (Exception e)
            {
                throw new Exception("Texture Anime File Write Exception", e);
            }
        }


        /// <summary>
        /// フレーム情報文字列の変換
        /// </summary>
        /// <param name="fs">読み込みフレーム情報文字列</param>
        /// <returns></returns>
        public static TextureAnimeFileDataFrame ReadFrameInfo(string fs)
        {
            TextureAnimeFileDataFrame ans = new TextureAnimeFileDataFrame();

            string[] sdata = fs.Split(',');
            int pos = 0;

            //コード
            ans.TextureCode = sdata[pos];
            pos += 1;

            //IndexX
            ans.IndexX = Convert.ToInt32(sdata[pos]);
            pos++;

            //IndexY
            ans.IndexY = Convert.ToInt32(sdata[pos]);
            pos++;

            //表示時間
            ans.FrameTime = Convert.ToInt64(sdata[pos]);
            pos++;

            return ans;
        }


    }
}
