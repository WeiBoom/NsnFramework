using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace NeverSayNever.NodeGraphView
{
    public abstract class DecoratorNode : BaseNode
    {
        [HideInInspector] public BaseNode child;

        public override BaseNode Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}
