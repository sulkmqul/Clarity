using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using SharpDX;
using Clarity.Element;
using Clarity.Element.Scene;

namespace Clarity
{
    /// <summary>
    /// ClarityEngine分割　外部への機能提供アクセサーはここに描くこと 基本的にstaticであること
    /// </summary>
    public partial class ClarityEngine
    {

        /// <summary>
        /// ClarityEngine設定のデフォルト設定の書き出し
        /// </summary>
        /// <param name="filepath"></param>
        public static void WriteDefaultEngineSetting(string filepath)
        {
            //データの書き出し
            ClarityEngineSetting cdata = new ClarityEngineSetting();
            Clarity.File.ClarityEngineSettingFile.WriteSetting(filepath, cdata);
        }



        #region World

        /// <summary>
        /// 世界の設定
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="wdata"></param>
        public static void SetWorld(int wid, WorldData wdata)
        {
            WorldManager.Mana.Set(wid, wdata);
        }
        #endregion


        #region Element
        

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
        /// シーンの登録
        /// </summary>
        /// <param name="scene"></param>
        public static void AddScene(BaseScene scene)
        {
            SceneManager.Manager.AddScene(scene);
        }


        /// <summary>
        /// シーン変更リクエスト
        /// </summary>
        /// <param name="sno"></param>
        public static void ChangeSceneRequest(int sno)
        {
            SceneManager.Manager.ChangeSceneRequest(sno);
        }

        /// <summary>
        /// Scene実行開始
        /// </summary>
        /// <param name="sno"></param>
        public static void ExecuteScene(int sno)
        {
            SceneManager.Manager.ExecuteScene(sno);
        }



        /// <summary>
        /// シーンの削除
        /// </summary>
        public static void ClearScene()
        {
        }


        /// <summary>
        /// 拡張関数を起動する
        /// </summary>        
        /// <param name="eno">イベント番号</param>
        /// <param name="oidlist">発行対象 nullで全オブジェクト</param>
        public static void ExecuteElementExtraEventProc<T>(int eno, List<long> oidlist)
        {
            ElementManager.Mana.ExecuteElementExtraEvent(eno, oidlist);
        }
        #endregion

        #region Texture  TextureAnime

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


        /// <summary>
        /// 対象テクスチャindexのoffsetを取得する
        /// </summary>
        /// <param name="tid">texture id</param>
        /// <param name="tix">index x</param>
        /// <param name="tiy">index y</param>
        /// <returns></returns>
        public static Vector2 GetTextureOffset(int tid, int tix, int tiy = 0)
        {
            Vector2 tsize = Texture.TextureManager.GetTextureDivSize(tid);

            Vector2 ans = new Vector2();
            ans.X = tsize.X * (float)tix;
            ans.Y = tsize.Y * (float)tiy;

            return ans;

        }

        #endregion

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
