using UnityEngine;
using UnityEditor;

namespace NeverSayNever.EditorUtilitiy
{
    public class LogNode : ActionNode
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


