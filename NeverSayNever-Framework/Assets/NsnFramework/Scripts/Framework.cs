using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nsn
{
    using UObject = UnityEngine.Object;

    public class Framework 
    {
        #region Framework Setting

        /// <summary>
        /// 是否启用lua脚本
        /// </summary>
        [HideInInspector]
        public static bool IsUsingLuaScript = false;

        /// <summary>
        /// 是否以AssetBundle的方式加载lua脚本
        /// </summary>
        public static bool IsUsingLuaBundleMode { get; private set; } = false;

        /// <summary>
        /// 框架核心script承载mono对象
        /// </summary>
        public static GameObject BridgeObject { get; private set; }

        /// <summary>
        /// UI根节点
        /// </summary>
        public static GameObject UIRoot { get; private set; }

        /// <summary>
        /// 音频播放源节点
        /// </summary>
        public static AudioSource AudioSource { get; private set; }

        #endregion

        private static Dictionary<string, IManager> mManagerDic;

        public static void StartUp()
        {
            mManagerDic = new Dictionary<string, IManager>(10);
            BridgeObject = new GameObject("NsnFramework");
            UObject.DontDestroyOnLoad(BridgeObject);

            // 添加协程管理的模块
            BridgeObject.AddComponent<CoroutineMgr>();
        }

        public static void OnUpdate(float deltaTime)
        {
            UpdateMgr(deltaTime);
        }

        private static void UpdateMgr(float deltaTime)
        {
            var e = mManagerDic.GetEnumerator();
            while (e.MoveNext())
            {
                e.Current.Value.OnUpdate(deltaTime);
            }
            e.Dispose();
        }


        public static void AddManager<T>(params object[] args) where T : IManager
        {
            System.Reflection.Assembly assembly = typeof(T).Assembly;
            string targetNamespace = typeof(T).Namespace;
            string typeName = typeof(T).Name;
            string targetName = typeName.Substring(1, typeName.Length - 1);
            string target = string.IsNullOrEmpty(targetNamespace) ? targetName : $"{targetNamespace}.{targetName}";
            if (!mManagerDic.ContainsKey(target))
            {
                object inst = RuntimeAssembly.CreateInstance(assembly, target, args);
                T script = (T)inst;
                if (script != null)
                {
                    //script.OnInitialized(args);
                    mManagerDic.Add(target, script);
                }
            }
        }

        public static T GetManager<T>() where T : IManager
        {   
            string key = typeof(T).ToString();
            mManagerDic.TryGetValue(key, out IManager mgr);
            return (T)mgr;
        }

        public static void SetLuaMode(bool enable,bool bundleMode)
        {
            IsUsingLuaScript = enable;
            IsUsingLuaBundleMode = enable && bundleMode;
        }

        public static void SetUIRoot(GameObject uiroot)
        {
            UIRoot = uiroot;
        }

        public static void SetAudioSourceRoot(AudioSource audioRoot)
        {
            AudioSource = audioRoot;
        }

        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;

            public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
            {
            }
        }

        public IList<IList<int>> ZigzagLevelOrder(TreeNode root) {
        
            List<IList<int>> ret = new List<IList<int>>();
            if(root == null) return ret;
            LinkedList<TreeNode> l = new LinkedList<TreeNode>();
            bool oriLeft = false;
            l.AddFirst(root);
            while(l.Count > 0)
            {
                int length = l.Count;
                List<int> list = new List<int>();
                for(int i = 0; i< length;i++)
                {
                    var node = l.First.Value;
                    list.Add(node.val);
                    l.RemoveFirst();
                    if(oriLeft)
                    {
                        list.Add(node.val);
                        l.ToList();
                        if(node.left != null) l.AddLast(node.left);
                        if(node.right != null) l.AddLast(node.right);
                    }
                    else{
                        if(node.left != null) l.AddFirst(node.left);
                        if(node.right != null) l.AddFirst(node.right);
                    }
                }
                oriLeft = !oriLeft;
                ret.Add(list);
            }

            return ret;
        }
    }
}