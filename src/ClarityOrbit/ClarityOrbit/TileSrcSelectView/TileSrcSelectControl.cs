using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarityOrbit.TileSrcSelectView
{
    /// <summary>
    /// タイル元画像の表示コントロール
    /// </summary>
    public partial class TileSrcSelectControl : UserControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="sinfo">管理データ</param>
        public TileSrcSelectControl(TileSrcImageInfo sinfo)
        {
            InitializeComponent();

            this.SInfo = sinfo;
        }

        /// <summary>
        /// 管理データ
        /// </summary>
        public TileSrcImageInfo SInfo { get; private set; }

        
        

        /// <summary>
        /// 初期化
        /// </summary>        
        /// <param name="sinfo">管理データ</param>
        public void Initialize()
        {
            //データ
            var sinfo = this.SInfo;

            //パス表示
            this.textBoxImagePath.Text = sinfo.FilePath;

            
            //画像初期化
            this.clarityImageViewerTileSrc.Init(sinfo.ImageData);

            //表示管理
            this.clarityImageViewerTileSrc.AddDisplayer(new TileSrcGridDisplayer(sinfo));
            
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 読み込まれた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileSrcSelectControl_Load(object sender, EventArgs e)
        {

        }
    }
}
