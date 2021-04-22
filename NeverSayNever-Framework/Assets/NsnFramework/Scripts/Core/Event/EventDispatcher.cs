using System;
using System.Collections;
using System.Collections.Generic;

namespace NeverSayNever.Core.Event
{
    public class EventDispatcher : IEventDispatcher
    {
        
        /// <summary>
        /// 能同时异步调用的最大事件数量
        /// </summary>
        public static ushort SameSyncEventMax = 10;
        /// <summary>
        /// 所有监听的事件
        /// </summary>
        private readonly Dictionary<Enum, List<Delegate>> _mEventDic;
        /// <summary>
        /// 只监听一次的事件
        /// </summary>
        private readonly Dictionary<Enum, List<Delegate>> _mUseOnceEventDic;
        /// <summary>
        /// 等待执行的事件队列
        /// </summary>
        private readonly Queue<Enum> _mReadyToExecuteEventQueue;

        protected EventDispatcher()
        {
            _mEventDic = new Dictionary<Enum, List<Delegate>>(100);
            _mUseOnceEventDic = new Dictionary<Enum, List<Delegate>>(10);
            _mReadyToExecuteEventQueue = new Queue<Enum>(20);
        }

        public void UpdateListener()
        {
            if(_mReadyToExecuteEventQueue == null || _mReadyToExecuteEventQueue.Count <= 0)
                return;

            var listenerId = _mReadyToExecuteEventQueue.Dequeue();
        }

        public void AddListener(Enum eventId, Delegate eventFunc, bool isFist = false)
        {
            var eventList = _mEventDic.ContainsKey(eventId) ? _mEventDic[eventId] : null;
            if (eventList == null)
            {
                _mEventDic[eventId] = new List<Delegate>();
            }

            if (_mEventDic[eventId].Contains(eventFunc))
            {
                //Debug.LogError("EventId : " + eventId + " 已经存在相同的事件");
                return;
            }

            if (isFist)
                _mEventDic[eventId].Insert(0, eventFunc);
            else
                _mEventDic[eventId].Add(eventFunc);
        }

        public bool HasListener(Enum eventId)
        {
            return _mEventDic.ContainsKey(eventId);
        }

        public bool HasListener(Enum eventId,Delegate eventFunc)
        {
            _mEventDic.TryGetValue(eventId, out var eventList);
            return eventList != null && eventList.Contains(eventFunc);
        }

        public void RemoveListener()
        {
            for(int i  =0;i < _mEventDic.Count;i++)
            {

            }


            _mEventDic.Clear();
            _mUseOnceEventDic.Clear();
        }

        public void RemoveListener(Enum eventId)
        {
            if (_mEventDic.ContainsKey(eventId))
            {
                _mEventDic.Remove(eventId);
            }

            if (_mUseOnceEventDic.ContainsKey(eventId))
            {
                _mUseOnceEventDic.Clear();
            }
        }

        public void RemoveListener(Enum eventId, Delegate eventFunc)
        {
            if (_mEventDic.ContainsKey(eventId) && _mEventDic[eventId].Contains(eventFunc))
                _mEventDic[eventId].Remove(eventFunc);

            if (_mUseOnceEventDic.ContainsKey(eventId) && _mUseOnceEventDic[eventId].Contains(eventFunc))
                _mUseOnceEventDic[eventId].Remove(eventFunc);
        }

        public void DispatchEvent(Enum eventId)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];

            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback;
                try
                {
                    callback?.Invoke();
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T>(Enum eventId, T arg0)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];
            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T>;
                try
                {
                    callback?.Invoke(arg0);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T, T1>(Enum eventId, T arg0, T1 arg1)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];

            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T,T1>;
                try
                {
                    callback?.Invoke(arg0, arg1);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T, T1, T2>(Enum eventId, T arg0, T1 arg1, T2 arg2)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];
            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T, T1, T2>;
                try
                {
                    callback?.Invoke(arg0, arg1, arg2);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T, T1, T2, T3>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];

            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T, T1, T2, T3>;
                try
                {
                    callback?.Invoke(arg0, arg1, arg2, arg3);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T, T1, T2, T3, T4>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];
            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T, T1, T2, T3, T4>;
                try
                {
                    callback?.Invoke(arg0, arg1, arg2, arg3, arg4);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T, T1, T2, T3, T4, T5>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];

            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T, T1, T2, T3, T4,T5>;
                try
                {
                    callback?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }

        public void DispatchEvent<T, T1, T2, T3, T4, T5, T6>(Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            if (!HasListener(eventId)) return;
            var eventList = _mEventDic[eventId];

            for (short i = 0; i < eventList.Count; i++)
            {
                var callback = eventList[i] as EventCallback<T, T1, T2, T3, T4, T5,T6>;
                try
                {
                    callback?.Invoke(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
                }
                catch (Exception error)
                {
                    Console.WriteLine(error);
                    throw;
                }
            }
            //Debug.LogError("EventId : " + eventId + " 事件不存在");
        }
    }
}

