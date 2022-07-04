using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NeverSayNever.BehaviourTree
{
    public class RootNode : TreeNode
    {
        public TreeNode child;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            return child.Update();
        }

        public override TreeNode Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }
    }
}