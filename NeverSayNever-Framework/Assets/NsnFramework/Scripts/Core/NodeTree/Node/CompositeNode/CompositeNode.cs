using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace NeverSayNever.BehaviourTree
{
	public abstract class CompositeNode : BaseNode
	{
		[HideInInspector] public List<BaseNode> children = new List<BaseNode>();

        public override BaseNode Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }

    }
}
