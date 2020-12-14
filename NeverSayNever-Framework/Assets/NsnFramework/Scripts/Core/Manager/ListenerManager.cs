using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Core
{
    using NeverSayNever.Utilities;

    public class ListenerManager : Singleton<ListenerManager>
    {
        private readonly List<IListener> _listeners = new List<IListener>();

        /// <summary>
        /// 注册监听者
        /// </summary>
        /// <param name="listener"></param>
        public void RegisterListener(IListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
            else
                ULog.Error("已经添加了相同的监听者");
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            // 更新每个监听者的事件
            foreach (var listener in _listeners)
            {
                listener?.UpdateListener();
            }
        }

        public override void OnDispose()
        {
            _listeners?.Clear();
            base.OnDispose();
        }
    }
}