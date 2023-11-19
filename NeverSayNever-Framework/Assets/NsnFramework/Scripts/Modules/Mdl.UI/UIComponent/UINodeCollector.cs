using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using Sirenix.OdinInspector;

namespace Nsn
{
    /// <summary>
    /// UI 节点容器,根据规则收集UI下所有的控件节点
    /// </summary>
    public class UINodeCollector : UIBehaviour
    {
        public enum LinkedObjectType
        {
            None,
            Transform,
            Button,
            Image,
            Texture,
            Text,
            TextMeshPro,
            InputField,
            Grid,
            Scroll,
            Slider,
        }

        [Serializable]
        public class LinkedNode
        {
            [HorizontalGroup("LinkedNodeAttribute",Width =100),LabelWidth(30)]
            public LinkedObjectType type;
            [HorizontalGroup("LinkedNodeAttribute"), LabelWidth(35)]
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


#if UNITY_EDITOR
        public SerializableDictionary<string, LinkedNode> FixedLinkedNodesDic => m_FixedLinkedNodesDic;

        public SerializableDictionary<string, LinkedNode> DynamicLinkedNodesDic => m_DynamicLinkedNodesDic;
#endif

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
        [Button("Collect Dynamic Nodes")]
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public void CollectNodes()
        {
            ClearDynamicNodes();
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
        
        [HorizontalGroup("ClearButtonGroup")]
        [Button("清空Fixed节点"),GUIColor(1.0f, 0.2f, 0) ] 
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        private void ClearFixedNodes()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorUtility.DisplayDialog("NsnFramework.UIMdl", "确定要清理掉Fixed收集的节点吗？", "确定", "取消"))
            {
                m_FixedLinkedNodesDic.Clear();
            }
#endif
        }
        
        [HorizontalGroup("ClearButtonGroup")]
        [Button("清空Dynamic节点"),GUIColor(0.0f, 0.6f, 0) ]
        public void ClearDynamicNodes()
        {
            m_DynamicLinkedNodesDic.Clear();
        }

        /// <summary>
        /// 递归查找节点，并添加到对应的文件中
        /// </summary>
        /// <param name="parent"></param>
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public void CollectNodes(Transform parent)
        {
            if (parent == null) return;
            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = parent.GetChild(i);
                TryAddTargetToDynamicObjectDic(child);
                CollectNodes(child);
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
                        new LinkedNode() { type = nodeType, node = node });
                }
            }
        }

        private LinkedObjectType GetNodeTypeByName(Transform node)
        {
            string nodeName = node.transform.name;
            if(nodeName.StartsWith("Node"))
                return LinkedObjectType.Transform;
            else if(nodeName.EndsWith("Btn"))
                return LinkedObjectType.Button;
            else if(nodeName.EndsWith("Img"))
                return LinkedObjectType.Image;
            else if(nodeName.EndsWith("Tex"))
                return LinkedObjectType.Texture;
            else if(nodeName.EndsWith("Txt"))
                return LinkedObjectType.Text;
            else if (nodeName.EndsWith("TMP"))
                return LinkedObjectType.TextMeshPro;
            else if(nodeName.EndsWith("Input"))
                return LinkedObjectType.InputField;
            else if(nodeName.EndsWith("Grid"))
                return LinkedObjectType.Grid;
            else if(nodeName.EndsWith("Scroll"))
                return LinkedObjectType.Scroll;
            else if(nodeName.EndsWith("Slider"))
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
            else if (nodeType == LinkedObjectType.Text)
                return linkedNode.GetNode<Text>();
            else if (nodeType == LinkedObjectType.TextMeshPro)
                return linkedNode.GetNode<TMPro.TextMeshProUGUI>();
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
