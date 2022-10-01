using System.Collections;

namespace NeverSayNever
{
    using Status = IFSMState.Status;

    public class FSMState : IFSMState
    {
        protected readonly float _timer;
        protected readonly string _name;
        protected readonly string _tag;
        protected Status currentStatus;

        public float Timer => _timer;
        public string Name => _name;
        public string Tag => _tag;
        public Status CurStatus => currentStatus;


        public FSMState()
        {
            _timer = UnityEngine.Time.deltaTime;
            _name = GetType().Name;
            _tag = string.Empty;
            currentStatus = Status.None;
        }

        public FSMState(float timer, string name, string tag)
        {
            _timer = timer;
            _name = name;
            _tag = tag;
            currentStatus = Status.None;
        }

        public virtual void OnEnter()
        {
            currentStatus = Status.Enter;
        }

        public virtual void OnExit()
        {
            currentStatus = Status.Exit;
        }

        public virtual void OnUpdate(float deltaTime)
        {
            if(currentStatus == Status.Enter)
                currentStatus = Status.Update;
        }

    }
}