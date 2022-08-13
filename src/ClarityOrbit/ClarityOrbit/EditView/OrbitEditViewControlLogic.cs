using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using Clarity.Engine;
using Clarity.Engine.Element;

namespace ClarityOrbit.EditView
{
    

    /// <summary>
    /// Dx関係のデータなど
    /// </summary>
    internal class OrbitEditViewData
    {
        /// <summary>
        /// グリッド構造の親()
        /// </summary>
        public LayerStructureElement? GridParent = null;


        /// <summary>
        /// グリッド一覧
        /// </summary>
        public List<OrbitGridFrame> GridList = new List<OrbitGridFrame>();

        /// <summary>
        /// カメラ
        /// </summary>
        public CameraElement Camera;

        /// <summary>
        /// マウスobject
        /// </summary>
        public MouseManageElement MouseElement;

    }

    /// <summary>
    /// 全体に関係する描画に使用する一時情報をまとめる
    /// </summary>
    internal class DisplayTemplateInfo
    {
        /// <summary>
        /// 選択エリア情報(xy=マウスからのindex wh=src image area rect size)
        /// </summary>
        public Rectangle SelectTileRect = new Rectangle(-1, -1, -1, -1);
    }

    internal class OrbitEditViewControlLogic : ClarityEnginePlugin

    {
        public OrbitEditViewControlLogic(OrbitEditViewControl con, OrbitEditViewData fdata)
        {
            this.Con = con;
            this.FData = fdata;        
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 関連コントロール
        /// </summary>
        private OrbitEditViewControl Con { get; init; }
        /// <summary>
        /// 初期データ}
        /// </summary>
        private OrbitEditViewData FData { get; init; }

        //エンジン実行タスク
        private Task EngineTask = null;

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//

        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {
            //ClarityEngineの初期化
            ClarityEngine.Init(this.Con.panelDx, OrbitGlobal.ClarityEngineSettingFilePath);
            this.EngineTask = ClarityEngine.RunAsync(this);

            //Shaderの初期化
            ClarityEngine.AddShader<FrameGridShaderData>(new List<string>() { OrbitGlobal.ShaderListFile });

            //頂点の読み込み
            ClarityEngine.AddVertexResources(OrbitGlobal.PolyListFilePath);

            //構造の初期化
            this.InitStructure();

            //カメラの作成と初期位置の設定
            this.FData.Camera = new CameraElement();
            this.FData.Camera.CameraPos = new Vector3(0.0f, 0.0f, -1000.0f);
            this.FData.Camera.AtPos = new Vector3(0.0f, 0.0f, 0.0f);
            ClarityAid.AddElement(EStructureCode.Manager, this.FData.Camera);


            //マウス管理の作成[
            this.FData.MouseElement = new MouseManageElement();
            ClarityAid.AddElement(EStructureCode.Infomation, this.FData.MouseElement);

            //世界観の初期化
            var wdata = this.CreateWorld(this.Con.panelDx.Width, this.Con.panelDx.Height);
            ClarityEngine.SetWorld(OrbitGlobal.OrbitWorldID, wdata);

        }


        /// <summary>
        /// 情報描画の作り直し
        /// </summary>
        /// <remarks>
        /// レイヤーの初期化と似たような処理が走るが、こちらは描画、あっちはデータと意味が異なる
        /// こちらとデータを揃えてリンクをなす形をとる、という発想のためこのままいく
        /// </remarks>
        public void InitInfoView()
        {
            //既存のデータをクリアする
            if (this.FData.GridParent != null)
            {
                ClarityEngine.RemoveManage(this.FData.GridParent);
            }

            //グリッドの再作成
            this.FData.GridParent = new LayerStructureElement(0);
            this.FData.GridList = this.CreateGridElement(OrbitGlobal.Project.BaseInfo.TileCount);

            //EngineへADD
            ClarityAid.AddElement(EStructureCode.Grid, this.FData.GridParent);
            this.FData.GridList.ForEach(x =>
            {
                ClarityEngine.AddManage(this.FData.GridParent, x);
            });
            
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 画面のリサイズ
        /// </summary>
        /// <param name="ns"></param>
        public override void ResizeView(Size ns)
        {
            var wdata = this.CreateWorld(ns.Width, ns.Height);
            ClarityEngine.SetWorld(OrbitGlobal.OrbitWorldID, wdata);
        }

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// 構造設定
        /// </summary>
        private void InitStructure()
        {
            //構造の読み込み
            ClarityEngine.CreateStructure(OrbitGlobal.StructureSettingFilePath);
        }


        /// <summary>
        /// Worldの作成
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        private WorldData CreateWorld(int w, int h)
        {
            WorldData ans = new WorldData();

            //ans.ProjectionMat = Matrix4x4.CreateOrthographic(w, h, 1.0f, 10000.0f);
            float aspect = (float)w / (float)h;            
            ans.ProjectionMat = Matrix4x4.CreatePerspectiveFieldOfView(1.0f, aspect, 1.0f, 10000.0f);
            ans.VPort = new ViewPortData(0, 0, w, h, 0, 1.0f);
            ans.ReCalcu();

            return ans;
        }
     

        /// <summary>
        /// グリッドの作成
        /// </summary>
        /// <param name="tcount">タイル数</param>
        private List<OrbitGridFrame> CreateGridElement(Size tcount)
        {
            List<OrbitGridFrame> anslist = new List<OrbitGridFrame>();

            //タイル数の取得           

            for (int y = 0; y < tcount.Height; y++)
            {
                for (int x = 0; x < tcount.Width; x++)
                {
                    OrbitGridFrame og = new OrbitGridFrame(new Point(x, y));
                    anslist.Add(og);


                }
            }

            return anslist;
        }
    }
}
