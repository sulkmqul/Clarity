using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Clarity.Engine.Element;
using Clarity;
using Clarity.Engine.Shader;
using Clarity.Engine.Vertex;
using System.Drawing;

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


        public class BuildInShaderIndex
        {
            public const int Default = -100;
            public const int NoTexture = -99;
            public const int TextureAnime = -98;
            public const int TextureUseAlpha = -97;
        }

        public class BuildInTextureIndex
        {
            public const int CollisionCircle = -100;
            public const int CollisionRect = -99;
        }
        public class BuildInPolygonModelIndex
        {
            public const int Rect = -100;
            public const int Line = -99;
        }



        /// <summary>
        /// 入力設定ファイルの読み込み
        /// </summary>
        /// <param name="isfilepath">読み込み入力設定ファイルパス</param>
        public static void LoadInputSetting(string isfilepath)
        {
            InputManager.Mana.ReadInputSetting(isfilepath);
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
        /// 世界情報の設定
        /// </summary>
        /// <param name="wid">WorldID</param>
        /// <param name="wdata">設定データ</param>
        public static void SetWorld(int wid, WorldData wdata)
        {
            WorldManager.Mana.Set(wid, wdata);
        }

        /// <summary>
        /// 世界情報の取得
        /// </summary>
        /// <param name="wid">WorldID</param>
        /// <returns></returns>
        public static WorldData GetWorld(int wid)
        {
            return WorldManager.Mana.Get(wid);
        }
        /// <summary>
        /// カメラの更新
        /// </summary>        
        /// <param name="cmat">カメラマトリックス</param>        
        /// <param name="wid">WorldID</param>
        public static void UpdateCamera(Matrix4x4 cmat, int wid = 0)
        {
            WorldManager.Mana.SetCamera(wid, cmat);
        }

        /// <summary>
        /// プロジェクションの更新
        /// </summary>
        /// <param name="cmat"></param>
        /// <param name="wid"></param>
        public static void UpdateProjection(Matrix4x4 cmat, int wid = 0)
        {
            WorldManager.Mana.SetProjection(wid, cmat);
        }

        /// <summary>
        /// 画面座標からWorld座標変換
        /// </summary>
        /// <param name="wid">世界ID</param>
        /// <param name="mx">画面位置X</param>
        /// <param name="my">画面位置Y</param>
        /// <param name="z">0.0～1.0の間で深さを指定</param>
        /// <returns></returns>
        public static Vector3 WindowToWorld(int wid, float mx, float my, float z = 0.0f)
        {
            //現在世界を取得
            Clarity.Engine.WorldData wdata = ClarityEngine.GetWorld(wid);

            //ここ、Viewport行列はsystemでないといけないかもしれない
            Matrix4x4 invmat = wdata.CalcuInvCameraProjectionView();

            Vector4 mpos = new Vector4(mx, my, z, 1.0f);
            mpos = Vector4.Transform(mpos, invmat);
            mpos /= mpos.W;

            //systemviewは正射影等倍表示なため、無視する。

            return new Vector3(mpos.X, mpos.Y, mpos.Z);
        }


        


        /// <summary>
        /// 3D空間上の座標を画面座標へ変換する
        /// </summary>
        /// <param name="wid">世界ID</param>
        /// <param name="pos">画面座標</param>
        /// <returns></returns>
        public static Vector2 WorldToWindow(int wid, Vector4 pos)
        {
            //現在世界を取得
            Clarity.Engine.WorldData wdata = ClarityEngine.GetWorld(wid);
            Matrix4x4 mat = wdata.CalcuCameraProjectionView();
                        
            var wpos = Vector4.Transform(pos, mat);
            wpos /= wpos.W;

            //systemviewは正射影等倍表示なため、無視する。

            return new Vector2(wpos.X, wpos.Y);

        }

        /// <summary>
        /// Viewのサイズを取得
        /// </summary>
        /// <returns></returns>
        public static Size GetViewSize()
        {
            return ClarityEngine.Engine.Con.Size;
        }

     
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        #region Model_Obect_Vertex

        /// <summary>
        /// 頂点ポリゴン一式の読み込み
        /// </summary>
        /// <param name="plfilepath"></param>
        public static void AddVertexResources(string listfilepath)
        {
            VertexManager.Mana.CreateResource(listfilepath);
        }

        /// <summary>
        /// 頂点ポリゴンの追加
        /// </summary>
        /// <param name="vno"></param>
        /// <param name="polfilepath"></param>
        public static void AddVertexResource(int vno, string polfilepath)
        {
            VertexManager.Mana.AddResource(vno, polfilepath);
        }

        /// <summary>
        /// 頂点データのクリア
        /// </summary>
        public static void ClearVertexResource()
        {
            VertexManager.Mana.ClearUserData();
        }


        /// <summary>
        /// 対象モデルの頂点情報を取得する(index展開はしていない生データ)
        /// </summary>
        /// <param name="vid"></param>
        /// <returns></returns>
        public static List<Vector3> GetVertexList(int vid)
        {
            List<Vector3> anslist = new List<Vector3>();
            PolyData podata = VertexManager.GetPolygonData(vid);

            podata.VertexList.ForEach(x =>
            {
                anslist.Add(new Vector3(x.Pos.X, x.Pos.Y, x.Pos.Z));
            });
            return anslist;
        }
            
        #endregion

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
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

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        #region Shader

        /// <summary>
        /// Shaderの追加
        /// </summary>
        /// <typeparam name="T">データ型</typeparam>
        /// <param name="shlist"></param>
        public static void AddShader<T>(List<string> shlist) where T : struct
        {
            ShaderManager.Mana.AddResource<T>(shlist);
        }


        /// <summary>
        /// Shaderデータの設定
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sid"></param>
        /// <param name="data"></param>
        public static void SetShaderData<T>(int sid, T data) where T : unmanaged
        {
            ShaderManager.SetShaderData<T>(data, sid);
        }

        #endregion
    }
}
