using System;
using System.Collections.Generic;
using System.Linq;

namespace NeverSayNever
{
    public class FSMMgr : IFSMMgr
    {
        private Dictionary<int, FSM> _fsmDic;

        public FSMMgr()
        {
            _fsmDic = new Dictionary<int, FSM>();
        }


        public void Update(float deltaTime)
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
            _fsmDic?.Clear();
        }
    }
}