namespace TrainSrcArranger
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ������
        /// </summary>
        private void InitForm()
        {
            //�摜�m�菈��
            TsGlobal.NextProcSub.Subscribe(async x =>
            {
                string nowpath = TsGlobal.Data.NowPath;
                try
                {
                    //�摜�ۑ��ƃv�����v�g�ۑ�
                    string? prompt = (this.checkBoxSavePromptFlag.Checked == true) ? this.textBoxPrompt.Text.Trim() : null;
                    int index = TsGlobal.Data.ProcNext();

                    this.arrangeEditor1.Visible = false;
                    await TsGlobal.SaveArrangeImages(index, x.Image, prompt);
                    this.arrangeEditor1.Visible = true;
                    this.arrangeEditor1.Focus();


                }
                catch
                {
                    MessageBox.Show($"{nowpath} �������s");
                    return;
                }

                //����
                if (x.NextImageFlag == true)
                {
                    this.NextImage();
                }

            });


            //�C�x���g����
            TsGlobal.EventSub.Subscribe(ev =>
            {
                this.UpdateInfo();
            });
        }




        /// <summary>
        /// �V�K�����̍쐬
        /// </summary>
        /// <param name="data"></param>
        private void CreateNewWorking(ArrangeData data)
        {
            //����̏����f�[�^�ݒ�
            TsGlobal.Mana._Data = data;

            TsGlobal.EventSub.OnNext(EControlEvent.New);

            this.arrangeEditor1.Init(data.CutSize);
            this.NextImage();
        }

        /// <summary>
        /// ���̉摜��\������
        /// </summary>
        private void NextImage()
        {
            string? fpath = TsGlobal.Data.ProcCurrentPath();
            if (fpath == null)
            {
                //�����I����ʒm
                TsGlobal.EventSub.OnNext(EControlEvent.Finish);
                return;
            }
            this.arrangeEditor1.LoadImage(fpath);
            this.arrangeEditor1.SetFitSize();
            TsGlobal.EventSub.OnNext(EControlEvent.Next);
        }

        /// <summary>
        /// ���̍X�V
        /// </summary>
        private void UpdateInfo()
        {
            //���\���e��

            //�i��
            this.labelProgress.Text = $"{TsGlobal.Data.NowFileIndex}/{TsGlobal.Data.WorkingFilePathList.Count}";
            //�����p�X
            this.Text = TsGlobal.Data.NowPath;

            this.labelScaleRate.Text = $"x{this.arrangeEditor1.ZoomRate:F3}";

        }


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// �ǂݍ��܂ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.InitForm();

        }

        private void �V�KNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm f = new SettingForm();
            var dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //�����ŏ���
            if (f.InputData == null)
            {
                return;
            }

            //������
            this.CreateNewWorking(f.InputData);


        }

        /// <summary>
        /// �I�����j���I��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void �I��XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ���{�\���{�^���������ꂽ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSameSize_Click(object sender, EventArgs e)
        {
            this.arrangeEditor1.SetSameSize();

        }

        /// <summary>
        /// Fit�\���{�^���������ꂽ��
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
            //File�̏ꍇ�̂݋���
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
                //FileDrop�̂ݎ󂯕t��
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

                //�Ώۃt�H���_�����݂��邩�H
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

                    //�����ŏ���
                    if (sf.InputData == null)
                    {
                        return;
                    }

                    //������
                    this.CreateNewWorking(sf.InputData);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("�ǂݍ��ݎ��s");
            }
        }
    }
}