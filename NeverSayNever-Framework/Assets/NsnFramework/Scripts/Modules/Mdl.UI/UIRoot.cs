using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nsn
{
    public class UIRoot : UIBase
    {
        private static bool m_Initialized = false;

        private Camera m_Camera;
        private Canvas m_Canvas;


        public Camera UICamera => m_Camera;
        public Canvas UICanvas => m_Canvas;

        public static void Create()
        {
            if(m_Initialized)
                return;
            m_Initialized = true;

            // TODO
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            DontDestroyOnLoad(this.gameObject);

            Initialize();
        }

        private void Initialize()
        {
            m_Camera = UIUtility.Search<Camera>(transform,"UICamera");
            m_Canvas = UIUtility.Search<Canvas>(transform,"UICanvas");
        }


        private void ApplyFixScreen()
        {

        }
    }
}

