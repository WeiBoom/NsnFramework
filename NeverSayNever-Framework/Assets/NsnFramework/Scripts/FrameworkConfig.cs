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
                    collecitonRuleConfig =
                        ScriptableObjectManager.Instance.GetScriptableObject<SOUIElementsCollectRule>();
                return collecitonRuleConfig;
            }
        }

        #endregion
    }
}
