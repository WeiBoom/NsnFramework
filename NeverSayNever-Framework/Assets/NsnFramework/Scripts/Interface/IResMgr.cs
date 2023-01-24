using System;

namespace Nsn
{
    public interface IResMgr : IManager
    {
        void LoadAsset(System.Type type, string location);

        void LoadAsset<T>(string location) where T : UnityEngine.Object;

        void LoadAssetAsync(System.Type type, string location);

        void LoadAssetAsync<T>(string location) where T : UnityEngine.Object;

        void UnloadUnusedAssets();

        int GetResourceVersion();
    }
}
