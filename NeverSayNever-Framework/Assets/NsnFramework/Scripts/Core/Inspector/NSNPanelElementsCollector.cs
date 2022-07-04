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
            var childCount = root.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var node = root.GetChild(i);
                var type = GetSpecialNodeComponentType(node);
                if (type != null)
                {
                    var component = node.GetComponent(type);
                    collectionList.Add(component);
                }

                if (node.childCount > 0)
                {
                    CheckPanelUIElements(node, ref collectionList);
                }
            }
        }



        private static UIElementType GetSpecialNodeType(Transform node)
        {
            if (node == null)
                return UIElementType.Node;

            var nodeName = node.gameObject.name.ToLower();
            // transform
            if (nodeName.StartsWith("node_"))
            {
                return UIElementType.Node;
            }
            // button
            if (nodeName.StartsWith("btn_"))
            {
                return UIElementType.Button;
            }
            // text
            if (nodeName.StartsWith("txt_"))
            {
                return UIElementType.Text;
            }
            // textMeshPro UGUI
            if (nodeName.StartsWith("tmp_"))
            {
                return UIElementType.TextMeshPro;
            }
            // sprite
            if (nodeName.StartsWith("img_"))
            {
                return UIElementType.Image;
            }
            // texture
            if (nodeName.StartsWith("tex_"))
            {
                return UIElementType.Texture;
            }
            // scroll view
            if (nodeName.StartsWith("scroll_"))
            {
                return UIElementType.Scroll;
            }
            // grid
            if (nodeName.StartsWith("grid_"))
            {
                return UIElementType.Grid;
            }
            return UIElementType.Node;
        }

        private static System.Type GetSpecialNodeComponentType(Transform node)
        {
            if (node == null)
                return null;

            var nodeName = node.gameObject.name.ToLower();
            // transform
            if (nodeName.StartsWith("node_"))
            {
                return typeof(Transform);
            }
            // button
            if (nodeName.StartsWith("btn_"))
            {
                return typeof(Button);
            }
            // text
            if (nodeName.StartsWith("txt_"))
            {
                return typeof(Text);
            }
            // textMeshPro UGUI
            if (nodeName.StartsWith("tmp_"))
            {
                return typeof(TextMeshProUGUI);
            }
            // sprite
            if (nodeName.StartsWith("img_"))
            {
                return typeof(Image);
            }
            // texture
            if (nodeName.StartsWith("tex_"))
            {
                return typeof(RawImage);
            }
            // scroll view
            if (nodeName.StartsWith("scroll_"))
            {
                return typeof(ScrollRect);
            }
            // grid
            if (nodeName.StartsWith("grid_"))
            {
                return typeof(Grid);
            }
            // slider
            if (nodeName.StartsWith("slider_"))
            {
                return typeof(Slider);
            }
            return null;
        }
    }
}
