using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Clarity.DLL
{
    public class Winmm
    {
        [DllImport("Winmm.dll")]
        public static extern uint timeBeginPeriod(uint uuPeriod);
    }
}
