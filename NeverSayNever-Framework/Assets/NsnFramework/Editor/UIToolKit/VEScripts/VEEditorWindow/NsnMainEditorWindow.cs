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

        private ListView m_MenuListView;
        private ScrollView m_MainScrollView;

        protected override void OnCreateGUI()
        {
            base.OnCreateGUI();
            // 准备所需的数据
            InitUIToolKitData();
            // 初始化获取UIElement
            InitUIElement();
            // 初始化功能列表
            InitMenuList();

        }

        private void InitUIToolKitData()
        {
            VEConfig.LoadConfig();
        }


        private void InitUIElement()
        {
            m_MenuListView = m_Root.Q<ListView>("MenuListView");
            m_MainScrollView = m_Root.Q<ScrollView>("MainScrollView");
        }

        private void InitMenuList()
        {
            foreach(var config in VEConfig.MenuConfigList)
            {
                string[] path = config.MenuPath.Split('/');
                var foldout = new Foldout();
                foldout.text = path[0];
                Button button = new Button();
                button.text = path[1];
                foldout.Add(button);

                m_MenuListView.Add(foldout);
            }
        }

        private void AddMenuToFoldout()
        {

        }
    }
}
