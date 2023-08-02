using System;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nsn.MVVM
{
    public enum UIViewType
    {
        FULL,
        POPUP,
        DIALOG,
    }
    
    public enum UIViewState
    {
        NONE,
        ANIMATION_ENTER_START,
        ANIMATION_ENTER_END,
        VISIBLE,
        INVISIBLE,
        ANIMATION_EXIT_START,
        ANIMATION_EXIT_END,
    }

    public class UIViewStateEventArgs
    {
        private readonly UIViewState m_OldState;
        private readonly UIViewState m_State;
        public IUIView m_UIView;

        public UIViewStateEventArgs(IUIView uiView, UIViewState oldState, UIViewState newState)
        {
            m_UIView = uiView;
            m_OldState = oldState;
            m_State = newState;
        }

        public UIViewState OldState => this.m_OldState;
        public UIViewState State => this.m_State;
        public IUIView Window => this.m_UIView;
    }
    
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class UIView : UIBehaviour , IUIView
    {
        private RectTransform m_RectTransform;
        private CanvasGroup m_CanvasGroup;
        
        private IAnimation m_EnterAnimation;
        private IAnimation m_ExitAnimation;
        
        private object m_Lock = new object();

        private EventHandler m_OnEnabledHandler;
        private EventHandler m_OnDisabledHandler;

        #region Interface Implement
        
        public GameObject GameObj => this.IsDestroyed() ? null : this.gameObject;
        
        public Transform Transform => !this.IsDestroyed() && this.transform != null ? this.transform.parent : null;
        
        public Transform Parent => this.IsDestroyed() ? null : this.transform;

        public string Name
        {
            get => !IsDestroyed() && this.gameObject != null ? this.gameObject.name : null;
            set { if (!this.IsDestroyed() && this.gameObject != null) this.gameObject.name = value; }
        }
        
        public RectTransform RectTransform
        {
            get
            {
                if (IsDestroyed()) return null;
                return m_RectTransform ? m_RectTransform : (m_RectTransform = this.GetComponent<RectTransform>());
            }
        }

        public CanvasGroup CanvasGroup
        {
            get
            {
                if (IsDestroyed()) return null;
                return m_CanvasGroup ? m_CanvasGroup : (m_CanvasGroup = this.GetComponent<CanvasGroup>());
            }
        }

        public virtual bool Interactable
        {
            get
            {
                if (this.IsDestroyed() || this.gameObject == null) return false;
                return this.CanvasGroup.interactable;
            }
            set
            {
                if (this.IsDestroyed() || this.gameObject == null) return;
                this.CanvasGroup.interactable = value;
            }
        }
        
        public virtual float Alpha
        {
            get => !this.IsDestroyed() && this.gameObject != null ? this.CanvasGroup.alpha : 0f;
            set { if (!this.IsDestroyed() && this.gameObject != null) this.CanvasGroup.alpha = value; }
        }

        public virtual IAnimation EnterAnimation
        {
            get => this.m_EnterAnimation;
            set => this.m_EnterAnimation = value;
        }
        
        public virtual IAnimation ExitAnimation
        {
            get => this.m_ExitAnimation;
            set => this.m_ExitAnimation = value;
        }
        
        #endregion
        
        // 是否可见
        public virtual bool Visibility
        {
            get => !this.IsDestroyed() && this.gameObject != null ? this.gameObject.activeSelf : false;
            set
            {
                if (this.IsDestroyed() || this.gameObject == null) return;
                if (this.gameObject.activeSelf == value) return;
                this.gameObject.SetActive(value);
            }
        }
        
        public event  EventHandler OnEnabledAction
        {
            add { lock (m_Lock) { m_OnEnabledHandler += value; } }
            remove { lock (m_Lock) { m_OnEnabledHandler -= value; } }
        }
        
        public event  EventHandler OnDisabledAction
        {
            add { lock (m_Lock) { m_OnDisabledHandler += value; } }
            remove { lock (m_Lock) { m_OnDisabledHandler -= value; } }
        }

        protected override void OnEnable()
        {
            OnVisibilityChanged();
            base.OnEnable();
            InternalOnEnabled();
        }

        protected override void OnDisable()
        {
            OnVisibilityChanged();
            base.OnDisable();
            InternalOnDisabled();
        }

        protected void InternalOnEnabled()
        {
            try
            {
                m_OnEnabledHandler?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                NsnLog.Error(e.ToString());
            }
        }

        protected void InternalOnDisabled()
        {
            try
            {
                m_OnDisabledHandler?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                NsnLog.Error(e.ToString());
            }
        }
        
        // 当显隐状态改变时触发
        protected virtual void OnVisibilityChanged(){}

    }
}