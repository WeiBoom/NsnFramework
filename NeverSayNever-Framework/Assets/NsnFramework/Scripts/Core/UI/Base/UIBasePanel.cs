using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Core.HUD
{
    using NeverSayNever.Core.Event;

    public class UIBasePanel : UGameBehaviour
    {
        #region Property && Component
        // 缓存面板的 Transform 组件
        protected Transform m_Trans;
        // 缓存面板的 RectTransform 组件
        protected RectTransform m_RectTrans;
        /// <summary>
        /// 当前UI面板的是否正在显示
        /// </summary>
        public bool IsShow { get; private set; }
        /// <summary>
        /// UI界面的基本信息
        /// </summary>
        public UIPanelInfo Info { get; private set; }

        #endregion

        // 缓存当前界面的监听事件，在界面关闭时移除
        private readonly Dictionary<Enum, Delegate> _eventListenerDic = new Dictionary<Enum, Delegate>();


        #region Monobehaviour Function

        protected override void OnAwake()
        {
            // 初始化组件， 属性
            m_Trans = this.transform;
            m_RectTrans = GetComponent<RectTransform>();

            Info = GetComponent<UIPanelInfo>();
            Info.InitCollectedUIComponents();

            // 初始化自动生成的属性
            OnInitAttribute();
        }

        protected override void OnShow()
        {
            IsShow = true;
            base.OnShow();
        }

        protected override void OnHide()
        {
            IsShow = false;
            base.OnHide();
        }

        protected override void OnDestroyMe()
        {
            RemoveListener();
            base.OnDestroyMe();
        }

        /// <summary> 
        /// 接受其他界面发送的消息 
        /// </summary>
        public virtual void ReceiveMessage(params object[] args)
        {
        }

        #endregion

        #region Custom Function

        /// <summary> Awake 调用 初始化属性</summary>
        protected virtual void OnInitAttribute() { }

        /// <summary>
        /// 添加事件监听
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="func"></param>
        protected void AddListener(Enum eventId, Delegate func)
        {
            if (!EventManager.HasEvent(eventId, func)) return;
            // UI事件的监听，应只关注与界面信息、状态的改变
            EventManager.AddEvent(eventId, func);
            // 添加事件的时候，同时添加到UI层的监听列表里，当UI关闭时全部移除
            _eventListenerDic.Add(eventId, func);
        }

        /// <summary>
        /// 移除事件监听
        /// </summary>
        protected void RemoveListener()
        {
            foreach (var eventId in _eventListenerDic.Keys)
            {
                if (!EventManager.HasEvent(eventId))
                    continue;
                foreach (var eventFunc in _eventListenerDic.Values)
                {
                    EventManager.RemoveEvent(eventId, eventFunc);
                }
            }
            _eventListenerDic.Clear();
        }


        /// <summary>
        /// 打开窗口界面
        /// </summary>
        /// <param name="widgetName"></param>
        protected void OpenWidget(string widgetName)
        {
        }

        #endregion
    }
}
