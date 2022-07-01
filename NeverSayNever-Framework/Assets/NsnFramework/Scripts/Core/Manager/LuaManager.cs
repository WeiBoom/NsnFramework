using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

using NeverSayNever.Core.Asset;
using NeverSayNever.Utilitiy;

namespace NeverSayNever.Core
{
    using NeverSayNever.Core.Asset;

    public class LuaManager :Singleton<LuaManager>
    {
        private const string LuaSuffix = ".lua";
        /// <summary>
        /// 非编辑器环境下，通过bundle加载的文件资源映射
        /// </summary>
        private readonly Dictionary<string, string> CodeFileMap = new Dictionary<string, string>();

        /// <summary>
        /// 编辑器下脚本的相对路径
        /// </summary>
        private readonly Dictionary<string, string> _editorScripsPathMap = new Dictionary<string, string>();

        // 打开一个lua界面 ，缓存lua里的全局方法
        private LuaFunction _callLuaNewWindow;

        /// <summary>
        /// XLua运行环境
        /// </summary>
        private static LuaEnv _luaEnv;
        // lua 上次gc的时间
        private static float LastGCTime = 0;
        // lua gc间隔时间
        private const float GCInterval = 1;
        // lua入口脚本
        private static string launchFileName = "Launcher";
        // lua 打包成的bundle资源的名字
        private const string luaBundleName = "lualogic.u3d";
        
        public static LuaTable Global => _luaEnv?.Global;

        public override void OnInitialize(params object[] args)
        {
            base.OnInitialize(args);
            //ULog.Print("加载Lua脚本");
            _luaEnv = new LuaEnv();
            _luaEnv.AddLoader((ref string filename) => System.Text.Encoding.UTF8.GetBytes(filename == "InMemory" ? "return {ccc = 9999}" : ReadLuaFile(filename)));

            launchFileName = args[0] as string;

            // Bundle 模式下需要先加载Lua的bundle资源，命名为luaLogic
            if (Framework.IsUsingLuaBundleMode)
                LoadScriptBundle(luaBundleName);
            else
            {
                var luaFilePath = FrameworkConfig.CommonConfig.LuaSciprtDirectory;
                GetAllLuaFiles(luaFilePath);
            }
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Time.time - LastGCTime > GCInterval)
            {
                _luaEnv?.Tick();
                LastGCTime = Time.time;
            }
        }

        public override void OnDispose()
        {
            base.OnDispose();
            _luaEnv?.Dispose();
        }

        /// <summary>
        /// 编辑器模式下获取指定目录下的所有Lua脚本
        /// </summary>
        private void GetAllLuaFiles(string strDirectory)
        {
            var directory = new DirectoryInfo(strDirectory);
            // 指定目录下的所有文件
            var directoryArray = directory.GetDirectories();
            // 当前文件下的所有文件
            var fileInfoArray = directory.GetFiles();
            if(fileInfoArray.Length > 0 )
            {
                foreach(var f in fileInfoArray)
                {
                    if (f.Name.EndsWith(LuaSuffix)) // 注意lua文件的后缀是小写的lua
                    {
                        _editorScripsPathMap.Add(f.Name.ToLower(), f.FullName.Replace("\\", "/"));
                    }
                }
            }
            // 递归遍历所有的文件
            foreach(var d in directoryArray)
            {
                GetAllLuaFiles(d.FullName);
            }
        }

        public LuaTable NewTable()
        {
            return _luaEnv?.NewTable();
        }
        
        /// <summary>
        /// 加载Lua的bundle资源，并缓存到映射表中
        /// </summary>
        /// <param name="name"></param>
        public void LoadScriptBundle(string name)
        {
            var luaBundleRequest = AssetBundleHelper.Instance.LoadBundle(name);
            if (luaBundleRequest == null) return;

            var luaBundle = luaBundleRequest.assetObj as AssetBundle;
            var luaFiles = luaBundle.LoadAllAssets<TextAsset>();
            foreach (var t in luaFiles)
            {
                CodeFileMap.Add(t.name.ToLower(), t.text);
            }
            luaBundleRequest.Unload();
        }

        /// <summary>
        /// 读取Lua文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ReadLuaFile(string fileName)
        {
            var strs = fileName.ToLower().Split('/');
            fileName = strs[strs.Length - 1];
            string str = null;
            if (Framework.IsUsingLuaBundleMode)
            {
                CodeFileMap.TryGetValue(fileName, out str);
                if (str == null)
                    Debug.LogError("没有找到脚本:" + fileName);
            }
            else
            {
                if (!fileName.Contains(LuaSuffix))
                    fileName += LuaSuffix;
                if (_editorScripsPathMap.ContainsKey(fileName))
                    str = File.ReadAllText(_editorScripsPathMap[fileName]);
            }
            return str;
        }

        /// <summary>
        /// 调用Lua层的方法，创建一个界面
        /// </summary>
        /// <param name="scriptName"></param>
        /// <returns></returns>
        public LuaTable CallNewLuaWindowFunc(string scriptName)
        {
            // 这里是 调用Lua全局方法中的NewLuaWindow的方法，创建一个界面
            try
            {
                if (_callLuaNewWindow == null)
                    _callLuaNewWindow = GetFunction("NewLuaWindow");
                return _callLuaNewWindow.Call(scriptName)[0] as LuaTable;
            }
            catch (Exception e)
            {
                Debug.LogError(e);

                throw;
            }
        }

        /// <summary>
        /// 执行lua文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public object[] DoFile(string fileName)
        {
            // 空的文件名
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("请传入正确的lua文件名");
                return null;
            }
            var scr = ReadLuaFile(fileName);
            var isEmpty = string.IsNullOrEmpty(scr);
            return isEmpty ? null : _luaEnv.DoString(scr, fileName + LuaSuffix);
        }

        /// <summary>
        /// 获取lua方法
        /// </summary>
        /// <param name="funcName"></param>
        /// <returns></returns>
        private LuaFunction GetFunction(string funcName)
        {
            return _luaEnv?.Global.Get<LuaFunction>(funcName);
        }

    }
}
