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
        private int[] FrameSizeList = { 10, 20, 50, 100, 200, 400 };
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

        /// <summary>
        /// マウス情報
        /// </summary>
        internal MouseInfo MInfo = new MouseInfo();

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
            var dragtemp = this.MouseMoveObs.SkipUntil(this.MouseDownObs.Do(x => { 
                this.MouseDownProc(x); 
            }))
                .TakeUntil(this.MouseUpObs.Do(x => this.MouseUpProc(x) ));

            //上記と同じだが処理がないver
            var dragtemp2 = this.MouseMoveObs.SkipUntil(this.MouseDownObs)
                .TakeUntil(this.MouseUpObs);

            //二つを前後の値をマージしてドラッグ処理を作る(本来なら同じ処理で行けるが、doで処理があるので分かり易くるために分ける)
            var drag  = dragtemp.Zip(dragtemp2.Skip(1), (x, y) => (x.Location, y.Location))
                .Finally(() => System.Diagnostics.Trace.WriteLine("end")).Repeat();

            //ドラッグ処理
            drag.Subscribe(x =>
            {
                //ドラッグ処理を描く
                this.MouseDragProc(x.Item1, x.Item2);
            });

            //マウス移動処理・・・ドラッグ中はキャンセルする
            this.MouseMoveObs.TakeWhile((x) => this.MInfo.DownFlag == false).Repeat().Subscribe(x =>
            {
                this.MouseMoveProc(x.Location);
            });


            //タグ情報の再作成
            this.RecreateTagPaint();
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

        /// <summary>
        /// タグ表示情報の再作成
        /// </summary>
        public void RecreateTagPaint()
        {
            var proj = CmGlobal.Project.Value;
            if (proj == null)
            {
                return;
            }
            //削除
            this.EData.PaintDataList.Clear();

            proj.ModifierList.ForEach(x =>
            {
                BaseFrameModifierPaintData data = this.CreateModifierPaint(x);
                data.Init(this, this.Painter);
                this.EData.PaintDataList.Add(data);
            });
        }
        /// <summary>
        /// タグ情報の確定
        /// </summary>
        /// <remarks>編集したタグ情報をプロジェクト情報へ書き出す</remarks>
        public void ApplyTagModifier()
        {
            var proj = CmGlobal.Project.Value;
            if (proj == null)
            {
                return;
            }

            //既存をクリア
            proj.ModifierList.Clear();

            //現在のデータを設定する
            this.EData.PaintDataList.ForEach(x =>
            {
                proj.ModifierList.Add(x.SrcData);
            });


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
            int maxframe = proj.MaxFrame;

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
            this.Cursor = Cursors.Default;
            this.EData.MouseCursor = this.Painter.GetSelect(mpos);

            var data = this.EData.PaintDataList.Where(x => x.DetectMouse(mpos)).FirstOrDefault();
            if (data != null)
            {

                switch (data.ChangeWork)
                {
                    case EChangeWork.Position:
                        this.Cursor = Cursors.Default;
                        break;
                    case EChangeWork.Left:
                    case EChangeWork.Right:
                        this.Cursor = Cursors.SizeWE;
                        break;
                }

                
            }
            this.Refresh();
        }

        /// <summary>
        /// マウスが押された時
        /// </summary>
        /// <param name="marg"></param>
        private void MouseDownProc(MouseEventArgs marg)
        {
            this.MInfo.DownMouse(marg);

            if (this.Painter == null)
            {
                return;
            }

            //選択対象データの割り出して対象を選択する
            this.EData.PaintDataList.ForEach(x => x.SelectedFlag = false);
            this.EData.TempSelect = this.EData.PaintDataList.Where(x => x.DetectMouse(marg.Location)).Select(d => { d.SelectedFlag = true; return d; }).FirstOrDefault();

        }


        /// <summary>
        /// マウスドラッグ
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        private void MouseDragProc(Point prev, Point next)
        {
            if (this.Painter == null)
            {
                return;
            }

            //ドラッグの選択をそれぞれ取得
            var pselect = this.Painter.GetSelect(prev);
            var nselect = this.Painter.GetSelect(next);
            if (pselect == null || nselect == null)
            {
                return;
            }

            //ドラッグ処理
            this.EData.TempSelect?.DragWork(pselect, nselect);

            this.Refresh();

        }

        /// <summary>
        /// マウスが離された時
        /// </summary>
        /// <param name="marg"></param>
        private void MouseUpProc(MouseEventArgs marg)
        {
            this.MInfo.UpMouse(marg);

            //既存の選択があった
            if (this.EData.TempSelect != null)
            {
                this.EditTagData(this.EData.TempSelect);
                return;
            }

            //何もない所でクリックだったら追加する
            //データの追加
            if (this.EData.MouseCursor != null)
            {
                this.AddTagData(this.EData.MouseCursor);
            }
        }


        /// <summary>
        /// タグデータの追加
        /// </summary>
        /// <param name="sec"></param>
        private void AddTagData(FrameEditorSelection sec)
        {
            switch (sec.Area)
            {
                case ETagType.Image:
                    {
                        //Imageの追加
                        this.AddImageTag(sec);
                    }
                    break;
                case ETagType.Tag:
                    {
                        this.AddTagModifier(sec);
                    }
                    break;
            }
        }


        /// <summary>
        /// 画像の追加
        /// </summary>
        /// <param name="sec">選択</param>
        private void AddImageTag(FrameEditorSelection sec)
        {
            //画面を出すとスクロールがリセットされて紛らわしいので小細工する。
            Panel fds = (Panel)this.Parent;
            int scval = fds.HorizontalScroll.Value;


            //画像の選択
            SrcImageSelectForm f = new SrcImageSelectForm(false);            
            var dret = f.ShowDialog();
            if (dret != DialogResult.OK)
            {
                return;
            }
            if (f.SelectedData == null)
            {
                return;
            }

            CmImageData image = f.SelectedData;

            //新しいデータの作成
            FrameImageModifier data = new FrameImageModifier();
            data.Frame = sec.FrameNo;
            data.FrameSpan = 1;
            data.ImageDataID = image.CmImageID;

            //描画用データの作成
            FrameModifierPaintDataImage pdata = new FrameModifierPaintDataImage(data);
            pdata.Init(this, this.Painter);

            //追加
            this.EData.PaintDataList.Add(pdata);

            //スクロールの復帰
            fds.HorizontalScroll.Value = scval;
        }

        /// <summary>
        /// Tagデータの追加
        /// </summary>
        /// <param name="sec"></param>
        private void AddTagModifier(FrameEditorSelection sec)
        {
            //FrameTagModifier data = new FrameTagModifier();
            //data.Frame = sec.FrameNo;
            //data.FrameSpan = 1;
            //data.TagId = sec.TagIndex;
            //data.TagCode = "";

            //入力処理
            TagInputForm f = new TagInputForm();
            DialogResult dret = f.ShowDialog(this.ParentForm);
            if(dret != DialogResult.OK)
            {
                return;
            }
            if(f.InputData == null)
            {
                return;
            }

            //入力の取得と必要情報の設定
            FrameTagModifier data = f.InputData;
            data.Frame = sec.FrameNo;
            data.FrameSpan = 1;
            data.TagId = sec.TagIndex;

            //描画用データ作成
            FrameModifierPaintDataTag tag = new FrameModifierPaintDataTag(data);
            tag.Init(this, this.Painter);

            //追加
            this.EData.PaintDataList.Add(tag);
        }


        /// <summary>
        /// 正しい表示クラスを作成取得する。
        /// 正しい表示クラスを作成取得する。
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private BaseFrameModifierPaintData CreateModifierPaint(BaseFrameModifier mod)
        {
            BaseFrameModifierPaintData? ans = null;
            switch (mod.TagType)
            {
                case ETagType.Image:
                    {
                        ans = new FrameModifierPaintDataImage((FrameImageModifier)mod);
                    }
                    break;
                case ETagType.Tag:
                    {
                        ans = new FrameModifierPaintDataTag((FrameTagModifier)mod);
                    }
                    break;
            }
            if (ans == null)
            {
                throw new Exception("未定義のpaint modifier変換");
            }

            return ans;
        }



        /// <summary>
        /// 選択タグの編集
        /// </summary>
        /// <param name="pdata"></param>
        private void EditTagData(BaseFrameModifierPaintData pdata)
        {
            switch (pdata.TagType)
            {
                case ETagType.Image:
                    {
                        //Imageの編集
                        
                    }
                    break;
                case ETagType.Tag:
                    {
                        //tagの編集
                        this.EditTagModifier((FrameModifierPaintDataTag)pdata);
                    }
                    break;
            }

            this.Refresh();
        }

        /// <summary>
        /// タグの編集
        /// </summary>
        /// <param name="ptag">編集タグ</param>
        private void EditTagModifier(FrameModifierPaintDataTag ptag)
        {
            TagInputForm f = new TagInputForm(ptag.TagData);
            DialogResult dret = f.ShowDialog(this.ParentForm);
            if (dret != DialogResult.OK)
            {
                return;
            }
            if (f.InputData == null)
            {
                return;
            }

            //入力の取得と必要情報の設定
            ptag.SrcData = f.InputData;            

            //初期化する。            
            ptag.Init(this, this.Painter);
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
            int maxframe = proj.MaxFrame;

            this.Painter?.Paint(e.Graphics, maxframe, this.EData);
        }

        /// <summary>
        /// キーが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameEditControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                //選択対象を削除
                this.EData.PaintDataList.RemoveAll(x => x.SelectedFlag == true);
                this.Refresh();
            }
        }
    }
}
