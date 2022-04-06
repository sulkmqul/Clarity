using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClarityAid.AidProcess;

namespace ClarityAid
{
    
    /// <summary>
    /// 処理本体
    /// </summary>
    class BatchMain
    {
        /// <summary>
        /// バッチの実行
        /// </summary>
        /// <param name="param"></param>
        public void Run(ArgParam param)
        {
            //処理の実行
            IAidProcess proc = this.CreateModeProc(param.Mode);
            proc.Proc(param);

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 処理モードに沿った処理クラスの作成
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        private IAidProcess CreateModeProc(EAidMode mode, int m = 0)
        {

            //起動物一式のsetを作成
            Dictionary<EAidMode, Type> dic = new Dictionary<EAidMode, Type>();
            {
                dic.Add(EAidMode.Texture, typeof(TextureCodeGenerator));    //texture
                dic.Add(EAidMode.TextureAnime, typeof(TextureAnimeCodeGenerator));  //テクスチャアニメ
                dic.Add(EAidMode.Shader, typeof(ShaderCodeGenerator));    //Shader
                dic.Add(EAidMode.Structure, typeof(StructureCodeGenerator));    //構造
                dic.Add(EAidMode.Vertex, typeof(VertexCodeGenerator));    //Vertex
                dic.Add(EAidMode.ClaritySetting, typeof(UserSettingCodeGenerator));    //設定値
                

                dic.Add(EAidMode.ImageMerge, typeof(ImageMerge));    //画像連結
                

            }

            //ちゃんと定義してある？
            if (dic.ContainsKey(mode) == false)
            {
                throw new Exception($"{mode} mode process is not Created!");
            }

            //処理物を作成
            IAidProcess ans = (IAidProcess)Activator.CreateInstance(dic[mode]);
            return ans;
        }


    }
}
