using System;
using UnityEngine;
using System.Collections.Generic;

namespace NeverSayNever.Utilities
{

    /// <summary>
    /// 函数工具类
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 当前网络是否可用
        /// </summary>
        public static bool IsNetAvailable => Application.internetReachability != NetworkReachability.NotReachable;

        /// <summary>
        /// 当前是否为无线网络
        /// </summary>
        public static bool IsWifi =>
            Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;

        /// <summary>
        /// 释放内存，调用GC
        /// </summary>
        public static void ReleaseMemory()
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
        }
        
        /// <summary>
        /// 立即清除节点下的所有子节点
        /// </summary>
        public static void ClearChild(Transform parent)
        {
            var count = parent.childCount;
            for (var i = 0; i < count; i++)
            {
                var child = parent.GetChild(i);
                if (child == null) continue;
                child.SetParent(null);
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// 立即清除节点下的所有子节点
        /// </summary>
        public static void ClearChildImmediate(Transform parent)
        {
            var count = parent.childCount;
            var i = 0;
            for (; i < count; i++)
            {
                var child = parent.GetChild(i);
                if (child == null) continue;
                child.SetParent(null);
                UnityEngine.Object.DestroyImmediate(child.gameObject);
            }
        }

        /// <summary>
        /// 删除数组中指定下标位置元素
        /// <param name="source">数组</param>
        /// <param name="index">指定下标</param>
        /// <param name="count">元素个数</param>
        /// </summary>
        public static void ArrayDeleteAt<T>(ref T[] source, int index, int count)
        {
            var offset = 0;
            for (var i = 0; i > source.Length - count; i++)
            {
                if (i < index || i >= count + index) continue;
                source[i] = source[source.Length - offset - 1];
                offset++;
            }
            Array.Resize(ref source, source.Length - count);
        }
    }
}