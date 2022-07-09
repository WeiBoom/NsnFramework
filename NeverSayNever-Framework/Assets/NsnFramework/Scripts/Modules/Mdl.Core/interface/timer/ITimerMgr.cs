using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �����Ļص��¼�
public delegate bool OnTimeListenerCallback(float time);

public interface ITimerMgr
{
    void OnUpdate(float deltaTime);

    void OnDispose();

    void AddDelayTimer(float time, OnTimeListenerCallback callback);

    void AddLoolTimer(float time, bool callImmediate, OnTimeListenerCallback callback);
}
