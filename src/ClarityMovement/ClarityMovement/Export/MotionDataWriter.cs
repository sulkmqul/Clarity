using Clarity;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement.Export
{
    /// <summary>
    /// データ一式の書き出し管理クラス
    /// </summary>
    internal class MotionDataWriter
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="project"></param>
        public MotionDataWriter(CmProject project)
        {
            this.Project = project;
        }

        /// <summary>
        /// 元ネタ
        /// </summary>
        private CmProject Project { get; set; }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 出力処理
        /// </summary>
        /// <param name="filepath">出力ファイルパス</param>
        /// <param name="mcode">モーションコード</param>
        public async Task Export(string filepath, string mcode)
        {

            //作業フォルダパスの作成
            string wfolpath = this.CreateWorkFolderPath(filepath);
            using (WorkingFolderState st = new WorkingFolderState(wfolpath, false))
            {
                //使用画像の取得と画像連結とtexturefileの作成
                await this.ExportTextureImage(wfolpath);

                //motion情報の書き出し
            }

            
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 作業フォルダパスを作成
        /// </summary>
        /// <param name="filepath>元ファイルパス</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string CreateWorkFolderPath(string filepath)
        {
            string path = Path.GetFullPath(filepath);
            path = Path.GetDirectoryName(path) ?? "";
            if(path.Length <= 0)
            {
                throw new Exception("Working directory can not create");
            }

            string ans = $"{path}\\_tmp";
            return ans;
        }

        /// <summary>
        /// motionテクスチャの書き出し
        /// </summary>
        /// <param name="wfolpath"></param>
        /// <returns></returns>
        private async Task ExportTextureImage(string wfolpath)
        {
            //使用画像一覧の取得
            List<CmImageData> ialist = this.CreateAvailableIamgeList();

            //連結と書き出し

            //テクスチャファイルの書き出し



            await Task.Delay(500);
        }

        /// <summary>
        /// 使用している画像を取得する
        /// </summary>
        /// <returns></returns>
        private List<CmImageData> CreateAvailableIamgeList()
        {
            var ilist = this.Project.ImageTagList;
            //使用している画像を一意で取得
            return ilist.Select(x => this.Project.ImageDataMana.GetImage(x.ImageDataID)).Distinct().ToList();

        }



    }
}
