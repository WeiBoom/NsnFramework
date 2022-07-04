using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.BehaviourTree
{
    public class SequenceNode : CompositeNode
    {
        int current = 0;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override State OnUpdate()
        {
            var child = children[current];
            switch (child.Update())
            {
                case State.Running:
                    return State.Running;
                case State.Failure:
                    return State.Failure;
                case State.Success:
                    current++;
                    break;
            }
            return current >= children.Count ? State.Success : State.Running;
        }

        protected override void OnStop()
        {
            current = 0;
        }
    }
}

