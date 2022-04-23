using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity.Engine.Element;
using Clarity;

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
            ClarityEngine.Engine.EngineData.SystemText.SetText(s, line + 2);
        }

        /// <summary>
        /// 構造の作成
        /// </summary>
        /// <param name="stfilepath"></param>
        public static void CreateStructure(string stfilepath)
        {
            //ユーザー構造作成先のデータ取得
            ClarityStructure ust = ClarityEngine.Engine.EngineData.SystemStructure.GetNode(ESystemStructureID.User);

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
        /// テクスチャファイル一式の読み込み
        /// </summary>
        /// <param name="filepathlist">テクスチャ一覧ファイル</param>
        public static void LoadTextureList(List<string> filepathlist)
        {
            Texture.TextureManager.Mana.CreateResource(filepathlist);
        }

        /// <summary>
        /// 読み込みテクスチャの解放
        /// </summary>
        public static void ClearTexture()
        {
            Texture.TextureManager.Mana.ClearUserData();
        }

        /// <summary>
        /// テクスチャアニメの読み込み
        /// </summary>
        /// <param name="filepathlist">テクスチャアニメファイル一式</param>
        public static void LoadTextureAnimeFile(List<string> filepathlist)
        {
            Texture.TextureAnimeFactory.Mana.ReadTextureAnimeFile(filepathlist);
        }

        /// <summary>
        /// テクスチャサイズの取得
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static Vector2 GetTextureSize(int tid)
        {
            return Texture.TextureManager.GetTextureSize(tid);
        }

        /// <summary>
        /// テクスチャの画像サイズを取得(分割数を考慮しない実サイズ)
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public static Vector2 GetTextureOriginalSize(int tid)
        {
            return Texture.TextureManager.GetTextureSize(tid, true);
        }

        /// <summary>
        /// テクスチャサイズの取得 Anime版
        /// </summary>
        /// <param name="aid">アニメID</param>
        /// <param name="aindex">アニメIndex</param>
        /// <returns></returns>
        public static Vector2 GetTextureSize(int aid, int aindex)
        {
            var adata = Texture.TextureAnimeFactory.GetAnime(aid);
            int tid = adata.FrameList[aindex].TextureID;

            return ClarityEngine.GetTextureSize(tid);
        }


        #region Input

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
        #endregion
    }
}
