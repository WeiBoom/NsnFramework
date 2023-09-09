using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn.MVC
{
    public class UIBaseComp : UIBehaviour
    {
        protected Canvas m_Canvas;
        protected CanvasScaler m_CanvasScaler;
        protected GraphicRaycaster m_GraphicRaycaster;
        
        protected RectTransform m_RectTrans;

        protected string m_UIName;
        protected object[] m_UIArgs;
        protected Dictionary<Action, string> m_EventDic;

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
        }
        
        protected T GetOrAddComponent<T>() where T : Component
        {
            T comp = gameObject.GetComponent<T>();
            if (comp == default(T))
                comp = gameObject.AddComponent<T>();
            return comp;
        }
    }
}