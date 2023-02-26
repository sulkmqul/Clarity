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
using System.Windows.Forms.Design;

namespace ClarityMovement
{
    /// <summary>
    /// 元画像割り当て画面
    /// </summary>
    internal partial class SrcImageAllocateForm : BaseSrcImageForm
    {
        public SrcImageAllocateForm()
        {
            InitializeComponent();
        }

        #region RX管理
        /// <summary>
        /// Offsetのvaluechange rx化
        /// </summary>
        internal IObservable<EventArgs> OffsetChangeObs
        {
            get
            {
                return Observable.FromEvent<EventHandler, EventArgs>(
                    h => (sender, ev) => h(ev),
                    h => this.numericUpDownOffset.ValueChanged += h,
                    h => this.numericUpDownOffset.ValueChanged -= h
                    );
            }
        }

        /// <summary>
        /// interval のvaluechange rx化
        /// </summary>
        internal IObservable<EventArgs> IntervalChangeObs
        {
            get
            {
                return Observable.FromEvent<EventHandler, EventArgs>(
                    h => (sender, ev) => h(ev),
                    h => this.numericUpDownInterval.ValueChanged += h,
                    h => this.numericUpDownInterval.ValueChanged -= h
                    );
            }
        }

        /// <summary>
        /// rx管理
        /// </summary>
        private CompositeDisposable RxDisp = new CompositeDisposable();
        #endregion
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 処理の反映 dialog_result=okの時のみ有効
        /// </summary>
        /// <param name="proj"></param>
        public void ApplyState(CmProject proj)
        {
            if (this.DialogResult != DialogResult.OK)
            {
                throw new InvalidOperationException("有効でありません");
            }


            //画像タグの全削除
            proj.ClearSelectTypeFrameModifier(ETagType.Image);

            int inter = (int)this.numericUpDownInterval.Value;
            int offset = (int)this.numericUpDownOffset.Value;
            int span = (int)this.numericUpDownFrame.Value;

            //選択リストの取得
            List<ImageSet> datalist = this.GetSelectImageSet();

            //フレーム最大まで、画像を順番に割り当て
            int i = 0;
            foreach (var data in datalist)
            {
                int nowf = i * span;
                if ((nowf + span) > proj.MaxFrame)
                {
                    break;
                }

                //データがない
                CmImageData? image = data.SrcObject;
                if (image == null)
                {
                    throw new Exception("計算できませんでした");
                }

                FrameImageModifier mod = new FrameImageModifier();
                mod.ImageDataID = image.CmImageID;
                mod.Frame = nowf;
                mod.FrameSpan = span;

                //データの追加
                proj.ModifierList.Add(mod);
                

                i++;
            }
        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        protected override void InitForm()
        {
            base.InitForm();

            //intervalとoffsetの選択値の変更をまとめて処理する、初めに現在値初期化したいので、強制で流す
            var vgs = Observable.Merge(this.OffsetChangeObs, this.IntervalChangeObs).StartWith(new EventArgs()).Subscribe(x =>
            {
                this.SelectImageInterval();
            });
            this.RxDisp.Add(vgs);

        }



        /// <summary>
        /// 値の再選択
        /// </summary>
        private void SelectImageInterval()
        {
            //現在の設定を取得
            int inter = (int)this.numericUpDownInterval.Value;
            int offset = (int)this.numericUpDownOffset.Value;

            //対象を選択する。
            this.listViewSrcImage.SelectedIndices.Clear();
            for (int i = offset; i < this.listViewSrcImage.Items.Count; i += inter)
            {
                this.listViewSrcImage.Items[i].Selected = true;
            }

            this.listViewSrcImage.Select();

            this.labelSelectedDataCount.Text = $"{this.listViewSrcImage.SelectedItems.Count}";
        }

        

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SrcImageAllocateForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// OKボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            //必要ならここで値を集めて正解を作成して置く

            this.DialogResult= DialogResult.OK;
        }

        /// <summary>
        /// キャンセルが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
