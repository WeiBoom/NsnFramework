using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    [RequireComponent(typeof(UIObjectCollector))]
    public class UIBaseView : UIBehaviour
    {
        protected UIObjectCollector m_Collector;

        protected override void Awake()
        {
            m_Collector = GetComponent<UIObjectCollector>();
        }

        public UnityEngine.Object Get(UIObjectCollector.LinkedObjectType type)
        {
            return null;
        }
    }
}
