using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace NeverSayNever.EditorUtilitiy
{
    public abstract class DecoratorNode : TreeNode
    {
        TreeNode child;
        //List<TreeNode> child;
    }
}
