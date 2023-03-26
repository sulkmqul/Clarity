using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clarity.GUI
{
    /// <summary>
    /// 非同期コントロール制御Statement
    /// </summary>
    public class AsyncControlState : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c">制御中、無効にするコントロール</param>
        /// <param name="envcon">制御中、有効にするコントロール</param>
        /// <param name="ev">即時視覚イベント可否</param>
        public AsyncControlState(Control c, Control? envcon = null, bool ev = true)
        {
            //制御対象管理へ
            this.DisableConList = new List<Control>();
            this.DisableConList.Add(c);

            this.EnableConList = new List<Control>();
            if (envcon != null)
            {
                this.EnableConList.Add(envcon);
            }

            //制御
            this.DisableConList.ForEach(x => x.Enabled = false);
            this.EnableConList.ForEach(x => x.Visible = x.Enabled = true);

            if (ev == true)
            {
                //this.Con.Refresh();
                Application.DoEvents();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="c">制御コントロール一式</param>
        /// <param name="ev">即時視覚イベント可否</param>
        public AsyncControlState(List<Control> clist, List<Control>? dlist = null,bool ev = true)
        {
            this.DisableConList = new List<Control>();
            this.DisableConList.AddRange(clist);

            this.EnableConList = new List<Control>();
            if (dlist != null)
            {
                this.EnableConList.AddRange(dlist);
            }

            this.DisableConList.ForEach(x => x.Enabled = false);
            this.EnableConList.ForEach(x => x.Visible = x.Enabled = true);

            if (ev == true)
            {
                //this.Con.Refresh();
                Application.DoEvents();
            }
        }


        /// <summary>
        /// 制御対象一覧
        /// </summary>
        protected List<Control> DisableConList = new List<Control>();

        /// <summary>
        /// 有効にするコントロール
        /// </summary>
        protected List<Control> EnableConList = new List<Control>();


        public virtual void Dispose()
        {
            this.DisableConList.ForEach(x => x.Enabled = true);
            this.EnableConList.ForEach(x => x.Visible = x.Enabled = false);
        }
    }

    /// <summary>
    /// 待ちアニメーションを表示する非同期エフェクト(仮)
    /// </summary>
    public class AsyncControlStateWait : AsyncControlState
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">待ちアニメコントロールを挿入する親コントロール</param>
        /// <param name="c">無効制御コントロール</param>
        public AsyncControlStateWait(Control parent, Control c) : base(c, null, false)
        {
            this.Parent = parent;

            this.AddEffectImage();
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">待ちアニメコントロールを挿入する親コントロール</param>
        /// <param name="clist">>無効制御コントロール一式</param>
        public AsyncControlStateWait(Control parent, List<Control> clist) : base(clist, null, false)
        {
            this.Parent = parent;

            this.AddEffectImage();

            Application.DoEvents();

        }

        Control Parent;
        PictureBox WaitEffect = new PictureBox();


        private void AddEffectImage()
        {
            this.WaitEffect = new PictureBox();
            //this.WaitEffect.Load(@"waitanime.gif");
            this.WaitEffect.SizeMode = PictureBoxSizeMode.AutoSize;

            //親コントロールの中心に表示する
            this.WaitEffect.Left = this.Parent.ClientSize.Width / 2 - this.WaitEffect.Width / 2;
            this.WaitEffect.Top = this.Parent.ClientSize.Height / 2 - this.WaitEffect.Height / 2;

            this.Parent.Controls.Add(this.WaitEffect);

            //最前面へ
            this.WaitEffect.BringToFront();
        }


        public override void Dispose()
        {
            //対象を削除する
            this.Parent.Controls.Remove(this.WaitEffect);
            this.WaitEffect.Dispose();
            base.Dispose();
        }
    }

    /// <summary>
    /// 画面レイアウトの変更ステートメント
    /// </summary>
    public class LayoutChangingState : IDisposable
    {
        public LayoutChangingState(Control c)
        {
            this.Con = c;
            c.SuspendLayout();
        }
        Control Con;

        public void Dispose()
        {
            this.Con.ResumeLayout();
        }
    }



    
}
