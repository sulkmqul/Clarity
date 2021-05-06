using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace Clarity
{

    /// <summary>
    /// 世界管理データ
    /// </summary>
    public class WorldData : IDisposable
    {
        /// <summary>
        /// カメラ行列[viewport index]
        /// </summary>
        /// <remarks>
        /// 複数ViewPortでの画面分割などではカメラが複数になりえるため配列とし、基本ViewPortIndexとの対応を前提とするが
        /// 詳細には規定しない
        /// </remarks>
        public Matrix[] CameraMatVec = new Matrix[1] { new Matrix() };
        /// <summary>
        /// 規定アクセスを簡略化
        /// </summary>
        public Matrix DefaultCameraMat
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
        public Matrix ProjectionMat;


        /// <summary>
        /// カメラ投影合成行列
        /// </summary>
        public Matrix CamProjectionMat
        {
            get;
            protected set;
        }


        /// <summary>
        /// 再計算
        /// </summary>
        public void ReCalcu(int ci = 0)
        {
            this.CamProjectionMat = Matrix.Multiply(this.CameraMatVec[ci], this.ProjectionMat);
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
        /// 最大ViewPort数（Defaultの一つを含む）
        /// </summary>
        public static readonly int MaxViewPort = 5;

        /// <summary>
        /// ViewPort管理・・・これはもう増えることもないし、露骨にいえば画面分割数ぐらい。あらかじめ固定配列で良いと思われる
        /// </summary>
        protected ViewPortData[] ViewPortVec = null;


        protected ViewPortData SystemViewPort = null;

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
            this.ViewPortVec = new ViewPortData[MaxViewPort];
            for (int i = 0; i < MaxViewPort; i++)
            {
                this.ViewPortVec[i] = null;
            }

        }






        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// SysteViewWolrdの作成
        /// </summary>
        /// <param name="width">画面横幅</param>
        /// <param name="height">画面縦幅</param>
        public void CreateSystemViewWorld(int width, int height)
        {
            //ViewPortの作成と登録
            Viewport vp = new Viewport(0, 0, width, height, 0.0f, 1.0f);
            this.SystemViewPort = new ViewPortData() { VPort = vp };


            //デフォルト世界の登録
            WorldData wdata = new WorldData();
            wdata.DefaultCameraMat = Matrix.LookAtLH(new Vector3(0.0f, 0.0f, 10000.0f), new Vector3(0.0f, 0.0f, 0.0f), Vector3.UnitY);
            wdata.ProjectionMat = Matrix.OrthoLH(width, height, 1.0f, 15000.0f);
            wdata.ReCalcu();

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
        /// ViewPort設定登録
        /// </summary>
        /// <param name="vp">登録ViewPort</param>
        /// <param name="index">登録index 0=デフォルトの変更</param>
        public void SetViewPort(Viewport vp, int index = 0)
        {
            this.ViewPortVec[index] = new ViewPortData() { VPort = vp };
        }

        /// <summary>
        /// ViewPortの取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Viewport GetViewPort(int index)
        {
            return this.ViewPortVec[index].VPort;
        }

        /// <summary>
        /// SystemViewportの取得
        /// </summary>
        /// <returns></returns>
        public Viewport GetSystemViewPort()
        {
            return this.SystemViewPort.VPort;
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
        public void SetCamera(int n, Matrix c, int ci = 0)
        {
            this.ManaDic[n].CameraMatVec[ci] = c;
            this.ManaDic[n].ReCalcu();
        }
        /// <summary>
        /// プロジェクションの設定
        /// </summary>
        /// <param name="n"></param>
        /// <param name="pro"></param>
        public void SetProjection(int n, Matrix pro)
        {
            this.ManaDic[n].ProjectionMat = pro;
            this.ManaDic[n].ReCalcu();
        }

    }
}
