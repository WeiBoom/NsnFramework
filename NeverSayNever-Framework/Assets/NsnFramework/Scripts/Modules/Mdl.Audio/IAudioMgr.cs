using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeverSayNever
{
    public enum eSoundType
    {
        BGM,
        SFX,
        Interphone,
    }


    public interface IAudioMgr : IManager
    {
        float BGMVolume { get; }

        float SFXVol { get; }

        // ������������
        void UpdateVolume(eSoundType soundType,float fVolume);

        // ��ͣĳһ����Ч
        void PauseSound(eSoundType soundType,bool bPuased);

        // ����������Ƶ���¼�
        void TriggerEvent();

        // ��ͣBGN
        void PauseBGM();

        // �ָ�BGM
        void ResumeBGM();

        // ����BGM
        void FadeOutBGM();

        // ����BGM
        void FadeInBGM();

        // ����������С
        void ApplyVolume(eSoundType soundType);
    }
}

