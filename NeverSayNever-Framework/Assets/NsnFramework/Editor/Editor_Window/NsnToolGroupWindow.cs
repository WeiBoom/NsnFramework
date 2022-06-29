using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using Sirenix.OdinInspector.Editor;
    
    public class NsnToolMenuEditorWindow : OdinMenuEditorWindow
    {

        //[MenuItem("Tools/Open NsnToolMenuWindow")]
        public static void OpenWindow()
        {
            var window = GetWindow<NsnToolMenuEditorWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        private NsnMenuBuildWindow mLaunchWindow = new NsnMenuBuildWindow();
        
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true)
            {
                {"NsnFramework"  , mLaunchWindow},
                
            };
            tree.AddAllAssetsAtPath("NsnFramework/NsnConfig","NsnFramework/Resources/Setting",typeof(ScriptableObject), true).AddThumbnailIcons();
            tree.Config.DrawSearchToolbar = true;

            return tree;
        }
    }

}