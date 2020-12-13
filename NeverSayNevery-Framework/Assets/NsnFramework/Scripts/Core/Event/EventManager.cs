using System;

namespace NeverSayNever.Core.Event
{
    public class EventManager : EventDispatcher
    {
        private static EventManager _inst;
        private static EventManager Inst => _inst ?? (_inst = new EventManager());

        protected virtual string ListenerName => "EventManager";


        public static void OnUpdate()
        {
            Inst.UpdateListener();
        }

        public void OnDispose()
        {
            RemoveEvent();
            _inst = null;
        }

        public static bool HasEvent(Enum eventId)
        {
            return Inst.HasListener(eventId);
        }

        public static bool HasEvent(Enum eventId, Delegate eventFunc)
        {
            return Inst.HasListener(eventId, eventFunc);
        }

        #region 添加事件

        public static void AddEvent(Enum eventId, Delegate eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent(Enum eventId, EventCallback eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent<T>(Enum eventId, EventCallback<T> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent<T, T1>(Enum eventId, EventCallback<T, T1> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent<T, T1, T2>(Enum eventId, EventCallback<T, T1, T2> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }
        public static void AddEvent<T, T1, T2, T3>(Enum eventId, EventCallback<T, T1, T2, T3> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent<T, T1, T2, T3, T4>(Enum eventId, EventCallback<T, T1, T2, T3, T4> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent<T, T1, T2, T3, T4, T5>(Enum eventId, EventCallback<T, T1, T2, T3, T4, T5> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        public static void AddEvent<T, T1, T2, T3, T4, T5, T6>(Enum eventId, EventCallback<T, T1, T2, T3, T4, T5, T6> eventFunc)
        {
            Inst.AddListener(eventId, eventFunc);
        }

        #endregion

        #region 派发事件

        public static void Dispatch(Enum eventId)
        {
            Inst.DispatchEvent(eventId);
        }

        public static void Dispatch<T>(Enum eventId, T arg0)
        {
            Inst.DispatchEvent(eventId, arg0);
        }

        public static void Dispatch<T, T1>(Enum eventId, T arg0, T1 arg1)
        {
            Inst.DispatchEvent(eventId, arg0, arg1);
        }
        public static void Dispatch<T, T1, T2>(Enum eventId, T arg0, T1 arg1, T2 arg2)
        {
            Inst.DispatchEvent(eventId, arg0, arg1, arg2);
        }

        public static void Dispatch<T, T1, T2, T3>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            Inst.DispatchEvent(eventId, arg0, arg1, arg2, arg3);
        }

        public static void Dispatch<T, T1, T2, T3, T4>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Inst.DispatchEvent(eventId, arg0, arg1, arg2, arg3, arg4);
        }

        public static void Dispatch<T, T1, T2, T3, T4, T5>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Inst.DispatchEvent(eventId, arg0, arg1, arg2, arg3, arg4, arg5);
        }

        public static void Dispatch<T, T1, T2, T3, T4, T5, T6>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Inst.DispatchEvent(eventId, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        #endregion

        #region 移除事件

        public static void RemoveEvent()
        {
            Inst.RemoveListener();
        }

        public static void RemoveEvent(Enum eventId)
        {
            Inst.RemoveListener(eventId);
        }

        public static void RemoveEvent(Enum eventId, Delegate eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent(Enum eventId, EventCallback eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent<T>(Enum eventId, EventCallback<T> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent<T, T1>(Enum eventId, EventCallback<T, T1> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent<T, T1, T2>(Enum eventId, EventCallback<T, T1, T2> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }
        
        public static void RemoveEvent<T, T1, T2, T3>(Enum eventId, EventCallback<T, T1, T2, T3> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent<T, T1, T2, T3, T4>(Enum eventId, EventCallback<T, T1, T2, T3, T4> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent<T, T1, T2, T3, T4, T5>(Enum eventId, EventCallback<T, T1, T2, T3, T4, T5> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }

        public static void RemoveEvent<T, T1, T2, T3, T4, T5, T6>(Enum eventId, EventCallback<T, T1, T2, T3, T4, T5, T6> eventFunc)
        {
            Inst.RemoveListener(eventId, eventFunc);
        }
        
        #endregion

    }
}