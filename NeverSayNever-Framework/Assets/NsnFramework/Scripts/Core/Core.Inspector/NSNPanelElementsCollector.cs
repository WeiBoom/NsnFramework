using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace NeverSayNever.EditorUtilitiy
{

    public static class NSNPanelElementsCollector
    {
        /// <summary>
        /// 检索并收集UI上的指定对象组件
        /// </summary>
        /// <param name="panel"></param>
        public static void CollectPanelUIElements(UIBaseBehaviour panel)
        {
#if UNITY_EDITOR
            if (panel == null) return;
            var collectionList = new List<Component>();

            CheckPanelUIElements(panel.transform, ref collectionList);

            panel.dynamicElements.Clear();

            foreach (var element in collectionList)
            {
                var item = new UIComponentItem(element.gameObject.name.ToLower(), element);
                panel.dynamicElements.Add(item);
            }

            UnityEditor.EditorUtility.SetDirty(panel);
#endif
        }

        /// <summary>
        /// 检查面板上的UI元素
        /// </summary>
        private static void CheckPanelUIElements(Transform root, ref List<Component> collectionList)
        {
            int childCount = root.childCount;
            for (var i = 0; i < childCount; i++)
            {
                Transform node = root.GetChild(i);
                if (node.childCount > 0)
                {
                    CheckPanelUIElements(node, ref collectionList);
                }

                System.Type compType = GetSpecialNodeComponentType(node);
                if(compType == null) continue;
                string compName = compType.Name;
                compName = compName.Replace("UnityEngine.UI", string.Empty);
                if (!string.IsNullOrEmpty(compName))
                {
                    Component component = node.GetComponent(compName);
                    if(component == null)
                    {
                        Debug.LogError($"GetComponent Error! type is {compType}");
                        continue;
                    }
                    collectionList.Add(component);
                }
            }
        }

        private static System.Type GetSpecialNodeComponentType(Transform node)
        {
            if (node == null)
                return null;

            var nodeName = node.gameObject.name.ToLower();
            System.Type type = null;
            
            if (nodeName.StartsWith("node_")) type = typeof(Transform);
            else if (nodeName.StartsWith("btn_")) type = typeof(Button);
            else if (nodeName.StartsWith("txt_")) type = typeof(Text);
            else if (nodeName.StartsWith("tmp_")) type = typeof(TextMeshProUGUI);
            else if (nodeName.StartsWith("img_")) type = typeof(Image);
            else if (nodeName.StartsWith("tex_")) type = typeof(RawImage);
            else if (nodeName.StartsWith("scroll_")) type = typeof(ScrollRect);
            else if (nodeName.StartsWith("grid_")) type = typeof(Grid);
            else if (nodeName.StartsWith("slider_")) type = typeof(Slider);

            return type;
        }
    }
}
