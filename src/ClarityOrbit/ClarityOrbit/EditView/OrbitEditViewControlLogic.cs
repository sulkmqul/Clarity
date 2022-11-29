using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reactive.Linq;
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
        public LayerStructureElement? GridParent = null;        //削除予定(構造に置き換え)

        /// <summary>
        /// グリッド一覧
        /// </summary>
        public List<OrbitGridFrame> GridList = new List<OrbitGridFrame>();  //削除予定(構造に置き換え)


        /// <summary>
        /// 構造描画の頂点
        /// </summary>
        public SimpleElement? StructTreeTop = null;

        /// <summary>
        /// 描画構造一覧[y][x]
        /// </summary>
        public List<List<LayerStructureElement>> StructTreeList = new List<List<LayerStructureElement>>();

        /// <summary>
        /// カメラ
        /// </summary>
        public CameraElement Camera;

        /// <summary>
        /// マウスobject
        /// </summary>
        public MouseManageElement MouseElement;


        /// <summary>
        /// カメラ位置(Zoomに等しい)
        /// </summary>
        public float[] CameraZPos = { -125.0f, -250.0f, -500.0f, -1000.0f, -2000.0f, -4000.0f};

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


        /// <summary>
        /// 有効View範囲Index(見えている範囲)
        /// </summary>
        public Rectangle ViewAreaIndexRect = new Rectangle(-1,-1,-1,-1);

    }

    /// <summary>
    /// EditViewロジック
    /// </summary>
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

        /// <summary>
        /// 編集釣り
        /// </summary>
        private IDisposable EditSub;

        /// <summary>
        /// エンジン実行タスク
        /// </summary>
        private Task EngineTask;

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
            ClarityEngine.AddShader<TilemapShaderData>(new List<string>() { OrbitGlobal.ShaderListFile });

            //頂点の読み込み
            ClarityEngine.AddVertexResources(OrbitGlobal.PolyListFilePath);

            //構造の初期化
            this.InitStructure();

            //カメラの作成と初期位置の設定
            this.FData.Camera = new CameraElement();
            //this.FData.Camera.CameraPos = new Vector3(0.0f, 0.0f, -1000.0f);
            //this.FData.Camera.AtPos = new Vector3(0.0f, 0.0f, 0.0f);
            this.FData.Camera.SetCameraXY(new Vector3(0.0f, 0.0f, 0.0f));
            this.FData.Camera.SetCameraZ(-1000.0f);

            ClarityAid.AddElement(EStructureCode.Manager, this.FData.Camera);


            //マウス管理の作成[
            this.FData.MouseElement = new MouseManageElement();
            ClarityAid.AddElement(EStructureCode.Infomation, this.FData.MouseElement);

            //世界観の初期化
            var wdata = this.CreateWorld(this.Con.panelDx.Width, this.Con.panelDx.Height);
            ClarityEngine.SetWorld(OrbitGlobal.OrbitWorldID, wdata);

            //編集タスクを仕掛ける
            this.EditSub = OrbitGlobal.Subject.TipEditSubject.Subscribe(x =>
            {
                TileEditCore.Edit(x);
            });

            //Layerが追加された時
            OrbitGlobal.Subject.OperationSubject.Where(x => x.Operation == EOrbitOperation.LayerAdd).Subscribe(data =>
            {
                LayerInfo? linfo = data.Data as LayerInfo;
                if (linfo == null)
                {
                    return;
                }

                //対象レイヤーの情報を構造へADDする
                this.AddLayerStructure(linfo);

            });
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
            if (OrbitGlobal.Project == null)
            {
                return;
            }


            //既存のデータのクリアを行う
            if (this.FData.StructTreeTop != null)
            {
                ClarityEngine.RemoveManage(this.FData.StructTreeTop);
            }
            //基礎構造の作成
            this.FData.StructTreeTop = new SimpleElement();
            this.FData.StructTreeList = this.CreateStructList(OrbitGlobal.Project.BaseInfo.TileCount, OrbitGlobal.Project.BaseInfo.TileSize);

            //Gridの作成
            this.CreateGrid(OrbitGlobal.Project.BaseInfo.TileCount);

            ClarityAid.AddElement(EStructureCode.Grid, this.FData.StructTreeTop);
            this.FData.StructTreeList.ForEach(x => x.ForEach(y => ClarityEngine.AddManage(this.FData.StructTreeTop, y)));

        }



        /// <summary>
        /// カメラ位置の拡縮処理
        /// </summary>
        /// <param name="f">true=拡大(近くする) false=縮小(遠くする)</param>
        public void ChangeZoomCameraPos(bool f)
        {
            int iz = this.SearchCameraPosIndex(this.FData.Camera.CameraPos.Z);
            iz = (f == true) ? iz -1 : iz + 1;
            iz = Math.Clamp(iz, 0, this.FData.CameraZPos.Length - 1);

            this.FData.Camera.SetCameraZ(this.FData.CameraZPos[iz]);

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
        /// グリッドの作成と追加
        /// </summary>
        /// <param name="tcount">タイル数</param>
        private void CreateGrid(Size tcount)
        {
            //構造の配下にgridを作成する
            foreach (var slist in this.FData.StructTreeList)
            {
                foreach (LayerStructureElement st in slist)
                {
                    st.ForEachTileIndex((x, y) =>
                    {
                        OrbitGridFrame og = new OrbitGridFrame(new Point(x, y));
                        ClarityEngine.AddManage(st.GridParent, og);
                    });
                }
            }

        }

        /// <summary>
        /// 描画構造の作成
        /// </summary>
        /// <param name="tcount"></param>
        /// <param name="tsize"></param>
        /// <returns></returns>
        private List<List<LayerStructureElement>> CreateStructList(Size tcount, Size tsize)
        {
            //作成サイズを算出する
            int sizew = (int)Math.Ceiling((float)tcount.Width / (float)OrbitGlobal.MAX_STRUCT_ELEMENT_COUNT);
            int sizeh = (int)Math.Ceiling((float)tcount.Height / (float)OrbitGlobal.MAX_STRUCT_ELEMENT_COUNT);

            List<List<LayerStructureElement>> anslist = new List<List<LayerStructureElement>>();

            for (int y = 0; y < sizeh; y++)
            {
                List<LayerStructureElement> alist = new List<LayerStructureElement>();

                for (int x = 0; x < sizew; x++)
                {
                    //index範囲の計算
                    int xi = x * OrbitGlobal.MAX_STRUCT_ELEMENT_COUNT;
                    int yi = y * OrbitGlobal.MAX_STRUCT_ELEMENT_COUNT;

                    Rectangle rc = new Rectangle(xi, yi, OrbitGlobal.MAX_STRUCT_ELEMENT_COUNT, OrbitGlobal.MAX_STRUCT_ELEMENT_COUNT);
                    if (rc.Right > tcount.Width)
                    {
                        rc.Width = (tcount.Width - rc.Left);
                    }
                    if (rc.Bottom > tcount.Height)
                    {
                        rc.Height = (tcount.Height - rc.Top);
                    }

                    //親を作成
                    LayerStructureElement a = new LayerStructureElement(rc, tsize);
                    alist.Add(a);
                }

                anslist.Add(alist);
                        
            }

            
            return anslist;
        }

        /// <summary>
        /// 適切なカメラ位置を算出
        /// </summary>
        /// <param name="cposz"></param>
        /// <returns></returns>
        private int SearchCameraPosIndex(float cposz)
        {
            float len = float.MaxValue;
            int ans = 0;
            for (int i = 0; i < this.FData.CameraZPos.Length; i++)
            {
                //距離が最小な場所を現在の拡大率とする
                float a = Math.Abs(this.FData.CameraZPos[i] - cposz);
                if (a < len)
                {
                    len = a;
                    ans = i;
                }
            }

            return ans;
        }

        /// <summary>
        /// 対象レイヤーを構造へ追加する
        /// </summary>
        /// <param name="linfo">追加対象レイヤー情報</param>
        private void AddLayerStructure(LayerInfo linfo)
        {
            //先頭がすでにClarityへ追加されているなら処理をしない
            if (linfo.TipMap[0][0].IsManaged == true)
            {
                return;
            }

            //構造の配下に新規のレイヤー情報を追加する
            foreach (var slist in this.FData.StructTreeList)
            {
                foreach (LayerStructureElement st in slist)
                {
                    st.AddLayer(linfo);
                }
            }
        }
    }
}
