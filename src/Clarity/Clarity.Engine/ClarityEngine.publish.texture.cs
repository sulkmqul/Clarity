using Clarity.Engine.Texture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Engine
{
    public partial class ClarityEngine    
    {
        public static class Texture
        {
            //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
            //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
            #region Texture        

            /// <summary>
            /// 対象テクスチャindexのoffsetを取得する
            /// </summary>
            /// <param name="tid">texture id</param>
            /// <param name="tix">index x</param>
            /// <param name="tiy">index y</param>
            /// <returns></returns>
            public static Vector2 GetTextureOffset(int tid, int tix, int tiy = 0)
            {
                Vector2 tsize = TextureManager.GetTextureDivSize(tid) ?? new Vector2(0, 0);

                Vector2 ans = new Vector2();
                ans.X = tsize.X * (float)tix;
                ans.Y = tsize.Y * (float)tiy;

                return ans;
            }


            /// <summary>
            /// テクスチャファイル一式の読み込み
            /// </summary>
            /// <param name="filepathlist">テクスチャ一覧ファイル</param>
            public static void LoadTextureList(List<string> filepathlist)
            {
                TextureManager.Mana.CreateResource(filepathlist);
            }

            /// <summary>
            /// 読み込みテクスチャの解放
            /// </summary>
            public static void ClearTexture()
            {
                TextureManager.Mana.ClearUserData();
            }

            /// <summary>
            /// テクスチャアニメの読み込み
            /// </summary>
            /// <param name="filepathlist">テクスチャアニメファイル一式</param>
            public static void LoadTextureAnimeFile(List<string> filepathlist)
            {
                TextureAnimeFactory.Mana.ReadTextureAnimeFile(filepathlist);
            }

            /// <summary>
            /// テクスチャサイズの取得
            /// </summary>
            /// <param name="tid"></param>
            /// <returns></returns>
            public static Vector2 GetTextureSize(int tid)
            {
                return TextureManager.GetTextureSize(tid);
            }

            /// <summary>
            /// テクスチャの画像サイズを取得(分割数を考慮しない実サイズ)
            /// </summary>
            /// <param name="tid"></param>
            /// <returns></returns>
            public static Vector2 GetTextureOriginalSize(int tid)
            {
                return TextureManager.GetTextureSize(tid, true);
            }

            /// <summary>
            /// テクスチャサイズの取得 Anime版
            /// </summary>
            /// <param name="aid">アニメID</param>
            /// <param name="aindex">アニメIndex</param>
            /// <returns></returns>
            public static Vector2 GetTextureSize(int aid, int aindex)
            {
                var adata = TextureAnimeFactory.GetAnime(aid);
                int tid = adata.FrameList[aindex].TextureID;

                return ClarityEngine.Texture.GetTextureSize(tid);
            }

            #endregion

        }
    }
}
