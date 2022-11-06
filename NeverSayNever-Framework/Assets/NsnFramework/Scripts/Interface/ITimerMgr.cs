using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����Ļص��¼�
public delegate bool OnTimeListenerCallback(float time);

namespace NeverSayNever
{
    public interface ITimerMdl : IModule
    {
        void AddDelayTimer(float delayTime, OnTimeListenerCallback callback);

        void AddLoopTimer(float intervalTime, int loopCount, bool callImmediate, OnTimeListenerCallback callback);
    }
}


