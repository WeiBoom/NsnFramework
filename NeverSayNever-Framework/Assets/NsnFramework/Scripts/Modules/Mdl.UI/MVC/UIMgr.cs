using System.Collections.Generic;
using UnityEngine;

namespace Nsn.MVC
{
    public class UIMgr : IUIMgr
    {
        /// <summary>
        /// 所有存活的UI
        /// </summary>
        private List<UIViewData> m_UIViewStack;
        /// <summary>
        /// 单独展示的界面
        /// </summary>
        private Dictionary<string,UIViewData> m_UISingletonViewStack;

        private Camera m_UICamera2D;

        private Vector2 m_DesignResolution = new Vector2(2400, 1080);
        
        public Camera UICamera2D => m_UICamera2D;
        public Vector2 DesignResolution => m_DesignResolution;

        public void OnInitialized(params object[] args)
        {
            m_UICamera2D = Camera.main;
            m_DesignResolution = new Vector2(2400, 1080);

            m_UIViewStack = new List<UIViewData>();
            m_UISingletonViewStack = new Dictionary<string, UIViewData>();
            
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnDisposed()
        {
        }

        public void Open(string viewName, params object[] args)
        {
            if (m_UIViewStack.Count > 60)
            {
                NsnLog.Error("打开了太多的界面!");
                return;
            }

            var view = m_UISingletonViewStack[viewName];
            if (view == null)
            {
                var viewData = new UIViewData();
            }

            #if UNITY_EDITOR
             // todo 记录打开的时间
            #endif

            if (view.Ctrl != null)
            {
                
            }
            else
            {
                OnUIViewNetRequireCompleted(viewName, args);
            }
            
        }

        public void Close(string view)
        {
        }

        public bool IsOpened(string view)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// 请求界面所需的网络数据完成后的回调
        /// </summary>
        private void OnUIViewNetRequireCompleted(string viewName,params object[] args)
        {
            
        }
    }
}