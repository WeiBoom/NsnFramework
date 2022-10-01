using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.NodeGraphView
{
    public abstract class BaseNode : ScriptableObject
    {
        public enum State
        {
            Default = 0,
            Running = 1,
            Success = 2,
            Failure = 3,
        }

        [HideInInspector]public string guid;
        [HideInInspector]public State state = State.Running;
        [HideInInspector]public Vector2 position;

        public bool IsStarted { get; private set; } = false;

        public State Update()
        {
            if (!IsStarted)
            {
                OnStart();
                IsStarted = true;
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

        public virtual BaseNode Clone()
        {
            return Instantiate(this);
        }
    }
}

