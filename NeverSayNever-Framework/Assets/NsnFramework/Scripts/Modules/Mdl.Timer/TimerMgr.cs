using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
// 计时器管理器
    public class TimerMdl : ITimerMdl
    {
        // 所有监听器列表
        private readonly List<NsnTimer> _allTimerListeners = new List<NsnTimer>(20);

        public void OnCreate(params object[] args)
        {
        }
        
        public void OnUpdate(float deltaTime)
        {
            for (var i = _allTimerListeners.Count - 1; i >= 0; i--)
            {
                var timer = _allTimerListeners[i];
                if (!timer.IsActive)
                {
                    _allTimerListeners.RemoveAt(i);
                    continue;
                }
                var active = timer.Update(deltaTime);
                if (active == false)
                    _allTimerListeners.RemoveAt(i);
            }
        }

        public void OnDispose()
        {
            _allTimerListeners.Clear();
        }

        /// <summary>
        /// 添加一个延时调用的计时器
        /// </summary>
        /// <param name="time">延时调用的时间</param>
        /// <param name="callback">回调事件</param>
        public void AddDelayTimer(float delayTime, OnTimeListenerCallback callback)
        {
            if (delayTime < 0 || callback == null) return;
            var timer = new NsnTimer(delayTime, callback);
            _allTimerListeners.Add(timer);
        }

        /// <summary>
        /// 添加一个Loop类型的计时器
        /// </summary>
        /// <param name="intervalTime">执行间隔的时间</param>
        /// <param name="loopCount">执行上限次数，小于等于0则表示无上限，由回调方法决定是否停止</param>
        /// <param name="callImmediate">添加的时候是否立即执行</param>
        /// <param name="callback">回调事件</param>
        public void AddLoopTimer(float intervalTime, int loopCount, bool callImmediate, OnTimeListenerCallback callback)
        {
            if (intervalTime < 0 || callback == null) return;
            var timer = new NsnTimer(intervalTime, loopCount, callback);
            _allTimerListeners.Add(timer);
            if (callImmediate)
                callback(loopCount);
        }


    }
}
