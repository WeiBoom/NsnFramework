using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    public abstract class TreeNode : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success,
        }

        public string guid;
        public State state = State.Running;
        public Vector2 position;
        public bool IsStarted { get; private set; } = false;

        public State Update()
        {
            if (!IsStarted)
            {
                OnStart();
                IsStarted = false;
            }

            state = OnUpdate();

            if (state != State.Running)
            {
                OnStop();
                IsStarted = false;
            }

            return state;
        }

        protected abstract void OnStart();
        protected abstract State OnUpdate();
        protected abstract void OnStop();

        //protected abstract void InstantiatePort();
    }
}

