using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public class SingletonMono<T> : GameBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object Lock = new object();

        private bool _bInitialized;
        private static bool _bClearMode;
        private static bool _bApplicationQuitting;

        // 是否初始化的标记

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (_bApplicationQuitting) return null;
                        CreateInstance();
                        return _instance;
                    }
                }
                return _instance;
            }
        }

        // 创建实例
        private static void CreateInstance()
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    var singleton = new GameObject($"[{typeof(T)}]");
                    var inst = singleton.AddComponent<T>() as SingletonMono<T>;
                    inst.OnInitialize();
                }
            }
        }

        // 销毁实例
        public static void DestroyInstance()
        {
            lock (Lock)
            {
                if (_instance == null) return;
                _bClearMode = true;
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            OnInitialize();
        }

        // 初始化
        public virtual void OnInitialize()
        {
            if (_bInitialized) return;
            GameObject o;
            _instance = (o = gameObject).GetComponent<T>();
            _bInitialized = true;
            DontDestroyOnLoad(o);
        }

        public virtual void OnDispose()
        {
            _instance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            if (_bClearMode == false)
                _bApplicationQuitting = true;
            _bClearMode = false;
        }
    }
}