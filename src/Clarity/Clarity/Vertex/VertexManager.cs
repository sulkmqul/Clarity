using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.Core;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.D3DCompiler;

namespace Clarity.Vertex
{
    /// <summary>
    /// 頂点情報管理
    /// </summary>
    internal class VertexManager : BaseClaritySingleton<VertexManager>, IDisposable
    {
        

        #region 型

        /// <summary>
        /// 頂点管理データ
        /// </summary>
        protected class VertexManageData : IDisposable
        {
            /// <summary>
            /// 頂点数
            /// </summary>
            public int Count = 0;

            /// <summary>
            /// 頂点バッファ
            /// </summary>
            public SharpDX.Direct3D11.Buffer VertexBuffer;

            /// <summary>
            /// indexバッファ
            /// </summary>
            public SharpDX.Direct3D11.Buffer IndexBuffer;

            public void Dispose()
            {
                this.VertexBuffer.Dispose();
                this.IndexBuffer.Dispose();
            }
        }
        #endregion

        #region メンバ変数
        /// <summary>
        /// データ管理
        /// </summary>
        protected Dictionary<int, VertexManageData> VertexDic = new Dictionary<int, VertexManageData>();

        #endregion


        /// <summary>
        /// 作成
        /// </summary>
        public static void Create()
        {
            Instance = new VertexManager();

            //デフォルトデータの読み込み
            Instance.LoadBuildInVertex();

        }

        //---------------------------------------------------------------------------------
        /// <summary>
        /// ビルドインのデフォルトデータの読み込み
        /// </summary>
        private void LoadBuildInVertex()
        {
            //読み込みデフォルトデータ一式(VertexCode, ファイル内容CSV文字列)
            (int code, string csvs)[] defdatavec = {
                (ClarityDataIndex.Vertex_Display, Properties.Resources.VD000),
            };

            foreach (var defdata in defdatavec)
            {
                //対象の読み込み
                PolygonObjectFile pof = new PolygonObjectFile();
                PolyData pol = pof.ReadCsvString(defdata.csvs);

                //ADD
                this.AddVertexDic(defdata.code, pol);
            }

        }



        /// <summary>
        /// ユーザー定義データのクリア
        /// </summary>
        private void ClearUserData()
        {
            int[] indexvec = this.VertexDic.Keys.ToArray();
            foreach (int index in indexvec)
            {
                if (index < CustomStartIndex)
                {
                    continue;
                }

                this.VertexDic[index].Dispose();
                this.VertexDic.Remove(index);
            }
            
            
        }

        /// <summary>
        /// 読み込んだデータの開放
        /// </summary>
        /// <param name="cf">デフォルトデータのクリア可否 true=デフォルトデータまでクリアする</param>
        protected void ClearVertexDic(bool cf = false)
        {
            //ユーザー定義だけクリア
            if (cf == false)
            {
                this.ClearUserData();
                return;
            }

            //完全クリア
            foreach (VertexManageData mdata in this.VertexDic.Values)
            {
                mdata.Dispose();
                
            }
            this.VertexDic.Clear();
        }


        /// <summary>
        /// VertexDicへADD
        /// </summary>
        /// <param name="vno">管理番号</param>
        /// <param name="pdata">対象データ</param>
        protected void AddVertexDic(int vno, PolyData pdata)
        {

            //頂点バッファの作製
            SharpDX.Direct3D11.Buffer vbuf = SharpDX.Direct3D11.Buffer.Create(DxManager.Mana.DxDevice, BindFlags.VertexBuffer, pdata.VertexList.ToArray());

            //Indexバッファの作成
            SharpDX.Direct3D11.Buffer ibuf = SharpDX.Direct3D11.Buffer.Create(DxManager.Mana.DxDevice, BindFlags.IndexBuffer, pdata.IndexList.ToArray());


            //管理データ作製
            VertexManageData mdata = new VertexManageData();
            {
                mdata.VertexBuffer = vbuf;
                mdata.IndexBuffer = ibuf;
                mdata.Count = pdata.IndexList.Count;

            }
            this.VertexDic.Add(vno, mdata);
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 使用する一覧ファイルの読み込み
        /// </summary>
        /// <param name="listfilepath"></param>
        public void CreateResource(string listfilepath)
        {
            List<string> flist = new List<string>() { listfilepath };
            this.CreateResource(flist);
        }
        /// <summary>
        /// データの読み込み
        /// </summary>
        /// <param name="filepath">読み込み頂点リストファイルパス一覧</param>
        public void CreateResource(List<string> fpathlist)
        {
            try
            {
                //解放
                this.ClearVertexDic();

                List<PolygonListFileDataRoot> rdatalist = new List<PolygonListFileDataRoot>();

                fpathlist.ForEach(fpath =>
                {
                    //ファイルの読み込み
                    PolygonListFile fp = new PolygonListFile();
                    PolygonListFileDataRoot rdata = fp.ReadFile(fpath);

                    rdatalist.Add(rdata);
                });

                //全データの読みこみ
                foreach (PolygonListFileDataRoot rdata in rdatalist)
                {
                    int index = rdata.RootID;
                    PolygonObjectFile pof = new PolygonObjectFile();
                    foreach (PolygonListData data in rdata.PolyFileList)
                    {
                        ///対象の読み込み
                        PolyData pol = pof.ReadFile(data.FilePath);

                        //ADD
                        this.AddVertexDic(index, pol);

                        index++;
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("VertexManager CreateResource Exception");
            }
        }

        /// <summary>
        /// モデル一つを追加登録する・・・デバッグ用
        /// </summary>
        /// <param name="index">ADD Index システム予約は使用しないこと。</param>
        /// <param name="cpofilepath">読み込みPolygonファイルパス</param>
        [Obsolete("このメソッドの使用は非推奨です。CreateResource関数を使用してください。")]
        public void AddResource(int index, string cpofilepath)
        {
            //キーの重複チェック
            bool ret = this.VertexDic.ContainsKey(index);
            if (ret == true)
            {
                throw new Exception("Already Add Key");
            }

            //対象の読み込み
            PolygonObjectFile pof = new PolygonObjectFile();
            PolyData pol = pof.ReadFile(cpofilepath);

            //ADD
            this.AddVertexDic(index, pol);
        }

        /// <summary>
        /// モデルを一つ追加する
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pdata"></param>
        internal void AddResource(int index, PolyData pdata)
        {
            this.AddVertexDic(index, pdata);
        }

        /// <summary>
        /// 全データの解放
        /// </summary>
        public void ReleaseResource()
        {
            this.ClearVertexDic();
        }


        /// <summary>
        /// 対象の描画、DeviceContext.Drawします。これより前にシェーダーなどを全て設定しておくこと。
        /// </summary>
        /// <param name="vid">描くVertex番号</param>
        /// <returns></returns>
        public static bool RenderData(int vid)
        {
            DeviceContext cont = DxManager.Mana.DxDevice.ImmediateContext;

            //対象の取得
            VertexManageData mdata = VertexManager.Instance.VertexDic[vid];

            //対象の頂点データ設定
            cont.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(mdata.VertexBuffer, Utilities.SizeOf<VertexInfo>(), 0));

            //Indexの設定
            cont.InputAssembler.SetIndexBuffer(mdata.IndexBuffer, Format.R32_UInt, 0);
            //cont.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;

            //描画！
            cont.DrawIndexed(mdata.Count, 0, 0);

            return true;
        }



        /// <summary>
        /// 開放時
        /// </summary>
        public void Dispose()
        {
            this.ClearVertexDic(true);
        }
    }
}
