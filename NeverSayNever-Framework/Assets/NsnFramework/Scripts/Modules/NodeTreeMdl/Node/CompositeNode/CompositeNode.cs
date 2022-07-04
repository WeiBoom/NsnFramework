using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace NeverSayNever.BehaviourTree
{
	public abstract class CompositeNode : TreeNode
	{
		[HideInInspector] public List<TreeNode> children = new List<TreeNode>();

        public override TreeNode Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }

    }
}
