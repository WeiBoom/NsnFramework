
using System;

namespace Nsn
{
    public class ResMgr : IResMgr
    {
        public void OnInitialized(params object[] args)
        {
        }

        public void OnDisposed()
        {
        }


        public void OnUpdate(float deltaTime)
        {

        }
        public int GetResourceVersion()
        {
            return 0;
        }

        public void LoadAsset(Type type, string location)
        {
        }

        public void LoadAsset<T>(string location) where T : UnityEngine.Object
        {
        }

        public void LoadAssetAsync(Type type, string location)
        {
        }

        public void LoadAssetAsync<T>(string location) where T : UnityEngine.Object
        {
        }


        public void UnloadUnusedAssets()
        {
        }
    }
}