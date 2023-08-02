using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    public class UIView : UIBehaviour
    {
        public UIViewAttribute ViewInfo { get; set; }

        protected UIObjectCollector m_Collector;
        
        protected override void Awake()
        {
            base.Awake();
            m_Collector = GetComponent<UIObjectCollector>();
        }

        public T Get<T>(string key) where T : Component
        {
            var comp = m_Collector.GetNodeComponent(key);
            return (T)comp;
        }
    }

}
