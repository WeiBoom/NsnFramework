using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����Ļص��¼�
public delegate bool OnTimeListenerCallback(float time);

namespace NeverSayNever
{
    public interface ITimerMgr : IManager
    {
        void AddDelayTimer(float time, OnTimeListenerCallback callback);

        void AddLoolTimer(float time, bool callImmediate, OnTimeListenerCallback callback);
    }
}


