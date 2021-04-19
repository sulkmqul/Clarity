using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

using Clarity;

namespace ClarityIntensity

{
    /// <summary>
    /// ClarityEngineテストアプリケーションです。
    /// </summary>
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                MainForm mf = new MainForm();                
                
                
                //エンジン初期化
                EngineSetupOption setop = new EngineSetupOption();
                Clarity.ClarityEngine.Init(mf, setop);

                //実行開始
                IntensityMain im = new IntensityMain();                
                EngineRunOption runop = new EngineRunOption();
                Clarity.ClarityEngine.Run(im, runop);
                
                
            }
            catch (Exception e)
            {
                Clarity.ClarityLog.WriteErrorException(e);
            }
                 
        }
    }
}
