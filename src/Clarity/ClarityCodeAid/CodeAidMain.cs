using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClarityCodeAid.AidProcess;

namespace ClarityCodeAid
{
    /// <summary>
    /// 処理モード
    /// </summary>
    public enum EAidMode
    {
        Texture,
        Vertex,
        Shader,
        Sound,
        ImageMerge,
        TexAnime,

        Max,
    }



    /// <summary>
    /// バッチ処理本体
    /// </summary>
    public class CodeAidMain
    {

        


        /// <summary>
        /// 処理本体
        /// </summary>
        /// <param name="args">起動引数</param>
        public void Run(string[] args)
        {
            try
            {
                //起動引数解析
                InputParam idata = new InputParam();
                idata.AnalyzeParam(args);


                Dictionary<EAidMode, Type> procmap = new Dictionary<EAidMode, Type>();
                procmap.Add(EAidMode.Texture, typeof(TexCodeGenerator));
                procmap.Add(EAidMode.Vertex, typeof(VertexCodeGenerator));
                procmap.Add(EAidMode.Shader, typeof(ShaderCodeGenerator));
                procmap.Add(EAidMode.Sound, typeof(SoundCodeGenerator));
                procmap.Add(EAidMode.ImageMerge, typeof(ImageMerger));
                procmap.Add(EAidMode.TexAnime, typeof(TexAnimeCodeGenerator));


                //起動コード取得
                Type runtype = procmap[idata.AidMode];
                IAidProcess iap = (IAidProcess)Activator.CreateInstance(runtype);
                iap.Proc(idata);

            }
            catch (Exception e)
            {
                throw new Exception("Aid Process Exception", e);
            }
        }
    }
}
