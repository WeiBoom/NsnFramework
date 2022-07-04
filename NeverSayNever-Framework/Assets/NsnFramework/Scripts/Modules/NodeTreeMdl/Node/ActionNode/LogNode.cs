using UnityEngine;
using UnityEditor;

namespace NeverSayNever.BehaviourTree
{
    public class LogNode : ActionNode
    {

        public string message;

        protected override void OnStart()
        {
            ULog.Print("[LogNode][OnStart] : " + message);
        }

        protected override State OnUpdate()
        {
            ULog.Print("[LogNode][OnUpdate] : " + message);
            return State.Success;
            //return base.Update();
        }

        protected override void OnStop()
        {
            ULog.Print("[LogNode][OnStop] : " + message);
        }
    }
}


