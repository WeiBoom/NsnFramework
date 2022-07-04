using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace NeverSayNever.BehaviourTree
{
    public abstract class DecoratorNode : TreeNode
    {
        [HideInInspector] public TreeNode child;

        public override TreeNode Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}
