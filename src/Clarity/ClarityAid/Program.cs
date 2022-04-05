using System;


namespace ClarityAid
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
        TextureAnime,
        Structure,
        ClaritySetting,

        //-----------
        Max,
    }

    

    /// <summary>
    /// Clarityコード補助
    /// </summary>
    class Program
    {
        /// <summary>
        /// エントリポイント
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
#if DEBUG
                #region デバッグ情報作成

                string[] arg = {
                    "cs",
                    "-i",
                    @"F:\作業領域\Game\Stellamaris\src\Stellamaris\Stellamaris\cs.xml",                    
                    "-o",
                    @"C:\Users\alk\Desktop\一時作業\新しいフォルダー",
                    
                };

                args = arg;

                #endregion
#endif
                

                //パラメータの解析
                ArgParam param = new ArgParam();
                bool ret = param.Analyze(args);
                if (ret == false)
                {
                    //パラメータ解析に失敗したら使い方を表示して終了
                    DisplayUsage();
                    return;
                }
                //-------------------------------------                
                Console.WriteLine($"CodeAid mode={param.Mode} start");
                {
                    BatchMain batch = new BatchMain();
                    batch.Run(param);
                }
                Console.WriteLine($"CodeAid mode={param.Mode} succsess!!");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CodeAid Exception mes={ex.Message}");
                Exception ie = ex.InnerException;
                while (ie != null)
                {
                    Console.WriteLine($"\tmes={ie.Message}");
                    ie = ie.InnerException;
                }
            }


            //Clarity.Engine.Element.ClarityObject obj = new Clarity.Engine.Element.ClarityObject(0);

            

        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 使い方の表示
        /// </summary>
        private static void DisplayUsage()
        {
            string help = @"
Clarity aid batch
Usage:
    ClarityAid [mode] ....        
    [mode]
        es : Create Structure Code.
            [required]
                -i : Clarity structure filepath
            [Option]
                -o : Output directory path.

        te : Create texture code.
            [required]

            [Option]
                -i : Clarity texture list filepath(-i multiple)
                -o : Output directory path.
        
        ve : Create Vertex code.
            [required]

            [Option]
                -i : Clarity polygon list filepath(-i multiple)
                -o : Output directory path.
        
        sh : Create Shader code.
            [required]
                -i : Clarity shader list filepath(-i multiple)
            [Option]
                -o : Output directory path.

        im : Create merge image
            [required]
                -i : Input directory path.
                -k : match extension(*.jpg  *.ping  *.bmp)
                -o : Output directory path
            [Option]
                -x : limit of columns 
                -y : limit of rows

        cs : Create Setting Code
            [required]
                -i : Input clarity setting file path
            [Option]                
                -o : Output directory path

";
            Console.WriteLine(help);


            Clarity.Engine.RendererSet momo = new Clarity.Engine.RendererSet();
            
        }
    }




    public static class TetsSpace
    {
        public enum ESettingCode
        {
        }

        
    }



}
