using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Display(ref m_Window, "Nsn-ToolKit(Alt+Shift+C)");
        }

        private Dictionary<string, List<string>> m_MenuDataDic;

        private NsnBaseEditorWidget m_CurEditorWidget;
        private ScrollView m_MenuScrollVew;
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
            // 初始化编辑器配置信息
            VEConfig.LoadConfig();
            // 初始化左侧menuList的数据
            m_MenuDataDic = new Dictionary<string, List<string>>();
            foreach (var config in VEConfig.MenuConfigList)
            {
                string[] path = config.MenuPath.Split('/');
                string menuGroup = path[0];
                m_MenuDataDic.TryGetValue(menuGroup, out var menuList);
                if (menuList == null)
                {
                    menuList = new List<string>();
                    m_MenuDataDic.Add(menuGroup, menuList);
                }
                string menuName = path[1];
                if (!menuList.Contains(menuName))
                {
                    menuList.Add(menuName);
                }
            }
        }

        private void InitUIElement()
        {
            m_MenuScrollVew = m_Root.Q<ScrollView>("MenuScrollView");
            m_MainScrollView = m_Root.Q<ScrollView>("MainScrollView");
        }

        private void InitMenuList()
        {
            var keyList = m_MenuDataDic.Keys.ToList();
            foreach (var key in keyList)
            {
                Foldout foldout = new Foldout() { text = key};
                var itemList = m_MenuDataDic[key];
                foreach(var item in itemList)   
                {
                    Button button = new Button() { text = item };
                    foldout.Add(button);  
                }
                m_MenuScrollVew.Add(foldout);
            }
            m_MainScrollView.contentContainer.StretchToParentSize();
        }
    }
}
