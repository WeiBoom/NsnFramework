using System;
using System.Collections.Generic;
using System.Linq;

namespace Nsn
{
    public class FSMMgr : IFSMMgr
    {
        private Dictionary<int, FSM> _fsmDic;

        public void OnInitialized(params object[] args)
        {
            _fsmDic = new Dictionary<int, FSM>();
        }

        public void OnDisposed()
        {
            Clear();
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0; i < _fsmDic.Count; i++)
            {
                var element = _fsmDic.ElementAt(i);
                var fsm = element.Value;
                fsm?.Update(deltaTime);
            }
        }

        public IFSM Add(int id)
        {
            FSM fsm = new FSM();
            fsm.SetId(id);
            _fsmDic.Add(id, fsm);
            return fsm;
        }

        public void Remove(int id)
        {
            _fsmDic.TryGetValue(id, out var fsm);
            fsm?.Dispose();
            _fsmDic.Remove(id);
        }

        public void Clear()
        {
            foreach (var fsm in _fsmDic.Values)
            {
                fsm.Dispose();
            }
            _fsmDic?.Clear();
        }

    }
}