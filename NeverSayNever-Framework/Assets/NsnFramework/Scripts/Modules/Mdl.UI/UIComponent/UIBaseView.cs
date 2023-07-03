using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    public class UIBaseView : UIBehaviour
    {
        protected UIObjectLinker mLinker;

        protected override void Awake()
        {
            mLinker = GetComponent<UIObjectLinker>();
        }

        public UnityEngine.Object Get(UIObjectLinker.LinkedObjectType type)
        {
            return null;
        }
    }
}
