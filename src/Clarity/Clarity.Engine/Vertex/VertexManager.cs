using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clarity.Engine.Vertex
{
    /// <summary>
    /// 頂点管理データ
    /// </summary>
    internal class VertexManageData : IDisposable
    {
        /// <summary>
        /// 頂点数
        /// </summary>
        public int Count = 0;

        /// <summary>
        /// 頂点バッファ
        /// </summary>
        public Vortice.Direct3D11.ID3D11Buffer VertexBuffer;

        /// <summary>
        /// indexバッファ
        /// </summary>
        public Vortice.Direct3D11.ID3D11Buffer IndexBuffer;

        public void Dispose()
        {
            
            this.VertexBuffer.Dispose();
            this.IndexBuffer.Dispose();
        }
    }


    internal class PolyCsvData
    {
        /// <summary>
        /// 管理番号
        /// </summary>
        public int VNo;

        /// <summary>
        /// PolygonファイルCSV一式
        /// </summary>
        public string Text;
    }

    /// <summary>
    /// 頂点情報管理
    /// </summary>
    internal class VertexManager : BaseClarityFactroy<VertexManager, VertexManageData>
    {   


        /// <summary>
        /// 作成
        /// </summary>
        public static void Create()
        {
            Instance = new VertexManager();

        }

        //---------------------------------------------------------------------------------
        



        /// <summary>
        /// VertexDicへADD
        /// </summary>
        /// <param name="vno">管理番号</param>
        /// <param name="pdata">対象データ</param>
        internal void AddVertexDic(int vno, PolyData pdata)
        {

            //頂点バッファの作製                                    
            Vortice.Direct3D11.ID3D11Buffer vbuf = Core.DxManager.Mana.DxDevice.CreateBuffer(Vortice.Direct3D11.BindFlags.VertexBuffer, pdata.VertexList.ToArray());

            //Indexバッファの作成
            Vortice.Direct3D11.ID3D11Buffer ibuf = Core.DxManager.Mana.DxDevice.CreateBuffer(Vortice.Direct3D11.BindFlags.IndexBuffer, pdata.IndexList.ToArray());

            //管理データ作製
            VertexManageData mdata = new VertexManageData();
            {
                mdata.VertexBuffer = vbuf;
                mdata.IndexBuffer = ibuf;
                mdata.Count = pdata.IndexList.Count;

            }
            this.ManaDic.Add(vno, mdata);
        }

        /// <summary>
        /// Buildin用、CSVファイル文字列からポリゴンを作成し、追加する
        /// </summary>
        /// <param name="datalist">読み込みセット一式</param>
        internal void LoadCSV(List<PolyCsvData> datalist)
        {
            //データの読み込み
            datalist.ForEach(x => {
                PolygonObjectFile pof = new PolygonObjectFile();
                PolyData pdata = pof.ReadCsvString(x.Text);

                VertexManager.Mana.AddVertexDic(x.VNo, pdata);
            });
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
                this.ClearManageDic();

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
        /// <param name="vno">ADD Index システム予約は使用しないこと。</param>
        /// <param name="cpofilepath">読み込みPolygonファイルパス</param>
        [Obsolete("このメソッドの使用は非推奨です。CreateResource関数を使用してください。")]
        public void AddResource(int vno, string cpofilepath)
        {
            //キーの重複チェック
            bool ret = this.ManaDic.ContainsKey(vno);
            if (ret == true)
            {
                throw new Exception("request key is already exists");
            }

            //対象の読み込み
            PolygonObjectFile pof = new PolygonObjectFile();
            PolyData pol = pof.ReadFile(cpofilepath);

            //ADD
            this.AddVertexDic(vno, pol);
        }

        /// <summary>
        /// モデルを一つ追加する
        /// </summary>
        /// <param name="vno"></param>
        /// <param name="pdata"></param>
        internal void AddResource(int vno, PolyData pdata)
        {
            this.AddVertexDic(vno, pdata);
        }

        /// <summary>
        /// 全データの解放
        /// </summary>
        public void ReleaseResource()
        {
            this.ClearManageDic();
        }


        //--------------------------------------------------------------------
        /// <summary>
        /// 対象の描画、DeviceContext.Drawします。これより前にシェーダーなどを全て設定しておくこと。
        /// </summary>
        /// <param name="vid">描くVertex番号</param>
        /// <returns></returns>
        public static bool RenderData(int vid)
        {
            var cont = Core.DxManager.Mana.DxContext;

            //対象の取得            
            VertexManageData mdata = VertexManager.Instance.ManaDic[vid];

            //対象の頂点データ設定
            cont.IASetVertexBuffer(0, mdata.VertexBuffer, System.Runtime.CompilerServices.Unsafe.SizeOf<VertexInfo>(), 0);

            cont.IASetIndexBuffer(mdata.IndexBuffer, Vortice.DXGI.Format.R32_UInt, 0);

            //描画！
            cont.DrawIndexed(mdata.Count, 0, 0);
            

            return true;
        }

    }
}
