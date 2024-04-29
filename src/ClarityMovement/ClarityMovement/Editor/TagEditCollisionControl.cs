using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Editor
{
    public enum ETagEditControlEvent
    {
        FrameLength, //フレームの長さを設定(data = int = framelength)
    }

    public delegate void ITagEditControlEventDelegate(ETagEditControlEvent eid, object data);

    public interface ITagEditControl
    {
        BaseEditTag CreateTag(int sframe, int eframe, Vector3 rpos);

        public event ITagEditControlEventDelegate? TagEditEvent; 
    }

    public partial class TagEditCollisionControl : UserControl, ITagEditControl
    {
        public TagEditCollisionControl()
        {
            InitializeComponent();
        }

        public event ITagEditControlEventDelegate? TagEditEvent = null;

        public BaseEditTag CreateTag(int sframe, int eframe, Vector3 rpos)
        {
            return new EditTagCollison();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void InitControl()
        {
            this.radioButtonColCircle.Checked = true;
        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TagEditCollisionControl_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        /// <summary>
        /// 円がチェックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonColCircle_CheckedChanged(object sender, EventArgs e)
        {
            this.panelCircle.Visible = true;
            this.panelRect.Visible = false;
        }

        /// <summary>
        /// 矩形がチェックされた
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButtonColRect_CheckedChanged(object sender, EventArgs e)
        {
            this.panelCircle.Visible = false;
            this.panelRect.Visible = true;
        }
    }
}
