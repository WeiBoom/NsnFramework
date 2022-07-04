
namespace NeverSayNever.EditorUtilitiy
{
    public class WaitNode : ActionNode
    {
        public float duration = 1;
        private float startTime;
        protected override void OnStart()
        {
            startTime = UnityEngine.Time.deltaTime;
        }

        protected override State OnUpdate()
        {
            startTime += UnityEngine.Time.deltaTime;
            if (UnityEngine.Time.time - startTime > duration)
            {
                return State.Success;
            }
            return State.Running;
        }

        protected override void OnStop()
        {
        }
    }
}