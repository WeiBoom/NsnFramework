using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T s_instance;

        private static bool m_bInitialized = false;

        public static T Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = (T)FindObjectOfType(typeof(T));
                    if (s_instance == null)
                    {
                        var singleton = new GameObject($"[{typeof(T)}]");
                        s_instance = singleton.AddComponent<T>();
                    }
                    if(!m_bInitialized)
                    {
                        m_bInitialized = true;
                        s_instance.OnInitialize();
                    }
                }

                return s_instance;
            }
        }


        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            OnDispose();
        }

        private void OnApplicationQuit()
        {
            s_instance = null;
        }

        public virtual void OnInitialize()
        {

        }

        public virtual void OnDispose()
        {
            s_instance = null;
            Destroy(this.gameObject);
        }

    }
}