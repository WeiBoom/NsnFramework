using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 监听的回调事件
public delegate bool OnTimeListenerCallback(float time);

namespace NeverSayNever
{
    public interface ITimerMdl : IModule
    {
        void AddDelayTimer(float delayTime, OnTimeListenerCallback callback);

        void AddLoopTimer(float intervalTime, int loopCount, bool callImmediate, OnTimeListenerCallback callback);
    }
}


