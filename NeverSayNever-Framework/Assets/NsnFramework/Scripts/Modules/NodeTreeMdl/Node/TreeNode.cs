using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.BehaviourTree
{
    public abstract class TreeNode : ScriptableObject
    {
        public enum State
        {
            Running,
            Failure,
            Success,
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

        public virtual TreeNode Clone()
        {
            return Instantiate(this);
        }
    }
}

