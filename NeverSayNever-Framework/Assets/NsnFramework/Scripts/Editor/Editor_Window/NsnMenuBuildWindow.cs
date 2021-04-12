using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace NeverSayNever.Editors
{
    using NeverSayNever.Core.Asset;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    public class NsnMenuBuildWindow
    {
        [LabelText("Welcome, Developer")]
        [DisableInEditorMode,ShowInInspector]
        private string Title = "NsnFramework";

        [LabelText("框架配置文件存放路径"),ShowInInspector]
        private string FrameworkConfigPath;

        [LabelText("UI脚本命名空间"),ShowInInspector]
        private string AutoUIScriptNamespace;


        public NsnMenuBuildWindow()
        {
            FrameworkConfigPath = AssetEditorDefine.ScriptableObjectAssetRootPath;
        }

        /// <summary>
        /// 生成资源路径配置
        /// </summary>
        [Button("一键生成默认全局配置")]
        private static void GenerateAssetPathConfig()
        {
            // 生成UI自动收集的规则配置
            NEditorTools.GenerateScriptableObjectAsset<SOUIElementsCollectRule>();

            var assetCfg = NEditorTools.GetScriptableObjectAsset<SOGlobalAssetConfig>();
            if (assetCfg == null)
            {
                // 生成打包资源配置
                NEditorTools.GenerateScriptableObjectAsset<SOGlobalAssetConfig>();
                assetCfg = NEditorTools.GetScriptableObjectAsset<SOGlobalAssetConfig>();

                assetCfg.VariesAssetFolderDic.Add("UI", InitializeAssetFolderInfo("Prefab/UIPanels", ".prefab"));
                assetCfg.VariesAssetFolderDic.Add("Model", InitializeAssetFolderInfo("Prefab/Models", ".prefab"));
                assetCfg.VariesAssetFolderDic.Add("Effect", InitializeAssetFolderInfo("Prefab/Effects", ".prefab"));
                assetCfg.VariesAssetFolderDic.Add("Texture", InitializeAssetFolderInfo("Texture", ".png"));
                assetCfg.VariesAssetFolderDic.Add("Audio", InitializeAssetFolderInfo("Audio", ".mp3"));
                assetCfg.VariesAssetFolderDic.Add("Font", InitializeAssetFolderInfo("Common/Fonts", ".asset"));
                assetCfg.VariesAssetFolderDic.Add("Shader", InitializeAssetFolderInfo("Common/Shaders", ".shader"));
                assetCfg.VariesAssetFolderDic.Add("TextAsset", InitializeAssetFolderInfo("Config", ".txt"));
            }
        }

        private static SOGlobalAssetConfig.AssetFolderInfo InitializeAssetFolderInfo(string folderName,string extension)
        {
            var folderInfo = new SOGlobalAssetConfig.AssetFolderInfo();
            folderInfo.extension = extension;
            folderInfo.folder = AssetDatabase.LoadAssetAtPath("Framework/NsnFramework/Examples/DataRes/" + folderName, typeof(UnityEngine.Object));
            if(folderInfo.folder != null)
            {
                folderInfo.path = AssetDatabase.GetAssetPath(folderInfo.folder);
            }
            return folderInfo;
        }

    }
}
