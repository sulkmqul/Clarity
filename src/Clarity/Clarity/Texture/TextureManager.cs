using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.Direct3D;
using SharpDX.Windows;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Clarity.Core;

namespace Clarity.Texture
{
    /// <summary>
    /// テクスチャ管理クラス
    /// </summary>
    internal class TextureManager : BaseClaritySingleton<TextureManager>, IDisposable
    {
        private TextureManager()
        {
        }


        /// <summary>
        /// テクスチャ管理データ
        /// </summary>
        internal class TextureManageData : IDisposable
        {
            /// <summary>
            /// これのリソース
            /// </summary>
            public ShaderResourceView Srv = null;

            /// <summary>
            /// これの識別名・基本的にはファイル名となる。
            /// </summary>
            public string Code;

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


        #region メンバ変数

        /// <summary>
        /// サンプラー。切り替える機会もないと思われるため
        /// </summary>
        public SamplerState TexSampler = null;

        /// <summary>
        /// テクスチャ管理Dic
        /// </summary>
        private Dictionary<int, TextureManageData> TexDic = new Dictionary<int, TextureManageData>();
        #endregion


        public static void Create()
        {
            Instance = new TextureManager();
        }





        /// <summary>
        /// テクスチャサンプラーの作製
        /// </summary>
        /// <returns></returns>
        protected SamplerState CreateTextureSampler()
        {
            //テクスチャサンプラーの作成
            SamplerStateDescription ssd = new SamplerStateDescription();

            //テクスチャのサンプル時に使用するフィルタリングメソッド D3D11_FILTERの値を指定
            ssd.Filter = Filter.ComparisonMinMagMipLinear;

            //テクスチャの座標解決方法 Wrapで繰り返し。D3D11_TEXTURE_ADDRESS_MODEの値を指定
            ssd.AddressU = TextureAddressMode.Wrap;
            ssd.AddressV = TextureAddressMode.Wrap;
            ssd.AddressW = TextureAddressMode.Wrap;

            //TextureAddressMode.Borderの時に有効な色
            ssd.BorderColor = SharpDX.Color.Black;

            //サンプリングデータを比較する方法
            ssd.ComparisonFunction = Comparison.Never;

            //計算されたmipmapレベルからのオフセット
            ssd.MipLodBias = 0;

            //ミップマップ範囲下限、上限
            ssd.MinimumLod = 0;
            ssd.MaximumLod = 0;


            SamplerState ans = new SamplerState(DxManager.Mana.DxDevice, ssd);


            return ans;
        }



        /// <summary>
        /// テクスチャの読み込み
        /// </summary>
        /// <param name="filename">読み込みファイル名</param>
        /// <param name="transcol">透過色</param>
        /// <returns></returns>
        protected ShaderResourceView ReadTexture(string filename, System.Drawing.Color? transcol)
        {
            ShaderResourceView ans = null;

            try
            {
                //ファイル読み込み
                using (Bitmap srcbit = new Bitmap(filename))
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
                            using (Texture2D tex = new Texture2D(DxManager.Mana.DxDevice, depdec, databox))
                            {
                                ans = new ShaderResourceView(DxManager.Mana.DxDevice, tex);
                            }

                        }
                        finally
                        {
                            bit.UnlockBits(bdata);
                        }
                    }
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
        /// <param name="filepath">読み込みリストファイルパス一覧</param>
        public void CreateResource(List<string> filepathlist)
        {

            try
            {
                //既存の物を開放
                this.ClearTexture();

                //TextureSamplerの作成
                if (this.TexSampler == null)
                {
                    this.TexSampler = this.CreateTextureSampler();
                }


                //テクスチャ一覧ファイルの読み込み
                List<TextureListFileDataRoot> rdatalist = new List<TextureListFileDataRoot>();
                filepathlist.ForEach(path =>
                {
                    TextureListFile tf = new TextureListFile();
                    TextureListFileDataRoot rdata = tf.ReadFile(path);
                    rdatalist.Add(rdata);
                });


                //全テクスチャの作成
                foreach (TextureListFileDataRoot rdata in rdatalist)
                {

                    //全データの読み込み
                    int code = rdata.RootID;
                    foreach (TextureListFileData tdata in rdata.TextureList)
                    {
                        ShaderResourceView srv = this.ReadTexture(tdata.FilePath, tdata.Color);

                        float dx = 1.0f / (float)tdata.IndexSize.Width;
                        float dy = 1.0f / (float)tdata.IndexSize.Height;
                        TextureManageData tex = new TextureManageData() { Srv = srv, IndexDiv = new Vector2(dx, dy), Code = tdata.Filename };
                        this.TexDic.Add(code, tex);
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
            bool ret = this.TexDic.ContainsKey(texid);
            if (ret == true)
            {
                throw new Exception("ID is Already Exists!");
            }

            //内部識別名の作成
            string texcode = System.IO.Path.GetFileNameWithoutExtension(filepath);


            float dx = 1.0f / (float)index_size.Width;
            float dy = 1.0f / (float)index_size.Height;
            ShaderResourceView sr = this.ReadTexture(filepath, null);
            TextureManageData td = new TextureManageData() { Srv = sr, IndexDiv = new Vector2(dx, dy), Code = texcode };
            this.TexDic.Add(texid, td);
        }



        /// <summary>
        /// 現在読み込まれているテクスチャの開放
        /// </summary>
        public void ClearTexture()
        {
            //開放
            foreach (TextureManageData td in this.TexDic.Values)
            {
                td.Dispose();
            }
            this.TexDic.Clear();
        }

        /// <summary>
        /// テクスチャIDの検索 なし=int.MinVal
        /// </summary>
        /// <param name="texcode"></param>
        /// <returns></returns>
        internal int SearchTextureID(string texcode)
        {
            //要素の取得
            var d = this.TexDic.FirstOrDefault(x =>
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
        internal TextureManageData GetTextureManageData(int tid)
        {
            return this.TexDic[tid];
        }

        //==============================================================================================================
        /// <summary>
        /// テクスチャの設定 設定テクスチャの分割サイズを返却
        /// </summary>
        /// <param name="texid">テクスチャ番号</param>
        /// <returns></returns>
        public static Vector2 SetTexture(int texid)
        {
            DeviceContext cont = DxManager.Mana.DxDevice.ImmediateContext;

            TextureManageData td = TextureManager.Mana.TexDic[texid];

            //今回の対象データ取得
            SamplerState texst = TextureManager.Mana.TexSampler;
            ShaderResourceView sr = td.Srv;

            //テクスチャサンプラ設定
            cont.PixelShader.SetSampler(0, texst);

            //テクスチャ設定
            cont.PixelShader.SetShaderResource(0, sr);

            return td.IndexDiv;
        }

        

        /// <summary>
        /// テクスチャ設定(外部)
        /// </summary>
        /// <param name="sr"></param>
        public static void SetTexture(ShaderResourceView sr)
        {
            DeviceContext cont = DxManager.Mana.DxDevice.ImmediateContext;

            //今回の対象データ取得
            SamplerState texst = TextureManager.Mana.TexSampler;

            //テクスチャサンプラ設定
            cont.PixelShader.SetSampler(0, texst);

            //テクスチャ設定
            cont.PixelShader.SetShaderResource(0, sr);
        }


        



        /// <summary>
        /// 削除
        /// </summary>
        public void Dispose()
        {
            //テクスチャクリア
            this.ClearTexture();

            //サンプラクリア
            this.TexSampler?.Dispose();
            this.TexSampler = null;
        }
    }
}
