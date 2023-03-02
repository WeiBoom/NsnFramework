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
            var menuDataDic = new Dictionary<string, List<string>>();
            foreach (var config in VEConfig.MenuConfigList)
            {
                string[] path = config.MenuPath.Split('/');
                string menuGroup = path[0];
                menuDataDic.TryGetValue(menuGroup, out var menuList);
                if (menuList == null)
                {
                    menuList = new List<string>();
                    menuDataDic.Add(menuGroup, menuList);
                }
                string menuName = path[1];
                if (!menuList.Contains(menuName))
                {
                    menuList.Add(menuName);
                }
            }

            var keyList = menuDataDic.Keys.ToList();
            System.Func<VisualElement> makeItem = () =>
            {
                Foldout foldout = new Foldout();
                foldout.StretchToParentSize();
                return foldout;
            };
            System.Action<VisualElement, int> bindItem = (e, i) =>
            {
                string key = keyList[i];
                Foldout foldout = e as Foldout;
                foldout.text = keyList[i];
                var itemList = menuDataDic[key];
                foreach(var item in itemList)
                {
                    Button button= new Button() { text = item };
                    foldout.Add(button);
                }
            };
            m_MenuListView.itemsSource = keyList;
            m_MenuListView.bindItem = bindItem;
            m_MenuListView.makeItem = makeItem;
            //foreach (var item in m_MenuFoldoutDic.Values)
            //{
            //    m_MenuListView.Add(item);
            //}
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
