using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using WeifenLuo.WinFormsUI.Docking;

namespace ClarityOrbit
{
    /// <summary>
    /// windowの管理
    /// </summary>
    public class OrbitWindowManager
    {
        /// <summary>
        /// タイル映像選択
        /// </summary>
        public static TileSrcSelectView.TileSrcSelectViewDockingContent SrcSelectView { get; private set; } = new TileSrcSelectView.TileSrcSelectViewDockingContent();


        /// <summary>
        /// タイル元画像選択画面の表示
        /// </summary>
        /// <param name="dpane"></param>
        public static void ShowTileSrcSelectView(DockPanel dpane)
        {
            //削除されていたら再作成
            if (OrbitWindowManager.SrcSelectView.IsDisposed == true)
            {
                OrbitWindowManager.SrcSelectView = new TileSrcSelectView.TileSrcSelectViewDockingContent();
            }
            //初期化
            OrbitWindowManager.SrcSelectView.InitWindow();

            //再表示
            OrbitWindowManager.SrcSelectView.Show(dpane);
        }
    }



    /// <summary>
    /// 全体クラス
    /// </summary>
    internal class OrbitGlobal : BaseClarityConstSingleton<OrbitGlobal>
    {
        /// <summary>
        /// 初期化
        /// </summary>
        public void Init()
        {

        }
        
        /// <summary>
        /// 画像選択フィルター
        /// </summary>
        public static readonly string ImageFileFilter = "ビットマップ(*.bmp)|*.bmp|Jpegファイル(*.jpg)|*.jpg|Pngファイル(*.png)|*.png|全てのファイル(*.*)|*.*";


        /// <summary>
        /// コントロールイベント
        /// </summary>
        public Subject<OrbitEvent> OrbitEventSub { get; init; } = new Subject<OrbitEvent>();
        /// <summary>
        /// タイル設定イベント
        /// </summary>
        public Subject<TileEditEvent> TileEditSub { get; init; } = new Subject<TileEditEvent>();



        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public OrbitData? Project { get; private set; } = null;

        /// <summary>
        /// プロジェクト情報の安全な取得
        /// </summary>
        public static OrbitData ProjectData
        {
            get
            {
                
                if (OrbitGlobal.Mana.Project == null)
                {
                    throw new NullReferenceException("Project file was not create.");
                }
                return OrbitGlobal.Mana.Project;
            }
        }
        
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        //静的
        /// <summary>
        /// 制御イベントの送信
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="val"></param>
        public static void SendEvent(EOrbitEventID ev, object? val = null)
        {
            OrbitGlobal.Mana.OrbitEventSub.OnNext(new OrbitEvent(ev, val));
        }
        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// プロジェクトの作成
        /// </summary>
        /// <param name="tsize">タイルサイズ</param>
        /// <param name="tcount">タイル数</param>
        public void CreateNewProject(Size tsize, Size tcount)
        {
            //プロジェクトの作成
            this.Project = new OrbitData();
            this.Project.CreateNewProject(tsize, tcount);
        }

    }
}
