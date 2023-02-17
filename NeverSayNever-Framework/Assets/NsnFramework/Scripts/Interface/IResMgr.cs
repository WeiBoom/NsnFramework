using System;

namespace Nsn
{
    using UObject = UnityEngine.Object;
    using Object = System.Object;
    
    public interface IResMgr : IManager
    {
        void LoadAsset(Type type, string location, Action<UObject> callback);

        void LoadAsset<T>(string location, Action<T> callback) where T : UObject;

        void LoadAssetAsync(Type type, string location, Action<UObject> callback);

        void LoadAssetAsync<T>(string location, Action<T> callback) where T : UObject;

        void UnloadUnusedAssets();

        int GetResourceVersion();
    }
}
