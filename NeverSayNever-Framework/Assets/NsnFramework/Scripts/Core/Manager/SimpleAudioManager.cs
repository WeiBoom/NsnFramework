using System;
using System.Collections;
using UnityEngine;

namespace NeverSayNever
{
    public class SimpleAudioManager : Singleton<SimpleAudioManager>
    {
        // 背景音源
        private AudioSource _mBGMSource;
        // 音效源
        private AudioSource _mClipSource;
        // 缓存所有的音频
        private readonly Hashtable _sounds = new Hashtable();

        public override void OnInitialize(params object[] args)
        {
            base.OnInitialize(args);
            var audioSource = args[0] as AudioSource;
            _mBGMSource = audioSource;
        }


        // 添加音频到缓存中
        private void Add(string key, AudioClip value)
        {
            if(_sounds[key] != null || value == null)
                return;
            
            _sounds.Add(key,value);
        }

        // 获取音频资源
        private AudioClip Get(string key)
        {
            if (_sounds[key] == null)
                return null;
            return _sounds[key] as AudioClip;
        }

        // 加载音频资源
        private void LoadAudioClip(string path, Action<AudioClip> action)
        {
            var audio = Get(path);
            if (audio != null)
            {
                action(audio);
                return;
            }
            
            var name = System.IO.Path.GetFileNameWithoutExtension(path);
            ResourceManager.LoadAudio(name, (asset) =>
            {
                var clip = asset as AudioClip;
                if (clip == null)
                {
                    ULog.Error($"加载音频文件失败 : {name}");
                    return;
                }

                Add(name, clip);
                action(clip);
            });
        }

        private void PlayInternal(AudioClip clip, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(clip,position);
        }
        
        // 播放音频
        public void Play(string path)
        {
            LoadAudioClip(path, (clip) =>
            {
                PlayInternal(clip,Vector3.zero);
            });
        }

        // 播放背景音乐
        public void PlayBGM(string name)
        {
            if (_mBGMSource == null)
                return;
            if (_mBGMSource.clip != null)
            {
                if (name.IndexOf(_mBGMSource.clip.name, StringComparison.Ordinal) <= -1) return;
                StopBGM();
                return;
            }

            _mBGMSource.loop = true;
            LoadAudioClip(name, clip =>
            {
                _mBGMSource.clip = clip;
                _mBGMSource.Play();
            });
        }

        /// <summary>
        ///  停止播放BGM
        /// </summary>
        public void StopBGM()
        {
            _mBGMSource.Stop();
            _mBGMSource.clip = null;
            Utils.ReleaseMemory();
        }

        // 暂停BGM
        public void PauseBGM()
        {
            _mBGMSource.Pause();
        }

        // 继续播放暂停的BGM
        public void UnPauseBGM()
        {
            _mBGMSource.UnPause();
        }
    }
}