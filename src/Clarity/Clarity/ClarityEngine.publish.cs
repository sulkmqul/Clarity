using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SharpDX;
using Clarity.Element;

namespace Clarity
{
    /// <summary>
    /// ClarityEngine分割　外部への機能提供アクセサーはここに描くこと
    /// </summary>
    public partial class ClarityEngine
    {
        /// <summary>
        /// 世界の設定
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="wdata"></param>
        public static void SetWorld(int wid, WorldData wdata)
        {
            WorldManager.Mana.Set(wid, wdata);
        }

        /// <summary>
        /// Element管理への登録
        /// </summary>
        /// <param name="ele"></param>
        public static void AddElement(BaseElement ele)
        {
            ElementManager.Mana.AddRequest(ele);
        }

        /// <summary>
        /// Element管理の削除
        /// </summary>
        /// <param name="ele"></param>
        public static void RemoveElement(BaseElement ele)
        {
            ElementManager.Mana.RemoveRequest(ele);

        }

        /// <summary>
        /// 対象の入力チェック
        /// </summary>
        /// <param name="gamekey">Clarity.GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKey(int gamekey)
        {
            return InputManager.TestKey(gamekey);
        }

        /// <summary>
        /// 対象の入力エッジチェック
        /// </summary>
        /// <param name="gamekey">Clarity.GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKeyEdge(int gamekey)
        {
            return InputManager.TestKeyEdge(gamekey);
        }


        /// <summary>
        /// 対象の離したエッジチェック
        /// </summary>
        /// <param name="gamekey">Clarity.GameKeyの値を設定する</param>
        /// <returns></returns>
        public static bool TestKeyReleaseEdge(int gamekey)
        {   
            return InputManager.TestKeyReleaseEdge(gamekey);
        }
    }
}
