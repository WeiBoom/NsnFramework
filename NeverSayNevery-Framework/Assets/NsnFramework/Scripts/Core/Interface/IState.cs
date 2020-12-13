﻿namespace NeverSayNever.Core.FSM
{
    public interface IState
    {
        float Timer { get; }
        string Name { get; }
        string Tag { get; }

        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}