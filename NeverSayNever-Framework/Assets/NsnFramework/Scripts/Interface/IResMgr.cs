using System;

namespace Nsn
{
    using UObject = UnityEngine.Object;

    public interface IResMgr : IManager
    {
        void LoadAsset(System.Type type, string location, System.Action<UObject> callback);

        void LoadAsset<T>(string location, System.Action<UObject> callback) where T : UObject;

        void LoadAssetAsync(System.Type type, string location, System.Action<UObject> callback);

        void LoadAssetAsync<T>(string location, System.Action<UObject> callback) where T : UObject;

        void UnloadUnusedAssets();

        int GetResourceVersion();
    }
}
