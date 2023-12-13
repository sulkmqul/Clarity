using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clarity.GUI;

namespace ClarityOrbit.LayerView
{
    /// <summary>
    /// Grid管理基礎
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class BaseGrid<T> where T : class
    {
        public BaseGrid(DataGridView dv)
        {
            this.Grid = dv;

            this.GridSource.DataSource = this.DataList;
            this.Grid.DataSource = this.GridSource;

        }

        /// <summary>
        /// 管理Grid
        /// </summary>
        protected DataGridView Grid;

        /// <summary>
        /// これのデータ
        /// </summary>
        protected List<T> DataList = new List<T>();

        /// <summary>
        /// これの元データ
        /// </summary>
        protected BindingSource GridSource = new BindingSource();

        /// <summary>
        /// Gridソースの再設定
        /// </summary>
        protected void ResetGridSource()
        {            
            this.GridSource.ResetBindings(true);
        }

        /// <summary>
        /// 選択データのオブジェクトを取得（複数選択を取得するときはGetSelectDataObjectListのほうを使用すること）
        /// </summary>
        /// <returns></returns>
        public virtual T? GetSelectedDataObject()
        {
            //選択している？
            if (this.Grid.SelectedRows.Count <= 0)
            {
                return null;
            }

            int index = this.Grid.SelectedRows[0].Index;
            return this.DataList[index];
        }

        /// <summary>
        /// 選択データの一式取得
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetSelectedDataList()
        {
            List<T> anslist = new List<T>();
            //選択している？
            if (this.Grid.SelectedRows.Count <= 0)
            {
                return anslist;
            }

            for (int i = 0; i < this.Grid.SelectedRows.Count; i++)
            {
                int index = this.Grid.SelectedRows[i].Index;
                anslist.Add(this.DataList[index]);
            }

            return anslist;
        }

    }

    /// <summary>
    /// Grid描画用データ
    /// </summary>
    internal class LayerGridData
    {
        public LayerGridData(OrbitLayer ldata, ImageList ilist)
        {
            this.Src = ldata;
            this.IconList = ilist;
        }


        /// <summary>
        /// 元ネタ
        /// </summary>
        public OrbitLayer Src;

        /// <summary>
        /// 使用アイコン一式
        /// </summary>
        private ImageList IconList;
        
        /// <summary>
        /// 順番
        /// </summary>
        public int OrderNo
        {
            get
            {
                return this.Src.OrderNo;
            }
        }

        /// <summary>
        /// 表示可否
        /// </summary>
        public Image? VisibleImage
        {
            get
            {
                return (this.Src.Visibled == true) ? this.IconList.Images[0] : null;
            }
        }
        /// <summary>
        /// 編集ロック可否
        /// </summary>
        public Image? LockImage
        {
            get
            {
                return (this.Src.Enabled == true) ? this.IconList.Images[1] : null;
            }
        }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name
        {
            get { return this.Src.Name; }
        }



    }

    /// <summary>
    /// レイヤーグリッドの管理
    /// </summary>
    internal class LayerGrid : BaseGrid<LayerGridData>
    {
        public LayerGrid(DataGridView dv, ImageList ilist) : base(dv)
        {
            this.IconList = ilist;
        }

        /// <summary>
        /// 使用画像一式
        /// </summary>
        private ImageList IconList;


        /// <summary>
        /// Girdの更新
        /// </summary>
        /// <param name="laylist"></param>
        public void RefleshGrid(List<OrbitLayer> laylist)
        {   

            using (LayoutChangingState st = new LayoutChangingState(this.Grid))
            {
                this.DataList.Clear();

                laylist.OrderBy(x => x.OrderNo).ToList().ForEach(x =>
                {
                    LayerGridData data = new LayerGridData(x, this.IconList);
                    this.DataList.Add(data);
                });

                this.ResetGridSource();

                //スタイル設定
                foreach (var col in this.Grid.Columns)
                {
                    var a = col as DataGridViewImageColumn;
                    if (a != null)
                    {
                        a.DefaultCellStyle.NullValue = null;
                    }
                }
            }


        }
    }
}

