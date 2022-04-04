using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity.Engine.Element;
using Clarity.Element;

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
        /// 入力設定ファイルの読み込み
        /// </summary>
        /// <param name="isfilepath">読み込み入力設定ファイルパス</param>
        public static void LoadInputSetting(string isfilepath)
        {
            InputManager.Mana.ReadInputSetting(isfilepath);
        }


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
            ClarityEngine.Engine.EngineData.SystemText.SetText(s, line + 1);
        }

        //////////////////////
        //Element操作

        /// <summary>
        /// 構造の作成
        /// </summary>
        /// <param name="stfilepath"></param>
        public static void CreateStructure(string stfilepath)
        {
            //ユーザー構造作成先のデータ取得
            ClarityStructure ust = ClarityEngine.Engine.EngineData.SystemStructure.GetNode(SystemStructureID.User);

            ClarityStructureFile fp = new ClarityStructureFile();
            {
                ust = fp.ReadStructure(stfilepath, ust);
            }

            //ユーザー管理構造を追加
            EngineStructureManager mana = new EngineStructureManager();
            mana.Init(ust);

            ClarityEngine.Engine.EngineData.UserStructure = mana;
        }

        /// <summary>
        /// 管理へ追加
        /// </summary>
        /// <param name="sid">追加先構造ID</param>
        /// <param name="data">追加データ</param>
        public static void AddManage(long sid, BaseElement data)
        {
            BaseElement parent = ClarityEngine.Engine.EngineData.UserStructure.GetNode(sid);
            ElementManager.AddRequest(parent, data);
        }

        
    }
}
