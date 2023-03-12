using Clarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClarityMovement.MotionFile
{
    /// <summary>
    /// テクスチャアニメモーション管理ファイル
    /// </summary>
    public class ClarityMotionFile
    {
        public ClarityMotionFile() 
        { 
        }


        /// <summary>
        /// ファイルの書き込み
        /// </summary>
        /// <param name="filepath">書き込みファイルパス</param>
        /// <param name="mdata">データ</param>
        public void Write(string filepath, ClarityMotion mdata)
        {
            using (FileStream fp = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                this.Write(fp, mdata);
            }
        }

        /// <summary>
        /// ファイルの書き込み
        /// </summary>
        /// <param name="st"></param>
        /// <
        /// <exception cref="Exception"></exception>
        public void Write(Stream st, ClarityMotion mdata)
        {
            try
            {
                XElement root = new XElement("Motion");

                root.Add(new XAttribute("id", mdata.MotionID));
                root.Add(new XAttribute("code", mdata.MotionCode));
                root.Add(new XAttribute("maxframe", mdata.MaxFrame));

                //フレームの書き出し
                mdata.FrameList.ForEach(x =>
                {
                    var data = this.WriteFrameElement(x);
                    root.Add(data);
                });


                root.Save(st);
            }
            catch (Exception ex)
            {
                throw new Exception("ClarityMotionFile Write", ex);
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 書き出し用にFrameデータのXElementを作成する。
        /// </summary>
        /// <param name="fdata">書き出しデータ</param>
        /// <returns>作成element</returns>
        private XElement WriteFrameElement(ClarityMotionFrame fdata)
        {
            XElement ans = new XElement("Frame");
            ans.Add(new XAttribute("no", fdata.FrameNo));

            //画像情報の追記
            {
                XElement itag = new XElement("Image");
                itag.Value = fdata.TexInfo.ToString();

                ans.Add(itag);
            }

            //tagの追記
            ClaritySettingCoreWriter ctw = new ClaritySettingCoreWriter();
            fdata.TagList.ForEach(x =>
            {   
                XElement tag = ctw.CreateElement(x);
                tag.Add(new XAttribute("no", x.Id));

                ans.Add(tag);
            });


            return ans;
            
        }
    }
}
