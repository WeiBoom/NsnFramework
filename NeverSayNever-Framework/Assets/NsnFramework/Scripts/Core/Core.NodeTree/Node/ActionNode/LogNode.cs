using UnityEngine;
using UnityEditor;

namespace NeverSayNever.NodeGraphView
{
    public class LogNode : ActionNode
    {

        public string message;

        protected override void OnStart()
        {
            NsnLog.Print("[LogNode][OnStart] : " + message);
        }

        protected override State OnUpdate()
        {
            NsnLog.Print("[LogNode][OnUpdate] : " + message);
            return State.Success;
            //return base.Update();
        }

        protected override void OnStop()
        {
            NsnLog.Print("[LogNode][OnStop] : " + message);
        }
    }
}


