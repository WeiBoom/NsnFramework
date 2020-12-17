using System.Collections;

namespace NeverSayNever.Core.AI
{
    public enum eFSMState
    {
        None,
        Enter,
        Update,
        Exit,
    }

    public class FSMState : IState
    {
        protected readonly float _timer;
        protected readonly string _name;
        protected readonly string _tag;
        public float Timer => _timer;
        public string Name => _name;
        public string Tag => _tag;

        public FSMState(float timer, string name, string tag)
        {
            _timer = timer;
            _name = name;
            _tag = tag;
        }

        public eFSMState State { get; private set; } = eFSMState.None;


        // 初始化行为，执行完成后需要设置状态为 Update
        public virtual void OnEnter()
        {
            State = eFSMState.Enter;
        }

        // 退出行为
        public virtual void OnExit()
        {
            State = eFSMState.Exit;
        }

        // 更新行为
        public virtual void OnUpdate()
        {
            State = eFSMState.Update;
        }

    }
}