
using System;

namespace NeverSayNever
{
    public interface IUIMgr : IManager
    {

        void RegisterCsPanel(string panelName, UIPanelMessenger panelMessenger);

        void RegisterCsPanelByReflect(string moduleName);

        void RegisterLuaPanel(string panelName);

        void OpenPanel(string panelName, params object[] args);

        void HidePanel(string panelName);

        void ClosePanel(string panelName);

        T GetUIMessenger<T>(string moduleName) where T : UIBaseMessenger;
    }

}
