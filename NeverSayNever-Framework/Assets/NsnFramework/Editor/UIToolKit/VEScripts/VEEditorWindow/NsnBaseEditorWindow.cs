using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.PackageManager.UI;


namespace Nsn.EditorToolKit
{
    public class NsnBaseEditorWindow : EditorWindow
    {
        public static void ForceClose<T>(ref T window) where T : NsnBaseEditorWindow
        {
            if (window != null)
            {
                try { window.Close(); }
                catch { }
                window = null;
            }
        }

        public static void Display<T>(ref T window, string title) where T : NsnBaseEditorWindow
        {
            ForceClose(ref window);
            window = GetWindow<T>(title);
        }

        protected VisualElement m_Root;

        private void CreateGUI()
        {
            m_Root = rootVisualElement;
            OnCreateGUI();
        }

        private void OnEnable()
        {
            if (m_Root == null)
                m_Root = rootVisualElement;
            OnShow();
        }

        private void Update() => OnUpdate();

        private void OnDestroy() => OnClose();


        protected virtual void OnCreateGUI()
        {
            string treeAssetName = this.GetType().Name;
            var visualTree = VEToolKit.LoadVEAssetVisualTree(treeAssetName);
            VisualElement visualElement = visualTree.CloneTree();
            m_Root.Add(visualElement);
            visualElement.StretchToParentSize();
        }

        protected virtual void OnShow() { }

        protected virtual void OnUpdate(){}
        
        protected virtual void OnClose(){}
    }

    public class NsnBaseEditorWidget : ScriptableObject
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
            ScriptableObject.DestroyImmediate(this);
        }
    }
}

