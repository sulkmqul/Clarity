using ClarityMovement.MotionFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Export
{
    /// <summary>
    /// 表示形式とファイル形式の橋渡しを行うもの
    /// </summary>
    internal class MotionConverter
    {

        /// <summary>
        /// 保持形式からファイル形式へ変換
        /// </summary>
        /// <param name="proj">元ネタ</param>
        /// <returns></returns>
        public ClarityMotion ConvertFileData(CmProject proj)
        {
            ClarityMotion ans = new ClarityMotion();

            for (int i = 0; i < proj.MaxFrame; i++)
            {
                //フレーム情報の作成
                ClarityMotionFrame fdata = new ClarityMotionFrame(i);


                {
                    //このフレームの画像を取得
                    var imgsrc = proj.ModifierList.Where(x => x.TagType == ETagType.Image && x.CheckFrame(i) == true);
                    if (imgsrc.Count() <= 0)
                    {
                        throw new Exception($"画像がないフレームがあります:{i}");
                    }
                    if (imgsrc.Count() != 1)
                    {
                        throw new Exception($"二枚以上の画像が設定されているフレームがあります:{i}");
                    }
                    FrameImageModifier itag = (FrameImageModifier)imgsrc.First();
                    CmImageData img = proj.ImageDataMana.GetImage(itag.ImageDataID);
                    //画像コードの設定                    
                    fdata.TexInfo = new ClarityMotionFrameImage();
                    fdata.TexInfo.TextureCode = img.ImageDataName;
                }
                {
                    //タグ情報の追加
                    var taglist = proj.ModifierList.Where(x => x.TagType == ETagType.Tag && x.CheckFrame(i) == true).Select(x => (FrameTagModifier)x).ToList();
                    foreach (var tag in taglist)
                    {
                        fdata.TagList.Add(tag.Data);
                    }


                }
                ans.FrameList.Add(fdata);

            }

            return ans;

        }
    }
}
