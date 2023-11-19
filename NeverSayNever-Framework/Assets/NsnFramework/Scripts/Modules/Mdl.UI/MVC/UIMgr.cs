using System.Collections.Generic;
using UnityEngine;

namespace Nsn.MVC
{
    public class UIMgr
    {
        /// <summary>
        /// 所有存活的UI
        /// </summary>
        private List<UIViewData> m_UIViewStack;
        /// <summary>
        /// 单独展示的界面
        /// </summary>
        private Dictionary<string,UIViewData> m_UISingletonViewStack;

        private UIRoot m_UIRoot;
        // UI根节点, 包含UICamera 和 UICanvas 的引用
        public UIRoot UIRoot => m_UIRoot;

        public Camera UICamera2D => m_UIRoot.UICamera;
        public Canvas RootCanvas => m_UIRoot.UICanvas;
        public Vector2 DesignResolution => m_UIRoot.UIDesignResolution;

        private UILayers m_UILayers;
        private UIConfigs m_UIConfigs;

        /// <summary>
        /// 初始化UIMgr
        /// </summary>
        /// <param name="root"></param>
        public void SetUp(UIRoot root)
        {
            m_UIRoot = root;
            m_UIViewStack = new List<UIViewData>();
            m_UISingletonViewStack = new Dictionary<string, UIViewData>();

            SetupUIConfigs();
            SetupLayers();
            SetupBlurEffect();
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnDisposed()
        {
        }

        
        public void Register(int viewID)
        {
            // todo
        }

        private void SetupUIConfigs()
        {
            m_UIConfigs = new UIConfigs();
            m_UIConfigs.SetUp();
        }
        
        private void SetupLayers()
        {
            m_UILayers = new UILayers();
            m_UILayers.SetUp(UIRoot);
        }

        private void SetupBlurEffect()
        {
            
        }

        public void Open(string viewName, params object[] args)
        {
            if (m_UIViewStack.Count > 60)
            {
                NsnLog.Error("打开了太多的界面!");
                return;
            }

            m_UISingletonViewStack.TryGetValue(viewName, out var viewData);
            if (viewData == null)
            {
                viewData = new UIViewData();
                viewData.Name = viewName;
                viewData.Layer = m_UIConfigs.GetConfig(viewName).Layer;
            }

            if (viewData.Ctrl != null)
            {
                
            }
            else
            {
                OnUIViewNetRequireCompleted(viewName, args);
            }
        }

        private void InternalInitUIView(string viewName,BaseUIView view, string parentViewName)
        {
            UIViewData viewData = m_UIConfigs.GetConfig(viewName);
            UILayerData layerData = viewData.Layer;
            
            view.SetName(viewName);
            if (!string.IsNullOrEmpty(parentViewName))
            {
                UIViewData parentViewData = GetViewData(parentViewName);
                if (parentViewData != null)
                {
                    // todo
                }
            }
        }

        public void Close(string view)
        {
        }

        public bool IsOpened(string view)
        {
            throw new System.NotImplementedException();
        }

        public UIViewData GetViewData(string viewName)
        {
            UIViewData targetViewData = null;
            foreach (var viewData in m_UIViewStack)
            {
                if (viewData.Name == viewName)
                {
                    targetViewData = viewData;
                    break;
                }
            }

            if (targetViewData == null)
                m_UISingletonViewStack.TryGetValue(viewName, out targetViewData);

            return targetViewData;
        }
        
        /// <summary>
        /// 请求界面所需的网络数据完成后的回调
        /// </summary>
        private void OnUIViewNetRequireCompleted(string viewName,params object[] args)
        {
            
        }
    }
}