namespace Nsn
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Concurrent;



    public class ObjectPoolMgr : IObjectPoolMgr
    {
        private Dictionary<System.Type, IObjectPool> m_ObjectPoolDic;

        public void OnInitialized(params object[] args)
        {
            m_ObjectPoolDic = new Dictionary<Type, IObjectPool>();
        }

        public void OnUpdate(float deltaTime)
        {
            // nothing to do
        }

        public void OnDisposed()
        {
            m_ObjectPoolDic = null;
        }

        public T GetPool<T>() where T : IObjectPool
        {
            IObjectPool pool = GetCachedPool<T>();
            // 这里只获取，不创建
            //if (pool == null)
                //pool = CreatePool<T>();
            return (T)pool;
        }

        public void ReleasePool<T>() where T : IObjectPool
        {
            var pool = GetCachedPool<T>() as IObjectPool<T>;
            if (pool != null)
            {
                // todo 这里需要释放池，并释放池中所有未被使用的对象
                //pool.Release();
            }
        }

        public IObjectPool<T> CreatePool<T>(System.Action<T> getAction = null, System.Action<T> releaseAction = null) where T : new()
        {
            System.Type type = typeof(T);
            IObjectPool pool = null;
            if (m_ObjectPoolDic.TryGetValue(type, out pool))
            {
                NsnLog.Warning($"已经存在 {type} 类型的对象池!");
                return pool as IObjectPool<T>;
            }
            pool = new ObjectPool<T>(getAction, releaseAction);
            m_ObjectPoolDic.Add(type, pool);
            return pool as IObjectPool<T>;
;        }

        private T GetCachedPool<T>()
        {
            System.Type type = typeof(T);
            m_ObjectPoolDic.TryGetValue(type, out var pool);
            return (T)pool;
        }
    }
    
    internal class ObjectPool<T> :IObjectPool<T> where T : new()
    {
        private readonly ConcurrentStack<T> m_Stack;
        private readonly System.Action<T> m_ActionOnGet;
        private readonly System.Action<T> m_ActionOnRelease;
        
        public int CountAll { get; private set; }
        public int CountActive => CountAll - CountInactive;
        public int CountInactive => m_Stack.Count;
        
        public ObjectPool(){}
        
        public ObjectPool(System.Action<T> actionOnGet, System.Action<T> actionOnRelease)
        {
            m_Stack = new ConcurrentStack<T>();
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }
        
        public ObjectPool(int capacity, System.Action<T> actionOnGet, System.Action<T> actionOnRelease)
        {
            m_Stack = new ConcurrentStack<T>();
            m_ActionOnGet = actionOnGet;
            m_ActionOnRelease = actionOnRelease;
        }
        
        public T Get(Func<T> func = null)
        {
            T element;
            if (!m_Stack.TryPop(out element))
            {
                if (func == null)
                    element = new T();
                else
                    element = func();
                CountAll++;
            }
            
            m_ActionOnGet?.Invoke(element);
            return element;
        }

        public void Release(T element)
        {
            if (m_Stack.TryPeek(out var temp))
            {
                if (ReferenceEquals(temp, element))
                {
                    NsnLog.Error("目标对象池已经被释放!");
                    return;
                }
            }
            m_ActionOnRelease?.Invoke(element);
            m_Stack.Push(element);
        }
        
    }
}