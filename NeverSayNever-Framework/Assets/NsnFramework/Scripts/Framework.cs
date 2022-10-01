using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    using UObject = UnityEngine.Object;

    public class Framework 
    {
        #region Framework Setting

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

        #region ScirptablaeObject Config

        public const string RootPath = "";

        private static Dictionary<System.Type, ScriptableObject> SOCacheDic = new Dictionary<System.Type, ScriptableObject>();

        #region UNITY_EDITOR
        /// <summary>
        /// 编辑器 - 框架配置文件默认存放路径
        /// </summary>
        public const string ScriptableObjectAssetRootPath = "Assets/NsnFramework/Resources/Setting/";

        #endregion

        private static NsnGlobalAssetConfig globalConfig;

        /// <summary>
        /// 框架默认的资源配置
        /// </summary>
        public static NsnGlobalAssetConfig GlobalConfig
        {
            get
            {
                if (globalConfig == null)
                    globalConfig = GetScriptableObject<NsnGlobalAssetConfig>();
                return globalConfig;
            }
        }

        private static NsnUIElementsCollectRule collecitonRuleConfig;

        /// <summary>
        /// UI元素收集配置
        /// </summary>
        public static NsnUIElementsCollectRule UICollectionConfig
        {
            get
            {
                if (collecitonRuleConfig == null)
                    collecitonRuleConfig =
                        GetScriptableObject<NsnUIElementsCollectRule>();
                return collecitonRuleConfig;
            }
        }


        public static T GetScriptableObject<T>() where T : ScriptableObject
        {
            var type = typeof(T);
            SOCacheDic.TryGetValue(type, out var target);
            if (target != null)
                return (T)target;

            target = GetScriptableObjectAsset<T>();
            if (target != null)
            {
                SOCacheDic.Add(type, target);
                return (T)target;
            }

            return null;
        }

        private static T GetScriptableObjectAsset<T>() where T : ScriptableObject
        {
            var name = typeof(T).Name;
            var finalPath = $"Setting/{name}";
            var asset = Resources.Load<T>(finalPath);
            return asset;
        }

        #endregion

        private static Dictionary<string, IManager> mManagerDic;
        private readonly static Dictionary<System.Type, INsnModule> mModuleDic = new Dictionary<System.Type, INsnModule>(10);

        public static void StartUp()
        {
            mManagerDic = new Dictionary<string, IManager>(10);
            BridgeObject = new GameObject("NsnFramework");
            UObject.DontDestroyOnLoad(BridgeObject);

            // 初始化模块
            InitModules();
            // 初始化管理器
            InitManagers();

            // 添加协程管理的模块
            BridgeObject.AddComponent<CoroutineMgr>();
            PrintFrameworkInfo();
        }

        public static void OnUpdate(float deltaTime)
        {
            UpdateMgr(deltaTime);
        }

        private static void UpdateMgr(float deltaTime)
        {
            var e = mManagerDic.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.OnUpdate(deltaTime);
            }
            e.Dispose();
        }

        private static void InitModules()
        {
            mModuleDic.Add(typeof(IResMdl), new ResourceMgr());
            mModuleDic.Add(typeof(IUIMdl), new UIMdl());
            mModuleDic.Add(typeof(ILuaMdl), new LuaMdl());
            mModuleDic.Add(typeof(ITimerMdl), new TimerMdl());
        }

        private static void InitManagers()
        {
            AddManager<IEventManager>();
            AddManager<IFSMMgr>();
            AddManager<IAudioMgr>();
        }

        public static void AddManager<T>(params object[] args) where T : IManager
        {
            System.Reflection.Assembly assembly = typeof(T).Assembly;
            string targetNamespace = typeof(T).Namespace;
            string typeName = typeof(T).Name;
            string targetName = typeName.Substring(1, typeName.Length - 1);
            string target = string.IsNullOrEmpty(targetNamespace) ? targetName : $"{targetNamespace}.{targetName}";
            if (!mManagerDic.ContainsKey(target))
            {
                object inst = NsnRuntime.CreateInstance(assembly, target);
                T script = (T)inst;
                if (script != null)
                {
                    script.OnInitialize(args);
                    mManagerDic.Add(target, script);
                }
            }
        }

        public static T GetManager<T>() where T : IManager
        {
            string key = typeof(T).ToString();
            mManagerDic.TryGetValue(key, out IManager mgr);
            return (T)mgr;
        }

        public static T GetModule<T>() where T : INsnModule
        {
            var mdlType = typeof(T);
            mModuleDic.TryGetValue(mdlType, out var targetMdl);
            return (T)targetMdl;
        }

        public static void SetAssetLoadType(EAssetLoadType loadType)
        {
            LoadType = loadType;
        }

        public static void SetLuaMode(bool enable,bool bundleMode)
        {
            IsUsingLuaScript = enable;
            IsUsingLuaBundleMode = enable && bundleMode;
        }

        public static void SetUIRoot(GameObject uiroot)
        {
            UIRoot = uiroot;
        }

        public static void SetAudioSourceRoot(AudioSource audioRoot)
        {
            AudioSource = audioRoot;
        }

        private static void PrintFrameworkInfo()
        {
            NsnLog.Print("NsnFramework初始化完成!");
            NsnLog.Print("Nsn ---> 资源加载方式 : " + LoadType);
            NsnLog.Print("Nsn ---> 是否加载lua脚本: " + IsUsingLuaScript);
            if (IsUsingLuaScript)
                NsnLog.Print("Nsn ---> 是否通过AssetBundle模式加载 lua 脚本: " + IsUsingLuaBundleMode);
        }

    }
}