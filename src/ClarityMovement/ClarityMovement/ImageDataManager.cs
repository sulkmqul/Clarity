using Clarity.Engine;
using Clarity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    public class CmImageData
    {
        /// <summary>
        /// これのID(管理テクスチャID)
        /// </summary>
        public int CmImageID { get; init; } = 0;

        /// <summary>
        /// 該当ファイルパス
        /// </summary>
        public string FilePath { get; set; } = "";

        /// <summary>
        /// これの名前
        /// </summary>
        public string ImageDataName { get; set; } = "";

        /// <summary>
        /// 該当画像
        /// </summary>
        public Bitmap Image { get; set; }
    }

    /// <summary>
    /// 画像データを管理する
    /// </summary>
    internal class ImageDataManager
    {
        public ImageDataManager()
        {
        }

        /// <summary>
        /// IDのシーケンス
        /// </summary>
        ClaritySequence CmIdSeq = new ClaritySequence();


        /// <summary>
        /// 管理dic [id, data]
        /// </summary>
        private Dictionary<int, CmImageData> ImageDic { get; set; } = new Dictionary<int, CmImageData>();


        /// <summary>
        /// 管理へ追加
        /// </summary>
        /// <param name="filepath">画像ファイルパス</param>
        public void AddImage(string filepath)
        {
            this.AddImage(new List<string> { filepath });
        }
        /// <summary>
        /// 管理へ追加
        /// </summary>
        /// <param name="filelist">画像ファイルパス一式</param>
        public void AddImage(List<string> filelist)
        {
            filelist.ForEach(x =>
            {
                Bitmap bit = new Bitmap(x);
                string name = Path.GetFileNameWithoutExtension(x);
                this.AddImage(name, x, bit);
            });
        }

        /// <summary>
        /// データの追加
        /// </summary>
        /// <param name="fname">ファイル名</param>
        /// <param name="filepath">ファイルパス</param>
        /// <param name="bit">画像データ</param>
        public void AddImage(string fname, string filepath, Bitmap bit)
        {
            //管理データの作成
            CmImageData idata = new CmImageData() { CmImageID = this.CmIdSeq.NextValue };            
            idata.ImageDataName = fname;
            idata.FilePath = filepath;
            idata.Image = bit;

            //テクスチャの読み込み
            ClarityEngine.Texture.LoadTexture(idata.CmImageID, bit);


        }


        /// <summary>
        /// 管理削除
        /// </summary>
        /// <param name="data"></param>
        public void RemoveImage(CmImageData data)
        {
            this.RemoveImage(data.CmImageID);
        }

        /// <summary>
        /// 管理削除
        /// </summary>
        /// <param name="cm_image_id"></param>
        public void RemoveImage(int cm_image_id)
        {
            //管理から削除し、clarity engineへのtexture addを解除する。
            ClarityEngine.Texture.RemoveTexture(cm_image_id);
            this.ImageDic.Remove(cm_image_id);
            
        }


        /// <summary>
        /// データの取得
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CmImageData GetImage(int id)
        {
            return this.ImageDic[id];
        }

        /// <summary>
        /// データ一覧に取得
        /// </summary>        
        /// <returns></returns>
        public List<CmImageData> GetImageList()
        {
            return this.ImageDic.Values.ToList();
        }
    }
}
