using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeverSayNever
{
    // 计时器类型
    internal enum ETimerType
    {
        // 延时
        Delay = 0,
        // 循环
        Loop = 1,
    }

    internal struct NsnTimer
    {
        // 计时器监听类型
        public ETimerType Type { get; private set; }

        // 计时器执行的总时间，Loop型每执行一次回调会清零重新记录
        public float DurationTime { get; private set; }

        // 延时调用时间
        public float DelayTime { get; private set; }

        // 调用间隔时间
        public float IntervalTime { get; private set; }

        // 计时器是否激活
        public bool IsActive { get; private set; }

        // 计时器回调执行次数
        public int ExecuteCount { get; private set; }

        // 计时器回调执行次数上限，-1表示无上限
        public int ExecuteLimit { get; private set; }

        // 计时器监听时的执行的回调
        public event OnTimeListenerCallback OnListenerCallback;

        /// <summary>
        /// 延时调用的计时器
        /// </summary>
        /// <param name="delayTime">延时调用的时间</param>
        /// <param name="callback">调用事件</param>
        public NsnTimer( float delayTime, OnTimeListenerCallback callback)
        {
            Type = ETimerType.Delay;
            DurationTime = 0;
            DelayTime = delayTime;
            IntervalTime = 0;
            ExecuteCount = 0;
            ExecuteLimit = 0;
            OnListenerCallback = callback;
            IsActive = true;
        }

        /// <summary>
        /// 循环调用的计时器
        /// </summary>
        /// <param name="intervalTime">每次调用的间隔时间</param>
        /// <param name="loopCount">循环次数</param>
        /// <param name="callback">调用事件</param>
        public NsnTimer(float intervalTime, int loopCount , OnTimeListenerCallback callback)
        {
            Type = ETimerType.Loop;
            DurationTime = 0;
            DelayTime = 0;
            IntervalTime = intervalTime;
            ExecuteCount = 0;
            ExecuteLimit = loopCount;
            OnListenerCallback = callback;
            IsActive = true;
        }


        private void UpdateDelayTimer()
        {
            if (DurationTime >= DelayTime)
            {
                OnListenerCallback?.Invoke(DurationTime);
                OnListenerCallback = null;
                IsActive = false;
            }
        }

        private void UpdateLoopTimer()
        {
            if (DurationTime >= IntervalTime)
            {
                DurationTime = 0;
                IsActive = OnListenerCallback(DurationTime);
                ExecuteCount += 1;
            }
            if (ExecuteLimit > 0 && ExecuteCount > ExecuteLimit)
            {
                IsActive = false;
            }
        }

        // 监听函数，循环调用
        public bool Update(float deltaTime)
        {
            if (!IsActive || OnListenerCallback == null) return false;

            DurationTime += deltaTime;
            switch (Type)
            {
                case ETimerType.Delay:
                    UpdateDelayTimer();
                    break;
                case ETimerType.Loop:
                    UpdateLoopTimer();
                    break;
                default:
                    return false;
            }
            return IsActive;
        }
    }
}
