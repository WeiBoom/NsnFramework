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
        private Dictionary<string, RDTreeNode> m_AllRDNodes = new Dictionary<string, RDTreeNode>();
        // 标记为脏的红点的集合
        private HashSet<RDTreeNode> m_DirtyNodes = new HashSet<RDTreeNode>();
        // 临时的脏红点的集合
        private List<RDTreeNode> m_TempDirtyNodes = new List<RDTreeNode>();

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
            
        }
        
        public void MarkDirtyNode(RDTreeNode node)
        {
            
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

            return null;
        }
    }
}