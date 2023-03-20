using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization;

namespace Clarity.GUI
{
    /// <summary>
    /// 入力不正
    /// </summary>
    public class InvaildInputException : Exception
    {
        public InvaildInputException(string message, params Control[] convec) : base(message)
        {
            this.TargetList.AddRange(convec);
        }
        public InvaildInputException(string message, List<Control> clist) : base(message)
        {
            this.TargetList.AddRange(clist);
        }
        public InvaildInputException(string message, Exception innerException, params Control[] convec) : base(message, innerException)
        {
            this.TargetList.AddRange(convec);
        }

        public InvaildInputException(string message, Exception innerException, List<Control> clist) : base(message, innerException)
        {
            this.TargetList.AddRange(clist);
        }
        protected InvaildInputException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// 例外対象
        /// </summary>
        public List<Control> TargetList { get; } = new List<Control>();
        


    }

    
    /// <summary>
    /// エラーコントロール表示
    /// </summary>

    public class ErrorControlState : IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="clist">不正コントロール</param>
        /// <param name="f">即時変色可否</param>
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
        /// コンストラクタ
        /// </summary>
        /// <param name="ex">不正入力例外</param>
        /// <param name="f">即時変色可否</param>
        public ErrorControlState(InvaildInputException ex, bool f = true) : this(ex.TargetList, f)
        {

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
