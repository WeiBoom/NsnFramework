using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NeverSayNever;

public class NodeTreeRunner : MonoBehaviour
{
    public NeverSayNever.BehaviourTree.NodeGraphTree tree;

    void Start()
    {
        tree = tree.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        var state = tree.Update();
    
        if(state != NeverSayNever.BehaviourTree.TreeNode.State.Running)
        {
            Debug.Log(state);
        }
    }
}
