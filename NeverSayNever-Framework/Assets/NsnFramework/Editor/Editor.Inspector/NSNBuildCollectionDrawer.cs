using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;

namespace NeverSayNever.EditorUtilitiy
{
    //[CustomEditor(typeof(SOAssetCollectionConfig))]
    public class NSNBuildCollectionDrawer : Editor
    {
       // private SOAssetCollectionConfig _collectionTarget;
        private ReorderableList _dynamicElementsList;
        private SerializedProperty _elements;
        private readonly UnityEditor.AnimatedValues.AnimBool _fadeGroup = new AnimBool(true);

        private void OnEnable()
        {
           // _collectionTarget = target as SOAssetCollectionConfig;
            _elements = serializedObject.FindProperty("elements");

            _fadeGroup.valueChanged.AddListener(this.Repaint);

            _dynamicElementsList =
                new ReorderableList(serializedObject, _elements, true, false, true, true)
                {
                    elementHeight = 25
                };

            _dynamicElementsList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                DrawElementCallback(_dynamicElementsList, rect, index);
            };

            /*
            _dynamicElementsList.drawHeaderCallback = (rect) =>
                EditorGUI.LabelField(rect, _dynamicElementsList.serializedProperty.displayName);*/

        }

        private void OnDisable()
        {
            _fadeGroup.valueChanged.RemoveListener(this.Repaint);
        }

        public override void OnInspectorGUI()
        {
            //if (_collectionTarget == null) return;

            EditorGUILayout.BeginVertical(GUI.skin.box);
            serializedObject.Update();

            _dynamicElementsList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.EndVertical();

            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }

        private void DrawElementCallback(ReorderableList list, Rect rect, int index)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.height -= 4;
            rect.y += 2;
            var width = rect.width;
            var perWidth = width / 3;
            var folderProperty = element.FindPropertyRelative("folder");
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, perWidth, EditorGUIUtility.singleLineHeight),
                folderProperty,
                GUIContent.none
            );
            var packType = element.FindPropertyRelative("packType");
            EditorGUI.PropertyField(
                new Rect(rect.x + perWidth, rect.y, perWidth, EditorGUIUtility.singleLineHeight),
                packType,
                GUIContent.none
            );
            var labelType = element.FindPropertyRelative("labelType");
            EditorGUI.PropertyField(
                new Rect(rect.x + perWidth * 2, rect.y, perWidth, EditorGUIUtility.singleLineHeight),
                labelType,
                GUIContent.none
            );
            EditorGUI.PropertyField(rect, element);
        }
    }
}