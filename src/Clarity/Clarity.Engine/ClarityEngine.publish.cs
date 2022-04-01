using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Clarity.Engine
{
    /// <summary>
    /// 外部公開関数定義
    /// </summary>
    public partial class ClarityEngine
    {
        /// <summary>
        /// 無効ID値
        /// </summary>
        public const int INVALID_ID = int.MinValue;


        /// <summary>
        /// 対象テクスチャindexのoffsetを取得する
        /// </summary>
        /// <param name="tid">texture id</param>
        /// <param name="tix">index x</param>
        /// <param name="tiy">index y</param>
        /// <returns></returns>
        public static Vector2 GetTextureOffset(int tid, int tix, int tiy = 0)
        {
            Vector2 tsize = Texture.TextureManager.GetTextureDivSize(tid) ?? new Vector2(0, 0);

            Vector2 ans = new Vector2();
            ans.X = tsize.X * (float)tix;
            ans.Y = tsize.Y * (float)tiy;

            return ans;
        }

        /// <summary>
        /// システムテキストの設定
        /// </summary>
        /// <param name="s">表示文字列</param>
        /// <param name="line">設定行</param>
        public static void SetSystemText(string s, int line = 0)
        {
            Core.SystemText.SetText(s, line + 1);
        }
    }
}
