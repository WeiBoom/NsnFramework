using System.Collections;
using System.Collections.Generic;
using System;


namespace Nsn
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class FSM : IFSM
    {
        // 当前状态机的id
        protected int Id;

        // 缓存当前状态机的所有行为状态
        protected Dictionary<int, IFSMState> AllStateDic;

        // 当前的状态
        protected IFSMState CurFSMState;


        public FSM()
        {
            AllStateDic = new Dictionary<int, IFSMState>();
        }
        
        // 设置状态机的Id
        public void SetId(int id)
        {
            this.Id = id;
        }

        // 更新当前状态的行为
        public void Update(float deltaTime)
        {
            if (CurFSMState == null
                || CurFSMState.Status == FSMRunStatus.Exit
                || CurFSMState.Status == FSMRunStatus.None) 
                return;
            CurFSMState.OnUpdate(deltaTime);
        }

        // 切换新的状态
        public void SetTransition(int stateId)
        {
            AllStateDic.TryGetValue(stateId, out var state);
            if (state != null)
            {
                CurFSMState?.OnExit();
                CurFSMState = state;
                CurFSMState?.OnEnter();
            }
            else
            {
                //ULog.Error("没找到Id为 " + stateId + " 的IFSMState");
            }
        }

        // 添加状态到状态机
        public void AddTransition(int stateId, IFSMState state)
        {
            if (!AllStateDic.ContainsKey(stateId))
                AllStateDic.Add(stateId, state);
        }

        public void Dispose()
        {
        }
    }
}

