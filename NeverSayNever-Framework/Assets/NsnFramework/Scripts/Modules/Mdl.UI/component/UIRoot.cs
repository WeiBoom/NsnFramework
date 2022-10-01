using System;
using UnityEngine;
using UnityEngine.UI;

namespace NeverSayNever
{ 
    public class UIRoot 
    {
        public GameObject Go { get; private set; }

        public bool IsInitialized { get; }

        public bool IsPrepare => Go != null;

        public Canvas UICanvas;

        public Camera UICamera { get; protected set;  }
    }
}
