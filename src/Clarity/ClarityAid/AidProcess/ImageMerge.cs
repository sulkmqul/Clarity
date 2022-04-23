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
            /// 合成画像のパス一覧
            /// </summary>
            public List<string> PathList = new List<string>();

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
            Image ans = this.CreateMergeImage(minfo);

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

            //有効なパスの確定             
            int aim = param.GetParameterInt("-m") ?? -1;
            List<string> proclist = this.CreateAviableProcPath(pathlist, aim);
            
            //初期値は横にずらっと並べる。
            int x = proclist.Count;
            int y = 0;

            //Limitの計算
            int tx = 0;
            foreach(string data in proclist)
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
            if (y <= 0)
            {
                y = 1;
            }

            //最終値を反映
            ans.ColCount = x;
            ans.RawCount = y;

            //処理一覧を保存
            ans.PathList = proclist;

            return ans;
        }


        /// <summary>
        /// 連結画像の作成
        /// </summary>
        /// <param name="minfo">合成情報</param>
        /// <param name="pathlist">パス一式</param>
        /// <returns></returns>
        private Image CreateMergeImage(MergeInfo minfo)
        {
            //合成サイズの作成
            Bitmap ans = new Bitmap(minfo.MergeSize.Width, minfo.MergeSize.Height, PixelFormat.Format32bppArgb);

            //処理パス取得
            var pathlist = minfo.PathList;

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


        /// <summary>
        /// 有効なパスの確定
        /// </summary>
        /// <param name="srclist">元ネタ一式</param>
        /// <param name="aim">有効枚数</param>
        /// <returns>有効パス一式</returns>
        private List<string> CreateAviableProcPath(List<string> srclist, int aim)
        {
            //基底枚数以下だった もしくは分解能以上だった
            if (aim < 2 || aim >= srclist.Count)
            {
                return srclist;
            }

            string[] retpath = new string[aim];
            //初めと終わりは確定
            retpath[0] = srclist[0];
            retpath[aim - 1] = srclist.Last();

            int restcount = aim - 2;

            //等間隔の間を取得
            double m = (srclist.Count - 2.0) / (double)(restcount + 1);
            double fpos = m;    //誤差収束のため浮動小数計算
            
            //順番に取得していく
            for (int i = 0; i < restcount; i++)
            {
                int pos = Convert.ToInt32(fpos);
                retpath[i + 1] = srclist[pos];
                fpos += m;
            }

            return new List<string>(retpath);

        }
    }
}
