using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Editors
{
    using Sirenix.OdinInspector.Editor;
    using System.Linq;
    using UnityEngine;
    using Sirenix.Utilities.Editor;
    using Sirenix.Serialization;
    using UnityEditor;
    using Sirenix.Utilities;

    public class NSNGlobalConfigWindow : OdinMenuEditorWindow
    {
        [MenuItem("NeverSayNever/MenuInspector")]
        private static void OpenWindow()
        {
            var window = GetWindow<NSNGlobalConfigWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: true)
            {
                { "Player Settings",                Resources.FindObjectsOfTypeAll<PlayerSettings>().FirstOrDefault()       },
            };

            tree.AddAllAssetsAtPath("NsnConfig","NsnFramework/Resources/Setting",typeof(ScriptableObject), true).AddThumbnailIcons();
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}