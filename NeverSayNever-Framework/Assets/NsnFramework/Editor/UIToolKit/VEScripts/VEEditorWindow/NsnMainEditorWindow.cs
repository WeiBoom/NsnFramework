using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnMainEditorWindow : VEBaseEditorWindow
    {
        private static NsnMainEditorWindow m_Window;
        [MenuItem("Nsn/ToolKit/工具库 &#C")]
        public static void ShowWindow()
        {
            if(m_Window != null)
            {
                try
                {
                    m_Window.Close();
                }
                catch
                {
                }
                m_Window = null;
            }

            m_Window = GetWindow<NsnMainEditorWindow>();
            m_Window.titleContent = new GUIContent("Nsn 工具库(Alt + Shift + C)");
        }

        private VEBaseEditorWidget m_CurEditorWidget;

        private VisualElement m_MenuListView;
        private ScrollView m_MainScrollView;

        protected override void OnCreateGUI()
        {
            base.OnCreateGUI();


            InitMenuList();

        }

        private void InitMenuList()
        {

        }
    }
}
