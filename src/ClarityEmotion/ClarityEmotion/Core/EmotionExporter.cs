using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ClarityEmotion.Core
{
    /// <summary>
    /// 出力用データ
    /// </summary>
    public class EmotionExporterData
    {
        /// <summary>
        /// 出力フォルダパス
        /// </summary>
        public string ExportFolderPath = "";

        public string Prefix = "em";
    }


    /// <summary>
    /// アニメーション出力処理
    /// </summary>
    public class EmotionExporter
    {
        /// <summary>
        /// 出力処理本体
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns></returns>
        public async Task ExportEmotion(EmotionExporterData sdata, ProgressBar pbar)
        {
            //出力処理者の作成と初期化
            EmotionGenerator gene = new EmotionGenerator(false);
            {
                Size osize = new Size(EmotionProject.Mana.BasicInfo.ImageWidth, EmotionProject.Mana.BasicInfo.ImageHeight);
                gene.SetImageInit(osize, osize);
            }

            //進捗表示初期化
            pbar.Maximum = EmotionProject.Mana.BasicInfo.MaxFrame;
            pbar.Value = 0;

            //全フレームの出力
            for (int i = 0; i < EmotionProject.Mana.BasicInfo.MaxFrame; i++)
            {
                //保存名の作成
                string filepath = this.CreateSaveImageFilePath(sdata, i);

                //出力画像作成
                Bitmap bit = await this.CreateFrameImage(gene, i);

                //保存
                await Task.Run(() => bit.Save(filepath, System.Drawing.Imaging.ImageFormat.Png));


                pbar.Value += 1;
            }
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 保存画像パスの作成
        /// </summary>
        /// <param name="sdata">設定</param>
        /// <param name="frame">フレーム番号</param>
        /// <returns>作成ファイルパス</returns>
        private string CreateSaveImageFilePath(EmotionExporterData sdata, int frame)
        {
            string ans = $@"{sdata.ExportFolderPath}\{sdata.Prefix}_{frame}.png";
            return ans;
        }


        /// <summary>
        /// 対象フレームの画像作成
        /// </summary>
        /// <param name="gene">フレーム画像作成者</param>
        /// <param name="frame">フレーム番号</param>
        /// <returns></returns>
        private Task<Bitmap> CreateFrameImage(EmotionGenerator gene, int frame)
        {
            return Task.Run(() => {
                Bitmap ans = new Bitmap(gene.ImageSize.Width, gene.ImageSize.Height);
                using (Graphics gra = Graphics.FromImage(ans))
                {
                    gra.Clear(Color.Transparent);
                    gene.Render(gra, EmotionProject.Mana.Anime.LayerList, frame);
                }
                return ans;
            });
        }


    }
}
