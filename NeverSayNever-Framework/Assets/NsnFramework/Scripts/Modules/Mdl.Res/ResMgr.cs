using System;

namespace Nsn
{
    using System.Collections.Generic;
    using UnityEngine;
    using YooAsset;

    using UObject = UnityEngine.Object;
    using Object = System.Object;

    public class ResMgr : IResMgr
    {
        public void OnInitialized(params object[] args)
        {
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnDisposed()
        {
        }

        public void LoadAsset(Type type, string location, Action<UObject> callback)
        {
            LoadAssetInternal(type, location, callback, false);
        }

        public void LoadAsset<T>(string location, Action<T> callback = null) where T : UnityEngine.Object
        {
            LoadAssetInternal<T>(location, callback, false);
        }

        public void LoadAssetAsync(Type type, string location, Action<UObject> callback)
        {
            LoadAssetInternal(type, location, callback, true);
        }

        public void LoadAssetAsync<T>(string location, Action<T> callback) where T : UnityEngine.Object
        {
            LoadAssetInternal<T>(location, callback, true);
        }

        public int GetResourceVersion()
        {
            return YooAssets.GetResourceVersion();
        }

        public void UnloadUnusedAssets()
        {
            YooAssets.UnloadUnusedAssets();
        }

        private void LoadAssetInternal<T>(string location, Action<T> callback, bool isAsync) where T : UObject
        {
            AssetOperationHandle operation = null;
            if (!isAsync)
                operation = YooAssets.LoadAssetSync<T>(location);
            else
                operation = YooAssets.LoadAssetAsync<T>(location);

            if (callback != null)
            {
                operation.Completed += handle => { callback.Invoke(handle.AssetObject as T); };
            }
        }

        private void LoadAssetInternal(System.Type type, string location, Action<UObject> callback, bool isAsync)
        {
            AssetOperationHandle operation = null;
            if (!isAsync)
                operation = YooAssets.LoadAssetSync(location, type);
            else
                operation = YooAssets.LoadAssetAsync(location, type);

            if (callback != null)
            {
                operation.Completed += handle => { callback.Invoke(handle.AssetObject); };
            }
        }


    }

    public class AssetMgr : IAssetMgr
    {
        private YooAssets.InitializeParameters _initParameters;

        public void OnInitialized(params object[] args)
        {
            _initParameters = args[0] as YooAssets.InitializeParameters;
            if (_initParameters == null)
                throw new Exception("ResMgr initialize failed , params is invalid .");

            InitializationOperation operation = InitializeAsync();
        }

        private InitializationOperation InitializeAsync()
        {
            return YooAssets.InitializeAsync(_initParameters);
        }

        public void OnDisposed()
        {
            ForceUnloadAllAssets();
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public int GetResourceVersion()
        {
            return YooAssets.GetResourceVersion();
        }

        public AssetOperationHandle LoadAsset(Type type, string location)
        {
            return YooAssets.LoadAssetSync(location, type);
        }

        public AssetOperationHandle LoadAsset<T>(string location) where T : UnityEngine.Object
        {
            return YooAssets.LoadAssetSync<T>(location);
        }

        public AssetOperationHandle LoadAssetAsync(Type type, string location)
        {
            return YooAssets.LoadAssetAsync(location, type);
        }

        public AssetOperationHandle LoadAssetAsync<T>(string location) where T : UnityEngine.Object
        {
            return YooAssets.LoadAssetAsync<T>(location);
        }

        public void UnloadUnusedAssets()
        {
            YooAssets.UnloadUnusedAssets();
        }

        public void ForceUnloadAllAssets()
        {
            YooAssets.ForceUnloadAllAssets();
        }
    }


}