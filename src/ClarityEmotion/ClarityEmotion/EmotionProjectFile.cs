using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity.Element;
using Clarity;
using System.Xml.Serialization;
using System.IO;


namespace ClarityEmotion
{
    [Serializable]
    public class EmotionProjectFileData
    {
        /// <summary>
        /// 基本情報
        /// </summary>
        public EmotionProjectDataBasic BasicInfo = new EmotionProjectDataBasic();

        /// <summary>
        /// アニメ定義情報
        /// </summary>
        public List<AnimeDefinitionData> AnimeDefinitionList = new List<AnimeDefinitionData>();

        /// <summary>
        /// アニメLayer情報
        /// </summary>
        public List<EmotionAnimeData> AnimeList = new List<EmotionAnimeData>();

    }

    class EmotionProjectFile
    {
        /// <summary>
        /// プロジェクトファイルの書き込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="pdata"></param>
        public void WriteProject(string filepath, EmotionProjectData pdata)
        {
            //projectデータの変換
            EmotionProjectFileData fpdata = this.ConvertFileData(pdata);

            //書き込み
            this.WriteProjectFile(filepath, fpdata);
        }


        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public EmotionProjectData ReadProject(string filepath)
        {
            //ファイル読み込み
            EmotionProjectFileData fdata = this.ReadProjectFile(filepath);

            //変換
            EmotionProjectData ans = this.ConvertFileData(fdata);
            return ans;
        }




        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//


        /// <summary>
        /// データの変換
        /// </summary>
        /// <param name="pdata"></param>
        /// <returns></returns>
        private EmotionProjectFileData ConvertFileData(EmotionProjectData pdata)
        {
            EmotionProjectFileData ans = new EmotionProjectFileData();

            ans.BasicInfo = pdata.BasicInfo;
            ans.AnimeDefinitionList = pdata.Anime.AnimeDefinitionDic.Values.ToList();

            ans.AnimeList = pdata.Anime.LayerList.Select(x => x.EaData).ToList();

            return ans;
        }


        /// <summary>
        /// データの変換
        /// </summary>
        /// <param name="fdata"></param>
        /// <returns></returns>
        private EmotionProjectData ConvertFileData(EmotionProjectFileData fdata)
        {
            EmotionProjectData ans = new EmotionProjectData();
            ans.BasicInfo = fdata.BasicInfo;
            ans.Anime = new EmotionProjectDataAnime();
            ans.Anime.CreateAnimeDefinitionDic(fdata.AnimeDefinitionList);
            ans.Anime.CreateLayerWithSrc(fdata.AnimeList);
            

            return ans;
        }


        /// <summary>
        /// データ保存
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="fpdata"></param>
        private void WriteProjectFile(string filepath, EmotionProjectFileData fpdata)
        {
            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(EmotionProjectFileData));
                using (FileStream fp = new FileStream(filepath, FileMode.Create))
                {
                    xml.Serialize(fp, fpdata);
                }
            }
            catch (Exception e)
            {
                throw new Exception("EmotionProjectFile WriteProjectFile Exception", e);
            }
        }

        /// <summary>
        /// データ読み込み
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private EmotionProjectFileData ReadProjectFile(string filepath)
        {
            EmotionProjectFileData ans = null;

            try
            {
                XmlSerializer xml = new XmlSerializer(typeof(EmotionProjectFileData));
                using (FileStream fp = new FileStream(filepath, FileMode.Open))
                {
                    ans = (EmotionProjectFileData)xml.Deserialize(fp);
                }
            }
            catch (Exception e)
            {
                throw new Exception("EmotionProjectFile ReadProjectFile", e);
            }

            return ans;
        }

    }
}
