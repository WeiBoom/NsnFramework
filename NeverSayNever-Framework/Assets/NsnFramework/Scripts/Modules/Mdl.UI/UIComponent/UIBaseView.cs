using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nsn
{
    public class UIBaseView : UIBase
    {
        protected UIObjectLinker mLinker;

        protected override void OnAwake()
        {
            base.OnAwake();
            mLinker = GetComponent<UIObjectLinker>();
        }

        public UnityEngine.Object Get(UIObjectLinker.LinkedObjectType type)
        {
            return null;
        }
    }
}
