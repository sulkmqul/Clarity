using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;



namespace Clarity.GUI
{
    /// <summary>
    /// マウス情報
    /// </summary>
    public class MouseInfo
    {
        /// <summary>
        /// マウス押されているか可否
        /// </summary>
        public bool DownFlag { get; private set; } = false;

        /// <summary>
        /// 押された時
        /// </summary>
        public Point DownPos { get; private set; } = new Point();

        /// <summary>
        /// 離された時
        /// </summary>
        public Point NowPos { get; private set; } = new Point();

        /// <summary>
        /// 押した位置からの距離
        /// </summary>
        public Point DownLength
        {
            get
            {
                return new Point(this.NowPos.X - this.DownPos.X,
                    this.NowPos.Y - this.DownPos.Y);
            }
        }

        /// <summary>
        /// 記憶値
        /// </summary>
        public Dictionary<int, object> MemoryDic = new Dictionary<int, object>();


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// マウス押した
        /// </summary>
        /// <param name="args"></param>
        public void DownMouse(MouseEventArgs args)
        {
            this.DownFlag = true;
            this.DownPos = new Point(args.X, args.Y);
            this.NowPos = new Point(args.X, args.Y);
        }

        /// <summary>
        /// マウス動いた
        /// </summary>
        /// <param name="args"></param>
        public void MoveMouse(MouseEventArgs args)
        {
            this.NowPos = new Point(args.X, args.Y);
        }

        /// <summary>
        /// マウス離した
        /// </summary>
        /// <param name="args"></param>
        public void UpMouse(MouseEventArgs args)
        {
            this.NowPos = new Point(args.X, args.Y);
            this.DownFlag = false;
        }

        /// <summary>
        /// メモリ値の設定
        /// </summary>
        /// <param name="data"></param>
        /// <param name="slot">記憶場所</param>
        public void SetMemory(object data, int slot = 0)
        {
            this.MemoryDic[slot] = data;
        }

        /// <summary>
        /// メモリ値の取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="slot">記憶場所</param>
        /// <returns></returns>
        public T GetMemory<T>(int slot = 0)
        {
            if (this.MemoryDic.ContainsKey(slot) == false)
            {
                return default(T);
            }

            T data = (T)this.MemoryDic[slot];
            return data;
        }
    }
}
