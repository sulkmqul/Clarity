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
        public ID3D11VertexShader? Vs = null;

        /// <summary>
        /// PixelShader
        /// </summary>
        public ID3D11PixelShader? Ps = null;

        /// <summary>
        /// 入力レイアウト設定
        /// </summary>
        public ID3D11InputLayout? Layout = null;

        /// <summary>
        /// これシェーダーで使用するContantBuffer(VertexShaderに送るデータ領域)
        /// </summary>
        public ID3D11Buffer? ConstantBuffer = null;

        public void Dispose()
        {
            this.Vs?.Dispose();
            this.Vs = null;
            this.Ps?.Dispose();
            this.Ps = null;
            this.Layout?.Dispose();
            this.Layout = null;
            this.ConstantBuffer?.Dispose();
            this.ConstantBuffer = null;
        }
    }



    /// <summary>
    /// Shader管理規定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ShaderManager : BaseClarityFactroy<ShaderManager, ShaderManageData>
    {
        private ShaderManager()
        {
        }

        /// <summary>
        /// 頂点ShaderVersion
        /// </summary>
        private string VertexShaderProfile
        {
            get
            {
                return ClarityEngine.EngineSetting.GetString("VertexShaderVersion");
            }
        }

        /// <summary>
        /// PixelShaderVersion
        /// </summary>
        private string PixelShaderProfile
        {
            get
            {
                return ClarityEngine.EngineSetting.GetString("PixelShaderVersion");
            }
        }

        /// <summary>
        /// 作成関数
        /// </summary>
        public static void Create()
        {
            Instance = new ShaderManager();
        }
        /// <summary>
        /// Shaderデータの設定
        /// </summary>
        /// <typeparam name="T">Shaderへの設定データの型(constant bufferの型)</typeparam>
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

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// Shaderの追加(デフォルト版)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcdata"></param>
        internal void AddResource(ShaderSrcData srcdata)
        {
            //デフォルトの頂点配置を採用する
            InputElementDescription[] ipvec = this.CreateDefaultVertexElement();
            this.AddResource<ShaderDataDefault>(srcdata, ipvec);
        }
        /// <summary>
        /// Shaderの追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcdata"></param>
        public void AddResource<T>(ShaderSrcData srcdata) where T : struct
        {
            //デフォルトの頂点配置を採用する
            InputElementDescription[] ipvec = this.CreateDefaultVertexElement();
            this.AddResource<T>(srcdata, ipvec);
        }

        /// <summary>
        /// Shaderの追加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="srcdata"></param>
        /// <param name="ipvec"></param>
        public void AddResource<T>(ShaderSrcData srcdata, InputElementDescription[] ipvec) where T : struct
        {
            //読込
            ShaderManageData mdata = this.CreateShaderManageData<T>(srcdata, ipvec);
            //管理追加
            this.ManaDic.Add(srcdata.Id, mdata);
        }


        /// <summary>
        /// ShaderListファイルからリソースを作成する。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath">読込ファイル「</param>
        /// <param name="ipvec">頂点レイアウト null=デフォルト使用</param>
        public async Task AddResourceFromShaderList<T>(string filepath, InputElementDescription[]? ipvec) where T : struct
        {
            if(ipvec == null)
            {
                ipvec = this.CreateDefaultVertexElement();
            }

            ShaderListFile fp = new ShaderListFile();
            ShaderListFileDataRoot root = await fp.ReadFile(filepath);

            root.ShaderList.ForEach(x =>
            {
                this.AddResource<T>(x, ipvec);
            });
        }

        /// <summary>
        /// データクリア
        /// </summary>
        public void ClearData()
        {
            this.ClearManageDic();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// デフォルト頂点配置の作成
        /// </summary>
        /// <returns></returns>
        private InputElementDescription[] CreateDefaultVertexElement()
        {
            InputElementDescription[] ipevec = {
                                        new InputElementDescription("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                                        new InputElementDescription("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                                        new InputElementDescription("TEXCOORD", 0, Format.R32G32_Float, 32, 0),
                                    };
            return ipevec;
        }


        /// <summary>
        /// Shader管理データの作成
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sd">読込情報</param>
        /// <param name="ipevec">頂点情報</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private ShaderManageData CreateShaderManageData<T>(ShaderSrcData sd, InputElementDescription[] ipevec) where T : struct
        {
            ShaderManageData data = new ShaderManageData();

            //VertexShaderのコンパイル
            var vs = this.CompileVertexShader(sd.SrcCode, sd.VsName, ipevec);
            data.Vs = vs.vs;
            data.Layout = vs.ivl;

            //PixelShader
            data.Ps = this.CompilePixelShader(sd.SrcCode, sd.PsName);
            if (data.Ps == null)
            {
                throw new Exception("CompilePixelShader Exception");
            }

            //コンスタントバッファの作成
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
        /// VertexShaderのコンパイル
        /// </summary>
        /// <param name="srcode">ソースコード</param>
        /// <param name="funcname">使用関数名</param>
        /// <param name="ipevec">頂点レイアウト</param>        
        /// <returns></returns>
        protected (ID3D11VertexShader vs, ID3D11InputLayout ivl) CompileVertexShader(string srcode, string funcname, InputElementDescription[] ipevec)
        {
            ID3D11VertexShader ans;
            ID3D11InputLayout anslayout;
            try
            {
                Vortice.Direct3D.Blob blob, erblob;
                var cpret = Vortice.D3DCompiler.Compiler.Compile(srcode, funcname, "unknown", this.VertexShaderProfile, out blob, out erblob);
                if (cpret != SharpGen.Runtime.Result.Ok)
                {
                    throw new Exception($"{erblob.ConvertToString()}");
                }

                //VSResourceの作成
                ans = DxManager.Mana.DxDevice.CreateVertexShader(blob);

                //頂点レイアウト作成
                anslayout = DxManager.Mana.DxDevice.CreateInputLayout(ipevec, blob);
            }
            catch (Exception ex)
            {
                throw new Exception("Crate VertexShader", ex);
            }

            return (ans, anslayout);
        }

        /// <summary>
        /// PixelShaderのコンパイル
        /// </summary>
        /// <param name="srccode">コンパイルソースコード</param>
        /// <param name="funcname">関数名</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected ID3D11PixelShader CompilePixelShader(string srccode, string funcname)
        {
            ID3D11PixelShader ans;
            try
            {

                Vortice.Direct3D.Blob blob, erblob;
                var cpret = Vortice.D3DCompiler.Compiler.Compile(srccode, funcname, "unknown", this.PixelShaderProfile, out blob, out erblob);
                if(cpret != SharpGen.Runtime.Result.Ok)
                {
                    throw new Exception($"{erblob.ConvertToString()}");
                }

                ans = DxManager.Mana.DxDevice.CreatePixelShader(blob);
                
            }
            catch (Exception ex)
            {
                throw new Exception("Pixel Shader Compile", ex);
            }

            return ans;
        }
    }
}
