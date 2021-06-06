using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// Direct2D管理のObject
    /// </summary>
    public abstract class BaseElementD2D : BaseElement
    {
        public BaseElementD2D(long oid) : base(oid)
        {        
        }


        /// <summary>
        /// 描画対象
        /// </summary>
        protected internal SharpDX.Direct2D1.RenderTarget CurrentRenderTarget = null;


        public SharpDX.Vector2 Pos2D
        {
            get
            {
                return this.TransSet.Pos2D;
            }
            set
            {
                this.TransSet.Pos2D = value;
            }
        }


        public float PosX
        {
            get
            {
                return this.TransSet.PosX;
            }
            set
            {
                this.TransSet.PosX = value;
            }
        }


        public float PosY
        {
            get
            {
                return this.TransSet.PosY;
            }
            set
            {
                this.TransSet.PosY = value;
            }
        }


        /// <summary>
        /// 色の設定
        /// </summary>
        public new SharpDX.Color Color
        {
            get;
            set;

        }


        /// <summary>
        /// 色の設定
        /// </summary>
        public new byte ColorAlpha
        {
            get
            {
                return this.Color.A;
            }
        }

        /// <summary>
        /// 2Dの描画
        /// </summary>
        /// <param name="rparam"></param>
        internal override void Render(FrameRenderParam rparam)
        {
            this.FrameInfo.RenderViewIndex = rparam.ViewIndex;
            this.FrameInfo.RenderIndex = rparam.RenderIndex;
            this.CurrentRenderTarget = rparam.Crt;

            this.RenderElement();
        }
    }

    

    /// <summary>
    /// テキスト表示オブジェクト
    /// </summary>
    public class TextObject : BaseElementD2D
    {   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s">表示文字列</param>
        /// <param name="oid">階層位置</param>
        /// <param name="fontname">フォント名</param>
        /// <param name="size">フォントサイズ</param>
        public TextObject(string s, long oid = 0, string fontname = "Arial", float size = 25.0f) : base(oid)
        {
            this.Text = s;
            this.TFormat = new SharpDX.DirectWrite.TextFormat(Clarity.Core.DxManager.Mana.FactDWrite, fontname, size);
            this.Color = SharpDX.Color.Black;
        }


        /// <summary>
        /// 表示テキスト
        /// </summary>
        public virtual string Text
        {
            get; set;
        }

        /// <summary>
        /// 文字表示領域サイズ
        /// </summary>
        public System.Drawing.Size ClipSize = new System.Drawing.Size(10000, 10000);
        

        /// <summary>
        /// 表示テキストフォーマット
        /// </summary>
        private SharpDX.DirectWrite.TextFormat TFormat = null;


        /// <summary>
        /// 初期化処理
        /// </summary>
        protected sealed override void InitElement()
        {
            
        }

        /// <summary>
        /// 処理
        /// </summary>
        protected sealed override void ProcElement()
        {
            
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        protected override void RenderElement()
        {
            Core.DxManager.Mana.CurrentTarget2D.BeginDraw();
            using (SharpDX.Direct2D1.SolidColorBrush sol = new SharpDX.Direct2D1.SolidColorBrush(this.CurrentRenderTarget, this.Color))
            {
                
                Core.DxManager.Mana.CurrentTarget2D.DrawText(this.Text, this.TFormat, new SharpDX.Mathematics.Interop.RawRectangleF(this.TransSet.PosX, this.TransSet.PosY, this.ClipSize.Width, this.ClipSize.Height), sol);
                
            }
            Core.DxManager.Mana.CurrentTarget2D.EndDraw();
        }

        
    }




    /// <summary>
    /// 複数行テキスト表示オブジェクト
    /// </summary>
    public class MultiTextObject : TextObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fontsize">文字サイズ</param>
        /// <param name="col">文字色</param>
        /// <param name="lines">文字行数</param>
        public MultiTextObject(float fontsize, SharpDX.Color col, int lines = 1) : base("", 0, "Arial", fontsize)
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
                    ans += System.Environment.NewLine;
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
    }

}
