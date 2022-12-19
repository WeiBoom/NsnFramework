using System.Collections;

namespace Nsn
{
    public class FSMState : IFSMState
    {
        protected readonly float _timer;
        protected readonly string _name;
        protected readonly string _tag;
        protected FSMRunStatus currentStatus;


        public float Timer => _timer;
        public string Name => _name;
        public string Tag => _tag;
        public FSMRunStatus Status => currentStatus;


        public FSMState()
        {
            _timer = UnityEngine.Time.deltaTime;
            _name = GetType().Name;
            _tag = string.Empty;
            currentStatus = FSMRunStatus.None;
        }

        public FSMState(float timer, string name, string tag)
        {
            _timer = timer;
            _name = name;
            _tag = tag;
            currentStatus = FSMRunStatus.None;
        }

        public virtual void OnEnter()
        {
            currentStatus = FSMRunStatus.Enter;
        }

        public virtual void OnExit()
        {
            currentStatus = FSMRunStatus.Exit;
        }

        public virtual void OnUpdate(float deltaTime)
        {
            if(currentStatus == FSMRunStatus.Enter)
                currentStatus = FSMRunStatus.Update;
        }

    }
}