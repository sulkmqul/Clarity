using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityOrbit
{
    internal class OrbitSetting
    {
        /// <summary>
        /// 表示設定クラス
        /// </summary>
        public OrbitDisplaySetting DisplaySetting { get; private set; } = new OrbitDisplaySetting();
    }

    /// <summary>
    /// 表示設定
    /// </summary>
    internal class OrbitDisplaySetting
    {
        public OrbitDisplaySetting()
        {

        }

        /// <summary>
        /// 元画像グリッド色
        /// </summary>
        public Color TileSrcImageGridColor = Color.Gray;

        /// <summary>
        /// 元画像マウスオーバー選択色
        /// </summary>
        public Color TileSrcImageMouseOverColor = Color.Black;


        /// <summary>
        /// 元画像領域選択色
        /// </summary>
        public Color TileSrcImageSelectAreaColor = Color.Red;
    }
}
