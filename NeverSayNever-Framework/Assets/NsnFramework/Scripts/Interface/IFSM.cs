using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    public interface IFSMMgr : IManager
    {
        IFSM Add(int id);

        void Remove(int id);

        void Clear();
    }

    public interface IFSM : IDisposable
    {
        void SetId(int id);

        void Update(float deltaTime);

        void SetTransition(int stateId);

        void AddTransition(int stateId, IFSMState state);
    }

    public interface IFSMState
    {
        public enum Status
        {
            None,
            Enter,
            Update,
            Exit,
        }

        public Status CurStatus { get; }

        float Timer { get; }
        string Name { get; }
        string Tag { get; }

        void OnEnter();
        void OnUpdate(float deltaTime);
        void OnExit();
    }
}
