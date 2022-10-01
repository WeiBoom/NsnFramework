using System;
using System.Collections.Generic;

namespace NeverSayNever
{
    public class UIMgr : IUIMgr
    {
        private Dictionary<string, UIConfigInfo> mUIConfigInfo;

        private Stack<uint> mUIScreenStack;

        private Stack<uint> mUIDialogStack;

        public void OnInitialize(params object[] args)
        {
            string uiconfigsPath = "";
            mUIScreenStack = new Stack<uint>();
            mUIDialogStack = new Stack<uint>();
            var uiJsonConfigs = Newtonsoft.Json.JsonConvert.DeserializeObject<UIConfigInfo>(uiconfigsPath);
        }

        public void OnUpdate(float deltaTime)
        {
            // todo
        }

        public void OnDispose()
        {
        }

        public void Register(string name, UIConfigInfo config)
        {

        }

        public void ClosePanel(string panelName)
        {
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
