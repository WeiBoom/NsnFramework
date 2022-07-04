using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    using NeverSayNever;
    public static class FrameworkConfig
    {

        #region ScirptablaeObject Config

        public const string RootPath = "";

        private static Dictionary<System.Type, ScriptableObject> SOCacheDic = new Dictionary<System.Type, ScriptableObject>();

        #region UNITY_EDITOR
        /// <summary>
        /// 编辑器 - 框架配置文件默认存放路径
        /// </summary>
        public const string ScriptableObjectAssetRootPath = "Assets/NsnFramework/Resources/Setting/";

        #endregion


        private static SOGlobalAssetConfig globalConfig;

        /// <summary>
        /// 框架默认的资源配置
        /// </summary>
        public static SOGlobalAssetConfig GlobalConfig
        {
            get
            {
                if (globalConfig == null)
                    globalConfig = GetScriptableObject<SOGlobalAssetConfig>();
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
                    collecitonRuleConfig =
                        GetScriptableObject<SOUIElementsCollectRule>();
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
    }
}
