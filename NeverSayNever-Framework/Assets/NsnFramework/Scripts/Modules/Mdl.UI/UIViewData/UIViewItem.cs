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

        public GameObject ViewObj => m_ViewObj;
        public string ViewName => m_ViewName;
        public int ViewID => m_ViewID;
        public int Layer => m_ViewLayer;

        private bool m_IsCreate;
        
        public UIViewItem(string name, int id)
        {
            m_ViewName = name;
            m_ViewID = id;
            m_ViewLayer = 0;
        }

        // 创建界面
        public void OnCreate(GameObject viewObj)
        {
            m_ViewObj = viewObj;
        }

        // 刷新UI
        public void OnRefresh(object[] userData)
        {
            m_UserData = userData;

        }

        // 关闭UI
        public void OnClose()
        {
            
        }

        // 销毁UI
        public void OnDestroy()
        {
            
        }
    }
}