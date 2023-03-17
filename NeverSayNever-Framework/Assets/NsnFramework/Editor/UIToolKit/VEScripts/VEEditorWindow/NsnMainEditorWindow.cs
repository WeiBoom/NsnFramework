using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
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
        }


        protected override void OnShow()
        {
            base.OnShow();

            InitWindow();
        }

        private void InitWindow()
        {
            AddWindowVEAssetToRoot();

            InitUIElement();

            // 准备所需的数据
            InitUIToolKitData();
            // 初始化获取UIElement
            // 初始化功能列表
            InitMenuList();

            AddStyles();
        }


        private void AddStyles()
        {
            var nodeStyleSheet = VEToolKit.LoadVEAssetStyleSheet(NEditorConst.NsnStyleSheet_Variables);
            m_Root.styleSheets.Add(nodeStyleSheet);
        }

        private void InitUIToolKitData()
        {
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

            TwoPaneSplitView twoPaneSplitView = new TwoPaneSplitView(0, 200, TwoPaneSplitViewOrientation.Horizontal);
            twoPaneSplitView.Add(m_Root.Q<VisualElement>("NsnMainMenuListPanel"));
            twoPaneSplitView.Add(m_Root.Q<VisualElement>("NsnMainWidgetPanel"));

            m_Root.Q<VisualElement>("NsnMainContainer").Add(twoPaneSplitView);
            twoPaneSplitView.StretchToParentSize();
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
                    Button button = VEToolKit.CreateButton(item);
                    foldout.Add(button);  
                }
                m_MenuScrollVew.Add(foldout);
            }
            m_MainScrollView.contentContainer.StretchToParentSize();
        }
    }
}
