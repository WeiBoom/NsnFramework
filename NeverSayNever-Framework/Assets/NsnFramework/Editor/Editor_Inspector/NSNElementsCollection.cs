using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

using NeverSayNever;

namespace NeverSayNever.EditorUtilitiy
{
    using NeverSayNever;

    public class NSNElementsCollection
    {
        private readonly AnimBool _fadeGroup;

        private readonly SerializedObject _serializedObject;
        private readonly ReorderableList _dynamicElementsList;
        private readonly ReorderableList _fixedElementsList;

        private bool _showFixedComponents;
        private bool _showDynamicComponents;

        private UIBaseBehaviour _uiTarget;

        public NSNElementsCollection(SerializedObject serializedObject)
        {
            _serializedObject = serializedObject;
            _fadeGroup = new AnimBool(true);
            
            var dynamicProp = _serializedObject.FindProperty("DynamicUIComponents");
            _dynamicElementsList = new ReorderableList(_serializedObject, dynamicProp, true, true, true, true);
            InitReorderableList(_dynamicElementsList);

            var fixedProp = _serializedObject.FindProperty("FixedUIComponents");
            _fixedElementsList = new ReorderableList(_serializedObject, fixedProp, true, true, true, true);
            InitReorderableList(_fixedElementsList);

        }

        private void InitReorderableList(ReorderableList list)
        {
            list.elementHeight = 25;
            list.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                DrawElementCallback(list, rect, index, isActive, isFocused);
            };
            list.drawHeaderCallback = (rect) =>
                EditorGUI.LabelField(rect, list.serializedProperty.displayName);
        }

        private void DrawElementCallback(ReorderableList list, Rect rect,int index,bool isActive,bool isFocused)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;

            var keyProperty = element.FindPropertyRelative("key");
            EditorGUI.TextField(
                new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight),
                keyProperty.stringValue,
                keyProperty.displayName
            );

            var elementProperty = element.FindPropertyRelative("element");
            EditorGUI.PropertyField(
                new Rect(rect.x + 100, rect.y, rect.width - 100, EditorGUIUtility.singleLineHeight),
                elementProperty,
                GUIContent.none
            );

            EditorGUI.PropertyField(rect, element);
        }

        // 注册动画监听
        public void AddFadeGroupListener(UnityAction action)
        {
            _fadeGroup.valueChanged.AddListener(action);
        }

        // 移除动画监听
        public void RemoveFaveGroupListener(UnityAction action)
        {
            _fadeGroup.valueChanged.RemoveListener(action);
        }

        // 绘制Inspector信息
        public void DrawUIElements(UIBaseBehaviour target)
        {
            _uiTarget = target;

            EditorGUILayout.BeginVertical(GUI.skin.box);

            var fixedComponentCount = _uiTarget.fixedElements.Count;
            var dynamicComponentCount = _uiTarget.dynamicElements.Count;

            _serializedObject.Update();

            _showFixedComponents = EditorGUILayout.Toggle($"固定UI组件 [{fixedComponentCount}]", _showFixedComponents);
            if (_showFixedComponents)
            {
                _fixedElementsList.DoLayoutList();
                if (GUILayout.Button("Clear All"))
                    ClearFixedElements();
            }
            _showDynamicComponents = EditorGUILayout.Toggle($"动态UI组件 [{dynamicComponentCount}]", _showDynamicComponents);
            if (_showDynamicComponents)
            {
                _dynamicElementsList.DoLayoutList();
                if (GUILayout.Button("Clear All"))
                    ClearDynamicElements();
            }
            EditorGUILayout.BeginHorizontal(GUI.skin.box);
            
            if (GUILayout.Button("Collect"))
                CollectDynamicElements();
            
            if (GUILayout.Button("Generate Panel Scripts"))
                GeneratePanelScripts();

            EditorGUILayout.EndHorizontal();
            
            _serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();

        }


        private void CollectDynamicElements()
        {
            NSNPanelElementsCollector.CollectPanelUIElements(_uiTarget);
        }

        private void ClearDynamicElements()
        {
            _uiTarget.dynamicElements.Clear();
        }

        private void GeneratePanelScripts()
        {
            if(EditorUtility.DisplayDialog("Generate C# UI Module Scripts",$"确定创建/更新 {_uiTarget.gameObject.name} 代码?","ok","cancel"))
                UIScriptBuilderForCSharp.BuildCSharpScriptForPanel(_uiTarget);
        }

        private void ClearFixedElements()
        {
            if(EditorUtility.DisplayDialog("谨慎操作","确定清楚当前预制体上的所有固定UI组件么?","动手","别！"))
                _uiTarget.fixedElements.Clear();
        }
    }
}
