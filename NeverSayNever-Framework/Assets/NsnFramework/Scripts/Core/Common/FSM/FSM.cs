using System.Collections;
using System.Collections.Generic;
using System;


namespace NeverSayNever
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class FSM : IDisposable
    {
        // 当前状态机的id
        protected int Id;

        // 缓存当前状态机的所有行为状态
        protected Dictionary<int, FSMState> AllStateDic;

        // 当前的状态
        protected FSMState CurrentState;


        public FSM()
        {
            AllStateDic = new Dictionary<int, FSMState>();
        }
        
        // 设置状态机的Id
        public void SetId(int id)
        {
            this.Id = id;
        }

        // 更新当前状态的行为
        public void Update()
        {
            if (CurrentState != null && CurrentState.State == FSMState.ActionState.Update)
                CurrentState.OnUpdate();
        }

        // 切换新的状态
        public void SetTransition(int stateId)
        {
            AllStateDic.TryGetValue(stateId, out var state);
            if (state != null)
            {
                CurrentState?.OnExit();
                CurrentState = state;
                CurrentState?.OnEnter();
            }
            else
            {
                //ULog.Error("没找到Id为 " + stateId + " 的FSMState");
            }
        }

        // 添加状态到状态机
        public void AddTransition(int stateId, FSMState state)
        {
            if (!AllStateDic.ContainsKey(stateId))
                AllStateDic.Add(stateId, state);
        }

        public void Dispose()
        {
        }
    }
}

