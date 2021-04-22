using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Core.Asset
{
    using UnityEngine.SceneManagement;
    using UObject = UnityEngine.Object;

    public enum EBundleLoadState
    {
        None,           // 未加载
        LoadingBundle,  // 正在加载bundle
        LoadingAsset,   // bundle加载完成，加载bundle包含的资源
        Loaded,         // 全部加载完成
        Unload,         // 已经卸载
    }
    // 资源引用
    public class BaseRefrence
    {
        /// <summary>
        /// 资源引用次数
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 包含的所有资源
        /// </summary>
        private List<UObject> requires = null;

        public bool IsUsed() => Count > 0;

        public bool IsUnused => Count <= 0;

        public virtual void Use() => Count++;

        public virtual void Release() => Count--;

        /// <summary>
        /// 依赖资源
        /// </summary>
        /// <param name="obj"></param>
        public void Require(UObject obj)
        {
            if (Count > 0) Release();
            if (requires == null)
            {
                requires = new List<UObject>();
                Use();
            }
            requires.Add(obj);
        }

        /// <summary>
        /// 解除依赖
        /// </summary>
        /// <param name="obj"></param>
        public void UnRequire(UObject obj)
        {
            if (requires != null)
                requires.Remove(obj);
        }

        public void UpdateRequires()
        {
            if (requires == null) return;
            // 检查是否还有依赖的资源
            for (var i = 0; i < requires.Count; i++)
            {
                if (requires[i] != null)
                    break;
                requires.RemoveAt(i);
                i--;
            }
            // 如果当前已经没有依赖的资源了，则清空列表
            if (requires.Count == 0)
            {
                Release();
                requires = null;
            }
        }
    }

    // 资源基类
    public class BaseAsset : BaseRefrence
    {
        // 资源类型
        public System.Type assetType;
        // 资源地址
        public string assetUrl { get; set; }
        // 资源名
        public string assetName { get; set; }
        // 资源加载状态
        public EBundleLoadState loadState { get; protected set; }

        public virtual bool isDone => true;

        public virtual float progress => 1;

        public virtual string error { get; protected set; }

        public string text { get; protected set; }

        public byte[] bytes { get; protected set; }

        public UObject assetObj { get; internal set; }

        public float LastUseTime { get; private set; }

        public bool IsAsync { get; protected set; } = false;

        public System.Action<object> loadComplete;

        public BaseAsset()
        {
            assetObj = null;
            loadState = EBundleLoadState.None;
        }

        public override void Use()
        {
            base.Use();
            LastUseTime = System.DateTime.Now.ToFileTime();
        }

        internal virtual void Load()
        {
        }

        internal virtual void LoadImmediate()
        {
        }

        internal virtual void Unload()
        {
            if (assetObj == null)
                return;
            if((assetObj is GameObject))
                Resources.UnloadAsset(assetObj);
            assetObj = null;
        }

        // 检查是否正在加载资源
        internal bool Tick()
        {
            if (!isDone)
                return true;

            if (loadComplete == null)
                return false;

            // 如果已经加载完了，则执行加载完成的回调，完成后清理回调函数
            try
            {
                loadComplete(assetObj);
            }
            catch(System.Exception e)
            {
                throw (e);
            }

            loadComplete = null;
            return false;
        }
    }

    // Bundle资源
    public class BundleRequest : BaseAsset
    {
        public readonly List<BundleRequest> dependencies = new List<BundleRequest>();

        private readonly List<BundleRequest> releaseTargets = new List<BundleRequest>();

        public virtual AssetBundle assetBundle
        {
            get { return assetObj as AssetBundle; }
            internal set { assetObj = value; }
        }

        internal override void Load()
        {
            assetObj = AssetBundle.LoadFromFile(assetUrl);
            if (assetBundle == null)
                error = $"LoadFromFile failed . url :  {assetUrl}";
        }

        internal override void Unload()
        {
            base.Unload();
            dependencies.Clear();
            if(assetBundle != null)
            {
                assetBundle.Unload(true);
                assetBundle = null;
            }
        }

        public override void Release()
        {
            base.Release();
            foreach(var depend in dependencies)
            {
                ReleaseFromBundle(depend);
            }
            releaseTargets.Clear();
        }

        private void ReleaseFromBundle(BundleRequest request)
        {
            // 释放本身的资源
            if(request.assetName.Equals(this.assetName))
            {
                base.Release();
            }
            // 如果是释放依赖的资源
            else
            {
                if (releaseTargets.Contains(request))
                    return;
                // 添加到释放资源列表
                releaseTargets.Add(request);
                // 释放依赖资源的依赖资源
                foreach(var dep in request.dependencies)
                {
                    ReleaseFromBundle(dep);
                }
            }
        }
    }

    // Bundle资源 - 异步
    public class BundleRequestAsync : BundleRequest
    {
        private AssetBundleCreateRequest unityBundleCreateRequest;

        public override AssetBundle assetBundle
        {
            get
            {
                if (unityBundleCreateRequest != null && !unityBundleCreateRequest.isDone)
                {
                    OnAssetBundleLoaded();
                }
                return base.assetBundle;
            }
            internal set => base.assetBundle = value;
        }

        public override bool isDone => IsAssetRequsetDone();

        public override float progress => unityBundleCreateRequest == null ? 0 : unityBundleCreateRequest.progress;

        internal override void Load()
        {
            if(unityBundleCreateRequest == null)
            {
                unityBundleCreateRequest = AssetBundle.LoadFromFileAsync(assetUrl);
                if(unityBundleCreateRequest == null)
                {
                    error = $"{assetUrl} LoadFromFile failed.";
                    return;
                }
                loadState = EBundleLoadState.LoadingBundle;
            }
        }

        internal override void Unload()
        {
            unityBundleCreateRequest = null;
            loadState = EBundleLoadState.Unload;
            base.Unload();
        }

        internal override void LoadImmediate()
        {
            base.LoadImmediate();
            Load();
            if(assetBundle != null)
            {
                Debug.LogWarning($"LoadImmediate : {assetBundle.name}");
            }
        }

        private void OnAssetBundleLoaded()
        {
            if (unityBundleCreateRequest == null)
                return;
            assetObj = unityBundleCreateRequest.assetBundle;
            if (assetObj == null)
            {
                error = $"unable to load assetbundle {assetUrl}";
            }
            loadState = EBundleLoadState.Loaded;
        }

        private bool IsAssetRequsetDone()
        {
            if (loadState == EBundleLoadState.None)
                return false;
            if (loadState == EBundleLoadState.Loaded)
                return true;

            if (loadState == EBundleLoadState.LoadingBundle && unityBundleCreateRequest.isDone)
            {
                OnAssetBundleLoaded();
            }

            return unityBundleCreateRequest == null || unityBundleCreateRequest.isDone;
        }
    }

    // Asset 资源
    public class AssetRequest : BaseAsset
    {
        protected string bundleName;
        protected BundleRequest bundleRequest;

        public AssetRequest(string bundle)
        {
            bundleName = bundle;//.ToLower();
            IsAsync = false;
        }

        internal override void Load()
        {

            if (AssetBundleHelper.assetLoader != null)//  && !AssetBundleHelper.bundleMode)
                assetObj = AssetBundleHelper.assetLoader(assetName, assetType);
            else
            {
                if (!System.IO.File.Exists(assetUrl))
                {
                    Debug.LogError("File doesn't exit : " + assetUrl);
                    return;
                }
                bundleRequest = AssetBundleHelper.Instance.LoadBundle(bundleName);
                var targetAssetName = assetName.ToLower();
                assetObj = bundleRequest.assetBundle.LoadAsset(targetAssetName, assetType);
            }

            if (assetObj == null)
                error = $"AssetRequest Load Failed : {assetName}";
        }

        internal override void Unload()
        {
            bundleRequest?.Release();
            bundleRequest = null;
            assetObj = null;
        }

    }

    // Asset 资源 异步
    public class AssetRequestAsync : AssetRequest
    {
        private AssetBundleRequest unityBundleRequset;

        public AssetRequestAsync(string bundleName) :base(bundleName)
        {
            IsAsync = true;
        }

        public override bool isDone => IsAssetRequsetDone();

        public override float progress => GetLoadingProgress();

        internal override void Load()
        {
            bundleRequest = AssetBundleHelper.Instance.LoadBundleAsync(bundleName);
            loadState = EBundleLoadState.LoadingBundle;
        }

        internal override void Unload()
        {
            unityBundleRequset = null;
            loadState = EBundleLoadState.Unload;
            base.Unload();
        }

        private bool IsAssetRequsetDone()
        {
            if (loadState == EBundleLoadState.Loaded || loadState == EBundleLoadState.Unload)
                return true;
            // 包含加载错误信息
            if (!error.IsNullOrEmpty() || !bundleRequest.error.IsNullOrEmpty())
                return true;

            // 检查依赖资源是否含有错误信息
            for (int i = 0, max = bundleRequest.dependencies.Count; i < max; i++)
            {
                var item = bundleRequest.dependencies[i];
                if (!item.error.IsNullOrEmpty())
                    return true;
            }

            switch (loadState)
            {
                case EBundleLoadState.None:
                    return false;
                case EBundleLoadState.Loaded:
                    return true;
                case EBundleLoadState.Unload:
                    return true;
                case EBundleLoadState.LoadingBundle:
                    // 检查bundle是否加载完成
                    if (!bundleRequest.isDone)
                        return false;
                    // 检查bundle的依赖资源是否加载完成
                    for (int i = 0, max = bundleRequest.dependencies.Count; i < max; i++)
                    {
                        var item = bundleRequest.dependencies[i];
                        if (!item.isDone)
                            return false;
                    }

                    if (bundleRequest.assetBundle == null)
                    {
                        error = "assetbundle is null !";
                        return true;
                    }
                    var assetName = System.IO.Path.GetFileName(assetUrl);
                    // 所有包含的bundle资源已经加载完成，开始加载bundle包含的资源文件
                    unityBundleRequset = bundleRequest.assetBundle.LoadAssetAsync(assetName, assetType);
                    // 切换状态为loadingAsset的状态
                    loadState = EBundleLoadState.LoadingAsset;
                    break;
                case EBundleLoadState.LoadingAsset:
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
            if (loadState != EBundleLoadState.LoadingAsset)
                return false;
            // 等待AssetBundleRequest加载资源
            if (!unityBundleRequset.isDone)
                return false;
            // 加载完成，修改完成状态
            assetObj = unityBundleRequset.asset;
            loadState = EBundleLoadState.Loaded;
            return true;
        }

        private float GetLoadingProgress()
        {

            var bundleLoadingProgress = bundleRequest.progress;
            if (bundleRequest.dependencies.Count <= 0)
                return bundleLoadingProgress * 0.3f + (unityBundleRequset == null ? 0 : unityBundleRequset.progress * 0.7f);

            int dependenciesCount = bundleRequest.dependencies.Count;
            for (int i = 0; i < dependenciesCount; i++)
            {
                var item = bundleRequest.dependencies[i];
                bundleLoadingProgress += item.progress;
            }
            var finalProgress = bundleLoadingProgress / (dependenciesCount + 1) * 0.3f + (unityBundleRequset == null ? 0 : unityBundleRequset.progress * 0.7f);
            return finalProgress;
        }


    }

    public class SceneAssetRequest : BaseAsset
    {
        public string assetBundleName;
        protected BundleRequest bundleRequest;

        public readonly LoadSceneMode loadSceneMode;
        protected readonly string sceneName;

        public SceneAssetRequest(string path,string bundleName,string sceneName,bool addictive)
        {
            assetUrl = path;
            assetBundleName = bundleName;
            this.sceneName = System.IO.Path.GetFileNameWithoutExtension(sceneName);
            loadSceneMode = addictive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        }

        public override float progress => 1;

        internal override void Load()
        {
            if(!assetBundleName.IsNullOrEmpty())
            {
                bundleRequest = AssetBundleHelper.Instance.LoadBundle(assetBundleName);
                if (bundleRequest != null)
                {
                    // 场景加载交给外部回掉去完成
                    //SceneManager.LoadScene(sceneName, loadSceneMode);
                }
            }
            else
            {
                try
                {
                    SceneManager.LoadScene(sceneName, loadSceneMode);
                    loadState = EBundleLoadState.LoadingAsset;
                }
                catch(System.Exception e)
                {
                    Debug.LogException(e);
                    error = e.ToString();
                    loadState = EBundleLoadState.Loaded;
                }
            }
        }
    }
}
