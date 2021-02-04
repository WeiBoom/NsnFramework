using System.Collections.Generic;
using UnityEngine;
using NeverSayNever;
using System;

namespace NeverSayNever.Core
{
    using NeverSayNever.Core.HUD;
    using NeverSayNever.Core.Asset;
    using NeverSayNever.Utilities;

    public class UIManager : Singleton<UIManager>
    {
        /// <summary>
        /// 界面注册信息
        /// </summary>
        class UIRegisterInfo
        {
            public Type Type;
            public bool IsLua;
            public string ScriptName;
            public UIBaseMessenger Messenger;
        }

        /// <summary>
        /// 保存所有的界面的信息
        /// </summary>
        private readonly Dictionary<string, UIRegisterInfo> _allPanelInfoDic = new Dictionary<string, UIRegisterInfo>();

        /// <summary>
        /// 所有UI界面
        /// </summary>
        private readonly Dictionary<string, UIBasePanel> _allPanelDic = new Dictionary<string, UIBasePanel>();

        /// <summary>
        /// 已经打开的界面
        /// </summary>
        private readonly Dictionary<string, UIBasePanel> _shownPanelDic = new Dictionary<string, UIBasePanel>();

        #region UI 节点属性

        // 界面根节点
        public Transform Root { get; private set; }

        // Panel缓存池节点
        public Transform PoolRoot { get; private set; }

        #endregion

        /// <summary>
        /// 初始化UI各节点
        /// </summary>
        public override void OnInitialize(params object[] args)
        {
            base.OnInitialize(args);
            try
            {
                var uiRoot = args[0] as GameObject;
                var uiPool = new GameObject("UIPool");
                Root = uiRoot.transform;
                PoolRoot = uiPool.transform;
                PoolRoot.position = Vector3.one * 1000000;
            }
            catch (Exception e)
            {
                Debug.LogError($"UIManager initialized failed, Error : {e}" );
                throw;
            }
        }

        /// <summary>
        /// 设置面板层级
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="layer"></param>
        private void SetRoot(Transform panel, EPanelLayer layer)
        {
            Transform panelRoot = Root;
            // 把界面放在UIRoot下并规范化大小
            panel.SetParentAndNormalized(panelRoot);
            // 默认新打开的界面都放在UI最前面显示
            panel.transform.SetAsLastSibling();
            var panelRect = panel.GetComponent<RectTransform>();
            //
            panelRect.sizeDelta = Vector2.zero;
            // 设置UI的锚点为覆盖整个UI面板
            UIAnchorTool.SetAnchor(panelRect, EUIAnchorPresets.StretchAll);
            // 设置轴心为中间
            UIAnchorTool.SetPivot(panelRect, EUIPivotPresets.MiddleCenter);
        }

        /// <summary>
        /// 注册界面
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isLua"></param>
        /// <param name="messenger"></param>
        private void RegisterPanel(string name, bool isLua, UIBaseMessenger messenger)
        {
            if(_allPanelInfoDic.ContainsKey(name))
            {
                Debug.LogError($"{name} 已经注册过");
                return;
            }
          
            var info = new UIRegisterInfo();
            info.IsLua = isLua;
            info.ScriptName = name;
            info.Type = messenger.GetType();
            info.Messenger = messenger;

            _allPanelInfoDic.Add(name, info);
            ULog.Print(($"注册界面 : {name}"));
        }

        /// <summary>
        /// 注册CS界面
        /// </summary>
        /// <param name="panelName"></param>
        /// <param name="panelMessenger"></param>
        public void RegisterCsPanel(string panelName, UIPanelMessenger panelMessenger)
        {
            RegisterPanel(panelName, false, panelMessenger);
        }

        /// <summary>
        /// 获取UIMessenger
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public T GetUIMessenger<T>(string moduleName) where T : UIBaseMessenger
        {
            _allPanelInfoDic.TryGetValue(moduleName, out var registerInfo);
            if(registerInfo != null)
            {
                return registerInfo.Messenger as T;
            }
            else
            {
                Debug.LogError($"模块 {moduleName} 没有注册.");
                return null;
            }
        }

        /// <summary>
        /// 通过反射生成messenger并实现类的注册
        /// </summary>
        /// <param name="moduleName">模块名</param>
        public void RegisterCsPanelByReflect(string moduleName)
        {
            var uiNamespace = Framework.GlobalConfig.UIScriptNamespace;

            var messengerName = uiNamespace.IsNullOrEmpty()? $"{moduleName}Messenger" :$"{uiNamespace}.{moduleName}Messenger" ;
            var panelName = $"{moduleName}Panel";
            var parameters = new object[] {panelName};

            var target = ScriptManager.Instance.CreateInstance(messengerName, parameters);
            if (target == null)
            {
                throw new System.Exception($"注册模块失败 : {moduleName}");
            }
            var panelMessenger = target as UIPanelMessenger;
            RegisterCsPanel(moduleName, panelMessenger);
        }

        /// <summary>
        /// 注册Lua界面
        /// </summary>
        public void RegisterLuaPanel(string panelName)
        {
            var bridge = new UIPanelMessengerForLua(panelName);
            RegisterPanel(panelName, true, bridge);
        }

        /// <summary>
        /// 打开界面
        /// </summary>
        /// <param name="panelName">面板名</param>
        /// <param name="args">开启界面传递的参数</param>
        public void OpenPanel(string moduleName, params object[] args)
        {
            _allPanelInfoDic.TryGetValue(moduleName, out var registerInfo);
            if (registerInfo == null)
            {
                Debug.LogError($"没找到界面信息，请先注册{ moduleName}");
                return;
            }

            if (registerInfo.IsLua)
            {
                OpenLuaPanel(registerInfo, args);
            }
            else
            {
                OpenCsPanel(registerInfo, args);
            }
        }

        /// <summary>
        /// 隐藏界面
        /// </summary>
        /// <param name="panelName"></param>
        public void HidePanel(string moduleName)
        {
            _allPanelInfoDic.TryGetValue(moduleName, out var info);
            if (info == null)
            {
                Debug.LogError($"注册列表中没有界面 {moduleName}");
                return;
            }
            info.Messenger.OnPreHide();
            _shownPanelDic.TryGetValue(info.Messenger.PanelName, out var panel);
            if (panel != null && panel.IsShow)
            {
                panel.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 关闭界面
        /// </summary>
        /// <param name="panelName">界面名字</param>
        /// <param name="isPlayCloseAnim">是否播放关闭动画</param>
        /// <param name="putInPool">是否把对象缓存起来</param>
        public void ClosePanel(string moduleName, bool putInPool = true)
        {
            _allPanelInfoDic.TryGetValue(moduleName, out var info);
            if(info == null)
            {
                Debug.LogError($"注册列表中没有界面 {moduleName}");
                return;
            }
            info.Messenger.OnPreClose();
            _shownPanelDic.TryGetValue(info.Messenger.PanelName, out var panel);
            if (panel != null)
            {
                if (putInPool)
                {
                    panel.gameObject.SetActive(false);
                    panel.transform.SetParent(PoolRoot);
                }
                else
                {
                    ResourceManager.ReleaseObject(panel.gameObject);
                }
                _shownPanelDic.Remove(info.Messenger.PanelName);
            }
        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="widgetName"></param>
        /// <param name="args"></param>
        public void OpenWidget(string widgetName, params object[] args)
        { 
            
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="widgerName"></param>
        public void CloseWidget(string widgetName)
        {

        }

        /// <summary>
        /// 打开界面 C#类型
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <param name="args"></param>
        private void OpenCsPanel(UIRegisterInfo registerInfo, params object[] args)
        {
            var messenger = (UIPanelMessenger)registerInfo.Messenger;
            if (_shownPanelDic.ContainsKey(messenger.PanelName))
            {
                messenger.OnReceiveMsg(args);
            }
            else
            {
                if(messenger.OnPreOpen(args))
                {
                    LoadCsPanel(messenger, args);
                }
            }
        }

        /// <summary>
        /// 打开界面 lua类型
        /// </summary>
        /// <param name="registerInfo"></param>
        /// <param name="panelName"></param>
        /// <param name="args"></param>
        private void OpenLuaPanel(UIRegisterInfo registerInfo, params object[] args)
        {
            var luaMessenger = (UIPanelMessengerForLua)registerInfo.Messenger;
            if (_shownPanelDic.ContainsKey(luaMessenger.PanelName))
            {
                luaMessenger.OnOpenPanel(luaMessenger.PanelObj);
            }
            else
            {
                // 初始化LuaFunction
                luaMessenger.OnInit();
                // 发送开启界面请求
                if (luaMessenger.OnPreOpen(args))
                {
                    LoadLuaPanel(luaMessenger.PanelName);
                }
            }
        }

        /// <summary>
        /// 加载 C#类型 Panel的预制体
        /// </summary>
        /// <param name="messenger"></param>
        /// <param name="args"></param>
        private void LoadCsPanel(UIPanelMessenger messenger, params object[] args)
        {
            var panelName = messenger.PanelName;

            // 加载完成后的回调
            void LoadPanelComplete(object obj)
            {
                if (obj == null)
                {
                    var message = $"加载 {panelName} 失败, 没有找到预制体对象, 请检查路径.";
                    Debug.LogError(message);
                    return;
                }
                // 创建预制体
                var panelObj = UnityEngine.Object.Instantiate(obj as GameObject);
                // 获取UI预制体上的的配置信息，这个挂在在预制体上，必须保存在预制体中
                var panelInfo = panelObj.GetComponent<UIPanelInfo>();
                // 把UI面板放在对应的节点下
                SetRoot(panelObj.transform, panelInfo.PanelLayer);
                // 接受需要的消息
                messenger.OnReceiveMsg(args);
                // 检查命名空间
                var uiScriptNamespace = Framework.GlobalConfig.UIScriptNamespace;
                // 添加的脚本名字
                var uiScript = uiScriptNamespace.IsNullOrEmpty()? panelName : $"{uiScriptNamespace}.{panelName}";
                // 添加对应的脚本
                var panelScript = ScriptManager.Instance.AddScript(panelObj, uiScript) as UIBasePanel;
                // 显示UI对象
                panelObj.gameObject.SetActive(true);
                // 添加到已经开启的界面缓存里
                _shownPanelDic.Add(panelName, panelScript);
                // 第一次创建会添加到所有UI的缓存里
                if (!_allPanelDic.ContainsKey(panelName))
                    _allPanelDic.Add(panelName, panelScript);
            }

            // 加载UI界面
            ResourceManager.LoadUIPanel(panelName, LoadPanelComplete);
        }

        /// <summary>
        /// 加载LuaPanel预制体，由Lua层调用
        /// </summary>
        /// <param name="panelName"></param>
        private void LoadLuaPanel(string panelName)
        {
           // ULog.Print("加载Lua界面" + panelName);
            _allPanelInfoDic.TryGetValue(panelName, out var info);
            if (info == null)
            {
                var message = $" {panelName} 尚未注册";
                Debug.LogError(message);
                return;
            }

            // 加载完成后的回调
            void LoadPanelComplete(object obj)
            {
                if (obj == null)
                {
                    var message = $"加载 {panelName} 失败, 没有找到预制体对象, 请检查路径.";
                    Debug.LogError(message);
                    return;
                }
                // 创建预制体
                var panelObj = UnityEngine.Object.Instantiate(obj as GameObject);
                // 获取UI预制体上的的配置信息，这个挂在在预制体上，必须保存在预制体中
                var uiInfo = panelObj.GetComponent<UIPanelInfo>();
                // 把UI面板放在对应的节点下
                SetRoot(panelObj.transform, uiInfo.PanelLayer);
                // 显示UI对象
                panelObj.gameObject.SetActive(true);
                // 调用对应的lua脚本
                var luaMessenger = (UIPanelMessengerForLua) info.Messenger;
                luaMessenger.OnOpenPanel(panelObj);
            }

            // 加载UI界面
            ResourceManager.LoadUIPanel(panelName, LoadPanelComplete);
        }

    }
}
