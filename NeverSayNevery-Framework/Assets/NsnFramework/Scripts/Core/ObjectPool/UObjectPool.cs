using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NeverSayNever.Core.ObjectPool
{
    public class UObjectPool<T> where T : class
    {
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly UnityAction<T> _actionOnGet;
        private readonly UnityAction<T> _actionOnRelease;

        private readonly object _objLock = new object();

        public int CountAll { get; private set; }
        public int CountActive => CountAll - CountInactive;
        public int CountInactive => _stack?.Count ?? 0;

        public UObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
        {
            _actionOnGet = actionOnGet;
            _actionOnRelease = actionOnRelease;
        }

        public T Get()
        {
            lock (_objLock)
            {
                T element = _stack.Pop();
                _actionOnGet?.Invoke(element);
                return element;
            }
        }

        public void Release(T element)
        {
            lock (_objLock)
            {
                if (_stack.Count > 0 && ReferenceEquals(_stack.Peek(), element))
                    Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
                _actionOnRelease?.Invoke(element);
                _stack.Push(element);
            }
        }
    }
}
