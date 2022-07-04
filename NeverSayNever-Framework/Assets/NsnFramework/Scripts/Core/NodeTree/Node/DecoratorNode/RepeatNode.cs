using System;

namespace NeverSayNever.BehaviourTree
{
    public class RepeatNode : DecoratorNode
    {


        protected override void OnStart()
        {

        }

        protected override State OnUpdate()
        {
            var childState = child.Update();
            return State.Running;
        }

        protected override void OnStop()
        {

        }

    }
}
