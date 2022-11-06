using System;
using UObject = UnityEngine.Object;

namespace NeverSayNever
{
    public enum EAssetType
    {
        UI,
        Model,
        Effect,
        Texture,
        Audio,
        Font,
        Shader,
        TextAsset,
        Scene,
        Atlas,
    }

    public enum EAssetFileType
    {
        Prefab,
        Texture,
        Atlas,
        Scene,
        Audio,
        Animation,
        TextAsset,
    }

    public enum EAssetLoadType
    {
        AssetDataBase,
        Resources,
        AssetBundle,
    }

    public interface IResMdl : IModule
    {
        void LoadUIAsset(string uiName, Action<object> callback);

        void LoadAudio(string audioName, Action<object> callback);

        void LoadTextAsset(string textName, Action<object> callback);

        void LoadModel(string modelName, Action<object> callback);

        void LoadTexture(string texName, Action<object> callback);

        void LoadScene(string sceneName, Action<object> callback);

        void ReleaseObject(UObject target);
    }
}

