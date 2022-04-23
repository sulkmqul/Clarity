﻿using System;
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
        public string Code;

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
        public ID3D11SamplerState TexSampler = null;
   
        #endregion


        public static void Create()
        {
            Instance = new TextureManager();
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
        /// テクスチャの読み込み
        /// </summary>
        /// <param name="srcbit"></param>
        /// <param name="transcol"></param>
        /// <returns></returns>
        protected ID3D11ShaderResourceView ReadTexture(Bitmap srcbit, System.Drawing.Color? transcol)
        {
            ID3D11ShaderResourceView ans = null;

            try
            {
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, srcbit.Width, srcbit.Height);

                //指定フォーマットに変換
                using (Bitmap bit = srcbit.Clone(rect, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                {
                    BitmapData bdata = bit.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bit.PixelFormat);
                    try
                    {
                        #region 透過色の設定
                        {
                            //作業領域作製             
                            int bitbufsize = bit.Width * bit.Height * 4;
                            byte[] buf = new byte[bitbufsize];

                            Marshal.Copy(bdata.Scan0, buf, 0, bitbufsize);

                            //透過色の設定
                            int size = bit.Width * bit.Height;
                            for (int i = 0; i < size; i++)
                            {
                                int pos = i * 4;

                                //RGBの値を取得
                                byte b = buf[pos];
                                pos++;

                                byte g = buf[pos];
                                pos++;

                                byte r = buf[pos];
                                pos++;

                                if (transcol != null)
                                {
                                    System.Drawing.Color tcol = transcol.Value;
                                    //透過色なら透明にする
                                    if (r == tcol.R && g == tcol.G && b == tcol.B)
                                    {
                                        buf[pos] = 0;
                                    }
                                }
                            }

                            //元へ戻す。
                            Marshal.Copy(buf, 0, bdata.Scan0, bitbufsize);
                        }
                        #endregion

                        //画像DataBox作製
                        DataBox[] databox = new DataBox[] { new DataBox(bdata.Scan0, bit.Width * 4, bit.Height) };
                        SubresourceData[] drbox = new SubresourceData[] { new SubresourceData(bdata.Scan0, bit.Width * 4, bit.Height) };

                        Texture2DDescription depdec = new Texture2DDescription();
                        #region TextureDescriptionの作製
                        depdec.Format = Format.B8G8R8A8_UNorm;    //バッファフォーマット　8bitRGBA
                        depdec.ArraySize = 1;       //テクスチャの数
                        depdec.MipLevels = 1;       //ミップレベル 基本1

                        depdec.Width = bit.Width;     //テクスチャサイズＷ
                        depdec.Height = bit.Height;   //テクスチャサイズH
                        depdec.SampleDescription = new SampleDescription(1, 0); //マルチサンプリングの値、count=1pixelあたりのサンプル数、quality=クオリティ0～  CheckMultiSampleQualityLevels  - 1まで
                        depdec.Usage = ResourceUsage.Default;   //使い方：基本default
                        depdec.BindFlags = BindFlags.ShaderResource;      //ShaderResourceとしての使用
                        depdec.CpuAccessFlags = CpuAccessFlags.None;    //許可するCPUアクセス、基本none
                        depdec.OptionFlags = ResourceOptionFlags.None;  //基本none
                        #endregion

                        //Texture作製
                        using (ID3D11Texture2D tex = DxManager.Mana.DxDevice.CreateTexture2D(depdec, drbox))
                        {
                            ans = DxManager.Mana.DxDevice.CreateShaderResourceView(tex);
                        }

                    }
                    finally
                    {
                        bit.UnlockBits(bdata);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("ReadTexture", ex);
            }

            return ans;
        }

        /// <summary>
        /// テクスチャの読み込み
        /// </summary>
        /// <param name="filename">読み込みファイル名</param>
        /// <param name="transcol">透過色</param>
        /// <returns></returns>
        protected ID3D11ShaderResourceView ReadTexture(string filename, System.Drawing.Color? transcol, ref Vector2 tsize)
        {
            ID3D11ShaderResourceView ans = null;
            tsize = new Vector2(0.0f);
            try
            {
                //ファイル読み込み
                using (Bitmap srcbit = new Bitmap(filename))
                {
                    ans = this.ReadTexture(srcbit, transcol);
                    tsize = new Vector2(srcbit.Width, srcbit.Height);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Read Texture Exception file=" + filename, e);
            }

            return ans;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// リソースの作成
        /// </summary>
        /// <param name="filepath"></param>
        public void CreateResource(string filepath)
        {
            List<string> flist = new List<string>() { filepath };
            this.CreateResource(flist);
        }


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
                    int code = rdata.RootID;
                    foreach (TextureListFileData tdata in rdata.TextureList)
                    {
                        Vector2 tsize = new Vector2();
                        ID3D11ShaderResourceView srv = null;

                        if (create_flag == true)
                        {
                            srv = this.ReadTexture(tdata.FilePath, tdata.Color, ref tsize);
                        }

                        float dx = 1.0f / (float)tdata.IndexSize.Width;
                        float dy = 1.0f / (float)tdata.IndexSize.Height;
                        TextureManageData tex = new TextureManageData() { Srv = srv, IndexDiv = new Vector2(dx, dy), Code = tdata.Filename, ImageSize = tsize };
                        this.ManaDic.Add(code, tex);
                        code++;

                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception("Texture Init Exception", e);
            }
        }


        /// <summary>
        /// テクスチャの個別追加
        /// </summary>
        /// <param name="texid"></param>
        /// <param name="filepath"></param>
        [Obsolete("このメソッドの使用は非推奨です。CreateResource関数を使用してください。")]
        public void AddTexture(int texid, string filepath, Size index_size)
        {
            //重複チェック
            bool ret = this.ManaDic.ContainsKey(texid);
            if (ret == true)
            {
                throw new Exception("ID is Already Exists!");
            }

            //内部識別名の作成
            string texcode = System.IO.Path.GetFileNameWithoutExtension(filepath);

            Vector2 tsize = new Vector2();
            float dx = 1.0f / (float)index_size.Width;
            float dy = 1.0f / (float)index_size.Height;
            ID3D11ShaderResourceView sr = this.ReadTexture(filepath, null, ref tsize);
            TextureManageData td = new TextureManageData() { Srv = sr, IndexDiv = new Vector2(dx, dy), Code = texcode, ImageSize = tsize };
            td.Code = System.IO.Path.GetFileNameWithoutExtension(filepath);
            this.ManaDic.Add(texid, td);

            //サンプラを作成していないなら作成
            if (this.TexSampler == null)
            {
                this.TexSampler = this.CreateTextureSampler();
            }
        }


        /// <summary>
        /// テクスチャの読み込み　Bitmap直版
        /// </summary>
        /// <param name="texid">addtexture id</param>
        /// <param name="bit">画像</param>
        /// <param name="texcode">識別名</param>
        /// <param name="index_size">indexサイズ</param>
        internal void AddTexture(int texid, Bitmap bit, string texcode, Size index_size)
        {
            //重複チェック
            bool ret = this.ManaDic.ContainsKey(texid);
            if (ret == true)
            {
                throw new Exception("ID is Already Exists!");
            }

            float dx = 1.0f / (float)index_size.Width;
            float dy = 1.0f / (float)index_size.Height;
            ID3D11ShaderResourceView sr = this.ReadTexture(bit, null);
            TextureManageData td = new TextureManageData() { Srv = sr, IndexDiv = new Vector2(dx, dy), Code = texcode, ImageSize = new Vector2(bit.Width, bit.Height)};
            this.ManaDic.Add(texid, td);
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
        /// 対象テクスチャ情報の取得・・・代替案があるならそちらの方が良い。
        /// </summary>
        /// <param name="tid">texture id</param>
        /// <returns></returns>
        private TextureManageData GetTextureManageData(int tid)
        {
            if (this.ManaDic.ContainsKey(tid) == false)
            {
                return null;
            }

            return this.ManaDic[tid];
        }

        /// <summary>
        /// IDとcodeの一覧を作成する（Aid用）
        /// </summary>
        /// <returns></returns>
        internal List<(int id, string code)> CreateIDList()
        {
            return this.ManaDic.Select(x => (x.Key, x.Value.Code)).ToList();
        }
        //==============================================================================================================
        /// <summary>
        /// テクスチャの設定 設定テクスチャの分割サイズを返却
        /// </summary>
        /// <param name="texid">テクスチャ番号</param>
        /// <param name="tslot">テクスチャスロット</param>
        /// <param name="sslot">サンプラースロット</param>
        /// <returns></returns>
        public static void SetTexture(int texid, int tslot = 0, int sslot = 0)
        {
            ID3D11DeviceContext cont = DxManager.Mana.DxContext;

            TextureManageData td = TextureManager.Mana.ManaDic[texid];

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
            TextureManageData mdata = TextureManager.Mana.GetTextureManageData(tid);
            if (f == true)            
            {
                return mdata.ImageSize;
            }
            if (mdata == null)
            {
                return new Vector2(0.0f);
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
            TextureManageData mdata = TextureManager.Mana.GetTextureManageData(tid);
            return mdata?.IndexDiv;

        }
    }
}