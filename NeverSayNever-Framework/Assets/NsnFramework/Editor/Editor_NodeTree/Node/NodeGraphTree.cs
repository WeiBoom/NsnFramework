using UnityEngine;
using UnityEditor;

namespace NeverSayNever.EditorUtilitiy
{
	public class NodeGraphTree : ScriptableObject
	{
		public TreeNode rootNode;

		public TreeNode.State treeState = TreeNode.State.Running;

		public TreeNode.State Update()
		{

			if(rootNode.state == TreeNode.State.Running)
            {
				treeState = rootNode.Update();
			}

			return treeState;
		}
	}
}

