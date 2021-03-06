using System;
using UnityEngine;

namespace NeverSayNever.Core
{
    using NeverSayNever.Core.Asset;
    using NeverSayNever.Core.Event;
    using UObject = UnityEngine.Object;

    public static class Framework
    {

        private static SOGlobalAssetConfig globalConfig;
        /// <summary>
        /// 框架默认的全局配置
        /// </summary>
        public static SOGlobalAssetConfig GlobalConfig
        {
            get
            {
                if (globalConfig == null)
                    globalConfig = ScriptableObjectManager.Instance.GetScriptableObject<SOGlobalAssetConfig>();
                return globalConfig;
            }
        }

        private static SOUIElementsCollectRule collecitonRuleConfig;
        /// <summary>
        /// UI元素收集配置
        /// </summary>
        public static SOUIElementsCollectRule UICollectionConfig
        {
            get
            {
                if (collecitonRuleConfig == null)
                    collecitonRuleConfig = ScriptableObjectManager.Instance.GetScriptableObject<SOUIElementsCollectRule>();
                return collecitonRuleConfig;
            }
        }

        /// <summary>
        /// 是否启动Lua
        /// </summary>
        public static bool IsActivateLua { get; set; } = false;

        /// <summary>
        /// 是否开启热更
        /// </summary>
        public static bool IsActivateHotfix { get; set; } = false;

        /// <summary>
        /// 是否启用lua脚本
        /// </summary>
        private static bool IsUsingLuaScript = false;
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

        /// <summary>
        /// 启动框架并初始化AssetBundle
        /// </summary>
        public static void StartUp()
        {
            BridgeObject = new GameObject("NsnFramework.Core");
            UObject.DontDestroyOnLoad(BridgeObject);

            // Step1. 初始化脚本管理器
            ScriptManager.Instance.OnInitialize();

            // Step2. 初始化资源加载管理器
            ResourceManager.OnInitialize(LoadType);

            // Step3. 初始化Lua模块管理器  如果不使用Lua,则跳过
            if(IsUsingLuaScript)
                LuaManager.Instance.OnInitialize("Launcher");

            // Step4. 初始化UI模块管理器
            UIManager.Instance.OnInitialize(UIRoot);

            // Step5. 初始化音频模块管理器
            SoundManager.Instance.OnInitialize(AudioSource);

            // Step6. 添加协程管理的模块
            BridgeObject.AddComponent<CoroutineManager>();

            PrintFrameworkInfo();
        }

        public static void OnUpdate()
        {
            // 轮询更新事件  todo 这里其实没有任何内容，每个事件都是派发的时候直接执行，后续修改为轮询模式
            EventManager.OnUpdate();
            // 更新Lua，清理GC
            LuaManager.Instance.OnUpdate();
            // 更新计时器计时器
            TimerManager.Instance.OnUpdate();
            // 更新资源
            ResourceManager.OnUpdate();
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
            Debug.Log("NsnFramework初始化完成!");
            Debug.Log("Nsn ---> 资源加载方式 : " + LoadType);
            Debug.Log("Nsn ---> 是否加载lua脚本: " + IsUsingLuaScript);
            if (IsUsingLuaScript)
            {
                Debug.Log("Nsn ---> 是否通过AssetBundle模式加载 lua 脚本: " + IsUsingLuaBundleMode);
            }
        }

    }
}