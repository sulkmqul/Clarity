using Clarity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClarityMovement
{
    enum EMovementUIEvent
    {
        NewProject,

        EditorZoomUp,
        EditorZoomDown,

        TagAdd,
        TagRemove,


    }
    /// <summary>
    /// イベント情報
    /// </summary>
    internal class MovementEvent
    {
        public MovementEvent(EMovementUIEvent et, object? data = null)
        {
            this.Event = et;
            this.Data = data;
        }

        /// <summary>
        /// イベント番号
        /// </summary>
        public EMovementUIEvent Event { get; init; }

        /// <summary>
        /// 追加情報
        /// </summary>
        public object? Data { get; init; } = null;

    }

    /// <summary>
    /// 全体データ
    /// </summary>
    internal class MvGlobal : BaseClarityConstSingleton<MvGlobal>
    {
        
        /// <summary>
        /// UiElement識別ID
        /// </summary>
        private Clarity.Util.ClaritySequence TagSeq = new Clarity.Util.ClaritySequence();


        /// <summary>
        /// タグのIDを取得
        /// </summary>
        /// <returns></returns>
        public static ulong GetUiElementID()
        {
            return MvGlobal.Mana.TagSeq.NextValue;
        }


        /// <summary>
        /// イベント
        /// </summary>
        public static Subject<MovementEvent> EventUI { get; private set; } = new Subject<MovementEvent>();

        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public static MvProject Project
        {
            get
            {
                if(MvGlobal.Mana.ProjectData == null)
                {
                    throw new InvalidOperationException("ProjectData is not created");
                }
                return MvGlobal.Mana.ProjectData;
            }
        }

        /// <summary>
        /// true=作成済み false=未作成
        /// </summary>
        public static bool ProjectCreateFlag
        {
            get
            {
                var proj = MvGlobal.Mana.ProjectData;
                if (proj == null)
                {
                    return false;
                }
                return true;
            }
        }


        /// <summary>
        /// 設定値
        /// </summary>
        public static MvSetting Setting
        {
            get
            {
                return MvGlobal.Mana.SettingData;
            }
        }

        /// <summary>
        /// プロジェクト情報
        /// </summary>
        public MvProject? ProjectData { get; private set; } = null;


        /// <summary>
        /// 設定値
        /// </summary>
        public MvSetting SettingData { get; private set; } = new MvSetting();

        //--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//--//
        /// <summary>
        /// プロジェクトの新規作成
        /// </summary>
        /// <param name="bilist"></param>
        public void CraeteProject(List<Bitmap> bilist)
        {
            this.ProjectData = new MvProject();
            this.ProjectData.BaseImageList = new List<Bitmap>();
            this.ProjectData.BaseImageList.AddRange(bilist);
        }

        /// <summary>
        /// イベントの荘子に
        /// </summary>
        /// <param name="ev"></param>
        /// <param name="data"></param>
        public static void SendEventUI(EMovementUIEvent ev, object? data = null)
        {
            MovementEvent edata = new MovementEvent(ev, data);
            MvGlobal.EventUI.OnNext(edata);
        }
    }
}
