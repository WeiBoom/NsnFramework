using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Core.Asset
{

    using UObject = UnityEngine.Object;

    public class AssetBundleHelper : Singleton<AssetBundleHelper>
    {
        // 非bundle模式下，使用的加载方式
        public static System.Func<string, System.Type, UObject> assetLoader = null;
        // 非bundle模式下，使用的异步加载方式
        public static System.Func<string, System.Type, UObject> assetAsyncLoader = null;

        // 所有的资源缓存
        private readonly Dictionary<string, BaseAsset> m_AssetRequestDic = new Dictionary<string, BaseAsset>();
        // 当前正在加载的资源列表
        private readonly List<BaseAsset> m_LoadingAssetRequestList = new List<BaseAsset>();
        // 所有未被使用的资源
        private readonly List<BaseAsset> m_UnusedAssetList = new List<BaseAsset>();
        // 已经加载完成的资源
        private readonly List<BaseAsset> m_LoadedAssetList = new List<BaseAsset>();
        // 清理一次未使用的资源
        private bool clearUnusedAssetOnce = false;
        // 持续检测并清理未使用的资源
        private bool clearUnusedAssetPersistent = false;

        #region About AssetBundle

        // 是否使用加载bundle的模式
        public static bool bundleMode { get; set; } = false;

        private static AssetBundleManifest manifest;
        // 每帧最大处理的bundle数量
        private static readonly int MAX_BUNDLES_PERFRAME = 10;
        // 所有的Bundle资源
        private static readonly Dictionary<string, BundleRequest> m_BundleRequestDic = new Dictionary<string, BundleRequest>();
        // 正在加载的Bundle资源
        private static readonly List<BundleRequest> m_LoadingBundleRequestList = new List<BundleRequest>();
        // 未使用的Bundle资源
        private static readonly List<BundleRequest> m_UnusedBundleList = new List<BundleRequest>();
        // 准备加载的bundle资源
        private static readonly List<BundleRequest> m_ReadToLoadBundleList = new List<BundleRequest>();

        private static readonly List<SceneAssetRequest> m_SceneBundleRequestList = new List<SceneAssetRequest>();
        // Bundle资源的存放目录
        private static string bundle_asset_path;

        // 当前正在运行的场景
        private static SceneAssetRequest m_RunningScene;

        #endregion

        public override void OnInitialize(params object[] args)
        {
            base.OnInitialize(args);
            if(bundleMode)
            {
                bundle_asset_path = args[0] as string;
                var manifestPath = $"{bundle_asset_path}/{NeverSayNever.Utilities.AppConst.Platform}";//.manifest";
                var manifestBundle = AssetBundle.LoadFromFile(manifestPath);
                manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                if (manifest == null)
                    throw new System.Exception("加载 AssetBundleManifest 失败");
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            // 更新加载中的资源
            OnUpdateLoadingAssets();
            // 更新正在使用的资源，没使用的移动到未使用列表中
            OnUpdateAllUsedAssets();
            // 更新场景资源
            OnUpdateSceneAssets();
            // 更新未使用的资源
            OnUpdateAllUnusedAssets();

        }

        /// <summary>
        /// 检查当前正在加载的资源
        /// </summary>
        private void OnUpdateLoadingAssets()
        {
            for(int i =0;i< m_LoadingAssetRequestList.Count;i++)
            {
                var request = m_LoadingAssetRequestList[i];
                // 如果资源正在加载，则不管
                if (request.Tick())
                    continue;
                // 如果资源加载错误
                if(!request.error.IsNullOrEmpty())
                {
                    if (request == null)
                    {
                        Debug.LogError("ReleaseAsset ----- 资源为空");
                        continue;
                    }
                    request.Release();
                    if (request.IsUnused)
                        m_UnusedAssetList.Add(request);
                    Debug.LogError($"加载资源失败。 --- {request.error} ");
                }
                // 加载完成
                else
                {
                    OnAssetLoaded(request);
                    if (!m_LoadedAssetList.Contains(request))
                        m_LoadedAssetList.Add(request);
                    else
                        Debug.LogError("资源已经加载过了");
                }
                // 从正在加载的列表中移除
                m_LoadingAssetRequestList.RemoveAt(i);
                i--;
            }
        }

        /// <summary>
        /// 检查当前加载完成的资源
        /// </summary>
        private void OnUpdateAllUsedAssets()
        {
            //如果不需要清理
            if (!clearUnusedAssetOnce && !clearUnusedAssetPersistent)
                return;
            clearUnusedAssetOnce = false;

            for (var i = 0; i < m_LoadedAssetList.Count; ++i)
            {
                var request = m_LoadedAssetList[i];
                request.UpdateRequires();
                if (request.IsUnused)
                {
                    m_UnusedAssetList.Add(request);
                    m_LoadedAssetList.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// 检查所有未使用的资源，并移除
        /// </summary>
        private void OnUpdateAllUnusedAssets()
        {
            if (m_UnusedAssetList.Count <= 0) return;

            for (int i = 0; i < m_UnusedAssetList.Count; i++)
            {
                var request = m_UnusedAssetList[i];
                m_AssetRequestDic.Remove(request.assetUrl);
                OnAssetUnLoad(request);
                request.Unload();
            }
            m_UnusedAssetList.Clear();
        }

        /// <summary>
        /// 检查当前的场景资源
        /// </summary>
        private void OnUpdateSceneAssets()
        {
            for (var i = 0; i < m_SceneBundleRequestList.Count; i++)
            {
                var sceneRequest = m_SceneBundleRequestList[i];
                if (sceneRequest.Tick() || !sceneRequest.IsUnused)
                    continue;
                // 从列表中移除
                m_SceneBundleRequestList.RemoveAt(i);
                // 添加到未使用的资源列表中
                m_UnusedAssetList.Add(sceneRequest);
                i--;
            }
        }

        /// <summary>
        /// 当资源加载完成
        /// </summary>
        /// <param name="request"></param>
        private void OnAssetLoaded(BaseAsset request)
        {
        }

        /// <summary>
        /// 当资源释放掉
        /// </summary>
        /// <param name="request"></param>
        private void OnAssetUnLoad(BaseAsset request)
        {
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public BaseAsset LoadAsset<T>(string assetPath, string assetName, System.Action<object> callback)
        {
            var type = typeof(T);
            return LoadAsset(assetPath, assetName,type, callback,false);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public BaseAsset LoadAssetAsync<T>(string assetPath, string assetName, System.Action<object> callback)
        {
            var type = typeof(T);
            return LoadAsset(assetPath, assetName, type, callback,true);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        /// <param name="isAsync"></param>
        /// <returns></returns>
        private BaseAsset LoadAsset(string path,string name ,System.Type type, System.Action<object> callback, bool isAsync)
        {
            BaseAsset request;
            // 检查资源是否正在加载
            if (m_AssetRequestDic.TryGetValue(path,out request))
            {
                request.Use();
                if (!m_LoadingAssetRequestList.Contains(request))
                    m_LoadingAssetRequestList.Add(request);
                return request;
            }

            var pathSplit = path.Split('/');
            var bundleName = pathSplit[pathSplit.Length - 1];
            request = isAsync ? new AssetRequestAsync(bundleName) : new AssetRequest(bundleName);
            request.assetName = name;
            request.assetUrl = path;
            request.assetType = type;
            request.loadComplete = callback;
            request.Use();
            request.Load();
            m_AssetRequestDic.Add(path, request);
            m_LoadingAssetRequestList.Add(request);

            return request;
        }

        /// <summary>
        /// 加载AssetBundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public BundleRequest LoadBundle(string bundleName)
        {
            return LoadBundle(bundleName, false);
        }

        /// <summary>
        /// 异步加载AssetBundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public BundleRequest LoadBundleAsync(string bundleName)
        {
            return LoadBundle(bundleName, true);
        }

        /// <summary>
        /// 加载bundle资源
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="isAsync"></param>
        private BundleRequest LoadBundle(string bundleName,bool isAsync)
        {
            if(bundleName.IsNullOrEmpty())
            {
                Debug.LogError("bundleName 为空");
                return null;
            }
            //var assetRootPath = GetDataPath(bundleName);
           // ResourceManager.GetBundleAssetFinalPath(System.IO.Path.GetFileName(bundleName));
            var url = $"{bundle_asset_path}/{bundleName}"; //assetRootPath + bundleName;

            BundleRequest bundle;
            if (m_BundleRequestDic.TryGetValue(bundleName, out bundle))
            {
                bundle.Use();
                // 如果还没加载完成且不是异步，则调用立即加载的方法
                if (!isAsync && !bundle.isDone)
                    bundle.LoadImmediate();
                return bundle;
            }

            bundle = isAsync ? new BundleRequestAsync() : new BundleRequest();
            bundle.assetUrl = url;
            bundle.assetName = bundleName;
            // 添加到bundle列表重
            m_BundleRequestDic.Add(bundleName, bundle);

            if (MAX_BUNDLES_PERFRAME > 0 && isAsync)
            {
                m_ReadToLoadBundleList.Add(bundle);
            }
            else
            {
                // 开始加载bundle 资源
                bundle.Load();
                m_LoadingBundleRequestList.Add(bundle);
            }

            // 依赖资源
            var dependencies = manifest.GetAllDependencies(bundleName);
            foreach(var dep in dependencies)
            {
                // 加载并缓存依赖资源
                var depBundle = LoadBundle(dep, isAsync);
                bundle.dependencies.Add(depBundle);
            }

            // 添加引用计数
            bundle.Use();

            return bundle;
        }

        /// <summary>
        /// 加载场景资源
        /// </summary>
        /// <param name="scenePath"></param>
        /// <param name="addictive"></param>
        /// <returns></returns>
        public SceneAssetRequest LoadScene(string scenePath,string sceneName, bool addictive,System.Action<object> callback)
        {
            if(scenePath.IsNullOrEmpty())
            {
                Debug.LogError("scene path is invalid");
                return null;
            }
            var pathSplit = scenePath.Split('/');
            var bundleName = pathSplit[pathSplit.Length - 1];
            var asset = new SceneAssetRequest(scenePath, bundleName, sceneName, addictive);
            if(!addictive)
            {
                if(m_RunningScene != null)
                {
                    m_RunningScene.Release();
                    m_RunningScene = null;
                }
                m_RunningScene = asset;
            }
            asset.loadComplete = callback;
            asset.Load();
            asset.Use();

            m_SceneBundleRequestList.Add(asset);
            return asset;
        }

        /// <summary>
        /// 获取资源路径
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        private static string GetDataPath(string bundleName)
        {
            return "";
        }
    }
}