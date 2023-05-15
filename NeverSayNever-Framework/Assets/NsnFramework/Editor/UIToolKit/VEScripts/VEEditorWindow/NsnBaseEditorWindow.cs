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

        protected string m_BindAssetName = string.Empty;

        protected string m_BindAssetFolderPath = string.Empty;

        protected string BindAssetName
        {
            get
            {
                if (m_BindAssetName == string.Empty)
                    m_BindAssetName = this.GetType().Name;
                return m_BindAssetName;
            }
        }

        protected string BindAssetFolderPath
        {
            get
            {
                if (m_BindAssetFolderPath == string.Empty)
                    m_BindAssetFolderPath = NEditorConst.NsnToolKitAssetRootPath;
                return m_BindAssetFolderPath;
            }
        }



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


        protected virtual void OnCreateGUI() { }

        protected virtual void OnShow() { }

        protected virtual void OnUpdate(){}
        
        protected virtual void OnClose(){}

        protected void AddWindowVEAssetToRoot()
        {
            string treeAssetName = this.GetType().Name;
            var visualTree = VEToolKit.LoadVEAssetVisualTree(treeAssetName);
            VisualElement visualElement = visualTree.CloneTree();
            m_Root.Add(visualElement);
            visualElement.StretchToParentSize();
        }
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

