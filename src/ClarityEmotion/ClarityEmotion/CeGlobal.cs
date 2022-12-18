using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClarityEmotion
{
    /// <summary>
    /// 全体管理
    /// </summary>
    internal class CeGlobal : Clarity.BaseClaritySingleton<CeGlobal>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CeGlobal()
        {
            
        }

        /// <summary>
        /// イベント管理
        /// </summary>
        public CeEvent EventData { get; } = new CeEvent();
        public static CeEvent Event
        {
            get
            {
                return Instance.EventData;
            }
        }


        /// <summary>
        /// project情報
        /// </summary>
        public EmotionProject ProjectData { get; } = new EmotionProject();

        public static EmotionProject Project
        {
            get
            {
                return Instance.ProjectData;
            }
        }

        public static void Create()
        {
            CeGlobal.Instance = new CeGlobal();            
        }
    }
}
