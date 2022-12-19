using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn
{

    [RequireComponent(typeof(UIBaseView))]
    public class UIObjectLinker : UIBase
    {
        [SerializeField]
        protected Dictionary<string, UnityEngine.Component> mLinkedObjectDic;

        private void CollectControl()
        {

        }
    }
}
