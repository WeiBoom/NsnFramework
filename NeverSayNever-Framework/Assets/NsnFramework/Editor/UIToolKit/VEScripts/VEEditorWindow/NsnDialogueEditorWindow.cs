using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueEditorWindow : NsnBaseEditorWindow
    {
        private static NsnDialogueEditorWindow m_DialogueWindow;

        private NsnDialogueGraphView m_DialogueGraphView;

        [MenuItem("Nsn/ToolKit/Dialogue &#D")]
        public static void ShowWindow()
        {
            Display(ref m_DialogueWindow ," Dialogue System");
        }

        protected override void OnCreateGUI()
        {
            // base.OnCreateGUI();
            m_DialogueGraphView = new NsnDialogueGraphView(this);
            m_DialogueGraphView.StretchToParentSize();
            m_Root.Add(m_DialogueGraphView);

        }


        public void EnableSaving(bool enable)
        {
            // todo
        }

    }
}

