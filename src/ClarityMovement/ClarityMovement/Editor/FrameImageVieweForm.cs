using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Editor
{
    public partial class FrameImageVieweForm : Form
    {
        public FrameImageVieweForm(Bitmap image)
        {
            InitializeComponent();

            this.Image = image;
        }

        /// <summary>
        /// 表示画像
        /// </summary>
        private Bitmap Image;


        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameImageViewer_Load(object sender, EventArgs e)
        {
            this.clarityImageViewer1.Init(this.Image);
        }


        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clarityImageViewer1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Close();
            }
        }
    }
}
