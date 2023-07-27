using System.Collections.Generic;
using System.Text;

namespace Nsn.Example
{
    public class RedDotMgr : SingletonSafe<RedDotMgr>
    {
        public const char SplitChar = '/';
        /// <summary>
        /// 红点树的根节点
        /// </summary>
        public RDTreeNode Root { get; private set; }

        /// <summary>
        /// 缓存的StringBuilder对象
        /// </summary>
        public StringBuilder StringBuilderCache;

        /// <summary>
        /// 节点数量改变的回调
        /// </summary>
        private System.Action m_NodeNumChangedCallback;
        /// <summary>
        /// 节点值改变的回调
        /// </summary>
        private System.Action<RDTreeNode,int> m_NodeValueChangedCallback;
        
        // 所有的红点集合
        private Dictionary<string, RDTreeNode> m_AllRDNodes;
        // 标记为脏的红点的集合
        private HashSet<RDTreeNode> m_DirtyNodes;
        // 临时的脏红点的集合
        private List<RDTreeNode> m_TempDirtyNodes;

        public RedDotMgr()
        {
            m_AllRDNodes = new Dictionary<string, RDTreeNode>();
            m_DirtyNodes = new HashSet<RDTreeNode>();
            m_TempDirtyNodes = new List<RDTreeNode>();
            StringBuilderCache = new StringBuilder();

            Root = new RDTreeNode("RDRoot");
        }

        public void Update()
        {
            if (m_DirtyNodes.Count == 0) return;
            
            // 先移动到临时的缓存列表中
            m_TempDirtyNodes.Clear();
            foreach (var node in m_DirtyNodes)
                m_TempDirtyNodes.Add(node);
            // 再清理当前节点
            m_DirtyNodes.Clear();
            // 处理所有的脏节点
            foreach (var node in m_TempDirtyNodes)
                node.ChangeValue();
        }

        public void MarkDirtyNode(RDTreeNode node)
        {
            if (node == null || node.Name == Root.Name)
                return;
            m_DirtyNodes.Add(node);
        }

        public void NotifyNodeNumChanged()
        {
            m_NodeNumChangedCallback?.Invoke();
        }

        public void NotifyNodeValueChanged(RDTreeNode target,int newValue)
        {
            m_NodeValueChangedCallback?.Invoke(target, newValue);
        }

        public RDTreeNode AddListener(string path, System.Action<int> callback)
        {
            if (callback == null) return null;
            var node = GetOrCreateTreeNode(path);
            node.AddListener(callback);
            return node;
        }

        public void RemoveListener(string path, System.Action<int> callback)
        {
            if (callback == null) return;
            var node = GetOrCreateTreeNode(path);
            node.RemoveListener(callback);
        }

        public void RemoveAllListener(string path)
        {
            var node = GetOrCreateTreeNode(path);
            node.RemoveAllListener();
        }

        public void ChangeValue(string path, int newValue)
        {
            var node = GetOrCreateTreeNode(path);
            node.ChangeValue(newValue);
        }

        public int GetValue(string path)
        {
            var node = GetOrCreateTreeNode(path);
            if (node == null)
                return 0;
            return node.Value;
        }
        
        public RDTreeNode GetOrCreateTreeNode(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                NsnLog.Error("[RedDotMgr] Path 不能为空!");
                return null;
            }

            // 获取到了，直接返回
            if (m_AllRDNodes.TryGetValue(path, out RDTreeNode node))
                return node;
            
            // 获取不到，根据path 创建相关联的父节点与目标节点
            RDTreeNode cur = Root;
            int length = path.Length;
            int startIndex = 0;
            for (int i = 0; i < length; i++)
            {
                // 遇到分割点，才认为是一个子节点
                if (path[i] == SplitChar)
                {
                    if (i == length - 1)
                    {
                        NsnLog.Error("[RedDotMgr] 路径不合法，不能以分隔符为结尾 : " + path);
                        return null;
                    }

                    int endIndex = i - 1;
                    if (endIndex < startIndex)
                    {
                        NsnLog.Error("[RedDotMgr] 路径不合法，不能存在连续的分隔符 或 以分隔符为开头 : " + path);
                        return null;
                    }
                    // 根据路径与字段,获取或创建子节点
                    RDTreeNode child = cur.GetOrAddChild(new RDString(path, startIndex, endIndex));
                    startIndex = i + 1;
                    cur = child; // 更新当前节点，继续查找子节点
                }
            }

            RDTreeNode target = cur.GetOrAddChild(new RDString(path, startIndex, length - 1));
            m_AllRDNodes.Add(path, target);

            return target;
        }

        public bool RemoveTreeNode(string path)
        {
            if (!m_AllRDNodes.ContainsKey(path))
                return false;
            var node = GetOrCreateTreeNode(path);
            m_AllRDNodes.Remove(path);
            return node.Parent.RemoveChild(new RDString(node.Name, 0, node.Name.Length - 1));
        }

        public void RemoveAllTreeNodes()
        {
            Root.RemoveAllChild();
            m_AllRDNodes.Clear();
        }
    }
}