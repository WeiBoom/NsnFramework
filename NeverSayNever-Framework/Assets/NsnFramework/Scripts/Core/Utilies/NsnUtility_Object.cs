namespace Nsn
{
    using UnityEngine;
    
    /* Object 相关 */
    public static partial class NsnUtility
    {
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
    }
}