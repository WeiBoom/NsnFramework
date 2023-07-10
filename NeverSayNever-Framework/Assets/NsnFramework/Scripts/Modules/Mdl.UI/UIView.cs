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

        protected UIObjectLinker m_Linker;
        
        protected override void Awake()
        {
            base.Awake();
            m_Linker = GetComponent<UIObjectLinker>();
        }

        public T Get<T>(string key) where T : Component
        {
            var comp = m_Linker.GetNodeComponent(key);
            return (T)comp;
        }
    }

}
