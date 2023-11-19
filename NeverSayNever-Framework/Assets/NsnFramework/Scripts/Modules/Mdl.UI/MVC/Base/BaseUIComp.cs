using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn.MVC
{
    public class BaseUIComp : UIBehaviour
    {
        protected Canvas m_Canvas;
        protected CanvasScaler m_CanvasScaler;
        protected GraphicRaycaster m_GraphicRaycaster;
        
        protected RectTransform m_RectTrans;

        protected string m_UIName;
        protected object[] m_UIArgs;
        
        protected Dictionary<int, EventDelegate> m_InternalEventDic;
        
        protected override void Awake()
        {
            base.Awake();
            m_RectTrans = transform.GetComponent<RectTransform>();
            m_UIName = gameObject.name;
        }

        protected override void OnEnable()
        {
        }

        protected override void OnDisable()
        {
        }

        protected override void OnDestroy()
        {
            m_UIArgs = null;
            foreach (var pair in m_InternalEventDic)
            {
                if(pair.Value != null)
                    Mgr.Event.RemoveEvent(pair.Key,pair.Value);
            }
        }
        
        protected T GetOrAddComponent<T>() where T : Component
        {
            T comp = gameObject.GetComponent<T>();
            if (comp == default(T))
                comp = gameObject.AddComponent<T>();
            return comp;
        }

        protected void AddEventListener(int eventID, EventDelegate callback, bool isOnce = false,
            bool isInsetHead = false)
        {
            if (callback == null) return;
            if (m_InternalEventDic[eventID] == null)
            {
                m_InternalEventDic[eventID] = callback;
                Mgr.Event.RegisterEvent(eventID,callback);
            }
        }

        protected void RemoveEventListener(int eventID, EventDelegate callback)
        {
            if (callback == null) return;
            if (m_InternalEventDic[eventID] != null)
            {
                m_InternalEventDic.Remove(eventID);
                Mgr.Event.RemoveEvent(eventID, callback);
            }
        }
        
        public void SetName(string newName, bool includeObj = true)
        {
            m_UIName = newName;
            if (includeObj)
                gameObject.name = newName;
        }

    }
}