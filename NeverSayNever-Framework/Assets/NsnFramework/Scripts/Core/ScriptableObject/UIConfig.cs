using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace NeverSayNever
{
    public enum UILayer
    {
        Base,
        Screen,
        Dislog,
        MessageBox,
        Top,
    }

    /// <summary>
    /// UI≈‰÷√–≈œ¢
    /// </summary>
    public class UIConfigInfo
    {
        public UILayer layer;
        public string path;
        public int index;
        public System.Enum type;
    }

    public class UIConfig : SerializedScriptableObject
    {
        public Dictionary<string, UIConfigInfo> uiInfoConfigs;

        public Stack<UIConfigInfo> uiConfigStack;
    }
}

