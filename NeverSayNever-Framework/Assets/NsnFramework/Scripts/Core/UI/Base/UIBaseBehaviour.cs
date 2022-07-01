using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using NeverSayNever.Utilitiy;

namespace NeverSayNever.Core.HUD
{
    [Serializable]
    public class UIComponentItem
    {
        public string key;
        public Component element;
        public UIComponentItem(string key, Component element)
        {
            this.key = key;
            this.element = element;
        }
    }

    [XLua.LuaCallCSharp]
    [SerializeField]
    public class UIBaseBehaviour : GameBehaviour
    {
        public enum EUIScriptType
        {
            CSharp,
            Lua
        }

        [EnumToggleButtons]
        public EUIScriptType uiScriptType;

        [FoldoutGroup("UI Elements Group", true)]
        [TableList(MaxScrollViewHeight = 400,MinScrollViewHeight = 100),Searchable]
        public List<UIComponentItem> fixedElements = new List<UIComponentItem>();

        [FoldoutGroup("UI Elements Group")]
        [TableList(MaxScrollViewHeight = 400,MinScrollViewHeight = 100),Searchable]
        public List<UIComponentItem> dynamicElements = new List<UIComponentItem>();

        [FoldoutGroup("UI Elements Group"),ShowIf("uiScriptType", EUIScriptType.CSharp)]
        [InlineButton("GenerateUIScriptForCSharp", "生成UI模块代码(C#)")]
        public bool isPanel = true;

        [FoldoutGroup("UI Elements Group"),ShowIf("uiScriptType", EUIScriptType.Lua)]
        [InlineButton("GenerateUIScriptForLua", "生成UI代码(Lua)")]
        public bool isLuaPanel = true;

        [InlineButton("SaveUIPrefab", "保存预制体")]
        
        /// <summary>
        /// 界面包含的UI元素的缓存,界面初始化时自动添加
        /// </summary>
        public readonly Dictionary<string, Component> Collection = new Dictionary<string, Component>();

        /// <summary>
        /// 拷贝List中收集的UI组件 到 Collection 中
        /// </summary>
        public void InitCollectedUIComponents()
        {
            Collection.Clear();
            AddComponentsToCollection(ref fixedElements);
            AddComponentsToCollection(ref dynamicElements);
        }

        public Component GetUICollection(string name)
        {
            Collection.TryGetValue(name, out var item);
            return item;
        }

        private void AddComponentsToCollection(ref List<UIComponentItem> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Collection.Add(list[i].key, list[i].element);
            }
            // 编辑器下可能需要在面板上继续观察按钮，所以这里不清除，不需要观察的环境下则直接清理掉
#if !UNITY_EDITOR
            list.Clear();
            list = null;
#endif
        }


#if UNITY_EDITOR

        [Button("获取UI组件", ButtonSizes.Small), GUIColor(0.4f, 0.8f, 1)]
        [HorizontalGroup("UI Elements Group/Horizontal", 0.7f)]
        [FoldoutGroup("UI Elements Group", true)]
        private void CollectDynamicUIComponents()
        {
            NeverSayNever.EditorUtilitiy.NSNPanelElementsCollector.CollectPanelUIElements(this);
        }

        [Button("清空UI组件"), GUIColor(253 / 255f, 74 / 255f, 73 / 255f)]
        [HorizontalGroup("UI Elements Group/Horizontal", 0.3f)]
        [FoldoutGroup("UI Elements Group", true)]
        private void ClearDynamicUIComponents()
        {
            dynamicElements.Clear();
        }

        private void GenerateUIScriptForCSharp()
        {
            if (UnityEditor.EditorUtility.DisplayDialog("Generate C# UI Module Scripts", $"确定创建/更新 {this.gameObject.name} 代码?", "ok", "cancel"))
            {
                SaveUIPrefab();
                NeverSayNever.EditorUtilitiy.UIScriptBuilderForCSharp.BuildCSharpScriptForPanel(this, isPanel);
            }
        }

        private void GenerateUIScriptForLua()
        {
            if (UnityEditor.EditorUtility.DisplayDialog("Generate Lua UI Module Scripts", $"确定创建/更新 {this.gameObject.name} 代码?", "ok", "cancel"))
            {
                SaveUIPrefab();
                NeverSayNever.EditorUtilitiy.UIScriptBuilderForLua.BuildLuaScriptForPanel(this, isPanel);
            }
        }

        [Button("保存UI预制体")]
        private void SaveUIPrefab()
        {
            if (isPanel == false)
            {
                ULog.Warning("只能创建类型为Panel的预制体");
                return;
            }

            FrameworkConfig.GlobalConfig.VariesAssetFolderDic.TryGetValue("UI", out var prefabDirectory);
            if(prefabDirectory == null || prefabDirectory.folder == null)
            {
                throw new Exception("无法找到UI保存路径，请检查配置");
            }

            var tarPath = UnityEditor.AssetDatabase.GetAssetPath(prefabDirectory.folder);
            var uiPrefabDirectory = $"{tarPath}/{gameObject.name}";
            if (!System.IO.Directory.Exists(uiPrefabDirectory))
            {
                Debug.Log($"创建目录 {uiPrefabDirectory}");
                System.IO.Directory.CreateDirectory(uiPrefabDirectory);
            }
            string genPrefabFullName = string.Concat(uiPrefabDirectory, "/", gameObject.name, ".prefab");
            UnityEditor.PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, genPrefabFullName, UnityEditor.InteractionMode.UserAction);
        }
#endif
    }
}