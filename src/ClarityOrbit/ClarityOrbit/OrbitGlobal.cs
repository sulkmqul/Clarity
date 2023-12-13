using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clarity;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection.Metadata.Ecma335;

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

        public static LayerView.LayerViewDockingContent LayerView { get; private set; } = new LayerView.LayerViewDockingContent();

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


        /// <summary>
        /// レイヤー画面の表示
        /// </summary>
        /// <param name="dpane"></param>
        public static void ShowLayerView(DockPanel dpane)
        {
            //Layer選択画面の表示
            if (OrbitWindowManager.LayerView.IsDisposed == true)
            {
                OrbitWindowManager.LayerView = new LayerView.LayerViewDockingContent();
            }

            //初期化
            OrbitWindowManager.LayerView.InitWindow();

            //再表示
            OrbitWindowManager.LayerView.Show(dpane, DockState.DockBottom);
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
        public static readonly string ImageFileFilter = "画像ファイル|*.bmp;*.png;*.jpg|ビットマップ|*.bmp|Jpegファイル(*.jpg)|*.jpg|Pngファイル(*.png)|*.png|全てのファイル(*.*)|*.*";


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
        /// ユーザー操作一時情報
        /// </summary>
        public OrbitUserTemp UserTempData { get; init; } = new OrbitUserTemp();

        /// <summary>
        /// 設定一式(いつか設定画面から自由に変更できるものたち)
        /// </summary>
        public OrbitSetting SettingData { get; init; } = new OrbitSetting();

        /// <summary>
        /// プロジェクト情報の安全な取得
        /// </summary>
        public static OrbitData ProjectData
        {
            get
            {
                
                if (OrbitGlobal.Mana.Project == null)
                {
                    throw new NullReferenceException("Project file is not created.");
                }
                return OrbitGlobal.Mana.Project;
            }
        }

        /// <summary>
        /// 設定情報
        /// </summary>
        public static OrbitSetting Settings
        {
            get
            {
                return OrbitGlobal.Mana.SettingData;
            }
        }

        /// <summary>
        /// 一時操作情報
        /// </summary>
        public static OrbitUserTemp UserTemp
        {
            get
            {
                return OrbitGlobal.Mana.UserTempData;
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
            this.Project = new OrbitData(tsize, tcount);            
        }

    }



    
}
