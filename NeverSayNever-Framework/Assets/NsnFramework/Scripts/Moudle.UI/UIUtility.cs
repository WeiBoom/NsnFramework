using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Nsn
{
    public static class UIUtility
    {
        private static readonly Queue<Transform> _childNodeSearchQueue = new Queue<Transform>();

        public static Transform Search(Transform root, string childName)
        {
            if (root == null || root.childCount == 0 || string.IsNullOrEmpty(childName))
				return null;
            _childNodeSearchQueue.Clear();
            _childNodeSearchQueue.Enqueue(root);

            while(_childNodeSearchQueue.Count != 0)
            {
                Transform node = _childNodeSearchQueue.Dequeue();
                int childCount = node.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = root.GetChild(i);
                    if (child.name.Equals(childName))
                        return child;
                    else
                    {
                        if (child.childCount != 0)
                            _childNodeSearchQueue.Enqueue(child);
                    }
                }
            }
            _childNodeSearchQueue.Clear();
            return null;
        }

        public static T Search<T>(Transform root, string childName)
        {
            Transform childNode  = Search(root, childName);
            if(childNode != null)
                return childNode.GetComponent<T>();
            
            return default(T);
        }
    }
}