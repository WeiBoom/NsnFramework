
using System;
using UnityEngine;

namespace NeverSayNever.Utilities
{
    public static class AppConst
    {

        public const string AppName = "NeverSayNever";
        //Socket服务器端口
        public const ushort SocketPort = 8080;
        //Socket服务器地址
        public const string SocketAddress = "127.0.0.1";        
        //Lua脚本放在Assets同层文件LuaScripts下面，这样可以不用处理meta文件的问题，同时避免更新脚本Unity自动加载卡顿的问题
        public static string EditorLuaScriptPath => Application.dataPath.Replace("Assets", "") + "LuaScripts/";

        /// <summary>
        /// 存放的资源根目录
        /// </summary>
        public static string DataPath
        {
            get
            {
                if(Application.isMobilePlatform) 
                {
                    return Application.persistentDataPath + "/";
                }
#if UNITY_EDITOR

                return Application.dataPath + "/StreamingAssets/";
#else

                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    int i = Application.dataPath.LastIndexOf('/');
                    return Application.dataPath.Substring(0, i + 1) + "/";
                }
                return "c:/";
#endif
            }
        }

        public static string Platform
        {
            get
            {
#if UNITY_EDITOR
                switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
                {
                    case UnityEditor.BuildTarget.Android:
                        return "Android";
                    case UnityEditor.BuildTarget.iOS:
                        return "iOS";
                    case UnityEditor.BuildTarget.StandaloneOSX:
                        return "OSX";
                    case UnityEditor.BuildTarget.StandaloneWindows:
                    case UnityEditor.BuildTarget.StandaloneWindows64:
                        return "Windows";
                    default:
                        return "Standalone";
                }
#else
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        return "Android";
                    case RuntimePlatform.IPhonePlayer:
                        return "iOS";
                    case RuntimePlatform.WindowsPlayer:
                        return "Windows";
                    case RuntimePlatform.OSXPlayer:
                        return "OSX";
                    default:
                        return "Default";
                }
#endif
            }
        }

    } 

}