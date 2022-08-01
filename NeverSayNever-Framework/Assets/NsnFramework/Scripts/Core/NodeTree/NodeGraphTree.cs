using UnityEngine;
using System.Collections.Generic;

namespace NeverSayNever.BehaviourTree
{
    [CreateAssetMenu(menuName ="创建NodeGraphTrees",fileName = "NodeGraphTree")]
	public class NodeGraphTree : ScriptableObject
	{
		public BaseNode rootNode;
		public BaseNode.State treeState = BaseNode.State.Running;

		public List<BaseNode> nodes = new List<BaseNode> ();

		public BaseNode.State Update()
		{
			if(rootNode.state == BaseNode.State.Running)
            {
				treeState = rootNode.Update();
			}

			return treeState;
		}

		public BaseNode CreateNode(System.Type type)
        {
#if UNITY_EDITOR
            BaseNode node =ScriptableObject.CreateInstance(type) as BaseNode;
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

		public void DeleteNode(BaseNode node)
        {
#if UNITY_EDITOR
            nodes.Remove(node);
            UnityEditor.AssetDatabase.RemoveObjectFromAsset(node);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

		public void AddChild(BaseNode parent,BaseNode child)
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

        public void RemoveChild(BaseNode parent, BaseNode child)
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

        public List<BaseNode> GetChildren(BaseNode parent)
        {
            List<BaseNode> children = new List<BaseNode>();

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

