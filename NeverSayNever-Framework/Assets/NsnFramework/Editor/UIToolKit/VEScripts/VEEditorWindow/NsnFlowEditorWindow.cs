using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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

        private NsnFlowBlackboard m_FlowBlackBoard;
        
        protected override void OnCreateGUI()
        {
            AddWindowVEAssetToRoot();

            InitElements();
            m_Root.StretchToParentSize();
            m_Content.StretchToParentSize();
        }

        private void InitElements()
        {
            m_Content = m_Root.Q<VisualElement>("Content");
            m_FlowGraphView = m_Root.Q<NsnFlowGraphView>("FlowGraphView");
            m_FlowInspectorView = m_Root.Q<NsnFlowInspectorView>("InspectorView");

            InitBlackboard();
        }

        private void InitBlackboard()
        {
            m_FlowBlackBoard = new NsnFlowBlackboard(m_FlowGraphView);
            m_FlowBlackBoard.addItemRequested = blackboard =>
            {
                // todo
            };
            
            m_FlowBlackBoard.editTextRequested = (blackboard, element, arg3) =>
            {
             // todo   
            };
            
            m_FlowBlackBoard.SetPosition(new Rect(10,30,200,300));
            m_FlowGraphView.Add(m_FlowBlackBoard);
        }
        
    }
}

