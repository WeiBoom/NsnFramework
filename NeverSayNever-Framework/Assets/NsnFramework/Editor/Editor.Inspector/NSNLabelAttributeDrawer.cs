using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace NeverSayNever.EditorUtilitiy
{

    [CustomPropertyDrawer(typeof(NSNLabelAttribute))]
    public class NSNLabelAttributeDrawer : PropertyDrawer
    {
        private GUIContent _contentLabel;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_contentLabel == null)
            {
                var name = (attribute as NSNLabelAttribute)?.Name;
                _contentLabel = new GUIContent(name);
            }

            EditorGUI.PropertyField(position, property, _contentLabel);
        }
    }
}

