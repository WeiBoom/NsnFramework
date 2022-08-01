using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    public class AudioMgr : IAudioMgr
    {
        public float BGMVolume => throw new System.NotImplementedException();

        public float SFXVol => throw new System.NotImplementedException();

        public void OnInitialize(params object[] args)
        {
        }

        public void OnUpdate(float deltaTime)
        {
        }

        public void OnDispose()
        {
        }

        public void UpdateVolume(eSoundType soundType, float fVolume)
        {
            throw new System.NotImplementedException();
        }

        public void PauseSound(eSoundType soundType, bool bPuased)
        {
            throw new System.NotImplementedException();
        }

        public void TriggerEvent()
        {
            throw new System.NotImplementedException();
        }

        public void PauseBGM()
        {
            throw new System.NotImplementedException();
        }

        public void ResumeBGM()
        {
            throw new System.NotImplementedException();
        }

        public void FadeOutBGM()
        {
            throw new System.NotImplementedException();
        }

        public void FadeInBGM()
        {
            throw new System.NotImplementedException();
        }

        public void ApplyVolume(eSoundType soundType)
        {
            throw new System.NotImplementedException();
        }
    }
}

