using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity;
using Clarity.Engine;

namespace ClarityOrbit.LayerView
{
    /// <summary>
    /// レイヤー表示コントロール
    /// </summary>
    public partial class LayerViewDockingContent : WeifenLuo.WinFormsUI.Docking.DockContent 
    {
        public LayerViewDockingContent()
        {
            InitializeComponent();
            this.Grid = new LayerGrid(this.dataGridViewLayerGird, this.imageList1);
        }


        /// <summary>
        /// Gird管理
        /// </summary>
        private LayerGrid Grid;

        private IDisposable OperationSub;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //レイヤーに関する行事が来たら更新を行う
            this.OperationSub = OrbitGlobal.Subject.OperationSubject.Where(x => (x.Operation | EOrbitOperation.Layer) != 0).Subscribe(x =>
            {
                this.RefleshDisplay();
            });

        }

        /// <summary>
        /// 削除
        /// </summary>
        public void Release()
        {
            this.OperationSub.Dispose();
        }


        /// <summary>
        /// データ再描画
        /// </summary>
        private void RefleshDisplay()
        {
            if (OrbitGlobal.Project == null)
            {
                return;
            }
            this.Grid.RefleshGrid(OrbitGlobal.Project.Layer.LayerList);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LayerViewDockingContent_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// レイヤー追加ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerAdd_Click(object sender, EventArgs e)
        {
            if (OrbitGlobal.Project == null)
            {
                return;
            }

            //起動
            LayerEditForm f = new LayerEditForm();
            DialogResult dret = f.ShowDialog(this);
            if (dret != DialogResult.OK)
            {
                return;
            }

            //レイヤーの追加と設定
            LayerInfo linfo = OrbitGlobal.Project.Layer.AddNewLayer();
            f.GetInput(ref linfo);

            //Operation
            OrbitGlobal.Subject.OperationSubject.OnNext(new OrbitOperationInfo(EOrbitOperation.LayerAdd, linfo));
        }

        /// <summary>
        /// レイヤー削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerRemove_Click(object sender, EventArgs e)
        {
            //Operation
            OrbitGlobal.Subject.OperationSubject.OnNext(new OrbitOperationInfo(EOrbitOperation.LayerRemove));
        }


        /// <summary>
        /// レイヤーを一つ上にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerUp_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// レイヤーを一つ下にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerDown_Click(object sender, EventArgs e)
        {

        }
    }
}
