using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace NeverSayNever
{
    /// <summary>
    /// AB资源构建标签类型
    /// </summary>
    public enum EBundleLabelType
    {
        None,
        /// <summary>
        /// 以文件名字命名
        /// </summary>
        ByFileName,
        /// <summary>
        /// 以文件夹名字命名 (文件夹下的所有资源打包成一个AssetBundle）
        /// </summary>
        ByFolderName,
        /// <summary>
        /// 把子文件夹文件单独打包成Bundle，并以子文件夹命名
        /// </summary>
        ByChildFolderName,
    }

    /// <summary>
    /// 资源构建的类型
    /// </summary>
    public enum EBundleBuildType
    {
        Prefab = 0,
        Texture = 1,
        Atalas = 2,
        Font = 3,
        Scene = 4,
        Audio = 5,
        Shader = 6,
        Material = 7,
        Lua = 8,
        AllPack = 9,
    }

    public class NsnGlobalAssetConfig : SerializedScriptableObject
    {
        public class AssetCollectionInfo
        {
            [LabelText("文件夹"),TableColumnWidth(20)]
            public UnityEngine.Object folder;
            [LabelText("构建类型"),TableColumnWidth(20)]
            public EBundleBuildType buildType = EBundleBuildType.Prefab;
            [LabelText("检索"),TableColumnWidth(40)]
            public EBundleLabelType labelType = EBundleLabelType.ByFileName;
            [LabelText("后缀"),TableColumnWidth(20)]
            public string fileFilter = "*.prefab";

            public AssetCollectionInfo() { }
        }

        public class AssetFolderInfo
        {
            [LabelText("文件夹")]
            public UnityEngine.Object folder;
            [LabelText("路径")]
            public string path;
            [LabelText("后缀")]
            public string extension;

            public AssetFolderInfo() 
            {
            }
        }

        public string LuaSciprtDirectory { get { return Application.dataPath.Replace("Assets", "LuaScripts"); ; } }

        [LabelText("Lua模板目录")]
        public UnityEngine.Object LuaTempleteFolder;

        [LabelText("存放UI script目录（cs）")]
        public UnityEngine.Object CSharpScriptFolder;

        [LabelText("存放UI script目录(Lua)")]
        public UnityEngine.Object LuaScriptFolder;

        [InfoBox("所需打包Bundle的资源文件目录"),TableList]
        public List<AssetCollectionInfo> BuildAssetCollections = new List<AssetCollectionInfo>();

        [InfoBox("指定资源文件存放的根目录"),DictionaryDrawerSettings(KeyLabel = "资源文件类型", ValueLabel = "文件及路径", DisplayMode = DictionaryDisplayOptions.Foldout)]
        public readonly Dictionary<string, AssetFolderInfo> VariesAssetFolderDic = new Dictionary<string, AssetFolderInfo>();

        /// <summary>
        /// 自动生产的cs脚本导出路径
        /// </summary>
        public string UIScriptRootForCs
        {
            get
            {
#if !UNITY_EDITOR
                return "Assets/NeverSayNever/Examples/Scripts/UI/";
#else
                if (CSharpScriptFolder == null)
                    return "Assets/Framework/NeverSayNever/Examples/Scripts/UI/";
                else
                {
                    return UnityEditor.AssetDatabase.GetAssetPath(CSharpScriptFolder) + "/";
                }
#endif
            }
        }

        /// <summary>
        /// 自动生产的lua脚本导出的路径
        /// </summary>
        public string UIScriptRootForLua
        {
            get
            {
#if !UNITY_EDITOR
                return "Assets/NeverSayNever/Examples/LuaLogic/UI/";
#else
                if (LuaScriptFolder == null)
                    return "Assets/Framework/NeverSayNever/Examples/LuaLogic/UI/";
                else
                    return UnityEditor.AssetDatabase.GetAssetPath(LuaScriptFolder) + "/";
#endif
            }
        }

#if UNITY_EDITOR

        [Button("更新资源文件路径")]
        private void RefreshAssetFolderPath()
        {
            foreach (var asset in VariesAssetFolderDic.Values)
            {
                if (asset == null) continue;
                if (asset.folder != null)
                {
                    asset.path = UnityEditor.AssetDatabase.GetAssetPath(asset.folder);
                }
            }
        }
#endif
            }
}