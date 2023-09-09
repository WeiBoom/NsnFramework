using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn.MVC
{
    public class UIBaseView : UIBaseComp
    {
        protected UIBaseModel m_Model;
        protected UIBaseCtrl m_Ctrl;
        protected UIBaseLayer m_Holder;

        protected int m_BaseOrder = 0;
        [SerializeField]
        protected int m_RelativeOrder = 0;
        [SerializeField]
        protected AnimationClip OpenViewAnim;
        [SerializeField]
        protected AudioClip OpenViewSound;

        public int RelativeOrder => m_RelativeOrder;
        
        protected override void Awake()
        {
            base.Awake();
            OnCreate();
        }

        public void Register(UIBaseLayer holder, UIBaseModel model, UIBaseCtrl ctrl, object[] args)
        {
            m_Holder = holder;
            m_Model = model;
            m_Ctrl = ctrl;
            m_UIArgs = args;
            m_EventDic = new Dictionary<Action, string>();
        }

        private void OnCreate()
        {
            m_RectTrans.offsetMax = Vector2.zero;
            m_RectTrans.offsetMin = Vector2.zero;
            m_RectTrans.localScale = Vector3.one;
            m_RectTrans.anchoredPosition3D = Vector3.zero;

            SetOrder(m_RelativeOrder);
        }

        protected void SetOrder(int relativeOrder)
        {
            
        }
        
    }
}