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

        class TextureSrcData
        {
            public string TexCode = "";
            public List<(CmImageData sdata, Point index)> SrcList = new List<(CmImageData sdata, Point index)>();
        }

        /// <summary>
        /// 出力テクスチャ分割数X
        /// </summary>
        public int MaxTextureDivX { get; set; } = 8;
        /// <summary>
        /// 出力テクスチャ分割数Y
        /// </summary>
        public int MaxTextureDivY { get; set; } = 8;

        /// <summary>
        /// 元ネタ
        /// </summary>
        private CmProject Project { get; set; }

        /// <summary>
        /// 書き出しコード
        /// </summary>
        private string MotionCode = "";


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 出力処理
        /// </summary>
        /// <param name="filepath">出力ファイルパス</param>
        /// <param name="mcode">モーションコード</param>
        public async Task Export(string filepath, string mcode)
        {
            this.MotionCode = mcode;

            //作業フォルダパスの作成
            string wfolpath = this.CreateWorkFolderPath(filepath);
            using (WorkingFolderState st = new WorkingFolderState(wfolpath, false))
            {
                //使用画像の取得と画像連結とtexturefileの作成
                await this.ExportTextureImage(wfolpath);

                //motion情報の書き出し
            }

            
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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
        /// <param name="wfolpath">作業フォルダパス</param>
        /// <returns>書き出しtexture情報</returns>
        private async Task<List<TextureSrcData>> ExportTextureImage(string wfolpath)
        {
            //使用画像一覧の取得
            List<CmImageData> avlist = this.CreateAvailableIamgeList();

            //使用画像を連結画像に対して割り当てる
            List<TextureSrcData> srclist = this.CalculateTexture(avlist);

            //連結と書き出し
            await this.MergeTexture(srclist);

            //テクスチャファイルの書き出し


            
            return srclist;

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

        /// <summary>
        /// テクスチャ一枚ごとのデータを作成する
        /// </summary>
        /// <param name="avlist">使用している画像一式</param>
        /// <returns>連結画像データ</returns>
        private List<TextureSrcData> CalculateTexture(List<CmImageData> avlist)
        {
            List<TextureSrcData> anslist = new List<TextureSrcData>();

            int index = 0;

            //出力テクスチャ数の割り出し
            int maxdiv = this.MaxTextureDivX * this.MaxTextureDivY;
            int texcount = (avlist.Count / maxdiv) + 1;

            for(int tc=0; tc<texcount; tc++)
            {
                TextureSrcData ans = new TextureSrcData();
                ans.TexCode = $"{this.MotionCode}_{tc}";


                for (int y=0; y<this.MaxTextureDivY; y++)
                {
                    for(int x=0; x<this.MaxTextureDivX; x++)
                    {
                        //連結の割り当て
                        ans.SrcList.Add((avlist[index], new Point(x, y)));
                        index++;

                        //有効なものを割り当て終了でおわり
                        if (index >= avlist.Count())
                        {
                            break;
                        }
                    }
                    if (index >= avlist.Count())
                    {
                        break;
                    }
                }

                //追加
                anslist.Add(ans);
            }




            return anslist;
        }

        
        /// <summary>
        /// 連結画像を出力する
        /// </summary>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private async Task MergeTexture(List<TextureSrcData> datalist)
        {

        }


    }
}
