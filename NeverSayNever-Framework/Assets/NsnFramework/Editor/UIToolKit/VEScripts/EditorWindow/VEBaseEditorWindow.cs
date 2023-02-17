using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;


namespace Nsn.EditorToolKit
{
    public class VEBaseEditorWindow : EditorWindow
    {
        protected VisualElement m_Root;

        private void CreateGUI()
        {
            m_Root = rootVisualElement;
            OnCreateGUI();
        }

        protected virtual void OnCreateGUI()
        {
            string treeAssetName = this.GetType().Name;
            var visualTree = VEToolKit.LoadEditorVEAsset(treeAssetName,VEAssetType.uxml);
            VisualElement visualElement = visualTree.CloneTree();
            m_Root.Add(visualElement);
        }
        
        private void Update() => OnUpdate();

        private void OnDestroy() => OnClose();
        
        protected virtual void OnUpdate(){}
        
        protected virtual void OnClose(){}
    }

    public class VEBaseEditorWidget : ScriptableObject
    {
        protected VisualElement m_Root;
        
        public void OnCreateWindow(VisualElement root)
        {
            m_Root = root;
            OnCreateGUI();
        }

        protected virtual void OnCreateGUI() { }

        public virtual void OnUpdate() { }

        public virtual void OnClose()
        {
            ScriptableObject.Destroy(this);
        }
    }
}

