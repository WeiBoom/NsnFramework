
namespace NeverSayNever
{
    public interface IEventDispatcher
    {
        void UpdateListener();

        void AddListener(System.Enum eventId, System.Delegate eventFunc, bool isFirst = false);

        void RemoveListener();
        void RemoveListener(System.Enum eventId);
        void RemoveListener(System.Enum eventId, System.Delegate eventFunc);

        bool HasListener(System.Enum eventId);
        bool HasListener(System.Enum eventId, System.Delegate eventFunc);

        void DispatchEvent(System.Enum eventId);
        void DispatchEvent<T>(System.Enum eventId, T arg0);
        void DispatchEvent<T, T1>(System.Enum eventId, T arg0, T1 arg1);
        void DispatchEvent<T, T1, T2>(System.Enum eventId, T arg0, T1 arg1, T2 arg2);
        void DispatchEvent<T, T1, T2, T3>(System.Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3);
        void DispatchEvent<T, T1, T2, T3, T4>(System.Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4);
        void DispatchEvent<T, T1, T2, T3, T4, T5>(System.Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
        void DispatchEvent<T, T1, T2, T3, T4, T5, T6>(System.Enum eventId, T arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

    }
}