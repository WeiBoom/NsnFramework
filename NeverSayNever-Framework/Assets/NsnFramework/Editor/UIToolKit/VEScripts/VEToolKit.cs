using System.Collections.Generic;
using System.IO;
using System;

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
        #region VisualElement

        public static VisualTreeAsset LoadEditorVEAsset(string assetName, VEAssetType assType)
        {
            string assetType = assType.ToString();
            string path = $"{NEditorConst.NsnUIToolKitAssetRootPath}/{assetType}/{assetName}.{assetType}";
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            if (asset == null)
                Debug.LogError("[X3.UIToolKit] load editor asset failed . assetName : " + assetName);
            return asset;
        }
        
        
        public static VisualElement LoadVETemplateAsset(string templateName)
        {
            string path = $"{NEditorConst.NsnUIToolKitAssetRootPath}/uxml/{templateName}.uxml";
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
    }
}