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

        // 更新音量设置
        void UpdateVolume(eSoundType soundType,float fVolume);

        // 暂停某一类音效
        void PauseSound(eSoundType soundType,bool bPuased);

        // 触发播放音频的事件
        void TriggerEvent();

        // 暂停BGN
        void PauseBGM();

        // 恢复BGM
        void ResumeBGM();

        // 淡出BGM
        void FadeOutBGM();

        // 淡入BGM
        void FadeInBGM();

        // 设置音量大小
        void ApplyVolume(eSoundType soundType);
    }
}

