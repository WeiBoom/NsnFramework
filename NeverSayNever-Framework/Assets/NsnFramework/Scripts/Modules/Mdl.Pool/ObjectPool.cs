using System;
using System.Collections.Concurrent;

namespace Nsn
{
    internal class ObjectPool<T> :IObjectPool<T> where T : new()
    {
        private readonly ConcurrentStack<T> m_Stack;
        private readonly System.Action<T> m_ActionOnGet;
        private readonly System.Action<T> m_ActionOnRelease;
        private readonly System.Action<T> m_ActionOnDestroy;

        public int CountAll { get; private set; }
        public int CountActive => CountAll - CountInactive;
        public int CountInactive => m_Stack.Count;
        
        public ObjectPool(){}
        
        public ObjectPool(
            System.Action<T> actionOnGet = null,
            System.Action<T> actionOnRelease = null,
            System.Action<T> actionOnDestroy = null,
            int maxSize = 10000)
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

        public void Clear()
        {
            
        }
    }
}