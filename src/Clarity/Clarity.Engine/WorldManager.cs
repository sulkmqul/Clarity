using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Vortice.Mathematics;

namespace Clarity.Engine
{

    /// <summary>
    /// 世界管理データ
    /// </summary>
    public class WorldData : IDisposable
    {
        /// <summary>
        /// これの使用するViewPort
        /// </summary>
        public ViewPortData VPort;

        /// <summary>
        /// カメラ行列[viewport index]
        /// </summary>
        /// <remarks>
        /// 複数ViewPortでの画面分割などではカメラが複数になりえるため配列とし、基本ViewPortIndexとの対応を前提とするが
        /// 詳細には規定しない
        /// </remarks>
        public Matrix4x4[] CameraMatVec = new Matrix4x4[1] { new Matrix4x4() };
        /// <summary>
        /// 規定アクセスを簡略化
        /// </summary>
        public Matrix4x4 DefaultCameraMat
        {
            get
            {
                return this.CameraMatVec[0];
            }
            set
            {
                this.CameraMatVec[0] = value;
            }
        }

        /// <summary>
        /// 投影行列
        /// </summary>
        public Matrix4x4 ProjectionMat;


        /// <summary>
        /// カメラ投影合成行列
        /// </summary>
        public Matrix4x4 CamProjectionMat
        {
            get;
            protected set;
        }


        /// <summary>
        /// 再計算
        /// </summary>
        public void ReCalcu(int ci = 0)
        {
            this.CamProjectionMat = Matrix4x4.Multiply(this.CameraMatVec[ci], this.ProjectionMat);            
        }


        /// <summary>
        /// 解放処理
        /// </summary>
        public void Dispose()
        {
            
        }
    }

    /// <summary>
    /// ViewPort管理データ・・・一応何かに備えてデータ化しておく
    /// </summary>
    public class ViewPortData
    {
        public Viewport VPort;
    }

    /// <summary>
    /// 世界管理規定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>camera projなど描画世界の単位をまとめて管理する 何個も世界管理があるとは思えないので継承させず、単一の管理クラスとして作成してしまう</remarks>
    internal class WorldManager : BaseClarityFactroy<WorldManager, WorldData>
    {
        private WorldManager()
        {
            
        }

        /// <summary>
        /// 基本View(RenderingTextureを描画するSystemViewのID)
        /// </summary>
        public static readonly int SystemViewID = -1;

        
        /// <summary>
        /// 作成
        /// </summary>
        public static void Create()
        {
            Instance = new WorldManager();
            Instance.Init();
        }

        /// <summary>
        /// このクラスの初期化
        /// </summary>
        private void Init()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// SysteViewWolrdの作成
        /// </summary>
        /// <param name="width">画面横幅</param>
        /// <param name="height">画面縦幅</param>
        public void CreateSystemViewWorld(int width, int height)
        {
            //デフォルト世界の登録
            WorldData wdata = new WorldData();            
            wdata.DefaultCameraMat = Matrix4x4.CreateLookAt(new Vector3(0.0f, 0.0f, -10000.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            wdata.ProjectionMat = Matrix4x4.CreateOrthographic(width, height, 1.0f, 15000.0f);
            wdata.ReCalcu();

            //この世界のViewPortを作成
            Viewport vp = new Viewport(0, 0, width, height, 0.0f, 1.0f);
            wdata.VPort = new ViewPortData() { VPort = vp };

            WorldManager.Mana.Set(WorldManager.SystemViewID, wdata);

        }


        
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public WorldData Get(int n)
        {
            return this.ManaDic[n];
        }

        /// <summary>
        /// SystemViewの取得
        /// </summary>
        /// <returns></returns>
        public WorldData GetSystemViewWorld()
        {
            return this.Get(WorldManager.SystemViewID);
        }

        /// <summary>
        /// 値の設定
        /// </summary>
        /// <param name="wid"></param>
        /// <param name="wdata"></param>
        public void Set(int wid, WorldData wdata)
        {
            this.ManaDic[wid] = wdata;
        }

        /// <summary>
        /// カメラ位置の設定
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        public void SetCamera(int n, Matrix4x4 c, int ci = 0)
        {
            this.ManaDic[n].CameraMatVec[ci] = c;
            this.ManaDic[n].ReCalcu();
        }
        /// <summary>
        /// プロジェクションの設定
        /// </summary>
        /// <param name="n"></param>
        /// <param name="pro"></param>
        public void SetProjection(int n, Matrix4x4 pro)
        {
            this.ManaDic[n].ProjectionMat = pro;
            this.ManaDic[n].ReCalcu();
        }

    }
}
