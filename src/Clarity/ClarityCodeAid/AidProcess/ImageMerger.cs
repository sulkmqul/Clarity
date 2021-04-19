using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

using Clarity;

namespace ClarityCodeAid.AidProcess
{
    /// <summary>
    /// 画像の結合
    /// </summary>
    public class ImageMerger : IAidProcess
    {
        private class MergeInfo
        {
            /// <summary>
            /// 結合前の一つの画像サイズ
            /// </summary>
            public Size ImageSize;

            /// <summary>
            /// 結合後の画像サイズ
            /// </summary>
            public Size MergeSize;
        }


        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string CreateWriteFilePath(InputParam param)
        {
            string datestr = DateTime.Now.ToString("yyyyMMdd_hhmmssfff");
            string fname = string.Format("merge_{0}.png", datestr);
            string ans = param.OutputDirectory + System.IO.Path.AltDirectorySeparatorChar + fname;
            return ans;
        }


        /// <summary>
        /// 画像結合処理
        /// </summary>
        /// <param name="param"></param>
        public void Proc(InputParam param)
        {
            //処理画像の一覧を取得
            List<string> inputlist = param.CreateInputFileList();
            ClarityLog.WriteInfo("Image Merge Start Input File Count", inputlist.Count);

            if (inputlist.Count < 2)
            {
                ClarityLog.WriteError("結合対象が不足しています");
                return;
            }

            //連結処理
            Image ans = this.CreateMergeImage(inputlist);

            //保存処理
            string outfilepath = this.CreateWriteFilePath(param);
            ans.Save(outfilepath);


            ClarityLog.WriteInfo("Image Merge Complete!!");
        }




        /// <summary>
        /// 結合情報の作成
        /// </summary>
        /// <param name="inputlist">処理対象ファイルパス一覧</param>
        /// <returns></returns>
        private MergeInfo CreateMeregeInfo(List<string> inputlist)
        {
            MergeInfo ans = new MergeInfo();

            using (Bitmap data = new Bitmap(inputlist[0]))
            {
                //一つの画像
                ans.ImageSize = new Size(data.Width, data.Height);

                //結合後サイズ1
                ans.MergeSize = new Size(data.Width * inputlist.Count, data.Height);
            }

            return ans;


        }


        /// <summary>
        /// 連結画像の作成本体
        /// </summary>
        /// <param name="inputlist">入力ファイル一覧</param>
        /// <returns></returns>
        private Image CreateMergeImage(List<string> inputlist)
        {
            MergeInfo minfo = this.CreateMeregeInfo(inputlist);

            Bitmap ans = new Bitmap(minfo.MergeSize.Width, minfo.MergeSize.Height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, minfo.ImageSize.Width, minfo.ImageSize.Height);
            using (Graphics gra = Graphics.FromImage(ans))
            {
                //全画像の連結
                foreach (string filepath in inputlist)
                {
                    using (Bitmap data = new Bitmap(filepath))
                    {
                        gra.DrawImage(data, rect, 0, 0, data.Width, data.Height, GraphicsUnit.Pixel);
                    }

                    //次へ
                    rect.X = rect.Right;
                }
            }
            return ans;
        }
    }
}
