using Clarity.Engine;
using Clarity.GUI;
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

namespace ClarityMovement.Viwer
{
    /// <summary>
    /// ClarityEngineをGUI表示させるものの基底
    /// </summary>
    public partial class BaseCeControl : UserControl
    {
        public BaseCeControl()
        {
            InitializeComponent();
        }

        #region メンバ変数
        /// <summary>
        /// 描画ループTask管理
        /// </summary>
        private Task? ClarityEngineLoopTask = null;
        /// <summary>
        /// 描画ループタスク破棄管理
        /// </summary>
        private CancellationTokenSource ClarityEngineLoopCanceller = new CancellationTokenSource();
        /// <summary>
        /// マウス管理
        /// </summary>
        protected MouseInfo Minfo { get; set; } = new MouseInfo();
        #endregion

        /// <summary>
        /// エンジン開始処理
        /// </summary>
        public virtual void Init()
        {
            //エンジンの初期化
            ClarityEngine.Init(this);            

            this.InitCE();

            //ClariyEngineLoop開始
            this.ClarityEngineLoopTask = ClarityEngine.Native.ProcLoop(1000.0f / 60.0f, this.ClarityEngineLoopCanceller.Token);
        }

        /// <summary>
        /// 解放
        /// </summary>
        /// <returns></returns>
        public async Task RelaseControl()
        {
            if (this.ClarityEngineLoopTask != null)
            {
                this.ClarityEngineLoopCanceller.Cancel();
                await this.ClarityEngineLoopTask;
            }
        }


        /// <summary>
        /// 追加初期化をするときはここで
        /// </summary>
        protected virtual void InitCE()
        {

        }

        /// <summary>
        /// 世界作成
        /// </summary>
        /// <returns></returns>
        protected virtual WorldData CreateWorld()
        {
            WorldData data = new WorldData();
            //VP
            data.VPort = new ViewPortData(0, 0, this.Width, this.Height, 0.0f, 1.0f);
            //カメラ位置
            data.DefaultCameraMat = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -100.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);

            //projection
            data.ProjectionMat = Matrix4x4.CreateOrthographic(this.Width, this.Height, 1.0f, 10000.0f);

            //計算
            data.ReCalcu();

            return data;
        }


        /// <summary>
        /// サイズが変更された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseCeControl_SizeChanged(object sender, EventArgs e)
        {
            if (ClarityEngine.IsEngineInit == false)
            {
                return;
            }

            {
                //再設定
                WorldData data = this.CreateWorld();
                ClarityEngine.SetWorld(1, data);
            }


            //リサイズ
            //this.ImageObject?.FitImage(this.Width, this.Height);
            //this.EventSubject.OnNext((DxControlEventID.ScaleChanged, this.ScaleRate));

        }
    }
}
