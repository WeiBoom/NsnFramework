using System;
using System.Collections.Generic;
using UnityEngine;

using UObject = UnityEngine.Object;

namespace NeverSayNever
{
    public class ResourceMgr : IResourceMgr
    {
        public EAssetLoadType LoadType { get; private set; } = EAssetLoadType.AssetDataBase;

        private AssetBundleHelper assetLoader;

        private const string asset_bundle_extension = ".u3d";

        private string bundle_platform_root;

        private List<Dictionary<string, string>> m_AllAssetCacheList = new List<Dictionary<string, string>>();

        public void OnInitialize(params object[] args)
        {
            var loadType = (EAssetLoadType)args[0];// as EAssetLoadType;
            if (loadType == EAssetLoadType.AssetDataBase)
            {
#if UNITY_EDITOR
                AssetBundleHelper.assetLoader = UnityEditor.AssetDatabase.LoadAssetAtPath;
#else
                AssetBundleHelper.assetLoader = Resources.Load;
#endif
            }
            else if (loadType == EAssetLoadType.Resources)
            {
                AssetBundleHelper.assetLoader = Resources.Load;
            }
            else if (loadType == EAssetLoadType.AssetBundle)
            {
                AssetBundleHelper.bundleMode = true;
            }
            LoadType = loadType;

            InitAssetRootPath();

            assetLoader = AssetBundleHelper.Inst;
            assetLoader.OnInitialize(bundle_platform_root);

        }

        public void OnUpdate(float deltaTime)
        {
            if(assetLoader != null)
            {
                assetLoader.OnUpdate();
            }
        }

        public void OnDispose()
        {
            if(assetLoader != null)
            {
                assetLoader.OnDispose();
            }
        }

        #region 资源路径

        // 初始化资源根目录
        private void InitAssetRootPath()
        {
            var cfg = Framework.GlobalConfig.VariesAssetFolderDic;

            bundle_platform_root = Application.streamingAssetsPath + "/" + NsnPlatform.Platform;  //$"{Application.dataPath.Replace("Assets", "")}/{AppSetting.Platform}";

        }

        // 获取资源路径
        private string GetAssetFinalPath(EAssetType assetType, string name)
        {
            var extension = LoadType == EAssetLoadType.Resources ? string.Empty : GetAssetExtension(assetType);
            var folderInfo = Framework.GlobalConfig.VariesAssetFolderDic[assetType.ToString()];
            if (folderInfo != null)
            {
                if (assetType == EAssetType.UI)
                    return $"{folderInfo.path}/{name}/{name}{extension}";
                else
                    return $"{folderInfo.path}/{name}{extension}";
            }
            return name;
        }

        // 获取bundle资源的路径
        private string GetBundleAssetFinalPath(EAssetType assetType,string name)
        {
            switch (assetType)
            {
                case EAssetType.UI:
                case EAssetType.Effect:
                case EAssetType.Model:
                    name = $"prefab_{ name}";
                    break;
                case EAssetType.Texture:
                    name = $"tex_{ name}";
                    break;
                case EAssetType.Audio:
                    name = $"audio_{ name}";
                    break;
                case EAssetType.Scene:
                    name = $"scene_{ name}";
                    break;
            }
            return $"{bundle_platform_root}/{name}{asset_bundle_extension}";
        }

        // 获取所有的资源路径
        private void GetAllAssetPath()
        {
            foreach (var dic in m_AllAssetCacheList)
                dic.Clear();
            m_AllAssetCacheList.Clear();

            var length = System.Enum.GetNames(typeof(EAssetFileType)).Length;
            for (int i = 0; i < length; i++)
            {
                var cacheDic = new Dictionary<string, string>();
                m_AllAssetCacheList.Add(cacheDic);

            }

        }

        // 获取资源后缀
        private static string GetAssetExtension(EAssetType type)
        {
            var assetConfigInfo = Framework.GlobalConfig.VariesAssetFolderDic;
            NsnGlobalAssetConfig.AssetFolderInfo folderInfo = assetConfigInfo[type.ToString()]; 
            return folderInfo == null ? string.Empty : folderInfo.extension;
        }

        #endregion

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        private void LoadAsset<T>(EAssetType type, string name, Action<object> callback)
        {
            var assetName = GetAssetFinalPath(type, name);
            var assetNameSplit = assetName.Split('/');
            var bundleName = assetNameSplit[assetNameSplit.Length - 1];
            var bundlePath = GetBundleAssetFinalPath(type, name);
            assetLoader.LoadAsset<T>(bundlePath, assetName, callback);
        }

        /// <summary>
        /// 加载UI界面
        /// </summary>
        /// <param name="panelName"></param>
        /// <param name="callback"></param>
        public void LoadUIPanel(string panelName, Action<object> callback)
        {
            LoadAsset<GameObject>(EAssetType.UI, panelName, callback);
        }

        /// <summary>
        /// 加载音频资源
        /// </summary>
        /// <param name="audioName"></param>
        /// <param name="callback"></param>
        public void LoadAudio(string audioName, Action<object> callback)
        {
            LoadAsset<AudioClip>(EAssetType.Audio, audioName, callback);
        }

        /// <summary>
        /// 加载文本（配置）
        /// </summary>
        /// <param name="textName"></param>
        /// <param name="callback"></param>
        public void LoadTextAsset(string textName, Action<object> callback)
        {
            LoadAsset<TextAsset>(EAssetType.TextAsset, textName, callback);
        }

        /// <summary>
        /// 加载模型
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="callback"></param>
        public void LoadModel(string modelName, Action<object> callback)
        {
            LoadAsset<GameObject>(EAssetType.Model, modelName, callback);
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="texName"></param>
        /// <param name="callback"></param>
        public void LoadTexture(string texName, Action<object> callback)
        {
            LoadAsset<Texture>(EAssetType.Texture, texName, callback);
        }

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="callback"></param>
        public void LoadScene(string sceneName,Action<object> callback)
        {
            var assetName = GetAssetFinalPath(EAssetType.Scene, sceneName);
            var assetNameSplit = assetName.Split('/');
            var bundleName = assetNameSplit[assetNameSplit.Length - 1];
            var bundlePath = GetBundleAssetFinalPath(EAssetType.Scene, sceneName);

            assetLoader.LoadScene(bundlePath, bundleName, false, callback);
        }

        /// <summary>
        /// 释放资源对象
        /// </summary>
        public void ReleaseObject(UObject target)
        {
            if(target != null)
            {
                UObject.Destroy(target);
            }
        }
    }
}