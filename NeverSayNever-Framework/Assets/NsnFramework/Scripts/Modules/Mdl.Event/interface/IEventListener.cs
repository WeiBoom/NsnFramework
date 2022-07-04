using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public enum EventDelegateState
    {
        None = 0,
        Continue = 1,
        Fail = 2,
        Success = 3,
    }

    public delegate EventDelegateState GameEventDelegate(EventData e);

    public interface IEventManager : IManager
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="gameEvent"></param>
        void RegisterEvent(int eventId, GameEventDelegate gameEvent);

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="gameEvent"></param>
        void RemoveEvent(int eventId, GameEventDelegate gameEvent);

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventId"></param>
        void RemoveEvent(int eventId);

        /// <summary>
        /// 派发事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="eventData"></param>
        void DispatchEvent(int eventId, EventData eventData);

        /// <summary>
        /// 是否已经注册指定事件
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        bool HasEvent(int eventId);
    }
}