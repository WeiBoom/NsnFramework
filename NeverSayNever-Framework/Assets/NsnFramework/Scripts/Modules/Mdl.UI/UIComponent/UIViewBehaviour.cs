using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    [RequireComponent(typeof(UINodeCollector))]
    public class UIViewBehaviour : UIBehaviour
    {
        protected UINodeCollector m_Collector;

        protected override void Awake()
        {
            m_Collector = GetComponent<UINodeCollector>();
        }

        public UnityEngine.Object Get(UINodeCollector.LinkedObjectType type)
        {
            return null;
        }
    }
}
