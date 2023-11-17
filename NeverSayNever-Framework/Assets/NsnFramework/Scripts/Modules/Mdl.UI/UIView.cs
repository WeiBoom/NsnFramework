using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    public abstract class UIView
    {
        private object[] m_UserDatas;
        
        private GameObject m_Panel;
        private Transform m_Trans;
        private Canvas m_Canvas;
        private Canvas[] m_SubCanvas;
        private GraphicRaycaster m_Raycaster;
        private GraphicRaycaster[] m_SubRaycaster;

        public Transform transform => m_Trans;
        public GameObject gameObject => m_Panel;
        
        public string ViewName { get; private set; }
        
        public int ViewLayer { get; private set; }

        public bool FullScreen { get; private set; }

        public System.Object UserData
        {
            get
            {
                if (m_UserDatas != null && m_UserDatas.Length > 0)
                    return m_UserDatas[0];
                else
                    return null;
            }
        }

        public System.Object[] UserDatas => m_UserDatas;

        public void Init(string name, int layer, bool fullScreen)
        {
            ViewName = name;
            ViewLayer = layer;
            FullScreen = fullScreen;
        }


        public abstract void OnCreate();

        public abstract void OnRefresh();

        public abstract void OnUpdate();
        
        public abstract void OnDestroy();



        private void OnCompleted()
        {
        }
    }

}
