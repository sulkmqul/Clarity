using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    /// <summary>
    /// 設定情報
    /// </summary>
    internal class MvSetting
    {
        public class EditorSetting
        {
            public Color ClearColor { get; set; } = Color.LightGray;

            public Color GridColor { get; set; } = Color.Gray;

            public Color GridSelectedColor { get; set; } = Color.Goldenrod;

            public Color FontColor { get; set; } = Color.Black;
        }

        public EditorSetting Editor { get; private set; } = new EditorSetting();
    }
}
