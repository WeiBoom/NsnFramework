using System;

namespace Nsn
{
    using System.Collections.Generic;
    using YooAsset;

    public class ResMgr : IAssetMgr
    {

        private YooAssets.InitializeParameters _initParameters;

        public void OnInitialized(params object[] args)
        {
            _initParameters = args[0] as YooAssets.InitializeParameters;
            if(_initParameters == null)
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

        public void Release(AssetOperationHandle handle)
        {
            handle.Release();
        }

    }
}