using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueGraphView : NsnBaseGraphView
    {
        private NsnDialogueEditorWindow m_DialogueEditorWindow;
        
        private int m_NameErrorAmount = 0;
        public int NameErrorAmount
        {
            get { return m_NameErrorAmount; }
            set { 
                m_NameErrorAmount = value;
                // m_NameErrorAmount 是递增的,只要曾大于1，就设为false，不用一直刷新
                if(NameErrorAmount == 0)
                    m_DialogueEditorWindow.EnableSaving(true);
                if(NameErrorAmount == 1)
                    m_DialogueEditorWindow.EnableSaving(false);
            }
        }

        public NsnDialogueGraphView(NsnDialogueEditorWindow dialogueEditorWindow)
        {
            m_DialogueEditorWindow = dialogueEditorWindow;

            var styleSheet = VEToolKit.LoadVEAssetStyleSheet("NsnDialogueGraphView");

            this.styleSheets.Add(styleSheet);
        }

        public void AddGroupedNode(NsnDialogueNode node, NsnDialogueGroup group)
        {
            // todo
        }

        public void AddUngroupedNode(NsnDialogueNode node)
        {
            // todo
        }

        public void RemoveGroupedNode(NsnDialogueNode node, NsnDialogueGroup group)
        {
            // todo
        }

        public void RemoveUngroupedNode(NsnDialogueNode node)
        {
            // todo
        }
    }
}

