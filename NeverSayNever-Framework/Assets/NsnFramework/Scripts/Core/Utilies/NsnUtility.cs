using System;
using UnityEngine;
using System.Collections.Generic;

namespace Nsn
{
    public static partial class NsnUtility
    {
        /// <summary>
        /// 释放内存，调用GC
        /// </summary>
        public static void ReleaseMemory()
        {
            GC.Collect();
            Resources.UnloadUnusedAssets();
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