using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Nsn.EditorToolKit
{
    public class NsnDialogueSearchEditorWindow : NsnBaseEditorWindow, ISearchWindowProvider
    {
        private NsnDialogueGraphView m_GraphView;
        private Texture2D m_IndentationIcon;

        public void Initialize(NsnDialogueGraphView graphView)
        {
            m_GraphView = graphView;

            m_IndentationIcon = new Texture2D(1, 1);
            m_IndentationIcon.SetPixel(0, 0, Color.clear);
            m_IndentationIcon.Apply();
        }

        #region interface implement

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Elements")),
                new SearchTreeGroupEntry(new GUIContent("Dialogue Nodes"), 1),
                new SearchTreeEntry(new GUIContent("Single Choice", m_IndentationIcon))
                {
                    userData = DialogueNodeType.SingleChoice,
                    level = 2
                },
                new SearchTreeEntry(new GUIContent("Multiple Choice", m_IndentationIcon))
                {
                    userData = DialogueNodeType.MultipleChoice,
                    level = 2
                },
                new SearchTreeGroupEntry(new GUIContent("Dialogue Groups"), 1),
                new SearchTreeEntry(new GUIContent("Single Group", m_IndentationIcon))
                {
                    userData = new Group(),
                    level = 2
                }
            };
            return searchTreeEntries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector2 localMousePosition = m_GraphView.GetLocalMousePosition(context.screenMousePosition, true);

            switch (SearchTreeEntry.userData)
            {
                case DialogueNodeType.SingleChoice:
                    {
                        DialogueSingleChoiceNode singleChoiceNode = (DialogueSingleChoiceNode)m_GraphView.CreateNode("DialogueName", DialogueNodeType.SingleChoice, localMousePosition);
                        m_GraphView.AddElement(singleChoiceNode);
                        return true;
                    }
                case DialogueNodeType.MultipleChoice:
                    {
                        DialogueMultipleChoiceNode multipleChoiceNode = (DialogueMultipleChoiceNode)m_GraphView.CreateNode("DialogueName", DialogueNodeType.MultipleChoice, localMousePosition);
                        m_GraphView.AddElement(multipleChoiceNode);
                        return true;
                    }
                case Group _:
                    {
                        m_GraphView.CreateGroup("DialogueGroup", localMousePosition);
                        return true;
                    }
                default:
                    return false;
            }
        }

        #endregion
    }
}
