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
