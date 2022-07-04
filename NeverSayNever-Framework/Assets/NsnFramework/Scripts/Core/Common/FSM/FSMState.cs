using System.Collections;

namespace NeverSayNever
{


    public class FSMState : IState
    {
        public enum ActionState
        {
            None,
            Enter,
            Update,
            Exit,
        }

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

        public ActionState State { get; private set; } = ActionState.None;


        // 初始化行为，执行完成后需要设置状态为 Update
        public virtual void OnEnter()
        {
            State = ActionState.Enter;
        }

        // 退出行为
        public virtual void OnExit()
        {
            State = ActionState.Exit;
        }

        // 更新行为
        public virtual void OnUpdate()
        {
            State = ActionState.Update;
        }

    }
}