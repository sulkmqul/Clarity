using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Vortice.Direct3D11;

using Clarity.Engine.Core;
using Vortice.DXGI;

namespace Clarity.Engine.Shader
{
    /// <summary>
    /// シェーダーデータ
    /// </summary>
    internal class ShaderManageData : IDisposable
    {
        /// <summary>
        /// VertexShaer
        /// </summary>
        public ID3D11VertexShader Vs = null;

        /// <summary>
        /// PixelShader
        /// </summary>
        public ID3D11PixelShader Ps = null;

        /// <summary>
        /// 入力レイアウト設定
        /// </summary>
        public ID3D11InputLayout Layout = null;

        /// <summary>
        /// これシェーダーで使用するContantBuffer(VertexShaderに送るデータ領域)
        /// </summary>
        public ID3D11Buffer ConstantBuffer = null;

        public void Dispose()
        {
            this.Vs?.Dispose();
            this.Ps?.Dispose();
            this.Layout?.Dispose();
            this.ConstantBuffer?.Dispose();

        }
    }



    /// <summary>
    /// Shader管理規定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>これは継承先ではsingletonにすること</remarks>
    internal class ShaderManager : BaseClarityFactroy<ShaderManager, ShaderManageData>
    {
        private ShaderManager()
        {
        }

        /// <summary>
        /// 頂点Shader
        /// </summary>
        private string VertexShaderProfile = "";

        /// <summary>
        /// PixelShader
        /// </summary>
        private string PixelShaderProfile = "";
        

        /// <summary>
        /// 作成関数
        /// </summary>
        public static void Create()
        {
            Instance = new ShaderManager();

            //必要な情報の取得
            Instance.VertexShaderProfile = ClarityEngine.EngineSetting.GetString("VertexShaderVersion", "vs_4_0");
            Instance.PixelShaderProfile = ClarityEngine.EngineSetting.GetString("PixelShaderVersion", "ps_4_0");
        }



        //==========================================================================================
        /// <summary>
        /// 対象ソースファイルの全行を読み込む
        /// </summary>
        /// <param name="filepath">読み込みファイルパス</param>
        /// <returns></returns>
        private string LoadSrcFileAll(string filepath)
        {
            string srccode = "";
            //ShaderFileの読み込み
            using (FileStream fp = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fp))
                {
                    srccode = sr.ReadToEnd();
                }
            }
            return srccode;
        }


        /// <summary>
        /// VertexShaderのコンパイル(文字列から)
        /// </summary>
        /// <param name="srcode"></param>
        /// <param name="funcname"></param>
        /// <param name="ipevec"></param>
        /// <param name="ipl"></param>
        /// <returns></returns>
        protected ID3D11VertexShader CompileVertexShader(string srcode, string funcname, InputElementDescription[] ipevec, out ID3D11InputLayout ipl)
        {
            ID3D11VertexShader ans = null;

            ipl = null;
            try
            {

                Vortice.Direct3D.Blob blob, erblob;
                var cpret = Vortice.D3DCompiler.Compiler.Compile(srcode, funcname, "unknown", this.VertexShaderProfile, out blob, out erblob);

                /*
                //コンパイル                
                var code = ShaderBytecode.Compile(srcode, funcname, this.VertexShaderProfile);
                if (code == null)
                {
                    throw new Exception("CompileFromFile NULL");
                }
                if (code.Bytecode == null)
                {
                    throw new Exception(code.Message);
                }*/

                //頂点レイアウトを作製
                /*
                ShaderSignature sig = ShaderSignature.GetInputSignature(code);
                ipl = new InputLayout(DxManager.Mana.DxDevice, sig, ipevec);
                */
                ipl = DxManager.Mana.DxDevice.CreateInputLayout(ipevec, blob);

                //作製
                //ans = new VertexShader(DxManager.Mana.DxDevice, code);
                ans = DxManager.Mana.DxDevice.CreateVertexShader(blob);
            }
            catch (Exception ex)
            {
                throw new Exception("Crate VertexShader", ex);
            }

            return ans;
        }

        /// <summary>
        /// VertexShaderのコンパイル(ファイルから)
        /// </summary>
        /// <param name="filepath">シェーダーファイル名</param>
        /// <param name="funcname">関数名</param>
        /// <param name="ipl">頂点レイアウト</param>
        /// <returns></returns>
        protected ID3D11VertexShader CompileVertexShaderFile(string filepath, string funcname, InputElementDescription[] ipevec, out ID3D11InputLayout ipl)
        {
            ID3D11VertexShader ans = null;

            try
            {
                //ソースファイルの読み込み
                string srccode = this.LoadSrcFileAll(filepath);

                ans = this.CompileVertexShader(srccode, funcname, ipevec, out ipl);
            }
            catch (Exception e)
            {
                throw new Exception("CompileVertexShaderFile filepath=" + filepath, e);
            }
            
            return ans;
        }




        /// <summary>
        /// PixelShaderのコンパイル
        /// </summary>
        /// <param name="srccode"></param>
        /// <param name="funcname"></param>
        /// <returns></returns>
        protected ID3D11PixelShader CompilePixelShader(string srccode, string funcname)
        {
            ID3D11PixelShader ans = null;
            try
            {
                /*
                //コンパイル                
                var code = ShaderBytecode.Compile(srccode, funcname, this.PixelShaderProfile);
                if (code == null)
                {
                    throw new Exception(code.Message);
                }*/

                Vortice.Direct3D.Blob blob, erblob;
                var cpret = Vortice.D3DCompiler.Compiler.Compile(srccode, funcname, "unknown", this.PixelShaderProfile, out blob, out erblob);

                //作製
                //ans = new PixelShader(DxManager.Mana.DxDevice, code);
                ans = DxManager.Mana.DxDevice.CreatePixelShader(blob);

            }
            catch (Exception ex)
            {
                throw new Exception("Pixel Shader Compile", ex);
            }

            return ans;
        }

        /// <summary>
        /// PixelShaderのコンパイル
        /// </summary>
        /// <param name="filepath">シェーダーファイル名</param>
        /// <param name="funcname">関数名</param>
        /// <returns></returns>
        protected ID3D11PixelShader CompilePixelShaderFile(string filepath, string funcname)
        {
            ID3D11PixelShader ans = null;
            try
            {
                //Shaderソース読みおｋ美
                string srccode = this.LoadSrcFileAll(filepath);

                //読み込み
                ans = this.CompilePixelShader(srccode, funcname);

            }
            catch (Exception e)
            {
                throw new Exception("CompilePixelShaderFile path=" + filepath, e);
            }

            return ans;
        }


        

        /// <summary>
        /// Shaderデータ一つの作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sd"></param>
        /// <param name="ipevec"></param>
        /// <returns></returns>
        private ShaderManageData CreateShaderManageData<T>(ShaderListData sd, InputElementDescription[] ipevec) where T : struct
        {
            ShaderManageData data = new ShaderManageData();

            //VertexShader
            data.Vs = this.CompileVertexShaderFile(sd.FilePath, sd.VsName, ipevec, out data.Layout);
            if (data.Vs == null)
            {
                throw new Exception("CompileVertexShader Exception");
            }

            //PixelShader
            data.Ps = this.CompilePixelShaderFile(sd.FilePath, sd.PsName);
            if (data.Ps == null)
            {
                throw new Exception("CompilePixelShader Exception");
            }


            //コンスタントバッファ
            //data.ConstantBuffer = new SharpDX.Direct3D11.Buffer(DxManager.Mana.DxDevice, Utilities.SizeOf<T>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

            BufferDescription desc = new BufferDescription();
            {
                desc.BindFlags = BindFlags.ConstantBuffer;
                desc.CpuAccessFlags = CpuAccessFlags.None;
                desc.OptionFlags = ResourceOptionFlags.None;
                desc.SizeInBytes = System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
                desc.StructureByteStride = 0;
            }
            data.ConstantBuffer = DxManager.Mana.DxDevice.CreateBuffer(desc);
            return data;
        }



        /// <summary>
        /// Shaderデータ一つの作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sd"></param>
        /// <param name="ipevec"></param>
        /// <returns></returns>
        private ShaderManageData CreateShaderManageData<T>(string srcode, string vs_name, string ps_name, InputElementDescription[] ipevec) where T : struct
        {
            ShaderManageData data = new ShaderManageData();

            //VertexShader
            data.Vs = this.CompileVertexShader(srcode, vs_name, ipevec, out data.Layout);
            if (data.Vs == null)
            {
                throw new Exception("CompileVertexShader Exception");
            }

            //PixelShader
            data.Ps = this.CompilePixelShader(srcode, ps_name);
            if (data.Ps == null)
            {
                throw new Exception("CompilePixelShader Exception");
            }

            //コンスタントバッファ
            BufferDescription desc = new BufferDescription();
            {
                desc.BindFlags = BindFlags.ConstantBuffer;
                desc.CpuAccessFlags = CpuAccessFlags.None;
                desc.OptionFlags = ResourceOptionFlags.None;
                desc.SizeInBytes = System.Runtime.CompilerServices.Unsafe.SizeOf<T>();
                desc.StructureByteStride = 0;
            }
            data.ConstantBuffer = DxManager.Mana.DxDevice.CreateBuffer(desc);

            return data;
        }

        /// <summary>
        /// 一つのShaderListFileデータの読み込み
        /// </summary>
        /// <param name="rdata">読み込みデータ</param>
        private void LoadShaderListFileData<T>(ShaderListFileDataRoot rdata, InputElementDescription[] ipevec) where T : struct
        {
            //シェーダーのcompileと読み込み
            int index = rdata.RootID;
            foreach (ShaderListData sd in rdata.ShaderList)
            {

                //一つのデータの読みこみ
                ShaderManageData data = null;

                if (sd.EnabledSrcCode == false)
                {
                    data = this.CreateShaderManageData<T>(sd, ipevec);
                }
                else
                {                    
                    data = this.CreateShaderManageData<T>(sd.SrcCode, sd.VsName, sd.PsName, ipevec);
                }

                //--------------------------------------
                //ADD
                this.ManaDic.Add(index, data);

                index++;
            }
        }


        /// <summary>
        /// 一つのデータ追加
        /// </summary>
        /// <typeparam name="T">Shader Data</typeparam>
        /// <param name="index">追加shader id</param>
        /// <param name="sd">読み込みShader情報</param>
        /// <param name="ipevec">頂点情報 null=デフォルト</param>
        private void AddShaderData<T>(int index, ShaderListData sd, InputElementDescription[] ipevec = null) where T : struct
        {            

            //一つのデータの読みこみ
            ShaderManageData data = null;

            data = this.CreateShaderManageData<T>(sd, ipevec);

            //--------------------------------------
            //ADD
            this.ManaDic.Add(index, data);
        }


        /// <summary>
        /// デフォルト頂点配置の作成
        /// </summary>
        /// <returns></returns>
        private InputElementDescription[] CreateDefailtVertexElement()
        {
            InputElementDescription[] ipevec = {
                                        new InputElementDescription("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                                        new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                                        new InputElementDescription("TEXCOORD", 0, Format.R32G32_Float, 32, 0),
                                    };
            return ipevec;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込みと初期化
        /// </summary>
        /// <param name="filepath">使用するShaderListのファイルパス</param>
        public void CreateResource(string filepath)
        {
            //頂点レイアウト
            //3Dではないので法線は不要。これは将来的に拡張が必要と思われるため、shaderlistfileのオプション化を検討する
            /*InputElement[] ipevec = {
                                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 32, 0),
                                    };*/

            InputElementDescription[] ipevec = this.CreateDefailtVertexElement();
            List<string> flist = new List<string>() { filepath };
            this.CreateResource<ShaderDataDefault>(flist, ipevec);
        }

        /// <summary>
        /// リソースの作成
        /// </summary>
        /// <param name="filepath">入力ShdaerListファイルパス一式</param>
        /// <param name="ipevec">頂点定義</param>
        /// <typeparam name="T">Shader Constant Data</typeparam>
        public void CreateResource<T>(List<string> filepathlist, InputElementDescription[] ipevec) where T : struct
        {
            try
            {
                this.ClearData();


                this.AddResource<T>(filepathlist, ipevec);

                /*
                //シェーダーリストの読み込み
                List<ShaderListFileDataRoot> rdatalist = new List<ShaderListFileDataRoot>();

                filepathlist.ForEach(fpath =>
                {
                    ShaderListFile sfp = new ShaderListFile();
                    ShaderListFileDataRoot rdata = sfp.ReadFile(fpath);
                    rdatalist.Add(rdata);
                });


                foreach (ShaderListFileDataRoot rdata in rdatalist)
                {                    
                    this.LoadShaderListFileData<T>(rdata, ipevec);
                }*/
            }
            catch (Exception e)
            {
                throw new Exception("ShaderManager CreateResource Exception", e);
            }
        }


        /// <summary>
        /// デフォルトShaderの読み込みと作成
        /// </summary>
        /// <param name="srcode"></param>
        /// <param name="rdata"></param>
        internal void CreateDefaultResource( ShaderListFileDataRoot rdata)
        {
            InputElementDescription[] ipevec = this.CreateDefailtVertexElement();
            this.LoadShaderListFileData<ShaderDataDefault>(rdata, ipevec);
        }


        /// <summary>
        /// データクリア
        /// </summary>
        public void ClearData()
        {
            this.ClearManageDic();
        }



        /// <summary>
        /// 追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepathlist"></param>
        /// <param name="ipevec"></param>
        public void AddResource<T>(List<string> filepathlist, InputElementDescription[] ipevec = null) where T : struct
        {
            try
            {
                if (ipevec == null)
                {
                    ipevec = this.CreateDefailtVertexElement();
                }

                //シェーダーリストの読み込み
                List<ShaderListFileDataRoot> rdatalist = new List<ShaderListFileDataRoot>();

                filepathlist.ForEach(fpath =>
                {
                    ShaderListFile sfp = new ShaderListFile();
                    ShaderListFileDataRoot rdata = sfp.ReadFile(fpath);
                    rdatalist.Add(rdata);
                });


                foreach (ShaderListFileDataRoot rdata in rdatalist)
                {
                    this.LoadShaderListFileData<T>(rdata, ipevec);
                }
            }
            catch (Exception e)
            {
                throw new Exception("ShaderManager AddResource", e);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Shaderデータの設定
        /// </summary>
        /// <typeparam name="T">Shaderへの設定データ</typeparam>
        /// <param name="data">設定データ</param>
        /// <param name="sid">Shader番号</param>
        public static void SetShaderData<T>(T data, int sid) where T : unmanaged
        {
            //設定データ取得
            ID3D11DeviceContext cont = DxManager.Mana.DxContext;
            ShaderManageData smana = ShaderManager.Mana.ManaDic[sid];

            //レイアウトの設定
            cont.IASetInputLayout(smana.Layout);

            //シェーダー設定
            cont.VSSetShader(smana.Vs);
            cont.VSSetConstantBuffer(0, smana.ConstantBuffer);
            cont.PSSetShader(smana.Ps);

            //ConstantBufferへ値の設定
            //cont.UpdateSubresource(ref data, smana.ConstantBuffer);
            cont.UpdateSubresource<T>(ref data, smana.ConstantBuffer);


        }

        /// <summary>
        /// デフォルトデータの設定
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sid"></param>
        public static void SetShaderDataDefault(ShaderDataDefault data, int sid)
        {
            ShaderManager.SetShaderData<ShaderDataDefault>(data, sid);
        }
    }
}
