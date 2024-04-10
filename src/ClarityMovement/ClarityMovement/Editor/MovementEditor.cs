using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityMovement.Editor
{
    /// <summary>
    /// タイムラインのエディタ
    /// </summary>
    public partial class MovementEditor : UserControl
    {
        public MovementEditor()
        {
            InitializeComponent();
        }


        /// <summary>
        /// subject管理
        /// </summary>
        private CompositeDisposable SubErase = new CompositeDisposable();

        /// <summary>
        /// 描画者
        /// </summary>
        private EditorRenderer Renderer = new EditorRenderer();

        /// <summary>
        /// マウス管理
        /// </summary>
        private MouseInfo MouseInfo = new MouseInfo();
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            var cd = MvGlobal.EventUI.Where(x => x.Event == EMovementUIEvent.NewProject).Subscribe(x =>
            {
                //描画領域の作成                
                this.Renderer.Init(MvGlobal.Project);
                this.SetScrollH();
                this.Refresh();
            });
            this.SubErase.Add(cd);


            //拡縮の変更
            cd = MvGlobal.EventUI.Where(x => (x.Event == EMovementUIEvent.EditorZoomUp || x.Event == EMovementUIEvent.EditorZoomDown)).Subscribe(x =>
            {
                this.SetScrollH();
                this.Refresh();
            });
        }

        /// <summary>
        /// 解放処理
        /// </summary>
        public void ReleaseControl()
        {
            this.SubErase.Dispose();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 横スクロールの最大幅を設定
        /// </summary>
        private void SetScrollH()
        {
            this.hScrollBar1.Maximum = (int)((this.Renderer.FrameRenderSize.Width - this.Renderer.CurrentBaseAreaWidth) * 0.9f);
        }


        private void ResizeRendererArea()
        {
            //描画領域のリサイズ
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovementEditor_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEditor_Paint(object sender, PaintEventArgs e)
        {
            //描画情報取得            
            if (MvGlobal.ProjectCreateFlag == false)
            {
                return;
            }

            int offsetx = -this.hScrollBar1.Value;
            this.Renderer.Render(this.pictureBoxEditor, new PointF(offsetx, 0), e.Graphics, MvGlobal.Project);
        }


        /// <summary>
        /// スクロールされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEditor_MouseDown(object sender, MouseEventArgs e)
        {
            this.MouseInfo.DownMouse(e);

            this.Refresh();
        }

        /// <summary>
        /// マウスが動いたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEditor_MouseMove(object sender, MouseEventArgs e)
        {
            this.MouseInfo.MoveMouse(e);
            this.Renderer.ColSelectArea(new PointF(this.MouseInfo.NowPos.X, this.MouseInfo.NowPos.Y));

            this.Refresh();
        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxEditor_MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseInfo.UpMouse(e);

            var f = this.Renderer.ColPointFrame(new PointF(this.MouseInfo.NowPos.X, this.MouseInfo.NowPos.Y));
            if (f != null)
            {
                var bit = MvGlobal.Project.BaseImageList[f.FrameIndex];
                FrameImageVieweForm viewer = new FrameImageVieweForm(bit);
                viewer.ShowDialog(this);
            }



            this.Refresh();
        }

        /// <summary>
        /// 画面がリサイズされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovementEditor_Resize(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// タグ追加ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddTag_Click(object sender, EventArgs e)
        {
            if (MvGlobal.ProjectCreateFlag == false)
            {
                return;
            }

            TagEditForm f = new TagEditForm();
            var dret = f.ShowDialog(this);
        }
    }
}
