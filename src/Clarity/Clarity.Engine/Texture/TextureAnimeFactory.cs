using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine.Texture
{
    /// <summary>
    /// テクスチャアニメを一括管理するもの
    /// </summary>
    /// <remarks>
    /// オブジェクト生成の度にテクスチャアニメファイルを読みに行くわけにはいかないため、
    /// 必要なものはこのクラスに保持、提供を行う。
    /// </remarks>
    internal class TextureAnimeFactory : BaseClarityFactroy<TextureAnimeFactory, TextureAnimeData>
    {
        private TextureAnimeFactory()
        {
        }
        
        /// <summary>
        /// 作成
        /// </summary>
        public static void Create()
        {
            Instance = new TextureAnimeFactory();
        }

        /// <summary>
        /// 読み込みデータ
        /// </summary>
        private class ReadTempData
        {
            public string AnimeCode
            {
                get
                {
                    return this.FileData.AnimeCode;
                }
            }

            /// <summary>
            /// 割り当てられたアニメID
            /// </summary>
            public int AnimeID = 0;


            /// <summary>
            /// 内部用データ
            /// </summary>
            public TextureAnimeData AnimeData = null;
            /// <summary>
            /// ファイルデータ
            /// </summary>
            public TextureAnimeFileDataElement FileData = null;
        }

        
        //************************************************************************************************

        /// <summary>
        /// フレーム情報を内部形式に変換する
        /// </summary>
        /// <param name="fdata"></param>
        /// <returns></returns>
        private TextureAnimeFrameInfo ConvertFrameInfo(TextureAnimeFileDataFrame fdata)
        {
            #region フレームのテクスチャ取得
            //コードから対象テクスチャの取得
            int tid = TextureManager.Mana.SearchTextureID(fdata.TextureCode);
            if (tid == int.MinValue)
            {
                throw new Exception("ConvertFrameInfo InValid TextureCode:" + fdata.TextureCode);
            }
            
            #endregion

            //フレーム情報の変換
            TextureAnimeFrameInfo ans = new TextureAnimeFrameInfo();
            ans.TextureID = tid;

            //インデックスオフセットの設定            
            ans.TextureOffset = ClarityEngine.GetTextureOffset(tid, fdata.IndexX, fdata.IndexY);

            ans.FrameTime = fdata.FrameTime;


            return ans;
        }

        /// <summary>
        /// アニメファイルデータを読み込みテンプレデータに変更する。
        /// </summary>
        /// <param name="rdata">読み込みファイルデータ</param>
        /// <returns></returns>
        private List<ReadTempData> ConvertTempData(TextureAnimeFileDataRoot rdata)
        {
            List<ReadTempData> anslist = new List<ReadTempData>();

            int aid = rdata.RootID;

            rdata.AnimeDataList.ForEach(x =>
            {
                TextureAnimeData adata = new TextureAnimeData();
                #region ファイル情報の変換
                adata.Code = x.AnimeCode;
                adata.Kind = x.Kind;
                //NextAnimeIndexはtemp作成段階では確定しない
                adata.FrameList = new List<TextureAnimeFrameInfo>();
                //フレーム情報の読み込み
                x.FrameData.ForEach(fs =>
                {
                    //
                    TextureAnimeFileDataFrame fdata = TextureAnimeFile.ReadFrameInfo(fs);

                    //TextureAnimationFrameInfoへ変換してAdd
                    TextureAnimeFrameInfo finfo = this.ConvertFrameInfo(fdata);
                    adata.FrameList.Add(finfo);

                });
                #endregion


                //template情報の作成
                ReadTempData ans = new ReadTempData();
                ans.AnimeData = adata;
                ans.FileData = x;

                //IDの割り当て
                ans.AnimeID = aid;
                aid++;

                anslist.Add(ans);

            });

            return anslist;
        }
        
        /// <summary>
        /// 次のアニメコードが有効化否かをチェックする true=有効なコード、検索するべき false=次は無し
        /// </summary>
        /// <param name="nextcode"></param>
        /// <returns></returns>

        private bool CheckNextAnimeLink(string nextcode)
        {
            if (nextcode.Length <= 0)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// NextAnimeをリンクさせる
        /// </summary>
        /// <param name="templist">リンクセルデータ（先のデータもすべて含まれていること）</param>
        private void LinkNextAnimation(List<ReadTempData> templist)
        {
            templist.ForEach(adata =>
            {
                //自身の次のコード取得
                string nextcode = adata.FileData.NextAnimeCode;
                bool nextf = this.CheckNextAnimeLink(nextcode);
                if (nextf == false)
                {
                    return;
                }


                //先のデータを検索
                var dist = from f in templist where f.FileData.AnimeCode == nextcode select f;
                
                //一つでない場合はエラー（0はlinkなし、以上はダブリ）
                int dcount = dist.Count();                
                if (dcount != 1)
                {
                    throw new Exception(string.Format("AnimeCodeLink Exception srcode={0}, next={1} count={2}", adata.AnimeCode, nextcode, dcount));
                }

                //next対象の取得
                ReadTempData dd = dist.First();
                adata.AnimeData.NextAnimeID = dd.AnimeID;

            });
        }


        /// <summary>
        /// ///読み込みデータを内部管理dicへ変換
        /// </summary>
        /// <param name="ralist">ファイルデータ一式</param>
        /// <returns></returns>
        private Dictionary<int, TextureAnimeData> CreateAnimeDic(List<TextureAnimeFileDataRoot> ralist)
        {
            Dictionary<int, TextureAnimeData> ansdic = new Dictionary<int, TextureAnimeData>();

            //読み込み
            List<ReadTempData> templist = new List<ReadTempData>();
            ralist.ForEach(ra =>
            {
                var t = this.ConvertTempData(ra);
                templist.AddRange(t);
            });


            //AnimeLinkの作成
            this.LinkNextAnimation(templist);

            //IDデータを元に辞書を作成
            templist.ForEach(adata =>
           {
               //同じkeyはさすがにマズイ。
               bool f = ansdic.ContainsKey(adata.AnimeID);
               if (f == true)
               {                   
                   throw new Exception(string.Format("AnimeID Already Exists id{0} code={1}", adata.AnimeID, adata.AnimeCode));
               }
               ansdic.Add(adata.AnimeID, adata.AnimeData);
           });

            
            

            return ansdic;
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 対象のテクスチャアニメファイルの読み込みを行う
        /// </summary>
        /// <param name="filepath">読み込みファイルパス一覧</param>
        public void ReadTextureAnimeFile(string filepath)
        {
            List<string> fpathlist = new List<string>() { filepath };
            this.ReadTextureAnimeFile(fpathlist);
        }


        /// <summary>
        /// テクスチャアニメファイル一式の読み込み
        /// </summary>
        /// <param name="fpathlist"></param>
        public void ReadTextureAnimeFile(List<string> fpathlist)
        {
            try
            {
                List<TextureAnimeFileDataRoot> ralist = new List<TextureAnimeFileDataRoot>();

                //読み込み
                fpathlist.ForEach(x =>
                {
                    TextureAnimeFileDataRoot rdata = TextureAnimeFile.ReadFile(x);
                    ralist.Add(rdata);
                });

                //管理データの作成
                this.ManaDic = this.CreateAnimeDic(ralist);

            }
            catch (Exception e)
            {
                throw new Exception("TextureAnimeFactory ReadTextureAnimeFile", e);
            }
        }


   

        /// <summary>
        /// アニメ情報の取得
        /// </summary>
        /// <param name="aid">取得AnimeID</param>
        /// <returns></returns>
        public static TextureAnimeData GetAnime(int aid)
        {
            TextureAnimeData ans = TextureAnimeFactory.Mana.ManaDic[aid];
            return ans;
        }


    }
}
