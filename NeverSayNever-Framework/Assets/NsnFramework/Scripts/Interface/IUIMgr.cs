
using System;

namespace NeverSayNever
{
    [System.Obsolete("�ɽӿڣ�����")]
    public interface IUIMgr : IManager
    {
        T GetUIMessenger<T>(string moduleName) where T : UIBaseMessenger;

        void Register(string uiName, UIConfigInfo config);

        void OpenPanel(string panelName, params object[] args);

        void HidePanel(string panelName);

        void ClosePanel(string panelName);

        bool IsUIOpen(int id);
    }

}
