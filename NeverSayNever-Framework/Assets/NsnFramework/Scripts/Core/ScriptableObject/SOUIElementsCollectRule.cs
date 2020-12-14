using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace NeverSayNever.Core.Asset
{
    public class SOUIElementsCollectRule : SerializedScriptableObject
    {
        
        public enum EKeyPos
        {
            Front,
            Last,
        }
        
        [InlineProperty]
        public struct CollectionDefine
        {
            public string name;
            public System.Type type;

            public CollectionDefine(string name, Type componentType)
            {
                this.name = componentType.Name;
                type = componentType;
            }
        }
        
        [DictionaryDrawerSettings(KeyLabel = "Element Key",ValueLabel = "Target Type",DisplayMode = DictionaryDisplayOptions.Foldout)]
        public Dictionary<string, CollectionDefine> UIElementsRuleDic = new Dictionary<string, CollectionDefine>()
        {
            {"node",new CollectionDefine("Transform",typeof(Transform))},
            {"btn",new CollectionDefine("Button",typeof(Button))},
            {"txt",new CollectionDefine("Text",typeof(Text))},
            {"tmp",new CollectionDefine("TextMeshProUGUI",typeof(TextMeshProUGUI))},
            {"img",new CollectionDefine("Image",typeof(Image))},
            {"tex",new CollectionDefine("RawImage",typeof(RawImage))},
            {"scroll",new CollectionDefine("ScrollRect",typeof(ScrollRect))},
            {"grid",new CollectionDefine("Grid",typeof(Grid))},
        };

    }

}