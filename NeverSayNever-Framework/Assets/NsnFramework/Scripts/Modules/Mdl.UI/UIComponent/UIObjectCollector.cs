using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    /// <summary>
    /// UI 节点容器,根据规则收集UI下所有的控件节点
    /// </summary>
    [RequireComponent(typeof(UIViewBase))]
    public class UIObjectCollector : UIBehaviour
    {
        public enum LinkedObjectType
        {
            None,
            Transform,
            Button,
            Image,
            Texture,
            Label,
            InputField,
            Grid,
            Scroll,
            Slider,
        }

        [Serializable]
        public class LinkedNode
        {
            public string name;
            public LinkedObjectType type;
            public Component node;

            public T GetNode<T>() where T : Component
            {
                var target = GetNode();
                return target as T;
            }

            public Component GetNode() => node;
        }

        // 固定的控件类型，由业务自己拖拽绑定
        [SerializeField]
        protected SerializableDictionary<string, LinkedNode> m_FixedLinkedNodesDic = new SerializableDictionary<string, LinkedNode>();
        // 动态的控件类型，自动生成，业务不关心
        [SerializeField]
        protected SerializableDictionary<string, LinkedNode> m_DynamicLinkedNodesDic = new SerializableDictionary<string, LinkedNode>();


        public Component GetDynamicNodeComponent(string key)
        {
            m_DynamicLinkedNodesDic.TryGetValue(key, out var linkedObject);
            return GetNodeComponent(linkedObject);
        }

        public Component GetFixedNodeComponent(string key)
        {
            m_FixedLinkedNodesDic.TryGetValue(key, out var linkedObject);
            return GetNodeComponent(linkedObject);
        }
        
        public Component GetNodeComponent(string key)
        {
            var comp = GetDynamicNodeComponent(key);
            if (comp == null)
                comp = GetFixedNodeComponent(key);
            return comp;
        }
        
        
        /// <summary>
        /// 收集指定的组件
        /// </summary>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void CollectControl()
        {
            // 清理动态控件
            m_DynamicLinkedNodesDic.Clear();
            // 通过搜索队列的方式，不用递归，减少函数栈
            Queue<Transform> checkQueue = new Queue<Transform>();
            checkQueue.Enqueue(this.transform);

            while (checkQueue.Count > 0 )
            {
                Transform node = checkQueue.Dequeue();
                int childCount = node.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform child = node.GetChild(i);
                    TryAddTargetToDynamicObjectDic(child);
                    if (child.childCount > 0)
                        checkQueue.Enqueue(child);
                }
            }
        }

        /// <summary>
        /// 递归查找节点，并添加到对应的文件中
        /// </summary>
        /// <param name="parent"></param>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void CollectControl(Transform parent)
        {
            if (parent == null) return;
            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = parent.GetChild(i);
                TryAddTargetToDynamicObjectDic(child);
                CollectControl(child);
            }
        }

        private void TryAddTargetToDynamicObjectDic(Transform node)
        {
            if (node != null)
            {
                LinkedObjectType nodeType = GetNodeTypeByName(node);
                if (nodeType == LinkedObjectType.None)
                    return;
                if (m_DynamicLinkedNodesDic.ContainsKey(node.name))
                {
                    NsnLog.Error("[ObjectLinker] CollectControl . Error! It can't contain control with same name : ", node.name);
                }
                else
                {
                    m_DynamicLinkedNodesDic.Add(node.name,
                        new LinkedNode() { name = node.name, type = nodeType, node = node });
                }
            }
        }

        private LinkedObjectType GetNodeTypeByName(Transform node)
        {
            string nodeName = node.transform.name;
            if(name.StartsWith("Trans_"))
                return LinkedObjectType.Transform;
            else if(name.StartsWith("Btn_"))
                return LinkedObjectType.Button;
            else if(name.StartsWith("Img_"))
                return LinkedObjectType.Image;
            else if(name.StartsWith("Tex_"))
                return LinkedObjectType.Texture;
            else if(name.StartsWith("Txt_"))
                return LinkedObjectType.Label;
            else if(name.StartsWith("Input_"))
                return LinkedObjectType.InputField;
            else if(name.StartsWith("Grid_"))
                return LinkedObjectType.Grid;
            else if(name.StartsWith("Scroll_"))
                return LinkedObjectType.Scroll;
            else if(name.StartsWith("Slider_"))
                return LinkedObjectType.Slider;
            return LinkedObjectType.None;
        }

        private Component GetNodeComponent(LinkedNode linkedNode)
        {
            if (linkedNode == null || linkedNode.node == null)
                return null;
            LinkedObjectType nodeType = linkedNode.type;
            if (nodeType == LinkedObjectType.Transform)
                return linkedNode.GetNode<Transform>();
            else if (nodeType == LinkedObjectType.Button)
                return linkedNode.GetNode<Button>();
            else if (nodeType == LinkedObjectType.Image)
                return linkedNode.GetNode<Image>();
            else if (nodeType == LinkedObjectType.Texture)
                return linkedNode.GetNode<RawImage>();
            else if (nodeType == LinkedObjectType.Label)
                return linkedNode.GetNode<Text>();
            else if (nodeType == LinkedObjectType.InputField)
                return linkedNode.GetNode<InputField>();
            else if (nodeType == LinkedObjectType.Grid)
                return linkedNode.GetNode<Grid>();
            else if (nodeType == LinkedObjectType.Scroll)
                return linkedNode.GetNode<ScrollRect>();
            else if (nodeType == LinkedObjectType.Slider)
                return linkedNode.GetNode<Slider>();

            return null;
        }
    }
}
