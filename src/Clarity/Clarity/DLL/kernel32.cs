using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Zmgbl
{
    /// <summary>
    /// kernel32.dll
    /// </summary>
    public class kernel32
    {
        private const string DllPath = "kernel32.dll";

        [DllImport(DllPath)]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);


        [DllImport(DllPath)]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);



        [DllImport(DllPath)]
        public static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

    }
}
