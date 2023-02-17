using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//ClarityEngineはInternal扱いとする
[assembly: InternalsVisibleTo("Clarity.Engine")]
[assembly: InternalsVisibleTo("ClarityAid")]


namespace Clarity
{
    class common
    {
    }

    
    /// <summary>
    /// 横方向のみのサイズを定義
    /// </summary>
    public class HorizontalSize
    {
        public HorizontalSize(int l, int r)
        {
            if (r < l)
            {
                throw new ArgumentException("left must be less than right.");
            }

            this._left = l;
            this._right = r;
        }

        private int _left = 0;
        private int _right = 0;


        public int Left
        {
            get
            {
                return this._left;
            }
            set
            {
                int a = Math.Min(value, this.Right);
                this._left = a;
            }
        }

        public int Right
        {
            get
            {
                return this._right;
            }
            set
            {
                int a = Math.Max(value, this.Left);
                this.Right = a;
            }
        }

        /// <summary>
        /// 横幅
        /// </summary>
        public int Width
        {
            get
            {
                return this.Right - this.Left;
            }
        }
    }

    /// <summary>
    /// 縦方向のサイズを定義
    /// </summary>
    public class VerticalSize
    {
        public VerticalSize(int t, int b)
        {
            if (b < t)
            {
                throw new ArgumentException("top must be less than bottom.");
            }

            this._top = t;
            this._bottom = b;
        }

        private int _top = 0;
        private int _bottom = 0;


        public int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                int a = Math.Min(value, this._bottom);
                this._top = a;
            }
        }

        public int Bottom
        {
            get
            {
                return this._bottom;
            }
            set
            {
                int a = Math.Max(value, this.Top);
                this._bottom = a;
            }
        }

        /// <summary>
        /// 縦幅
        /// </summary>
        public int Height
        {
            get
            {
                return this.Bottom - this.Top;
            }
        }
    }
}
