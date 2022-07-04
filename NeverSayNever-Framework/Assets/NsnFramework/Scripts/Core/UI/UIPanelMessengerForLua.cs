
using System;
using UnityEngine;
using XLua;

namespace NeverSayNever
{
    
    

    public class UIPanelMessengerForLua : UIBaseMessenger
    {
        public bool IsInitialized { get; private set; }

        public bool IsShow { get; private set; }

        public bool IsClosed { get; private set; }

        private UIPanelInfo panelInfo;

        private LuaTable luaTable;
        private EventCallbackBool<object> onPreOpen;
        private EventCallback<GameObject> onInitFunc;
        private EventCallback onShownFunc;
        private EventCallback onHideFunc;
        private EventCallback onCloseFunc;
        private EventCallback<object> onHandelMsgFunc;


        public GameObject PanelObj { get; private set; }

        public UIPanelMessengerForLua(string panelName) : base(panelName)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void OnInit()
        {
            if (IsInitialized) return;

            IsInitialized = true;
            IsClosed = false;

            luaTable = LuaManager.Instance.CallNewLuaWindowFunc(PanelName);
            if (luaTable == null)
            {
                ULog.Error(($"获取Lua脚本失败 Name : {PanelName}"));
                return;
            }
            // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            var meta = LuaManager.Instance.NewTable();
            meta.Set("__index", LuaManager.Global);
            luaTable.SetMetaTable(meta);
            meta.Dispose();
            luaTable.Set("self", this);

            luaTable.Get("OnPreOpen", out onPreOpen);
            luaTable.Get("OnAwake", out onInitFunc);
            luaTable.Get("OnShown", out onShownFunc);
            luaTable.Get("OnHide", out onHideFunc);
            luaTable.Get("OnClose", out onCloseFunc);
            luaTable.Get("HandleMessage", out onHandelMsgFunc);

        }
        public void OnOpenPanel(GameObject panelObj)
        {
            PanelObj = panelObj;
            if (panelInfo == null)
            {
                panelInfo = panelObj.GetComponent<UIPanelInfo>();
                panelInfo.InitCollectedUIComponents();
            }

            if (!IsShow)
            {
                onInitFunc?.Invoke(panelObj);
            }
            else
            {
                IsShow = true;
                onShownFunc?.Invoke();
            }
        }

        public override bool OnPreOpen(params object[] args)
        {
            if (onPreOpen == null)
                return true;
            return onPreOpen.Invoke(args);
        }

        public override void OnPreHide(params object[] args)
        {
            IsShow = false;
            onHideFunc?.Invoke();
        }

        public override void OnPreClose(params object[] args)
        {
            IsClosed = true;
            onCloseFunc?.Invoke();
        }

        public override void OnSendMsg(params object[] args)
        {
        }

        public override void OnReceiveMsg(params object[] args)
        {
            onHandelMsgFunc?.Invoke(args);
        }
    }
}
