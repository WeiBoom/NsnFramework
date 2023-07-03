using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Nsn
{
    [RequireComponent(typeof(RectTransform), typeof(CanvasGroup))]
    public class UIView : UIBehaviour
    {
        private CanvasGroup m_CanvasGroup;


        private object m_Lock = new object();
        private EventHandler onEnabled;
        private EventHandler onDisabled;

        public event  EventHandler OnEnabled
        {
            add { lock (m_Lock) { onEnabled += value; } }
            remove { lock (m_Lock) { onEnabled -= value; } }
        }
        
        public event  EventHandler OnDisabled
        {
            add { lock (m_Lock) { onDisabled += value; } }
            remove { lock (m_Lock) { onDisabled -= value; } }
        }
        
        public RectTransform RectTransform { get; }
        public CanvasGroup CanvasGroup { get; }

        protected override void Awake()
        {
            base.Awake();
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

        protected void InternalOnAwake()
        {
            
        }
        
        protected void InternalOnEnabled()
        {
            try
            {
                onEnabled?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
            }
        }

        protected void InternalOnDisabled()
        {
            try
            {
                onDisabled?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
            }
        }
        
        protected virtual void OnVisibilityChanged(){}
        

    }
}