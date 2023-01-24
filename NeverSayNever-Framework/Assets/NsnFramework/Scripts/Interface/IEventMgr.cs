using System;

namespace Nsn
{
    public interface IEventManager : IManager
    {
        void RegisterEvent(int eventId, EventDelegate gameEvent);

        void RemoveEvent(int eventId, EventDelegate gameEvent);

        void RemoveEvent(int eventId);

        void DispatchEvent(int eventId, EventData eventData);

        bool HasEvent(int eventId);
    }

    public delegate EventDelegateState EventDelegate(EventData e);

    public enum EventDelegateState
    {
        None = 0,
        Continue = 1,
        Fail = 2,
        Success = 3,
    }

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



}