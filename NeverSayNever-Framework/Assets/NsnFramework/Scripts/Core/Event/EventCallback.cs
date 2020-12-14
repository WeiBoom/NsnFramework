namespace NeverSayNever.Core.Event
{
    public delegate void EventCallback();
    public delegate void EventCallback<in T>(T t);
    public delegate void EventCallback<in T, in T1>(T t, T1 t1);
    public delegate void EventCallback<in T, in T1, in T2>(T t, T1 t1, T2 t2);
    public delegate void EventCallback<in T, in T1, in T2, in T3>(T t, T1 t1, T2 t2, T3 t3);
    public delegate void EventCallback<in T, in T1, in T2, in T3, in T4>(T t, T1 t1, T2 t2, T3 t3, T4 t4);
    public delegate void EventCallback<in T, in T1, in T2, in T3, in T4, in T5>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
    public delegate void EventCallback<in T, in T1, in T2, in T3, in T4, in T5, in T6>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

    public delegate bool EventCallbackBool();
    public delegate bool EventCallbackBool<in T>(T t);
    public delegate bool EventCallbackBool<in T, in T1>(T t, T1 t1);
    public delegate bool EventCallbackBool<in T, in T1, in T2>(T t, T1 t1, T2 t2);
    public delegate bool EventCallbackBool<in T, in T1, in T2, in T3>(T t, T1 t1, T2 t2, T3 t3);
    public delegate bool EventCallbackBool<in T, in T1, in T2, in T3, in T4>(T t, T1 t1, T2 t2, T3 t3, T4 t4);
    public delegate bool EventCallbackBool<in T, in T1, in T2, in T3, in T4, in T5>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
    public delegate bool EventCallbackBool<in T, in T1, in T2, in T3, in T4, in T5, in T6>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);

    public delegate object EventCallbackObj();
    public delegate object EventCallbackObj<in T>(T t);
    public delegate object EventCallbackObj<in T, in T1>(T t, T1 t1);
    public delegate object EventCallbackObj<in T, in T1, in T2>(T t, T1 t1, T2 t2);
    public delegate object EventCallbackObj<in T, in T1, in T2, in T3>(T t, T1 t1, T2 t2, T3 t3);
    public delegate object EventCallbackObj<in T, in T1, in T2, in T3, in T4>(T t, T1 t1, T2 t2, T3 t3, T4 t4);
    public delegate object EventCallbackObj<in T, in T1, in T2, in T3, in T4, in T5>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
    public delegate object EventCallbackObj<in T, in T1, in T2, in T3, in T4, in T5, in T6>(T t, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6);
}