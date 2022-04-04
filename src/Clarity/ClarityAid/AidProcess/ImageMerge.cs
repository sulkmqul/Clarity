using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ClarityAid.AidProcess
{
    /// <summary>
    /// 画像の結合
    /// </summary>
    internal class ImageMerge : IAidProcess
    {
        public string ClassName => "";

        private class MergeInfo
        {
            /// <summary>
            /// 横の画像数
            /// </summary>
            public int ColCount = 0;

            /// <summary>
            /// 縦の画像数
            /// </summary>
            public int RawCount = 0;
                        
            /// <summary>
            /// 結合前の一つの画像サイズ(pixel)
            /// </summary>
            public Size ImageSize;

            /// <summary>
            /// 結合後の画像サイズ(pixel)
            /// </summary>
            public Size MergeSize
            {
                get
                {
                    Size size = new Size(this.ImageSize.Width * this.ColCount, this.ImageSize.Height * this.RawCount);
                    return size;
                }
            }
        }


        /// <summary>
        /// 処理main
        /// </summary>
        /// <param name="param"></param>
        public void Proc(ArgParam param)
        {
            //処理画像一覧の取得
            List<string> pathlist = this.CreateSrcPathList(param);

            //合成情報の作成
            MergeInfo minfo = this.CreateMergeInfo(param, pathlist);

            //合成画像の作成
            Image ans = this.CreateMergeImage(minfo, pathlist);

            //保存
            string opath = param.CreateOutputFile("merge.png");
            ans.Save(opath);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 合成対象のパス一覧を作成する
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private List<string> CreateSrcPathList(ArgParam param)
        {
            //入力フォルダの取得
            List<string> inlist  = param.GetInputList();
            //検索拡張子の取得
            List<string> klist = param.GetParameter("-k");

            //全データのリストアップ
            List<string> anslist = Clarity.Util.ClarityUtil.ListupSpecifiedFiles(inlist, klist);
            return anslist;
        }

        /// <summary>
        /// 合成情報の作成
        /// </summary>
        /// <param name="param">処理パラメータ</param>
        /// <param name="pathlist">処理対象一式</param>
        /// <returns></returns>
        private MergeInfo CreateMergeInfo(ArgParam param, List<string> pathlist)
        {
            MergeInfo ans = new MergeInfo();
            //最大サイズの取得
            int maxcol = param.GetParameterInt("-x") ?? int.MaxValue;
            int maxraw = param.GetParameterInt("-y") ?? int.MaxValue;

            //基準画像のサイズを取得
            using (Bitmap bit = new Bitmap(pathlist[0]))
            {
                ans.ImageSize = new Size(bit.Size.Width, bit.Size.Height);
            }

            //初期値は横にずらっと並べる。
            int x = pathlist.Count;
            int y = 1;

            //Limitの計算
            int tx = 0;
            foreach(string data in pathlist)
            {
                tx += 1;
                if (tx >= maxcol)
                {                    
                    y += 1;
                    x = maxcol;
                    tx = 0;
                }

                //Yの値が超えたら終了とする
                if (y >= maxraw)
                {
                    y = maxraw;
                }
            }

            //最終値を反映
            ans.ColCount = x;
            ans.RawCount = y;

            return ans;
        }


        /// <summary>
        /// 連結画像の作成
        /// </summary>
        /// <param name="minfo">合成情報</param>
        /// <param name="pathlist">パス一式</param>
        /// <returns></returns>
        private Image CreateMergeImage(MergeInfo minfo, List<string> pathlist)
        {
            //合成サイズの作成
            Bitmap ans = new Bitmap(minfo.MergeSize.Width, minfo.MergeSize.Height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, minfo.ImageSize.Width, minfo.ImageSize.Height);
            using (Graphics gra = Graphics.FromImage(ans))
            {
                //全画像の連結
                int fpos = 0;
                for (int y = 0; y < minfo.RawCount; y++)
                {
                    for (int x = 0; x < minfo.ColCount; x++)
                    {
                        //サイズオーバー(maxで揃わない時は超えることはあり得る)
                        if (pathlist.Count <= fpos)
                        {
                            break;
                        }

                        rect.X = x * minfo.ImageSize.Width;
                        rect.Y = y * minfo.ImageSize.Height;

                        using (Bitmap bit = new Bitmap(pathlist[fpos]))
                        {
                            gra.DrawImage(bit, rect, 0, 0, bit.Width, bit.Height, GraphicsUnit.Pixel);
                        }

                        fpos++;
                    }
                }
            }

            return ans;
        }
    }
}
