using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Clarity;
using Clarity.GUI;

namespace ClarityOrbit.TipSelectView
{
    /// <summary>
    /// チップ画像選択View
    /// </summary>
    public partial class TileSelectViewDockingContent : WeifenLuo.WinFormsUI.Docking.DockContent 
    {
        public TileSelectViewDockingContent()
        {
            InitializeComponent();
        }



        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 新しい元画像を追加する
        /// </summary>
        /// <param name="filepath"></param>
        private void AddTileSrcImage(string filepath)
        {
            
            
        }


        /// <summary>
        /// 新しい画像ページの作成
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private TabPage CreateAddTileImageNewPage(string filepath)
        {
            //ページの追加
            var data = OrbitGlobal.Project.AddNewTileImageSrc(filepath);
            TabPage ans = new TabPage();
            
            
        }
        


        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// チップ画像ADDボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTipAdd_Click(object sender, EventArgs e)
        {
            //ファイルの選択
            string filepath = "";            
            using (OpenFileDialog diag = new OpenFileDialog())
            {
                diag.Filter = OrbitGlobal.ImageFileFilter;
                DialogResult dret = diag.ShowDialog(this);
                if (dret != DialogResult.OK)
                {
                    return;
                }
                filepath = diag.FileName;
            }

            //Tabの追加
            this.AddTileSrcImage(filepath);
        }

        /// <summary>
        /// チップ画像削除ボタンが押された時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonTipRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
