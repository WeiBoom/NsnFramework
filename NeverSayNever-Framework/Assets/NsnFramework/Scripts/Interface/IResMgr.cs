using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
