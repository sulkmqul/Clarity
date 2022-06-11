using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity.DLL;
using System.Windows.Forms;

namespace Clarity
{
    /// <summary>
    /// メッセージループ処理クラス
    /// </summary>
    public class ClarityLoop
    {
        private ClarityLoop()
        {

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 実行関数
        /// </summary>
        /// <param name="con">管理Control</param>
        /// <param name="ac">コールバックアクション</param>
        public static void Run(Control con, Action ac)
        {
            bool loopflag = true;

            //削除時の終了通知
            con.Disposed += (con, sender)=> {
                loopflag = false;
            };

            IntPtr mmp = con.Handle;
            con.Show();
            while (loopflag)
            {
                
                tagMSG msg;
                while (User32.PeekMessage(out msg, con.Handle, 0, 0, User32.PM_REMOVE) == WinDef.TRUE)                
                {
                    User32.TranslateMessage(ref msg);
                    User32.DispatchMessage(ref msg);

                    //WM_QUITはvortice WM_NCDESTROYはSharpDXで受けているID
                    if (msg.message == WindowsMessageCode.WM_QUIT || msg.message == WindowsMessageCode.WM_NCDESTROY)
                    {
                        loopflag = false;
                        break;
                    }
                }

                //起動
                ac();
            }
                       

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

    }
}
