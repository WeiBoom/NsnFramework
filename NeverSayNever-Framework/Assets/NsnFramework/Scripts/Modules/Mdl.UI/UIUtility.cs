using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Nsn
{
    public static class UIUtility
    {
        private static readonly Queue<Transform> m_ChildNodeSearchQueue = new Queue<Transform>();

        public static Transform Search(Transform root, string childName)
        {
            if (root == null || root.childCount == 0 || string.IsNullOrEmpty(childName))
				return null;
            m_ChildNodeSearchQueue.Clear();
            m_ChildNodeSearchQueue.Enqueue(root);

            while(m_ChildNodeSearchQueue.Count != 0)
            {
                Transform node = m_ChildNodeSearchQueue.Dequeue();
                int childCount = node.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = root.GetChild(i);
                    if (child.name.Equals(childName))
                        return child;
                    else
                    {
                        if (child.childCount != 0)
                            m_ChildNodeSearchQueue.Enqueue(child);
                    }
                }
            }
            m_ChildNodeSearchQueue.Clear();
            return null;
        }

        public static T Search<T>(Transform root, string childName)
        {
            Transform childNode  = Search(root, childName);
            if(childNode != null)
                return childNode.GetComponent<T>();
            
            return default(T);
        }


        #region Extension

        public static bool IsPrepared(this UIViewItem viewItem)
        {
            if (viewItem == null || string.IsNullOrEmpty(viewItem.ViewName) || viewItem.ViewID == 0)
                return false;
            if (viewItem.ViewObj == null)
                return false;
            return true;
        }

        #endregion
    }
}