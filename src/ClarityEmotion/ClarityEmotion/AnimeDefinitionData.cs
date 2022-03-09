using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;

using System.Drawing;
using System.Xml.Serialization;

namespace ClarityEmotion
{
    /// <summary>
    /// 画像とパスの組
    /// </summary>
    [Serializable]
    public class ImagePathSet
    {
        public ImagePathSet()
        {

        }
        public ImagePathSet(string path)
        {
            this.FilePath = path;
            this.BitImage = new Bitmap(path);
        }


        [XmlIgnore]
        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(this.FilePath);
            }
        }

        public string FilePath { get; set; }

        [XmlIgnore]
        public Bitmap BitImage { get; protected set; } = null;

        /// <summary>
        /// 画像の再読みこみ
        /// </summary>
        public void ReloadImage()
        {
            this.BitImage = new Bitmap(this.FilePath);
        }
    }

    /// <summary>
    /// アニメ定義データ
    /// </summary>
    [Serializable]
    public class AnimeDefinitionData : IDisposable
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// これのデータ名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// データ一式
        /// </summary>
        public List<ImagePathSet> ImageDataList { get; set; } = new List<ImagePathSet>();


        /// <summary>
        /// 解放
        /// </summary>
        public void Dispose()
        {
            this.ImageDataList.ForEach(x => x.BitImage.Dispose());
            this.ImageDataList.Clear();
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    

}
