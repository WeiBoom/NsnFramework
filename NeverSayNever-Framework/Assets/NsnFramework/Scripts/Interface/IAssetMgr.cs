using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nsn
{
    using YooAsset;

    public interface IAssetMgr : IManager
    {
        int GetResourceVersion();

        AssetOperationHandle LoadAsset(Type type, string location);

        AssetOperationHandle LoadAsset<T>(string location) where T : UnityEngine.Object;

        AssetOperationHandle LoadAssetAsync<T>(string location) where T : UnityEngine.Object;

        void UnloadUnusedAssets();
        void ForceUnloadAllAssets();

    }
}
