using Clarity.GUI;
using ClarityEmotion.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace ClarityEmotion
{
    /// <summary>
    /// 画像出力者
    /// </summary>
    internal class EmotionWriter
    {
     
        /// <summary>
        /// 連番画像の出力
        /// </summary>
        /// <param name="folpath">出力フォルダパス</param>
        /// <param name="fm">画像フォーマット</param>
        /// <param name="prefix">連番画像の接頭語</param>
        /// <param name="acprogress">進捗返却(全体,現在値)</param>
        /// <returns></returns>
        public async Task ExportImages(string folpath, System.Drawing.Imaging.ImageFormat fm, string prefix = "", Action<int, int>? acprogress = null)
        {
            //全フレーム数
            int end = CeGlobal.Project.BasicInfo.MaxFrame;
            int iw = CeGlobal.Project.BasicInfo.ImageWidth;
            int ih = CeGlobal.Project.BasicInfo.ImageHeight;

            //変換の作成
            ImageViewerTranslator ivt = new ImageViewerTranslator(new SizeF(iw, ih), new SizeF(iw, ih), new SizeF(iw, ih));
            
            //出力の初期化
            EmotionCore core = new EmotionCore();
            core.Init(CeGlobal.Project.Anime.LayerList);

            //全フレームを出力する
            for (int i = 0; i < end; i++)
            {
                //フレーム画像作成
                Bitmap bit = await this.CreateFrameImage(i, core, ivt);

                //保存
                string fpath = this.CreateSaveFilePath(folpath, fm, prefix, i);
                await Task.Run(() => bit.Save(fpath, fm));

                acprogress?.Invoke(end, i);
            }
        }




        public async Task ExportMJpeg(string filepath, Action<int, int>? acprogress = null)
        {
            //全フレーム数
            int end = CeGlobal.Project.BasicInfo.MaxFrame;
            int iw = CeGlobal.Project.BasicInfo.ImageWidth;
            int ih = CeGlobal.Project.BasicInfo.ImageHeight;

            //変換の作成
            ImageViewerTranslator ivt = new ImageViewerTranslator(new SizeF(iw, ih), new SizeF(iw, ih), new SizeF(iw, ih));

            //出力の初期化
            EmotionCore core = new EmotionCore();
            core.Init(CeGlobal.Project.Anime.LayerList);

            using (FileStream fp = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {   
                //全フレームを出力する
                for (int i = 0; i < end; i++)
                {
                    //フレーム画像作成
                    Bitmap bit = await this.CreateFrameImage(i, core, ivt);

                    using (MemoryStream mst = new MemoryStream())
                    {
                        bit.Save(mst, System.Drawing.Imaging.ImageFormat.Jpeg);
                        await fp.WriteAsync(mst.ToArray());
                    }
                    acprogress?.Invoke(end, i);
                }

                
            }
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 画像イメージの作成
        /// </summary>
        /// <param name="frame">作成フレーム数</param>
        /// <param name="core">コア処理</param>
        /// <param name="ivt">画像変換</param>
        /// <returns>作成画像</returns>
        private async Task<Bitmap> CreateFrameImage(int frame, EmotionCore core, ImageViewerTranslator ivt)
        {
            return await Task.Run(() =>
            {
                //bitmapの作成
                Bitmap ans = new Bitmap((int)ivt.SrcSize.Width, (int)ivt.SrcSize.Height);
                
                using (Graphics gra = Graphics.FromImage(ans))
                {
                    gra.Clear(Color.Transparent);
                    core.GenerateEmotion(frame, gra, ivt);
                }
                return ans;
            });

        }

        /// <summary>
        /// 出力ファイル名の作成
        /// </summary>
        /// <param name="fpath"></param>
        /// <param name="fm"></param>
        /// <param name="prefix"></param>
        /// <param name="frame"></param>
        /// <returns></returns>
        private string CreateSaveFilePath(string fpath, System.Drawing.Imaging.ImageFormat fm, string prefix, int frame)
        {
            return $@"{fpath}\{prefix}{frame,00000000}.{fm.ToString().ToLower()}";
        }

                
    }
}
