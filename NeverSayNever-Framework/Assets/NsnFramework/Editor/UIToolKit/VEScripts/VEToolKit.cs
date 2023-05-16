using System.Collections.Generic;
using System.IO;
using System;

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace Nsn.EditorToolKit
{
    public enum VEAssetType
    {
        uss,
        uxml,
        txt,
    }
    
    public static class VEToolKit
    {

        #region Load VisualElement Asset

        public static T LoadVEAssetVirualTree<T>(string folderPath, string assetName, VEAssetType assetType)
            where T : ScriptableObject
        {
            string path = $"{folderPath}/{assetName}.{assetType}";
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
                Debug.LogError($"[Nsn] load editor asset failed . path : {path}");

            return asset;
        }

        public static string GetVEAssetPath(VEAssetType assetType, string assetName)
        {
            string path = $"{NEditorConst.NsnToolKitAssetRootPath}/{assetType}/{assetName}.{assetType}";
            return path;
        }

        public static VisualTreeAsset LoadVEAssetVisualTree(string assetName)
        {
            var asset = LoadVEAsset<VisualTreeAsset>(assetName, VEAssetType.uxml);
            return asset;
        }


        public static StyleSheet LoadVEAssetStyleSheet(string assetName)
        {
            var asset = LoadVEAsset<StyleSheet>(assetName, VEAssetType.uss);
            return asset;
        }

        public static T LoadVEAsset<T>(string assetName, VEAssetType assType) where T : UnityEngine.Object
        {
            //string assetType = assType.ToString();
            string path = GetVEAssetPath(assType, assetName);//$"{NEditorConst.NsnToolKitAssetRootPath}/{assetType}/{assetName}.{assetType}";
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
                Debug.LogError($"[Nsn] load editor asset failed . path : {path}");

            return asset;
        }

        public static T LoadVEAsset<T>(string path) where T : UnityEngine.Object
        {
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset == null)
                Debug.LogError($"[Nsn] load editor asset failed . path : {path}");
            return asset;
        }
        
        
        public static VisualElement LoadVETemplateAsset(string templateName)
        {
            string path = $"{NEditorConst.NsnToolKitAssetRootPath}/uxml/{templateName}.uxml";
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            VisualElement visualElement = visualTreeAsset.CloneTree();
            return visualElement;
        }
        
        public static T LoadVETemplateElement<T>(string templateName, string elementName) where T : VisualElement
        {
            VisualElement visualElement = LoadVETemplateAsset(templateName);
            T elementObj = visualElement.Q<T>(elementName);
            return elementObj;
        }


        #endregion

        #region Create VisualElement

        public static Button CreateButton(string text, Action onClick = null)
        {
            Button button = new Button(onClick)
            {
                text = text
            };

            return button;
        }

        public static Foldout CreateFoldout(string title, bool collapsed = false)
        {
            Foldout foldout = new Foldout()
            {
                text = title,
                value = !collapsed
            };

            return foldout;
        }

        public static Port CreatePort(this NsnBaseNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
        {
            Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));
            port.portName = portName;

            return port;
        }

        public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textField = new TextField()
            {
                value = value,
                label = label
            };

            if (onValueChanged != null)
                textField.RegisterValueChangedCallback(onValueChanged);

            return textField;
        }

        public static TextField CreateTextArea(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(value, label, onValueChanged);
            textArea.multiline = true;

            return textArea;
        }

        #endregion
    }
}