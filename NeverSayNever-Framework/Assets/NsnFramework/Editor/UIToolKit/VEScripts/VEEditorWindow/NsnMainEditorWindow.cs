using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnMainEditorWindow : NsnBaseEditorWindow
    {
        private static NsnMainEditorWindow m_Window;
        [MenuItem("Nsn/ToolKit/工具库 &#C")]
        public static void ShowWindow()
        {
            Display(ref m_Window, "Nsn-工具库(Alt + Shift + C)");
        }

        private Dictionary<string, Foldout> m_MenuFoldoutDic;

        private NsnBaseEditorWidget m_CurEditorWidget;
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
            m_MenuFoldoutDic = new Dictionary<string, Foldout>();
            foreach (var config in VEConfig.MenuConfigList)
            {
                string[] path = config.MenuPath.Split('/');
                string menuGroup = path[0];
                Foldout foldout = GetFoldoutItem(menuGroup);
                Button button = new Button();
                button.text = path[1];
                foldout.Add(button);
            }

            foreach (var item in m_MenuFoldoutDic.Values)
            {
                m_MenuListView.Add(item);
            }
        }

        private Foldout GetFoldoutItem(string name)
        {
            Foldout foldout = null;
            m_MenuFoldoutDic.TryGetValue(name, out foldout);
            if (foldout == null)
            {
                foldout = new Foldout();
                m_MenuFoldoutDic.Add(name, foldout);
            }
            foldout.text = name;
            return foldout;
        }
    }
}
