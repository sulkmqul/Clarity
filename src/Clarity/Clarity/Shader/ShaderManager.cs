using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.D3DCompiler;
using SharpDX.DXGI;

using Clarity.Core;

namespace Clarity.Shader
{
    /// <summary>
    /// シェーダーデータ
    /// </summary>
    public class ShaderManageData : IDisposable
    {
        /// <summary>
        /// VertexShaer
        /// </summary>
        public VertexShader Vs = null;

        /// <summary>
        /// PixelShader
        /// </summary>
        public PixelShader Ps = null;

        /// <summary>
        /// 入力レイアウト設定
        /// </summary>
        public InputLayout Layout = null;

        /// <summary>
        /// これシェーダーで使用するContantBuffer(VertexShaderに送るデータ領域)
        /// </summary>
        public SharpDX.Direct3D11.Buffer ConstantBuffer = null;

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

        public static void Create()
        {
            Instance = new ShaderManager();
        }


        
        //==========================================================================================


        /// <summary>
        /// VertexShaderのコンパイル
        /// </summary>
        /// <param name="filename">シェーダーファイル名</param>
        /// <param name="funcname">関数名</param>
        /// <param name="ipl">頂点レイアウト</param>
        /// <returns></returns>
        protected VertexShader CompileVertexShaderFile(string filename, string funcname, InputElement[] ipevec, out InputLayout ipl)
        {
            VertexShader ans = null;

            ipl = null;
            try
            {
                //コンパイル                
                var code = ShaderBytecode.CompileFromFile(filename, funcname, "vs_4_0");
                if (code == null)
                {
                    throw new Exception("CompileFromFile NULL");
                }
                if (code.Bytecode == null)
                {
                    throw new Exception(code.Message);
                }

                //頂点レイアウトを作製
                ShaderSignature sig = ShaderSignature.GetInputSignature(code);
                ipl = new InputLayout(DxManager.Mana.DxDevice, sig, ipevec);


                //作製
                ans = new VertexShader(DxManager.Mana.DxDevice, code);
            }
            catch (Exception e)
            {
                throw e;
            }

            return ans;
        }

        /// <summary>
        /// PixelShaderのコンパイル
        /// </summary>
        /// <param name="filename">シェーダーファイル名</param>
        /// <param name="funcname">関数名</param>
        /// <returns></returns>
        protected PixelShader CompilePixelShaderFile(string filename, string funcname)
        {
            PixelShader ans = null;
            try
            {
                //コンパイル                
                var code = ShaderBytecode.CompileFromFile(filename, funcname, "ps_4_0");
                if (code == null)
                {
                    throw new Exception(code.Message);
                }

                //作製
                ans = new PixelShader(DxManager.Mana.DxDevice, code);

            }
            catch (Exception e)
            {
                throw e;
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
        private ShaderManageData CreateShaderManageData<T>(ShaderListData sd, InputElement[] ipevec) where T : struct
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
            data.ConstantBuffer = new SharpDX.Direct3D11.Buffer(DxManager.Mana.DxDevice, Utilities.SizeOf<T>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0);

            return data;
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
            InputElement[] ipevec = {
                                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0),
                                        new InputElement("TEXCOORD", 0, Format.R32G32_Float, 32, 0),
                                    };

            List<string> flist = new List<string>() { filepath };
            this.CreateResource<ShaderDataDefault>(flist, ipevec);
        }

        /// <summary>
        /// リソースの作成
        /// </summary>
        /// <param name="filepath">入力バッファ</param>
        /// <param name="ipevec">頂点定義</param>
        /// <typeparam name="T">Shader Constant Data</typeparam>
        public void CreateResource<T>(List<string> filepathlist, InputElement[] ipevec) where T : struct
        {
            try
            {
                this.ClearData();


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
                    //シェーダーのcompileと読み込み
                    int index = rdata.RootID;
                    foreach (ShaderListData sd in rdata.ShaderList)
                    {
                        //一つのデータの読みこみ
                        ShaderManageData data = this.CreateShaderManageData<T>(sd, ipevec);

                        //--------------------------------------
                        //ADD
                        this.ManaDic.Add(index, data);

                        index++;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("ShaderManager CreateResource Exception", e);
            }
        }


        /// <summary>
        /// データクリア
        /// </summary>
        public void ClearData()
        {
            this.ClearManageDic();
        }


        ////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Shaderデータの設定
        /// </summary>
        /// <typeparam name="T">Shaderへの設定データ</typeparam>
        /// <param name="data">設定データ</param>
        /// <param name="sid">Shader番号</param>
        public static void SetShaderData<T>(T data, int sid) where T : struct
        {
            //設定データ取得
            DeviceContext cont = DxManager.Mana.DxDevice.ImmediateContext;
            ShaderManageData smana = ShaderManager.Mana.ManaDic[sid];

            //レイアウトの設定
            cont.InputAssembler.InputLayout = smana.Layout;

            //シェーダー設定
            cont.VertexShader.Set(smana.Vs);
            cont.VertexShader.SetConstantBuffer(0, smana.ConstantBuffer);
            cont.PixelShader.Set(smana.Ps);

            //ConstantBufferへ値の設定
            cont.UpdateSubresource(ref data, smana.ConstantBuffer);
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
