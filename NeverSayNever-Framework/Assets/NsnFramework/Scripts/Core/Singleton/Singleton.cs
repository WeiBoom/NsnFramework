using UnityEngine;
using System.Collections;
using System;

namespace NeverSayNever
{
    public class Singleton<T> :ISingleton where T : class, new()
    {
        private volatile static T _instance;

        // 必须加锁 synchronized 才能保证单例，但加锁会影响效率
        public static T Inst => GetInstance();

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static T GetInstance()
        {
            if (_instance == null)
                _instance = Activator.CreateInstance<T>();
            else
                Debug.LogError($"已经存在相同的Singleton : {typeof(T)}");
            return _instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == default(T)) return;
            (_instance as Singleton<T>)?.OnDispose();
            _instance = default;
        }

        public virtual void OnInitialize(params object[] args)
        {
        }

        public virtual void OnUpdate()
        {
        }
        
        public virtual void OnDispose()
        {
        }
    }
}

