namespace TrainSrcArranger
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 初期化
        /// </summary>
        private void InitForm()
        {
            //画像確定処理
            TsGlobal.NextProcSub.Subscribe(async x =>
            {
                string nowpath = TsGlobal.Data.NowPath;
                try
                {
                    //画像保存とプロンプト保存
                    string? prompt = (this.checkBoxSavePromptFlag.Checked == true) ? this.textBoxPrompt.Text.Trim() : null;
                    int index = TsGlobal.Data.ProcNext();

                    this.arrangeEditor1.Visible = false;
                    await TsGlobal.SaveArrangeImages(index, x.Image, prompt);
                    this.arrangeEditor1.Visible = true;
                    this.arrangeEditor1.Focus();


                }
                catch
                {
                    MessageBox.Show($"{nowpath} 処理失敗");
                    return;
                }

                //次へ
                if (x.NextImageFlag == true)
                {
                    this.NextImage();
                }

            });


            //イベント処理
            TsGlobal.EventSub.Subscribe(ev =>
            {
                this.UpdateInfo();
            });
        }




        /// <summary>
        /// 新規処理の作成
        /// </summary>
        /// <param name="data"></param>
        private void CreateNewWorking(ArrangeData data)
        {
            //今回の処理データ設定
            TsGlobal.Mana._Data = data;

            TsGlobal.EventSub.OnNext(EControlEvent.New);

            this.arrangeEditor1.Init(data.CutSize);
            this.NextImage();
        }

        /// <summary>
        /// 次の画像を表示する
        /// </summary>
        private void NextImage()
        {
            string? fpath = TsGlobal.Data.ProcCurrentPath();
            if (fpath == null)
            {
                //処理終了を通知
                TsGlobal.EventSub.OnNext(EControlEvent.Finish);
                return;
            }
            this.arrangeEditor1.LoadImage(fpath);
            this.arrangeEditor1.SetFitSize();
            TsGlobal.EventSub.OnNext(EControlEvent.Next);
        }

        /// <summary>
        /// 情報の更新
        /// </summary>
        private void UpdateInfo()
        {
            //情報表示各種

            //進捗
            this.labelProgress.Text = $"{TsGlobal.Data.NowFileIndex}/{TsGlobal.Data.WorkingFilePathList.Count}";
            //処理パス
            this.Text = TsGlobal.Data.NowPath;

            this.labelScaleRate.Text = $"x{this.arrangeEditor1.ZoomRate:F3}";

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitForm();

        }

        private void 新規NToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm f = new SettingForm();
            var dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //ここで処理
            if (f.InputData == null)
            {
                return;
            }

            //初期化
            this.CreateNewWorking(f.InputData);


        }

        /// <summary>
        /// 終了メニュ選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 等倍表示ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSameSize_Click(object sender, EventArgs e)
        {
            this.arrangeEditor1.SetSameSize();

        }

        /// <summary>
        /// Fit表示ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImageFit_Click(object sender, EventArgs e)
        {
            this.arrangeEditor1.SetFitSize();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            this.NextImage();
        }

        private void arrangeEditor1_DragEnter(object sender, DragEventArgs e)
        {
            //Fileの場合のみ許可
            bool f = e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false;
            if (f == true)
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        private void arrangeEditor1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                //FileDropのみ受け付け
                bool f = e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false;
                if (f == false)
                {
                    return;
                }

                string[]? svec = (string[]?)e.Data?.GetData(DataFormats.FileDrop);
                if (svec == null)
                {
                    return;
                }

                //対象フォルダが存在するか？
                bool dext = Directory.Exists(svec[0]);
                if (dext == false)
                {
                    return;
                }

                {

                    SettingForm sf = new SettingForm(svec[0]);
                    var dret = sf.ShowDialog(this);
                    if (dret != DialogResult.OK)
                    {
                        return;
                    }

                    //ここで処理
                    if (sf.InputData == null)
                    {
                        return;
                    }

                    //初期化
                    this.CreateNewWorking(sf.InputData);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み失敗");
            }
        }
    }
}