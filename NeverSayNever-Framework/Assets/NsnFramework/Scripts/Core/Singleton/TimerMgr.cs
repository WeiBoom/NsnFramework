using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
// 计时器管理器
    public class TimerMgr : Singleton<TimerMgr>
    {

        #region 计时器

        // 计时器类型
        private enum ETimerType
        {
            // 延时
            Delay = 0,

            // 循环
            Loop = 1,

            // 自定义
            Diy = 2,
        }

        // 监听的回调事件
        public delegate bool OnTimeListenerCallback(float time);
                
        // 计时监听器
        private class Timer
        {
            // 计时器监听类型
            private readonly ETimerType _timerType;

            // 计时器，记录当前监听的时间
            private float _time;

            // 延时调用时间
            private readonly float _delayTime;

            // 调用间隔时间
            private readonly float _interval;

            // 计时器是否激活
            private bool _isActive;

            // 计时器监听时的执行的回调
            private OnTimeListenerCallback _onListenerCallback;

            // 构造函数 
            public Timer(ETimerType type, float time, OnTimeListenerCallback callback)
            {
                _timerType = type;
                _time = 0;
                _delayTime = type == ETimerType.Delay ? time : 0;
                _interval = type == ETimerType.Loop ? time : 0;
                _onListenerCallback = callback;
                _isActive = true;
            }

            // 监听函数，循环调用
            public bool Update()
            {
                if (!_isActive || _onListenerCallback == null) return false;
                _time += Time.deltaTime;
                switch (_timerType)
                {
                    case ETimerType.Delay:
                        if (_time >= _delayTime)
                        {
                            _onListenerCallback?.Invoke(_time);
                            _onListenerCallback = null;
                            _isActive = false;
                        }

                        break;
                    case ETimerType.Loop:
                        if (_time >= _interval)
                        {
                            _time = 0;
                            _isActive = _onListenerCallback(_time);
                        }

                        break;
                    case ETimerType.Diy:
                        _isActive = _onListenerCallback(_time);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return _isActive;
            }

        }
        
        #endregion

        // 所有监听器列表
        private readonly List<Timer> _allTimerListeners = new List<Timer>(20);

        public override void OnUpdate()
        {
            base.OnUpdate();
            for (var i = _allTimerListeners.Count - 1; i >= 0; i++)
            {
                var timer = _allTimerListeners[i];
                if (timer == null)
                {
                    _allTimerListeners.RemoveAt(i);
                    continue;
                }

                var active = timer.Update();
                if (active == false)
                    _allTimerListeners.RemoveAt(i);
            }
        }

        public override void OnDispose()
        {
            _allTimerListeners.Clear();
            base.OnDispose();
        }

        // 添加一个延时调用的监听器
        public void Delay(float time, OnTimeListenerCallback callback)
        {
            if (time < 0 || callback == null) return;
            var timer = new Timer(ETimerType.Delay, time, callback);
            _allTimerListeners.Add(timer);
        }

        // 添加一个循环调用的监听器
        public void Loop(float time, bool callImmediate, OnTimeListenerCallback callback)
        {
            if (time < 0 || callback == null) return;
            var timer = new Timer(ETimerType.Loop, time, callback);
            _allTimerListeners.Add(timer);
            
            if (callImmediate)
                callback(time);
        }

        // 添加一个自定义的监听器
        public void Diy(float time, bool callImmediate,OnTimeListenerCallback callback)
        {
            if (time < 0 || callback == null) return;
            var timer = new Timer(ETimerType.Diy, time, callback);
            _allTimerListeners.Add(timer);
            
            if (callImmediate)
                callback(time);
        }
    }
}
