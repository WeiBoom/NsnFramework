namespace Nsn
{

    using System;

    public interface IObjectPool
    {
    }
    
    public interface IObjectPool<T> : IObjectPool
    {
        T Get(System.Func<T> func = null);
        void Release(T element);
    }
    
    public interface IObjectPoolMgr : IManager
    {
        T GetPool<T>() where T : IObjectPool;

        void ReleasePool<T>() where T : IObjectPool;

        IObjectPool<T> CreatePool<T>(Action<T> getAction = null, Action<T> releaseAction = null) where T : new();
    }
    
    

}