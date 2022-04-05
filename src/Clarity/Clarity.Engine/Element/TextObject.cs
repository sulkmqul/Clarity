using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vortice.DirectWrite;

namespace Clarity.Engine.Element
{
    /// <summary>
    /// Direct2D描画基底所作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class D2DRenderingBehavior<T> : BaseModelBehavior<T> where T : BaseElement
    {
        public override void Execute(object beo)
        {
            try
            {
                Core.DxManager.Mana.CurrentTarget2D.BeginDraw();
                base.Execute(beo);
            }
            finally
            {
                Core.DxManager.Mana.CurrentTarget2D.EndDraw();
            }
        }
    }

    /// <summary>
    /// テキスト描画所作
    /// </summary>
    internal class TextRenderingBehavior : D2DRenderingBehavior<TextObject>
    {
        protected override void ExecuteBehavior(TextObject obj)
        {
            //表示領域の確定
            System.Drawing.RectangleF rc = new System.Drawing.RectangleF(new System.Drawing.PointF(obj.TransSet.PosX, obj.TransSet.PosY), obj.ClipSize);

            //描画処理
            var rtarget = Core.DxManager.Mana.CurrentTarget2D;
            using (var bru = rtarget.CreateSolidColorBrush(obj.Color))
            {
                rtarget.DrawText(obj.Text, obj.TFormat, rc, bru);
            }
        }
    }

    /// <summary>
    /// テキスト描画オブジェクト
    /// </summary>
    public class TextObject :  ClarityObject
    {
        public TextObject(string s, long oid = 0, string fontname = "Arial", float size = 25.0f, int maxline = 8) : base(oid)
        {
            this.Lines = Enumerable.Repeat<string>("", maxline).ToList();
            this.Text = s;
            this.TFormat = Core.DxManager.Mana.FactDWrite.CreateTextFormat(fontname, size);
            this.Color = new Vortice.Mathematics.Color4(1.0f, 0.0f, 0.0f, 1.0f);

            this.RenderBehavior = new TextRenderingBehavior();
        }

        /// <summary>
        /// 表示テキスト
        /// </summary>
        public virtual string Text
        {
            get
            {
                string ans = "";
                this.Lines.ForEach(x => ans += (x + System.Environment.NewLine));
                return ans;
            }
            set
            {
                this.Lines[0] = value;
            }
        }


        /// <summary>
        /// これの行数
        /// </summary>
        protected List<string> Lines = null;


        /// <summary>
        /// 文字表示領域サイズ
        /// </summary>
        public System.Drawing.Size ClipSize = new System.Drawing.Size(10000, 10000);

        /// <summary>
        /// 表示テキストフォーマット
        /// </summary>
        public IDWriteTextFormat TFormat { get; protected set; } = null;

        /// <summary>
        /// 文字色
        /// </summary>
        public Vortice.Mathematics.Color4 Color { get; set; }

        /// <summary>
        /// 行の設定
        /// </summary>
        /// <param name="s"></param>
        /// <param name="index"></param>
        public void SetText(string s, int index = 0)
        {
            this.Lines[index] = s;
        }
    }
}
