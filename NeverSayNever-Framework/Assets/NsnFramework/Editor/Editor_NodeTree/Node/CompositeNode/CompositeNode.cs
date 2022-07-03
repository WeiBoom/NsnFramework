using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

namespace NeverSayNever.EditorUtilitiy
{
	public abstract class CompositeNode : TreeNode
	{
		[HideInInspector] public List<TreeNode> children = new List<TreeNode>();	
	}
}
