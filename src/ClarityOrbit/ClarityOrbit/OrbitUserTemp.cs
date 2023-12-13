using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit
{
    

    /// <summary>
    /// 選択情報など操作一時情報を格納する
    /// </summary>
    internal class OrbitUserTemp
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OrbitUserTemp()
        {

        }

        /// <summary>
        /// 元タイル画像選択情報
        /// </summary>
        public TileSrcImageSelectedInfo TileSrcSelectInfo { get; init; } = new TileSrcImageSelectedInfo();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// プロジェクト情報取得
        /// </summary>
        private OrbitData Project
        {
            get
            {
                return OrbitGlobal.ProjectData;
            }
        }
    }

    /// <summary>
    /// 元タイル画像選択情報
    /// </summary>
    internal class TileSrcImageSelectedInfo
    {
        /// <summary>
        /// 選択元画像ID
        /// </summary>
        public int TileSrcImageID { get; set; }

        /// <summary>
        /// 選択エリア
        /// </summary>
        public Rectangle Index;

    }


    
}
