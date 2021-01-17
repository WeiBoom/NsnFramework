using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeverSayNever.Example
{
    using NeverSayNever.Core;
    using NeverSayNever.Core.Asset;

    public class GameLauncher : MonoBehaviour
    {
        [Title("资源加载方案")] 
        private EAssetLoadType loadType = EAssetLoadType.AssetDataBase;
        [Title("UI 根节点")]
        public GameObject UIRoot;
        [Title("音频 根节点")]
        public AudioSource AudioSourceRoot;
        [InfoBox("是否加载Lua")]
        public bool luaMode;
        [ShowIf("luaMode")][InfoBox("是否以AB模式加载lua（需要先打包）")]
        public bool luaBundle;
        [InfoBox("是否启用AssetBundle模式")]
        public bool bundleMode;

        private void Awake()
        {
            loadType = bundleMode ? EAssetLoadType.AssetBundle : EAssetLoadType.AssetDataBase;

            Framework.SetLuaMode(luaMode, luaBundle);
            Framework.SetAssetLoadType(loadType);
            Framework.SetUIRoot(UIRoot);
            Framework.SetAudioSourceRoot(AudioSourceRoot);

            Framework.StartUp();
        }

        private void Start()
        {
            Debug.Log("GameLauncher Start");
            if (loadType == Core.Asset.EAssetLoadType.AssetBundle)
            {
                // 预先加载shader，material，font等资源
                AssetBundleHelper.Instance.LoadBundle("shaders.u3d");
                AssetBundleHelper.Instance.LoadBundle("fonts.u3d");
            }

            if(luaMode)
            {
                LuaManager.Instance.DoFile("Launcher");
            }
            else
            {
                //注册UI界面
                UIManager.Instance.RegisterCsPanelByReflect("GameMain");
                //打开面板
                UIManager.Instance.OpenPanel("GameMainPanel");
            }

        }

        private void Update()
        {
            Framework.OnUpdate();
        }
    }
}
