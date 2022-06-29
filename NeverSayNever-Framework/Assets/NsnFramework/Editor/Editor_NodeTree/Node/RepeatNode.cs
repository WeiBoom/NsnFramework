using System;

namespace NeverSayNever.EditorUtilitiy
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {

        }

        protected override State OnUpdate()
        {
            return base.Update();
        }

        protected override void OnStop()
        {

        }

    }
}
