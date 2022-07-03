using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace NeverSayNever.EditorUtilitiy
{
    public abstract class DecoratorNode : TreeNode
    {
        [HideInInspector] public TreeNode child;
    }
}
