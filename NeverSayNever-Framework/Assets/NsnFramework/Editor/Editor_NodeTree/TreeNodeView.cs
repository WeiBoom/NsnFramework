using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeverSayNever.EditorUtilitiy;

public class TreeNodeView : UnityEditor.Experimental.GraphView.Node
{
    public TreeNode node;

    public TreeNodeView(TreeNode node)
    {
        this.node = node;
        this.title = node.name;
    }
}
