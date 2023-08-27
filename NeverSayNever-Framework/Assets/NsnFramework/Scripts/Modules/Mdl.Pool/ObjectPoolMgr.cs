using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Nsn
{
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
                // 需要释放池，并释放池中所有未被使用的对象
                //pool.Clear();
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
}