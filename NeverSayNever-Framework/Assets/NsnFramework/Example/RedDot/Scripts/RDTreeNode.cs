using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn.Example
{
    public class RDTreeNode
    {
        private Dictionary<RDString, RDTreeNode> m_Children;
        private System.Action<int> m_ValueChangeCallback;
        private string m_FullPath;

        /// <summary>
        /// 节点名ForceMeshUpdate
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 节点值
        /// </summary>
        public int Value { get; private set; }
        
        /// <summary>
        /// 父节点对象
        /// </summary>
        public RDTreeNode Parent { get; private set; }

        /// <summary>
        /// 子节点对象
        /// </summary>
        public Dictionary<RDString, RDTreeNode>.ValueCollection Children => m_Children?.Values;

        /// <summary>
        /// 子节点的数量(包含所有叶子节点)
        /// </summary>
        public int ChildCount 
        {
            get
            {
                if (m_Children == null)
                    return 0;
                int sum = m_Children.Count;
                foreach (var child in m_Children.Values)
                    sum += child.ChildCount;
                return sum;
            }
        }
        
        /// <summary>
        /// 节点路径
        /// </summary>
        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(m_FullPath))
                {
                    if (Parent == null || Parent == RedDotMgr.Inst.Root)
                        m_FullPath = Name;
                    else
                        m_FullPath = Parent.FullPath + RedDotMgr.SplitChar + Name;
                }
                return m_FullPath;
            }
        }


        public RDTreeNode(string name)
        {
            Name = name;
            Value = 0;
            m_ValueChangeCallback = null;
        }

        public RDTreeNode(string name, RDTreeNode parent) : this(name)
        {
            Parent = parent;
        }


        public void AddListener(System.Action<int> callback) => m_ValueChangeCallback += callback;

        public void RemoveListener(System.Action<int> callback) => m_ValueChangeCallback -= callback;

        public void RemoveAllListener() => m_ValueChangeCallback = null;

        public void ChangeValue(int newValue)
        {
            // 只能修改叶子节点，如果存在子节点，则不能直接修改当前值
            if (m_Children != null && m_Children.Count != 0)
                return;
            InternalChangeValue(newValue);
        }

        public void ChangeValue()
        {
            // 根据子节点的值修改自身节点，如果子节点为空，则不处理
            if (m_Children == null || m_Children.Count == 0)
                return;
                            
            int sum = 0;
            foreach (var child in m_Children)
                sum += child.Value.Value;
            InternalChangeValue(sum);
        }

        private void InternalChangeValue(int newValue)
        {
            if (newValue == Value) return;
            Value = newValue;
            m_ValueChangeCallback?.Invoke(newValue);
            // 数值改变，标记为脏数据
            RedDotMgr.Inst.MarkDirtyNode(this);
            RedDotMgr.Inst.NotifyNodeValueChanged(this, newValue);
        }

        public RDTreeNode GetOrAddChild(RDString key)
        {
            RDTreeNode child = GetChild(key);
            if (child == null)
                child = AddChild(key);

            return child;
        }

        public RDTreeNode GetChild(RDString key)
        {
            if (m_Children == null)
                return null;
            m_Children.TryGetValue(key, out var child);
            return child;
        }

        public RDTreeNode AddChild(RDString key)
        {
            if (m_Children == null)
                m_Children = new Dictionary<RDString, RDTreeNode>();
            if (m_Children.ContainsKey(key))
            {
                NsnLog.Error("红点异常! 不允许重复添加子节点  : " + FullPath);
                return null;
            }

            RDTreeNode child = new RDTreeNode(key.ToString(), this);
            m_Children.Add(key, child);
            // 通知红点节点数量改变
            RedDotMgr.Inst.NotifyNodeNumChanged();
            return child;
        }
        
        
    }
}