using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace NeverSayNever.EditorUtilitiy
{

    public class RootNode : TreeNode
    {
        public TreeNode child;

        protected override void OnStart()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnStop()
        {
            throw new System.NotImplementedException();
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