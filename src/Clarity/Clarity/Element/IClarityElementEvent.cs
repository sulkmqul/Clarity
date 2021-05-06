﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clarity.Element
{
    /// <summary>
    /// 発生イベント
    /// </summary>
    public class ClarityElementEventID
    {
        /// <summary>
        /// 対象オブジェクトが破棄された
        /// </summary>
        public static readonly int Destroy = 1;

    }

    /// <summary>
    /// Elementイベント通知Interface
    /// </summary>
    public interface IClarityElementEvent
    {
        /// <summary>
        /// データイベント
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="data"></param>
        void EventCallback(int eid, BaseElement data);
    }
}