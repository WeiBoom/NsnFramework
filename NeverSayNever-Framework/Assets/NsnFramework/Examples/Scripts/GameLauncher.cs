using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NeverSayNever.Example
{
    using NeverSayNever;

    public class GameLauncher : MonoBehaviour
    {
        [LabelText("资源加载方案")] 
        private EAssetLoadType loadType = EAssetLoadType.AssetDataBase;
        [LabelText("UI 根节点")]
        public GameObject UIRoot;
        [LabelText("音频 根节点")]
        public AudioSource AudioSourceRoot;
        [LabelText("是否启用AssetBundle模式")]
        public bool bundleMode;
        [LabelText("是否加载Lua"),HorizontalGroup("Lua")]
        public bool luaMode;
        [ShowIf("luaMode")][HorizontalGroup("Lua"),LabelText("使用lua[AB](需要先打包)")]
        public bool luaBundle;

        
        private void InitFramework()
        {
            loadType = bundleMode ? EAssetLoadType.AssetBundle : EAssetLoadType.AssetDataBase;

            // -- 废弃 todo
            Framework.SetLuaMode(luaMode, luaBundle);
            Framework.SetAssetLoadType(loadType);
            Framework.SetUIRoot(UIRoot);
            Framework.SetAudioSourceRoot(AudioSourceRoot);
            // -- 废弃 todo

            // 只通过这一个接口初始化
            NeverSayNever.FrameworkCore.Initialize();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitFramework();

            Debug.Log("GameLauncher Start");
            if (loadType == EAssetLoadType.AssetBundle)
            {
                // 预先加载shader，material，font等资源
                AssetBundleHelper.Inst.LoadBundle("shaders.u3d");
                AssetBundleHelper.Inst.LoadBundle("fonts.u3d");
            }

            if(luaMode)
            {
                //LuaMgr.DoFile("Launcher");
            }
            else
            {
                //注册UI界面
                //UIManager.Instance.RegisterCsPanelByReflect(UIModuleGroup.GameMain.GetModuleName());
                //UIManager.Instance.RegisterCsPanelByReflect(UIModuleGroup.GameLoading.GetModuleName());
                //打开面板
                //UIManager.Instance.OpenPanel(UIModuleGroup.GameMain.GetModuleName());
            }

        }

        private void Update()
        {
            //Framework.OnUpdate();
        }
    }
}
