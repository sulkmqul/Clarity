using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityCodeAid
{
    /// <summary>
    /// リソースファイルリストからコードで使用できるデータを作成するコマンドラインバッチ処理
    /// </summary>
    class Program
    {
        /// <summary>
        /// 使い方の表示
        /// </summary>
        private static void DisplayHelp()
        {
            string help = @"
Usage:
    ClarityCodeAid [mode] -i [InputPath]... -k [SearhKey] -o [OutputDirectory]
    -i : Input File Pathes or File Directory
    -k : Search Extension Key
    -o : Output Directory 

    [mode]
        te = Generate Texture Code.
        ve = Generate Vetex Code.
        sh = Generate Shader Code.
        so = Generate Sound Code.
        ta = Generate Texture Anime Code.
        im = Image Merge.
";
            Console.Write(help);
        }

        
        /// <summary>
        /// エントリポイント
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            //デバッグでないなら起動引数が渡される
#if DEBUG
            /*string[] testarg = {
                "te",
                "-i",
                @"F:\作業領域\Game\Clarity\src\Clarity\sapmlefile\TextureListFile.txt",                
                "-k",
                "*.txt",
                "-o",
                @"F:\作業領域\Game\Clarity\src\Clarity\sapmlefile",
                
            };*/
            /*
            string[] testarg = {
                "ta",
                "-i",
                @"F:\作業領域\Game\Clarity\src\Clarity\sapmlefile\AnimeList.txt",
                "-k",
                "*.txt",
                "-o",
                @"F:\作業領域\Game\Clarity\src\Clarity\sapmlefile",

            };*/
            string[] testarg = {
                "im",
                "-i",
                @"F:\作業領域\Game\Clarity\src\Clarity\sapmlefile\",
                "-k",
                "*.png",
                "-o",
                @"F:\作業領域\Game\Clarity\src\Clarity\sapmlefile",

            };

            args = testarg;

#endif
            bool ret = false;
            try
            {

                //ログの初期化
                Clarity.ClarityLog.Init(Clarity.EClarityLogLevel.Info, Clarity.ELogMode.Console);

                //処理本体
                CodeAidMain m = new CodeAidMain();
                m.Run(args);

                ret = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Aid Process Exception mes={0}", e.InnerException.Message));
            }

            Console.WriteLine("Aid Process End. result=" + ret.ToString());
            
        }
    }
}
