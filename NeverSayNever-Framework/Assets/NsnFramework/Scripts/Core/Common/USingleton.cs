using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Core
{
    public class USingleton<T> : UGameBehaviour where T : MonoBehaviour
    {
        private bool _isInitialized = false;
        private static T _instance;
        private static readonly object Lock = new object();

        private static bool ClearMode;
        private static bool ApplicationIsQuitting;

        // 是否初始化的标记

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        if (ApplicationIsQuitting) return null;
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
                    var inst = singleton.AddComponent<T>() as USingleton<T>;
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
                ClearMode = true;
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
            if (_isInitialized) return;
            GameObject o;
            _instance = (o = gameObject).GetComponent<T>();
            _isInitialized = true;
            DontDestroyOnLoad(o);
        }

        public virtual void OnDispose()
        {
            _instance = null;
        }

        protected virtual void OnApplicationQuit()
        {
            if (ClearMode == false)
                ApplicationIsQuitting = true;
            ClearMode = false;
        }
    }
}