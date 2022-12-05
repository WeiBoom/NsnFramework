using NeverSayNever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Nsn
{
    [System.Serializable]
    public class UIViewConfig
    {
        public int ID;
        public string Name;
        public int LayerID;
    }

    [System.Serializable]
    public class UIViewInfo
    {
        public enum UILayer
        {
            Window = 0,
            Widget = 1,
            Popup = 2,
            System = 3,
        }

        public UIViewConfig Config;

        public UILayer Layer => (UILayer)Config.LayerID;
        public string ViewName => Config.Name;
        public int ViewID => Config.ID;

        public UIViewInfo(UIViewConfig config)
        {
            Config = config;
        }
    }


    public class UIMdl
    {
        public Dictionary<int, string> m_UIViewID2NameDic = new Dictionary<int, string>();
        public Dictionary<string, UIViewInfo> m_UIViewInfoDic = new Dictionary<string, UIViewInfo>();

        public Stack<UIViewInfo> m_UIViewStack = new Stack<UIViewInfo>();

        public void Initialzie()
        {
            // TODO 初始化所有UIView的信息
            
        }

        private void CreateUIViewInfo(UIViewConfig config)
        {
            UIViewInfo viewInfo = new UIViewInfo(config);
            m_UIViewID2NameDic.Add(config.ID, config.Name);
            m_UIViewInfoDic.Add(config.Name, viewInfo);
        }

        private UIViewInfo GetUIViewInfo(int id)
        {
            m_UIViewID2NameDic.TryGetValue(id, out string uiName);
            return GetUIViewInfo(uiName);
        }

        private UIViewInfo GetUIViewInfo(string uiName)
        {
            if (string.IsNullOrEmpty(uiName))
                return null;
            m_UIViewInfoDic.TryGetValue(uiName, out UIViewInfo uiViewInfo);
            return uiViewInfo;
        }

    }
}
