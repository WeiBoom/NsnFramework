using UnityEditor;
using UnityEngine;

namespace Nsn
{
    public class UIViewItem
    {
        public static UIViewItem Empty => default(UIViewItem);

        private GameObject m_ViewObj;
        private string m_ViewName;
        private int m_ViewID;
        private int m_ViewLayer;
        private object[] m_UserData;
        private bool m_FullScreen;
        private bool m_IsCreate;

        public GameObject ViewObj => m_ViewObj;
        public string ViewName => m_ViewName;
        public int ViewID => m_ViewID;
        public int Layer => m_ViewLayer;
        public bool FullScreen => m_FullScreen;

        
        public UIViewItem(string name, int id)
        {
            m_ViewID = id;
            m_ViewName = name;
            m_ViewLayer = 0;
        }

        public void OnCreate(GameObject viewObj)
        {
            m_ViewObj = viewObj;
        }

        public void OnRefresh(object[] userData)
        {
            m_UserData = userData;
        }

        public void OnClose()
        {
        }

        public void OnDestroy()
        {
            UnityEngine.Object.Destroy(m_ViewObj);
        }
    }
}