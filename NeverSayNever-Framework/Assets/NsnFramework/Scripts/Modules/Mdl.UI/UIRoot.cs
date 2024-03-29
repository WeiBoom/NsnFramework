using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Nsn
{
    public class UIRoot : UIBehaviour
    {
        private static bool m_Initialized = false;

        private Camera m_Camera;
        private Canvas m_Canvas;


        public Camera UICamera => m_Camera;
        public Canvas UICanvas => m_Canvas;

        // 默认16：9的设计分辨率
        public Vector2 UIDesignResolution = new Vector2(1366, 768);

        public static void Create()
        {
            if(m_Initialized)
                return;
            m_Initialized = true;
        }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this.gameObject);

            Initialize();
        }

        private void Initialize()
        {
            m_Camera = UIUtility.Search<Camera>(transform,"UICamera");
            m_Canvas = UIUtility.Search<Canvas>(transform,"UICanvas");
        }
    }
}

