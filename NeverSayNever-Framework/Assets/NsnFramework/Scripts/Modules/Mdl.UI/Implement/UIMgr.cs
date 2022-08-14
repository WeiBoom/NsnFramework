using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    public class UIMgr : IUIMgr
    {
        private Dictionary<string, UIConfigInfo> mUIConfigInfo;

        private Stack<UIBaseMessenger> mUIOpenStack;

        public void OnInitialize(params object[] args)
        {
            // todo
        }

        public void OnUpdate(float deltaTime)
        {
            // todo
        }

        public void OnDispose()
        {
            // todo
        }

        public void ClosePanel(string panelName)
        {
           // todo
        }

        public T GetUIMessenger<T>(string moduleName) where T : UIBaseMessenger
        {
            // todo
            return default(T);
        }

        public void HidePanel(string panelName)
        {
           // todo
        }

        public bool IsUIOpen(int id)
        {
            // todo
            return false;
        }




        public void OpenPanel(string panelName, params object[] args)
        {
           // todo
        }

        public void RegisterCsPanel(string panelName, UIPanelMessenger panelMessenger)
        {
           // todo
        }

        public void RegisterCsPanelByReflect(string moduleName)
        {
           // todo
        }

        public void RegisterLuaPanel(string panelName)
        {
           // todo
        }
    }
}
