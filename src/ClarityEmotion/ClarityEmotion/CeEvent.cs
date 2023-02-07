using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Subjects;

namespace ClarityEmotion
{
    /// <summary>
    /// イベント番号
    /// </summary>
    [Flags]
    internal enum EEventID : long
    {

        CreateProject = Create | Project,   //EmotionProject
        OpenProject = CreateProject,
        AddLayer =  Create| Layer,
        RemoveLayer = Delete| Layer,
        LayerSelectedChanged = Select | Update | Layer, //AnimeElement
        LayerUpdate = Update | Layer,   //AnimeElement
        AnimeDefinitionUpdate = Update | AnimeDefinition,   //EmotionProjectDataAnime
        //--------------------------------------------
        Create  = 1 << 0,
        Update = 1 << 1,
        Delete = 1 << 2,
        Select = 1 << 3,
                
        ProjectBasic = 1 << 4,
        ProjectAnime = 1 << 5,
        ProjectInfo = 1 << 6,
        ProjectOption = 1 << 7,
        Project = ProjectBasic | EEventID.ProjectAnime | EEventID.ProjectInfo | EEventID.ProjectOption,
        Layer = 1 << 8,
        AnimeDefinition = 1 << 9,

        
        
        //--------------------------------------------
        None = 0,
    }


    /// <summary>
    /// イベント管理
    /// </summary>
    internal class CeEvent
    {
        /// <summary>
        /// フレーム値変更
        /// </summary>
        public Subject<FrameChangeEvent> FrameChange { get; } = new Subject<FrameChangeEvent>();

        /// <summary>
        /// 値変更
        /// </summary>
        public Subject<ValueChangeEvent> ValueChange { get; } = new Subject<ValueChangeEvent>();


        /// <summary>
        /// 値変更の送付
        /// </summary>
        /// <param name="eid">イベントID</param>
        /// <param name="data">データ</param>
        public void SendValueChangeEvent(EEventID eid, object data)
        {
            this.ValueChange.OnNext(new ValueChangeEvent() { EventID = eid, Data = data });
        }

        /// <summary>
        /// フレーム選択の送信
        /// </summary>
        /// <param name="frame">送信選択フレーム</param>
        public void SendFrameSelectEvent(int frame)
        {
            this.FrameChange.OnNext(new FrameChangeEvent() { Frame = frame });
        }
    }


    /// <summary>
    /// フレーム変更データ
    /// </summary>
    internal class FrameChangeEvent
    {
        public int Frame { init; get; } = -1;
    }

    /// <summary>
    /// 値変更イベントデータ
    /// </summary>
    internal class ValueChangeEvent
    {
        public ValueChangeEvent()
        { 
        }

        /// <summary>
        /// 変更イベントID
        /// </summary>
        public EEventID EventID { init; get; } = EEventID.None;

        /// <summary>
        /// データ
        /// </summary>
        public object Data { init; get; } = 0;
    }
}
