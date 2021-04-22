using System;
using System.Collections.Generic;

namespace NeverSayNever.Core.Event
{

    public enum EventDelegateState
    {
        None = 0,
        Continue = 1,
        Fail = 2,
    }

    public delegate EventDelegateState GameEventDeleglate(EventData e);

    public class EventData
    {
        public Enum eventType;
        public int eventId;
        public int param1;
        public int param2;
        public float param3;
        public ulong param4;
        public object paramObj1;
        public object paramObj2;

        public EventData()
        {
        }

        public EventData(Enum type, int id, int p1, int p2, object obj1, object obj2, float p3, ulong p4)
        {
            eventType = type;
            eventId = id;
            param1 = p1;
            param2 = p2;
            param3 = p3;
            param4 = p4;

            paramObj1 = obj1;
            paramObj2 = obj2;
        }

        public void ClearObj()
        {
            paramObj1 = paramObj2 = null;
        }
    }

    public sealed  class EventListener : GameModule
    {
        public class InternalEvent
        {
            public WeakReference Data;
            public WeakReference List;
        }
        
        private Dictionary<int, List<GameEventDeleglate>> m_eventDic;

        private Queue<InternalEvent> m_eventQueue;
        private Queue<InternalEvent> m_eventPool;

        private int m_maxEventCount = 10;
            
        public override void OnInitialize()
        {
            base.OnInitialize();
            m_eventDic = new Dictionary<int, List<GameEventDeleglate>>();
            m_eventQueue = new Queue<InternalEvent>(m_maxEventCount);
            m_eventPool = new Queue<InternalEvent>(m_maxEventCount);
            for (int i = 0; i < m_maxEventCount; i++)
            {
                m_eventPool.Enqueue(new InternalEvent());
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);

            if (m_eventQueue == null)
                return;
            int count = m_eventQueue.Count;
            for (int i = 0; i < count; i++)
            {
                InternalEvent e = m_eventQueue.Dequeue();
                if (e.Data == null || e.List == null || e.Data.IsAlive == false || e.List.IsAlive == false)
                    continue;
                EventData data = e.Data.Target as EventData;
                List<GameEventDeleglate> list = e.List.Target as List<GameEventDeleglate>;
                for (int j = 0; j < list.Count; j++)
                {
                    GameEventDeleglate gameEvent = list[j];
                    gameEvent?.Invoke(data);
                }

                e.Data = null;
                e.List = null;
                m_eventPool.Enqueue(e);
            }
        }

        public override void OnDispose()
        {
            base.OnDispose();
            m_eventDic.Clear();
            m_eventQueue.Clear();
            m_eventPool.Clear();
            m_eventDic = null;
            m_eventPool = null;
            m_eventQueue = null;
        }

        public void RegisterEvent(int eventId, GameEventDeleglate gameEvent)
        {
            m_eventDic.TryGetValue(eventId, out var eventList);
            if (eventList != null)
            {
                if(!eventList.Contains(gameEvent))
                {
                    eventList.Add(gameEvent);
                }
                else
                {
                    UnityEngine.Debug.LogError(" Register Event Error! ");
                }
            }
            else
            {
                eventList = new List<GameEventDeleglate>();
                eventList.Add(gameEvent);
                m_eventDic.Add(eventId,eventList);
            }
        }

        public void RemoveEvent(int eventId)
        {
            if (m_eventDic.TryGetValue(eventId, out var eventList))
            {
                m_eventDic.Remove(eventId);
            }
        }

        public void RemoveEvent(int eventId, GameEventDeleglate gameEvent)
        {
            if (m_eventDic.TryGetValue(eventId, out var eventList))
            {
                if (eventList.Contains(gameEvent))
                {
                    eventList.Remove(gameEvent);
                }
            }
        }

        public void DispatchEvent(int eventId, EventData eventData)
        {
            m_eventDic.TryGetValue(eventId, out var eventList);
            if(eventList != null && eventList.Count > 0)
            {
                InternalEvent e = m_eventPool.Dequeue();
                if (e != null)
                {
                    e.Data = new WeakReference(eventData);
                    e.List = new WeakReference(eventList);
                    
                    m_eventQueue.Enqueue(e);
                }
            }
        }

        public bool HasEvent(int eventId)
        {
            return m_eventDic.ContainsKey(eventId);
        }
    }
}