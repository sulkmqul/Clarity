using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Numerics;

using Clarity.Engine.Core;
using Vortice.Direct3D11;
using Vortice.Mathematics;
using Vortice.DXGI;
using Vortice;

namespace Clarity.Engine.Texture
{
    /// <summary>
    /// テクスチャ管理データ
    /// </summary>
    internal class TextureManageData : IDisposable
    {
        /// <summary>
        /// これのリソース
        /// </summary>
        public ID3D11ShaderResourceView Srv = null;        

        /// <summary>
        /// これの識別名・基本的にはファイル名となる。
        /// </summary>
        public string Code = "";

        /// <summary>
        /// これの画像サイズ
        /// </summary>
        public Vector2 ImageSize = new Vector2(0.0f);

        /// <summary>
        /// 画像分割サイズ(アニメ用で例えば一つの画像に4つのアニメがある場合、1/4=0.25が設定される)
        /// </summary>
        public Vector2 IndexDiv = new Vector2(1.0f);

        public void Dispose()
        {
            this.Srv.Dispose();
            this.Srv = null;
        }
    }

    /// <summary>
    /// テクスチャ管理クラス
    /// </summary>
    internal class TextureManager : BaseClarityFactroy<TextureManager, TextureManageData>, IDisposable
    {
        private TextureManager()
        {
            
        }
             


        #region メンバ変数

        /// <summary>
        /// サンプラー。切り替える機会もないと思われるため
        /// </summary>
        private ID3D11SamplerState? _TexSampler = null;
        public ID3D11SamplerState TexSampler
        {
            get
            {
                if(this._TexSampler == null)
                {
                    throw new Exception("TextureManager is not initialized");
                }
                return this._TexSampler;
            }
            private set
            {
                this._TexSampler = value;
            }
        }

        #endregion


        public static void Create()
        {
            Instance = new TextureManager();

            //サンプラ作成・・・場所が良くない？
            Instance.TexSampler = Instance.CreateTextureSampler();
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// リソースの作成
        /// </summary>
        /// <param name="filepathlist">読み込みリストファイルパス一覧</param>
        /// <param name="create_flag">実際にShaderResourceViewを作成するか true=作成する</param>
        public void CreateResource(List<string> filepathlist, bool create_flag = true)
        {
            try
            {
                //既存の物を開放
                this.ClearUserData();

                //TextureSamplerの作成
                if (this.TexSampler == null && create_flag == true)
                {
                    this.TexSampler = this.CreateTextureSampler();
                }

                //テクスチャ一覧ファイルの読み込み
                List<TextureListFileDataRoot> rdatalist = new List<TextureListFileDataRoot>();
                filepathlist.ForEach(path =>
                {
                    TextureListFile tf = new TextureListFile();
                    TextureListFileDataRoot rdata = tf.ReadTextureList(path);
                    rdatalist.Add(rdata);
                });


                //全テクスチャの作成
                foreach (TextureListFileDataRoot rdata in rdatalist)
                {

                    //全データの読み込み
                    int tid = rdata.RootID;
                    foreach (TextureListData tdata in rdata.TextureList)
                    {
                        using (Bitmap bit = new Bitmap(tdata.FilePath))
                        {
                            this.AddTexture(tid, tdata.Filename, bit, tdata.IndexSize, tdata.Color);
                        }
                        tid++;

                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("Texture Init Exception", e);
            }
        }
        
        /// <summary>
        /// テクスチャの追加
        /// </summary>
        /// <param name="texid">texture識別ID</param>
        /// <param name="texcode">テキスチャコード</param>
        /// <param name="bit">読込テクスチャ</param>
        /// <param name="index_size">分割数</param>
        /// <param name="transcol">透過色 nullで指定なし</param>
        /// <exception cref="Exception"></exception>
        public void AddTexture(int texid, string texcode, Bitmap bit, Size index_size, System.Drawing.Color? transcol = null)
        {
            //RGBA変換
            byte[] rgba = this.CreateRGBABuffer(bit, transcol);
            this.AddTexture(texid, texcode, bit.Width, bit.Height, rgba, index_size);
        }


        /// <summary>
        /// テクスチャの追加
        /// </summary>
        /// <param name="texid">テキスチャ識別ID</param>
        /// <param name="texcode"テクスチャ識別コード></param>
        /// <param name="width">画像横幅</param>
        /// <param name="height">画像縦幅</param>
        /// <param name="rgba_buf">RGBAバッファ</param>
        /// <param name="index_size">分割数</param>        
        public void AddTexture(int texid, string texcode, int width, int height, byte[] rgba_buf, Size index_size)
        {
            //重複チェック
            bool ret = this.ManaDic.ContainsKey(texid);
            if (ret == true)
            {
                throw new Exception("ID is Already Exists!");
            }

            float dx = 1.0f / (float)index_size.Width;
            float dy = 1.0f / (float)index_size.Height;
            ID3D11ShaderResourceView sr = this.LoadTexture(width, height, rgba_buf);
            TextureManageData td = new TextureManageData() { Srv = sr, IndexDiv = new Vector2(dx, dy), Code = texcode, ImageSize = new Vector2(width, height) };
            this.ManaDic.Add(texid, td);
        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// テクスチャの設定
        /// </summary>
        /// <param name="texid">テクスチャ番号</param>
        /// <param name="tslot">テクスチャスロット</param>
        /// <param name="sslot">サンプラースロット</param>
        /// <returns></returns>
        public static void SetTexture(int texid, int tslot = 0, int sslot = 0)
        {
            ID3D11DeviceContext cont = DxManager.Mana.DxContext;

            //TextureManageData td = TextureManager.Mana.ManaDic[texid];
            TextureManageData? td = TextureManager.Mana.GetTextureManageData(texid);
            if (td == null)
            {
                return;
            }

            //今回の対象データ取得
            ID3D11SamplerState texst = TextureManager.Mana.TexSampler;
            ID3D11ShaderResourceView sr = td.Srv;

            //テクスチャサンプラ設定            
            cont.PSSetSampler(sslot, texst);

            //テクスチャ設定            
            cont.PSSetShaderResource(tslot, sr);

        }

        
        /// <summary>
        /// テクスチャ設定(外部)
        /// </summary>
        /// <param name="sr"></param>
        public static void SetTexture(ID3D11ShaderResourceView sr)
        {
            ID3D11DeviceContext cont = DxManager.Mana.DxContext;

            //今回の対象データ取得
            ID3D11SamplerState texst = TextureManager.Mana.TexSampler;

            //テクスチャサンプラ設定            
            cont.PSSetSampler(0, texst);

            //テクスチャ設定            
            cont.PSSetShaderResource(0, sr);
        }

        /// <summary>
        /// 対象テクスチャのサイズを取得
        /// </summary>
        /// <param name="tid">テクスチャID</param>
        /// <param name="f">trueの時は実サイズ、falseの時は実際に使用される分割サイズを返却</param>
        /// <returns></returns>
        public static Vector2 GetTextureSize(int tid, bool f = false)
        {
            TextureManageData? mdata = TextureManager.Mana.GetTextureManageData(tid);
            if (mdata == null)
            {
                return new Vector2(0.0f);
            }
            if (f == true)
            {
                return mdata.ImageSize;
            }


            Vector2 ans = new Vector2(mdata.ImageSize.X * mdata.IndexDiv.X, mdata.ImageSize.Y * mdata.IndexDiv.Y);
            return ans;
        }


        /// <summary>
        /// テクスチャ分割サイズの取得
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static Vector2? GetTextureDivSize(int tid)
        {
            TextureManageData? mdata = TextureManager.Mana.GetTextureManageData(tid);
            return mdata?.IndexDiv;

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// テクスチャの読込
        /// </summary>
        /// <param name="width">横幅</param>
        /// <param name="height">縦幅</param>
        /// <param name="rgba_buf">RGBAバッファ</param>
        /// <returns></returns>
        protected ID3D11ShaderResourceView LoadTexture(int width, int height, byte[] rgba_buf)
        {
            //コピー領域を作成
            IntPtr pbuf = IntPtr.Zero;
            using (BlockProcedureState st = new BlockProcedureState(() =>
            {
                pbuf = Marshal.AllocCoTaskMem(rgba_buf.Length);
            },
            () =>
            {
                Marshal.FreeCoTaskMem(pbuf);
            }))
            {

                Marshal.Copy(rgba_buf, 0, pbuf, rgba_buf.Length);
                


                //画像DataBox作製
                //DataBox[] databox = new DataBox[] { new DataBox(bdata.Scan0, bit.Width * 4, bit.Height) };
                SubresourceData[] drbox = new SubresourceData[] { new SubresourceData(pbuf, width * 4, height) };

                Texture2DDescription depdec = new Texture2DDescription();
                #region TextureDescriptionの作製
                //depdec.Format = Format.B8G8R8A8_UNorm;    //バッファフォーマット　8bitBGRA                        
                depdec.Format = Format.R8G8B8A8_UNorm;    //バッファフォーマット　8bitRGBA                        
                depdec.ArraySize = 1;       //テクスチャの数
                depdec.MipLevels = 1;       //ミップレベル 基本1

                depdec.Width = width;     //テクスチャサイズＷ
                depdec.Height = height;   //テクスチャサイズH
                depdec.SampleDescription = new SampleDescription(1, 0); //マルチサンプリングの値、count=1pixelあたりのサンプル数、quality=クオリティ0～  CheckMultiSampleQualityLevels  - 1まで
                depdec.Usage = ResourceUsage.Default;   //使い方：基本default
                depdec.BindFlags = BindFlags.ShaderResource;      //ShaderResourceとしての使用
                depdec.CpuAccessFlags = CpuAccessFlags.None;    //許可するCPUアクセス、基本none
                depdec.OptionFlags = ResourceOptionFlags.None;  //基本none
                #endregion

                //Texture作製
                ID3D11ShaderResourceView ans;
                using (ID3D11Texture2D tex = DxManager.Mana.DxDevice.CreateTexture2D(depdec, drbox))
                {
                    ans = DxManager.Mana.DxDevice.CreateShaderResourceView(tex);
                }
                return ans;
            }
        }

        /// <summary>
        /// テクスチャサンプラーの作製
        /// </summary>
        /// <returns></returns>
        protected ID3D11SamplerState CreateTextureSampler()
        {
            //テクスチャサンプラーの作成
            SamplerDescription ssd = new SamplerDescription();

            //テクスチャのサンプル時に使用するフィルタリングメソッド D3D11_FILTERの値を指定
            ssd.Filter = Filter.ComparisonMinMagMipLinear;
            

            //テクスチャの座標解決方法 Wrapで繰り返し。D3D11_TEXTURE_ADDRESS_MODEの値を指定
            ssd.AddressU = TextureAddressMode.Wrap;
            ssd.AddressV = TextureAddressMode.Wrap;
            ssd.AddressW = TextureAddressMode.Wrap;

            //TextureAddressMode.Borderの時に有効な色
            ssd.BorderColor = Color4.Black;

            //サンプリングデータを比較する方法
            ssd.ComparisonFunction = ComparisonFunction.Never;

            //計算されたmipmapレベルからのオフセット
            ssd.MipLODBias = 0;

            //ミップマップ範囲下限、上限
            ssd.MinLOD = 0;
            ssd.MaxLOD = 0;

            //作成
            ID3D11SamplerState ans = DxManager.Mana.DxDevice.CreateSamplerState(ssd);

            return ans;
        }
                

        /// <summary>
        /// 指定テクスチャの削除
        /// </summary>
        /// <param name="texid">削除管理ID</param>
        internal void RemoveTexture(int texid)
        {
            TextureManageData mdata = this.ManaDic[texid];
            mdata.Dispose();
            this.ManaDic.Remove(texid);
        }

        /// <summary>
        /// テクスチャIDの検索 なし=int.MinVal
        /// </summary>
        /// <param name="texcode"></param>
        /// <returns></returns>
        internal int SearchTextureID(string texcode)
        {
            //要素の取得
            var d = this.ManaDic.FirstOrDefault(x =>
            {
                return (x.Value.Code == texcode);
            });
            if (d.Value == null)
            {
                return int.MinValue;
            }
            

            return d.Key;
        }

        /// <summary>
        /// IDとcodeの一覧を作成する（Aid用）
        /// </summary>
        /// <returns></returns>
        internal List<(int id, string code)> CreateIDList()
        {
            return this.ManaDic.Select(x => (x.Key, x.Value.Code)).ToList();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 対象テクスチャ情報の取得・・・代替案があるならそちらの方が良い。
        /// </summary>
        /// <param name="tid">texture id</param>
        /// <returns></returns>
        private TextureManageData? GetTextureManageData(int tid)
        {
            if (this.ManaDic.ContainsKey(tid) == false)
            {
                return null;
            }

            return this.ManaDic[tid];
        }




        /// <summary>
        /// BitmapをRGBAバッファに変換する
        /// </summary>
        /// <param name="bit">作成元bitmap</param>
        /// <param name="transcol">透過色</param>
        /// <returns>作成したRGBAバッファ</returns>        
        public byte[] CreateRGBABuffer(Bitmap bit, System.Drawing.Color? transcol = null)
        {
            int w = bit.Width;
            int h = bit.Height;
            byte[] bgrabuf = new byte[w * h * 4];


            Rectangle rect = new Rectangle(0, 0, w, h);
            BitmapData bdata = bit.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            try
            {
                //一括コピー
                Marshal.Copy(bdata.Scan0, bgrabuf, 0, bgrabuf.Length);
            }
            finally
            {
                bit.UnlockBits(bdata);
            }


            //BGRAをRGBAに変換する
            byte[] ans = this.ConvertBGRA_RGBA(w, h, bgrabuf, transcol);

            return ans;
        }



        /// <summary>
        /// RGBAのバッファをbitmapBGRA形式に変換する、逆もそのまま使えます
        /// </summary>
        /// <param name="width">画像横幅</param>
        /// <param name="height">画像縦幅</param>
        /// <param name="colbuf">RGBAバッファ</param>
        /// <returns>BGRAバッファ</returns>
        private byte[] ConvertBGRA_RGBA(int width, int height, byte[] colbuf, System.Drawing.Color? transcol)
        {
            int colset = 4;

            byte[] ansbuf = new byte[width * height * colset];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int bpos = (y * width * colset) + (x * colset);

                    ansbuf[bpos] = colbuf[bpos + 2];
                    ansbuf[bpos + 1] = colbuf[bpos + 1];
                    ansbuf[bpos + 2] = colbuf[bpos];
                    ansbuf[bpos + 3] = colbuf[bpos + 3];

                    if (transcol != null)
                    { 
                        //透過させる
                        if (ansbuf[bpos] == transcol?.R && ansbuf[bpos + 1] == transcol?.G && ansbuf[bpos + 2] == transcol?.B)
                        {
                            ansbuf[bpos] = 0;
                            ansbuf[bpos + 1] = 0;
                            ansbuf[bpos + 2] = 0;
                            ansbuf[bpos + 3] = 0;
                        }
                    }
                }
            }

            return ansbuf;
        }


    }
}
