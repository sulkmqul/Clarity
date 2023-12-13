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

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 初期化
        /// </summary>
        public void InitWindow()
        {
            this.RefreshDisplay();
        }

        /// <summary>
        /// 削除
        /// </summary>
        public void Release()
        {

        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// データ再描画
        /// </summary>
        private void RefreshDisplay()
        {
            //レイヤーの取得
            var lalist = OrbitGlobal.ProjectData.LayerList;
            this.Grid.RefleshGrid(lalist);
        }


        /// <summary>
        /// レイヤー追加処理
        /// </summary>
        private void AddLayer()
        {
            OrbitGlobal.ProjectData.AddLayer();
            this.RefreshDisplay();
        }

        /// <summary>
        /// 選択レイヤーの削除
        /// </summary>
        private void RemoveSelectLayer()
        {
            //選択対象の取得
            var data = this.Grid.GetSelectedDataObject()?.Src;
            if (data == null)
            {
                return;
            }

            //対象の削除
            OrbitGlobal.ProjectData.RemoveLayer(data);
            this.RefreshDisplay();
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
            try
            {
                this.AddLayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// レイヤー削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonLayerRemove_Click(object sender, EventArgs e)
        {
            try
            {
                this.RemoveSelectLayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        /// <summary>
        /// セルがダブルクリックされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewLayerGird_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //選択対象の取得
                var data = this.Grid.GetSelectedDataObject()?.Src;
                if (data == null)
                {
                    return;
                }

                LayerEditForm f = new LayerEditForm(data);
                f.ShowDialog(this);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
