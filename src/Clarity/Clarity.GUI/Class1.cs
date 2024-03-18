using System.Diagnostics;

namespace Clarity.GUI
{
    public class Class1
    {

    }


    public class GuiUitl
    {
        /// <summary>
        /// タスクの終了を同期で待つ
        /// </summary>
        /// <param name="t">待つタスク</param>
        /// <param name="tms">タイムアウト(ms)</param>
        /// <returns>true=正常 false=timeout</returns>
        public static bool WaitTaskEndSync(Task t, long tms = long.MaxValue)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (t.IsCompleted != true)
            {
                Application.DoEvents();
                Thread.Sleep(5);

                if(sw.ElapsedMilliseconds > tms)
                {
                    return false;
                }
            }

            return true;
        }
    }
}