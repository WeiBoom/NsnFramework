using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


namespace Nsn.UIToolKit
{
    public enum VEAssetType
    {
        uss,
        uxml,
    }

    public class BaseEditorWidget : ScriptableObject
    {
        private const string m_BEWindowAssetRootPath = "Assets/NsnFramework/Editor/UIToolKit/VEAssets";

        protected VisualElement m_Root;

        public static VisualTreeAsset LoadEditorVisualAsset(string assetName, VEAssetType assType)
        {
            string assetType = assType.ToString();
            string path = $"{m_BEWindowAssetRootPath}/{assetType}/{assetName}.{assetType}";
            var asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
            if (asset == null)
                throw new System.Exception("[Nsn.UIToolKit] load editor asset failed . assetName : " + assetName);
            return asset;
        }

        public void OnCreateWindow(VisualElement root)
        {
            m_Root = root;
            OnCreateGUI();
        }

        protected virtual void OnCreateGUI()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnClose()
        {
            ScriptableObject.Destroy(this);
        }
    }
}

