using UnityEngine;
using System.Collections;
using System;
using System.Runtime.CompilerServices;

namespace Nsn
{
    public class Singleton<T>  where T : class, new()
    {
        private volatile static T _instance;
        public static T Inst => GetInstance();

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static T GetInstance()
        {
            if (_instance == null)
                _instance = Activator.CreateInstance<T>();
            return _instance;
        }

        public static void DestroyInstance()
        {
            if (_instance == default(T)) return;
            (_instance as Singleton<T>)?.OnDispose();
            _instance = default;
        }

        public virtual void OnInitialize() { }

        public virtual void OnUpdate() { }

        public virtual void OnDispose(){}

    }

    public class SingletonSafe<T> where T : class, new()
    {
        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

        public static T Inst { get { return lazy.Value; } }

        public virtual void OnDispose(){}

        public virtual void OnInitialize(params object[] args){}

        public virtual void OnUpdate(){}
    }
}

