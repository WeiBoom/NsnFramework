using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    using UObject = UnityEngine.Object;

    public static class Framework
    {

        #region framework setting

        /// <summary>
        /// 是否启用lua脚本
        /// </summary>
        [HideInInspector]
        public static bool IsUsingLuaScript = false;

        /// <summary>
        /// 是否以AssetBundle的方式加载lua脚本
        /// </summary>
        public static bool IsUsingLuaBundleMode { get; private set; } = false;

        /// <summary>
        /// 框架所采取的加载方式
        /// </summary>
        public static EAssetLoadType LoadType { get; private set; } = EAssetLoadType.AssetDataBase;

        /// <summary>
        /// 框架核心script承载mono对象
        /// </summary>
        public static GameObject BridgeObject { get; private set; }

        /// <summary>
        /// UI根节点
        /// </summary>
        public static GameObject UIRoot { get; private set; }

        /// <summary>
        /// 音频播放源节点
        /// </summary>
        public static AudioSource AudioSource { get; private set; }

        #endregion

        private static Dictionary<System.Type, IManager> mManagerDic = new Dictionary<Type, IManager>();

        /// <summary>
        /// 启动框架并初始化AssetBundle
        /// </summary>
        public static void StartUp()
        {
            BridgeObject = new GameObject("NsnFramework.Core");
            UObject.DontDestroyOnLoad(BridgeObject);
            
            // 初始化事件管理器
            //EventManager.Instance.OnInitialize();
            // 初始化脚本管理器
            //NsnRuntime.OnInitialize();
            // 初始化资源加载管理器
            ResourceMgr.OnInitialize(LoadType);
            // 初始化Lua模块管理器  如果不使用Lua,则跳过
            //if(IsUsingLuaScript)
                //LuaMgr.Instance.OnInitialize("Launcher");
            // 初始化UI模块管理器
            //UIMgr.Instance.OnInitialize(UIRoot);
            // 初始化音频模块管理器
            SoundMgr.Instance.OnInitialize(AudioSource);
            // 添加协程管理的模块
            BridgeObject.AddComponent<CoroutineMgr>();

            PrintFrameworkInfo();
        }

        public static void OnUpdate()
        {
            // 更新事件 
            //EventManager.Instance.OnUpdate(); // EventManager.OnUpdate();
            // 更新Lua，清理GC
            //LuaMgr.Instance.OnUpdate();
            // 更新计时器计时器
            TimerMgr.Instance.OnUpdate();
            // 更新资源
            ResourceMgr.OnUpdate();
            // 更新模块系统
            //ModuleManager.Instance.OnUpdate();
        }

        private static void Initialize()
        {
            InitializeManager<IEventManager>();
        }

        private static void InitializeManager<T>() where T: IManager
        {

        }

        public static IManager GetModule<T>() where T: IManager
        {
            mManagerDic.TryGetValue(typeof(T), out var manager);
            return manager;
        }

        // 设置资源加载模式
        public static void SetAssetLoadType(EAssetLoadType loadType)
        {
            LoadType = loadType;
        }

        // 设置lua，是否启用，以及是否以assetbundle的模式加载lua脚本
        public static void SetLuaMode(bool enable,bool bundleMode)
        {
            IsUsingLuaScript = enable;
            IsUsingLuaBundleMode = enable && bundleMode;
        }

        // 设置UI根节点
        public static void SetUIRoot(GameObject uiroot)
        {
            UIRoot = uiroot;
        }

        // 设置音频播放器节点
        public static void SetAudioSourceRoot(AudioSource audioRoot)
        {
            AudioSource = audioRoot;
        }

        private static void PrintFrameworkInfo()
        {
            ULog.Print("NsnFramework初始化完成!");
            ULog.Print("Nsn ---> 资源加载方式 : " + LoadType);
            ULog.Print("Nsn ---> 是否加载lua脚本: " + IsUsingLuaScript);
            if (IsUsingLuaScript)
                ULog.Print("Nsn ---> 是否通过AssetBundle模式加载 lua 脚本: " + IsUsingLuaBundleMode);
        }

    }
}