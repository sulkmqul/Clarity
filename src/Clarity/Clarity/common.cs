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
    /// <summary>
    /// Enumに文字列を付加するためのAttributeクラス
    /// </summary>
    public class StringAttribute : Attribute
    {
        /// <summary>
        /// データ
        /// </summary>
        public string Data { get; protected set; }

        /// <summary>
        /// 値の設定
        /// </summary>
        /// <param name="value"></param>
        public StringAttribute(string value)
        {
            this.Data = value;
        }
    }

    public static class common
    {
        /// <summary>
        /// Enumの追記情報を取得する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetAttributeString(this Enum value)
        {
            
            //対象型の情報取得
            Type type = value.GetType();
            System.Reflection.FieldInfo? fieldInfo = type.GetField(value.ToString());            

            //範囲外の値チェック
            if (fieldInfo == null)
            {
                return null;
            }
            //対象のアトリビュート取得
            StringAttribute[]? abvec = fieldInfo.GetCustomAttributes(typeof(StringAttribute), false) as StringAttribute[];
            if(abvec == null)
            {
                return null;
            }
            if (abvec.Length <= 0)
            {
                return null;
            }

            return abvec[0].Data;
        }
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
