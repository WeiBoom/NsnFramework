using System;
using System.Collections.Generic;
using System.Linq;

namespace NeverSayNever
{
    public class FSMManager
    {
        private Dictionary<int, FSM> _fsmDic;

        public FSMManager()
        {
            _fsmDic = new Dictionary<int, FSM>();
        }


        public void UpdateFSM()
        {
            for (int i = 0; i < _fsmDic.Count; i++)
            {
                var element = _fsmDic.ElementAt(i);
                var fsm = element.Value;
                fsm?.Update();
            }
        }

        public FSM AddFSM(int id)
        {
            FSM fsm = new FSM();
            fsm.SetId(id);
            _fsmDic.Add(id, fsm);
            return fsm;
        }

        public void RemoveFSM(int id)
        {
            _fsmDic.TryGetValue(id, out var fsm);
            fsm?.Dispose();
            _fsmDic.Remove(id);
        }

        public void ClearFSM()
        {
            _fsmDic?.Clear();
        }
    }
}