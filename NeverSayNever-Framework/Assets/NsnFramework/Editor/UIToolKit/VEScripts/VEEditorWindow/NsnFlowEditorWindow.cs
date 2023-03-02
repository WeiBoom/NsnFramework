using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnFlowEditorWindow : NsnBaseEditorWindow
    {
        private static NsnFlowEditorWindow m_Window;

        [MenuItem("Nsn/ToolKit/FlowGraph &#F")]
        private static void ShowWindow()
        {
            Display(ref m_Window, "Nsn-FlowGraph(Alt + Shift + F)");
        }

        private VisualElement m_Content;
        private NsnFlowGraphView m_FlowGraphView;
        private NsnFlowInspectorView m_FlowInspectorView;

        protected override void OnCreateGUI()
        {
            base.OnCreateGUI();
            InitElements();

            m_Root.StretchToParentSize();
            m_Content.StretchToParentSize();
        }

        private void InitElements()
        {
            m_Content = m_Root.Q<VisualElement>("Content");
            m_FlowGraphView = m_Root.Q<NsnFlowGraphView>("FlowGraphView");
            m_FlowInspectorView = m_Root.Q<NsnFlowInspectorView>("InspectorView");
        }
    }
}

