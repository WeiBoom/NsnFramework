using System.Collections;
using System.Collections.Generic;
using NeverSayNever.Utilities;
using UnityEngine;
using UnityEditor;
using UnityEditor.Graphs;

namespace NeverSayNever.EditorUtilitiy
{
    using NeverSayNever.Core.Asset;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.OdinInspector;
    public class NsnMenuBuildWindow
    {
        [Title("NsnFramework", "keep it simple and stupid")]
        //private const string FrameworkName = "NsnFramework";
        
        //[Title("Default Config", "default config of framework")] 
        //[LabelText("Default Path of Framework Config "),ShowInInspector]
        //[LabelText("框架配置文件默认存放路径"),ShowInInspector]
        //private string FrameworkConfigPath;
        //[LabelText("Script's Namespace of UI"),ShowInInspector]
        [LabelText("自动生成的UIScript 的命名空间"),ShowInInspector]
        private string AutoUIScriptNamespace;

        public NsnMenuBuildWindow()
        {
            //FrameworkConfigPath = FrameworkConfig.ScriptableObjectAssetRootPath; //AssetEditorDefine.ScriptableObjectAssetRootPath;
            //ULog.Print($"框架配置文件默认存放路径 ： {FrameworkConfigPath}");
        }

        [TitleGroup("AssetBundle","build AssetBundle for project")]
        [Button("Open AssetBundle Config of Framework",ButtonSizes.Small,ButtonStyle.CompactBox)]
        //[Button("打开 AssetBundle 构建配置",ButtonSizes.Small,ButtonStyle.CompactBox)]
        private void OpenAssetBundleBuildConfig()
        {
            // todo;
        }
        
        [TitleGroup("AssetBundle","build AssetBundle for project")]
        //[Button("Build AssetBundle By Config",ButtonSizes.Large,ButtonStyle.CompactBox)]
        [Button("构建 AssetBundle 资源",ButtonSizes.Large,ButtonStyle.CompactBox)]
        private void BuildAssetBundleByCurrentPlatform()
        {
            BundleBuildTool.MenuItem_BuildAssetBundle();
        }
        
        [Title("Initialize","generate default scriptable config for framework")]
        //[Button("Generate Default Config For Framework",ButtonSizes.Large)]
        [Button("初始化项目配置全局配置",ButtonSizes.Large)]
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
