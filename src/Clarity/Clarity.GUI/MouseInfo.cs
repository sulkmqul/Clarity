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
        public bool DownFlag
        {
            get
            {
                if (this.DownButton == MouseButtons.None)
                {
                    return false;
                }
                return true;
            }
        }


        /// <summary>
        /// 押されているボタン
        /// </summary>
        public MouseButtons DownButton { get; private set; } = MouseButtons.None;

        /// <summary>
        /// 押された位置
        /// </summary>
        public Point DownPos { get; private set; } = new Point();

        /// <summary>
        ///　現在値
        /// </summary>
        private Point _NowPos = new Point();
        /// <summary>
        /// 現在値
        /// </summary>
        public Point NowPos
        {
            get
            {
                return this._NowPos;
            }
            private set
            {
                this.PrevPos = this._NowPos;
                this._NowPos = value;
            }
        }

        /// <summary>
        /// 前回の位置
        /// </summary>
        public Point PrevPos { get; private set; } = new Point();


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
        /// 前回から動いた距離
        /// </summary>
        public Point PrevMoveLength
        {
            get
            {
                return new Point(this.NowPos.X - this.PrevPos.X,
                    this.NowPos.Y - this.PrevPos.Y);
            }
        }



        /// <summary>
        /// 記憶値
        /// </summary>
        protected Dictionary<int, object> MemoryDic = new Dictionary<int, object>();


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// マウス押した
        /// </summary>
        /// <param name="args"></param>
        public void DownMouse(MouseEventArgs args)
        {
            this.DownButton = args.Button;
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
            this.DownButton = MouseButtons.None;
        }
        /// <summary>
        /// 位置の更新
        /// </summary>
        /// <param name="args"></param>
        public void UpdatePositon(MouseEventArgs args)
        {
            this.NowPos = new Point(args.X, args.Y);
        }

        /// <summary>
        /// マウスホイール
        /// </summary>
        /// <param name="args"></param>
        public void WheelMouse(MouseEventArgs args)
        {
            this.NowPos = new Point(args.X, args.Y);            
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
        public T? GetMemory<T>(int slot = 0)
        {
            if (this.MemoryDic.ContainsKey(slot) == false)
            {
                return default;
            }

            T data = (T)this.MemoryDic[slot];
            return data;
        }
    }
}
