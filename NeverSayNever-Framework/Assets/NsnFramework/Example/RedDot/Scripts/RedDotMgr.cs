using System.Collections.Generic;
using System.Text;

namespace Nsn.Example
{
    public class RedDotMgr : SingletonSafe<RedDotMgr>
    {
        public const char SplitChar = '/';
        /// <summary>
        /// ������ĸ��ڵ�
        /// </summary>
        public RDTreeNode Root { get; private set; }

        /// <summary>
        /// �����StringBuilder����
        /// </summary>
        public StringBuilder StringBuilderCache;

        /// <summary>
        /// �ڵ������ı�Ļص�
        /// </summary>
        private System.Action m_NodeNumChangedCallback;
        /// <summary>
        /// �ڵ�ֵ�ı�Ļص�
        /// </summary>
        private System.Action<RDTreeNode,int> m_NodeValueChangedCallback;
        
        // ���еĺ�㼯��
        private Dictionary<string, RDTreeNode> m_AllRDNodes = new Dictionary<string, RDTreeNode>();
        // ���Ϊ��ĺ��ļ���
        private HashSet<RDTreeNode> m_DirtyNodes = new HashSet<RDTreeNode>();
        // ��ʱ������ļ���
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