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
            /// テクスチャの追加
            /// </summary>
            /// <param name="tid">指定ID</param>            
            /// <param name="bit">追加データ</param>
            public static void LoadTexture(int tid, System.Drawing.Bitmap bit)
            {
                TextureManager.Mana.AddTexture(tid, tid.ToString(), bit, new System.Drawing.Size(1, 1));
            }

            /// <summary>
            /// RGBAバッファからテクスチャを読み込む
            /// </summary>
            /// <param name="tid">設定テクスチャID</param>
            /// <param name="width">横幅</param>
            /// <param name="height">縦幅</param>
            /// <param name="rgba">RGBAバッファ</param>
            public static void LoadTexture(int tid, int width, int height, byte[] rgba)
            {
                TextureManager.Mana.AddTexture(tid, tid.ToString(), width, height, rgba, new System.Drawing.Size(1, 1));
            }


            /// <summary>
            /// 対象テクスチャの削除
            /// </summary>
            /// <param name="tid"></param>
            public static void RemoveTexture(int tid)
            {
                TextureManager.Mana.RemoveTexture(tid);
            }
            /// <summary>
            /// 読み込みテクスチャの解放
            /// </summary>
            public static void ClearTexture()
            {
                TextureManager.Mana.ClearUserData();
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

        
            

            #endregion

        }
    }
}
