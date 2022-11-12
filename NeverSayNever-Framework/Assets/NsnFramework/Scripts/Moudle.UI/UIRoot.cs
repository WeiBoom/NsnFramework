using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nsn
{
    public class UIRoot : UIBehaviour
    {
        public Camera UICamera;
        public Canvas UICancas;

        protected override void OnAwake()
        {
            base.OnAwake();
            DontDestroyOnLoad(this.gameObject);
        }

        public static void Create()
        {
        }
    }
}

