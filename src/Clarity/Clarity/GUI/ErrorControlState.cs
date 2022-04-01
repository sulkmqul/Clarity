using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Clarity.GUI
{
    /// <summary>
    /// エラーコントロール表示
    /// </summary>

    public class ErrorControlState : IDisposable
    {
        public ErrorControlState(List<Control> clist, bool f = true)
        {
            this.ControlList = new List<Control>();
            this.ControlList.AddRange(clist);

            //復帰色データ作成
            this.DefaultColorDic = this.CreateDefaultColorDic(clist);

            if (f == true)
            {
                this.ChangeColor();

            }
        }

        /// <summary>
        /// エラー表示色の設定
        /// </summary>
        public System.Drawing.Color ErrorColor { get; set; } = System.Drawing.Color.Pink;

        /// <summary>
        /// 制御一覧
        /// </summary>
        private List<Control> ControlList;


        /// <summary>
        /// 元色保持
        /// </summary>
        private Dictionary<Control, Color> DefaultColorDic;


        public void Dispose()
        {
            this.RestoreColor();
        }


        /// <summary>
        /// 色変更
        /// </summary>
        /// <param name="f">trueの場合即時反映</param>
        public void ChangeColor(bool f = true)
        {
            this.ControlList.ForEach(x =>
            {
                x.BackColor = this.ErrorColor;
            });

            if (f == true)
            {
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 色のリセット
        /// </summary>
        private void RestoreColor()
        {
            this.ControlList.ForEach(x =>
            {
                x.BackColor = this.DefaultColorDic[x];
            });
        }

        /// <summary>
        /// 元色復帰を作成
        /// </summary>
        /// <param name="clist"></param>
        /// <returns></returns>
        private Dictionary<Control, Color> CreateDefaultColorDic(List<Control> clist)
        {
            Dictionary<Control, Color> ansdic = new Dictionary<Control, Color>();

            clist.ForEach(x =>
            {
                ansdic.Add(x, x.BackColor);
            });

            return ansdic;
        }
    }
}
