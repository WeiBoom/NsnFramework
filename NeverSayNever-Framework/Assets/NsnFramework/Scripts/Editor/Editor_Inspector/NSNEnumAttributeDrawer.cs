using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


namespace NeverSayNever.EditorUtilitiy
{

    [CustomPropertyDrawer(typeof(NSNEnumAttribute))]
    public class NSNEnumAttributeDrawer : PropertyDrawer
    {
        private readonly List<string> _displayNames = new List<string>();

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 替换属性名称
            NSNEnumAttribute enumAttribute = (NSNEnumAttribute)attribute;
            label.text = enumAttribute.Name;

            var isElement = Regex.IsMatch(property.displayName, "Element \\d+");
            if (isElement)
            {
                label.text = property.displayName;
            }

            if (property.propertyType == SerializedPropertyType.Enum)
            {
                DrawEnum(position, property, label);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        /// <summary>
        /// 重新绘制枚举类型属性
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        private void DrawEnum(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            var type = fieldInfo.FieldType;

            string[] names = property.enumNames;
            string[] values = new string[names.Length];
            while (type.IsArray)
            {
                type = type.GetElementType();
            }

            for (var i = 0; i < names.Length; ++i)
            {
                var info = type.GetField(names[i]);
                NSNEnumAttribute[] enumAttributes = (NSNEnumAttribute[])info.GetCustomAttributes(typeof(NSNEnumAttribute), false);
                values[i] = enumAttributes.Length == 0 ? names[i] : enumAttributes[0].Name;
            }

            var index = EditorGUI.Popup(position, label.text, property.enumValueIndex, values);
            if (EditorGUI.EndChangeCheck() && index != -1)
            {
                property.enumValueIndex = index;
            }
        }
    }
}