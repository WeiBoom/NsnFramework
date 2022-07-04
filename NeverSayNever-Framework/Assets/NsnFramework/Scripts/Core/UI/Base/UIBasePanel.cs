using System;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever
{
    

    public class UIBasePanel : GameBehaviour
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
        /// 打开窗口界面
        /// </summary>
        /// <param name="widgetName"></param>
        protected void OpenWidget(string widgetName)
        {
        }

        #endregion
    }
}
