using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Core
{
    /// <summary>
    /// システム文字描画
    /// </summary>
    internal class SystemText : Element.TextObject
    {
        public static readonly int SlotFPS = 0;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fontsize">文字サイズ</param>
        /// <param name="col">文字色</param>
        /// <param name="lines">文字行数</param>
        public SystemText(float fontsize, SharpDX.Color col, int lines = 1) : base("", 0, "Arial", fontsize)
        {
            this.Color = col;
            this.DataList = new List<string>();
            for (int i = 0; i < lines; i++)
            {
                this.DataList.Add("");
            }

            //サイズ設定
            int w = ClarityEngine.Setting.RenderingViewSize.Width;
            int h = ClarityEngine.Setting.RenderingViewSize.Height;
            this.ClipSize = new System.Drawing.Size(w, h);
        }

        /// <summary>
        /// 行数管理用
        /// </summary>
        List<string> DataList = new List<string>();

        /// <summary>
        /// 描画文字列の取得
        /// </summary>
        public override string Text
        {
            get
            {
                string ans = "";
                this.DataList.ForEach(x =>
                {
                    ans += x;
                    //ans += System.Environment.NewLine;
                });
                return ans;
            }
               
        }


        /// <summary>
        /// 値の設定
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="s"></param>
        public void SetText(int slot, string s)
        {
            this.DataList[slot] = s;
        }

        /// <summary>
        /// FPSの設定
        /// </summary>
        /// <param name="s"></param>
        public void SetFPS(string s)
        {
            this.SetText(SlotFPS, s);
        }

        protected override void RenderElement()
        {
            base.RenderElement();
        }
    }
}
