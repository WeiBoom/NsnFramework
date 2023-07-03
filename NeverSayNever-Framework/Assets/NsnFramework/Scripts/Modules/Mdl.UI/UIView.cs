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

        protected override void Awake()
        {
            base.Awake();
        }
    }

}
