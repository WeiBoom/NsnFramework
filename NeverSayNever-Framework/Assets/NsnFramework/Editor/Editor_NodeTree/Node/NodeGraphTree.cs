using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace NeverSayNever.EditorUtilitiy
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
			TreeNode node =ScriptableObject.CreateInstance(type) as TreeNode;
			node.name = type.Name;
			node.guid = GUID.Generate().ToString();
			nodes.Add(node);

			AssetDatabase.AddObjectToAsset(node, this);
			AssetDatabase.SaveAssets();

			return node;
        }

		public void DeleteNode(TreeNode node)
        {
			nodes.Remove(node);
			AssetDatabase.RemoveObjectFromAsset(node);
			AssetDatabase.SaveAssets();
        }


		public void AddChild(TreeNode parent,TreeNode child)
        {
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
    }
}

