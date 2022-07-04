using UnityEngine;
using System.Collections.Generic;

namespace NeverSayNever.BehaviourTree
{
    [CreateAssetMenu(menuName ="创建NodeGraphTrees",fileName = "NodeGraphTree")]
	public class NodeGraphTree : ScriptableObject
	{
		public TreeNode rootNode;
		public TreeNode.State treeState = TreeNode.State.Running;

		public List<TreeNode> nodes = new List<TreeNode> ();

		public TreeNode.State Update()
		{
			if(rootNode.state == TreeNode.State.Running)
            {
				treeState = rootNode.Update();
			}

			return treeState;
		}

		public TreeNode CreateNode(System.Type type)
        {
#if UNITY_EDITOR
            TreeNode node =ScriptableObject.CreateInstance(type) as TreeNode;
			node.name = type.Name;
			node.guid = UnityEditor.GUID.Generate().ToString();
			nodes.Add(node);

            UnityEditor.AssetDatabase.AddObjectToAsset(node, this);
            UnityEditor.AssetDatabase.SaveAssets();

			return node;
#else 
            return null;
#endif
        }

		public void DeleteNode(TreeNode node)
        {
#if UNITY_EDITOR
            nodes.Remove(node);
            UnityEditor.AssetDatabase.RemoveObjectFromAsset(node);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

		public void AddChild(TreeNode parent,TreeNode child)
        {

            RootNode root = parent as RootNode;
            if (root)
            {
                root.child = child;
            }

            DecoratorNode decorator = parent as DecoratorNode;
			if(decorator)
            {
				decorator.child = child;
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
				composite.children.Add(child);
            }
        }

        public void RemoveChild(TreeNode parent, TreeNode child)
        {
            RootNode root = parent as RootNode;
            if (root)
            {
                root.child = null;
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = null;
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.children.Remove(child);
            }
        }

        public List<TreeNode> GetChildren(TreeNode parent)
        {
            List<TreeNode> children = new List<TreeNode>();

            RootNode root = parent as RootNode;
            if (root && root.child != null)
            {
                children.Add(root.child);
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                return composite.children;
            }

            return children;
        }


        public NodeGraphTree Clone()
        {
            NodeGraphTree tree = Instantiate(this);

            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }

    }
}

