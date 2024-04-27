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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        private EditorLogic Logic = new EditorLogic();

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
                this.Logic.Init(MvGlobal.Project);
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
            this.SubErase.Add(cd);


            //タグの追加
            cd = MvGlobal.EventUI.Where(x => x.Event == EMovementUIEvent.TagAdd ).Subscribe(x =>
            {                
                var tag = x.Data as BaseEditTag;
                if (tag != null)
                {
                    this.Logic.AddTagUiElement(tag);
                }
            });
            this.SubErase.Add(cd);
            //タグの削除
            cd = MvGlobal.EventUI.Where(x => x.Event == EMovementUIEvent.TagRemove).Subscribe(x =>
            {
                var tag = x.Data as BaseEditTag;
                if (tag != null)
                {
                    this.Logic.RemoveTagUiElement(tag);
                }
            });
            this.SubErase.Add(cd);
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
            this.hScrollBar1.Maximum = (int)((this.Logic.FrameRenderSize.Width - this.Logic.CurrentBaseAreaWidth) * 0.9f);
        }

        /// <summary>
        /// 画面リサイズ処理
        /// </summary>
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
            this.Logic.Render(this.pictureBoxEditor, new PointF(offsetx, 0), e.Graphics, MvGlobal.Project);
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

            //対象フレームを取得
            FrameRenderingInfo? finfo = this.Logic.ColPointFrame(this.MouseInfo.NowPos);
            if(finfo == null)
            {
                return;
            }
            this.MouseInfo.SetMemory(finfo, 99);
            this.Logic.SelectedElement = this.Logic.ColSelectArea(new PointF(this.MouseInfo.NowPos.X, this.MouseInfo.NowPos.Y));
            this.Logic.SelectedElement?.Grab(this.MouseInfo, finfo);


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
            
            //対象フレームを取得
            FrameRenderingInfo? finfo = this.Logic.ColPointFrame(this.MouseInfo.NowPos);
            if (finfo == null)
            {
                return;
            }

            this.Logic.MouseOverElement = this.Logic.ColSelectArea(new PointF(this.MouseInfo.NowPos.X, this.MouseInfo.NowPos.Y));


            if (this.MouseInfo.DownFlag == true)
            {
                var sframe = this.MouseInfo.GetMemory<FrameRenderingInfo>(99);
                if (sframe != null)
                {
                    this.Logic.SelectedElement?.GrabMove(this.MouseInfo, sframe, finfo);
                }
            }

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
            if (dret != DialogResult.OK)
            {
                return;
            }

            //入力の取得
            var data = f.GetInput();
            //タグの追加
            MvGlobal.Project.AddTag(data);
        }
    }
}
