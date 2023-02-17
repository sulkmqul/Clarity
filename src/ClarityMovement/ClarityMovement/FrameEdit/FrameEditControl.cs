using Clarity.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Permissions;
using System.Reflection.Metadata.Ecma335;

namespace ClarityMovement.FrameEdit
{
    /// <summary>
    /// フレーム編集コントロール
    /// </summary>
    public partial class FrameEditControl : UserControl
    {
        public FrameEditControl()
        {
            InitializeComponent();

            


        }

        /// <summary>
        /// フレームサイズのパラメータ
        /// </summary>
        internal FrameEditorParam SizeParam = new FrameEditorParam();

        /// <summary>
        /// 拡縮処理は面倒なので1フレームの描画サイズを定義しておく
        /// </summary>
        private int[] FrameSizeList = { 20, 50, 100, 200, 400 };
        /// <summary>
        /// FrameSizeListの選択index
        /// </summary>
        private int FrameSizePos = 0;
        /// <summary>
        /// 描画処理者
        /// </summary>
        private FrameEditorPainter? Painter = null;

        /// <summary>
        /// 編集描画データ一式
        /// </summary>
        internal EditorData EData { get; private set; } = new EditorData();

        #region イベントrx変換
        /// <summary>
        /// マウスダウンイベントのrx
        /// </summary>
        internal IObservable<MouseEventArgs> MouseDownObs
        {
            get
            {
                return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                    h => (sender, ev) => h(ev),
                    h => this.pictureBoxCanvas.MouseDown += h,
                    h => this.pictureBoxCanvas.MouseDown -= h
                    );
            }
        }
        /// <summary>
        /// MouseMoveイベントのrx
        /// </summary>
        internal IObservable<MouseEventArgs> MouseMoveObs
        {
            get
            {
                return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                    h => (sender, ev) => h(ev),
                    h => this.pictureBoxCanvas.MouseMove += h,
                    h => this.pictureBoxCanvas.MouseMove -= h
                    );
            }
        }

        /// <summary>
        /// MouseUpイベントのrx
        /// </summary>
        internal IObservable<MouseEventArgs> MouseUpObs
        {
            get
            {
                return Observable.FromEvent<MouseEventHandler, MouseEventArgs>(
                    h => (sender, ev) => h(ev),
                    h => this.pictureBoxCanvas.MouseUp += h,
                    h => this.pictureBoxCanvas.MouseUp -= h
                    );
            }
        }

        /// <summary>
        /// ドラッグドロップのrx
        /// </summary>
        internal IObservable<(Point, Point)> MouseDragObs
        {
            get
            {
                //ドラッグobsのの作成
                var drag = this.MouseDownObs.SelectMany(x => this.MouseMoveObs).TakeUntil(this.MouseUpObs);                
                return drag.Zip(drag.Skip(1), (x, y) => (x.Location, y.Location)).Repeat();

            }
        }
        #endregion

        //--//
        /// <summary>
        /// 初期化されるとき
        /// </summary>
        public void Init()
        {
            //ペインターの初期化
            this.Painter = new FrameEditorPainter(this);
            this.Painter.Init();

            //フレームサイズのリセット
            this.FrameSizePos = 0;
            this.ResizeControl();

            //mouse moveをmouseodownからmouseupまで取り出すもの、この時ついでにmousedown時とmouseupそして後始末の処理をする
            var dragtemp = this.MouseMoveObs.SkipUntil(this.MouseDownObs.Do(x => System.Diagnostics.Trace.WriteLine("mdown")))
                .TakeUntil(this.MouseUpObs.Do(x => System.Diagnostics.Trace.WriteLine("mup")));

            //上記と同じだが処理がないver
            var dragtemp2 = this.MouseMoveObs.SkipUntil(this.MouseDownObs.Do(x => { } ))
                .TakeUntil(this.MouseUpObs.Do(x => { }));

            //二つを前後の値をマージしてドラッグ処理を作る(本来なら同じ処理で行けるが、doで処理があるので・・・)
            var drag  = dragtemp.Zip(dragtemp2.Skip(1), (x, y) => (x.Location, y.Location))
                .Finally(() => System.Diagnostics.Trace.WriteLine("end")).Repeat();

        }


        /// <summary>
        /// 拡縮処理
        /// </summary>
        /// <param name="f">true=拡大 false=縮小</param>
        public void Zoom(bool f)
        {
            if (f == true)
            {
                this.FrameSizePos = Math.Min(this.FrameSizeList.Length - 1, this.FrameSizePos + 1);
            }
            else
            {
                this.FrameSizePos = Math.Max(0, this.FrameSizePos - 1);
            }

            //領域再計算処理
            this.ResizeControl();

            this.Refresh();
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// コントロールのリサイズ
        /// </summary>
        private void ResizeControl()
        {
            if (CmGlobal.Project.Value == null)
            {
                return;
            }
            if (this.Painter == null)
            {
                return;
            }
            //フレームの取得
            var proj = CmGlobal.Project.Value;
            int maxframe = proj.Frame;

            //サイズの計算            
            this.SizeParam.FrameSize = this.FrameSizeList[this.FrameSizePos];
            Size csize = this.Painter.ResizeArea(maxframe, this.SizeParam);

            //サイズの規定
            this.Width = csize.Width + 1;
            this.Height = csize.Height + 1; 
        }


        /// <summary>
        /// マウス移動処理
        /// </summary>
        /// <param name="mpos"></param>
        private void MouseMoveProc(Point mpos)
        {
            if (this.Painter == null)
            {
                return;
            }
            //カーソル移動
            this.EData.MouseCursor = this.Painter.GetSelect(mpos);
            this.Refresh();
        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="mpos"></param>
        private void MouseDownProc(Point mpos)
        {
            if (this.Painter == null)
            {
                return;
            }

            var seldata = this.EData.PaintDataList.Where(x => x.FixedArea.Contains(mpos)).FirstOrDefault();
            if (seldata != null)
            {
                //ここで対象の編集画面
                return;
            }

            //追加画面
            var cursor = this.EData.MouseCursor;
            if (cursor == null)
            {
                return;
            }

            if (cursor.Area == ETagType.Image)
            {
                SrcImageSelectForm f = new SrcImageSelectForm(false);
                var dret = f.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }
                var ddd = f.SelectedData;
            }
            
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameEditControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 描画されるとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBoxCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (CmGlobal.Project.Value == null)
            {
                return;
            }
            //フレームの取得
            var proj = CmGlobal.Project.Value;
            int maxframe = proj.Frame;

            this.Painter?.Paint(e.Graphics, maxframe, this.EData);
        }


    }
}
